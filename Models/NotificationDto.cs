using System;
using kms.Data.Entities;

namespace kms.Models
{
    public class NotificationDto
    {
        public NotificationDto(Notifications notification, bool read)
        {
            NotificationId = notification.NotificationId;
            Content = notification.Content;
            TimeFired = notification.TimeFired;
            Meta = notification.Meta;
            Read = read;
        }

        public long NotificationId { get; set; }
        public string Content { get; set; }
        public DateTime TimeFired { get; set; }
        public string Meta { get; set; }
        public bool Read { get; set; }
    }
}
