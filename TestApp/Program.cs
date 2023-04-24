using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;
using TestApp.EntityModels;

var builder = WebApplication.CreateBuilder(args);


var configuration = new ConfigurationBuilder()
    .AddEnvironmentVariables().Build();

var defaultConnection = configuration.GetSection("ConnectionStrings")["DefaultConnection"];
var passFile = configuration.GetSection("ConnectionStrings")["PasswordFile"];
string password = File.ReadAllText(passFile);

// Add services to the container.
builder.Services.AddDbContext<sqldbContext>(options => options.UseSqlServer(defaultConnection + ";Password=" + password));
builder.Services.AddHttpContextAccessor();
builder.Services.AddControllersWithViews();
builder.Services.AddStackExchangeRedisCache(opt => opt.Configuration = "s_redis:6379");

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseStaticFiles();

app.UseRouting();

app.MapDefaultControllerRoute();

app.Run();
