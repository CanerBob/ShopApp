using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ShopAPP.Identities;
using ShopAPP.Models.Layer.Database;
using ShopAPP.Repository.Layer.AllRepositories;
using ShopAPP.Repository.Layer.CategoryRepository;
using ShopAPP.Repository.Layer.GenericRepositories;
using ShopAPP.Repository.Layer.ProductRepository;
using ShopAPP.Service.Layer.Services;
using ShopAPP.Service.Layer.Services.CategoryService;
using ShopAPP.Service.Layer.Services.EmailServices;
using ShopAPP.Service.Layer.Services.ProductManager;

namespace ShopAPP.Dependesies
{
    public static class MyDependensies
    {
        public static IServiceCollection AddDependencyServices(this IServiceCollection services,ConfigurationManager configuration)
        {
            services.ServicesConfigure();
            //services.AddDbContext<APPDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("SqlConnection")));
            services.AddScoped<IEmailSender, SmtpEmailSender>(i=> new SmtpEmailSender
                (
                configuration["EmailSender:Host"],
                configuration.GetValue<int>("EmailSender:Port"),
                configuration.GetValue<bool>("EmailSender:EnableSSl"),
                configuration["EmailSender:UserName"],
                configuration["EmailSender:Password"]
                ));
            services.AddDbContext<ContextApp>(opt => opt.UseSqlServer(configuration.GetConnectionString("SqlConnection")));
            services.AddIdentity<Person,IdentityRole>().AddEntityFrameworkStores<ContextApp>().AddDefaultTokenProviders();
            services.Configure<IdentityOptions>(opt =>
            {
                //parola içerisinde sayısal bi değer olmalıdır.
                opt.Password.RequireDigit = true;
                //parola içerisinde küçük harf olmak zorunda
                opt.Password.RequireLowercase = true;
                //parola içerisinde büyük harf olmak zorunda
                opt.Password.RequireUppercase = true;
                //parola minimum 6 karakter olmak zorunda
                opt.Password.RequiredLength = 6;
                //parolada alfanumrik karakter olmak zorunda
                opt.Password.RequireNonAlphanumeric = true;

                //Lockout
                //kullanıcı parolasını maks 3 kere yanlış girebilir
                opt.Lockout.MaxFailedAccessAttempts = 3;
                //kullanıcı kilitlendikten sonra 5 dakika sonra giriş yapabilir
                opt.Lockout.DefaultLockoutTimeSpan=TimeSpan.FromMinutes(5);
                //yeniden giriş yapabilmesi için
                opt.Lockout.AllowedForNewUsers = true;


                //User
                //Her kullanıcının birbirinden farklı mail adresi olması gerekir
                opt.User.RequireUniqueEmail = true;
                //Kullanıcı üye olduktan sonra emailnin onaylamak zorunda
                opt.SignIn.RequireConfirmedEmail = true;
                //Verdiği telefon numarası için bir onay olması gerekiyor
                opt.SignIn.RequireConfirmedPhoneNumber = false;
            });
            services.ConfigureApplicationCookie(opt =>
            {
                opt.LoginPath = "/account/login";
                opt.LogoutPath = "/account/logout";
                opt.AccessDeniedPath = "/account/accesdenied";
                opt.SlidingExpiration = true;
                opt.ExpireTimeSpan = TimeSpan.FromMinutes(60);
                opt.Cookie = new CookieBuilder 
                {
                HttpOnly = true,
                Name=".ShopAPP.Security.Cookie",
                SameSite=SameSiteMode.Strict 
                };
            });
            return services;
        }
        public static IServiceCollection ServicesConfigure(this IServiceCollection services) 
        {
            services.AddScoped<IProductRepository,ProductRepository>();
            services.AddScoped<ICateogoryRepository,EfCoreCategoryRepository>();
            services.AddScoped<IProductManager,ProductManager>();
            services.AddScoped<ICategoryService,CategoryService>();
            return services;
        }
        public static WebApplication WebRoute(WebApplication app) 
        {
           
            return app;
        }
    }
}