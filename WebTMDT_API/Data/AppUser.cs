using Microsoft.AspNetCore.Identity;


namespace WebTMDT_API.Data
{
    public class AppUser : IdentityUser
    {
        public string? imgUrl { get; set; }
        public int Coins { get; set; }
        public virtual IList<Book> Wishlist { get; set; }

        public AppUser()
        {
            Wishlist = new List<Book>();
        }
    }
}