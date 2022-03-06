using WebTMDT_Client.ResponseModel;
using WebTMDTLibrary.DTO;

namespace WebTMDT_Client.Service
{
    public interface IReviewService
    {
        public Task<PostReviewResponseModel> GetPostReviewResponse(PostReviewDTO dTO,string token);
        public Task<DeleteReviewResponse> GetDeleteReviewResponse(DeleteReviewDTO dTO, string token);
    }
}
