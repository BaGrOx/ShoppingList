using Microsoft.EntityFrameworkCore;
using ShopList.Data.DbModels;
using ShopList.Data.Repositories.Interfaces;

namespace ShopList.Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ProductRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddProductAsync(Product product)
        {
            await _dbContext.Products.AddAsync(product);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateProductAsync(Product product)
        {
            _dbContext.Update(product);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Product?> FindByIdAsync(Guid id) => await _dbContext.Products.FirstOrDefaultAsync(x => x.Id == id);

        public async Task<IEnumerable<Product>> GetAllProductsAsync() => await _dbContext.Products.ToListAsync();
    }
}
