using DataAccess;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebApp_Store;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews().AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<Entities.Validators.LoginValidator>());

var conStrIdentity = builder.Configuration.GetConnectionString("conStrIdentity");
builder.Services.AddDbContext<AccountDbContext>(x => x.UseSqlServer(conStrIdentity));



builder.Services.AddIdentity<Entities.Concrete.AppUser, IdentityRole>(x =>
{
    x.SignIn.RequireConfirmedPhoneNumber = false;
    x.SignIn.RequireConfirmedEmail = false;
    x.SignIn.RequireConfirmedAccount = false;
    x.Password.RequiredLength = 5;
    x.Password.RequireNonAlphanumeric = false;
    x.Password.RequiredUniqueChars = 0;
    x.Password.RequireDigit = false;
    x.Password.RequireUppercase = false;
    x.Password.RequireLowercase = false;
    x.User.RequireUniqueEmail = true;
}).AddEntityFrameworkStores<AccountDbContext>().AddDefaultTokenProviders();

var app = builder.Build();

// Configure the HTTP request pipeline.


app.UseAuthentication();

app.UseRouting();

app.UseAuthorization();

app.UseStaticFiles();


app.MapControllerRoute(
    name: "storeApp",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
