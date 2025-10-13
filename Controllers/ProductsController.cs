using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Owhytee_Phones.Core.Application.Interface.Service;
using Owhytee_Phones.Models.ProductModel;

namespace Owhytee_Phones.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ILogger<ProductsController> _logger;
        private readonly IProductService _productService;
        public ProductsController(ILogger<ProductsController> logger, IProductService productService)
        {
            _logger = logger;
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult> GetProducts(ProductFilterResponse response, int page = 1, int pageSize = 20)
        {
            try
            {
                var products = await _productService.GetProductAsync(response,page,pageSize);
                return Ok(products);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving products");
                return StatusCode(500, new { message = "Internal server error" });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductResponse>> GetProductById(int id)
        {
            try
            {
                var product = await _productService.GetProductByIdAsync(id);
                if (product == null)
                {
                    return NotFound(new { message = "Product not found" });
                }
                return Ok(product);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving product by ID");
                return StatusCode(500, new { message = "Internal server error" });
            }
        }

        [HttpGet("brands")]
        public async Task<ActionResult> GetBrands()
        {
            try
            {
                var brands = await _productService.GetBrandsAsync();
                return Ok(brands);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving brands");
                return StatusCode(500, new { message = "Internal server error" });
            }
        }

        [HttpPost]
        public async Task<ActionResult> CreateProduct([FromBody] ProductRequest productRequest)
        {
            try
            {
                var product = await _productService.CreateProductAsync(productRequest);
                if (product == null)
                {
                    return BadRequest(new { message = "Product creation failed" });
                }
                return Ok(product);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating product");
                return StatusCode(500, new { message = "Internal server error" });
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            try
            {
                var result = await _productService.DeleteProductAsync(id);
                if (!result)
                {
                    return NotFound(new { message = "Product not found or deletion failed" });
                }
                return Ok(new { message = "Product deleted successfully" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting product");
                return StatusCode(500, new { message = "Internal server error" });
            }
        }

        [HttpPut("{id}/stock")]
        public async Task<ActionResult> UpdateStockStatus(int id, [FromBody] bool isInStock)
        {
            try
            {
                var result = await _productService.UpdateStockStatusAsync(id, isInStock);
                if (!result)
                {
                    return NotFound(new { message = "Product not found or update failed" });
                }
                return Ok(new { message = "Stock status updated successfully" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating stock status");
                return StatusCode(500, new { message = "Internal server error" });
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateProduct(int id, [FromBody] UpdateProductRequest productRequest)
        {
            try
            {
                var product = await _productService.UpdateProductAsync(id, productRequest);
                if (product == null)
                {
                    return NotFound(new { message = "Product not found or update failed" });
                }
                return Ok(product);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating product");
                return StatusCode(500, new { message = "Internal server error" });
            }
        }


        [HttpGet("{id}/variants")]
        public async Task<ActionResult<List<ProductVariantResponse>>> GetProductVariants(int id)
        {
            var variants = await _productService.GetProductVariantsAsync(id);
            return Ok(variants);
        }

        [HttpPost("{id}/variants")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ProductVariantResponse>> CreateVariant(int id, ProductVariantRequest createVariantDto)
        {
            var variant = await _productService.AddProductVariantAsync(id, createVariantDto);
            return Ok(variant);
        }

        [HttpPut("variants/{variantId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateVariant(int variantId, UpdateProductVariantRequest updateVariantDto)
        {
            var variant = await _productService.UpdateVariantAsync(variantId, updateVariantDto);
            if (variant == null)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("variants/{variantId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteVariant(int variantId)
        {
            var success = await _productService.DeleteVariantAsync(variantId);
            if (!success)
                return NotFound();

            return NoContent();
        }

        [HttpGet("{id}/images")]
        public async Task<ActionResult<List<ProductImageResponse>>> GetProductImages(int id)
        {
            var images = await _productService.GetProductImageAsync(id);
            return Ok(images);
        }

        [HttpPost("{id}/images")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ProductImageResponse>> AddProductImage(int id, ProductImageRequest createImageDto)
        {
            var image = await _productService.AddProductImageAsync(id, createImageDto);
            return Ok(image);
        }

        [HttpDelete("images/{imageId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteProductImage(int imageId)
        {
            var success = await _productService.DeleteProductImageAsync(imageId);
            if (!success)
                return NotFound();

            return NoContent();
        }

        [HttpPatch("images/{imageId}/primary")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> SetPrimaryImage(int imageId)
        {
            var success = await _productService.SetPrimaryImageAsync(imageId);
            if (!success)
                return NotFound();

            return NoContent();
        }
    }
}
