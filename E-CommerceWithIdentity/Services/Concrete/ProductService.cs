using E_Commerce_Shared;
using E_Commerce_Shared.Dto;
using E_Commerce_Shared.Entity;
using E_CommerceWithIdentity.Areas.Identity.Data;
using E_CommerceWithIdentity.Services.Abstract;

namespace E_CommerceWithIdentity.Services.Concrete
{
    public class ProductService : IProductService
    {
        private readonly E_CommerceWithIdentityContext _context;

        public ProductService(E_CommerceWithIdentityContext context)
        {
            _context = context;
        }

        public Task<ServiceResponse<ProductDto>> CreateProduct(ProductDto model)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<bool>> DeleteProduct(int productId)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<Product>> GetProduct(int productId)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<ProductSearchResult>> GetProducts(int page)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<List<Product>>> GetProductsByCategory(int categoryId)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<List<Product>>> ListProduct()
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<ProductDto>> UpdateProduct(int productId, ProductDto model)
        {
            throw new NotImplementedException();
        }
    }
}
