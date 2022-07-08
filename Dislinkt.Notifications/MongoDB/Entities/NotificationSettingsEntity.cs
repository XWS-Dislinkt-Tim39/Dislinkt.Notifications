using Dislinkt.Notifications.Domain;
using Dislinkt.Notifications.MongoDB.Attributes;
using System;
using System.Linq;

namespace Dislinkt.Notifications.MongoDB.Entities
{
    [CollectionName("NotificationSettings")]
    public class NotificationSettingsEntity : BaseEntity
    {
        public Guid UserId { get; set; }
        public bool MessageOn { get; set; }
        public bool PostOn { get; set; }
        public bool JobOn { get; set; }
        public bool FriendRequestOn { get; set; }

        public NotificationEntity[] Notifications { get; set; }


        public static NotificationSettingsEntity ToNotificationSettingsEntity(NotificationSettings settings)
        {
            return new NotificationSettingsEntity
            {
                Id = settings.Id,
                UserId = settings.UserId,
                MessageOn = settings.MessageOn,
                PostOn = settings.PostOn,   
                JobOn = settings.JobOn,
                FriendRequestOn=settings.FriendRequestOn,
                Notifications = NotificationEntity.ToNotificationEntities(settings.Notifications),

            };
        }
        public NotificationSettings ToNotificationSettings()
            => new NotificationSettings(this.Id, this.UserId, this.MessageOn,this.PostOn,this.JobOn,this.FriendRequestOn, this.Notifications == null ? Array.Empty<Notification>() : this.Notifications.Select(p => p.ToNotification()).ToArray());

    }
}
