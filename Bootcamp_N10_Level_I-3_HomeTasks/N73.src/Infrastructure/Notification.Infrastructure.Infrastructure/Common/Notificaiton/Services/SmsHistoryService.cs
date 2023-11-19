using FluentValidation;
using Notification.Infrastructure.Application.Common.Models.Querying;
using Notification.Infrastructure.Application.Common.Notifications.Services;
using Notification.Infrastructure.Domain.Entities;
using Notification.Infrastructure.Domain.Enums;
using Notification.Infrastructure.Persistance.Repositories.Interfaces;

namespace Notification.Infrastructure.Infrastructure.Common.Notificaiton.Services;

public class SmsHistoryService : ISmsHistoryService
{
    private readonly ISmsHistoryRepository _smsHistoryRepository;
    private readonly IValidator<SmsHistory> _smsHistoryValidator;

    public SmsHistoryService(
        ISmsHistoryRepository smsHistoryRepository,
        IValidator<SmsHistory> smsHistoryValidator
        )
    {
        _smsHistoryRepository = smsHistoryRepository;
        _smsHistoryValidator = smsHistoryValidator;
    }
    public async ValueTask<SmsHistory> CreateAsync(
        SmsHistory smsHistory, 
        bool saveChanges = true, 
        CancellationToken cancellationToken = default)
    {
        var validationResult = await _smsHistoryValidator.ValidateAsync(smsHistory,
            options => options.IncludeRuleSets(EntityEvent.OnCreate.ToString()),
            cancellationToken);

        if (!validationResult.IsValid) throw new ValidationException(validationResult.Errors);

        return await _smsHistoryRepository.CreateAsync(smsHistory, saveChanges, cancellationToken);
    }

    public ValueTask<IList<SmsHistory>> GetByFilterAsync(FilterPagination paginationOptions, bool asNoTracking = false, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
