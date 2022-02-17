
using Microsoft.AspNetCore.Mvc;
using WebTMDT_Client.DTO;

namespace WebTMDT.Views.Shared.Components.ProductList
{
    [ViewComponent]
    public class ProductList : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var rs = "";
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7099/api/");
                //HTTP GET
                var responseTask = client.GetAsync("Product/search?pageNumber=1&pageSize=4");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();

                    rs = readTask.Result;
                }
                else //web api sent error response 
                {
                    //log response status here..

                    rs = "null";

                    Console.WriteLine(result.StatusCode);
                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();
                    Console.WriteLine(readTask.Result);

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }

            return View("ProductList",rs);
        }

    }
}
