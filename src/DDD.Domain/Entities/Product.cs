using DDD.Domain.Base;

namespace DDD.Domain.Entities;

public class Product : EntityBase
{
    public string? Name { get; set; }
}
