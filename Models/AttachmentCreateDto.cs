using System;
using Microsoft.AspNetCore.Http;

namespace kms.Models
{
    public class AttachmentCreateDto
    {
        public string Name { get; set; }
        public IFormFile File { get; set; }
    }
}
