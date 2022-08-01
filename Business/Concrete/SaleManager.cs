using DataAccess.Concrete;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class SaleManager
    {
        private SaleDal _saleDal;

        public SaleManager(SaleDal saleDal)
        {
            _saleDal = saleDal;
        }


        public async Task<bool> AddSale(Sale sale)
        {
            return _saleDal.Add(sale);
        }
    }
}
