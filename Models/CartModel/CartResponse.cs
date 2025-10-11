namespace Owhytee_Phones.Models.CartModel
{
    public class CartResponse
    {
        public int Id { get; set; }
        public string SessionId { get; set; } = default!;
        public List<CartItemResponse> Items { get; set; } = new List<CartItemResponse>();
        public decimal Total { get; set; }
        public int ItemCount { get; set; }
        public DateTime UpadtedAt { get; set; }
    }
}
