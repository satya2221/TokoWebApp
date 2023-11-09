using TokoWebApp.Models;

namespace TokoWebApp.Services.Interfaces
{
    public interface IProductService
    {
        Task<List<ProductModel>> GetProductsAsync();
        Task AddProduct(ProductModel product);
        Task UpdateProduct(ProductModel product);
        Task DeleteProduct(Guid id);
        Task<ProductModel> GetProductById(Guid id);
    }
}
