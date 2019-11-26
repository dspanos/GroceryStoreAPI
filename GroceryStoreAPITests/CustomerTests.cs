using GroceryStoreAPI.Controllers;
using GroceryStoreAPI.Models;
using GroceryStoreAPI.Repo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace GroceryStoreAPITests
{
    [TestCategory("Customers")]
    [TestClass]
    public class CustomerTests
    {
        private CustomersController _controller;
        private GenericRepository<Customer> _repo;
        private GenericRepository<Order> _ordersRepo;

        public CustomerTests()
        {
            _repo = new GenericRepository<Customer>();
            _ordersRepo = new GenericRepository<Order>();
            _controller = new CustomersController();
        }

        [TestMethod]
        public void Get_ShouldReturnAllCustomers()
        {
            // Arrange
            var testCustomers = _repo.GetData(JSONData.customers).ToList();
            var controller = new CustomersController();

            // Act
            var result = controller.Get().Value as IEnumerable<Customer>;

            // Assert
            Assert.AreEqual(testCustomers.Count, result.Count());
        }

        [TestMethod]
        public void Get_ShouldReturnCorrectCustomer()
        {
            // Arrange
            var customerId = 3;
            var testCustomers = _repo.GetData(JSONData.customers).FirstOrDefault(c => c.Id == customerId);
            var controller = new CustomersController();

            // Act
            var result = controller.Get(3).Value as Customer;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(testCustomers.Id, result.Id);
        }

        [TestMethod]
        public void Get_ShouldReturnCustomerOrder()
        {
            // Arrange
            var customerId = 1;
            var testCustomerOrder = _ordersRepo.GetData(JSONData.orders).Where(o => o.CustomerId == customerId).FirstOrDefault();
            var controller = new CustomersController();

            // Act
            var result = controller.Get(1).Value as Customer;

            //Assert
            Assert.AreEqual(testCustomerOrder.CustomerId, result.Id);
        }

        [TestMethod]
        public void Post_CreateNewCustomerReturnsOK()
        {
            // Arrange
            var controller = new CustomersController();

            // Act
            var result = controller.Post(new Customer { Id = 10, Name = "Customer10" });

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(OkResult));
        }
    }
}
