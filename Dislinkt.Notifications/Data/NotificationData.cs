﻿

using Dislinkt.Notifications.Domain;
using System;
using Type = Dislinkt.Notifications.Domain.Type;

namespace Dislinkt.Notifications.Data
{
    public class NotificationData
    {

        public Guid NotificationSettingsId { get; set; }
        public Guid From { get; set; }

        public Type Type { get; set; }
    }
}
