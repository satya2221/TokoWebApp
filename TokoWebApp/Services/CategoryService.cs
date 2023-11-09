using TokoWebApp.Models;
using TokoWebApp.Services.Interfaces;
using TokoWebApp.Helper;

namespace TokoWebApp.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly HttpClient _httpClient;
        public const string BasePath = "/api/Category";

        public CategoryService(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public async Task AddCategory(CategoryModel category)
        {
            await _httpClient.PostAsJsonAsync(BasePath, category);
        }

        public async Task DeleteCategory(Guid id)
        {
            await _httpClient.DeleteAsync(BasePath + $"/{id}");
        }

        public async Task<List<CategoryModel>> GetCategoriesAsync()
        {
            var response = await _httpClient.GetAsync(BasePath);
            return await response.ReadContentAsync<List<CategoryModel>>();
        }

        public async Task<CategoryModel> GetCategoryById(Guid id)
        {
            var response = await _httpClient.GetAsync(BasePath + $"/{id}");
            return await response.ReadContentAsync<CategoryModel>();
        }

        public async Task UpdateCategory(CategoryModel category)
        {
            await _httpClient.PutAsJsonAsync(BasePath + $"/{category.CategoryId}", category);
        }
    }
}
