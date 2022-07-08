
using Dislinkt.Notifications.Data;
using Dislinkt.Notifications.Domain;
using Dislinkt.Notifications.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Dislinkt.Notifications.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NottificationController : Controller
    {
        private readonly INotificationRepository _notificationRepository;
        public NottificationController(INotificationRepository notificationRepository)
        {

            _notificationRepository = notificationRepository;
        }

        [HttpPost]
        [Route("/create-notification-settings")]
        public async Task CreateNotificationSettings([FromBody] NewNotificationSettingsData settings)
        {
            await _notificationRepository.CreateNotificationSettingsAsync(settings);

        }


        [HttpPost]
        [Route("/update-notification-settings")]
        public async Task UpdateNotificationSettings([FromBody] NewNotificationSettingsData settings)
        {
            await _notificationRepository.UpdateNotificationSettings(settings);

        }

        [HttpGet]
        [Route("/get-all-by-userId")]
        public async Task<NotificationSettings> GetAll(Guid userId)
        {
            var result = await _notificationRepository.GetAllByUserId(userId);

            return result;
        }
        [HttpGet]
        [Route("/get-witout-messages-by-userId")]
        public async Task<NotificationSettings> GetWithoutMessages(Guid userId)
        {
            var result = await _notificationRepository.GetWithoutMessagesByUserId(userId);

            return result;
        }

        [HttpPost]
        [Route("/add-notification")]
        public async Task<bool> AddNewInterest(NotificationData newNotification)
        {
            var existingSettings = await _notificationRepository.GetById(newNotification.NotificationSettingsId);

            if (existingSettings == null) return false;

            var updatedNotifications = existingSettings.Notifications.Append(new Domain.Notification(Guid.NewGuid(), newNotification.From, newNotification.Type,newNotification.Seen)).ToArray();

            await _notificationRepository.AddNotification(new NotificationSettings(existingSettings.Id, existingSettings.UserId, existingSettings.MessageOn, existingSettings.PostOn,existingSettings.JobOn,existingSettings.FriendRequestOn, updatedNotifications));

            return true;

        }

        [HttpPost]
        [Route("/update-notification-seen")]
        public async Task Handle(NotificationSeenUpdateData data)
        {
      

            await _notificationRepository.UpdateNotificationSeenAsync(data);

       
        }


    }
}
