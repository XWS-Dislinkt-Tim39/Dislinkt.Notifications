using Dislinkt.Notifications.Data;
using Dislinkt.Notifications.Interfaces.Repositories;
using Grpc.Core;
using GrpcAddNotificationService;
using Type = Dislinkt.Notifications.Domain.Type;
using GrpcNotificationService;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Linq;
using Dislinkt.Notifications.Domain;

namespace GrpcService.Services
{
    public class AddNotificationService : addNotificationGreeter.addNotificationGreeterBase
    {
        private INotificationRepository _notificationRepository;

        public AddNotificationService(INotificationRepository notificationRepository)
        {

            _notificationRepository = notificationRepository;
        }

        public override async Task<NotificationReply> addNotification(NotificationRequest request, ServerCallContext context)
        {
            bool result;
            try
            {
                Dislinkt.Notifications.Domain.Type t = Dislinkt.Notifications.Domain.Type.Message;
                if (request.Type == "Message")
                {
                    t = Dislinkt.Notifications.Domain.Type.Message;
                }
                else if (request.Type == "Post")
                {
                    t = Dislinkt.Notifications.Domain.Type.Post;
                }
                else if (request.Type == "Job")
                {
                    t = Dislinkt.Notifications.Domain.Type.Job;
                }
                else if (request.Type == "FriendRequest")
                {
                    t = Dislinkt.Notifications.Domain.Type.FriendRequest;
                }

               
                    var existingSettings = await _notificationRepository.GetAllByUserId(Guid.Parse(request.UserId));


                    var updatedNotifications = existingSettings.Notifications.Append(new Dislinkt.Notifications.Domain.Notification(Guid.NewGuid(), Guid.Parse(request.From),t, request.Seen)).ToArray();

                    await _notificationRepository.AddNotification(new NotificationSettings(existingSettings.Id, existingSettings.UserId, existingSettings.MessageOn, existingSettings.PostOn, existingSettings.JobOn, existingSettings.FriendRequestOn, updatedNotifications));

               



            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                return await Task.FromResult(new NotificationReply
                {
                    Successful = false,
                    Message = $"{ex}"
                });
            }
            return await Task.FromResult(new NotificationReply
            {
                Successful = true,
                Message = $"Notifikacija: {request.UserId} | {request.From} | {request.Type} | {request.Seen}"
            });
        }



    }
}
