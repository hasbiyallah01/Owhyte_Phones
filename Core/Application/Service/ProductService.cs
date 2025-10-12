using Owhytee_Phones.Core.Application.Interface.Repository;
using Owhytee_Phones.Core.Application.Interface.Service;
using Owhytee_Phones.Core.Domain.Entity;
using Owhytee_Phones.Models.ProductModel;

namespace Owhytee_Phones.Core.Application.Service
{
    public class ProductService : IProductService
    {
        private IProductRepository _productRepository;
        private IUnitOfWork _unitOfWork;
        private IProductImageRepository _productImageRepository;
        private IProductVariantRepository _productVariantRepository;
        private ICloudinaryService _cloudinaryService;
        public ProductService(IProductRepository productRepository, IUnitOfWork unitOfWork, IProductVariantRepository 
            productVariantRepository, IProductImageRepository productImageRepository, ICloudinaryService cloudinaryService)
        {
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
            _productImageRepository = productImageRepository;
            _productVariantRepository = productVariantRepository;
            _cloudinaryService = cloudinaryService;
        }

        public async Task<ProductImageResponse> AddProductImageAsync(int productId, ProductImageRequest imageRequest)
        {
            var product = await _productRepository.GetAsync(productId);
            if (product == null)
            {
                return null;
            }
            var upload = await _cloudinaryService.UploadImageAsync(imageRequest.Image, "product_images");
            if (upload == null)
            {
                return null;
            }

            var image = new ProductImage
            {
                ProductId = productId,
                IamgeUrl = upload,
                IsPrimary = imageRequest.IsPrimary,
                Order = imageRequest.Order,
                Product = product,
            };
            await _productImageRepository.AddAsync(image);
            await _unitOfWork.SaveAsync();
            return new ProductImageResponse
            {
                Id = image.Id,
                ImageUrl = image.IamgeUrl,
                IsPrimary = image.IsPrimary,
                Order = image.Order,
            };
        }

        public async Task<ProductVariantResponse> AddProductVariantAsync(int productId, ProductVariantRequest variantRequest)
        {
            var product = await _productRepository.GetAsync(productId);
            if (product == null) 
            {
                return null;
            }
            var variant = new ProductVariant
            {
                ProductId = productId,
                Color = variantRequest.Color,
                Storage = variantRequest.Storage,
                PriceAdjustment = variantRequest.PriceAdjustment,
                IsAvaliable = variantRequest.IsAvailable
            };

            await _productVariantRepository.AddAsync(variant);
            await _unitOfWork.SaveAsync();
            return new ProductVariantResponse
            {
                Id = variant.Id,
                Color = variant.Color,
                Storage = variant.Storage,
                PriceAdjustment = variant.PriceAdjustment,
                IsAvailable = variant.IsAvaliable
            };
        }

        public async Task<ProductResponse> CreateProductAsync(ProductRequest productRequest)
        {
            var product = new Product
            {
                Name = productRequest.Name,
                Battery = productRequest.Battery,
                Brand = productRequest.Brand,
                Camera = productRequest.Camera,
                Description = productRequest.Description,
                Display = productRequest.Display,
                OperatingSystem = productRequest.OperatingSystem,
                Price = productRequest.Price,
                Processor = productRequest.Processor,
                ProductUrl = productRequest.ProductUrl,
                RAM = productRequest.RAM,
                Storage = productRequest.Storage,
            };
            await _productRepository.AddAsync(product);
            await _unitOfWork.SaveAsync();

            return MapToProductDto(product);
        }

        public async Task<bool> DeleteProductAsync(int Id)
        {
            var product = await _productRepository.GetAsync(Id);
            if (product == null)
                return false;
             _productRepository.Remove(product);
            await _unitOfWork.SaveAsync();
            return true;
        }

        public async Task<bool> DeleteProductImageAsync(int imageId)
        {
            var productImage = await _productImageRepository.GetAsync(imageId);
            if (productImage == null)
                return false;
            _productImageRepository.Remove(productImage);
            await _unitOfWork.SaveAsync();
            return true;
        }

        public async Task<bool> DeleteVariantAsync(int variantId)
        {
            var variant = await _productVariantRepository.GetAsync(variantId);
            if (variant == null)
                return false;
            _productVariantRepository.Remove(variant);
            await _unitOfWork.SaveAsync();
            return true;
        }

        public async Task<List<string>> GetBrandsAsync()
        {
            var products = await _productRepository.GetAllAsync();
            var productsByBrand = products.Select(p  => p.Brand)
                                        .Distinct()
                                        .OrderBy(p => p)
                                        .ToList();

            return productsByBrand;
        }

