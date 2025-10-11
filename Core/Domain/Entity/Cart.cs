namespace Owhytee_Phones.Core.Domain.Entity
{
    public class Cart
    {
        public string SessionId { get; set; }

        public ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
    }
}
