using System;

namespace Dislinkt.Notifications.Domain
{
    public class NotificationSettings
    {
        public Guid Id { get; }

        public Guid UserId { get; }

        public bool MessageOn { get; }
        public bool PostOn { get; }
        public bool JobOn { get; }
        public bool FriendRequestOn { get; }
        public Notification[] Notifications { get; }

        public NotificationSettings(Guid id, Guid userId, bool messageOn, bool postOn, bool jobOb, bool friendRequeston, Notification[] notifications)
        {
            Id = id;
            UserId = userId;
            MessageOn = messageOn;
            PostOn= postOn;
            JobOn= jobOb;
            FriendRequestOn= friendRequeston;
            Notifications = notifications;
        }
    }
}
