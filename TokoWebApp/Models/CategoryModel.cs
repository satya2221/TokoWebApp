namespace TokoWebApp.Models
{
    public class CategoryModel
    {
        public Guid CategoryId { get; set; }

        public string CategoryName { get; set; } = null!;

        //public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
