using AutoMapper;
using WebTMDTLibrary.DTO;
using WebTMDT_API.Repository;
using Microsoft.AspNetCore.Mvc;
using WebTMDT_API.Data;
using Microsoft.AspNetCore.Authorization;
using WebTMDT_API.Helper;

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
                var genre = await unitOfWork.Genres.GetAll(null, q => q.OrderBy(genre => genre.Id), null);
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

        [HttpGet("details")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> GetGenreInfoForAdmin(string orderby, string sort, int pageNumber, int pageSize)
        {
            Func<IQueryable<Genre>, IOrderedQueryable<Genre>> orderBy = null;
            switch (orderby)
            {
                case "Name":
                    orderBy = (sort == "Asc") ? q => q.OrderBy(o => o.Name) : q => q.OrderByDescending(o => o.Name);
                    break;
                case "Id":
                    orderBy = (sort == "Asc") ? q => q.OrderBy(o => o.Id) : q => q.OrderByDescending(o => o.Id);
                    break;
                case "NumberOfBook":
                    orderBy = (sort == "Asc") ? q => q.OrderBy(o => o.Books.Count) : q => q.OrderByDescending(o => o.Books.Count);
                    break;
            }
            try
            {
                var genres = await unitOfWork.Genres.GetAll(null, orderBy, new List<string> { "Books" }, new PaginationFilter(pageNumber, pageSize));
                var result = mapper.Map<IList<GenreInfoAdminDTO>>(genres);
                var count = await unitOfWork.Genres.GetCount(null);

                return Ok(new { success = true, result = result, total = count });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> PostGenre([FromBody] CreateGenreDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return Ok(new { error = "Dữ liệu chưa hợp lệ", success = false });
            }
            var exist = await unitOfWork.Genres.Get(q => q.Name == dto.Name.Trim());
            if (exist!=null)
            {
                return Ok(new { error = "Đã tồn tại thể loại này rồi", success = false });
            }
            try
            {
                var genre = mapper.Map<Genre>(dto);
                await unitOfWork.Genres.Insert(genre);
                await unitOfWork.Save();


                return Ok(new {  success = true,genre = genre });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> PutGenre([FromBody] CreateGenreDTO dto,int id)
        {
            if (!ModelState.IsValid)
            {
                return Ok(new { error = "Dữ liệu chưa hợp lệ", success = false });
            }
            var genre = await unitOfWork.Genres.Get(q => q.Id == id);
            if (genre == null)
            {
                return Ok(new { error = "Không tìm thấy thể loại", success = false });
            }
            var exist = await unitOfWork.Genres.Get(q=>q.Id!=id&&q.Name==dto.Name.Trim());
            if (exist != null)
            {
                return Ok(new { error = "Đã tồn tại thể loại này rồi", success = false });
            }
            try
            {
                mapper.Map(dto, genre);
                unitOfWork.Genres.Update(genre);
                await unitOfWork.Save();


                return Ok(new { success = true, genre = genre });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteGenre(int id)
        {
            try
            {
                var genre = await unitOfWork.Genres.Get(q => q.Id == id);
                if (genre == null)
                {
                    return Ok(new { error = "Không tìm thấy thể loại", success = false });
                }
                await unitOfWork.Genres.Delete(id);
                await unitOfWork.Save();
                return Ok(new { success = true });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.ToString() });
            }
        }
    }
}
