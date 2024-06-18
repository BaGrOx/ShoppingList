using ShopList.Data.DbModels;

namespace ShopList.Data.Repositories.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task AddProductAsync(Product product);
        Task<Product?> FindByIdAsync(Guid id);
        Task UpdateProductAsync(Product product);
    }
}
