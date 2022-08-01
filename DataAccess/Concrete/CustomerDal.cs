using Core.Concrete;
using DataAccess.Abstract;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete
{
    public class CustomerDal : RepositoryBase<Customer,StoreDbContext>, ICustomerDal
    {

        //Bu metot asenkron şekilde yazılmış olmasına rağmen içinde await parametresi alan herhangi bir kod bulunmamaktadır.
        //Bu şekilde yazılmasının sebebi bu durumun örneğini göstermektir.

        public async Task<bool> CheckUserAsync(string Username)
        {
            using (StoreDbContext context = new StoreDbContext())
            {
                List<Customer> Customers = context.Customers.ToList();

                List<string> CustomerNames = new List<string>();

                foreach (var item in Customers)
                {
                    CustomerNames.Add(item.CustomerName);
                }
                return CustomerNames.Any(x => x.Contains(Username));
            } 
        }

        public int GetLastCustomer()
        {
            using (StoreDbContext context = new StoreDbContext())
            {
                if (context.Customers != null)
                {
                    int last = context.Customers.OrderBy(x => x.CustomerId).LastOrDefault().CustomerId;
                    return last;
                }
            return 0;
            }
        }
    }
}
