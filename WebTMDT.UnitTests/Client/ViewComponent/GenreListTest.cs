using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebTMDT_Client.Service;
using WebTMDT_Client.ViewModel;
using WebTMDT_Client.Views.Shared.Components.GenreList;
using WebTMDTLibrary.DTO;
using Xunit;
namespace WebTMDT.UnitTests.Client.ViewComponent
{
    public class GenreListTest
    {
        [Fact]
        public async Task GenreListTest_ValidCall_ReturnViewAsync()
        {
            //arrange
            List<GenreDTO> list = new List<GenreDTO>() { 
            new GenreDTO { Id=1,Name="Abcd1"},
            new GenreDTO { Id=2,Name="Abcd2"},
            new GenreDTO { Id=3,Name="Abcd3"},
            new GenreDTO { Id=4,Name="Abcd4"}};
            var mockService = new Mock<IGenreService>();
            mockService.Setup(s => s.GetGenres()).ReturnsAsync(list);
            var viewcomponent = new GenreList(mockService.Object);
            //act

            var result = await viewcomponent.InvokeAsync() as ViewViewComponentResult;
            var result_model = result.ViewData.Model;
            //assert

            Assert.NotNull(result);
            Assert.NotNull(result_model);
            Assert.Equal(list, result_model);

            Console.WriteLine();
        }

    }
}
