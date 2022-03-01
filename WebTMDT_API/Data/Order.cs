namespace WebTMDT_API.Data
{
    public class Order
    {
        public int Id { get; set; }

        public string UserID { get; set; }
        public virtual AppUser User { get; set; }
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
        public virtual IList<OrderDetail> OrderDetails { get; set; }
        public int? DiscountCodeID { get; set; }
        public DiscountCode DiscountCode { get; set; }
        public string? ShipperID { get; set; }
        public virtual AppUser Shipper { get; set; }
    }


}
