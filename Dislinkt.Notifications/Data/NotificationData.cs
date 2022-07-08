

using Dislinkt.Notifications.Domain;
using System;
using Type = Dislinkt.Notifications.Domain.Type;

namespace Dislinkt.Notifications.Data
{
    public class NotificationData
    {

        public Guid UserId { get; set; }
        public Guid From { get; set; }
        public bool Seen { get; set; }

        public Type Type { get; set; }
    }
}
