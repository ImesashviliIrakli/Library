using Library.UI.Interfaces;
using Library.UI.Services;
using Library.UI.Utility;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpClient();
builder.Services.AddHttpClient<IAccountService, AccountService>();
builder.Services.AddHttpClient<IAuthorService, AuthorService>();
builder.Services.AddHttpClient<IBookService, BookService>();

builder.Services.AddScoped<IBaseService, BaseService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<ITokenProvider, TokenProvider>();
builder.Services.AddScoped<IAuthorService, AuthorService>();
builder.Services.AddScoped<IBookService, BookService>();

// Authentication
builder.Services.AddAuthentication("LibraryCookie")
    .AddCookie("LibraryCookie", options =>
    {
        options.Cookie.Name = "LibraryAuthCookie";
        options.ExpireTimeSpan = TimeSpan.FromHours(10);
        options.LoginPath = "/Auth/Login";
        options.AccessDeniedPath = "/Auth/AccessDenied";
    });

SD.LibraryAPIBase = builder.Configuration["ServiceUrls:LibraryAPI"];

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
