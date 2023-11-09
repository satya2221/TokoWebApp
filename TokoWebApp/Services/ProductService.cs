using TokoWebApp.Models;
using TokoWebApp.Services.Interfaces;
using TokoWebApp.Helper;

namespace TokoWebApp.Services
{
    public class ProductService : IProductService
    {
        private readonly HttpClient _httpClient;
        public const string BasePath = "/api/Product";

        public ProductService(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }
        public async Task AddProduct(ProductModel product)
        {
            await _httpClient.PostAsJsonAsync(BasePath, product);
        }

        public async Task DeleteProduct(Guid id)
        {
            await _httpClient.DeleteAsync(BasePath + $"/{id}");
        }

        public async Task<ProductModel> GetProductById(Guid id)
        {
            var response = await _httpClient.GetAsync(BasePath + $"/{id}");
            return await response.ReadContentAsync<ProductModel>();
        }

        public async Task<List<ProductModel>> GetProductsAsync()
        {
            var response = await _httpClient.GetAsync(BasePath);
            return await response.ReadContentAsync<List<ProductModel>>();
        }

        public async Task UpdateProduct(ProductModel product)
        {
            await _httpClient.PutAsJsonAsync(BasePath + $"/{product.ProductId}", product);
        }
    }
}
