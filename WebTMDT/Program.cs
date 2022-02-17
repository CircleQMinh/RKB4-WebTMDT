﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebTMDT.Data;
using WebTMDT.Helper;
using WebTMDT.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
//cors
builder.Services.AddCors(o =>
{
    o.AddPolicy("AllowAll", p =>
    {
        p.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});

//configure dbcontext
builder.Services.AddDbContext<DatabaseContext>(options =>
    options.UseSqlServer(builder.Configuration["ConnectionStrings:sqlConnection"])
);
//configure unitofwork
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();

//configure identity

builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
{
    // Thiết lập về Password
    options.Password.RequireDigit = false; // Không bắt phải có số
    options.Password.RequireLowercase = false; // Không bắt phải có chữ thường
    options.Password.RequireNonAlphanumeric = false; // Không bắt ký tự đặc biệt
    options.Password.RequireUppercase = false; // Không bắt buộc chữ in
    options.Password.RequiredLength = 3; // Số ký tự tối thiểu của password
    options.Password.RequiredUniqueChars = 1; // Số ký tự riêng biệt

    // Cấu hình Lockout - khóa user
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5); // Khóa 5 phút
    options.Lockout.MaxFailedAccessAttempts = 5; // Thất bại 5 lầ thì khóa
    options.Lockout.AllowedForNewUsers = true;

    // Cấu hình về User.
    options.User.AllowedUserNameCharacters = // các ký tự đặt tên user
        "abcdeghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZđĐáàảãạăắằẳẵặâấầẩẫậưứừửữựơớờởỡợôốồổỗộêếềểễệíìỉĩịýỳỷỹỵúùủũụóòỏõọÁÀẢÃẠĂẮẰẲẴẶÂẤẦẨẪẬƯỨỪỬỮỰƠỚỜỞỠỢÔỐỒỔỖỘÊẾỀỂỄỆÍÌỈĨỊÝỲỶỸỴÚÙỦŨỤÓÒỎÕỌ0123456789-._@+ ";
    options.User.RequireUniqueEmail = true;  // Email là duy nhất

    // Cấu hình đăng nhập.
    options.SignIn.RequireConfirmedEmail = true;            // Cấu hình xác thực địa chỉ email (email phải tồn tại)
    options.SignIn.RequireConfirmedPhoneNumber = false;     // Xác thực số điện thoại
})
.AddEntityFrameworkStores<DatabaseContext>()
.AddDefaultTokenProviders();

//configure automapper 
builder.Services.AddAutoMapper(typeof(AutoMapperSetting));


builder.Services.AddControllers().AddNewtonsoftJson(
    op => op.SerializerSettings.ReferenceLoopHandling
    = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{

}
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseCors("AllowAll");
app.UseAuthentication();

app.UseAuthorization();

app.UseStaticFiles();
app.UseDefaultFiles();

app.MapControllers();

app.Run();