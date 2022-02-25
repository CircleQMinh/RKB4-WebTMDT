using WebTMDT_Client.ResponseModel;
using WebTMDTLibrary.DTO;

namespace WebTMDT_Client.Service
{
    public interface IOrderService
    {
        public string GetVNPayUrl(double totalPrice,string token);
        public PostOrderResponseModel GetPostOrderResponse(PostOrderDTO dto,Cart cart,string token,string userId);
    }
}
