using System.ComponentModel.DataAnnotations;

namespace ShopAPP.Models.Layer.Models;
public class LoginModel
{
    [Required]
    //public string UserName { get; set; }
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}