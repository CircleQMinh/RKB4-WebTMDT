namespace WebTMDT_Client.Data
{
    public class DiscountCode
    {
        public int Id { get; set; }
        public virtual Order Order { get; set; }
        public string Code { get; set; }
        public string? DiscountPercent { get; set; }
        public string? DiscountAmount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Status { get; set; }
    }
}
