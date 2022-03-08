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
    public class AccountControllerTest
    {
        private readonly AccountController accountController;
        private readonly Mock<IAccountService> accountServiceMock;
        public AccountControllerTest()
        {
            accountServiceMock = new Mock<IAccountService>();
            accountController = new AccountController(accountServiceMock.Object);
        }


        [Fact]
        public void AccountControllerLoginGET_ValidCall_ReturnLoginView()
        {
            //arrange
            var redirectUrl = "abcd";

            //act

            var result = accountController.Login(redirectUrl) as ViewResult;

            //assert

            Assert.NotNull(result);
            Assert.Equal(redirectUrl, result.ViewData["RedirectUrl"]);
            Console.WriteLine();
        }


        [Fact]
        public async void AccountControllerLoginPOST_ValidCall_ReturnRedirect()
        {
            //arrange
            var redirectUrl = "/abcd";
            var loginUserDTO = new LoginUserDTO() { Email = "email@gmail.com", Password = "Password" };

            var token = "token";
            var user = new SimpleUserDTO();

            accountServiceMock.Setup(x => x.Login(loginUserDTO)).ReturnsAsync(new LoginResponseModel() 
            { success=true,token=token,user=user});
            //mock httpcontext
            Mock<HttpContext> mockHttpContext = new Mock<HttpContext>();
            MockHttpSession mockSession = new MockHttpSession();

            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            accountController.ControllerContext.HttpContext = mockHttpContext.Object;
            //act

            var result = await accountController.Login(loginUserDTO, redirectUrl) as RedirectResult;

            //assert

            Assert.NotNull(result);
            Assert.Equal("https://localhost:7094/abcd", result.Url);
    
            Console.WriteLine();
        }

    }
}
