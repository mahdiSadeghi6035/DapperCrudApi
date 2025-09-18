using System.ComponentModel.DataAnnotations;

namespace CRUD.Model;

public record UpdateProductModel
{
    public int Id { get; set; }
    [Required]
    [MaxLength(50)]
    public string? Name { get; set; }
    public Decimal UnitPrice { get; set; }
}
