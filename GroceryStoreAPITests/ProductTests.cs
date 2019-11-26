using GroceryStoreAPI.Controllers;
using GroceryStoreAPI.Models;
using GroceryStoreAPI.Repo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace GroceryStoreAPITests
{
    [TestCategory("Products")]
    [TestClass]
    public class ProductTests
    {
        private ProductsController _controller;
        private GenericRepository<Product> _repo;
        public ProductTests()
        {
            _repo = new GenericRepository<Product>();
            _controller = new ProductsController();
        }

        [TestMethod]
        public void Get_ShouldReturnAllProducts()
        {
            // Arrange
            var testProducts = _repo.GetData(JSONData.products).ToList();
            var controller = new ProductsController();

            // Act
            var result = controller.Get().Value as IEnumerable<Product>;

            // Assert
            Assert.AreEqual(testProducts.Count, result.Count());
        }

        [TestMethod]
        public void Get_ShouldReturnCorrectProduct()
        {
            // Arrange
            var productId = 2;
            var testProducts = _repo.GetData(JSONData.products).FirstOrDefault(c => c.Id == productId);
            var controller = new ProductsController();

            // Act
            var result = controller.Get(2).Value as Product;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(testProducts.Id, result.Id);
        }

        [TestMethod]
        public void Post_CreateNewProductReturnsOK()
        {
            // Arrange
            var controller = new ProductsController();

            // Act
            var result = controller.Post(new Product { Id = 100, Description="Test Description", Price=3.45M});

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(OkResult));
        }
    }
}
