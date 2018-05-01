using System;
using kms.Data.Entities;

namespace kms.Models
{
    public class DocumentShortDto
    {
        public DocumentShortDto(Documents document)
        {
            if (document != null) {
                DocumentId = document.DocumentId;
                ParentDocumentId = document.ParentDocumentId;
                ProjectId = document.ProjectId;
                CreatorId = document.CreatorId;
                Slug = document.Slug;
                Title = document.Title;
                Subtitle = document.Subtitle;
                DateCreated = document.DateCreated;
                IsDraft = document.IsDraft;
            }
        }
        public int DocumentId { get; set; }
        public int? ParentDocumentId { get; set; }
        public int ProjectId { get; set; }
        public int? CreatorId { get; set; }
        public string Slug { get; set; }
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public DateTime DateCreated { get; set; }
        public bool IsDraft { get; set; }
        public int LikesCount { get; set; }
    }
}
