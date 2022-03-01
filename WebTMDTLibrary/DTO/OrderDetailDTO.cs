

namespace WebTMDTLibrary.DTO
{
    public class OrderDetailDTO
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int BookId { get; set; }
        public SimpleBookInfoDTO Book { get; set; }
        public int Quantity { get; set; }
        public string? PromotionPercent { get; set; }
        public string? PromotionAmount { get; set; }
    }

    public class CreateOrderDetailDTO
    {
        public int BookId { get; set; }
        public int Quantity { get; set; }
        public string? PromotionPercent { get; set; }
        public string? PromotionAmount { get; set; }
    }
}
