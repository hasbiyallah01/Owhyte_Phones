namespace Owhytee_Phones.Models.OrderModel
{
    public class OrderItemRequest
    {
        public int ProductId { get; set; }
        public int? ProductVariantId { get; set; }
        public int Quantity { get; set; }
    }
}
