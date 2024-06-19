namespace ShopList.Models
{
    public class ProductViewModel
    {
        public Guid Id { get; set; }
        public string Description { get; set; } = null!;
        public bool IsDeleted { get; set; }
        public List<ShoppingListViewModel> ShoppingLists { get; set; } = new List<ShoppingListViewModel>();
        public Guid? SelectedShoppingListId { get; set; }
    }
}
