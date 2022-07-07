using DataAccess;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebApp_Account;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews().AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<Entities.Validators.RegisterValidator>());

string conStr = builder.Configuration.GetConnectionString("conStr");
builder.Services.AddDbContext<AccountDbContext>(opt => opt.UseSqlServer(conStr));

string conStr = builder.Configuration.GetConnectionString("conStr");
builder.Services.AddDbContext<StoreDbContext>(opt => opt.UseSqlServer(conStr));

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

app.UseAuthorization();

app.UseStaticFiles();

app.UseRouting();

app.MapControllerRoute(
    name: "accountApp",
    pattern: "{controller=Account}/{action=Register}/{id?}");

app.Run();
