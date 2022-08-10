using DataAccess.Abstract;
using DataAccess.Concrete;
using Entities.Concrete.Dto_s;
using Entities.Concrete.MailConfiguration;
using Entities.Validators;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebApp_Store;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.


builder.Services.AddControllersWithViews()
    .AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<RegisterValidator>())
    .AddFluentValidation()
    .AddSessionStateTempDataProvider();


var conStrIdentity = builder.Configuration.GetConnectionString("conStrIdentity");
builder.Services.AddDbContext<AccountDbContext>(x => x.UseSqlServer(conStrIdentity));


builder.Services.AddSession();

builder.Services.Configure<MailSettings>(builder.Configuration.GetSection(nameof(MailSettings)));

builder.Services.AddTransient<IMailDal, MailDal>();


builder.Services.AddIdentity<Entities.Concrete.AppUser, IdentityRole>(x =>
{
    x.SignIn.RequireConfirmedPhoneNumber = false;
    x.SignIn.RequireConfirmedEmail = false;
    x.SignIn.RequireConfirmedAccount = false;
    x.Password.RequireNonAlphanumeric = false;
    x.Password.RequiredUniqueChars = 0;
    x.Password.RequireDigit = false;
    x.Password.RequireUppercase = false;
    x.Password.RequireLowercase = false;
    x.User.RequireUniqueEmail = true;
}).AddEntityFrameworkStores<AccountDbContext>().AddDefaultTokenProviders();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();


app.UseStaticFiles();

app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=home}/{action=index}");


app.Run();
