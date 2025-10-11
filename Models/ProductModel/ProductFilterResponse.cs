namespace Owhytee_Phones.Models.ProductModel
{
    public class ProductFilterResponse
    {
        public string? Brand { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public string? SearchTerm { get; set; }
        public bool? InStockOnly { get; set; }
    }
}
