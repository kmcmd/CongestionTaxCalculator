using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> 
        where TEntity : BaseEntity
    {
        private readonly ApplicationContext _context;
        private readonly DbSet<TEntity> dbSet;

        public GenericRepository(ApplicationContext context)
        {
            _context = context;
            dbSet = _context.Set<TEntity>();
        }

        public async Task<List<TEntity>> GetAsync(Expression<Func<TEntity, bool>> filter = null, bool noTracking = true)
        {
            IQueryable<TEntity> query = dbSet;
            if(noTracking)
                query = query.AsNoTracking();

            if(filter != null)
                return await query.Where(filter).ToListAsync();

            return await query.ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync(int id, bool noTracking = true)
        {
            IQueryable<TEntity> query = dbSet;
            if (noTracking)
                query = query.AsNoTracking();

            return await query.FirstOrDefaultAsync(c=> c.Id == id);
        }
    }
}
