using N70.Domain.Common;
using N70.Domain.Enums;

namespace N70.Domain.Entities;

public class Role : IEntity
{
    public Guid Id { get; set; }
    public RoleType Type { get; set; }
    public bool IsDisabled { get; set; }
    public DateTime CreatedTime { get; set; }
    public DateTime ModifiedTime { get; set; }
}