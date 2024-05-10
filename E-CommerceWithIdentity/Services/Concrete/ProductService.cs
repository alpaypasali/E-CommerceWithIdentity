using E_Commerce_Shared;
using E_Commerce_Shared.Dto;
using E_Commerce_Shared.Entity;
using E_CommerceWithIdentity.Areas.Identity.Data;
using E_CommerceWithIdentity.Services.Abstract;
using Microsoft.EntityFrameworkCore;

namespace E_CommerceWithIdentity.Services.Concrete
{
    public class ProductService : IProductService
    {
        private readonly E_CommerceWithIdentityContext _context;

        public ProductService(E_CommerceWithIdentityContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse<ProductDto>> CreateProduct(ProductDto model)
        {
            ServiceResponse<ProductDto> response = new ServiceResponse<ProductDto>();
            Product product = new()
            {
                CategoryId = model.CategoryId,
                ImageUrl = model.ImageUrl,
                ProductDescription = model.ProductDescription,
                Price = model.Price,
                ProductName = model.ProductName,

            };
            _context.Add(product);
            if(await _context.SaveChangesAsync() > 0 ) {
                response.Success = true;
                response.Message = "Product is created";
                return response;

            }
            else
            {
                response.Success = false;
                response.Message = "Product is not created";
                return response;

            }
        }

        public async Task<ServiceResponse<bool>> DeleteProduct(int productId)
        {
           var product = await _context.Products.FindAsync(productId);
            ServiceResponse<bool> _response = new ServiceResponse<bool>();
            if(product is not null)
            {
                _context.Products.Remove(product);
                if (await _context.SaveChangesAsync() > 0) {

                    _response.Success = true;
                    _response.Message = "Product is deleted";
                    return _response;
                
                }
               
                
            }
            _response.Success = false;
            _response.Message = "Operation failed";
            return _response;
        }

        public async Task<ServiceResponse<Product>> GetProduct(int productId)
        {
            var result = await _context.Products.Include(x=>x.Category).FirstOrDefaultAsync(x=>x.Id == productId);
            ServiceResponse<Product> _response = new ServiceResponse<Product>();
            if(result is not null)
            {
                _response.Data = result;
                return _response;
            }
            _response.Success= false;
            return _response;
        }

        public async Task<ServiceResponse<ProductSearchResult>> GetProducts(int page)
        {
            var pageResult = 10f;
            var result = await _context.Products.ToListAsync();
            var pageCount = Math.Ceiling((result).Count / pageResult);
            var products = await _context.Products.Skip((page - 1) * (int)pageResult).Take((int)pageResult).ToListAsync();
            ServiceResponse<ProductSearchResult> _response = new ServiceResponse<ProductSearchResult>()
            {
                Data = new ProductSearchResult
                {
                    Products = products,
                    CurrentPage = page,
                    Pages = (int)pageCount,

                }

            };
            return _response; 

        }

        public async Task<ServiceResponse<List<Product>>> GetProductsByCategory(int categoryId)
        {
            var result = await _context.Products.Where(x=>x.CategoryId == categoryId).ToListAsync();
            ServiceResponse<List<Product>> _response = new ServiceResponse<List<Product>>();
            if(result is not null)
            {
                _response.Data = result;
                _response.Success = true;
                _response.Message = "Proccess is succes";
                return _response;
            }
            else
            {
                _response.Success = false;
                _response.Message = "Proccess is failed";
                return _response;
            }
        }

        public async Task<ServiceResponse<List<Product>>> ListProduct()
        {
            var result = await _context.Products.ToListAsync();
            ServiceResponse<List<Product>> _response = new ServiceResponse<List<Product>>();
            if (result is not null)
            {
                _response.Data = result;
                _response.Success = true;
                _response.Message = "Proccess is succes";
                return _response;
            }
            else
            {
                _response.Success = false;
                _response.Message = "Proccess is failed";
                return _response;
            }
        }

        public async Task<ServiceResponse<ProductDto>> UpdateProduct(int productId, ProductDto model)
        {
            ServiceResponse<ProductDto> _response = new ServiceResponse<ProductDto>();
            var _object = await _context.Products.FindAsync(productId);
            if(_object is not null)
            {
                _object.ProductDescription = model.ProductDescription;
                _object.Price = model.Price;
                _object.ProductName = model.ProductName;
                _object.CategoryId = model.CategoryId;
                _object.ImageUrl = model.ImageUrl;
                await _context.SaveChangesAsync();
                _response.Success = true;
                _response.Message = "Update proccess is success";
               
                return _response;
            }
            _response.Success = false;
            _response.Message = "Update proccess is failed";
            return _response;
        }
    }
}
