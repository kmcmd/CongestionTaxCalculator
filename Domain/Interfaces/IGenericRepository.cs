using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        public Task<TEntity> GetByIdAsync(int id, bool noTracking = true);

        public Task<List<TEntity>> GetAsync(
            Expression<Func<TEntity, bool>> filter = null,
            bool noTracking = true);
    }
}
