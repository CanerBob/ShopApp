using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using ShopAPP.Dependesies;
using ShopAPP.Models.Layer.Database;
using ShopAPP.Repository.Layer.ProductRepository;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDependencyServices(builder.Configuration);
var app = builder.Build();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseAuthentication();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapControllerRoute
    (
    name: "adminusers",
    pattern: "/admin/user/list",
    defaults: new { controller = "Admin", action = "UserList" }
    );
app.MapControllerRoute(
    name: "adminuseredit",
    pattern: "admin/user/{id?}",
    defaults: new { controller = "Admin", action = "UserEdit" }
    );

app.MapControllerRoute(
    name: "adminroleedit",
    pattern: "admin/role/{id?}",
    defaults: new { controller = "AdminRole", action = "RoleEdit" }
);
app.MapControllerRoute(
    name: "adminrolelist",
    pattern: "adminro/role/list",
    defaults: new { controller = "AdminRole", action = "RoleList" }
);

app.MapControllerRoute(
    name: "adminproductcreate",
    pattern: "admin/products/create",
    defaults: new { controller = "Admin", action = "ProductCreate" }
);
app.MapControllerRoute(
    name: "adminproductedit",
    pattern: "admin/products/{id?}",
    defaults: new { controller = "Admin", action = "ProductEdit" }
);
app.MapControllerRoute(
   name: "admincategories",
   pattern: "admin/categories",
   defaults: new { controller = "Admin", action = "CategoryList" }
);
app.MapControllerRoute(
    name: "admincategorycreate",
    pattern: "admin/categories/create",
    defaults: new { controller = "Admin", action = "CategoryCreate" }
);
app.MapControllerRoute(
    name: "admincategoryedit",
    pattern: "admin/categories/{id?}",
    defaults: new { controller = "Admin", action = "CategoryEdit" }
);
// localhost/search    
app.MapControllerRoute(
    name: "search",
    pattern: "search",
    defaults: new { controller = "Shop", action = "search" }
);

app.MapControllerRoute(
    name: "productdetails",
    pattern: "{url}",
    defaults: new { controller = "Shop", action = "details" }
);
//app.MapControllerRoute(
//    name: "products",
//    pattern: "products/{category?}",
//    defaults: new { controller = "Shop", action = "list" }
//);
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);
app.Run();