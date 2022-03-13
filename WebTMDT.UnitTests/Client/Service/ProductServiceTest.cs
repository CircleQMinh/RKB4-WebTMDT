using Microsoft.Extensions.Configuration;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebTMDT_Client.Service;
using WebTMDT_Client.ViewModel;
using Xunit;

namespace WebTMDT.UnitTests.Client.Service
{
    public class ProductServiceTest
    {

        [Fact]
        public async void ProductServiceGetProductListViewModel_ValidInput_ReturnModel()
        {
            //arrange
            //phải có api chạy 
            var inMemorySettings = new Dictionary<string, string>
            {
                {"Setting:API_URL", "https://localhost:7099/api/"},
                {"Setting:API_ENDPOINT:Product:GetProduct","Product/search" }

            };
            IConfiguration mockConfiguration = new ConfigurationBuilder()
               .AddInMemoryCollection(inMemorySettings)
               .Build();
            ProductListFilterModel model = new ProductListFilterModel()
            {
                pageNumber = 1,
                pageSize = 10
            };
            //act

            ProductService productService = new ProductService(mockConfiguration);
            var result = await productService.GetProductListViewModel(model);
            //assert

            Assert.NotNull(result);
            Assert.InRange(result.result.Count, 0, 10);
            Console.WriteLine();
        }
    }


}
