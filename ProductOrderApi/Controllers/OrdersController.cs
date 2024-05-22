using Microsoft.AspNetCore.Mvc;
using ProductOrderApi.Data.Entities;
using ProductOrderApi.Data.Models;
using ProductOrderApi.Services;

namespace ProductOrderApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly OrderService _orderService;
        public OrdersController(OrderService orderService)
        {
            _orderService = orderService;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
        {
            return Ok(await _orderService.GetOrders());
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrder(int id)
        {
            var order = await _orderService.GetOrder(id);
            return order == null ? NotFound() : Ok(order);
        }
        [HttpPost]
        public async Task<ActionResult<Order>> AddOrder(CreateOrderModel model)
        {
            return Ok(await _orderService.CreateOrder(model));
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrder(int id, Order order)
        {
            var updateOrder = await _orderService.UpdateOrder(order);
            return updateOrder == null ? BadRequest() : NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            return (await _orderService.DeleteOrder(id)) ? NoContent() : NotFound();

        }
    }
}
