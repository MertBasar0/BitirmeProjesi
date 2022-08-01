using DataAccess.Abstract;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class CustomerManager
    {
        ICustomerDal customerDal;

        public CustomerManager(ICustomerDal _customerDal)
        {
            customerDal = _customerDal;
        }

        

        public bool AddCustomer(Customer customer)
        {
            try
            {
                bool success = customerDal.Add(customer);
                return success;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
           
        }



        public async Task<Customer> GetCustomerByName(string name)
        {
            return await customerDal.Get(x => x.CustomerName == name);
        }


        public async Task<List<Customer>> GetAllCustomers()
        {
            return await customerDal.GetAll(null);
        }



        public async Task<bool> CheckUserAsync(string UserName)
        {
            return await customerDal.CheckUserAsync(UserName);
        }



        public int GetLastCustomer()
        {
            return customerDal.GetLastCustomer();
        }
    }
}
