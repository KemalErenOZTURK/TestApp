using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;
using TestApp.EntityModels;

var builder = WebApplication.CreateBuilder(args);

var configuration = new ConfigurationBuilder().AddKeyPerFile(directoryPath: "/run/secrets", optional: true)
    .Build();

var connectionString = configuration.GetConnectionString("DefaultConnection");
var passwordFilePath = configuration.GetValue<string>("ConnectionStrings:PasswordFile");
//string password = File.ReadAllText(passwordFilePath);
Console.WriteLine("DC: "+connectionString);
Console.WriteLine("PF: "+passwordFilePath);


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
