using System;
using System.Collections.Generic;

namespace kms.Data.Entities
{
    public partial class DocumentText
    {
        public int DocumentTextId { get; set; }
        public int DocumentId { get; set; }
        public int? EditorId { get; set; }
        public string Content { get; set; }
        public string QuillDelta { get; set; }
        public DateTime? TimeUpdated { get; set; }
        public bool IsActual { get; set; }

        public Documents Document { get; set; }
        public Users Editor { get; set; }
    }
}
