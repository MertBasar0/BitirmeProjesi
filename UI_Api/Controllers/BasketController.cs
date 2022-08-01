using Business.Concrete;
using DataAccess.Concrete;
using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace UI_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private ProductManager _productManager;
        private BasketManager _basketManager;
        private BasketProductManager _basketProductManager;

        public BasketController()
        {
            _productManager = new ProductManager(new ProductDal());
            _basketManager = new BasketManager(new BasketDal());
            _basketProductManager = new BasketProductManager(new BasketProductDal());
        }


        [HttpGet("{customerName}")]
        public async Task<IActionResult> GetProductsByBasketFromCustomer(string customerName)
        {
            List<Entities.Product> products = new List<Entities.Product>();
            Basket basket = await _basketManager.GetBasketByCustomerName(customerName);
            List<BasketProduct> basketProducts = await _basketProductManager.GetAllBasketProductsByBasketId(basket.BasketId);

            if (basket != null)
            {
                foreach (var item in basketProducts)
                {
                    products.Add(await _productManager.GetProductAsync((int)item.ProductId));
                }

                if (products != null)
                {
                    return Ok(products);
                }
            }

            return BadRequest();

        }


    }
}
