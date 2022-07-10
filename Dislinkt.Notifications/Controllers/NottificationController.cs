
using Dislinkt.Notifications.Data;
using Dislinkt.Notifications.Domain;
using Dislinkt.Notifications.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using OpenTracing;

namespace Dislinkt.Notifications.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NottificationController : Controller
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly ITracer _tracer;
        public NottificationController(INotificationRepository notificationRepository, ITracer tracer)
        {

            _notificationRepository = notificationRepository;
            _tracer = tracer;
        }

        [HttpPost]
        [Route("/create-notification-settings")]
        public async Task<bool> CreateNotificationSettings([FromBody] NewNotificationSettingsData settings)
        {
            return await _notificationRepository.CreateNotificationSettingsAsync(settings);

        }


        [HttpPost]
        [Route("/update-notification-settings")]
        public async Task UpdateNotificationSettings([FromBody] NewNotificationSettingsData settings)
        {
            var actionName = ControllerContext.ActionDescriptor.DisplayName;
            using var scope = _tracer.BuildSpan(actionName).StartActive(true);
            await _notificationRepository.UpdateNotificationSettings(settings);

        }

        [HttpGet]
        [Route("/get-all-by-userId")]
        public async Task<NotificationSettings> GetAll(Guid userId)
        {
            var actionName = ControllerContext.ActionDescriptor.DisplayName;
            using var scope = _tracer.BuildSpan(actionName).StartActive(true);
            var result = await _notificationRepository.GetAllByUserId(userId);

            return result;
        }
        [HttpGet]
        [Route("/get-without-messages-by-userId")]
        public async Task<NotificationSettings> GetWithoutMessages(Guid userId)
        {
            var actionName = ControllerContext.ActionDescriptor.DisplayName;
            using var scope = _tracer.BuildSpan(actionName).StartActive(true);
            var result = await _notificationRepository.GetWithoutMessagesByUserId(userId);

            return result;
        }


        [HttpPost]
        [Route("/update-notification-seen")]
        public async Task Handle(NotificationSeenUpdateData data)
        {
            var actionName = ControllerContext.ActionDescriptor.DisplayName;
            using var scope = _tracer.BuildSpan(actionName).StartActive(true);
            await _notificationRepository.UpdateNotificationSeenAsync(data);

       
        }

        [HttpDelete]
        [Route("/delete-by-userId/{id}")]
        public async Task DeleteByUserId(Guid id)
        {

            var actionName = ControllerContext.ActionDescriptor.DisplayName;
            using var scope = _tracer.BuildSpan(actionName).StartActive(true);
            await _notificationRepository.DeleteByUserId(id);



        }


    }
}
