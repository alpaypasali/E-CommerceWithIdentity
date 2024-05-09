using E_Commerce_Shared.Dto;
using E_CommerceWithIdentity.Services.Abstract;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;

namespace E_CommerceWithIdentity.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _categoryService.ListCategory();
            if(result.Data == null)
            {
                return View(result.Data);
            }
            return View(result.Data);
        }
        [HttpGet]
        public IActionResult CreateCategory()
        {
            
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateCategory(CategoryDto model)
        {
            var result = await _categoryService.CreateCategory(model);
            if(result.Success == true)
            {
                return RedirectToAction("Index" , "Category");

            }
            return View();
        }

        [HttpGet("UpdateCategory/{categoryId}")]
        public async Task<IActionResult> UpdateCategory([FromRoute]int categoryId)
        {
            var category = await _categoryService.GetCategory(categoryId);
            if(category == null)
            {
                return View();
            }
            return View(category.Data);

        }
        [HttpPost("UpdateCategory/{categoryId}")]
        public async Task<IActionResult> UpdateCategory([FromRoute]int categoryId, CategoryDto model)
        {

            var result = await _categoryService.UpdateCategory(categoryId, model);
            if(result.Success == true)
            {
                return RedirectToAction("Index" , "Category");
            }
            return View();
        }

        [HttpPost("{categoryId}")]
        public async Task<IActionResult> DeleteCategory([FromRoute] int categoryId)
        {
            var result = await _categoryService.DeleteCategory(categoryId);
            if(result.Success == true)
            {
                return RedirectToAction("Index", "Category");
            }
            return BadRequest("Hata Oluştu");
        }
    }
}
