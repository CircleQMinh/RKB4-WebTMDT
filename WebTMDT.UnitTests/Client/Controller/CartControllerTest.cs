using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebTMDT.UnitTests.MockHepler;
using WebTMDT_Client.Controllers;
using WebTMDTLibrary.DTO;
using Xunit;

namespace WebTMDT.UnitTests.Client.Controller
{
    public class CartControllerTest
    {
        [Fact]
        public void CartControllerIndex_ValidCallHaveCart_ReturnViewWithCart()
        {
            //arrange
            var controller = new CartController();
            //mock httpcontext
            Mock<HttpContext> mockHttpContext = new Mock<HttpContext>();
            MockHttpSession mockSession = new MockHttpSession();

            var cart = "{\"TotalItem\":2,\"TotalPrice\":106000.0,\"Items\":[{\"BookId\":10,\"ImgUrl\":\"https://cdn0.fahasa.com/media/catalog/product/cache/1/small_image/600x600/9df78eab33525d08d6e5fb8d27136e95/8/9/8935095623860_1.jpg\",\"Title\":\"Thuật Hùng Biện\",\"Price\":48000.0,\"Quantity\":1,\"PromotionPercent\":null,\"PromotionAmount\":null},{\"BookId\":4,\"ImgUrl\":\"https://cdn0.fahasa.com/media/catalog/product/cache/1/small_image/600x600/9df78eab33525d08d6e5fb8d27136e95/i/m/image_188285.jpg\",\"Title\":\"Chuyện Con Mèo Dạy Hải Âu Bay\",\"Price\":65000.0,\"Quantity\":1,\"PromotionPercent\":null,\"PromotionAmount\":\"7000\"}]}";
            mockSession["cart"] = cart;

            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            controller.ControllerContext.HttpContext = mockHttpContext.Object;

            //act

            var result = controller.Index() as ViewResult;
            var model = result.Model as Cart;

            //assert

            Assert.NotNull(result);
            Assert.NotNull(model);
            Assert.Equal(2, model.TotalItem);
            Assert.Equal(106000.0, model.TotalPrice);

            Console.WriteLine();
        }

        [Fact]
        public void CartControllerIndex_ValidCallHaveNoCart_ReturnViewWithNoCart()
        {
            //arrange
            var controller = new CartController();
            //mock httpcontext
            Mock<HttpContext> mockHttpContext = new Mock<HttpContext>();
            MockHttpSession mockSession = new MockHttpSession();

            mockSession["cart"] = null;

            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            controller.ControllerContext.HttpContext = mockHttpContext.Object;

            //act

            var result = controller.Index() as ViewResult;
            var model = result.Model as Cart;

            //assert

            Assert.NotNull(result);
            Assert.NotNull(model);
            Assert.Equal(0, model.TotalItem);
            Assert.Equal(0, model.TotalPrice);

            Console.WriteLine();
        }
        [Fact]
        public void CartControllerAddtoCart_ValidCall_ReturnSuccess()
        {
            //arrange
            var controller = new CartController();
            //mock httpcontext
            Mock<HttpContext> mockHttpContext = new Mock<HttpContext>();
            MockHttpSession mockSession = new MockHttpSession();

            var cart = "{\"TotalItem\":2,\"TotalPrice\":106000.0,\"Items\":[{\"BookId\":10,\"ImgUrl\":\"https://cdn0.fahasa.com/media/catalog/product/cache/1/small_image/600x600/9df78eab33525d08d6e5fb8d27136e95/8/9/8935095623860_1.jpg\",\"Title\":\"Thuật Hùng Biện\",\"Price\":48000.0,\"Quantity\":1,\"PromotionPercent\":null,\"PromotionAmount\":null},{\"BookId\":4,\"ImgUrl\":\"https://cdn0.fahasa.com/media/catalog/product/cache/1/small_image/600x600/9df78eab33525d08d6e5fb8d27136e95/i/m/image_188285.jpg\",\"Title\":\"Chuyện Con Mèo Dạy Hải Âu Bay\",\"Price\":65000.0,\"Quantity\":1,\"PromotionPercent\":null,\"PromotionAmount\":\"7000\"}]}";
            mockSession["cart"] = cart;


            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            controller.ControllerContext.HttpContext = mockHttpContext.Object;

            CartItem cartItem = new CartItem() {
                BookId = 100,
                ImgUrl = "",
                Price = 10000,
                PromotionAmount = null,
                PromotionPercent = null,
                Quantity = 1,
                Title = "Abcd" };

            //act
            var result = controller.AddToCart(cartItem) as AcceptedResult;
            var value = result.Value ;
            var success = value.GetType().GetProperty("success").GetValue(value, null);
            var cart_result = value.GetType().GetProperty("cart").GetValue(value, null) as Cart;
            //assert

            Assert.NotNull(result);
            Assert.NotNull(success);
            Assert.NotNull(cart_result);
            Assert.Equal(true, success);
            Assert.Equal(3, cart_result.TotalItem);
            Assert.Equal(116000.0,cart_result.TotalPrice);

            Console.WriteLine();
        }

    }

}
