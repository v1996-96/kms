using System;
using System.Collections.Generic;

namespace kms.Data.Entities
{
    public partial class NotificationsRead
    {
        public int UserId { get; set; }
        public DateTime TimeRead { get; set; }

        public Users User { get; set; }
    }
}
