using WebTMDT_Client.ResponseModel;
using WebTMDTLibrary.DTO;

namespace WebTMDT_Client.Service
{
    public interface IReviewService
    {
        public PostReviewResponseModel GetPostReviewResponse(PostReviewDTO dTO,string token);
        public DeleteReviewResponse GetDeleteReviewResponse(DeleteReviewDTO dTO, string token);
    }
}
