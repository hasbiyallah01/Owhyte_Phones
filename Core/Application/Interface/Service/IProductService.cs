using Owhytee_Phones.Models.ProductModel;

namespace Owhytee_Phones.Core.Application.Interface.Service
{
    public interface IProductService
    {
        Task<PagedResult<ProductResponse>> GetProductAsync(ProductFilterResponse filter, int page = 1, int pageSize = 20);
        Task<ProductResponse> GetProductByIdAsync(int Id);
        Task<List<string>> GetBrandsAsync();
        Task<ProductResponse> CreateProductAsync(int id, UpdateProductRequest updateProductDto);
        Task<bool> DeleteProductAsync(int Id);
        Task<bool> UpdateStockStatusAsync(int Id, bool isInStock);
        Task<List<ProductVariantResponse>> GetProductVariantsAsync(int productId);
        Task<ProductVariantResponse> AddProductVariantAsync(int productId, ProductVariantRequest variantRequest); 
        Task<ProductVariantResponse> UpdateVariantAsync(int variantId, UpdateProductVariantRequest variantRequest);
        Task<bool> DeleteVariantAsync(int variantId);
        Task<List<ProductImageResponse>> GetProductImageAsync(int productId);
        Task<ProductImageResponse> AddProductImageAsync(int productId, ProductImageRequest imageRequest);
        Task<bool> DeleteProductImageAsync(int imageId);
        Task<bool> SetPrimaryImageAsync(int imageId);
    }
}
