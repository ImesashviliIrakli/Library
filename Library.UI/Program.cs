using Library.UI.Interfaces;
using Library.UI.Services;
using Library.UI.Utility;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();

builder.Services.AddHttpClient("LibraryAPI")
    .AddHttpMessageHandler<TokenExpirationHandler>();
builder.Services.AddTransient<TokenExpirationHandler>();

builder.Services.AddHttpClient<IAccountService, AccountService>();
builder.Services.AddHttpClient<IAuthorService, AuthorService>();
builder.Services.AddHttpClient<IBookService, BookService>();

builder.Services.AddScoped<IBaseService, BaseService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<ITokenProvider, TokenProvider>();
builder.Services.AddScoped<IAuthorService, AuthorService>();
builder.Services.AddScoped<IBookService, BookService>();

// Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = "LibraryCookie";
    options.DefaultChallengeScheme = "LibraryCookie";
}).AddCookie("LibraryCookie", options =>
{
    options.LoginPath = "/Account/Login";
    options.LogoutPath = "/Account/Logout";
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
