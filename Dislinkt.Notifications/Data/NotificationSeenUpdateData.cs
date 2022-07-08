using System;

namespace Dislinkt.Notifications.Data
{
    public class NotificationSeenUpdateData
    {
        public Guid UserId { get; set; }
        public Guid NotificationId { get; set; }

        public bool Seen { get; set; }
    }
}
