using System;
using System.Collections.Generic;

namespace kms.Data.Entities
{
    public partial class Competences
    {
        public Competences()
        {
            UserCompetences = new HashSet<UserCompetences>();
        }

        public int CompetenceId { get; set; }
        public string Name { get; set; }

        public ICollection<UserCompetences> UserCompetences { get; set; }
    }
}
