using Microsoft.AspNetCore.Mvc;
using Moq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ThamcoProducts.Controllers;
using ThamcoProducts.Services.ProductRespository;
using ThamcoProducts.Services.Undercutters;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThamcoProductsTests
{
    [TestClass]
    public class ProductControllerTests
    {
        private Mock<ILogger<ProductController>> _mockLogger;
        private Mock<IUndercuttersService> _mockUndercuttersService;
        private Mock<IProductService> _mockProductService;
        private ProductController _controller;

        [TestInitialize]
        public void Setup()
        {
            _mockLogger = new Mock<ILogger<ProductController>>();
            _mockUndercuttersService = new Mock<IUndercuttersService>();
            _mockProductService = new Mock<IProductService>();

            _controller = new ProductController(_mockLogger.Object, _mockUndercuttersService.Object, _mockProductService.Object);
        }

        [TestMethod]
        public async Task UnderCutters_ReturnsOkResult_WithProducts()
        {
            // Arrange
            var products = new List<ProductDto>
            {
                new ProductDto { Id = 1, Name = "Product 1", Price = 100 },
                new ProductDto { Id = 2, Name = "Product 2", Price = 200 }
            };

            _mockUndercuttersService.Setup(service => service.GetProductsAsync()).ReturnsAsync(products);

            // Act
            var result = await _controller.UnderCutters();

            // Assert
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            var returnValue = okResult.Value as List<ProductDto>;
            Assert.IsNotNull(returnValue);
            Assert.AreEqual(2, returnValue.Count);
            Assert.AreEqual("Product 1", returnValue.First().Name);
        }

        [TestMethod]
        public async Task UnderCutters_ReturnsEmptyList_WhenExceptionOccurs()
        {
            // Arrange
            _mockUndercuttersService.Setup(service => service.GetProductsAsync()).ThrowsAsync(new System.Exception("Service failure"));

            // Act
            var result = await _controller.UnderCutters();

            // Assert
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            var returnValue = okResult.Value as List<ProductDto>;
            Assert.IsNotNull(returnValue);
            Assert.AreEqual(0, returnValue.Count); // Ensure an empty list is returned
        }

        [TestMethod]
        public async Task FakeProducts_ReturnsOkResult_WithProducts()
        {
            // Arrange
            var products = new List<Product>
            {
                new Product { Id = 1, Name = "Fake Product 1", Price = 100 },
                new Product { Id = 2, Name = "Fake Product 2", Price = 200 }
            };

            _mockProductService.Setup(service => service.GetProductsAsync()).ReturnsAsync(products);

            // Act
            var result = await _controller.FakeProducts();

            // Assert
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            var returnValue = okResult.Value as List<Product>;
            Assert.IsNotNull(returnValue);
            Assert.AreEqual(2, returnValue.Count);
            Assert.AreEqual("Fake Product 1", returnValue.First().Name);
        }

        [TestMethod]
        public async Task FakeProducts_ReturnsEmptyList_WhenExceptionOccurs()
        {
            // Arrange
            _mockProductService.Setup(service => service.GetProductsAsync()).ThrowsAsync(new System.Exception("Service failure"));

            // Act
            var result = await _controller.FakeProducts();

            // Assert
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            var returnValue = okResult.Value as List<Product>;
            Assert.IsNotNull(returnValue);
            Assert.AreEqual(0, returnValue.Count); // Ensure an empty list is returned
        }
    }
}
