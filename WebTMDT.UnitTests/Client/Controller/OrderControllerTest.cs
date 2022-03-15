using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebTMDT.UnitTests.MockHepler;
using WebTMDT_Client.Controllers;
using WebTMDT_Client.ResponseModel;
using WebTMDT_Client.Service;
using WebTMDTLibrary.DTO;
using Xunit;

namespace WebTMDT.UnitTests.Client.Controller
{
    public class OrderControllerTest
    {
        private readonly Mock<IOrderService> _mockOrderService;
        private readonly OrderController _orderController;

        public OrderControllerTest()
        {
            _mockOrderService = new Mock<IOrderService>();
            _orderController = new OrderController(_mockOrderService.Object);

        }

        [Fact]
        public void OrderControllerCheckoutGET_ValidCallEmptyCart_ReturnCartView()
        {
            //arrange
            var payUrl = "this_is_pay_url";
            _mockOrderService.Setup(s => s.GetVNPayUrl(It.IsAny<double>(), It.IsAny<string>())).ReturnsAsync(payUrl);


            var user_string = "{ " +
               "\"Id\":\"be9382db-e66c-47ac-b69e-a7915223ede2\"," +
               "\"UserName\":\"Quoc Minh\"," +
               "\"PhoneNumber\":\"0788283307\"," +
               "\"Email\":\"quocminh.vutran3105@gmail.com\"," +
               "\"imgUrl\":\"data:image/png\"," +
               "\"Coins\":0," +
               "\"Roles\":[\"User\"]}";
            var token = "randomtoken";
            //mock httpcontext
            Mock<HttpContext> mockHttpContext = new Mock<HttpContext>();
            MockHttpSession mockSession = new MockHttpSession();
            mockSession["User"] = user_string;
            mockSession["Token"] = token;
            mockSession["cart"] = null;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            _orderController.ControllerContext.HttpContext = mockHttpContext.Object;

            //act

            var result = _orderController.Checkout() as RedirectToActionResult;

            //assert
            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName);
            Assert.Equal("Cart", result.ControllerName);


        }


        [Fact]
        public void OrderControllerCheckoutGET_ValidCallHaveCart_ReturnCheckoutView()
        {
            //arrange
            var payUrl = "this_is_pay_url";
            _mockOrderService.Setup(s => s.GetVNPayUrl(It.IsAny<double>(), It.IsAny<string>())).ReturnsAsync(payUrl);


            var user_string = "{ " +
               "\"Id\":\"be9382db-e66c-47ac-b69e-a7915223ede2\"," +
               "\"UserName\":\"Quoc Minh\"," +
               "\"PhoneNumber\":\"0788283307\"," +
               "\"Email\":\"quocminh.vutran3105@gmail.com\"," +
               "\"imgUrl\":\"data:image/png\"," +
               "\"Coins\":0," +
               "\"Roles\":[\"User\"]}";
            var cart = "{\"TotalItem\":2,\"TotalPrice\":106000.0,\"Items\":[{\"BookId\":10,\"ImgUrl\":" +
                "\"https://cdn0.fahasa.com/media/catalog/product/cache/1/small_image/600x600/9df78eab33525d08d6e5fb8d27136e" +
                "95/8/9/8935095623860_1.jpg\",\"Title\":\"Thuật Hùng Biện\",\"Price\":48000.0,\"Quantity\":1," +
                "\"PromotionPercent\":null,\"PromotionAmount\":null},{\"BookId\":4,\"ImgUrl\":" +
                "\"https://cdn0.fahasa.com/media/catalog/product/cache/1/small_image/600x600/9df78eab33525d08d6e" +
                "5fb8d27136e95/i/m/image_188285.jpg\",\"Title\":\"Chuyện Con Mèo Dạy Hải Âu Bay\",\"Price" +
                "\":65000.0,\"Quantity\":1,\"PromotionPercent\":null,\"PromotionAmount\":\"7000\"}]}";

            var token = "randomtoken";
            //mock httpcontext
            Mock<HttpContext> mockHttpContext = new Mock<HttpContext>();
            MockHttpSession mockSession = new MockHttpSession();
            mockSession["User"] = user_string;
            mockSession["Token"] = token;
            mockSession["cart"] = cart;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            _orderController.ControllerContext.HttpContext = mockHttpContext.Object;

            //act

            var result = _orderController.Checkout() as ViewResult;

            //assert
            Assert.NotNull(result);
            Assert.NotNull(result.Model);
        }

