using System;
using kms.Data.Entities;

namespace kms.Models
{
    public class DocumentDto
    {
        public DocumentDto(Documents document, string content = "", int likesCount = 0, string quillDelta = "")
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

                if (document.Creator != null) {
                    Creator = new UserShortDto(document.Creator);
                }

                if (document.Project != null) {
                    ProjectName = document.Project.Name;
                    ProjectSlug = document.Project.Slug;
                }

                if (document.ParentDocument != null) {
                    ParentDocumentSlug = document.ParentDocument.Slug;
                }
            }

            Content = content;
            LikesCount = likesCount;
            QuillDelta = quillDelta;
        }
        public int DocumentId { get; set; }
        public int? ParentDocumentId { get; set; }
        public string ParentDocumentSlug { get; set; }
        public int? CreatorId { get; set; }
        public string Slug { get; set; }
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string Content { get; set; }
        public string QuillDelta { get; set; }
        public DateTime DateCreated { get; set; }
        public bool IsDraft { get; set; }
        public int LikesCount { get; set; }
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public string ProjectSlug { get; set; }
        public UserShortDto Creator { get; set; }
    }
}
