using System;

namespace kms.Models
{
    public class DocumentCreateDto
    {
        public int? ParentDocumentId { get; set; }
        public int ProjectId { get; set; }
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string Content { get; set; }
        public bool IsDraft { get; set; }
        public string QuillDelta { get; set; }
    }
}
