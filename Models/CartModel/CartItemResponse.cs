namespace Owhytee_Phones.Models.CartModel
{
    public class CartItemResponse
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; } = default!;
        public string ProductBrand { get; set; } = default!;
        public string? ProductImageUrl { get; set; }
        public int? ProductVariantId { get; set; }
        public string? VariantDetails { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
