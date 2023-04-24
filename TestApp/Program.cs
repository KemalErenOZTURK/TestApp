using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;
using TestApp.EntityModels;

var builder = WebApplication.CreateBuilder(args);

//var configuration = new ConfigurationBuilder().AddKeyPerFile(directoryPath: "/run/secrets", optional: true)
//    .Build();

//var connectionString = configuration.GetConnectionString("DefaultConnection");
//var passwordFilePath = configuration.GetValue<string>("ConnectionStrings:PasswordFile");
////string password = File.ReadAllText(passwordFilePath);
//Console.WriteLine("DC: "+connectionString);
//Console.WriteLine("PF: "+passwordFilePath);

var configuration = new ConfigurationBuilder()
    .AddEnvironmentVariables().Build();

var defaultConnection = configuration.GetSection("ConnectionStrings")["DefaultConnection"];
Console.WriteLine("DF: " + defaultConnection);

var passFile = configuration.GetSection("ConnectionStrings")["PasswordFile"];
var asdad = "/run/secrets/sec_db_pass";
string password = File.ReadAllText(passFile);
Console.WriteLine("Password: " + password);


// Add services to the container.
builder.Services.AddDbContext<sqldbContext>(options =>options.UseSqlServer(defaultConnection + ";Password="+ password));
builder.Services.AddHttpContextAccessor();
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseStaticFiles();

app.UseRouting();

app.MapDefaultControllerRoute();

app.Run();
