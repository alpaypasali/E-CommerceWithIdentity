using E_Commerce_Shared;
using E_Commerce_Shared.Dto;
using E_Commerce_Shared.Entity;

namespace E_CommerceWithIdentity.Services.Abstract
{
    public interface IProductService
    {
        Task<ServiceResponse<ProductDto>> CreateProduct(ProductDto model);
        Task<ServiceResponse<bool>> DeleteProduct(int productId);
        Task<ServiceResponse<Product>> GetProduct(int productId);
        Task<ServiceResponse<ProductDto>> UpdateProduct(int productId, ProductDto model);
        Task<ServiceResponse<List<Product>>> GetProductsByCategory(int categoryId);
        Task<ServiceResponse<List<Product>>> ListProduct();
        Task<ServiceResponse<ProductSearchResult>> GetProducts(int page);



    }
}
