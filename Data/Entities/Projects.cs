using System;
using System.Collections.Generic;

namespace kms.Data.Entities
{
    public partial class Projects
    {
        public Projects()
        {
            Activity = new HashSet<Activity>();
            Documents = new HashSet<Documents>();
            FollowedProjects = new HashSet<FollowedProjects>();
            ProjectTeam = new HashSet<ProjectTeam>();
            QuickLinksHousingProject = new HashSet<QuickLinks>();
            QuickLinksProject = new HashSet<QuickLinks>();
            Templates = new HashSet<Templates>();
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

        public ICollection<Activity> Activity { get; set; }
        public ICollection<Documents> Documents { get; set; }
        public ICollection<FollowedProjects> FollowedProjects { get; set; }
        public ICollection<ProjectTeam> ProjectTeam { get; set; }
        public ICollection<QuickLinks> QuickLinksHousingProject { get; set; }
        public ICollection<QuickLinks> QuickLinksProject { get; set; }
        public ICollection<Templates> Templates { get; set; }
    }
}
