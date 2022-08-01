using Business.Concrete;
using DataAccess.Concrete;
using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApp_Store.Controllers
{
    [Authorize(Roles = "User,Admin,Manager")]
    public class SaleController : Controller
    {
        private SaleManager _saleManager;
        private BasketManager _basketManager;
        private BasketProductManager _basketProductManager;
        private ProductManager _productManager;
        private CustomerManager _customerManager;

        public SaleController()
        {
            _saleManager = new SaleManager(new SaleDal());
            _basketManager = new BasketManager(new BasketDal());
            _basketProductManager = new BasketProductManager(new BasketProductDal());
            _productManager = new ProductManager(new ProductDal());
            _customerManager = new CustomerManager(new CustomerDal());
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> AddSale()
        {
            int _total = 0;
            Basket basket = await _basketManager.GetBasketByCustomerName(TempData["Message"].ToString());
            List<BasketProduct> basketProducts = await _basketProductManager.GetAllBasketProductsByBasketId(basket.BasketId);

            foreach (var item in basketProducts)
            {
                Product product = await _productManager.GetProductAsync((int)item.ProductId);
                _total += (int)product.UnitPrice;
                await _basketProductManager.DeleteBasketProduct((int)item.ProductId,basket.BasketId);
            }

            _saleManager.AddSale(new Sale { Total = _total, Customer = await _customerManager.GetCustomerByName(TempData["Message"].ToString()) });
            TempData.Keep("Message");

            return PartialView("Index", _total);
        }
    }
}
