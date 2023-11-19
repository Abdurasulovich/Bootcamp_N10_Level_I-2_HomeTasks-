using System.Linq.Expressions;
using Notification.Infrastructure.Domain.Entities;
using Notification.Infrastructure.Persistance.DataContexts;
using Notification.Infrastructure.Persistance.Repositories.Interfaces;

namespace Notification.Infrastructure.Persistance.Repositories;

public class SmsTemplateRepository : EntityRepositoryBase<SmsTemplate, NotificationDbContext>, ISmsTemplateRepository
{
    public SmsTemplateRepository(NotificationDbContext dbContext) : base(dbContext)
    {
    }

    public IQueryable<SmsTemplate> Get(Expression<Func<SmsTemplate, bool>>? predicate = default,
        bool asNoTracking = false)
        => base.Get(predicate, asNoTracking);

    public ValueTask<SmsTemplate> CreateAsync(SmsTemplate smsTemplate, bool saveChanges = true,
        CancellationToken cancellationToken = default)
        => base.CreateAsync(smsTemplate, saveChanges, cancellationToken);
}