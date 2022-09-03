using Business.Concrete;
using DataAccess;
using DataAccess.Concrete;
using Entities;
using Entities.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;


//////////////////Managers///////////////////////

BasketProductManager _basketProductManager = new BasketProductManager(new BasketProductDal());
BasketManager _basketManager = new BasketManager(new BasketDal());
CustomerManager _customerManager = new CustomerManager(new CustomerDal());
ProductManager _productManager = new ProductManager(new ProductDal());

//--------------------------//


string choise = "";
bool success = false;


do
{
    Console.WriteLine("Yapmak istediğiniz işlem grubunu seçiniz.\n1.Müşteri işlemleri\n");
    choise = Console.ReadLine();

    if (choise == "1")
    {
        do
        {
            Console.WriteLine("1.Müşteri kayıt\n2.Müşterileri Listele\n3.Müşteri sepetini anlık görüntüle");
            Console.WriteLine("Çıkmak için herhangi bir tuşa basın");
            choise = Console.ReadLine();

            if (choise == "1")
            {
                Console.WriteLine("Müşteri adını giriniz..");
                string ad = Console.ReadLine();

                Customer customer = new Customer() { CustomerName = ad };

                success = _customerManager.AddCustomer(customer);

                _basketManager.AddBasket(new Basket() { CustomerID = customer.CustomerId });

                if (success)
                    Console.WriteLine("Kayıt Başarılı.");
                else
                    Console.WriteLine("Kayıt yapılamadı.");

            }
            else if (choise == "2")
            {
                List<Customer> customers = await _customerManager.GetAllCustomers();

                foreach (Customer customer in customers)
                {
                    Console.WriteLine(customer.ToString());
                }

            }
            else if (choise == "3")
            {
                Console.WriteLine("Müşterinin adını giriniz..");
                string ad = Console.ReadLine();
                Basket basket = await _basketManager.GetBasketByCustomerName(ad);
                
                if (basket != null)
                {
                    List<BasketProduct> basketProducts = await _basketProductManager.GetAllBasketProductsByBasketId(basket.BasketId);
                    if (basketProducts.Count != 0)
                    {
                        foreach (var item in basketProducts)
                        {
                        Product product = await _productManager.GetProductAsync((int)item.ProductId);
                        Console.WriteLine(product.ToString());
                        }
                    }
                }
                else
                    Console.WriteLine("Sepette herhangi bir ürün bulunamadı..");
            }
            else
            {
                break;
            }

        } while (true);
    }

} while (true);




