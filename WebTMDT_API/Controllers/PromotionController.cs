using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using WebTMDT_API.Data;
using WebTMDT_API.Helper;
using WebTMDT_API.Repository;
using WebTMDTLibrary.DTO;

namespace WebTMDT_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PromotionController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public PromotionController(IUnitOfWork _unitOfWork, IMapper _mapper)
        {
            unitOfWork = _unitOfWork;
            mapper = _mapper;
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> GetAllPromotion(string status, string orderby, string sort, int pageNumber, int pageSize)
        {
            try
            {
                Expression<Func<Promotion, bool>> expression = status == "all" ? q => true : q => q.Status == int.Parse(status);
                Func<IQueryable<Promotion>, IOrderedQueryable<Promotion>> orderBy = null;
                switch (orderby)
                {
                    case "Id":
                        orderBy = (sort == "Asc") ? q => q.OrderBy(p => p.Id) : q => q.OrderByDescending(p => p.Id);
                        break;
                    case "startDate":
                        orderBy = (sort == "Asc") ? q => q.OrderBy(p => p.StartDate) : q => q.OrderByDescending(p => p.StartDate);
                        break;
                    case "endDate":
                        orderBy = (sort == "Asc") ? q => q.OrderBy(p => p.EndDate) : q => q.OrderByDescending(p => p.EndDate);
                        break;
                    default:
                        orderBy = (sort == "Asc") ? q => q.OrderBy(p => p.Id) : q => q.OrderByDescending(p => p.Id);
                        break;
                }

                var promos = await unitOfWork.Promotions.GetAll(expression, orderBy, new List<string> { "PromotionInfos" }, new PaginationFilter(pageNumber, pageSize));
                var count = await unitOfWork.Promotions.GetCount(expression);
                var result = mapper.Map<IList<FullPromotionDTO>>(promos);



                return Accepted(new { result = result, total = count });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }

        }
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> PostPromotion([FromBody] CreatPromotionDTO promoDTO)
        {
            if (!ModelState.IsValid)
            {
                return Ok(new { error = "Dữ liệu chưa hợp lệ", success = false });
            }
            try
            {
                var newPromo = mapper.Map<Promotion>(promoDTO);
                await unitOfWork.Promotions.Insert(newPromo);
                await unitOfWork.Save();
                return Ok(new { promotion = newPromo, success = true });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> PutPromotion([FromBody] CreatPromotionDTO promoDTO, int id)
        {
            if (!ModelState.IsValid)
            {
                return Ok(new { error = "Dữ liệu chưa hợp lệ", success = false });
            }
            try
            {
                var promo = await unitOfWork.Promotions.Get(q => q.Id == id);
                if (promo == null)
                {
                    return Ok(new { error = "Không tìm thấy dữ liệu", success = false });
                }
                mapper.Map(promoDTO, promo);
                unitOfWork.Promotions.Update(promo);
                await unitOfWork.Save();

                return Ok(new { promotion = promo, success = true });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeletePromotion(int id)
        {
            try
            {
                var promo = await unitOfWork.Promotions.Get(q => q.Id == id, new List<string> { "PromotionInfos" });
                if (promo == null)
                {
                    return Ok(new { success = false, msg = "Không tìm thấy chương trình!" });
                }
                foreach (var item in promo.PromotionInfos)
                {
                    var product = await unitOfWork.Books.Get(q => q.PromotionInfoID == item.Id, new List<string> { "PromotionInfo" });
                    if (product != null)
                    {
                        product.PromotionInfoID = null;
                        product.PromotionInfo = null;
                        unitOfWork.Books.Update(product);
                        await unitOfWork.Save();
                    }
                }
                await unitOfWork.Promotions.Delete(promo.Id);
                await unitOfWork.Save();
                return Ok(new { success = true });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.ToString() });
            }
        }
        [HttpGet("{id}/promotionInfos")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> GetPromotionInfosByPromotionId(int id)
        {
            try
            {
                var promoInfos = await unitOfWork.PromotionInfos.GetAll(q => q.PromotionId == id, null, new List<string> { "Book" });
                if (promoInfos.Count == 0)
                {
                    return Ok(new { success = false, msg = "Không tìm thấy thông tin.", result = new string[0] });
                }
                var result = mapper.Map<IList<PromotionInfoDTO>>(promoInfos);
                return Ok(new { success = true, result });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("{id}/promotionInfos")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> PostPromotionInfo(CreatePromotionInfoDTO dto, int id)
        {
            try
            {
                var product = await unitOfWork.Books.Get(q => q.Id == dto.BookId);
                var promotion = await unitOfWork.Promotions.Get(q => q.Id == id);
                if (product == null || promotion == null)
                {
                    return Ok(new { success = false, msg = "Không tìm thấy thông tin." });
                }
                var promoInfo = new PromotionInfo() { PromotionId = id, PromotionAmount = dto.PromotionAmount, PromotionPercent = dto.PromotionPercent };
                product.PromotionInfo = promoInfo;
                unitOfWork.Books.Update(product);
                await unitOfWork.Save();
                return Ok(new { success = true });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPut("promotionInfos/{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> PutPromotionInfo(EditPromotionInfoDTO dto, int id)
        {
            try
            {

                var promotionInfo = await unitOfWork.PromotionInfos.Get(q => q.Id == id);
                if (promotionInfo == null)
                {
                    return Ok(new { success = false, msg = "Không tìm thấy thông tin." });
                }

                promotionInfo.PromotionAmount = dto.PromotionAmount;
                promotionInfo.PromotionPercent = dto.PromotionPercent;

                unitOfWork.PromotionInfos.Update(promotionInfo);
                await unitOfWork.Save();

                return Ok(new { success = true });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpDelete("promotionInfos/{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeletePromotionInfo(int id)
        {
            try
            {
                var promoI = await unitOfWork.PromotionInfos.Get(q => q.Id == id);
                if (promoI == null)
                {
                    return Ok(new { success = false, msg = "Không tìm thấy dữ liệu!" });
                }
                var product = await unitOfWork.Books.Get(q => q.PromotionInfoID == id, new List<string> { "PromotionInfo" });
                if (product != null)
                {
                    product.PromotionInfoID = null;
                    product.PromotionInfo = null;
                    unitOfWork.Books.Update(product);
                    await unitOfWork.Save();
                }

                await unitOfWork.PromotionInfos.Delete(promoI.Id);
                await unitOfWork.Save();
                return Ok(new { success = true });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.ToString() });
            }
        }

        [HttpGet("getPromotableProduct")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> GetPromotableProduct()
        {
            try
            {
                var books = await unitOfWork.Books.GetAll(q => q.PromotionInfoID == null, q => q.OrderBy(b => b.Id), null);
                var result = mapper.Map<IList<BookDTO>>(books);
                return Ok(new { result = result });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.ToString() });
            }
        }
    }
}

