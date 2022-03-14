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
    public class GenreServiceTest
    {
        [Fact]
        public async void GenreServiceGetGenre_ValidInput_ReturnGenreInfoDTO()
        {
            //arrange
            //phải có api chạy 
            var inMemorySettings = new Dictionary<string, string>
            {
                {"Setting:API_URL", "https://localhost:7099/api/"},
                {"Setting:API_ENDPOINT:Genre:GetGenre","Genre/" }

            };
            IConfiguration mockConfiguration = new ConfigurationBuilder()
               .AddInMemoryCollection(inMemorySettings)
               .Build();

            int id = 1;
            var service = new GenreService(mockConfiguration);
            //act

            var result = await service.GetGenre(id) as GenreInfoDTO;
            //assert
            
            Assert.NotNull(result);
            Assert.IsType<GenreInfoDTO>(result);
            Assert.Equal(id, result.Id);
            Console.WriteLine();

        }
    }
}
