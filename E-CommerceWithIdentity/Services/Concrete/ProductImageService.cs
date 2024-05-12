using E_Commerce_Shared;
using E_Commerce_Shared.Dto;
using E_Commerce_Shared.Entity;
using E_CommerceWithIdentity.Areas.Identity.Data;
using E_CommerceWithIdentity.Services.Abstract;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace E_CommerceWithIdentity.Services.Concrete
{
    public class ProductImageService : IProductImageService
    {

        private readonly E_CommerceWithIdentityContext _context;

        public ProductImageService(E_CommerceWithIdentityContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse<List<ProductImage>>> CreateMultipleImageProduct(List<ProductImageDto> model, int productId)
        {
            ServiceResponse<List<ProductImage>> _productImage = new ServiceResponse<List<ProductImage>>();
            List<ProductImage> productImages = new List<ProductImage>();

            var result = await _context.Products.Where(x => x.Id == productId).Include(x => x.ProductImages).FirstOrDefaultAsync();
            foreach (var item in model) {
            
                ProductImage request = new ProductImage();
                request.ImageName = item.ImageName;
                productImages.Add(request);

            
            }

            if(model.Count is not 0)
            {
                _context.ProductImages.AddRange(productImages);
                await _context.SaveChangesAsync();   
                foreach(var image in productImages)
                {

                    var insertedImage = _context.ProductImages.Single(x=>x.Id == image.Id);
                    result.ProductImages.Add(insertedImage);
                    await _context.SaveChangesAsync();  
                }
                _productImage.Data = productImages;
                return _productImage; 
            }
            return null;

        }
    }
}
