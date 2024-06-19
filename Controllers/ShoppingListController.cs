using Microsoft.AspNetCore.Mvc;
using ShopList.Data.DbModels;
using ShopList.Data.Repositories.Interfaces;
using ShopList.Models;

namespace ShopList.Controllers
{
    public class ShoppingListController : Controller
    {
        private readonly IShoppingListRepository _shoppingListRepository;

        public ShoppingListController(IShoppingListRepository shoppingListRepository)
        {
            _shoppingListRepository = shoppingListRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var shoppingLists = await _shoppingListRepository.GetAllShoppingListAsync();

            var shoppingListsVM = shoppingLists.Select(x => new ShoppingListViewModel
            {
                Id = x.Id,
                Name = x.Name,
                PlannedPurchaseDate = x.PlannedPurchaseDate,
                Products = x.Products.Select(p => new ProductViewModel
                {
                    Id = p.Id,
                    Description = p.Description,
                    IsDeleted = p.IsDeleted,
                }).ToList()
            }).ToList();

            return View(shoppingListsVM);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var newShoppingList = new ShoppingListViewModel()
            {
                PlannedPurchaseDate = DateTime.Today,
            };

            return View(newShoppingList);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ShoppingListViewModel shoppingList)
        {
            if (!ModelState.IsValid)
                return View(shoppingList);

            if (ValidatePlannedPurchaseDate(shoppingList.PlannedPurchaseDate))
            {
                TempData["Message"] = "Data zakupu nie może być starsza, od dnia utworzenia";
                return View(shoppingList);
            }

            var shoppingListDB = new ShoppingList
            {
                Id = Guid.NewGuid(),
                Name = shoppingList.Name,
                PlannedPurchaseDate = shoppingList.PlannedPurchaseDate,
            };

            await _shoppingListRepository.AddShoppingListAsync(shoppingListDB);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == Guid.Empty)
                return RedirectToAction("Index");

            await _shoppingListRepository.DeleteShoppingListAsync(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            if (id == Guid.Empty)
                return RedirectToAction("Index");

            var shoppingListDb = await _shoppingListRepository.GetShoppingListAsync(id);
            if (shoppingListDb == null)
                return RedirectToAction("Index");

            var shoppingListVM = new ShoppingListViewModel
            {
                Id = id,
                Name = shoppingListDb.Name,
                PlannedPurchaseDate = shoppingListDb.PlannedPurchaseDate
            };

            return View(shoppingListVM);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ShoppingListViewModel vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            var dbModel = new ShoppingList
            {
                Id = vm.Id,
                Name = vm.Name,
                PlannedPurchaseDate = vm.PlannedPurchaseDate
            };

            await _shoppingListRepository.UpdateShoppingListAsync(dbModel);
            return RedirectToAction("Index");
        }

        private bool ValidatePlannedPurchaseDate(DateTime plannedPurchaseDate)
        {
            return plannedPurchaseDate < DateTime.Today;
        }
    }
}
