using WebTMDT_Client.Data;

namespace WebTMDT_Client.DTO
{
    public class PromotionInfoDTO
    {
        public int Id { get; set; }
        public int PromotionId { get; set; }
        //public SimpleBookInfoDTO Book { get; set; }
        public PromotionDTO Promotion { get; set; }
        public string? PromotionPercent { get; set; }
        public string? PromotionAmount { get; set; }
    }
    public class SimplePromotionInfoDTO
    {
        public int Id { get; set; }
        public int PromotionId { get; set; }
        public string? PromotionPercent { get; set; }
        public string? PromotionAmount { get; set; }
    }

    public class CreatePromotionInfoDTO
    {
        public int BookId { get; set; }
        public string? PromotionPercent { get; set; }
        public string? PromotionAmount { get; set; }
    }

    public class EditPromotionInfoDTO
    {
        public string? PromotionPercent { get; set; }
        public string? PromotionAmount { get; set; }
    }
}
