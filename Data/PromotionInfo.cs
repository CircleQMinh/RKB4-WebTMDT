namespace WebTMDT.Data
{
    public class PromotionInfo
    {
        public int Id { get; set; }

        public Book Book { get; set; }
        public int PromotionId { get; set; }
        public Promotion Promotion { get; set; }
        public string? PromotionPercent { get; set; }
        public string? PromotionAmount { get; set; }
    }
}
