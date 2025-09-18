using System.ComponentModel.DataAnnotations;

namespace CRUD.Model;

public record AddProductModel
{
    [Required]
    [MaxLength(50)]
    public string? Name { get; set; }
    public Decimal UnitPrice { get; set; }
}
