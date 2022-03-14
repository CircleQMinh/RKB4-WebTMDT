using Microsoft.Extensions.Configuration;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebTMDT_Client.ResponseModel;
using WebTMDT_Client.Service;
using WebTMDT_Client.ViewModel;
using WebTMDTLibrary.DTO;
using Xunit;

namespace WebTMDT.UnitTests.Client.Service
{
    public class AccountServiceTest
    {
        [Fact]
        public async void AccountServiceLogin_ValidInput_ReturnLoginResponseModel()
        {
            //arrange
            //phải có api chạy 
            var inMemorySettings = new Dictionary<string, string>
            {
                {"Setting:API_URL", "https://localhost:7099/api/"},
                {"Setting:API_ENDPOINT:Account:Login","Account/login" }

            };
            IConfiguration mockConfiguration = new ConfigurationBuilder()
               .AddInMemoryCollection(inMemorySettings)
               .Build();

            var service = new AccountService(mockConfiguration);
            var model = new LoginUserDTO() { Email = "quocminh.vutran3105@gmail.com", Password = "p@ssworD3" };

            //act

            var result = await service.Login(model) as LoginResponseModel;
            //assert
            Assert.NotNull(result);
            Assert.True(result.success);
            Assert.NotNull(result.token);
            Assert.NotNull(result.user);
            Assert.Equal("Quốc Minh", result.user.UserName);

            Console.WriteLine();
        }
    }
}
