using AutoMapper;
using WebTMDTLibrary.Data;
using WebTMDTLibrary.DTO;

namespace WebTMDTLibrary.Helper
{
    public class AutoMapperSetting : Profile
    {
        public AutoMapperSetting()
        {
            //Genre
            CreateMap<Genre, GenreDTO>().ReverseMap();
            CreateMap<Genre, SmallerGenreDTO>().ReverseMap();
            //User
            CreateMap<AppUser, UserRegisterDTO>().ReverseMap();
            CreateMap<AppUser, LoginUserDTO>().ReverseMap();
            CreateMap<AppUser, UpdateUserDTO>().ReverseMap();
            CreateMap<AppUser, SimpleUserDTO>().ReverseMap();
            CreateMap<AppUser, SimpleUserForAdminDTO>().ReverseMap();
            //Publisher
            CreateMap<Publisher, PublisherDTO>().ReverseMap();
            CreateMap<Publisher, DetailPublisherDTO>().ReverseMap();
            //Author
            CreateMap<Author, AuthorDTO>().ReverseMap();
            CreateMap<Author, DetailAuthorDTO>().ReverseMap();
            //Book
            CreateMap<Book, BookDTO>().ReverseMap();
            CreateMap<Book, SimpleBookInfoDTO>().ReverseMap();
            //PromotionInfo
            CreateMap<PromotionInfo, PromotionInfoDTO>().ReverseMap();
            CreateMap<PromotionInfo, SimplePromotionInfoDTO>().ReverseMap();
            //Promotion
            CreateMap<Promotion,PromotionDTO>().ReverseMap();
            CreateMap<Promotion, FullPromotionDTO>().ReverseMap();
            CreateMap<Promotion, CreatPromotionDTO>().ReverseMap();
            //Review
            CreateMap<Review, ReviewDTO>().ReverseMap();
            CreateMap<Review, CreateReviewDTO>().ReverseMap();
            //Order
            CreateMap<Order, OrderDTO>().ReverseMap();
            CreateMap<Order,CreateOrderDTO>().ReverseMap();
            //Order Details
            CreateMap<OrderDetail, OrderDetailDTO>().ReverseMap();
            CreateMap<OrderDetail, CreateOrderDetailDTO>().ReverseMap();
            //Employee
            CreateMap<Employee, EmployeeDTO>().ReverseMap();
            CreateMap<Employee, CreateEmployeeDTO>().ReverseMap();
            //DiscountCode
            CreateMap<DiscountCode, DiscountCodeDTO>().ReverseMap();
            CreateMap<DiscountCode, CreateDiscountCodeDTO>().ReverseMap();
        }
    }
}
