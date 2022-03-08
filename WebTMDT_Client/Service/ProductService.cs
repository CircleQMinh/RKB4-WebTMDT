using Newtonsoft.Json;
using WebTMDTLibrary.DTO;
using WebTMDT_Client.ResponseModel;
using WebTMDT_Client.ViewModel;
using WebTMDTLibrary.Hepler;

namespace WebTMDT_Client.Service
{
    public class ProductService : IProductService
    {
        private readonly IConfiguration Configuration;
        public ProductService(IConfiguration _configuration)
        {
            this.Configuration = _configuration;
        }
        public async Task<ProductListViewModel> GetProductListViewModel(ProductListFilterModel model)
        {
            ProductListViewModel books = new ProductListViewModel();
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Configuration["Setting:API_URL"]);
                    string url = $"{Configuration["Setting:API_ENDPOINT:Product:GetProduct"]}?pageNumber={model.pageNumber}&pageSize={model.pageSize}";
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
                    var result = await responseTask;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsStringAsync();
 
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

        public async Task<ProductViewViewModel> GetProductViewViewModel(ProductListFilterModel model)
        {
            BooksDeserialize books = new BooksDeserialize();
            GenresDeserialize genres = new GenresDeserialize();

            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Configuration["Setting:API_URL"]);
                    string url = $"{Configuration["Setting:API_ENDPOINT:Product:GetProduct"]}?pageNumber={model.pageNumber}&pageSize={model.pageSize}";
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

                    var result = await responseTask;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsStringAsync();
  
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
                    client.BaseAddress = new Uri(Configuration["Setting:API_URL"]);
                    var url = $"genre";
                    var responseTask = client.GetAsync(url);

                    var result = await responseTask;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsStringAsync();
                        var data = readTask.Result;
 
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


       public async Task<ProductDetailViewModel> GetProductDetailViewModel(int id)
       {
            ProductDetailViewModel model = new ProductDetailViewModel();
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Configuration["Setting:API_URL"]);
                    var url = $"{Configuration["Setting:API_ENDPOINT:Product:GetProductDetail"]}{id}";
                    var responseTask = client.GetAsync(url);

                    var result = await responseTask;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsStringAsync();

                        var data = readTask.Result;

                        model = JsonConvert.DeserializeObject<ProductDetailViewModel>(data);
                    }
                    else //web api sent error response 
                    {
                        Console.WriteLine(result.StatusCode);
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return model;
       }

        public async Task<ProductDisplayViewModel> GetLatestProduct()
        {
            ProductDisplayViewModel model = new ProductDisplayViewModel();
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Configuration["Setting:API_URL"]);
                    var url = $"{Configuration["Setting:API_ENDPOINT:Product:GetProductLatest"]}?numberOfBook={ProjectConst.NumberOfProductDisplay}";
                    var responseTask = client.GetAsync(url);

                    var result = await responseTask;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsStringAsync();

                        var data = readTask.Result;

                        model = JsonConvert.DeserializeObject<ProductDisplayViewModel>(data);
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
            return model;
        }
        public async Task<ProductDisplayViewModel> GetSuggestProduct()
        {
            ProductDisplayViewModel model = new ProductDisplayViewModel();
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Configuration["Setting:API_URL"]);
                    var url = $"{Configuration["Setting:API_ENDPOINT:Product:GetProductSuggest"]}?numberOfBook={ProjectConst.NumberOfProductDisplay}";
                    var responseTask = client.GetAsync(url);

                    var result = await responseTask;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsStringAsync();

                        var data = readTask.Result;

                        model = JsonConvert.DeserializeObject<ProductDisplayViewModel>(data);
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
            return model;
        }

        public async Task<ProductDisplayViewModel> GetPoppularProduct()
        {
            ProductDisplayViewModel model = new ProductDisplayViewModel();
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Configuration["Setting:API_URL"]);
                    var url = $"{Configuration["Setting:API_ENDPOINT:Product:GetProductPopular"]}?numberOfBook={ProjectConst.NumberOfProductDisplay}";
                    var responseTask = client.GetAsync(url);

                    var result = await responseTask;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsStringAsync();

                        var data = readTask.Result;

                        model = JsonConvert.DeserializeObject<ProductDisplayViewModel>(data);
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
            return model;
        }

        public async Task<ProductDisplayViewModel> GetRelatedProduct(int id)
        {
            ProductDisplayViewModel model = new ProductDisplayViewModel();
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Configuration["Setting:API_URL"]);
                    var url = $"{Configuration["Setting:API_ENDPOINT:Product:Base"]}" +
                                $"/{id}" +
                    $"{Configuration["Setting:API_ENDPOINT:Product:GetProductRelated"]}?numberOfBook={ProjectConst.NumberOfProductDisplay}";

                    Console.WriteLine(url);
                    var responseTask = client.GetAsync(url);

                    var result = await responseTask;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsStringAsync();

                        var data = readTask.Result;

                        model = JsonConvert.DeserializeObject<ProductDisplayViewModel>(data);
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
            return model;
        }
    }

}
