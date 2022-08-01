using Business.Concrete;
using DataAccess.Concrete;
using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApp_Store.Controllers
{
    [Authorize(Roles = "User,Admin,Manager")]
    public class BasketProductController : Controller
    {
        private BasketProductManager  _manager;
        private BasketManager _basketManager;
        private ProductManager _productManager;

        public BasketProductController()
        {
            _manager = new BasketProductManager(new BasketProductDal());
            _basketManager = new BasketManager(new BasketDal());
            _productManager = new ProductManager(new ProductDal());
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {

            return View();
        }
         
        [HttpGet]
        public async Task<PartialViewResult> GetProductInBasketproducts()
        {
            List<Product> products = new List<Product>();

            ViewBag.sepet = await _basketManager.GetBasketByCustomerName(TempData["Message"].ToString());
            TempData.Keep("Message");

            List<BasketProduct> basketProducts = await _manager.GetAllBasketProductsByBasketId(((Basket)ViewBag.sepet).BasketId);

            foreach (BasketProduct item in basketProducts)
            {
                products.Add(await _productManager.GetProductAsync((int)item.ProductId));
            }

            return PartialView("Index", products);
        }

        public async Task<IActionResult> Delete(int productId)
        {
            Basket basket = await _basketManager.GetBasketByCustomerName(TempData["Message"].ToString());
            TempData.Keep("Message");

            await _manager.DeleteBasketProduct(productId,basket.BasketId);

            return RedirectToAction("GetProductInBasketproducts");
        }
    }
}