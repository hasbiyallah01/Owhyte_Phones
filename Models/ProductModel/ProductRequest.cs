namespace Owhytee_Phones.Models.ProductModel
{
    public class PagedResult<T>
    {
        public List<T> Items { get; set; } = new();
        public int TotalCount { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public bool HasNextPage { get; set; }
        public bool HasPreviousPage { get; set; }
    }

    public class UpdateProductRequest
    {
        public string Name { get; set; } = string.Empty;
        public string Brand { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string Description { get; set; } = string.Empty;
        public bool IsInStock { get; set; }
        public string RAM { get; set; } = string.Empty;
        public string Storage { get; set; } = string.Empty;
        public string Battery { get; set; } = string.Empty;
        public string Display { get; set; } = string.Empty;
        public string Camera { get; set; } = string.Empty;
        public string Processor { get; set; } = string.Empty;
        public string OperatingSystem { get; set; } = string.Empty;
    }

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
