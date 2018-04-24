using System;
using System.Collections.Generic;

namespace kms.Data.Entities
{
    public partial class InviteTokens
    {
        public int InviteTokenId { get; set; }
        public int UserId { get; set; }
        public string Token { get; set; }
        public DateTime TimeCreated { get; set; }

        public Users User { get; set; }
    }
}
