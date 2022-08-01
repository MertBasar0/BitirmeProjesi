using DataAccess.Abstract;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class ProductManager
    {
        IProductDal _productDal;
        public ProductManager(IProductDal productDal)
        {
            _productDal = productDal;
        }

        public bool AddProduct(Product product)
        {
            bool success = false;

            success = _productDal.Add(product);
            return success;
        }

        public async Task<List<Product>> GetProducts(string? word = null)
        {
            List<Product> products;
            if (string.IsNullOrEmpty(word))
            {
                products = await _productDal.GetAll();
            }
            else
            {
                products = await _productDal.GetAll(x => x.ProductName.StartsWith(word) || x.ProductName.EndsWith(word));
            }
            
           return products;
        }

        public async Task<Product> GetProductAsync(int _productId)
        {
            Product product = await _productDal.Get(x => x.ProductId == _productId);
            return product;
        }

        public async Task<bool> EditProduct(Product product)
        {
            return await _productDal.Update(product);
        }
    }
}
