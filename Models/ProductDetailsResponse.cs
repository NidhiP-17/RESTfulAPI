namespace RESTfulAPI.Models
{
    public class ProductDetailsResponse : BaseResponse
    {
        public Product data { get; set; }
        public List<Product> dataList { get; set; }
    }
}
