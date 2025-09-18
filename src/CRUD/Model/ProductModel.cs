namespace CRUD.Model;

public class ProductModel
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public decimal UnitPrice { get; set; }
    public DateTime CreateDate { get; set; }
}
