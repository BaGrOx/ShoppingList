namespace ShopList.Data.DbModels
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Description { get; set; } = null!;
        public bool IsDeleted { get; set; }
    }
}
