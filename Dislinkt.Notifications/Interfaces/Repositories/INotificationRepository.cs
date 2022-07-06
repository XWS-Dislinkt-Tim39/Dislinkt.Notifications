using Dislinkt.Notifications.Data;
using Dislinkt.Notifications.Domain;
using System;
using System.Threading.Tasks;

namespace Dislinkt.Notifications.Interfaces.Repositories
{
    public interface INotificationRepository
    {

        Task CreateNotificationSettingsAsync(NewNotificationSettingsData settings);

        Task AddNotification(NotificationSettings settings);
        Task UpdateNotificationSettings(NewNotificationSettingsData settings);
        Task<NotificationSettings> GetByUserId(Guid userId);
        Task<NotificationSettings> GetById(Guid id);
    }
}
