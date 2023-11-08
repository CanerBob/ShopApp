using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ShopAPP.Identities;
using ShopAPP.Models.Layer.Extensionss;
using ShopAPP.Models.Layer.Models;
using ShopAPP.Service.Layer.Services.CategoryService;
using ShopAPP.Service.Layer.Services.ProductManager;
using ShopAPP.ViewModels;

namespace ShopAPP.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IProductManager _productManager;
        private readonly ICategoryService _categoryService;
        private readonly UserManager<Person> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public AdminController(IProductManager productManager, ICategoryService categoryService, UserManager<Person> userManager, RoleManager<IdentityRole> roleManager)
        {
            _productManager = productManager;
            _categoryService = categoryService;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public IActionResult UserList() 
        {
        return View(_userManager.Users);
        }
        [HttpGet]
        public async Task<IActionResult> UserEdit(string id) 
        {
            var user= await _userManager.FindByIdAsync(id);
            if (user != null) 
            {
                var selectedRoles = await _userManager.GetRolesAsync(user);
                var roles = _roleManager.Roles.Select(x => x.Name);
                ViewBag.Roles=roles;
                return View(new UserDetailModel()
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    EmailConfirmed = user.EmailConfirmed,
                    SelectedRoles = selectedRoles
                }) ;
                return Redirect("~/admin/user/list");
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> UserEdit(UserDetailModel model, string[] selectedRoles) 
        {
            if (ModelState.IsValid) 
            {
            var user= await _userManager.FindByIdAsync(model.UserId);
                if (user != null) 
                {
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.Email = model.Email;
                user.EmailConfirmed = model.EmailConfirmed;
                user.UserName = model.UserName;
                    var result= await _userManager.UpdateAsync(user);
                    if (result.Succeeded) 
                    {
                        var userRoles = await _userManager.GetRolesAsync(user);
                        selectedRoles = selectedRoles ?? new string[] { };
                        await _userManager.AddToRolesAsync(user, selectedRoles.Except(userRoles).ToArray<string>());
                        await _userManager.RemoveFromRolesAsync(user, userRoles.Except(selectedRoles).ToArray<string>());
                        return Redirect("/admin/user/list");
                    }
                }
                return Redirect("/admin/user/list");
            }
            return View(model);
        }
        public IActionResult ProductList()
        {
            return View(new ProductListViewModel()
            {
                Products = _productManager.GetAll()
            }) ;
        }
        public IActionResult CategoryList()
        {
            return View(new CategoryListViewModel()
            {
                Categories = _categoryService.GetAll()
            }) ;
        }
        [HttpGet]
        public IActionResult ProductCreate() 
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ProductCreate(ProductModel model)
        {
            if (ModelState.IsValid)
            {
                var entity = new Product()
                {
                    Name = model.Name,
                    Description = model.Description,
                    Price = model.Price,
                    //ImageUrl = model.ImageUrl,
                    Url = model.Url,

                };
                _productManager.Create(entity);
                TempData.Put("message", new AlertMessage
                {
                Title="Ürün Başarılı Bir Şekilde Eklenmiştir",
                Message="Ürün Başarılı Bir Şekilde Eklenmiştir",
                AlertType="success"
                });
                return RedirectToAction(nameof(ProductList));
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult ProductEdit(int? id) 
        {
            if (id == null) 
            {
                return NotFound();
            }
            var entity = _productManager.GetByIdWithCategories((int)id);
            if (entity==null)
            {
                return NotFound();
            }
            var model = new ProductModel()
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                Price = entity.Price,
                ImageUrl = entity.ImageUrl,
                Url = entity.Url,
                SelectedCategories = entity.ProductCategories.Select(x => x.Category).ToList(),
            };
            ViewBag.categories=_categoryService.GetAll();
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> ProductEdit(ProductModel model, int[] categoryIds, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                var entity = _productManager.GetById(model.Id);
                if (entity == null)
                {
                    return NotFound();
                }
                entity.Name = model.Name;
                entity.Price = model.Price;
                entity.Url = model.Url;
                entity.Description = model.Description;
                entity.IsHome = model.IsHome;
                entity.IsApproved = model.IsApproved;

                if (file != null)
                {
                    var extention = Path.GetExtension(file.FileName);
                    var randomName = string.Format($"{Guid.NewGuid()}{extention}");
                    entity.ImageUrl = randomName;
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img", randomName);

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                }

                if (_productManager.Update(entity, categoryIds))
                {
                    TempData.Put("message", new AlertMessage()
                    {
                        Title = "kayıt güncellendi",
                        Message = "kayıt güncellendi",
                        AlertType = "success"
                    });
                    return RedirectToAction("ProductList");
                }
                TempData.Put("message", new AlertMessage()
                {
                    Title = "hata",
                    //Message = _productManager.ErrorMessage,
                    AlertType = "danger"
                });
            }
            ViewBag.Categories = _categoryService.GetAll();
            return View(model);
        }
        [HttpGet]
        public IActionResult CategoryCreate()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CategoryCreate(CategoryModel model)
        {
            if (!ModelState.IsValid) 
            {
                var entity = new Category()
                {
                    Name = model.Name,
                    Url = model.Url
                };
            _categoryService.Create(entity);
                TempData.Put("message", new AlertMessage()
                {
                    Title = "Kategori Başarılı Bir Şekilde Eklenmiştir",
                    Message = "Kategori Başarılı Bir Şekilde Eklenmiştir",
                    AlertType = "success"
                });
                return RedirectToAction(nameof(CategoryList));
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult CategoryEdit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entity = _categoryService.GetByIdWithProducts((int)id);

            if (entity == null)
            {
                return NotFound();
            }

            var model = new CategoryModel()
            {
                Id = entity.Id,
                Name = entity.Name,
                Url = entity.Url,
                Products = entity.ProductCategories.Select(p => p.Product).ToList()
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult CategoryEdit(CategoryModel model)
        {
            if (ModelState.IsValid)
            {
                var entity = _categoryService.GetById(model.Id);
                if (entity == null)
                {
                    return NotFound();
                }
                entity.Name = model.Name;
                entity.Url = model.Url;

                _categoryService.Update(entity);

                var msg = new AlertMessage()
                {
                    Message = $"{entity.Name} isimli category güncellendi.",
                    AlertType = "success"
                };

                TempData["message"] = JsonConvert.SerializeObject(msg);

                return RedirectToAction("CategoryList");
            }
            return View(model);
        }
        public IActionResult DeleteProduct(int productId)
        {
            var entity = _productManager.GetById(productId);

            if (entity != null)
            {
                _productManager.Delete(entity);
            }

            var msg = new AlertMessage()
            {
                Message = $"{entity.Name} isimli ürün silindi.",
                AlertType = "danger"
            };

            TempData["message"] = JsonConvert.SerializeObject(msg);

            return RedirectToAction("ProductList");
        }

        public IActionResult DeleteCategory(int categoryId)
        {
            var entity = _categoryService.GetById(categoryId);

            if (entity != null)
            {
                _categoryService.Delete(entity);
            }

            var msg = new AlertMessage()
            {
                Message = $"{entity.Name} isimli category silindi.",
                AlertType = "danger"
            };

            TempData["message"] = JsonConvert.SerializeObject(msg);

            return RedirectToAction("CategoryList");
        }

        [HttpPost]
        public IActionResult DeleteFromCategory(int productId, int categoryId)
        {
            _categoryService.DeleteFromCategory(productId, categoryId);
            return Redirect("/admin/categories/" + categoryId);
        }
    }
    }