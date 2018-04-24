using System;
using System.Collections.Generic;

namespace kms.Data.Entities
{
    public partial class UserRoles
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }

        public Roles Role { get; set; }
        public Users User { get; set; }
    }
}
