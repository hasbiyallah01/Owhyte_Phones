using Owhytee_Phones.Core.Domain.Enum;

namespace Owhytee_Phones.Models.ProductModel
{
    public class ProductResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Brand { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string Description { get; set; } = string.Empty;
        public bool IsInStock { get; set; }
        public string RAM { get; set; } = string.Empty;
        public string Storage { get; set; } = string.Empty;
        public string ProductUrl { get; set; } = string.Empty;
        public string Battery { get; set; } = string.Empty;
        public string Display { get; set; } = string.Empty;
        public string Camera { get; set; } = string.Empty;
        public string Processor { get; set; } = string.Empty;
        public string OperatingSystem { get; set; } = string.Empty;
        public List<ProductImageResponse> Images { get; set; } = new List<ProductImageResponse>();
        public List<ProductVariantResponse> Variants { get; set; } = new List<ProductVariantResponse>();
    }

    public class UpdateProductVariantRequest
    {
        public string Color { get; set; } = string.Empty;
        public string Storage { get; set; } = string.Empty;
        public decimal PriceAdjustment { get; set; }
        public bool IsAvailable { get; set; }
    }

    public class ProductImageRequest
    {
        public IFormFile Image { get; set; }
        public string CloudinaryPublicId { get; set; } = string.Empty;
        public bool IsPrimary { get; set; }
        public int Order { get; set; }
    }

    public class OrderFilterResponse
    {
        public int? CooperativeId { get; set; }
        public OrderStatus? Status { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? CustomerName { get; set; }
        public string? SearchTerm { get; set; }
    }
}
