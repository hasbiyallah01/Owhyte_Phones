namespace Owhytee_Phones.Models.ProductModel
{
    public class ProductVariantResponse
    {
        public int Id { get; set; }
        public string Color { get; set; } = string.Empty;
        public string Storage { get; set; } = string.Empty;
        public decimal PriceAdjustment { get; set; }
        public bool IsAvailable { get; set; }   
    }

    public class ProductVariantRequest
    {
        public string Color { get; set; } = string.Empty;
        public string Storage { get; set; } = string.Empty;
        public decimal PriceAdjustment { get; set; }
        public bool IsAvailable { get; set; } = true;
    }
}
