using kms.Data.Entities;

namespace kms.Models
{
    public class TemplateTypeDto
    {
        public TemplateTypeDto(TemplateTypes type)
        {
            if (type != null) {
                TemplateTypeSlug = type.TemplateTypeSlug;
                Name = type.Name;
                System = type.System;
            }
        }
        public string TemplateTypeSlug { get; set; }
        public string Name { get; set; }
        public bool System { get; set; }
    }
}
