using E_Commerce_Shared;
using E_Commerce_Shared.Dto;
using E_Commerce_Shared.Entity;
using E_CommerceWithIdentity.Areas.Identity.Data;
using E_CommerceWithIdentity.Services.Abstract;
using Microsoft.EntityFrameworkCore;

namespace E_CommerceWithIdentity.Services.Concrete
{
    public class CategoryService : ICategoryService
    {
        private readonly E_CommerceWithIdentityContext _context;

        public CategoryService(E_CommerceWithIdentityContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse<CategoryDto>> CreateCategory(CategoryDto request)
        {
            Category category = new Category
            {
                CategoryName = request.Name
            };
            var result = _context.Categories.Add(category);
            if (await _context.SaveChangesAsync() > 0)
            {
                return new ServiceResponse<CategoryDto>
                {
                    Message = "Operation Success",
                    Success = true,
                };
            }
            else
            {
                return new ServiceResponse<CategoryDto>
                {
                    Message = "Operation Failed",
                    Success = false,
                };
            }
        }

        public async Task<ServiceResponse<bool>> DeleteCategory(int categoryId)
        {
            var result = await _context.Categories.FindAsync(categoryId);
            if (result == null)
            {
                return new ServiceResponse<bool>
                {
                    Message = "Category is not found",
                    Success = false,
                };
            }
            else
            {
                _context.Categories.Remove(result);
                if (await _context.SaveChangesAsync() > 0)
                {
                    return new ServiceResponse<bool>
                    {
                        Message = "Operation Success",
                        Success = true,
                    };

                }
                else
                {
                    return new ServiceResponse<bool>
                    {
                        Message = "Operation Failed",
                        Success = false,
                    };

                }
            }
        }

        public List<Category> GetCategories()
        {
            var result = _context.Categories.ToList();
            if (result != null)
            {
                return result;
            }
            return null;
        }

        public async Task<ServiceResponse<CategoryDto>> GetCategory(int categoryId)
        {
            var result = await _context.Categories.Include(x => x.Products).FirstOrDefaultAsync(x => x.Id == categoryId);
            CategoryDto categoryDto = new()
            {
                Name = result.CategoryName,
                Id = categoryId
            };
            if (result == null)
            {
                return new ServiceResponse<CategoryDto>
                {
                    Success = true,
                    Message = "This category has not product",
                };

            }
            else
            {
                return new ServiceResponse<CategoryDto> { Success = true, Data = categoryDto };
            }
        }

        public async Task<Category> GetCategoryByName(string categoryName)
        {
            var result = await _context.Categories.FirstOrDefaultAsync(x => x.CategoryName == categoryName);
            if (result != null) { return result; }
            return null;
        }

        public async Task<ServiceResponse<List<Category>>> ListCategory()
        {
            ServiceResponse<List<Category>> serviceResponse = new ServiceResponse<List<Category>>();
            var result = await _context.Categories.ToListAsync();
            if (result.Count == 0)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Categories are not found";
                return serviceResponse;

            }
            else
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Categories listed";
                serviceResponse.Data = result;
                return serviceResponse;

            }
        }

        public async Task<ServiceResponse<CategoryDto>> UpdateCategory(int categoryId, CategoryDto categoryDto)
        {
            var result = await _context.Categories.FindAsync(categoryId);
            if (result == null)
            {
                return new ServiceResponse<CategoryDto>
                {
                    Success = false,
                    Message = "Category is not found"
                };
            }
            else
            {
                result.CategoryName = categoryDto.Name;
                await _context.SaveChangesAsync();
                return new ServiceResponse<CategoryDto>
                {
                    Success = true,
                    Message = "your proccess is successful"

                };
            }

        }
    }
}
