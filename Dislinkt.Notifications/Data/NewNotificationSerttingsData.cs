using System;

namespace Dislinkt.Notifications.Data
{
    public class NewNotificationSerttingsData
    {
        public Guid UserId { get; set; }
        public bool MessageOn { get; set; }
        public bool PostOn { get; set; }
        public bool JobOn { get; set; }
        public bool FriendRequestOn { get; set; }

    }
}
