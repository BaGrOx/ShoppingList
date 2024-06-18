using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopList.Data.DbModels;
using ShopList.Data.Repositories.Interfaces;

namespace ShopList.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var products = await _productRepository.GetAllProductsAsync();
            return View(products);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product)
        {
            if (!ModelState.IsValid)
                return View(product);

            product.Id = Guid.NewGuid();
            await _productRepository.AddProductAsync(product);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            var product = await _productRepository.FindByIdAsync(id);
            if(product is null)
                return RedirectToAction(nameof(Index));

            if(product.IsDeleted)
            {
                TempData["Message"] = "Wybrany produkt jest już skreślony";
                return RedirectToAction(nameof(Index));
            }    

            product.IsDeleted = true;
            await _productRepository.UpdateProductAsync(product);

            return RedirectToAction(nameof(Index));
        }
    }
}
