namespace ShopList.Models
{
    public class ShoppingListViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public DateTime PlannedPurchaseDate { get; set; }
        public List<ProductViewModel> Products { get; set; } = new List<ProductViewModel>();
    }
}
