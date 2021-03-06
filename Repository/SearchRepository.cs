using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using kms.Data;
using kms.Data.Entities;
using kms.Models;
using Microsoft.EntityFrameworkCore;

namespace kms.Repository
{
    public class SearchRepository : ISearchRepository
    {
        private readonly KMSDBContext _db;
        private readonly DbConnection _dbconnection;
        public SearchRepository(KMSDBContext context, IKMSDBConnection connection)
        {
            this._dbconnection = connection.Connection;
            this._db = context;
        }

        public IQueryable<Users> SearchUsers(string query) => SearchUsers(_db.Users, query);
        public IQueryable<Users> SearchUsers(IQueryable<Users> statement, string query)
            => statement.FromSql("select * from users where name % {0} or surname % {0} order by name <-> {0}, surname <-> {0}", query);


        public IQueryable<Projects> SearchProjects(string query) => SearchProjects(_db.Projects, query);
        public IQueryable<Projects> SearchProjects(IQueryable<Projects> statement, string query)
            => statement.FromSql("select * from projects where name % {0} order by name <-> {0}", query);


        public IQueryable<Competences> SearchCompetences(string query) => SearchCompetences(_db.Competences, query);
        public IQueryable<Competences> SearchCompetences(IQueryable<Competences> statement, string query)
            => statement.FromSql("select * from competences where name % {0} order by name <-> {0}", query);


        public IQueryable<Documents> SearchDocuments(string query) => SearchDocuments(_db.Documents, query);
        public IQueryable<Documents> SearchDocuments(IQueryable<Documents> statement, string query)
            => statement.FromSql(@"select * from documents
                where document_tsv @@ phraseto_tsquery(coalesce({0}, ''))
                order by ts_rank(document_tsv, phraseto_tsquery(coalesce({0}, ''))) desc", query);


        public IQueryable<Bookmarks> SearchBookmarks(string query) => SearchBookmarks(_db.Bookmarks, query);
        public IQueryable<Bookmarks> SearchBookmarks(IQueryable<Bookmarks> statement, string query)
            => statement.FromSql(@"select b.* from bookmarks as b
                left join documents as d on d.document_id = b.document_id
                where b.document_id is not null and d.document_tsv @@ phraseto_tsquery(coalesce({0}, ''))
                order by ts_rank(d.document_tsv, phraseto_tsquery(coalesce({0}, ''))) desc", query);


        public IQueryable<FollowedProjects> SearchFollowedProjects(string query) => SearchFollowedProjects(_db.FollowedProjects, query);
        public IQueryable<FollowedProjects> SearchFollowedProjects(IQueryable<FollowedProjects> statement, string query)
            => statement.FromSql(@"select fp.* from followed_projects as fp
                left join projects as p on p.project_id = fp.project_id
                where p.project_id is not null and p.name % {0} order by p.name <-> {0}", query);


        public IQueryable<LastSeenDocuments> SearchLastSeenDocuments(string query) => SearchLastSeenDocuments(_db.LastSeenDocuments, query);
        public IQueryable<LastSeenDocuments> SearchLastSeenDocuments(IQueryable<LastSeenDocuments> statement, string query)
            => statement.FromSql(@"select l.* from last_seen_documents as l
                left join documents as d on d.document_id = l.document_id
                where l.document_id is not null and d.document_tsv @@ phraseto_tsquery(coalesce({0}, ''))
                order by ts_rank(d.document_tsv, phraseto_tsquery(coalesce({0}, ''))) desc", query);


        public IQueryable<ProjectTeam> SearchProjectTeam(string query) => SearchProjectTeam(_db.ProjectTeam, query);
        public IQueryable<ProjectTeam> SearchProjectTeam(IQueryable<ProjectTeam> statement, string query)
            => statement.FromSql(@"select pt.* from project_team as pt
                left join users as u on u.user_id = pt.user_id
                where u.name % {0} or u.surname % {0} order by u.name <-> {0}, u.surname <-> {0}", query);


        public IQueryable<QuickLinks> SearchQuickLinks(string query) => SearchQuickLinks(_db.QuickLinks, query);
        public IQueryable<QuickLinks> SearchQuickLinks(IQueryable<QuickLinks> statement, string query)
            => statement.FromSql("select * from quick_links where name % {0} order by name <-> {0}", query);


        public IQueryable<Templates> SearchTemplates(string query) => SearchTemplates(_db.Templates, query);
        public IQueryable<Templates> SearchTemplates(IQueryable<Templates> statement, string query)
            => statement.FromSql(@"select * from templates
                where template_tsv @@ phraseto_tsquery(coalesce({0}, ''))
                order by ts_rank(template_tsv, phraseto_tsquery(coalesce({0}, ''))) desc", query);

        public IQueryable<TemplateTypes> SearchTemplateTypes(string query) => SearchTemplateTypes(_db.TemplateTypes, query);
        public IQueryable<TemplateTypes> SearchTemplateTypes(IQueryable<TemplateTypes> statement, string query)
            => statement.FromSql("select * from template_types where name % {0} order by name <-> {0}", query);


        public async Task<SearchResultsDto> CommonSearch(string query, int? limit = 50, int? offset = 0)
        {
            var sqlQuery = @"select * from (
                (select
                    p.name as name,
                    p.project_id as id,
                    p.avatar as avatar,
                    null as parent_slug,
                    p.slug as slug,
                    'project' as type,
                    similarity(p.name, @query) as rank
                from projects as p
                where p.name % @query
                order by p.name <-> @query)
                union
                (select
                    d.title as name,
                    d.document_id as id,
                    null as avatar,
                    pr.slug as parent_slug,
                    d.slug as slug,
                    'document' as type,
                    ts_rank(d.document_tsv, phraseto_tsquery(coalesce(@query, ''))) as rank
                from documents as d
                left join projects as pr on pr.project_id = d.project_id
                where d.document_tsv @@ phraseto_tsquery(coalesce(@query, ''))
                order by ts_rank(d.document_tsv, phraseto_tsquery(coalesce(@query, ''))) desc)
                union
                (select
                    (u.name || ' ' || u.surname) as name,
                    u.user_id as id,
                    u.avatar as avatar,
                    null as parent_slug,
                    null as slug,
                    'user' as type ,
                    (similarity(u.name, @query) + similarity(u.surname, @query))/2 as rank
                from users as u
                where u.name % @query or u.surname % @query
                order by u.name <-> @query, u.surname <-> @query)
                ) as results order by results.rank desc";

            var searchResults = await _dbconnection.QueryAsync<SearchResults>(sqlQuery, new { query });

            return new SearchResultsDto{
                Count = searchResults.Count(),
                Results = searchResults.Skip(offset.HasValue ? offset.Value : 0).Take(limit.HasValue ? limit.Value : 50)
            };
        }
    }
}
