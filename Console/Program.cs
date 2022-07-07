using Business.Concrete;
using DataAccess;
using DataAccess.Concrete;
using Entities;
using Microsoft.Extensions.DependencyInjection;


//string choise = "";

//Console.WriteLine("Yapmak istediğiniz işlem nedir ?\n1.Müşteri kayıt\n2.Sepet kayıt");
//choise = Console.ReadLine();

//if (choise == "1")
//{
//    Console.WriteLine("Müşteri adını giriniz..");
//    string ad = Console.ReadLine();
//    Console.WriteLine("İletişim numarası giriniz..");
//    string no = Console.ReadLine();

//    try
//    {
//BasketProductManager basketProductmanager =new BasketProductManager(new BasketProductDal());

//BasketProduct BasketProduct = new BasketProduct();
//BasketProduct.Product = new Product()
//{
//    UnitsInStock =49,
//    ProductName = "armut",
//    UnitPrice = 12,
//};
//BasketProduct.Basket = new Basket()
//{
//    BasketOfCustomerId = 1,
//    InitTime = DateTime.Now
//};

//basketProductmanager.AddBasketProduct(BasketProduct);

//try
//{
//    BasketManager BasketManager = new BasketManager(new BasketDal());
//    BasketManager.AddBasket(new Basket() { InitTime = DateTime.Now});
//}
//catch (Exception ex)
//{ 
//    Console.WriteLine(ex.Message);
//}
Basket br = new Basket();
Product pro = new Product() { ProductName = "mandalina", UnitPrice = 31, UnitsInStock = 62 };
try
{
   
    BasketManager manager2 = new BasketManager(new BasketDal());
    manager2.AddBasket(br);
    //BasketProductManager manager = new BasketProductManager(new BasketProductDal());
    //manager.AddBasketProduct(new BasketProduct() { Product = new Product() { ProductName = "AMUT", UnitPrice = 20, UnitsInStock = 30 }, Basket = new Basket() { InitTime = DateTime.Now } });
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}


try
{
  
    ProductManager productManager = new ProductManager(new ProductDal());
    productManager.AddProduct(pro);
}
catch (Exception ex)
{

    Console.WriteLine(ex.Message);
}
try
{
    BasketProductManager productProductManager = new BasketProductManager(new BasketProductDal());
    productProductManager.AddBasketProduct(new BasketProduct() { BasketId=br.BasketId,ProductId=pro.ProductId});
}
catch (Exception ex)
{

    Console.WriteLine(ex.Message);
}

Console.ReadLine();



//    }
//    catch (Exception ex)
//    {

//        Console.WriteLine(ex.Message);
//    }

//}


