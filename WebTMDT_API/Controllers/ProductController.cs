using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using WebTMDT_API.Data;
using WebTMDTLibrary.DTO;
using WebTMDTLibrary.Helper;
using WebTMDT_API.Repository;
using WebTMDT_API.Helper;

namespace WebTMDT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public ProductController(IUnitOfWork _unitOfWork, IMapper _mapper)
        {
            unitOfWork = _unitOfWork;
            mapper = _mapper;
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookById(int id)
        {
            try
            {
                var book = await unitOfWork.Books.Get(q => q.Id == id, new List<string> { "Authors", "Genres", "Publisher", "PromotionInfo" });
                if (book == null)
                {
                    return Ok(new { success = false, msg = "Không tìm thấy sản phẩm" });
                }
                var result = mapper.Map<BookDTO>(book);
                var rev = await unitOfWork.Reviews.GetAll(q => q.BookId == id, q => q.OrderBy(r => r.Date), new List<string> { "User" });
                var reviews = mapper.Map<IList<ReviewDTO>>(rev);
                return Ok(new { result = result, success = true, reviews = reviews });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.ToString() });
            }
        }

        [HttpGet("search")]
        public async Task<IActionResult> GetBooks(int pageNumber, int pageSize, string? keyword = null, string? priceRange = null, string? genreFilter = null)
        {

            Expression<Func<Book, bool>> expression = q => true;

            try
            {
                if (keyword != null)
                {
                    Expression<Func<Book, bool>> expression_keyword = q => q.Title.Contains(keyword);
                    expression = expression.AndAlso(expression_keyword);

                }
                if (priceRange != null)
                {
                    string[] pr = priceRange.Split(',');
                    Expression<Func<Book, bool>> expression_priceRange = q => q.Price > int.Parse(pr[0]) && q.Price < int.Parse(pr[1]);
                    expression = expression.AndAlso(expression_priceRange);
                }
                if (genreFilter != null)
                {
                    string[] listGenre = genreFilter.Split(",");
                    Expression<Func<Book, bool>> expression_genre = q => q.Genres.Any(genre => listGenre.ToList().Contains(genre.Name));
                    expression = expression.AndAlso(expression_genre);
                }


                var books = await unitOfWork.Books.GetAll(expression, q => q.OrderBy(book => book.Id),
                    new List<string> { "Authors", "Genres", "Publisher", "PromotionInfo" }, new PaginationFilter(pageNumber, pageSize));
                var count = await unitOfWork.Books.GetCount(expression);
                var result = mapper.Map<IList<BookDTO>>(books);

                return Ok(new { result = result, totalPage = (int)Math.Ceiling((double)count / pageSize) });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpGet("{id}/related")]
        public async Task<IActionResult> GetRelatedBook(int id, int numberOfBook)
        {
            try
            {
                var book = await unitOfWork.Books.Get(q => q.Id == id, new List<string> { "Authors", "Genres", "Publisher" });
                if (book == null)
                {
                    return Ok(new { success = false, msg = "Không tìm thấy sản phẩm" });
                }

                Expression<Func<Book, bool>> expression =
                    q =>
                    (q.Authors.Any(author => book.Authors.Contains(author))
                || q.Genres.Any(genre => book.Genres.Contains(genre))
                || q.Publisher == book.Publisher)
                && q.Id != id;

                var relatedBook = await unitOfWork.Books.GetAll(expression, q => q.OrderBy(book => Guid.NewGuid()), new List<string>() { "Authors", "Genres", "Publisher", "PromotionInfo" }, new PaginationFilter(1, numberOfBook));
                if (relatedBook.Count < numberOfBook)
                {
                    Expression<Func<Book, bool>> expression_revert =
                        q =>
                    !(q.Authors.Any(author => book.Authors.Contains(author))
                    || q.Genres.Any(genre => book.Genres.Contains(genre))
                    || q.Publisher == book.Publisher)
                    && q.Id != id;
                    var randomBooks = await unitOfWork.Books.GetAll(expression_revert, q => q.OrderBy(book => Guid.NewGuid()),
                        new List<string>() { "PromotionInfo" },
                        new PaginationFilter(1, numberOfBook - relatedBook.Count));
                    relatedBook = relatedBook.Concat(randomBooks).ToList();
                }
                var result = mapper.Map<IList<BookDTO>>(relatedBook);


                return Ok(new { result });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.ToString() });
            }
        }

        [HttpGet("getRandomProduct")]
        public async Task<IActionResult> GetRandomBook(int numberOfBook)
        {
            try
            {

                var randomBooks = await unitOfWork.Books.GetAll(null, q => q.OrderBy(book => Guid.NewGuid()), new List<string>() { "Authors", "Genres", "Publisher", "PromotionInfo" }, new PaginationFilter(1, numberOfBook));
                var result = mapper.Map<IList<BookDTO>>(randomBooks);

                return Ok(new { result });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.ToString() });
            }
        }

        [HttpGet("getPopularProduct")]
        public async Task<IActionResult> GetPopularProduct(int numberOfBook)
        {
            try
            {
                var orderDetails = await unitOfWork.OrderDetails.GetAll(q => q.Order.Status == (int)OrderStatus.Done, null, new List<string> { "Book" });
                Dictionary<int, int> dic = new Dictionary<int, int>();
                foreach (var od in orderDetails)
                {
                    if (!dic.ContainsKey(od.BookId))
                    {
                        dic.Add(od.BookId, od.Quantity);
                    }
                    else
                    {
                        dic[od.BookId] += od.Quantity;
                    }
                }
                List<PopularBookDTO> result = new List<PopularBookDTO>();
                foreach (var item in dic)
                {
                    var od = orderDetails.Where(q => q.BookId == item.Key).FirstOrDefault();
                    var rs = new PopularBookDTO() { Book = mapper.Map<BookDTO>(od.Book), Sales = item.Value };
                    result.Add(rs);
                }

                result.Sort((a, b) => b.Sales - a.Sales); //Desc
                result = result.Take(numberOfBook).ToList();

                foreach (var item in result)
                {
                    var temp = await unitOfWork.Books.Get(q => q.Id == item.Book.Id, new List<string> { "Authors", "Genres", "Publisher", "PromotionInfo" });
                    item.Book = mapper.Map<BookDTO>(temp);
                }


                return Ok(new { result }); ;
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.ToString() });
            }
        }

        [HttpGet("getLatestBook")]
        public async Task<IActionResult> GetLatestBook(int number)
        {
            try
            {
                var books = await unitOfWork.Books.GetAll(q => true, q => q.OrderByDescending(p => p.Id), new List<string> { "Authors", "Genres", "Publisher", "PromotionInfo" }, new PaginationFilter(1, number));

                return Ok(new { success = true, result = books });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.ToString() });
            }
        }


        [HttpPost]
        public async Task<IActionResult> PostBook(CreateBookDTO bookDTO)
        {
            if (!ModelState.IsValid)
            {
                return Ok(new { error = "Dữ liệu chưa hợp lệ", success = false });
            }
            try
            {
                var newBook = new Book();
                newBook.Title = bookDTO.Title;
                newBook.Description = bookDTO.Description;
                newBook.imgUrl = bookDTO.imgUrl;
                newBook.Price = bookDTO.Price;
                newBook.NumberOfPage = bookDTO.NumberOfPage;
                newBook.PublishYear = bookDTO.PublishYear;
                newBook.Authors = new List<Author>();
                newBook.Genres = new List<Genre>();

                var publisher = await unitOfWork.Publishers.Get(q => q.Name == bookDTO.PublisherName);
                if (publisher == null)
                {
                    publisher = new Publisher() { Name = bookDTO.PublisherName };
                    newBook.Publisher = publisher;
                }
                else
                {
                    newBook.PublisherId = publisher.Id;
                }

                await unitOfWork.Books.Insert(newBook);
                await unitOfWork.Save();

                foreach (var authorName in bookDTO.Authors)
                {
                    var author = await unitOfWork.Authors.Get(q => q.Name == authorName);
                    if (author == null)
                    {
                        author = new Author() { Name = authorName };
                    }
                    newBook.Authors.Add(author);

                }

                foreach (var item in bookDTO.Genres)
                {
                    var genre = await unitOfWork.Genres.Get(q => q.Name == item);
                    if (genre == null)
                    {
                        genre = new Genre() { Name = item };
                    }
                    newBook.Genres.Add(genre);
                }


                await unitOfWork.Save();
                return Ok(new { book = newBook, success = true });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.ToString(), success = false });
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBook([FromBody] CreateBookDTO bookDTO, int id)
        {
            if (!ModelState.IsValid)
            {
                return Ok(new { error = "Dữ liệu chưa hợp lệ", success = false });
            }
            try
            {
                var book = await unitOfWork.Books.Get(q => q.Id == id, new List<string> { "Authors", "Genres", "Publisher" });
                if (book == null)
                {
                    return Ok(new { error = "Không tìm thấy sản phẩm", success = false });
                }
                book.Title = bookDTO.Title;
                book.Description = bookDTO.Description;
                book.imgUrl = bookDTO.imgUrl;
                book.Price = bookDTO.Price;
                book.NumberOfPage = bookDTO.NumberOfPage;
                book.PublishYear = bookDTO.PublishYear;
                //loại các author ko còn trong danh sách
                List<Author> authorListToDelete = new List<Author>();
                foreach (var author in book.Authors)
                {
                    bool match = false;
                    foreach (var item in bookDTO.Authors)
                    {
                        if (author.Name == item)
                        {
                            match = true;
                            break;
                        }
                    }
                    if (!match)
                    {
                        authorListToDelete.Add(author);
                    }
                }
                foreach (var author in book.Authors.ToList())
                {
                    if (authorListToDelete.Contains(author))
                    {
                        Console.WriteLine("Remove : " + author.Name);
                        unitOfWork.Authors.Update(author);
                        book.Authors.Remove(author);
                    }
                }


                //thêm các author mới
                List<Author> authorListToAdd = new List<Author>();
                foreach (var item in bookDTO.Authors)
                {
                    bool match = false;
                    foreach (var author in book.Authors.ToList())
                    {
                        if (item == author.Name)
                        {
                            match = true;
                            break;
                        }
                    }
                    if (!match)
                    {
                        authorListToAdd.Add(new Author() { Name = item });
                    }
                }
                foreach (var author in authorListToAdd)
                {
                    book.Authors.Add(author);
                }


                //update publisher
                if (book.Publisher.Name != bookDTO.PublisherName)
                {
                    var publisher = await unitOfWork.Publishers.Get(q => q.Name == bookDTO.PublisherName);
                    if (publisher != null)
                    {
                        unitOfWork.Publishers.Update(publisher);
                        book.Publisher = publisher;
                    }
                    else
                    {
                        var newPublisher = new Publisher() { Name = bookDTO.PublisherName };
                        book.Publisher = newPublisher;
                    }
                }

                //loại các genre ko còn trong danh sách
                List<Genre> genreListToDelete = new List<Genre>();
                foreach (var genre in book.Genres)
                {
                    bool match = false;
                    foreach (var item in bookDTO.Genres)
                    {
                        if (genre.Name == item)
                        {
                            match = true;
                            break;
                        }
                    }
                    if (!match)
                    {
                        genreListToDelete.Add(genre);
                    }
                }
                foreach (var genre in book.Genres.ToList())
                {
                    if (genreListToDelete.Contains(genre))
                    {
                        unitOfWork.Genres.Update(genre);
                        book.Genres.Remove(genre);
                    }
                }

                //thêm các genre mới
                List<Genre> genreListToAdd = new List<Genre>();
                foreach (var item in bookDTO.Genres)
                {
                    bool match = false;
                    foreach (var genre in book.Genres.ToList())
                    {
                        if (item == genre.Name)
                        {
                            match = true;
                            break;
                        }
                    }
                    if (!match)
                    {
                        genreListToAdd.Add(new Genre() { Name = item });
                    }
                }
                foreach (var genre in genreListToAdd)
                {
                    book.Genres.Add(genre);
                }
                unitOfWork.Books.Update(book);
                await unitOfWork.Save();
                book = await unitOfWork.Books.Get(q => q.Id == id, new List<string> { "Authors", "Genres", "Publisher" });
                var result = mapper.Map<BookDTO>(book);
                return Ok(new { result = result, success = true });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }

        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            try
            {
                var book = await unitOfWork.Books.Get(q => q.Id == id, new List<string> { "Authors", "Genres", "Publisher" });
                if (book == null)
                {
                    return Ok(new { success = false, msg = "Không tìm thấy sản phẩm" });
                }
                await unitOfWork.Books.Delete(book.Id);
                await unitOfWork.Save();
                return Ok(new { success = true });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.ToString() });
            }
        }


    }
}
