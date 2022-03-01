using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebTMDT_API.Repository;
using WebTMDT_API.Authorize;
using WebTMDT_API.Data;
using WebTMDTLibrary.DTO;
using WebTMDTLibrary.Helper;

namespace WebTMDT_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IAuthManager authManager;
        private readonly UserManager<AppUser> userManager;

        public ReviewController(IUnitOfWork _unitOfWork, IMapper _mapper, UserManager<AppUser> _userManager, IAuthManager _authManager)
        {
            userManager = _userManager;
            unitOfWork = _unitOfWork;
            mapper = _mapper;
            authManager = _authManager;

        }
        [HttpPost]
        //[Authorize]
        public async Task<IActionResult> PostReview([FromBody] CreateReviewDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return Ok(new { error = "Dữ liệu chưa hợp lệ", success = false });
            }
            try
            {
                var product = await unitOfWork.Books.Get(q => q.Id == dto.BookId);
                var user = await userManager.FindByIdAsync(dto.UserID);
                if (user == null || product == null)
                {
                    return Ok(new { error = "Dữ liệu chưa hợp lệ", success = false });
                }

                var review = await unitOfWork.Reviews.Get(q => q.BookId == dto.BookId && q.UserID == dto.UserID);
                if (review == null)
                {
                    review = mapper.Map<Review>(dto);
                    await unitOfWork.Reviews.Insert(review);
                    await unitOfWork.Save();
                    return Ok(new { success = true, newReview = true, update = false, error = "" });
                }

                review.Content = dto.Content;
                review.Date = dto.Date;
                review.Star = dto.Star;
                review.Recomended = dto.Recomended;

                unitOfWork.Reviews.Update(review);
                await unitOfWork.Save();


                return Ok(new { success = true, newReview = false, update = true, error = "" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> DeleteReview([FromBody] DeleteReviewDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return Ok(new { error = "Dữ liệu chưa hợp lệ", success = false });
            }
            try
            {
                var product = await unitOfWork.Books.Get(q => q.Id == dto.BookId);
                var user = await userManager.FindByIdAsync(dto.UserID);
                if (user == null || product == null)
                {
                    return Ok(new { error = "Dữ liệu chưa hợp lệ", success = false });
                }
                var review = await unitOfWork.Reviews.Get(q => q.BookId == dto.BookId && q.UserID == dto.UserID);
                await unitOfWork.Reviews.Delete(review.Id);
                await unitOfWork.Save();
                return Ok(new { success = true });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //[HttpGet]
        //public async Task<IActionResult> GetCurrentDate()
        //{
        //    return Accepted(new {str = DateTime.UtcNow.ToString("o", System.Globalization.CultureInfo.InvariantCulture) });
        //}
    }
}

