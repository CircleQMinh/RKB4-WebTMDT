using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Moq;
using WebTMDT.UnitTests.MockHepler;
using Xunit;

namespace WebTMDT.UnitTests.Client.ViewComponent
{
    public class CartTest
    {
        [Fact]
        public void CartInvoke_ValidCall_ReturnView()
        {
            //arrange
            var cart = "{\"TotalItem\":2,\"TotalPrice\":106000.0,\"Items\":[{\"BookId\":10,\"ImgUrl\":" +
           "\"https://cdn0.fahasa.com/media/catalog/product/cache/1/small_image/600x600/9df78eab33525d08d6e5fb8d27136e" +
           "95/8/9/8935095623860_1.jpg\",\"Title\":\"Thuật Hùng Biện\",\"Price\":48000.0,\"Quantity\":1," +
           "\"PromotionPercent\":null,\"PromotionAmount\":null},{\"BookId\":4,\"ImgUrl\":" +
           "\"https://cdn0.fahasa.com/media/catalog/product/cache/1/small_image/600x600/9df78eab33525d08d6e" +
           "5fb8d27136e95/i/m/image_188285.jpg\",\"Title\":\"Chuyện Con Mèo Dạy Hải Âu Bay\",\"Price" +
           "\":65000.0,\"Quantity\":1,\"PromotionPercent\":null,\"PromotionAmount\":\"7000\"}]}";

        
            //mock httpcontext
            Mock<HttpContext> mockHttpContext = new Mock<HttpContext>();
            MockHttpSession mockSession = new MockHttpSession();
            mockSession["cart"] = cart;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);

            

            //mock viewcontext
            var viewContext = new ViewContext();
            viewContext.HttpContext = mockHttpContext.Object;
            var viewComponentContext = new ViewComponentContext
            {
                ViewContext = viewContext
            };

            var viewComponent = new WebTMDT_Client.Views.Shared.Components.Cart.Cart();
            viewComponent.ViewComponentContext = viewComponentContext;
            //act

            var result = viewComponent.Invoke() as ViewViewComponentResult;
            var model = result.ViewData.Model as WebTMDTLibrary.DTO.Cart;

            //assert

            Assert.NotNull(result);
            Assert.NotNull(model);
            Assert.Equal(2,model.TotalItem);
            Assert.Equal(106000.0, model.TotalPrice);

            Console.WriteLine();
        }
    }
}
