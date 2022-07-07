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
            bool success = false;
            success = _basketProductDal.Add(basketProduct);
            if (success)
            {
                throw new Exception("Eşleştirme başarıyla gerçekleştirildi..");
            }
            else if(!success)
            {
                throw new Exception("Kayıt oluşturulurken bir hata oluştu");
            }
        }
    }
}
