using Microsoft.AspNetCore.Mvc;
using ShopAPP.Models;
using ShopAPP.Models.Layer.Models;
using ShopAPP.Repository.Layer.ProductRepository;
using ShopAPP.Service.Layer.Services;
using ShopAPP.Service.Layer.Services.ProductManager;
using ShopAPP.ViewModels;
using System.Diagnostics;

namespace ShopAPP.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IProductManager _productService;

        public HomeController(IProductManager productService, ILogger<HomeController> logger)
        {
            _productService = productService;
            _logger = logger;
        }
        public IActionResult Index()
        {
            var productViewModel = new ProductListViewModel()
            {
                Products = _productService.GetHomePageProducts()
			};
			return View(productViewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult About()
        {
            return View();
        }
        public IActionResult MyView()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}