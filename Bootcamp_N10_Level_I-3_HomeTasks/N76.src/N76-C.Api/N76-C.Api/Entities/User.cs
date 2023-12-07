using N76_C.Api.Models.Common.Entities;
using N76_C.Api.Models.Common.Interfaces;

namespace N76_C.Api.Entities;

public class User : AuditableEntity, ICreationAuditableEntity, IModificationAuditableEntity, IDeletionAuditableEntity
{
    public string FirstName { get; set; } = default!;

    public string LastName { get; set; } = default!;

    public Guid? ModifiedByUserId { get; set; }
    
    public Guid? DeletedByUserId { get; set; }
    
    public Guid CreatedByUserId { get; set; }
}
