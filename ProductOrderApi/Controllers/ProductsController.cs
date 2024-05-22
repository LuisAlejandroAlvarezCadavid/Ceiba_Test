using Microsoft.AspNetCore.Mvc;
using ProductOrderApi.Data.Entities;
using ProductOrderApi.Services;

namespace ProductOrderApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ProductService _productService;
        public ProductsController(ProductService productService)
        {
            _productService = productService;
        }
        [HttpGet]
        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _productService.GetProducts();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _productService.GetProduct(id);
            return product == null ? NotFound() : Ok(product);
        }
        [HttpPost]
        public async Task<ActionResult<Product>> AddProduct(Product product)
        {
            return await _productService.AddProduct(product);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, Product product)
        {
            var updateProduct = await _productService.UpdateProduct(product);
            return updateProduct == null ? NotFound() : NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            await _productService.DeleteProduct(id);
            return NoContent();
        }
    }
}
