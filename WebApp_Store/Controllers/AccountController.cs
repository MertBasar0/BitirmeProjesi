using Entities.Concrete;
using Entities.Concrete.Dto_s;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace WebApp_Store.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager; //Identity sınıfının bana sağladığı hazır managerların referanslarını oluşturdum.
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IPasswordHasher<AppUser> _passwordHasher;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IPasswordHasher<AppUser> passwordHasher)
        {
            _userManager = userManager;  //DependencyInjection ile managerların AccountController sınıfıma enjekte ettim.
            _signInManager = signInManager;
            _passwordHasher = passwordHasher;
        }

        #region Register

        [HttpGet,AllowAnonymous]
        public IActionResult Register() => View(); //  Register action'ım için Get actionı oluşturdum.


        [HttpPost, ValidateAntiForgeryToken, AllowAnonymous]
        public async Task<IActionResult> Register(RegisterDto model) // Register için post action'ı oluşturdum.
        {
            if (ModelState.IsValid) //  FluentValidation ile kontrol edilen değerlerin kontrolü.
            {
                
                var appUser  = new AppUser { UserName = model.UserName, Email = model.Mail};                                // yeni kullanıcı new'lenerek view'den alınan
                                                                                                                            // değerler propların üzerine atılır.
                                                                                                                            //_userManager da tanımlanmış hazır create
                IdentityResult result = await _userManager.CreateAsync(appUser, model.Password);                            //metoduyla kullanıcı Identity sınıfının
                                                                                                                            //oluşturduğu tablolara eklenir.
                if (result.Succeeded)
                {
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

        #region Login
        [AllowAnonymous,HttpGet]
        public IActionResult Login(string returnUrl)
        {
            return View(new LoginDto() { ReturnUrl = returnUrl });
        }

        [AllowAnonymous,ValidateAntiForgeryToken,HttpPost]
        public async Task<IActionResult> Login(LoginDto model)
        {
            //if (ModelState.IsValid)
            //{
                AppUser user = await _userManager.FindByNameAsync(model.UserName);
                if (user != null)
                {
                Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(user.UserName, model.Password, false, false);

                    if (result.Succeeded)
                    {
                        return Redirect(model.ReturnUrl ?? "/");
                    }
                    else
                    {
                        ModelState.AddModelError("", "hata");
                    }
                }
            //}
            return View(model);
            #endregion
        }
    }
}
