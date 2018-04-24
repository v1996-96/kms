using System;
using System.Collections.Generic;

namespace kms.Data.Entities
{
    public partial class RefreshTokens
    {
        public int RefreshTokenId { get; set; }
        public int UserId { get; set; }
        public string Token { get; set; }
        public bool Revoked { get; set; }
        public DateTime TimeCreated { get; set; }

        public Users User { get; set; }
    }
}
