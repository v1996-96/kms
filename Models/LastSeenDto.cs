using System;
using kms.Data.Entities;

namespace kms.Models
{
    public class LastSeenDto
    {
        public LastSeenDto(LastSeenDocuments lastSeen)
        {
            if (lastSeen != null) {
                LastSeenDocumentsId = lastSeen.LastSeenDocumentsId;
                UserId = lastSeen.UserId;
                TimeCreated = lastSeen.TimeCreated;
                DocumentId = lastSeen.DocumentId;

                if (lastSeen.Document != null) {
                    DocumentTitle = lastSeen.Document.Title;
                    DocumentSlug = lastSeen.Document.Slug;
                }
            }
        }

        public int LastSeenDocumentsId { get; set; }
        public int UserId { get; set; }
        public DateTime TimeCreated { get; set; }
        public int DocumentId { get; set; }
        public string DocumentTitle { get; set; }
        public string DocumentSlug { get; set; }
    }
}
