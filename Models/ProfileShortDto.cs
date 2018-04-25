using System;
using System.Collections.Generic;
using System.Linq;
using kms.Data.Entities;

namespace kms.Models
{
    public class ProfileShortDto
    {
        public ProfileShortDto(Users user)
        {
            if (user != null) {
                UserId = user.UserId;
                Name = user.Name;
                Surname = user.Surname;
                Email = user.Email;
                Avatar = user.Avatar;
                DateRegistered = user.DateRegistered;
            }
        }
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Avatar { get; set; }
        public DateTime? DateRegistered { get; set; }
    }
}
