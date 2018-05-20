using System;
using System.Collections.Generic;
using System.Linq;
using kms.Data.Entities;

namespace kms.Models
{
    public class ProjectDto
    {
        public ProjectDto(Projects project, int membersCount = 0, IEnumerable<string> permissions = null)
        {
            QuickLinks = new List<QuickLinkShortDto>();

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

                if (project.QuickLinksHousingProject != null && project.QuickLinksHousingProject.Count > 0) {
                    QuickLinks = project.QuickLinksHousingProject.Where(q => q != null).Select(q => new QuickLinkShortDto(q));
                }
            }

            MembersCount = membersCount;
            Permissions = permissions;
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
        public int MembersCount { get; set; }
        public IEnumerable<QuickLinkShortDto> QuickLinks { get; set; }
        public IEnumerable<string> Permissions { get; set; }
    }
}
