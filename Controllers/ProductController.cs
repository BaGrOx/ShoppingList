using Microsoft.AspNetCore.Mvc;
using ShopList.Data.DbModels;
using ShopList.Data.Repositories.Interfaces;
using ShopList.Models;

namespace ShopList.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly IShoppingListRepository _shoppingListRepository;

        public ProductController(IProductRepository productRepository, IShoppingListRepository shoppingListRepository)
        {
            _productRepository = productRepository;
            _shoppingListRepository = shoppingListRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var products = await _productRepository.GetAllProductsAsync();
            return View(products);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var shoppingLists = await _shoppingListRepository.GetAllShoppingListAsync();
            var newProduct = new ProductViewModel
            {
                ShoppingLists = await GetShoppingListsVM()
            };

            return View(newProduct);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductViewModel product)
        {
            if (!ModelState.IsValid)
                return View(product);

            if (product.SelectedShoppingListId is null)
            {
                TempData["Message"] = "Należy wybrać listę, do której dodać produkt";
                return View(product);
            }

            var productDB = new Product
            {
                Id = Guid.NewGuid(),
                Description = product.Description,
                IsDeleted = false,
                ShoppingListId = product.SelectedShoppingListId.Value
            };

            await _productRepository.AddProductAsync(productDB);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            var product = await _productRepository.FindByIdAsync(id);
            if (product is null)
                return RedirectToAction(nameof(Index));

            if (product.IsDeleted)
            {
                TempData["Message"] = "Wybrany produkt jest już skreślony";
                return RedirectToAction(nameof(Index));
            }

            product.IsDeleted = true;
            await _productRepository.UpdateProductAsync(product);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            if(id == Guid.Empty)
                return RedirectToAction(nameof(Index));

            var product = await _productRepository.FindByIdAsync(id);
            if(product is null)
                return RedirectToAction(nameof(Index));

            var shoppingLists = await _shoppingListRepository.GetAllShoppingListAsync();

            var productVM = new ProductViewModel
            {
                Id = product.Id,
                Description = product.Description,
                SelectedShoppingListId = product.ShoppingListId,
                ShoppingLists = await GetShoppingListsVM()
            };

            return View(productVM);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProductViewModel vm)
        {
            if (!ModelState.IsValid)
                return RedirectToAction(nameof(Index));

            if(vm.SelectedShoppingListId is null)
                return View(vm);

            var dbModel = new Product
            {
                Id = vm.Id,
                Description = vm.Description,
                ShoppingListId = vm.SelectedShoppingListId.Value
            };

            await _productRepository.UpdateProductAsync(dbModel);
            return RedirectToAction(nameof(Index));
        }

        private async Task<List<ShoppingListViewModel>> GetShoppingListsVM()
        {
            var shoppingLists = await _shoppingListRepository.GetAllShoppingListAsync();
            return shoppingLists.Select(x => new ShoppingListViewModel
            {
                Id = x.Id,
                Name = x.Name,
            }).ToList();
        }
    }
}
