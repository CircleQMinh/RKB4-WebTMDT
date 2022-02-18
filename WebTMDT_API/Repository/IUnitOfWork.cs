
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebTMDTLibrary.Data;

namespace WebTMDT.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Book> Books { get; }
        IGenericRepository<Genre> Genres { get; }
        IGenericRepository<Author> Authors { get; }
        IGenericRepository<Publisher> Publishers { get; }

        IGenericRepository<Review> Reviews { get; }
        IGenericRepository<PromotionInfo> PromotionInfos { get; }
        IGenericRepository<Promotion> Promotions { get; }
        IGenericRepository<Order> Orders { get; }
        IGenericRepository<OrderDetail> OrderDetails { get; }
        IGenericRepository<DiscountCode> DiscountCodes { get; }

        IGenericRepository<Employee> Employees { get; }
        IGenericRepository<AppUser> Users { get; }
        Task Save();
    }
}
