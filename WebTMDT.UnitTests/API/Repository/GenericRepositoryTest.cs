using Microsoft.EntityFrameworkCore;
using Moq;
using System.Linq.Expressions;
using WebTMDT_API.Data;
using WebTMDT_API.Repository;
using Xunit;
using WebTMDT.UnitTests.MockHepler;
using System.Data.Entity.Infrastructure;
using MockQueryable.Moq;

namespace WebTMDT.UnitTests.API.Repository
{
    public class GenericRepositoryTest
    {
        private readonly IGenericRepository<Book> genericRepository;
        private readonly Mock<DatabaseContext> mockDatabaseContext;
        private readonly Mock<DbSet<Book>> mockDBSet;
        public GenericRepositoryTest()
        {

        }

        [Fact]
        public async void GenericRepositoryGet_ValidInput_ReturnList()
        {

            //arrange


            Expression<Func<Book, bool>> expression = q => q.Id == 1;


            var data = new List<Book>
            {
                new Book { Title = "BBB",Id=1 },
                new Book { Title = "ZZZ",Id=2 },
                new Book { Title = "AAA",Id=3 },
            };

            var mockSet = data.AsQueryable().BuildMockDbSet();
            //sử dụng MockQueryable.Moq và MockQueryable.EntityFrameworkCore


            var mockContext = new Mock<DatabaseContext>();
            mockContext.Setup(c => c.Set<Book>()).Returns(mockSet.Object);


            var genericRepository = new GenericRepository<Book>(mockContext.Object);
            //act

            var result = await genericRepository.Get(expression, null);

            //assert

            Assert.NotNull(result);
            Assert.Equal("BBB",result.Title);
            Assert.Equal(1,result.Id);

            Console.WriteLine(result);


        }
    }
}
