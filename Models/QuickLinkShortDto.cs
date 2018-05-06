using kms.Data.Entities;

namespace kms.Models
{
    public class QuickLinkShortDto
    {
        public QuickLinkShortDto(QuickLinks quickLink)
        {
            if (quickLink != null) {
                QuickLinkId = quickLink.QuickLinkId;
                ProjectId = quickLink.ProjectId;
                DocumentId = quickLink.DocumentId;
                UserId = quickLink.UserId;
                ExternalLink = quickLink.ExternalLink;
                Name = quickLink.Name;

                if (quickLink.Project != null) {
                    ProjectSlug = quickLink.Project.Slug;
                }

                if (quickLink.Document != null) {
                    DocumentSlug = quickLink.Document.Slug;
                }
            }
        }
        public int QuickLinkId { get; set; }
        public int? ProjectId { get; set; }
        public int? DocumentId { get; set; }
        public int? UserId { get; set; }
        public string ExternalLink { get; set; }
        public string Name { get; set; }
        public string ProjectSlug { get; set; }
        public string DocumentSlug { get; set; }
    }
}
