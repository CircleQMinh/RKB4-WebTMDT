namespace WebTMDT.Data
{
    public class Promotion
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string imgUrl { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public virtual IList<PromotionInfo> PromotionInfos { get; set; }
        public int Status { get; set; }
        public int Visible { get; set; }
    }
}
