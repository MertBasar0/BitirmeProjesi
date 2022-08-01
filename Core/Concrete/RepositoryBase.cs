using Core.Abstract.DataAccessGeneric;
using Core.Abstract.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Concrete
{
    public class RepositoryBase<TEntity,TContext> : IRepositoryBase<TEntity>
        where TEntity : class, IEntity, new() // belirli bir grupla sınırlamak için IEntity gibi bir interface ile kısıtlama sağlamadan devem etmeye izin vermiyor.
        where TContext : DbContext, new()
    {
        private TContext _context;
        public RepositoryBase()
        {
            _context = new TContext();
        }

        
        public bool Add(TEntity entity)
        {
                int success = 0;
                if (entity == null)
                {
                    throw new Exception("Lütfen veri girişi yapın..");
                }
                else
                {
                    var addedEntity = _context.Entry(entity);
                    addedEntity.State = EntityState.Added;
                    success = _context.SaveChanges();
                    return Convert.ToBoolean(success);
                }
           
            
        }

        public async Task<bool> DeleteAsync(TEntity entity)
        {

            await Task.Run(() => { _context.Set<TEntity>().Remove(entity); });
            int isSuccess = await _context.SaveChangesAsync();
            return Convert.ToBoolean(isSuccess);
            
        }

        public async Task<List<TEntity>> GetAll(Expression<Func<TEntity, bool>>? filter = null)
        {

                if (filter == null)
                {
                    return await _context.Set<TEntity>().ToListAsync();
                }
                else
                {
                    return await _context.Set<TEntity>().Where(filter).ToListAsync();
                }
            
        }

        public async Task<TEntity> Get(Expression<Func<TEntity, bool>>? filter)
        {

                return await _context.Set<TEntity>().FirstOrDefaultAsync(filter);
            
        }


        public async Task<bool> Update(TEntity entity)
        {
            //await Task.Run(() => { _context.Set<TEntity>().Update(entity); });

            var updatedEntity = _context.Entry(entity);
            updatedEntity.State = EntityState.Modified;
            int isSuccess = await _context.SaveChangesAsync();
            return Convert.ToBoolean(isSuccess);
        }
    }
}
