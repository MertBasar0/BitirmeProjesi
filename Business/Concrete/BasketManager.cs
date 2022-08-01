using DataAccess.Abstract;
using DataAccess.Concrete;
using Entities;

namespace Business.Concrete
{
    public class BasketManager
    {
        private IBasketDal basketDal;
        public BasketManager(IBasketDal _basketDal)
        {
            basketDal = _basketDal;
        }
        

        public bool AddBasket(Basket basket)
        {
            bool success = false;
            success = basketDal.Add(basket);
            return success;
        }

        public async Task<List<Basket>> GetAll(string name)
        {
            return await basketDal.GetAll(x => x.Customer.CustomerName == name);
        }

        public async Task<Basket> GetBasketByCustomerName(string name)
        {
            return await basketDal.Get(x => x.Customer.CustomerName == name);
        }
        

        //public async List<Product> GetProductsFromBasket(Basket basket)
        //{

        //}
    }
}