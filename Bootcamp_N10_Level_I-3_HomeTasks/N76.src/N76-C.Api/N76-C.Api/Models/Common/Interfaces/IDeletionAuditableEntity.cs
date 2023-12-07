namespace N76_C.Api.Models.Common.Interfaces;

public interface IDeletionAuditableEntity
{
    Guid? DeletedByUserId { get; set; }
}
