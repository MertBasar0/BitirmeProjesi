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

        public void AddCustomer(Customer customer)
        {
            bool success = false;
            try
            {
                success = customerDal.Add(customer);
                if (!success)
                {
                    throw new Exception("Müşteri eklenirken bir hata oluştu.\nLütfen database bağlantısını kontrol ediniz..");
                }
                else
                {
                    throw new Exception("Müşteri başarıyla eklendi..");
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
           
        }
    }
}
