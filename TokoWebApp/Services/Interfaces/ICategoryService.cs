using TokoWebApp.Models;

namespace TokoWebApp.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<List<CategoryModel>> GetCategoriesAsync();
        Task AddCategory(CategoryModel category);
        Task UpdateCategory(CategoryModel category);
        Task DeleteCategory(Guid id);
        Task<CategoryModel> GetCategoryById(Guid id);
    }
}
