using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WebTMDT.Controllers;
using WebTMDT_API.Data;
using WebTMDT_API.Helper;
using WebTMDT_API.Repository;
using WebTMDTLibrary.DTO;
using Xunit;

namespace WebTMDT.UnitTests.API.Controller
{
    public class ProductControllerTest
    {
        private readonly Mock<IUnitOfWork> _unitofwork;
        private readonly Mock<IMapper> _mapper;
        private readonly ProductController _productController;
        public ProductControllerTest()
        {
            _unitofwork = new Mock<IUnitOfWork>();
            _mapper = new Mock<IMapper>();
            _productController = new ProductController(_unitofwork.Object, _mapper.Object);
        }

        [Fact]
        public async void ProductControllerGetBooks_ValidInput_Return200Ok()
        {
            //arrange
            _unitofwork.Setup(s => s.Books.GetAll(
                It.IsAny<Expression<Func<Book, bool>>>(),
                It.IsAny<Func<IQueryable<Book>, IOrderedQueryable<Book>>>(),
                It.IsAny<List<string>>(),
                It.IsAny<PaginationFilter>()
            )).ReturnsAsync(new List<Book>());
            _unitofwork.Setup(s => s.Books.GetCount(
                It.IsAny<Expression<Func<Book,bool>>>()
            )).ReturnsAsync(It.IsAny<int>);

            _mapper.Setup(s=>s.Map<IList<BookDTO>>(It.IsAny<List<Book>>())).Returns(new List<BookDTO>());

            int pageNumber = 1;
            int pageSize = 10;
            string keyworld = "ABC";
            string priceRange = null;
            string genreFilter = null;

            //act

            var result = await _productController.GetBooks(pageSize,pageNumber,keyworld,priceRange,genreFilter);
            var okobjresult = result as OkObjectResult;

            //assert
            Assert.NotNull(result);
            Assert.Equal((int)HttpStatusCode.OK, okobjresult.StatusCode);

        }
        [Fact]
        public async void ProductControllerGetBookById_ValidInput_Return200Ok()
        {
            //arrange
            int id = 0;
            _unitofwork.Setup(s => s.Books.Get(
                It.IsAny<Expression<Func<Book,bool>>>(),
                It.IsAny<List<string>>()
                )).ReturnsAsync(new Book());
            _unitofwork.Setup(s=>s.Reviews.GetAll(
                It.IsAny<Expression<Func<Review, bool>>>(),
                It.IsAny<Func<IQueryable<Review>, IOrderedQueryable<Review>>>(),
                It.IsAny<List<string>>()
                )).ReturnsAsync(new List<Review>());
            _mapper.Setup(s => s.Map<BookDTO>(It.IsAny<Book>())).Returns(new BookDTO());
            _mapper.Setup(s => s.Map<IList<ReviewDTO>>(It.IsAny<List<Review>>())).Returns(new List<ReviewDTO>());

            //act


            var result = await _productController.GetBookById(id);
            var okobjresult = result as OkObjectResult;

            //assert

            Assert.NotNull(result);
            Assert.Equal((int)HttpStatusCode.OK, okobjresult.StatusCode);

        }
    }
}
