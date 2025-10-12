namespace Owhytee_Phones.Core.Domain.Entity
{
    public class CartItem : Auditables
    {
        public int CartId { get; set; }
        public int ProductId { get; set; }
        public int? VariantId { get; set; }
        public int Quantity { get; set; }
        public Cart Cart { get; set; } = default!;
        public Product Product { get; set; } = default!;
        public ProductVariant? Variant { get; set; }
    }
}
