using System.Linq.Expressions;
using Notification.Infrastructure.Domain.Entities;
using Npgsql.Replication.PgOutput.Messages;

namespace Notification.Infrastructure.Persistance.Repositories.Interfaces;

public interface IEmailHistoryRepository
{
    IQueryable<EmailHistory> Get(Expression<Func<EmailHistory, bool>>? predicate = default,
        bool asNoTracking = false
        );

    ValueTask<EmailHistory> CreateAsync(
        EmailHistory emailHistory,
        bool saveChanges = true,
        CancellationToken cancellationToken = default
    );
}