using Dislinkt.Notifications.Data;
using Dislinkt.Notifications.Domain;
using System;
using System.Threading.Tasks;

namespace Dislinkt.Notifications.Interfaces.Repositories
{
    public interface INotificationRepository
    {

        Task<bool> CreateNotificationSettingsAsync(NewNotificationSettingsData settings);
        Task UpdateNotificationSeenAsync(NotificationSeenUpdateData data);
        Task AddNotification(NotificationSettings settings);
        Task UpdateNotificationSettings(NewNotificationSettingsData settings);
        Task<NotificationSettings> GetAllByUserId(Guid userId);
        Task<NotificationSettings> GetWithoutMessagesByUserId(Guid userId);
        Task<NotificationSettings> GetById(Guid id);

        Task DeleteByUserId(Guid userId);
        Task<Notification> GetNotificationById(Guid id);
    }
}
