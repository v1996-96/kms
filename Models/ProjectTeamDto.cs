using System;
using kms.Data.Entities;

namespace kms.Models
{
    public class ProjectTeamDto
    {
        public ProjectTeamDto(ProjectTeam member)
        {
            if (member != null) {
                UserId = member.UserId;
                DateJoined = member.DateJoined;
                Position = member.Position;

                if (member.ProjectRole != null) {
                    ProjectRole = new ProjectRoleShortDto(member.ProjectRole);
                }
            }
        }
        public int UserId { get; set; }
        public DateTime DateJoined { get; set; }
        public string Position { get; set; }
        public ProjectRoleShortDto ProjectRole { get; set; }
    }
}
