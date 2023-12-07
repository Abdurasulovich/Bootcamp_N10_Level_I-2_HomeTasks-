namespace N76_C.Api.Models.Common.Interfaces;

public interface IModificationAuditableEntity
{
    Guid? ModifiedByUserId { get; set; }
}
