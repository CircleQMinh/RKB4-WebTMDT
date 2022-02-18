
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebTMDTLibrary.Data;
using WebTMDT.Data;

namespace WebTMDT.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DatabaseContext context;
        private IGenericRepository<Book> books;
        private IGenericRepository<Genre> genres;
        private IGenericRepository<Author> authors;
        private IGenericRepository<Publisher> publishers;
        private IGenericRepository<Review> reviews;
        private IGenericRepository<OrderDetail> orderDetail;
        private IGenericRepository<Order> order;
        private IGenericRepository<Promotion> promotion;
        private IGenericRepository<PromotionInfo> promotionInfo;
        private IGenericRepository<DiscountCode> discountCode;
        private IGenericRepository<Employee> employees;
        private IGenericRepository<AppUser> users;

        public UnitOfWork(DatabaseContext databaseContext)
        {
            context = databaseContext;
        }
        public IGenericRepository<Book> Books => books ??= new GenericRepository<Book>(context);
        public IGenericRepository<Genre> Genres => genres ??= new GenericRepository<Genre>(context);
        public IGenericRepository<Author> Authors => authors ??= new GenericRepository<Author>(context);
        public IGenericRepository<Publisher> Publishers => publishers ??= new GenericRepository<Publisher>(context);
        public IGenericRepository<Review> Reviews => reviews ??= new GenericRepository<Review>(context);
        public IGenericRepository<OrderDetail> OrderDetails => orderDetail ??= new GenericRepository<OrderDetail>(context);
        public IGenericRepository<Order> Orders => order ??= new GenericRepository<Order>(context);
        public IGenericRepository<Promotion> Promotions => promotion ??= new GenericRepository<Promotion>(context);
        public IGenericRepository<PromotionInfo> PromotionInfos => promotionInfo ??= new GenericRepository<PromotionInfo>(context);
        public IGenericRepository<DiscountCode> DiscountCodes => discountCode ??= new GenericRepository<DiscountCode>(context);

        public IGenericRepository<Employee> Employees => employees ??= new GenericRepository<Employee>(context);
        public IGenericRepository<AppUser> Users => users ??= new GenericRepository<AppUser>(context);
        public void Dispose()
        {
            context.Dispose();
            GC.SuppressFinalize(this);
        }
        public async Task Save()
        {
            await context.SaveChangesAsync();
        }
    }
}
