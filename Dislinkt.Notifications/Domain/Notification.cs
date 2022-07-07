using System;

namespace Dislinkt.Notifications.Domain
{
    public class Notification
    {
        public Guid Id { get; }
        public Guid From { get; }

        public bool Seen { get; }

        public Type Type { get; }
        public Notification(Guid id, Guid from, Type type, bool seen)
        {
            Id = id;
            From = from;
            Type = type;
            Seen = seen;
        }

    }
}
