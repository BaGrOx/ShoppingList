using ShopList.Data.DbModels;

namespace ShopList.Data.Repositories.Interfaces
{
    public interface IShoppingListRepository
    {
        Task<IEnumerable<ShoppingList>> GetAllShoppingListAsync();
        Task AddShoppingListAsync(ShoppingList shoppingList);
        Task DeleteShoppingListAsync(Guid shoppingListId);
        Task<ShoppingList?> GetShoppingListAsync(Guid shoppingListId);
        Task UpdateShoppingListAsync(ShoppingList shoppingList);
    }
}
