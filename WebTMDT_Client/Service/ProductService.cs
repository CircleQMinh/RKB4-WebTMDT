using Newtonsoft.Json;
using WebTMDTLibrary.DTO;
using WebTMDT_Client.ViewModel;

namespace WebTMDT_Client.Service
{
    public class ProductService : IProductService
    {
        public ProductListViewModel GetProductListViewModel(ProductListFilterModel model)
        {
            ProductListViewModel books = new ProductListViewModel();
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:7099/api/");
                    string url = $"Product/search?pageNumber={model.pageNumber}&pageSize={model.pageSize}";
                    if (model.keyword != null)
                    {
                        url += $"&keyword={model.keyword}";
                    }
                    if (model.genreFilter != null)
                    {
                        url += $"&genreFilter={model.genreFilter}";
                    }
                    if (model.priceFilter != null)
                    {
                        url += $"&priceRange={model.priceFilter}";
                    }
                    Console.WriteLine(url);
                    //HTTP GET
                    var responseTask = client.GetAsync(url);
                    responseTask.Wait();

                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsStringAsync();
                        readTask.Wait();
                        var data = readTask.Result;
                        books = JsonConvert.DeserializeObject<ProductListViewModel>(data);
                    }
                    else //web api sent error response 
                    {
                        Console.WriteLine(result.StatusCode);
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return books;
        }

        public ProductViewViewModel GetProductViewViewModel(ProductListFilterModel model)
        {
            BooksDeserialize books = new BooksDeserialize();
            GenresDeserialize genres = new GenresDeserialize();

            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:7099/api/");
                    string url = $"Product/search?pageNumber={model.pageNumber}&pageSize={model.pageSize}";
                    if (model.keyword != null)
                    {
                        url += $"&keyword={model.keyword}";
                    }
                    if (model.genreFilter != null)
                    {
                        url += $"&genreFilter={model.genreFilter}";
                    }
                    if (model.priceFilter != null)
                    {
                        url += $"&priceFilter={model.priceFilter}";
                    }
                    Console.WriteLine(url);
                    //HTTP GET
                    var responseTask = client.GetAsync(url);
                    responseTask.Wait();

                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsStringAsync();
                        readTask.Wait();
                        var data = readTask.Result;
                        books = JsonConvert.DeserializeObject<BooksDeserialize>(data);
                    }
                    else //web api sent error response 
                    {
                        Console.WriteLine(result.StatusCode);
                    }
                }
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:7099/api/");
                    var url = $"genre";
                    var responseTask = client.GetAsync(url);
                    responseTask.Wait();

                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsStringAsync();
                        readTask.Wait();
                        var data = readTask.Result;
                       // Console.WriteLine(data);
                        genres = JsonConvert.DeserializeObject<GenresDeserialize>(data);
                    }
                    else //web api sent error response 
                    {
                        Console.WriteLine(result.StatusCode);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return new ProductViewViewModel() { Books = books, Genres = genres, pageNumber = model.pageNumber, pageSize = model.pageSize };
        }
    }
}
