using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopAPP.Identities;

namespace ShopAPP.Controllers
{
    [Authorize(Roles = "Admin")]
    //[Authorize]
    public class AdminRoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<Person> _userManager;

        public AdminRoleController(RoleManager<IdentityRole> roleManager, UserManager<Person> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }
        
        [HttpGet]
        public IActionResult RoleList()
        {
            return View(_roleManager.Roles);
        }
        [HttpPost]
        public async Task<IActionResult> RoleEdit(RoleEditModel model) 
        {
            //Ertunç'a sor
                foreach (var userId in model.IdsToAdd?? new string[] { })
                {
                    var user = await _userManager.FindByIdAsync(userId);
                    if (user != null) 
                    {
                        var result = await _userManager.AddToRoleAsync(user, model.RoleName);
                        if (!result.Succeeded) 
                        {
                            foreach (var error in result.Errors)
                            {
                                ModelState.AddModelError("", error.Description);
                            }
                        }
                    }
                }
                foreach (var userId in model.IdsToDelete ?? new string[] { })
                {
                    var user = await _userManager.FindByIdAsync(userId);
                    if (user != null)
                    {
                        var result = await _userManager.RemoveFromRoleAsync(user, model.RoleName);
                        if (!result.Succeeded)
                        {
                            foreach (var error in result.Errors)
                            {
                                ModelState.AddModelError("", error.Description);
                            }
                        }
                    }
                }
            return Redirect("/admin/role/"+model.RoleId);
        }
        [HttpGet]
        public async Task<IActionResult> RoleEdit(string Id)
        {
            var role= await _roleManager.FindByIdAsync(Id);
            var members= new List<Person>();
            var nonmembers= new List<Person>();
            foreach (var person in _userManager.Users)
            {
                var list = await _userManager.IsInRoleAsync(person, role.Name) ? members : nonmembers;
                list.Add(person);
            }
            var model = new RoleDetails()
            {
                Role=role,
                Members=members,
                NonMembers=nonmembers
            };
            return View(model);
        }
        [HttpGet]
        public IActionResult CreateRole () 
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateRole(RoleModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _roleManager.CreateAsync(new IdentityRole(model.Name));
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(RoleList));
                }
                else 
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View(model);
        }
    }
}
