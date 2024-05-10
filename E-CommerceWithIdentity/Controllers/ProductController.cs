using E_Commerce_Shared.Dto;
using E_Commerce_Shared.Entity;
using E_CommerceWithIdentity.Models;
using E_CommerceWithIdentity.Services.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Build.Framework;

namespace E_CommerceWithIdentity.Controllers
{
    [Authorize(Roles ="Admin")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductController(IProductService productService, ICategoryService categoryService, IWebHostEnvironment webHostEnvironment)
        {
            _productService = productService;
            _categoryService = categoryService;
            _webHostEnvironment = webHostEnvironment;
        }
        [HttpGet]
        public async Task<IActionResult> CreateProduct()
        {
            ProductModel model = new ProductModel();
            LoadCategorySelectDataView();
            ViewData["Title"] = "Ürün oluştur";
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> CreateProduct(ProductModel model)
        {
            if(ModelState.IsValid)
            {
                ProductDto productDto = new ProductDto();
                productDto.ProductDescription = model.ProductDescription;
                productDto.ProductName = model.ProductName;
                productDto.Price = model.Price;
                productDto.CategoryId = model.CategoryId;   
                if(model.CoverPhoto is not null)
                {
                    string folder = "Images\\";
                    folder += model.CoverPhoto.FileName;
                    string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folder);
                    await model.CoverPhoto.CopyToAsync(new FileStream(serverFolder, FileMode.Create));  

                }
                productDto.ImageUrl = model.CoverPhoto.FileName;
                var result = await _productService.CreateProduct(productDto);
                if(result.Success is true)
                {
                    return RedirectToAction("ProductList", "Product");
                }

            }
            return RedirectToAction("ProductList", "Product");

        }
        public async Task<IActionResult> ProductList()
        {
            var result = await _productService.ListProduct();
            if(result.Data is not null)
            {
                return View(result.Data);
            }
            return RedirectToAction("CreateProduct", "Product");
        }
        [HttpPost("Delete/{productId}")]
        public async Task<IActionResult> DeleteProduct([FromRoute] int productId)
        {
            var result = await _productService.DeleteProduct(productId);
            if(result.Success is true)
            {
                return RedirectToAction("ProductList", "Product");

            }
            return BadRequest("One error occured");
        }

        public IActionResult Index()
        {
            return View();
        }

        private void LoadCategorySelectDataView()
        {
            List<Category> categories = _categoryService.GetCategories();
            List<SelectListItem> selectListItems = categories.Select(x=> new SelectListItem(x.CategoryName,x.Id.ToString())).ToList();
            ViewData["categories"] = selectListItems;
        }
    }
}
