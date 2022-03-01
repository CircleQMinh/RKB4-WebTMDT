using AutoMapper;
using WebTMDTLibrary.DTO;

using WebTMDT_API.Repository;
using Microsoft.AspNetCore.Mvc;

namespace WebTMDT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublisherController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public PublisherController(IUnitOfWork _unitOfWork, IMapper _mapper)
        {
            unitOfWork = _unitOfWork;
            mapper = _mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var publishers = await unitOfWork.Publishers.GetAll(q => q.Id != 0, q => q.OrderBy(p => p.Id), new List<string> { "Books" });
                var result = mapper.Map<IList<DetailPublisherDTO>>(publishers);
                return Ok(new { result = result });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.ToString() });
            }
        }
    }
}
