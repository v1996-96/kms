using System;
using kms.Data.Entities;

namespace kms.Models
{
    public class ProjectShortDto
    {
        public ProjectShortDto(Projects project)
        {
            if (project != null) {
                ProjectId = project.ProjectId;
                Slug = project.Slug;
                Name = project.Name;
                Description = project.Description;
                Goal = project.Goal;
                DateStart = project.DateStart;
                DateEnd = project.DateEnd;
                Avatar = project.Avatar;
                IsOpen = project.IsOpen;
                IsActive = project.IsActive;
            }
        }
        public int ProjectId { get; set; }
        public string Slug { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Goal { get; set; }
        public DateTime? DateStart { get; set; }
        public DateTime? DateEnd { get; set; }
        public string Avatar { get; set; }
        public bool IsOpen { get; set; }
        public bool IsActive { get; set; }
    }
}