        [Fact]
        public async void OrderControllerCheckoutPOST_ValidCallHaveCartVNPay_ReturnVNPAYSite()
        {

            //arrange
            var payUrl = "this_is_pay_url";
            _mockOrderService.Setup(s => s.GetVNPayUrl(It.IsAny<double>(), It.IsAny<string>())).ReturnsAsync(payUrl);

            var user_string = "{ " +
            "\"Id\":\"be9382db-e66c-47ac-b69e-a7915223ede2\"," +
            "\"UserName\":\"Quoc Minh\"," +
            "\"PhoneNumber\":\"0788283307\"," +
            "\"Email\":\"quocminh.vutran3105@gmail.com\"," +
            "\"imgUrl\":\"data:image/png\"," +
            "\"Coins\":0," +
            "\"Roles\":[\"User\"]}";
            var cart = "{\"TotalItem\":2,\"TotalPrice\":106000.0,\"Items\":[{\"BookId\":10,\"ImgUrl\":" +
                "\"https://cdn0.fahasa.com/media/catalog/product/cache/1/small_image/600x600/9df78eab33525d08d6e5fb8d27136e" +
                "95/8/9/8935095623860_1.jpg\",\"Title\":\"Thuật Hùng Biện\",\"Price\":48000.0,\"Quantity\":1," +
                "\"PromotionPercent\":null,\"PromotionAmount\":null},{\"BookId\":4,\"ImgUrl\":" +
                "\"https://cdn0.fahasa.com/media/catalog/product/cache/1/small_image/600x600/9df78eab33525d08d6e" +
                "5fb8d27136e95/i/m/image_188285.jpg\",\"Title\":\"Chuyện Con Mèo Dạy Hải Âu Bay\",\"Price" +
                "\":65000.0,\"Quantity\":1,\"PromotionPercent\":null,\"PromotionAmount\":\"7000\"}]}";

            var token = "randomtoken";
            //mock httpcontext
            Mock<HttpContext> mockHttpContext = new Mock<HttpContext>();
            MockHttpSession mockSession = new MockHttpSession();
            mockSession["User"] = user_string;
            mockSession["Token"] = token;
            mockSession["cart"] = cart;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            _orderController.ControllerContext.HttpContext = mockHttpContext.Object;

            var postOrderDTO = new PostOrderDTO
            {
                AddressNo = "38",
                City = "HCM",
                ContactName = "Minh",
                District = "Phường 9",
                Email = "email@gmail.com",
                Note = "note",
                PaymentMethod = "vnpay",
                Phone = "0788283307",
                Street = "Chiến Thắng",
                Ward = "Phú Nhuận"
            };

            //act

            var result = await _orderController.Checkout(postOrderDTO) as RedirectResult;

            //assert

            Assert.NotNull(result);
            Assert.Equal(payUrl, result.Url);

        }


        [Fact]
        public async void OrderControllerCheckoutPOST_ValidCallHaveCartCash_ReturnVNPAYSite()
        {

            //arrange
            var payUrl = "this_is_pay_url";
            _mockOrderService.Setup(s => s.GetVNPayUrl(It.IsAny<double>(), It.IsAny<string>())).ReturnsAsync(payUrl);
            _mockOrderService.Setup(s => s.GetPostOrderResponse(
                It.IsAny<PostOrderDTO>(),
                It.IsAny<Cart>(),
                It.IsAny<string>(),
                It.IsAny<string>()))
                .ReturnsAsync(new PostOrderResponseModel() { success = true });
            var user_string = "{ " +
            "\"Id\":\"be9382db-e66c-47ac-b69e-a7915223ede2\"," +
            "\"UserName\":\"Quoc Minh\"," +
            "\"PhoneNumber\":\"0788283307\"," +
            "\"Email\":\"quocminh.vutran3105@gmail.com\"," +
            "\"imgUrl\":\"data:image/png\"," +
            "\"Coins\":0," +
            "\"Roles\":[\"User\"]}";
            var cart = "{\"TotalItem\":2,\"TotalPrice\":106000.0,\"Items\":[{\"BookId\":10,\"ImgUrl\":" +
                "\"https://cdn0.fahasa.com/media/catalog/product/cache/1/small_image/600x600/9df78eab33525d08d6e5fb8d27136e" +
                "95/8/9/8935095623860_1.jpg\",\"Title\":\"Thuật Hùng Biện\",\"Price\":48000.0,\"Quantity\":1," +
                "\"PromotionPercent\":null,\"PromotionAmount\":null},{\"BookId\":4,\"ImgUrl\":" +
                "\"https://cdn0.fahasa.com/media/catalog/product/cache/1/small_image/600x600/9df78eab33525d08d6e" +
                "5fb8d27136e95/i/m/image_188285.jpg\",\"Title\":\"Chuyện Con Mèo Dạy Hải Âu Bay\",\"Price" +
                "\":65000.0,\"Quantity\":1,\"PromotionPercent\":null,\"PromotionAmount\":\"7000\"}]}";

            var token = "randomtoken";
            //mock httpcontext
            Mock<HttpContext> mockHttpContext = new Mock<HttpContext>();
            MockHttpSession mockSession = new MockHttpSession();
            mockSession["User"] = user_string;
            mockSession["Token"] = token;
            mockSession["cart"] = cart;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            _orderController.ControllerContext.HttpContext = mockHttpContext.Object;

            var postOrderDTO = new PostOrderDTO
            {
                AddressNo = "38",
                City = "HCM",
                ContactName = "Minh",
                District = "Phường 9",
                Email = "email@gmail.com",
                Note = "note",
                PaymentMethod = "cash",
                Phone = "0788283307",
                Street = "Chiến Thắng",
                Ward = "Phú Nhuận"
            };

            //act

            var result = await _orderController.Checkout(postOrderDTO) as RedirectToActionResult;


            //assert

            Assert.NotNull(result);
            Assert.Equal("Order", result.ControllerName);
            Assert.Equal("Thankyou", result.ActionName);
            Console.WriteLine();
        }
    }
}
