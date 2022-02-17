namespace WebTMDT_Client.Data
{
    public class OrderDetail
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; }
        public int Quantity { get; set; }

        public string? PromotionPercent { get; set; }
        public string? PromotionAmount { get; set; }
    }
}
