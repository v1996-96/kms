using System;
using System.Collections.Generic;
using NpgsqlTypes;

namespace kms.Data.Entities
{
    public partial class Templates
    {
        public Templates()
        {
            TemplateText = new HashSet<TemplateText>();
        }

        public int TemplateId { get; set; }
        public string TemplateTypeSlug { get; set; }
        public int? ProjectId { get; set; }
        public int? CreatorId { get; set; }
        public string Slug { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DateCreated { get; set; }
        public NpgsqlTsVector TemplateTsv { get; set; }

        public Users Creator { get; set; }
        public Projects Project { get; set; }
        public TemplateTypes TemplateTypeSlugNavigation { get; set; }
        public ICollection<TemplateText> TemplateText { get; set; }
    }
}
