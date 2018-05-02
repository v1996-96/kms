namespace kms.Models
{
    public class ProjectTeamCreateDto
    {
        public int UserId { get; set; }
        public string Position { get; set; }
        public ProjectRoleShortDto ProjectRole { get; set; }
    }
}
