using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Linq.Expressions;
using System.Net;
using WebTMDT.Controllers;
using WebTMDT.UnitTests.MockHepler;
using WebTMDT_API.Data;
using WebTMDT_API.Repository;
using WebTMDT_Client.Controllers;
using WebTMDT_Client.ResponseModel;
using WebTMDT_Client.Service;
using WebTMDTLibrary.DTO;
using Xunit;

namespace WebTMDT.UnitTests.Client.Controller
{
    public class ReviewControllerTest
    {
        [Fact]
        public async void ReviewControllerPostReview()
        {
            //arrange

            var mockReviewService = new Mock<IReviewService>();
            PostReviewDTO dto = new PostReviewDTO();

            //mock httpcontext
            Mock<HttpContext> mockHttpContext = new Mock<HttpContext>();
            MockHttpSession mockSession = new MockHttpSession();
            mockSession["Token"] = "myToken";
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
          
            
            
            PostReviewResponseModel res = new PostReviewResponseModel()
            {
                newReview = true,
                success = true,
                update=false
            };
            mockReviewService.Setup(s => s.GetPostReviewResponse(dto, It.IsAny<string>())).ReturnsAsync(new PostReviewResponseModel());


            var controller = new ReviewController(mockReviewService.Object);
            controller.ControllerContext.HttpContext= mockHttpContext.Object;
            //act

            var result = await controller.PostReview(dto) as RedirectToActionResult;

            //assert

            Assert.NotNull(result);
            Assert.Equal("Index",result.ActionName);
            Assert.Equal("Product", result.ControllerName);
            Console.WriteLine();
        }


    }
}
