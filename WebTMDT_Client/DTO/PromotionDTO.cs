using WebTMDT_Client.Data;

namespace WebTMDT_Client.DTO
{
    public class PromotionDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string imgUrl { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }

    public class FullPromotionDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string imgUrl { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public virtual IList<SimplePromotionInfoDTO> PromotionInfos { get; set; }
        public int Status { get; set; }
        public int Visible { get; set; }
    }

    public class CreatPromotionDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string imgUrl { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Status { get; set; }
        public int Visible { get; set; }
    }
}
