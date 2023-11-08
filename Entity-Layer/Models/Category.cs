namespace Entity_Layer.Models;
public class Category
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<ProductCategory> ProductCategory { get; set; }
}
