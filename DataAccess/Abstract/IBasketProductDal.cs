using Core.Abstract.DataAccessGeneric;
using Core.Concrete;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IBasketProductDal : IRepositoryBase<BasketProduct>
    {
    }
}
