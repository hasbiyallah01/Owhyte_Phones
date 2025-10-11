namespace Owhytee_Phones.Models.ProductModel
{
    public class ProductImageResponse
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public bool IsPrimary { get; set; }
        public int Order {  get; set; }
    }
}
