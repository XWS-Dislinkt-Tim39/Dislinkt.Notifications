using Dislinkt.Notifications.Domain;
using System;
using System.Linq;
using Type = Dislinkt.Notifications.Domain.Type;

namespace Dislinkt.Notifications.MongoDB.Entities
{
    public class NotificationEntity : BaseEntity
    {
        public Guid From { get; set; }

        public Type Type { get; set; }

        public static NotificationEntity ToNotificationEntity(Notification notification)
        {
            return new NotificationEntity
            {
                Id = notification.Id,
                From = notification.From,
                Type = notification.Type,

            };

        }
        public static NotificationEntity[] ToNotificationEntities(Notification[] notifications)
         => notifications.Select(p => ToNotificationEntity(p)).ToArray();

        public Notification ToNotification()
            => new Notification(this.Id, this.From, this.Type);

    }
}
