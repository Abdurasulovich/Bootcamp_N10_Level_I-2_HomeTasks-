using N76_C.Api.Models.Common.Interfaces;

namespace N76_C.Api.Models.Common.Entities;

public class AuditableEntity : SoftDeletedEntity, IAuditableEntity
{
    public DateTime CreatedTime { get; set; }
    public DateTime? ModifiedTime { get; set; }
}
