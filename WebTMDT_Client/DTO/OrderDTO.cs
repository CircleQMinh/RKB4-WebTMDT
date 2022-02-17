using WebTMDT_Client.Data;

namespace WebTMDT_Client.DTO
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
