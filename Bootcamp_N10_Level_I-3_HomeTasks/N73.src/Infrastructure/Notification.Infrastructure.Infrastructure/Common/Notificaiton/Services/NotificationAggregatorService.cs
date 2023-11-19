using AutoMapper;
using Microsoft.Extensions.Options;
using Notification.Infrastructure.Application.Common.Identity.Services;
using Notification.Infrastructure.Application.Common.Models.Querying;
using Notification.Infrastructure.Application.Common.Notifications.Models;
using Notification.Infrastructure.Application.Common.Notifications.Services;
using Notification.Infrastructure.Domain.Common.Exceptions;
using Notification.Infrastructure.Domain.Entities;
using Notification.Infrastructure.Domain.Enums;
using Notification.Infrastructure.Domain.Extensions;
using Notification.Infrastructure.Infrastructure.Common.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notification.Infrastructure.Infrastructure.Common.Notificaiton.Services
{
    public class NotificationAggregatorService : INotificationAggregatorService
    {
        private readonly IMapper _mapper;
        private readonly IOptions<NotificationSettings> _notificationSettings;
        private readonly ISmsTemplateService _smsTemplateService;
        private readonly IEmailTemplateService _emailTemplateService;
        private readonly ISmsOrchestrationService _smsOrchestrationService;
        private readonly IEmailOrchestrationService _emailOrchestrationService;
        private readonly IUserSettingsService _userSettingsService;
        private readonly IUserService _userService;

        public NotificationAggregatorService(
            IMapper mapper,
            IOptions<NotificationSettings> notificationSettings,
            ISmsTemplateService smsTemplateService,
            IEmailTemplateService emailTemplateService,
            ISmsOrchestrationService smsOrchestrationService,
            IEmailOrchestrationService emailOrchestrationService,
            IUserSettingsService userSettingsService,
            IUserService userService)
        {
            _mapper = mapper;
            _notificationSettings = notificationSettings;
            _smsTemplateService = smsTemplateService;
            _emailTemplateService = emailTemplateService;
            _smsOrchestrationService = smsOrchestrationService;
            _emailOrchestrationService = emailOrchestrationService;
            _userSettingsService = userSettingsService;
            _userService = userService;
        }
        public async ValueTask<IList<NotificationTemplate>> GetTemplatesByFilterAsync(
            NotificationTemplateFilter filter, 
            CancellationToken cancellationToken = default
            )
        {
            var templates = new List<NotificationTemplate>();

            if (filter.TemplateType.Contains(Domain.Enums.NotificationType.Sms))
                templates.AddRange(
                    await _smsTemplateService.GetByFilterAsync(filter, cancellationToken: cancellationToken));

            if (filter.TemplateType.Contains(NotificationType.Email))
                templates.AddRange(
                    await _emailTemplateService.GetByFilterAsync(filter, cancellationToken: cancellationToken));

            return templates;
        }

        public async ValueTask<FuncResult<bool>> SendAsync(
            NotificationRequest notificationRequest, 
            CancellationToken cancellationToken = default
            )
        {
            var sendNotificationTask = async () =>
            {
                var senderUser = notificationRequest.SenderUserId.HasValue
                ? await _userService.GetByIdAsync(notificationRequest.SenderUserId.Value,
                cancellationToken: cancellationToken)
                : await _userService.GetSystemUserAsync(true, cancellationToken: cancellationToken);


                notificationRequest.SenderUserId = senderUser!.Id;

                var receiverUser = await _userService.GetByIdAsync(notificationRequest.ReceiverUserId,
                    cancellationToken: cancellationToken);

                //if notification provider type is not specified, get from receiver user settings
                if (!notificationRequest.Type.HasValue && receiverUser!.UserSettings.PreferredNotificationType.HasValue)
                    notificationRequest.Type = receiverUser!.UserSettings.PreferredNotificationType!.Value;

                //If user not specified preferred notification type get from settings
                if (!notificationRequest.Type.HasValue)
                    notificationRequest.Type = _notificationSettings.Value.DefaultNotificationType;

                var sendNotificationTask = notificationRequest.Type switch
                {
                    NotificationType.Sms => _smsOrchestrationService.SendAsync(
                        _mapper.Map<SmsNotificationRequest>(notificationRequest),
                        cancellationToken),
                    NotificationType.Email => _emailOrchestrationService.SendAsync(
                        _mapper.Map<EmailNotificationRequest>(notificationRequest),
                        cancellationToken),
                    _ => throw new NotImplementedException("This type of notification is not supported yet.")
                };

                var sendNotificationResult = await sendNotificationTask;
                return sendNotificationResult.Data;
            };

            return await sendNotificationTask.GetValueAsync();
        }
    }
}
