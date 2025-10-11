namespace Owhytee_Phones.Core.Domain.Entity
{
    public class ProductVariant : Auditables
    {
        public string Color { get; set; } = string.Empty;
        public string Storage { get; set; } = string.Empty;
        public decimal PriceAdjustment { get; set; }
        public bool IsAvaliable { get; set; } = true;
        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;
    }
}
