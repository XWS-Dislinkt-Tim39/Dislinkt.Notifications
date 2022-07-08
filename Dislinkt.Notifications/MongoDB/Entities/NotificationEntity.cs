using Dislinkt.Notifications.Domain;
using Dislinkt.Notifications.MongoDB.Attributes;
using System;
using System.Linq;
using Type = Dislinkt.Notifications.Domain.Type;

namespace Dislinkt.Notifications.MongoDB.Entities
{
    [CollectionName("Notifications")]
    public class NotificationEntity : BaseEntity
    {
        public Guid From { get; set; }

        public Type Type { get; set; }
        public bool Seen { get; set; }

        public static NotificationEntity ToNotificationEntity(Notification notification)
        {
            return new NotificationEntity
            {
                Id = notification.Id,
                From = notification.From,
                Type = notification.Type,
                Seen = notification.Seen,

            };

        }
        public static NotificationEntity[] ToNotificationEntities(Notification[] notifications)
         => notifications.Select(p => ToNotificationEntity(p)).ToArray();

        public Notification ToNotification()
            => new Notification(this.Id, this.From, this.Type, this.Seen);

    }
}
