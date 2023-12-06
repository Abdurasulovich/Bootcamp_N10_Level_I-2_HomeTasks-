namespace N76.Api.Models.Common;

public interface ICreationAuditableEntity
{
    Guid CreatedByUserid { get; set; }
}
