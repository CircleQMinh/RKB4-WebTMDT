using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebTMDT.Helper;
using WebTMDT.Repository;

namespace WebTMDT.Views.Shared.Components.ProductList
{
    [ViewComponent]
    public class ProductList : ViewComponent
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        public ProductList(IUnitOfWork _unitOfWork, IMapper _mapper)
        {
            unitOfWork = _unitOfWork;
            mapper = _mapper;
        }
        public  IViewComponentResult Invoke()
        {
            var books = unitOfWork.Books.GetAll(q=>true, q => q.OrderBy(book => book.Id),
                    new List<string> { "Authors", "Genres", "Publisher", "PromotionInfo" }, new PaginationFilter(1, 8)).Result;
            return View("ProductList",books);
        }

    }
}
