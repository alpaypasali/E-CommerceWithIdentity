using E_Commerce_Shared;
using E_Commerce_Shared.Dto;
using E_Commerce_Shared.Entity;

namespace E_CommerceWithIdentity.Services.Abstract
{
    public interface IProductImageService
    {
        Task<ServiceResponse<List<ProductImage>>> CreateMultipleImageProduct(List<ProductImageDto> model , int productId);
    }
}
