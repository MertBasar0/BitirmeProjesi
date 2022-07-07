using Business.Concrete;
using DataAccess.Concrete;
using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApp_Store.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        private ProductManager _productManager;
        public ProductController()
        {
            _productManager = new ProductManager(new ProductDal());
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(Product product)
        {
            if (ModelState.IsValid)
            {
                bool IsSuccess = await Task.Run(() => _productManager.AddProduct(product));
                if (IsSuccess)
                {
                    ViewBag.success = "Ürün başarıyla kaydedildi..";
                }
                else
                {
                    ViewBag.faild = "Bir terslik var..";
                }
            }
            else
            {
                ViewBag.empty = "Lütfen bir değer girin..";
            }
            return View();
        }
    }
}
