using N76_C.Api.Models.Common.Interfaces;

namespace N76_C.Api.Models.Common.Entities;

public class Entity : IEntity
{
    public Guid Id { get; set; }
}
