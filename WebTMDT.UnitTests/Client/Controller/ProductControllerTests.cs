using Microsoft.AspNetCore.Mvc;
using Moq;
using WebTMDT_Client.Controllers;
using WebTMDT_Client.Service;
using WebTMDT_Client.ViewModel;
using Xunit;

namespace WebTMDT.UnitTests
{
    public class ProductControllerTests
    {
        private readonly Mock<IProductService> _mockProductService;
        private readonly ProductController _productController;

        public ProductControllerTests()
        {
            _mockProductService = new Mock<IProductService>();
            _productController = new ProductController(default, _mockProductService.Object);
        }

        [Fact]
        public void ProductControllerIndex_ValidId_ReturnProduct()
        {
            // Arrange
            _mockProductService.Setup(s => s.GetProductDetailViewModel(It.IsAny<int>()))
                .Returns(new ProductDetailViewModel());
            var id = 1;

            // Act
            var result = (ViewResult)_productController.Index(id);

            // Assert
            Assert.NotNull(result.Model);
        }
    }
}
