using System;
using System.Collections.Generic;
using System.Linq;
using kms.Data.Entities;

namespace kms.Models
{
    public class UserShortDto
    {
        public UserShortDto(Users user)
        {
            if (user != null) {
                UserId = user.UserId;
                Name = user.Name;
                Surname = user.Surname;
                Email = user.Email;
                Avatar = user.Avatar;
                DateRegistered = user.DateRegistered;

                if (user.UserRoles != null && user.UserRoles.Count > 0) {
                    Roles = user.UserRoles.Where(c => c.Role != null).Select(c => new RoleShortDto(c.Role));
                }
            }
        }
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Avatar { get; set; }
        public DateTime? DateRegistered { get; set; }
        public IEnumerable<RoleShortDto> Roles { get; set; }
    }
}
