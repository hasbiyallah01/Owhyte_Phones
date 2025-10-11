namespace Owhytee_Phones.Models.ProductModel
{
    public class BulUploadResultDto
    {
        public int TotalRows { get; set; }
        public int SuccessfulUploads { get; set; }
        public int FailedUploads { get; set; }
        public List<string> Errors { get; set; } = new List<string>();
        public List<ProductResponse> Products { get; set; } = new List<ProductResponse>();
    }

    public class UpdateStockStatusDto
    {

        public bool IsInStock { get; set; }

    }
}
