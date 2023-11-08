using System.ComponentModel.DataAnnotations;

namespace ShopAPP.Models.Layer.Models;
public class CategoryModel
{
    public int Id { get; set; }
    [Required(ErrorMessage ="Name Alanı Gereklidir")]
    [StringLength(100,MinimumLength =5,ErrorMessage ="Kategori 5 ile 100 karakter arasında olmalıdır")]
    public string Name { get; set; }
    [Required(ErrorMessage = "Url Alanı Gereklidir")]
    [StringLength(100, MinimumLength = 5, ErrorMessage = "Url 5 ile 100 karakter arasında olmalıdır")]
    public string Url { get; set; }
    public List<Product> Products { get; set; }
}
