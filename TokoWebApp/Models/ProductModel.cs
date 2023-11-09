namespace TokoWebApp.Models
{
    public class ProductModel
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; } = null!;
        public int Stock { get; set; }
        public int Price { get; set; }
        public Guid CatId { get; set; }
        public virtual CategoryModel Cat { get; set; } = null!;
    }
}
