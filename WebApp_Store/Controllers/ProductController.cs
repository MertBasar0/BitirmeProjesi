using Business.Concrete;
using DataAccess.Concrete;
using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApp_Store.Controllers
{
    [Authorize(Roles ="User,Admin,Manager")]
    public class ProductController : Controller
    {
        private ProductManager _productManager;
        private BasketManager _basketManager;
        private BasketProductManager _basketProductManager;

        public ProductController()
        {
            _productManager = new ProductManager(new ProductDal());
            _basketManager = new BasketManager(new BasketDal());
            _basketProductManager = new BasketProductManager(new BasketProductDal());
        }


        [HttpGet]
        public async Task<IActionResult> Index(string? word = null)
        {
            List<Product> resultList  = new List<Product>();

            Basket basket = await _basketManager.GetBasketByCustomerName(TempData["Message"].ToString());
            TempData.Keep("Message");

            List<BasketProduct> basketProducts = await _basketProductManager.GetAllBasketProductsByBasketId(basket.BasketId);

            List<Product> products = await _productManager.GetProducts(word);

            if (basketProducts.Count != 0)
            {
                resultList.AddRange(products);
                foreach (var x in products)
                {
                    foreach (var y in basketProducts)
                    {
                        if (x.ProductId == y.ProductId)
                        {
                            resultList.Remove(x);
                        }
                    }
                }
            }
            else
            {
                resultList.AddRange(await _productManager.GetProducts(word));
            }

            return View(resultList);
        }


        public IActionResult Add()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Add(Product product)
        {
            if (product != null)
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
            }
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            Product product = await _productManager.GetProductAsync(id);
            return View(product);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(Product product)
        {
            bool isSuccess = await _productManager.EditProduct(product);
            if (isSuccess)
            {
                ViewBag.result = "ürün güncellendi.";
            }
            else
            {
                ViewBag.result = "ürün güncellenirken bir hata oluştu.";
            }

            return RedirectToAction("index");
        }


        public async Task<IActionResult> AddProductToBasket(int _productId)
        {
            if (TempData["Message"] != null)
            {
                string customerName = TempData["Message"].ToString();
                TempData.Keep("Message");

                if (customerName != null)
                {
                    Basket basket = await _basketManager.GetBasketByCustomerName(customerName);

                    _basketProductManager.AddBasketProduct(new BasketProduct() { ProductId = _productId, BasketId = basket.BasketId });
                }
            }
            return RedirectToAction("index");
        }

        public async Task<IActionResult> deleteProduct(int _productId)
        {
            Product pro = await _productManager.GetProductAsync(_productId);
            bool success = await _productManager.deleteProductAsync(_productId);
            if (success)
            {
                return RedirectToAction("index");
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