        public async Task<PagedResult<ProductResponse>> GetProductAsync(ProductFilterResponse filter, int page = 1, int pageSize = 20)
        {
            var query = await _productRepository.GetAllAsync();

            if(!string.IsNullOrEmpty(filter.Brand))
                query = query.Where(p => p.Brand.ToLower() == filter.Brand.ToLower());
            if (filter.MinPrice.HasValue)
                query = query.Where(p => p.Price == filter.MinPrice.Value);
            if (filter.MaxPrice.HasValue)
                query = query.Where(p => p.Price == filter.MaxPrice.Value);
            if (!string.IsNullOrEmpty(filter.SearchTerm))
                query = query.Where(p => p.Name.Contains(filter.SearchTerm) || p.Description.Contains(filter.SearchTerm));

            if(filter.InStockOnly == true)
                query = query.Where(p => p.InStock);

            var totalCount = query.Count();

            var totalPages = (int)Math.Ceiling(totalCount/(double)pageSize);

            var products = query
                            .Skip((page - 1) * pageSize)
                            .Take(pageSize)
                            .ToList();

            var productResponse = products.Select(MapToProductDto).ToList();
            return new PagedResult<ProductResponse>
            {
                TotalPages = totalPages,
                Items = productResponse,
                Page = page,
                TotalCount = totalCount,
                PageSize = pageSize,
                HasNextPage = page < totalPages,
                HasPreviousPage = page > 1            
            }; 
        }

        public async Task<ProductResponse> GetProductByIdAsync(int Id)
        {
            var product = await _productRepository.GetAsync(Id);
            return product != null ? MapToProductDto(product) : null;
        }

        public async Task<List<ProductImageResponse>> GetProductImageAsync(int productId)
        {
            var answer = await _productImageRepository.GetAllAsync();
            var images = answer.OrderBy(i => i.Order);

            return images.Select(i => new ProductImageResponse
            {
                Id = i.Id,
                ImageUrl = i.IamgeUrl,
                IsPrimary = i.IsPrimary,
                Order = i.Order,
            }).ToList();
        }

        public async Task<List<ProductVariantResponse>> GetProductVariantsAsync(int productId)
        {
            var variants = await _productVariantRepository.GetAllByProductIdAsync(productId);
            return variants.Select(v => new ProductVariantResponse
            {
                Id = v.Id,
                Color = v.Color,
                IsAvailable = v.IsAvaliable,
                PriceAdjustment = v.PriceAdjustment,
                Storage = v.Storage,
            }).ToList();
        }

        public async Task<bool> SetPrimaryImageAsync(int imageId)
        {
            var image = await _productImageRepository.GetAsync(imageId);
            if (image == null)
                return false;
            var otherImages = await _productImageRepository.GetAllAsync(image.ProductId, imageId);

            foreach (var item in otherImages)
            {
                item.IsPrimary = false;
            }
            image.IsPrimary = true;
            _productImageRepository.Update(image);
            await _unitOfWork.SaveAsync();
            return true;
        }

        public async Task<ProductResponse> UpdateProductAsync(int id, UpdateProductRequest productRequest)
        {
            var product = await _productRepository.GetAsync(id);
            if (product == null)
                return null;

            product.RAM = productRequest.RAM;
            product.Name = productRequest.Name; 
            product.Description = productRequest.Description;
            product.Processor = productRequest.Processor;
            product.Price = productRequest.Price;
            product.Battery = productRequest.Battery;
            product.Display = productRequest.Display;
            product.OperatingSystem = productRequest.OperatingSystem;
            product.Brand = productRequest.Brand;
            product.InStock = productRequest.IsInStock;
            productRequest.Storage = productRequest.Storage;
            productRequest.Camera = productRequest.Camera;

            return await GetProductByIdAsync(id);
        }

        public async Task<bool> UpdateStockStatusAsync(int Id, bool isInStock)
        {
            var product = await _productRepository.GetAsync(Id); 
            if (product == null) return false;
            product.InStock = isInStock;
            _productRepository.Update(product);
            await _unitOfWork.SaveAsync();
            return true;
        }

        public async Task<ProductVariantResponse> UpdateVariantAsync(int variantId, UpdateProductVariantRequest variantRequest)
        {
            var variant = await _productVariantRepository.GetAsync(variantId);
            if (variant == null)
                return null;

            variant.Color = variantRequest.Color;
            variant.IsAvaliable = variantRequest.IsAvailable;
            variant.PriceAdjustment = variantRequest.PriceAdjustment;
            variant.Storage = variantRequest.Storage;

            _productVariantRepository.Update(variant);
            await _unitOfWork.SaveAsync();

            return new ProductVariantResponse
            {
                Id = variantId,
                Color = variantRequest.Color,
                PriceAdjustment = variantRequest.PriceAdjustment,
                Storage = variantRequest.Storage,
                IsAvailable = variantRequest.IsAvailable,
            };
        }

        private ProductResponse MapToProductDto(Product product)
        {
            return new ProductResponse
            {
                Id = product.Id,
                Price = product.Price,
                Processor = product.Processor,
                ProductUrl = product.ProductUrl,
                Battery = product.Battery,
                Brand = product.Brand,
                Camera = product.Camera,
                Description = product.Description,
                Display = product.Display,
                OperatingSystem = product.OperatingSystem,
                Name = product.Name,
                RAM = product.RAM,
                Storage = product.Storage,
                IsInStock = product.InStock,
                Images = product.ProductImages.Select(i => new ProductImageResponse
                {
                    Id = i.Id,
                    IsPrimary = i.IsPrimary,
                    Order = i.Order,
                    ImageUrl = i.IamgeUrl,
                }).ToList(),
                Variants = product.ProductVariants.Select(i => new ProductVariantResponse
                {
                    Id = i.Id,
                    Color = i.Color,
                    IsAvailable = i.IsAvaliable,
                    PriceAdjustment = i.PriceAdjustment,
                    Storage = i.Storage,
                }).ToList()
            };
        }
    }
}