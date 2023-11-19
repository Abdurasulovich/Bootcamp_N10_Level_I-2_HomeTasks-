using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Notification.Infrastructure.Domain.Entities;
using Notification.Infrastructure.Persistance.DataContexts;
using Notification.Infrastructure.Persistance.Repositories.Interfaces;

namespace Notification.Infrastructure.Persistance.Repositories;

public class SmsHistoryRepository : EntityRepositoryBase<SmsHistory, NotificationDbContext>, ISmsHistoryRepository
{
    public SmsHistoryRepository(NotificationDbContext dbContext) : base(dbContext)
    {
    }

    public IQueryable<SmsHistory> Get(Expression<Func<SmsHistory, bool>>? predicate = default,
        bool asNoTracking = false)
        => base.Get(predicate, asNoTracking);

    public async ValueTask<SmsHistory> CreateAsync(SmsHistory smsHistory, bool saveChanges = true,
        CancellationToken cancellationToken = default)
    {
        if (smsHistory.SmsTemplate is not null)
            DbContext.Entry(smsHistory.SmsTemplate).State = Microsoft.EntityFrameworkCore.EntityState.Unchanged;

        var createHistory = await base.CreateAsync(smsHistory, saveChanges, cancellationToken);

        if (smsHistory.SmsTemplate is not null)
            DbContext.Entry(smsHistory.SmsTemplate).State = EntityState.Detached;

        return createHistory;
    }
}