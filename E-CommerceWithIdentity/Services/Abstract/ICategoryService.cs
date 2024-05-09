using E_Commerce_Shared;
using E_Commerce_Shared.Dto;
using E_Commerce_Shared.Entity;

namespace E_CommerceWithIdentity.Services.Abstract
{
    public interface ICategoryService
    {
        Task<ServiceResponse<CategoryDto>> CreateCategory(CategoryDto request);
        Task<ServiceResponse<bool>> DeleteCategory(int categoryId);
        Task<ServiceResponse<CategoryDto>> GetCategory(int categoryId);
        Task<ServiceResponse<CategoryDto>> UpdateCategory(int categoryId, CategoryDto categoryDto);
        Task<ServiceResponse<List<Category>>> ListCategory();
        List<Category> GetCategories();
        Task<Category> GetCategoryByName(string categoryName);

    }
}
