using kms.Data.Entities;

namespace kms.Models
{
    public class CompetenceDto
    {
        public CompetenceDto(Competences competence)
        {
            if (competence != null) {
                CompetenceId = competence.CompetenceId;
                Name = competence.Name;
            }
        }
        public int CompetenceId { get; set; }
        public string Name { get; set; }
    }
}
