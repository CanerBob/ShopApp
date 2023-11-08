using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ShopAPP.Identities;
using ShopAPP.Models.Layer.Extensionss;
using ShopAPP.Models.Layer.Models;
using ShopAPP.Service.Layer.Services.EmailServices;

namespace ShopAPP.Controllers
{
    [AutoValidateAntiforgeryToken]
    //[Authorize]
    
    public class AccountController : Controller
    {
        private readonly UserManager<Person> _userManager;
        //Cookie işlemleri için
        private readonly SignInManager<Person> _signInManager;
        //email işlemleri için
        private readonly IEmailSender _emailSender;

        public AccountController(UserManager<Person> userManager, SignInManager<Person> signInManager, IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
        }
        [HttpGet]
        public IActionResult accesdenied()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (!ModelState.IsValid) 
            {
            return View(model);
            }
            var user= await _userManager.FindByEmailAsync(model.Email);
            if (user == null) 
            {
                ModelState.AddModelError("","Kullanıcı Bulunamadı");
                return View(model);
            }
            if (!await _userManager.IsEmailConfirmedAsync(user)) 
            {
                ModelState.AddModelError("", "Mail hesabınıza gelen link ile hesabını onaylayın");
                return View(model);
            }
            var result = await _signInManager.PasswordSignInAsync(user, model.Password, true, false);
            if (result.Succeeded) 
            {
                return RedirectToAction("Index", "Home");
            }
            ModelState.AddModelError("", "Girilen Kullanıcı Adı veya Parola Yanlış");
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> Logout() 
        {
            await _signInManager.SignOutAsync();
            TempData.Put("message", new AlertMessage
            {
                Title = "Oturum Kapatıldı",
                Message = "Güvenli Bir Şekilde Çıkış Yaptınız",
                AlertType = "success"
            });
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public IActionResult Register() 
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel model) 
        {
            if (!ModelState.IsValid) 
            {
            return View(model);
            }
            var user = new Person()
            {
            FirstName = model.FirstName,
            LastName = model.LastName,
            Email = model.Email,
            UserName = model.UserName,
            };
            var result= await _userManager.CreateAsync(user,model.Password);
            if (result.Succeeded) 
            {
                //token oluşacak
                var code= await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var url = Url.Action("ConfirmEmail", "Account", new
                {
                    userId= user.Id,
                    token=code
                });
                Console.WriteLine(url);
            
                //email
                await _emailSender.SendEmailAsync(model.Email, "Hesabınızı onaylayınız.", $"Lütfen email hesabınızı onaylamak için linke <a href='https://localhost:7171{url}'>tıklayınız.</a>");
                return RedirectToAction("Login", "Account");
            }
            return View(model);
        }
        public async Task<IActionResult> ConfirmEmail(string userId, string token) 
        {
            if (userId == null || token == null) 
            {
                TempData.Put("message", new AlertMessage
                {
                    Title="Geçersiz Token",
                    Message="Geçersiz Token",
                    AlertType="danger"
                });
                return View();
            }
            var user= await _userManager.FindByIdAsync(userId);
            if (user != null) 
            {
                var result = await _userManager.ConfirmEmailAsync(user, token);
                if (result.Succeeded)
                {
                    TempData.Put("message", new AlertMessage
                    {
                        Title = "Hesabınız Onaylandı",
                        Message = "Hesabınız Onaylandı",
                        AlertType = "success"
                    });
                    return View();
                }
                
            }
            TempData.Put("message", new AlertMessage
            {
                Title = "Hesabınız Onaylanmadı",
                Message = "Hesabınız Onaylanmadı",
                AlertType = "warning"
            });
            return View();         
        }
        public IActionResult ForgotPassword() 
        {
        return View();
        }
        [HttpPost]
        public async Task<IActionResult> ForgotPassword(string Email)
        {
            if (string.IsNullOrEmpty(Email)) 
            {
                return View();
            }
            var user = await _userManager.FindByEmailAsync(Email);
            if ((user==null))
            {
                TempData.Put("message", new AlertMessage
                {
                    Title = "Böyle bir kullanıcı yok",
                    Message = "Böyle bir kullanıcı yok",
                    AlertType = "warning"
                });
            }
            var code=await _userManager.GeneratePasswordResetTokenAsync(user);
            var url = Url.Action("ResetPassword", "Account", new
            {
                userId=user.Id,
                token=code
            });
            //email
            await _emailSender.SendEmailAsync(Email, "Reset Password.", $"Lütfen parolanızı yenilemek için linke <a href='https://localhost:7171{url}'>tıklayınız.</a>");
            return View();
        }
        public IActionResult ResetPassword(string userId,string token) 
        {
            if(userId == null|| token==null) 
            {
                return RedirectToAction("Home", "Index");
            };
            var model = new ResetPasswordModel { Token = token };
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordModel model)
        {
            if (!ModelState.IsValid) 
            {
            return View(model);
            }
            var user= await _userManager.FindByEmailAsync(model.Email);
            if (user == null) 
            {
                return RedirectToAction("Home", "Index");
            }
            var result= await _userManager.ResetPasswordAsync(user,model.Token,model.Password);
            if (result.Succeeded) 
            {
                return RedirectToAction("Login", "Account");
            }
            return View(model);
        }
    }
}