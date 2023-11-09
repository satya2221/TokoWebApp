namespace TokoWebAPI.DTO
{
    public class ProductDto
    {
        public string ProductName { get; set; } = null!;
        public int Stock { get; set; }
        public int Price { get; set; }
        public Guid CatId { get; set; }
    }
}
