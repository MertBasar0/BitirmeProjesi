using Business.Concrete;
using DataAccess.Concrete;
using Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace WebApp_Store.Controllers
{
    [Authorize(Roles = "User,Admin,Manager")]
    public class BasketController : Controller
    {
        private BasketManager _basketManager;

        public BasketController(UserManager<AppUser> signInManager)
        {
            _basketManager = new BasketManager (new BasketDal());
        }


        //public void asgas()
        //{
        //    _basketManager.
        //}

    }
}
