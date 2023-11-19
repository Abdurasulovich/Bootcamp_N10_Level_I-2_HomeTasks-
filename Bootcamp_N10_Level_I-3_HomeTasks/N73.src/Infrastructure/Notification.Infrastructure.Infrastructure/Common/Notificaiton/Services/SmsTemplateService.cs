using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Notification.Infrastructure.Application.Common.Models.Querying;
using Notification.Infrastructure.Application.Common.Notifications.Services;
using Notification.Infrastructure.Application.Common.Querying.Extensions;
using Notification.Infrastructure.Domain.Entities;
using Notification.Infrastructure.Domain.Enums;
using Notification.Infrastructure.Persistance.Repositories.Interfaces;
using System.Linq.Expressions;

namespace Notification.Infrastructure.Infrastructure.Common.Notificaiton.Services;

public class SmsTemplateService : ISmsTemplateService
{
    private readonly ISmsTemplateRepository _smsTemplateRepository;
    private readonly IValidator<SmsTemplate> _smsTemplateValidator;

    public SmsTemplateService(
        ISmsTemplateRepository smsTemplateRepository,
        IValidator<SmsTemplate> smsTemplateValidator)
    {
        _smsTemplateRepository = smsTemplateRepository;
        _smsTemplateValidator = smsTemplateValidator;
    }

    public IQueryable<SmsTemplate> Get(
        Expression<Func<SmsTemplate, bool>>? predicate = default, 
        bool asNoTracking =false)
        =>_smsTemplateRepository.Get(predicate, asNoTracking);
    public ValueTask<SmsTemplate> CreateAsync(
        SmsTemplate smsTemplate, 
        bool saveChanges = true, 
        CancellationToken cancellationToken = default)
    {
        var validationResult = _smsTemplateValidator.Validate(smsTemplate);
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        return _smsTemplateRepository.CreateAsync(smsTemplate, saveChanges, cancellationToken);
    }

    public async ValueTask<IList<SmsTemplate>> GetByFilterAsync(
        FilterPagination paginationOptions, 
        bool asNoTracking = false, 
        CancellationToken cancellationToken = default
        )=>
        await Get(asNoTracking: asNoTracking)
            .ApplyPagination(paginationOptions)
            .ToListAsync(cancellationToken: cancellationToken);

    public async ValueTask<SmsTemplate?> GetByTypeAsync(
        NotificationTemplateType templateType,
        bool asNoTracking = false,
        CancellationToken cancellationToken = default)
    => await _smsTemplateRepository.Get(template => template.TemplateType == templateType, asNoTracking)
        .SingleOrDefaultAsync(cancellationToken: cancellationToken);
}
