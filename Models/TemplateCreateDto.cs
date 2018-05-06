namespace kms.Models
{
    public class TemplateCreateDto
    {
        public int? ProjectId { get; set; }
        public string TemplateType { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public string QuillDelta { get; set; }
    }
}
