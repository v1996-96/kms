using System;
using System.Collections.Generic;

namespace kms.Data.Entities
{
    public partial class TemplateTypes
    {
        public TemplateTypes()
        {
            Templates = new HashSet<Templates>();
        }

        public string TemplateTypeSlug { get; set; }
        public string Name { get; set; }
        public bool System { get; set; }

        public ICollection<Templates> Templates { get; set; }
    }
}
