using Business.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete;
using Entities;
using Entities.Concrete;
using Entities.Concrete.Dto_s;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApp_Store.Models;

namespace WebApp_Store.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager; //Identity sınıfının bana sağladığı hazır managerların referanslarını oluşturdum.
        private readonly SignInManager<AppUser> _signInManager;
        private CustomerManager _customerManager;
        private BasketManager _basketManager;
        private RoleManager<IdentityRole> _roleManager;
        private IMailDal _mail;

        [TempData]
        public string Message { get; set; }


        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager,IMailDal mail)
        {
         
            _userManager = userManager;  //DependencyInjection ile managerların AccountController sınıfıma enjekte ettim.
            _signInManager = signInManager;
            _roleManager = roleManager;
            _customerManager = new CustomerManager(new CustomerDal());
            _basketManager = new BasketManager(new BasketDal());
            _mail = mail;
        }



        #region Register

        [HttpGet, AllowAnonymous]
        public IActionResult Register() => View(); //  Register için Get actionı oluşturdum.


        [HttpPost, ValidateAntiForgeryToken, AllowAnonymous]
        public async Task<IActionResult> Register(RegisterDto model) // Register için post action'ı oluşturdum.
        {
            if (ModelState.IsValid) //  FluentValidation ile kontrol edilen değerlerin çıktısı.
            {

                var appUser = new AppUser { UserName = model.UserName, Email = model.Mail };                                // yeni kullanıcı new'lenerek view'den alınan
                                                                                                                            // değerler propların üzerine atılır.
                                                                                                                            //_userManager da tanımlanmış hazır create
                IdentityResult result = await _userManager.CreateAsync(appUser, model.Password);                                                                                                      //metoduyla kullanıcı Identity sınıfının
                                                                                                                                                                                                      //oluşturduğu tablolara eklenir.
                if (result.Succeeded)
                {
                    bool check = await _customerManager.CheckUserAsync(appUser.UserName);
                    if (!check)
                    {
                        Customer cus = new Customer() { CustomerName = appUser.UserName };

                        if (cus != null)
                        {
                            _customerManager.AddCustomer(cus);
                        }

                        Basket basket = new Basket() { CustomerID = cus.CustomerId };

                        if (basket != null)
                        {
                            _basketManager.AddBasket(basket);
                        }

                    }

                    #region Role ekleme

                    List<IQueryable> roleList = new List<IQueryable>() { _roleManager.Roles };
                    int x = roleList.Count;
                    if (x == 0)
                    {
                        await _roleManager.CreateAsync(new IdentityRole() { Name = "User" });
                        await _roleManager.CreateAsync(new IdentityRole() { Name = "Manager" });
                        await _roleManager.CreateAsync(new IdentityRole() { Name = "Admin" });

                        await _userManager.AddToRoleAsync(appUser, "Manager");
                    }

                    await _userManager.AddToRoleAsync(appUser, "User");

                    #endregion



                    #region Mail gönderimi


                    MailDataDTO mmail = new MailDataDTO(new List<string>() { appUser.Email }, $"Mert Basar bitirme projesinde kullanıcı hesabınız oluşturuldu. Kullanıcı Adınız, {appUser.UserName}. Bu mail test amaçlıdır ve SendGrid smtp aracılığıyla gönderilmiştir..");

                    await _mail.SendAsync(mmail, new CancellationToken());

                    #endregion


                    return RedirectToAction("Login");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View(model);
        }
        #endregion

        //#region Session
        //[NonAction]
        //public void OnGet(string userName)
        //{
           
        //    if (string.IsNullOrEmpty(HttpContext.Session.GetString(SessionKeyName)))
        //    {
        //        HttpContext.Session.SetString(SessionKeyName, userName);
        //    }
        //}
        //#endregion

        [AllowAnonymous]
        public IActionResult Login(string returnUrl)
        {
            return View(new LoginDto { ReturnUrl = returnUrl });
        }


        [AllowAnonymous,ValidateAntiForgeryToken,HttpPost]
        public async Task<IActionResult> Login(LoginDto model)
        {
            if (ModelState.IsValid)
            {
                AppUser user = await _userManager.FindByNameAsync(model.UserName);
                if (user != null)
                {
                Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(user.UserName, model.Password, false, false);

                    if (result.Succeeded)
                    {
                        var check = await _customerManager.CheckUserAsync(user.UserName);
                        if (!check)
                        {
                            Customer cus = new Customer() { CustomerName = user.UserName };
                            if (cus != null)
                            {
                                _customerManager.AddCustomer(cus);
                            }

                            Basket basket = new Basket() { CustomerID = cus.CustomerId };

                            if (basket != null)
                            {
                                _basketManager.AddBasket(basket);
                            }
                        }
                        else if (check)
                        {
                            Basket basket = await _basketManager.GetBasketByCustomerName(user.UserName);
                            if (basket == null)
                            {
                                Customer custo = await _customerManager.GetCustomerByName(user.UserName);
                                _basketManager.AddBasket(new Basket() { CustomerID = custo.CustomerId});
                            }
                        }
                        
                        Message = user.UserName;

                        return Redirect(model.ReturnUrl ?? "/");
                    }
                    else
                    {
                        ModelState.AddModelError("", "hata");
                    }
                }
            }
            return View(model);
         
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("login","account"); 
        }
    }
}
