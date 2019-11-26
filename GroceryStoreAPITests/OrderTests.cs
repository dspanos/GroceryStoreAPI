using GroceryStoreAPI.Controllers;
using GroceryStoreAPI.Models;
using GroceryStoreAPI.Repo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GroceryStoreAPITests
{
    [TestCategory("Orders")]
    [TestClass]
    public class OrderTests
    {
        private OrdersController _controller;
        private GenericRepository<Order> _repo;

        public OrderTests()
        {
            _repo = new GenericRepository<Order>();
            _controller = new OrdersController();
        }

        [TestMethod]
        public void Get_ShouldReturnAllOrders()
        {
            // Arrange
            var testOrders = _repo.GetData(JSONData.orders).ToList();
            var controller = new OrdersController();

            // Act
            var result = controller.Get().Value as IEnumerable<Order>;

            // Assert
            Assert.AreEqual(testOrders.Count, result.Count());
        }

        [TestMethod]
        public void Get_ShouldReturnOrderByDate()
        {
            // Arrange
            var dateCreated = new DateTime(2019, 7, 4);
            var orders = _repo.GetData(JSONData.orders);
            var filteredOrder = orders.Where(d => d.OrderDate.Date.ToShortDateString() == dateCreated.Date.ToShortDateString()).FirstOrDefault();
            var controller = new OrdersController();

            // Act
            var result = controller.Get(3).Value;

            // Assert
            Assert.AreEqual(filteredOrder.Id, result.Id);

        }

        [TestMethod]
        public void Get_ShouldReturnCorrectOrder()
        {
            // Arrange
            var orderId = 3;
            var testOrders = _repo.GetData(JSONData.orders).FirstOrDefault(o => o.Id == orderId);
            var controller = new OrdersController();

            // Act
            var result = controller.Get(3).Value as Order;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(testOrders.Id, result.Id);
        }
    }
}
