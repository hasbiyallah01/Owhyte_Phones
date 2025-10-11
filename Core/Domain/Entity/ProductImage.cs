namespace Owhytee_Phones.Core.Domain.Entity
{
    public class ProductImage : Auditables
    {
        public string IamgeUrl { get; set; } = string.Empty;
        public bool IsPrimary { get; set; }
        public int Order { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;
    }
}
