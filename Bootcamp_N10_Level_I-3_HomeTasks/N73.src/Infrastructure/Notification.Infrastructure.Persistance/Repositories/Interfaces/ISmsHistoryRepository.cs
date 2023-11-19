using System.Linq.Expressions;
using Notification.Infrastructure.Domain.Entities;

namespace Notification.Infrastructure.Persistance.Repositories.Interfaces;

public interface ISmsHistoryRepository
{
    IQueryable<SmsHistory> Get(
        Expression<Func<SmsHistory, bool>>? predicate = default, bool asNoTracking = false);

    ValueTask<SmsHistory> CreateAsync(
        SmsHistory smsHistory,
        bool saveChanges = true,
        CancellationToken cancellationToken = default
    );
}