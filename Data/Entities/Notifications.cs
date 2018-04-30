using System;
using System.Collections.Generic;

namespace kms.Data.Entities
{
    public partial class Notifications
    {
        public long NotificationId { get; set; }
        public int UserId { get; set; }
        public string Content { get; set; }
        public DateTime TimeFired { get; set; }
        public string Meta { get; set; }

        public Users User { get; set; }
    }
}
