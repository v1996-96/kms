using System;
using System.Collections.Generic;

namespace kms.Data.Entities
{
    public partial class NotificationTypes
    {
        public NotificationTypes()
        {
            Notifications = new HashSet<Notifications>();
        }

        public int NotificationTypeId { get; set; }
        public string Name { get; set; }

        public ICollection<Notifications> Notifications { get; set; }
    }
}
