using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopList.Data.DbModels;

namespace ShopList.Data.Configuration
{
    public class ShoopingListConfiguration : IEntityTypeConfiguration<ShoppingList>
    {
        public void Configure(EntityTypeBuilder<ShoppingList> builder)
        {
            builder.ToTable("ShoppingList");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.PlannedPurchaseDate).IsRequired();

            builder.HasMany(x => x.Products)
                .WithOne(p => p.ShoppingList)
                .HasForeignKey(p => p.ShoppingListId);
        }
    }
}
