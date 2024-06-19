namespace ShopList.Data.DbModels
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Description { get; set; } = null!;
        public bool IsDeleted { get; set; }
        public Guid ShoppingListId { get; set; }
        public ShoppingList ShoppingList { get; set; } = null!;
    }
}
