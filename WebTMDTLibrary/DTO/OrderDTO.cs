using System.ComponentModel.DataAnnotations;
using WebTMDTLibrary.Data;

namespace WebTMDTLibrary.DTO
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public string UserID { get; set; }
        public string ContactName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string PaymentMethod { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime ShippedDate { get; set; }
        public int TotalItem { get; set; }
        public double TotalPrice { get; set; }
        public int Status { get; set; }
        public string Note { get; set; }
        public virtual IList<OrderDetailDTO> OrderDetails { get; set; }
        public int? DiscountCodeID { get; set; }
        public string? ShipperID { get; set; }
        public virtual SimpleUserDTO Shipper { get; set; }

    }
    public class PostOrderDTO
    {
        [Required(ErrorMessage = "Phải nhập tên liên lạc!")]
        public string ContactName { get; set; }
        [Required(ErrorMessage = "Phải nhập số nhà!")]
        public string AddressNo { get; set; }
        [Required(ErrorMessage = "Phải nhập tên đường!")]
        public string Street { get; set; }
        [Required]
        public string Ward { get; set; }
        [Required]
        public string District { get; set; }
        [Required]
        public string City { get; set; }
        [Required(ErrorMessage = "Phải nhập số điện thoại")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "SDT không hợp lệ")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "Phải nhập email của bạn")]
        [EmailAddress(ErrorMessage = "Phải nhập email hợp lệ")]
        public string Email { get; set; }
        public string PaymentMethod { get; set; }
        public string? Note { get; set; }
        public int? DiscountCodeID { get; set; }
    }
    public class CreateOrderDTO
    {
        public string UserID { get; set; }
        public string ContactName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string PaymentMethod { get; set; }
        public DateTime OrderDate { get; set; }
        public int TotalItem { get; set; }
        public double TotalPrice { get; set; }
        public int Status { get; set; }
        public string Note { get; set; }
        public virtual IList<CreateOrderDetailDTO> OrderDetails { get; set; }
        public int? DiscountCodeID { get; set; }
    }
    public class EditOrderDTO
    {
        public int Status { get; set; }
        public string Note { get; set; }
    }

    public class AcceptOrderDTO
    {
        public int OrderID { get; set; }
        public string ShipperId { get; set; }
    }

    public class CompleteOrderDTO
    {
        public int OrderID { get; set; }
        public string ShipperId { get; set; }
        public string Note { get; set; }
        public int Status { get; set; }
        public DateTime ShippedDate { get; set; }
    }
}
