using N76_C.Api.Models.Common.Interfaces;

namespace N76_C.Api.Models.Common.Entities;

public class SoftDeletedEntity : Entity, ISoftDeletedEntity
{
    public bool IsDeleted { get; set; }
    public DateTime? DeletedTime { get; set; }
}
