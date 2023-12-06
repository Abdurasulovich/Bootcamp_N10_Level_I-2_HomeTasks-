namespace N76.Api.Models.Common;

public interface IModificationAuditableEntity
{
    Guid? ModifiedByUserId { get; set; }
}
