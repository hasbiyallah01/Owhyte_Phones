namespace Owhytee_Phones.Core.Domain.Entity
{
    public class Product : Auditables
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Brand { get; set; } = null!;
        public decimal Price { get; set; }
        public bool InStock { get; set; }
        public string ProductUrl { get; set; }
        public string RAM { get; set; }
        public string Storage { get; set; } = null!;
        public string Battery { get; set; } = null!;
        public string Display { get; set; } = null!;    
        public string Camera { get; set; } = null!;
        public string Processor { get; set; } = null!;
        public string OperatingSystem { get; set; } = null!;

        public ICollection<ProductImage> ProductImages { get; set; } = new List<ProductImage>();
        public ICollection<ProductVariant> ProductVariants { get; set; } = new List<ProductVariant>();
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}
