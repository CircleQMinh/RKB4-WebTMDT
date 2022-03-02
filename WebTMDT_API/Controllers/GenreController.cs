using AutoMapper;
using WebTMDTLibrary.DTO;
using WebTMDT_API.Repository;
using Microsoft.AspNetCore.Mvc;
using WebTMDT_API.Data;

namespace WebTMDT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenreController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public GenreController(IUnitOfWork _unitOfWork, IMapper _mapper)
        {
            unitOfWork = _unitOfWork;
            mapper = _mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var genre = await unitOfWork.Genres.GetAll(q => q.Id != 0, q => q.OrderBy(genre => genre.Id), null);
                var result = mapper.Map<IList<GenreDTO>>(genre);
                return Ok(new {  result });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.ToString() });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var genre = await unitOfWork.Genres.Get(q=>q.Id == id);
                var result = mapper.Map<GenreInfoDTO>(genre);
                return Ok(new { result });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.ToString() });
            }
        }

        [HttpGet("test")]
        public async Task<ActionResult<List<GenreDTO>>> Test()
        {
            var model = await unitOfWork.Genres.GetAll(q=>true,null,null);
            var result = mapper.Map<IList<GenreDTO>>(model);
            return Ok( new { result,success=true });
        }
    }
}
