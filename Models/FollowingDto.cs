using System;
using kms.Data.Entities;

namespace kms.Models
{
    public class FollowingDto
    {
        public FollowingDto(FollowedProjects following)
        {
            if (following != null) {
                FollowingId = following.FollowedProjectsId;
                UserId = following.UserId;
                ProjectId = following.ProjectId;
                TimeCreated = following.TimeCreated;

                if (following.Project != null) {
                    Avatar = following.Project.Avatar;
                    ProjectSlug = following.Project.Slug;
                    ProjectName = following.Project.Name;
                }
            }
        }

        public int FollowingId { get; set; }
        public int UserId { get; set; }
        public int ProjectId { get; set; }
        public DateTime TimeCreated { get; set; }
        public string Avatar { get; set; }
        public string ProjectSlug { get; set; }
        public string ProjectName { get; set; }
    }
}
