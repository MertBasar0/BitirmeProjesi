using DataAccess.Abstract;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class BasketProductManager
    {
        private IBasketProductDal _basketProductDal;

        public BasketProductManager(IBasketProductDal basketProductDal)
        {
            _basketProductDal = basketProductDal;
        }


        public void AddBasketProduct(BasketProduct basketProduct)
        {
            bool success = _basketProductDal.Add(basketProduct);
        }


        public async Task<List<BasketProduct>> GetAllBasketProductsByBasketId(int id)
        {
            return await _basketProductDal.GetAll(x =>x.BasketId == id);
        }

        public async Task DeleteBasketProduct(int _productId, int _basketId)
        {


            BasketProduct x = await _basketProductDal.Get(x => x.ProductId == _productId && x.BasketId == _basketId);
            if (x != null)
            {
                await _basketProductDal.DeleteAsync(x);
            }
        }
    }
}
