using Sms.Infrastructure.Application.Common.Enums;
using Sms.Infrastructure.Domain.Common.Exceptions;

namespace Sms.Infrastructure.Application.Notifications.Services;

public interface ISmsOrchestrationService
{
    ValueTask<FuncResult<bool>> SendAsync(
        string senderPhoneNumber,
        string receiverPhoneNumber,
        NotificationTemplateType templateType,
        Dictionary<string, string> variables,
        CancellationToken cancellationToken
        );
}