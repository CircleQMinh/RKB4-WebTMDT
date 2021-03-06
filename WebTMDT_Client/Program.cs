using WebTMDT_Client.Mapper;
using WebTMDT_Client.Service;
using WebTMDTLibrary.Helper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddTransient<IProductService, ProductService>();
builder.Services.AddTransient<IAccountService, AccountService>();
builder.Services.AddTransient<IReviewService, ReviewService>();
builder.Services.AddTransient<IGenreService, GenreService>();
builder.Services.AddTransient<IOrderService, OrderService>();

builder.Services.AddSingleton<IConfiguration>(builder.Configuration);

builder.Services.AddAutoMapper(typeof(AutoMapperSetting));

builder.Services.AddSession(options => {
    options.IdleTimeout = TimeSpan.FromMinutes(120);//You can set Time   
});
builder.Services.AddHttpContextAccessor();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.UseSession();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(name: "product",
                    pattern: "product/{id?}",
                    defaults: new { controller = "Product", action = "Index" });
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
    endpoints.MapRazorPages();
});


app.Run();
