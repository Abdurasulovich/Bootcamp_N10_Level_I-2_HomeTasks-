namespace N76_C.Api.Models.Common.Interfaces;

public interface ISoftDeletedEntity
{
    bool IsDeleted { get;set; }

    DateTime? DeletedTime { get; set; }
}
