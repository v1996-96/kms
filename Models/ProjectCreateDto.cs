using System;
using System.Collections;
using System.Collections.Generic;

namespace kms.Models
{
    public class ProjectCreateDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Goal { get; set; }
        public DateTime? DateStart { get; set; }
        public DateTime? DateEnd { get; set; }
        public string Avatar { get; set; }
        public bool IsOpen { get; set; }
        public bool IsActive { get; set; }
        public IEnumerable<ProjectTeamCreateDto> Team { get; set; }
    }
}
