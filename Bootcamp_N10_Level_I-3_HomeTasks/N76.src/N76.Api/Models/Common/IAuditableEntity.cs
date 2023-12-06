namespace N76.Api.Models.Common;

public interface IAuditableEntity : IEntity
{
    DateTime CreatedTime { get; set; }

    DateTime? ModifiedTime { get; set; }
}
