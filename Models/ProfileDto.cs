using System;
using System.Collections.Generic;
using System.Linq;
using kms.Data.Entities;

namespace kms.Models
{
    public class ProfileDto
    {
        public ProfileDto(Users user, ICollection<Roles> roles = null, ICollection<Permissions> permissions = null)
        {
            if (user != null) {
                UserId = user.UserId;
                Name = user.Name;
                Surname = user.Surname;
                Email = user.Email;
                Avatar = user.Avatar;
                DateRegistered = user.DateRegistered;
            }

            if (roles != null) {
                Roles = roles.Select(r => r.Name).ToList();
            }

            if (permissions != null) {
                Permissions = permissions.Select(p => p.PermissionSlug).ToList();
            }
        }
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Avatar { get; set; }
        public DateTime? DateRegistered { get; set; }
        public ICollection<string> Roles { get; set; }
        public ICollection<string> Permissions { get; set; }
    }
}
