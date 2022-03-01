using AutoMapper;
using WebTMDTLibrary.DTO;

namespace WebTMDT_Client.Mapper
{
    public class AutoMapperSetting : Profile
    {
        public AutoMapperSetting()
        {

            //Special
            CreateMap<PostOrderDTO, CreateOrderDTO>().ReverseMap();
            CreateMap<CartItem,CreateOrderDetailDTO>().ReverseMap();
        }
    }
}
