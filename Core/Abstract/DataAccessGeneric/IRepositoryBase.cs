﻿using Core.Abstract.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Abstract.DataAccessGeneric
{
    public interface IRepositoryBase<T> where T : class, IEntity, new()
    {
        bool Add(T entity);
        Task<bool> Update(T entity);
        Task<bool> DeleteAsync(T entity);
        Task<List<T>> GetAll(Expression<Func<T,bool>>? filter = null);
        Task<T> Get(Expression<Func<T,bool>> filter);
    }
}
