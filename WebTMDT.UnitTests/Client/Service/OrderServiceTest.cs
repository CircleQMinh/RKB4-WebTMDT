using AutoMapper;
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
    public class OrderServiceTest
    {

        
        [Fact]
        public async void OrderServiceGetVNPayUrl_ValidInput_ReturnUrl()
        {
            //arrange
            var inMemorySettings = new Dictionary<string, string>
            {
                {"Setting:API_URL", "https://localhost:7099/api/"},
                {"Setting:API_ENDPOINT:Order:GetVNPayUrl","Order/getVNPayUrl" }

            };
            IConfiguration mockConfiguration = new ConfigurationBuilder()
               .AddInMemoryCollection(inMemorySettings)
               .Build();

            var mockMapper = new Mock<IMapper>();

            var service = new OrderService(mockMapper.Object,mockConfiguration);
            var totalPrice = 100000;
            var payUrl = "https://sandbox.vnpayment.vn/paymentv2/vpcpay.html";
            var token = "ValidToken";
            //act

            var result = await service.GetVNPayUrl(totalPrice,token);
            var url = result.Substring(0,50);
            //assert

            Assert.NotNull(result);
            Assert.Equal(payUrl, url);

            Console.WriteLine();

        }
    }
}
