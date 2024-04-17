namespace RESTfulAPI.Models
{
    public class UpdatePriceRequestResponse :BaseResponse
    {
        public Dictionary<int, decimal> referenceJSON { get; set; }
    }
}
