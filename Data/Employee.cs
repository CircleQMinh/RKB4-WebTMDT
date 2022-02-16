namespace WebTMDT.Data
{
    public class Employee
    {
        public int Id { get; set; }

        public string? ShipperId{ get; set; }
        public virtual AppUser Shipper { get; set; }

        public string Name { get; set; }
        public DateTime DoB { get; set; }
        public string Address { get; set; }
        public string Sex { get; set; }
        public int Salary { get; set; }
        public string CMND { get; set; }
        public DateTime StartDate { get; set; }
        public string Email { get; set; }
        public string imgUrl { get; set; }
        public int Status { get; set; }
    }
}
