﻿using Core.Abstract.DataAccessGeneric;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface ICustomerDal : IRepositoryBase<Customer>
    {
        Task<bool> CheckUserAsync(string UserName);

        int GetLastCustomer();
    }
}
