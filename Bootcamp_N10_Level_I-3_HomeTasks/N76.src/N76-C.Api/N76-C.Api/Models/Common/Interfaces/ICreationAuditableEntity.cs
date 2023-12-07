namespace N76_C.Api.Models.Common.Interfaces;

public interface ICreationAuditableEntity
{
    Guid CreatedByUserId { get; set; }
}
