using System.Linq;
using kms.Data.Entities;

namespace kms.Repository
{
    public interface ISearchRepository
    {
        IQueryable<Users> SearchUsers(string query);
        IQueryable<Users> SearchUsers(IQueryable<Users> statement, string query);

        IQueryable<Projects> SearchProjects(string query);
        IQueryable<Projects> SearchProjects(IQueryable<Projects> statement, string query);

        IQueryable<Competences> SearchCompetences(string query);
        IQueryable<Competences> SearchCompetences(IQueryable<Competences> statement, string query);

        IQueryable<Documents> SearchDocuments(string query);
        IQueryable<Documents> SearchDocuments(IQueryable<Documents> statement, string query);

        IQueryable<Bookmarks> SearchBookmarks(string query);
        IQueryable<Bookmarks> SearchBookmarks(IQueryable<Bookmarks> statement, string query);

        IQueryable<FollowedProjects> SearchFollowedProjects(string query);
        IQueryable<FollowedProjects> SearchFollowedProjects(IQueryable<FollowedProjects> statement, string query);

        IQueryable<LastSeenDocuments> SearchLastSeenDocuments(string query);
        IQueryable<LastSeenDocuments> SearchLastSeenDocuments(IQueryable<LastSeenDocuments> statement, string query);

        IQueryable<ProjectTeam> SearchProjectTeam(string query);
        IQueryable<ProjectTeam> SearchProjectTeam(IQueryable<ProjectTeam> statement, string query);

        IQueryable<QuickLinks> SearchQuickLinks(string query);
        IQueryable<QuickLinks> SearchQuickLinks(IQueryable<QuickLinks> statement, string query);


        IQueryable<Templates> SearchTemplates(string query);
        IQueryable<Templates> SearchTemplates(IQueryable<Templates> statement, string query);
    }
}