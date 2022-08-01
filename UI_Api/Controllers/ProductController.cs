using Business.Concrete;
using DataAccess.Concrete;
using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace UI_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private ProductManager _productManager;

        public ProductController()
        {
            _productManager = new ProductManager(new ProductDal());
        }


        [HttpGet("{word?}")]
        public async Task<IActionResult> GetProducts(string? word=null)
        {
            List<Product> products = await _productManager.GetProducts(word);

            return Ok(products);
        }


        [HttpPost("{productName}, {unitPrice}")]
        public IActionResult AddProducts(string productName, decimal unitPrice)
        {
            Product product = new Product() { ProductName =productName, UnitPrice = unitPrice};
            var success =  _productManager.AddProduct(product);

            if (success)
            {
                return Ok(product);
            }

            return NotFound();
        }
    }
}
