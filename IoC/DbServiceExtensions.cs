using ShopList.Data.Repositories;
using ShopList.Data.Repositories.Interfaces;

namespace ShopList.IoC
{
    public static class DbServiceExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IShoppingListRepository, ShoppingListRepository>();

            return services;
        }
    }
}
