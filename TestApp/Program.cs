using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TestApp.EntityModels;

var builder = WebApplication.CreateBuilder(args);

var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .Build();

var connectionString = configuration.GetConnectionString("DefaultConnection");
var passwordFilePath = configuration.GetValue<string>("ConnectionStrings:PasswordFile");
string password = File.ReadAllText(passwordFilePath);
Console.WriteLine(connectionString);
Console.WriteLine(password);

// Add services to the container.
builder.Services.AddDbContext<sqldbContext>(options =>options.UseSqlServer(connectionString+";Password="+passwordFilePath));
builder.Services.AddHttpContextAccessor();
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseStaticFiles();

app.UseRouting();

app.MapDefaultControllerRoute();

app.Run();
