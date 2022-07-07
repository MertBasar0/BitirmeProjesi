using DataAccess.Abstract;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class BasketManager
    {
        private IBasketDal basketDal;
        public BasketManager(IBasketDal _basketDal)
        {
            basketDal = _basketDal;
        }

        public void AddBasket(Basket basket)
        {
            bool success = false;
            success = basketDal.Add(basket);
            if (success)
            {
            throw new Exception("Sepet başarıyla eklendi.");
            }
            else
            {
                throw new Exception("Bir hata oluştu..");
            }
        }
        public async Task<List<Basket>> GetAll()
        {
            return await basketDal.GetAll(null);
        }

    }
}