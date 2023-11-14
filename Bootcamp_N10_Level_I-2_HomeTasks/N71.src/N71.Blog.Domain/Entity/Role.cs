using System.Runtime.InteropServices.JavaScript;
using N71.Blog.Domain.Common;
using N71.Blog.Domain.Enums;

namespace N71.Blog.Domain.Entity;

public class Role : IEntity
{
    public Guid Id { get; set; }
    
    public RoleType Type { get; set; }
    
    public bool IsDisable { get; set; }
    
    public DateTime CratedTime { get; set; }
    
    public DateTime UpdatedTime { get; set; }
    
}