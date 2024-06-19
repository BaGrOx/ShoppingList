using Microsoft.EntityFrameworkCore;
using ShopList.Data.DbModels;
using ShopList.Data.Repositories.Interfaces;

namespace ShopList.Data.Repositories
{
    public class ShoppingListRepository : IShoppingListRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ShoppingListRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddShoppingListAsync(ShoppingList shoppingList)
        {
            await _dbContext.ShoppingLists.AddAsync(shoppingList);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteShoppingListAsync(Guid shoppingListId)
        {
            var shoppingList = await _dbContext.ShoppingLists
                .Include(x => x.Products)
                .FirstOrDefaultAsync(x => x.Id == shoppingListId);
            if (shoppingList is null)
                return;

            _dbContext.ShoppingLists.Remove(shoppingList);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateShoppingListAsync(ShoppingList shoppingList)
        {
            _dbContext.Update(shoppingList);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<ShoppingList>> GetAllShoppingListAsync() => await 
            _dbContext.ShoppingLists
            .Include(x => x.Products)
            .ToListAsync();

        public Task<ShoppingList?> GetShoppingListAsync(Guid shoppingListId) => _dbContext.ShoppingLists.FirstOrDefaultAsync(x => x.Id == shoppingListId);
    }
}
