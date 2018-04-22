using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace kms.Models
{
    public class RefreshToken : BaseModel
    {
        [JsonIgnore]
        public int RefreshTokenId { get; set; }
        public string Token { get; set; }
        public int UserId { get; set; }
        public bool Revoked { get; set; }
        public DateTime TimeCreated { get; set; }
    }
}
