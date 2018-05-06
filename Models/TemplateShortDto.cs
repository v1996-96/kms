using System;
using kms.Data.Entities;

namespace kms.Models
{
    public class TemplateShortDto
    {
        public TemplateShortDto(Templates template)
        {
            if (template != null) {
                TemplateId = template.TemplateId;
                TemplateType = template.TemplateTypeSlug;
                ProjectId = template.ProjectId;
                CreatorId = template.CreatorId;
                Slug = template.Slug;
                Title = template.Title;
                Description = template.Description;
                DateCreated = template.DateCreated;
            }
        }
        public int TemplateId { get; set; }
        public string TemplateType { get; set; }
        public int? ProjectId { get; set; }
        public int? CreatorId { get; set; }
        public string Slug { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
