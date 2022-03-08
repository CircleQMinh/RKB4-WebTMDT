using System.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebTMDT.UnitTests.MockHepler;
using WebTMDT_Client.Controllers;
using WebTMDT_Client.Service;
using WebTMDT_Client.ViewModel;
using Xunit;

namespace WebTMDT.UnitTests.Client.Controller
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
        public async void ProductControllerIndex_ValidId_ReturnProductDetailView()
        {
            //Arrange
            _mockProductService.Setup(s => s.GetProductDetailViewModel(It.IsAny<int>()))
                .ReturnsAsync(new ProductDetailViewModel());
            var id = 1;
            var user_string = "{ " +
                "\"Id\":\"be9382db-e66c-47ac-b69e-a7915223ede2\"," +
                "\"UserName\":\"Quoc Minh\"," +
                "\"PhoneNumber\":\"0788283307\"," +
                "\"Email\":\"quocminh.vutran3105@gmail.com\"," +
                "\"imgUrl\":\"data:image/png\"," +
                "\"Coins\":0," +
                "\"Roles\":[\"User\"]}";
            var token = "randomtoken";
            var reviewPostResult = "0";

            //mock httpcontext
            Mock<HttpContext> mockHttpContext = new Mock<HttpContext>();
            MockHttpSession mockSession = new MockHttpSession();
            mockSession["User"] = user_string;
            mockSession["Token"] = token;
            mockSession["reviewPostResult"] = reviewPostResult;
            mockSession["reviewDeleteResult"] = null;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            _productController.ControllerContext.HttpContext = mockHttpContext.Object;
            //Act


            var result = await _productController.Index(id) as ViewResult; //<-- lấy ViewResult từ mvc nè

            //Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Model);

        }
    }
}
