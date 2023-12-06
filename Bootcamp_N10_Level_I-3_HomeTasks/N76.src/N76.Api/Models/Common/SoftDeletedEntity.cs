
namespace N76.Api.Models.Common;

public class SoftDeletedEntity : Entity, ISoftDeletedEntity
{
    public bool IsDeleted { get; set; }

    public DateTime? DeletedTime { get; set; }
}
