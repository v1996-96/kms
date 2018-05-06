using System;
using System.Collections.Generic;
using NpgsqlTypes;

namespace kms.Data.Entities
{
    public partial class Documents
    {
        public Documents()
        {
            Attachments = new HashSet<Attachments>();
            Bookmarks = new HashSet<Bookmarks>();
            Comments = new HashSet<Comments>();
            DocumentLikes = new HashSet<DocumentLikes>();
            DocumentText = new HashSet<DocumentText>();
            InverseParentDocument = new HashSet<Documents>();
            LastSeenDocuments = new HashSet<LastSeenDocuments>();
            QuickLinks = new HashSet<QuickLinks>();
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
        public NpgsqlTsVector DocumentTsv { get; set; }

        public Users Creator { get; set; }
        public Documents ParentDocument { get; set; }
        public Projects Project { get; set; }
        public ICollection<Attachments> Attachments { get; set; }
        public ICollection<Bookmarks> Bookmarks { get; set; }
        public ICollection<Comments> Comments { get; set; }
        public ICollection<DocumentLikes> DocumentLikes { get; set; }
        public ICollection<DocumentText> DocumentText { get; set; }
        public ICollection<Documents> InverseParentDocument { get; set; }
        public ICollection<LastSeenDocuments> LastSeenDocuments { get; set; }
        public ICollection<QuickLinks> QuickLinks { get; set; }
    }
}
