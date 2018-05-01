using kms.Data.Entities;

namespace kms.Models
{
    public class QuickLinkDto
    {
        public QuickLinkDto(QuickLinks quickLink)
        {
            if (quickLink != null) {
                QuickLinkId = quickLink.QuickLinkId;
                ProjectId = quickLink.ProjectId;
                DocumentId = quickLink.DocumentId;
                UserId = quickLink.UserId;
                ExternalLink = quickLink.ExternalLink;
                Name = quickLink.Name;
                HousingProjectId = quickLink.HousingProjectId;

                if (quickLink.Project != null) {
                    Project = new ProjectShortDto(quickLink.Project);
                }

                if (quickLink.Document != null) {
                    Document = new DocumentShortDto(quickLink.Document);
                }
            }
        }
        public int QuickLinkId { get; set; }
        public int? ProjectId { get; set; }
        public int? DocumentId { get; set; }
        public int? UserId { get; set; }
        public string ExternalLink { get; set; }
        public string Name { get; set; }
        public int? HousingProjectId { get; set; }
        public ProjectShortDto Project { get; set; }
        public DocumentShortDto Document { get; set; }
    }
}
