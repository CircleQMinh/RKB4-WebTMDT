using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebTMDT_Client.Controllers;
using Xunit;

namespace WebTMDT.UnitTests.Client.Controller
{
    public class HomeControllerTest
    {
        [Fact]
        public void HomeControllerIndex_ValidCall_ReturnView()
        {
            //arrange
            var mockILogger = new Mock<ILogger<HomeController>>();
            var controller = new HomeController(mockILogger.Object);
            //act
            var result = controller.Index();
            //assert
            Assert.NotNull(result);
            Console.WriteLine();
        }
    }
}
