namespace N76.Api.Models.Common;

public interface ISoftDeletedEntity
{
    bool IsDeleted { get; set; }
    DateTime? DeletedTime { get; set; }
}
