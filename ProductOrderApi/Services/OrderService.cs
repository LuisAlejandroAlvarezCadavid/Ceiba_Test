using ProductOrderApi.Data.Entities;
using ProductOrderApi.Data.Models;
using ProductOrderApi.Data.Repositories;

namespace ProductOrderApi.Services
{
    public class OrderService
    {
        private readonly OrderRepository _orderRepository;
        private readonly ProductRepository _productRepository;

        public OrderService(OrderRepository orderRepository, ProductRepository productRepository)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
        }
        public async Task<IEnumerable<Order>> GetOrders()
        {
            return await _orderRepository.GetOrdersAsync();
        }
        public async Task<Order> GetOrder(int id)
        {
            return await _orderRepository.GetOrderAsync(id);
        }
        public async Task<Order> CreateOrder(CreateOrderModel order)
        {
            var products = await _productRepository.GetProducts();
            var toalPrice = order.OrderProducts.Join(products, productOrder => productOrder.ProductId, product => product.Id, (productOrder, product) => new { productOrder.Quantity, product.Price }).Sum(relation => relation.Quantity * relation.Price);
            var id = (await _orderRepository.GetOrdersAsync()).Max(order => order.Id) + 1;
            Order newOrder = new Order
            {
                Id = id,
                OrderDate = DateTime.Now,
                TotalPrice = toalPrice

            };
            return await _orderRepository.CreateOrderAsync(newOrder);
        }
        public async Task<Order> UpdateOrder(Order order)
        {
            return await _orderRepository.UpdateOrderAsync(order);
        }
        public async Task DeleteOrder(int id)
        {
            await _orderRepository.DeleteOrderAsync(id);
        }
    }
}
