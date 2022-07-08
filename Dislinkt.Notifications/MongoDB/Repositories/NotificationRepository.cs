using Dislinkt.Notifications.Data;
using Dislinkt.Notifications.Domain;
using Dislinkt.Notifications.Interfaces.Repositories;
using Dislinkt.Notifications.MongoDB.Common;
using Dislinkt.Notifications.MongoDB.Entities;
using MongoDB.Driver;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Dislinkt.Notifications.MongoDB.Repositories
{
    public class NotificationRepository:INotificationRepository
    {
        private readonly IQueryExecutor _queryExecutor;

        public NotificationRepository(IQueryExecutor queryExecutor)
        {
            _queryExecutor = queryExecutor;
        }

        public async Task CreateNotificationSettingsAsync(NewNotificationSettingsData settings)
        {
            try
            {
                await _queryExecutor.CreateAsync(NotificationSettingsEntity.ToNotificationSettingsEntity(new NotificationSettings(Guid.NewGuid(), settings.UserId, settings.MessageOn, settings.PostOn,settings.JobOn,settings.FriendRequestOn, Array.Empty<Notification>())));
            }
            catch (MongoWriteException ex)
            {
                throw ex;
            }

        }

        public async Task<NotificationSettings> GetById(Guid id)
        {
            var result = await _queryExecutor.FindByIdAsync<NotificationSettingsEntity>(id);

            return result?.ToNotificationSettings() ?? null;
        }

        public async Task<Notification> GetNotificationById(Guid id)
        {
            var result = await _queryExecutor.FindByIdAsync<NotificationEntity>(id);

            return result?.ToNotification() ?? null;
        }

        public async Task<NotificationSettings> GetAllByUserId(Guid userId)
        {
            var filter = Builders<NotificationSettingsEntity>.Filter.Eq(u => u.UserId, userId);
          
            var result= await _queryExecutor.FindAsync(filter);

            return result?.AsEnumerable()?.FirstOrDefault(u => u.UserId == userId)?.ToNotificationSettings() ?? null;

        }

        public async Task<NotificationSettings> GetWithoutMessagesByUserId(Guid userId)
        {
           // var filter = Builders<NotificationSettingsEntity>.Filter.Eq(u => u.UserId, userId);
            var filter2 = Builders<NotificationSettingsEntity>.Filter.ElemMatch(u => u.Notifications, Builders<NotificationEntity>.Filter.Eq(u => u.Seen, false));

            //filter &= filter2;
            var result = await _queryExecutor.FindAsync(filter2);

            return result?.AsEnumerable()?.FirstOrDefault(u => u.UserId == userId)?.ToNotificationSettings() ?? null;
            
        }

        public async Task AddNotification(NotificationSettings settings)
        {
            var filter = Builders<NotificationSettingsEntity>.Filter.Eq(u => u.Id, settings.Id);

            var update = Builders<NotificationSettingsEntity>.Update.Set(u => u.Notifications, NotificationEntity.ToNotificationEntities(settings.Notifications));

            await _queryExecutor.UpdateAsync(filter, update);
        }

        public async Task UpdateNotificationSettings(NewNotificationSettingsData settings)
        {
            var filter = Builders<NotificationSettingsEntity>.Filter.Eq(u => u.UserId, settings.UserId);

            var update = Builders<NotificationSettingsEntity>.Update
                .Set(u => u.MessageOn, settings.MessageOn)
                .Set(u => u.PostOn, settings.PostOn)
                .Set(u => u.JobOn, settings.JobOn)
                .Set(u => u.FriendRequestOn, settings.FriendRequestOn);

            await _queryExecutor.UpdateAsync(filter, update);
        }

        public async Task UpdateNotificationSeenAsync(NotificationSeenUpdateData data)
        {
            var filter = Builders<NotificationSettingsEntity>.Filter.ElemMatch(u => u.Notifications, Builders<NotificationEntity>.Filter.Eq(u => u.Id, data.NotificationId));

            var update = Builders<NotificationSettingsEntity>.Update.Set(u => u.Notifications[-1].Seen, data.Seen);

            await _queryExecutor.UpdateAsync(filter, update);
        }

    }
}
