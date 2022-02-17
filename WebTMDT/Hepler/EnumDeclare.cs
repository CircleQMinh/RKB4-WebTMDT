namespace WebTMDT.Helper
{
    public class EnumDeclare
    {
    }
    enum OrderStatus
    {
        NotChecked = 0,   
        Checked = 1,   
        Delivering = 2,    
        Done = 3,
        Canceled =  4
    }
    enum DiscountCodeCost
    {
        Amount10K = 100,
        Amount20K = 200,
        Amount50K = 300,
        Percent10 = 300,
        Percent20 = 400,
        Percent50 = 500
    }

    enum DiscountCodeValue
    {
        Amount10K = 10000,
        Amount20K = 20000,
        Amount50K = 50000,
        Percent10 = 10,
        Percent20 = 20,
        Percent50 = 50
    }
}
