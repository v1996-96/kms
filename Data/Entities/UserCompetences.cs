using System;
using System.Collections.Generic;

namespace kms.Data.Entities
{
    public partial class UserCompetences
    {
        public int CompetenceId { get; set; }
        public int UserId { get; set; }

        public Competences Competence { get; set; }
        public Users User { get; set; }
    }
}
