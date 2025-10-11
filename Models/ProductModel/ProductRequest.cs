namespace Owhytee_Phones.Models.ProductModel
{
    public class ProductRequest
    {
        public string Name { get; set; } = string.Empty;
        public string Brand { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string Description { get; set; } = string.Empty;
        public string RAM { get; set; } = string.Empty;
        public string Storage { get; set; } = string.Empty;
        public string ProductUrl { get; set; } = string.Empty;
        public string Battery { get; set; } = string.Empty;
        public string Display { get; set; } = string.Empty;
        public string Camera { get; set; } = string.Empty;
        public string Processor { get; set; } = string.Empty;
        public string OperatingSystem { get; set; } = string.Empty;
    }
}
