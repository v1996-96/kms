namespace kms.Models
{
    public class QuickLinkCreateDto
    {
        public string Name { get; set; }
        public int? HousingProjectId { get; set; }
        public int? ProjectId { get; set; }
        public int? DocumentId { get; set; }
        public string ExternalLink { get; set; }
    }
}
