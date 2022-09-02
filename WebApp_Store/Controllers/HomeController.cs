using DataAccess.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApp_Store.Models;

namespace WebApp_Store.Controllers
{
    [Authorize(Roles = "User,Admin,Manager")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IMailDal _mail;

        public HomeController(ILogger<HomeController> logger, IMailDal mail)
        {
            _logger = logger;
            _mail = mail;   
        }

        public async Task<IActionResult> Index()
        {
            string receivers = _mail.RabbitMQReceivedAsync();

            return View(receivers);
            
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Info()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}