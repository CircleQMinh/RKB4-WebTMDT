namespace WebTMDTLibrary.DTO
{
    public class DiscountCodeDTO
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string? DiscountPercent { get; set; }
        public string? DiscountAmount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Status { get; set; }
    }

    public class CreateDiscountCodeDTO
    {
        public string Code { get; set; }
        public string? DiscountPercent { get; set; }
        public string? DiscountAmount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Status { get; set; }
    }

    public class ApplyDiscountCode
    {

        public string Code { get; set; }
        public int OrderID { get; set; }
    }

    public class RedeemDiscountCodeDTO
    {
        public string DicountMode { get;set; }
        public string UserId { get; set; }
    }
}
