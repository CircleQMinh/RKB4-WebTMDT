using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Linq.Expressions;
using System.Net;
using WebTMDT.Controllers;
using WebTMDT.Repository;
using WebTMDTLibrary.Data;
using WebTMDTLibrary.DTO;
using Xunit;

namespace WebTMDT.UnitTests
{
    public class GenreControllerTests
    {
        private readonly Mock<IUnitOfWork> _unitofwork;
        private readonly Mock<IMapper> _mapper;
        private readonly GenreController _genreController;
        public GenreControllerTests( )
        {
            _unitofwork = new Mock<IUnitOfWork>();
            _mapper = new Mock<IMapper>();
            _genreController = new GenreController(_unitofwork.Object,_mapper.Object);
        }

        [Fact]
        public async void GenreControllerGetAll_ValidCall_Return200Ok()
        {
            var id = 1;
            var name = "test name";
            // Arrange
            var listGenre = new List<Genre>
            {
                new Genre {Id = id, Name = name, Description = "test description"}
            };
            _unitofwork.Setup(s => s.Genres.GetAll(
                It.IsAny<Expression<Func<Genre, bool>>>(),
                It.IsAny<Func<IQueryable<Genre>, IOrderedQueryable<Genre>>>(),
                It.IsAny<List<string>>())
            )
            .ReturnsAsync(listGenre);

            _mapper.Setup(m => m.Map<IList<GenreDTO>>(It.IsAny<List<Genre>>()))
                .Returns(new List<GenreDTO> { new GenreDTO { Id = id, Name = name } });

            // Act
            var result = await _genreController.Get();
            var okobjresult = result as OkObjectResult;
            // Assert
            Assert.NotNull(result);
            Assert.Equal((int)HttpStatusCode.OK, okobjresult.StatusCode);
            // Additional tests
            var response = okobjresult.Value;
            var r = response.GetType().GetProperty("result").GetValue(response, null);
            var resultData = r as List<GenreDTO>;
            Assert.NotNull(resultData);
            Assert.Equal(1, resultData.Count);
            Assert.Equal(id, resultData[0].Id);
            Assert.Equal(name, resultData[0].Name);
        }
    }
}
