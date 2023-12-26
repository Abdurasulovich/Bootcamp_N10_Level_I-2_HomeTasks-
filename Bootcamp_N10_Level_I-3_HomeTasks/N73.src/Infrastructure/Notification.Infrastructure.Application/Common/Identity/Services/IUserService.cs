using Notification.Infrastructure.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notification.Infrastructure.Application.Common.Identity.Services;

public interface IUserService
{
    ValueTask<IList<User>> GetByIdsAsync(
       IEnumerable<Guid> usersId,
       bool asNoTracking = false,
       CancellationToken cancellationToken = default
   );

    ValueTask<User?> GetSystemUserAsync(
        bool asNoTracking = false, 
        CancellationToken cancellationToken = default);

    ValueTask<User?> GetByIdAsync(
        Guid userId,
        bool asNoTracking = false,
        CancellationToken cancellationToken = default
    );
}
