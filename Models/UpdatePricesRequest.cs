using System.ComponentModel.DataAnnotations;

namespace RESTfulAPI.Models
{
    public class UpdatePricesRequest
    {
        [Required]
        public Dictionary<int, decimal>? PriceUpdates { get; set; }
    }
}
