namespace WebTMDT.DTO
{
    public class EmployeeDTO
    {
        public int Id { get; set; }

        public string? ShipperId { get; set; }
        public virtual SimpleUserDTO Shipper { get; set; }

        public string Name { get; set; }
        public DateTime DoB { get; set; }
        public string Address { get; set; }
        public string Sex { get; set; }
        public int Salary { get; set; }
        public string CMND { get; set; }
        public DateTime StartDate { get; set; }
        public int Status { get; set; }
    }
    public class CreateShipperDTO
    {
        public string imgUrl { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public IList<string> Roles { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }


        public DateTime DoB { get; set; }
        public string Address { get; set; }
        public string Sex { get; set; }
        public int Salary { get; set; }
        public string CMND { get; set; }
        public DateTime StartDate { get; set; }
    }

    public class CreateEmployeeDTO
    {
        public string Name { get; set; }
        public DateTime DoB { get; set; }
        public string Address { get; set; }
        public string Sex { get; set; }
        public int Salary { get; set; }
        public string CMND { get; set; }
        public DateTime StartDate { get; set; }
        public string Email { get; set; }
        public string imgUrl { get; set; }
    }

}
