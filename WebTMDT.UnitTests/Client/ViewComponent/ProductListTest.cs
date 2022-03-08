using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebTMDT.Views.Shared.Components.ProductList;
using WebTMDT_Client.Service;
using WebTMDT_Client.ViewModel;
using Xunit;

namespace WebTMDT.UnitTests.Client.ViewComponent
{
    public class ProductListTest
    {
        private readonly ProductList productList;
        private readonly Mock<IProductService> productService;
        public ProductListTest()
        {
            productService = new Mock<IProductService>();
            productList = new ProductList(productService.Object);
        }

        [Fact]
        public async Task InvokeAsync_ValidCall_ReturnViewAsync()
        {
            //arrange
            var model = new ProductListFilterModel() {pageNumber=1,pageSize=10 };

            productService.Setup(s => s.GetProductListViewModel(It.IsAny<ProductListFilterModel>())
            ).ReturnsAsync(new ProductListViewModel());

            //act
            var result = await productList.InvokeAsync(model) as ViewViewComponentResult;
         

            //assert

            Assert.NotNull(result);

            Assert.True(result.ViewData.ContainsKey("pageNumber"));
            Assert.True(result.ViewData.ContainsKey("pageSize"));
        }

        [Fact]
        public async Task InvokeAsync_InvalidCall_ReturnViewAsync()
        {
            //arrange
            var model = new ProductListFilterModel() { };

            productService.Setup(s => s.GetProductListViewModel(It.IsAny<ProductListFilterModel>())
            ).ReturnsAsync(new ProductListViewModel());

            //act
            var result = await productList.InvokeAsync(model) as ViewViewComponentResult;


            //assert

            Assert.Null(result);

        }
    }
}
