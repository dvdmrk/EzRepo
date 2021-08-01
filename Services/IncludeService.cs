using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace EzRepo.Services
{
    public interface IIncludeService<TEntity> where TEntity : class
    {
        IQueryable<TEntity> GetIncluiveEntities(DbSet<TEntity> entities);
    }
    public class IncludeService<TEntity> : IIncludeService<TEntity> where TEntity : class
    {
        public IQueryable<TEntity> GetIncluiveEntities(DbSet<TEntity> entities)
        {
            return entities.AsQueryable();
        }
    }
}