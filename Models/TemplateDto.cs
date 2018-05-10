using System;
using kms.Data.Entities;

namespace kms.Models
{
    public class TemplateDto
    {
        public TemplateDto(Templates template, string content = "", string quillDelta = "")
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

                if (template.Creator != null) {
                    Creator = new UserShortDto(template.Creator);
                }

                if (template.TemplateTypeSlugNavigation != null) {
                    System = template.TemplateTypeSlugNavigation.System;
                }

                if (template.Project != null) {
                    ProjectName = template.Project.Name;
                    ProjectSlug = template.Project.Slug;
                }
            }

            Content = content;
            QuillDelta = quillDelta;
        }
        public int TemplateId { get; set; }
        public string TemplateType { get; set; }
        public int? ProjectId { get; set; }
        public string ProjectName { get; set; }
        public string ProjectSlug { get; set; }
        public int? CreatorId { get; set; }
        public string Slug { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public string QuillDelta { get; set; }
        public DateTime DateCreated { get; set; }
        public UserShortDto Creator { get; set; }
        public bool System { get; set; }
    }
}
