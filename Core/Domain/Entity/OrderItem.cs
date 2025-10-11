namespace Owhytee_Phones.Core.Domain.Entity
{
    public class OrderItem : Auditables
    {
        public int OrderId { get; set; }
        public int PeoductId { get; set; }
        public int? VariantId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }

        public Order Order { get; set; }
        public Product Product { get; set; }
        public ProductVariant? Variant { get; set; }    
    }
}
