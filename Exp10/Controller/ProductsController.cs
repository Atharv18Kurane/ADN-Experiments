using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Exp10.Models;

namespace Exp10.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ILogger<ProductsController> _logger;
        private readonly IMemoryCache _cache;

        public ProductsController(ILogger<ProductsController> logger, IMemoryCache cache)
        {
            _logger = logger;
            _cache = cache;
        }

        private static List<Product> products = new List<Product>
        {
            new Product { Id = 1, Name = "Laptop", Price = 50000 },
            new Product { Id = 2, Name = "Mobile", Price = 20000 }
        };

        [HttpGet]
        public IActionResult GetProducts()
        {
            _logger.LogInformation("Fetching products...");

            string cacheKey = "productList";

            if (!_cache.TryGetValue(cacheKey, out List<Product> cachedProducts))
            {
                _logger.LogInformation("Cache MISS");

                cachedProducts = products;

                var options = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromSeconds(30));

                _cache.Set(cacheKey, cachedProducts, options);
            }
            else
            {
                _logger.LogInformation("Cache HIT");
            }

            return Ok(cachedProducts);
        }

        [HttpPost]
        public IActionResult AddProduct(Product product)
        {
            _logger.LogInformation("Adding product");

            product.Id = products.Count + 1;
            products.Add(product);

            _cache.Remove("productList");

            return Ok(product);
        }
    }
}