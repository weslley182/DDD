using System.ComponentModel.DataAnnotations;

namespace DDD.Application.Dtos;

public class ProductRequest
{
    [Required]
    public string? Name { get; set; }
}
