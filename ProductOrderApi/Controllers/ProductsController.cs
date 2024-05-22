﻿using Microsoft.AspNetCore.Mvc;
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
            throw new NotImplementedException();
        }
        [HttpPost]
        public async Task<ActionResult<Product>> AddProduct(Product product)
        {
            return await _productService.AddProduct(product);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, Product product)
        {
            throw new NotImplementedException();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            throw new NotImplementedException();
        }
    }
}
