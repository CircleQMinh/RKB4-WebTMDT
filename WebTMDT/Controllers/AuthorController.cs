using AutoMapper;
using WebTMDT.DTO;

using WebTMDT.Repository;
using Microsoft.AspNetCore.Mvc;

namespace DotNet6WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public AuthorController(IUnitOfWork _unitOfWork, IMapper _mapper)
        {
            unitOfWork = _unitOfWork;
            mapper = _mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var authors = await unitOfWork.Authors.GetAll(q => q.Id != 0, q => q.OrderBy(p => p.Id), new List<string> { "Books"});
                var result = mapper.Map<IList<DetailAuthorDTO>>(authors);
                return Ok(new { result = result });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.ToString() });
            }
        }
    }
}
