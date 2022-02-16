using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebTMDT.DTO;
using WebTMDT.Repository;

namespace WebTMDT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public ProductController(IUnitOfWork _unitOfWork, IMapper _mapper)
        {
            unitOfWork = _unitOfWork;
            mapper = _mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetProduct()
        {
            return Ok(new {success=true});  
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookById(int id)
        {
            try
            {
                var book = await unitOfWork.Books.Get(q => q.Id == id, new List<string> { "Authors", "Genres", "Publisher", "PromotionInfo" });
                if (book == null)
                {
                    return Ok(new { success = false, msg = "Không tìm thấy sản phẩm" });
                }
                var result = mapper.Map<BookDTO>(book);
                var rev = await unitOfWork.Reviews.GetAll(q => q.BookId == id, q => q.OrderBy(r => r.Date), new List<string> { "User" });
                var reviews = mapper.Map<IList<ReviewDTO>>(rev);
                return Ok(new { result = result, success = true, reviews = reviews });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.ToString() });
            }
        }
    }
}
