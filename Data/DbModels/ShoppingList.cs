namespace ShopList.Data.DbModels
{
    public class ShoppingList
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public DateTime PlannedPurchaseDate { get; set; }
        public List<Product> Products { get; set; } = new List<Product>();
    }
}
