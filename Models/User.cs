using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace kms.Models
{
    public class User : BaseModel
    {
        [JsonProperty(PropertyName = "user_id")]
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Avatar { get; set; }
        [JsonIgnore]
        public IEnumerable<Role> Roles { get; set; }
    }
}
