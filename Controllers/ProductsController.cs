using Azure;
using Microsoft.AspNetCore.Mvc;
using RESTfulAPI.Data;
using RESTfulAPI.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Diagnostics;

namespace RESTfulAPI.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductsController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpPost("list-products")]
        [Produces("application/json")]
        public ProductDetailsResponse ListProducts()
        {
            var response = new ProductDetailsResponse { Message = "", Status = 400 };
            try
            {
                var products = _context.Products.ToList();
                if (products == null)
                {
                    response.dataList = null;
                }
                else
                {
                    response.dataList = products.ToList();
                }
                response.Status = 200;
                return response;
            }
            catch (Exception ex)
            {
                response.Status = 500;
                response.Message = "Error:" + ex.Message;
                return response;
            }
        }
        [HttpPost("update-prices")]
        public UpdatePriceRequestResponse UpdatePrices([FromBody] UpdatePricesRequest request)
        {
            var response = new UpdatePriceRequestResponse { Message = "", Status = 400 };
            if (!ModelState.IsValid)
            {
                // Generate reference JSON format
                var referenceJson = new Dictionary<int, decimal>
                {
                    { 1, 29.99m },
                    { 2, 49.99m },
                    { 3, 19.99m }
                    // Add more example data as needed
                };
                response.Status = 400;
                response.Message = "Invalid JSON format. Please provide a dictionary of product IDs and prices.";
                response.referenceJSON = referenceJson;
                return response;
            }
            try
            {
                // Validate product IDs
                var invalidProductIds = request.PriceUpdates.Keys
                    .Where(id => !_context.Products.Any(p => p.Id == id))
                    .ToList();

                if (invalidProductIds.Any())
                {
                    response.Status = 400;
                    response.Message = "Products with the following IDs not found: " + string.Join(", ", invalidProductIds) + "";
                    return response;
                }
                foreach (var update in request.PriceUpdates)
                {
                    var product = _context.Products.FirstOrDefault(p => p.Id == update.Key);
                    product.Price = update.Value;
                }

                _context.SaveChanges();
                response.Status = 200;
                response.Message = "Prices updated successfully.";
                return response;
            }
            catch (Exception ex)
            {
                response.Status = 500;
                response.Message = "Error:" + ex.Message;
                return response;
            }
        }

    }
}
