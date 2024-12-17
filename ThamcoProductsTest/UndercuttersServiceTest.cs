using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using ThamcoProducts.Services.Undercutters;

using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;
using Moq.Protected;

namespace ThamcoProductsTests
{
    [TestClass]
    public class UndercuttersServiceTests
    {
        private Mock<IConfiguration> _mockConfiguration;
        private Mock<HttpMessageHandler> _mockHttpMessageHandler;
        private HttpClient _mockHttpClient;
        private UndercuttersService _undercuttersService;

        [TestInitialize]
        public void Setup()
        {
            // Mock IConfiguration
            _mockConfiguration = new Mock<IConfiguration>();
            _mockConfiguration.Setup(config => config["WebServices:Undercutters:BaseURL"]).Returns("http://fakeapi.com");

            // Mock HttpMessageHandler
            _mockHttpMessageHandler = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            _mockHttpMessageHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<System.Threading.CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new List<ProductDto>
                    {
                        new ProductDto { Id = 1, Name = "Product 1", Price = 100 },
                        new ProductDto { Id = 2, Name = "Product 2", Price = 200 }
                    }))
                    {
                        // Set the content type correctly
                        Headers = { ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json") }
                    }
                });

            // Create HttpClient using the mock handler
            _mockHttpClient = new HttpClient(_mockHttpMessageHandler.Object);

            // Create UndercuttersService with the mocked HttpClient and IConfiguration
            _undercuttersService = new UndercuttersService(_mockHttpClient, _mockConfiguration.Object);
        }

        [TestMethod]
        public async Task GetProductsAsync_ReturnsProductList_WhenResponseIsSuccessful()
        {
            // Act
            var products = await _undercuttersService.GetProductsAsync();

            // Assert
            Assert.IsNotNull(products);
            Assert.AreEqual(2, products.Count());
            Assert.AreEqual("Product 1", products.First().Name);
            Assert.AreEqual(100, products.First().Price);
        }

        [TestMethod]
        public async Task GetProductsAsync_ReturnsEmptyList_WhenExceptionOccurs()
        {
            // Arrange: Make the mock HttpMessageHandler throw an exception
            _mockHttpMessageHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<System.Threading.CancellationToken>())
                .ThrowsAsync(new System.Exception("Service failure"));

            // Act
            var products = await _undercuttersService.GetProductsAsync();

            // Assert
            Assert.IsNotNull(products);
            Assert.AreEqual(0, products.Count()); // Ensure it returns an empty list when an exception occurs
        }

        [TestMethod]
        public async Task GetProductsAsync_ReturnsEmptyList_WhenResponseIsNotSuccessful()
        {
            // Arrange: Simulate an unsuccessful HTTP response (non-2xx status code)
            _mockHttpMessageHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<System.Threading.CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent("Internal Server Error")
                });

            // Act
            var products = await _undercuttersService.GetProductsAsync();

            // Assert
            Assert.IsNotNull(products);
            Assert.AreEqual(0, products.Count()); // Ensure it returns an empty list when the status code is not successful
        }
    }
}
