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

                if (member.User != null) {
                    Name = member.User.Name;
                    Surname = member.User.Surname;
                    Avatar = member.User.Avatar;
                }
            }
        }
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Avatar { get; set; }
        public DateTime DateJoined { get; set; }
        public string Position { get; set; }
        public ProjectRoleShortDto ProjectRole { get; set; }
    }
}
