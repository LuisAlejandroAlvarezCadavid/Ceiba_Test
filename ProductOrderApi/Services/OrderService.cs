﻿using ProductOrderApi.Data.Entities;
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
        public async Task<Order?> GetOrder(int id)
        {
            return await _orderRepository.GetOrderAsync(id);
        }
        public async Task<Order> CreateOrder(CreateOrderModel order)
        {
            var products = await _productRepository.GetProducts();
            var orderedProducts = order.OrderProducts.Join(products, productOrder => productOrder.ProductId, product => product.Id, (productOrder, product) => new { productOrder.Quantity, product.Price, productOrder.ProductId });
            var totalPrice = orderedProducts.Sum(relation => relation.Quantity * relation.Price);
            var orders = await _orderRepository.GetOrdersAsync();
            var id = orders.Any() ? orders.Max(order => order.Id) + 1 : 1;
            var orderId = orders.Any() ? orders.SelectMany(order => order.OrderProducts!).Max(order => order.Id) + 1 : 1;
            var orderProduct = orderedProducts.Select(product => new OrderProduct { Id = orderId++, OrderId = id, Price = product.Price, ProductId = product.ProductId, Quantity = product.Quantity }).ToList();
            Order newOrder = new()
            {
                Id = id,
                OrderDate = DateTime.Now,
                TotalPrice = totalPrice,
                OrderProducts = orderProduct
            };
            return await _orderRepository.CreateOrderAsync(newOrder);
        }
        public async Task<Order?> UpdateOrder(Order order)
        {
            return await _orderRepository.UpdateOrderAsync(order);
        }
        public async Task<bool> DeleteOrder(int id)
        {
            return await _orderRepository.DeleteOrderAsync(id);
        }
    }
}
