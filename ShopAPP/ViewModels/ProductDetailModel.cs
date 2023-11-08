using ShopAPP.Models.Layer.Models;

namespace ShopAPP.ViewModels;
public class ProductDetailModel
{
    public Product Product { get; set; }
    public List<Category> Categories { get; set; }
}
