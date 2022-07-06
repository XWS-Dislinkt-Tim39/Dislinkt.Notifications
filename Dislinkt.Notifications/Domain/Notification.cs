using System;

namespace Dislinkt.Notifications.Domain
{
    public class Notification
    {
        public Guid Id { get; }
        public Guid From { get; }

        public Type Type { get; }
        public Notification(Guid id, Guid from, Type type)
        {
            Id = id;
            From = from;
            Type = type;
        }

    }
}
