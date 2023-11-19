

using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Notification.Infrastructure.Application.Common.Models.Querying;
using Notification.Infrastructure.Application.Common.Notifications.Services;
using Notification.Infrastructure.Application.Common.Querying.Extensions;
using Notification.Infrastructure.Domain.Entities;
using Notification.Infrastructure.Domain.Enums;
using Notification.Infrastructure.Persistance.Repositories.Interfaces;

namespace Notification.Infrastructure.Infrastructure.Common.Notificaiton.Services
{
    public class EmailHistoryService : IEmailHistoryService
    {
        private IEmailHistoryRepository _emailHistoryRepository;
        private IValidator<EmailHistory> _emailHistoryValidator;

        public EmailHistoryService(
            IEmailHistoryRepository emailHistoryRepository,
            IValidator<EmailHistory> emailHistoryValidator)
        {
            _emailHistoryRepository = emailHistoryRepository;
            _emailHistoryValidator = emailHistoryValidator;
        }
        public async ValueTask<EmailHistory> CreateAsync(
            EmailHistory emailHistory, 
            bool saveChanges = false, 
            CancellationToken cancellationToken = default)
        {
            var validationResult = await _emailHistoryValidator.ValidateAsync(emailHistory,
                options => options.IncludeRuleSets(EntityEvent.OnCreate.ToString()),
                cancellationToken);

            if (!validationResult.IsValid) throw new ValidationException(validationResult.Errors);

            return await _emailHistoryRepository.CreateAsync(emailHistory, saveChanges, cancellationToken);
        }

        public async ValueTask<IList<EmailHistory>> GetByFilterAsync(
            FilterPagination paginationOptions,
            bool asNoTracking = false,
            CancellationToken cancellationToken = default)
        =>
            await _emailHistoryRepository.Get().ApplyPagination(paginationOptions).ToListAsync(cancellationToken);
    }
}
