using System;
using System.Collections.Generic;

namespace kms.Data.Entities
{
    public partial class TemplateText
    {
        public int TemplateTextId { get; set; }
        public int TemplateId { get; set; }
        public int? EditorId { get; set; }
        public string Content { get; set; }
        public string QuillDelta { get; set; }
        public DateTime? TimeUpdated { get; set; }
        public bool IsActual { get; set; }

        public Users Editor { get; set; }
        public Templates Template { get; set; }
    }
}
