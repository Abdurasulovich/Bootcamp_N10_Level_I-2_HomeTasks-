namespace N76_C.Api.Models.Common.Interfaces;

public interface IAuditableEntity
{
    DateTime CreatedTime { get; set; }

    DateTime? ModifiedTime { get; set; }
}
