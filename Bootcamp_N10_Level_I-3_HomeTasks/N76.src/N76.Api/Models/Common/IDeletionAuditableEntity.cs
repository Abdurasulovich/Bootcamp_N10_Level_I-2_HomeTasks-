namespace N76.Api.Models.Common;

public interface IDeletionAuditableEntity
{
    Guid? DeletedByUserId { get; set; }
}
