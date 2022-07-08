using Dislinkt.Notifications.Data;
using Dislinkt.Notifications.Interfaces.Repositories;
using Grpc.Core;
using GrpcNotificationService;
using MediatR;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace GrpcService.Services
{
    public class GreeterService:notificationSettingsGreeter.notificationSettingsGreeterBase
    {
        private INotificationRepository _notificationRepository;
     
        public GreeterService(INotificationRepository notificationRepository)
        {
          
            _notificationRepository = notificationRepository;
        }

        public override async Task<NotificationSettingsReply> CreateSettings(NotificationSettingsRequest request, ServerCallContext context)
        {
            bool result;
            try
            {
                var settings = new NewNotificationSettingsData
                {
                    UserId = Guid.Parse(request.UserId),
                    MessageOn = request.MessageOn,
                    PostOn = request.PostOn,
                    JobOn = request.JobOn,
                    FriendRequestOn = request.FriendRequestOn
                };

                await _notificationRepository.CreateNotificationSettingsAsync(settings);
                result = true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                return await Task.FromResult(new NotificationSettingsReply
                {
                    Successful = false,
                    Message = $"{ex}"
                });
            }
            return await Task.FromResult(new NotificationSettingsReply
            {
                Successful = result,
                Message = $"Notifikacija: {request.UserId} | {request.MessageOn} | {request.PostOn} | {request.JobOn} |{request.FriendRequestOn}"
            });
        }



    }
}
