using System.Linq;
using System.Threading.Tasks;
using EzRepo.Models;

namespace EzRepo.Services
{
    public interface ISearchService<TEntity>
    {
        IQueryable<TEntity> Browse(IQueryable<TEntity> entities, BrowseQuery query);
        IQueryable<TEntity> Browse(IQueryable<TEntity> entities, BrowseQuery<IQueryable<TEntity>> query);
    }
    public class SearchService<TEntity> : ISearchService<TEntity>
    {
        public IQueryable<TEntity> Browse(IQueryable<TEntity> entities, BrowseQuery query)
        {
            throw new System.NotImplementedException();
        }

        public IQueryable<TEntity> Browse(IQueryable<TEntity> entities, BrowseQuery<IQueryable<TEntity>> query)
        {
            return SkipTake(query.CustomQuery(entities), query);
        }
        
        private IQueryable<TEntity> SkipTake(IQueryable<TEntity> entities, BrowseQuery<IQueryable<TEntity>> query)
        {
            if (query.Skip > 0)
            {
                entities = entities.Skip(query.Skip);
            }
            
            if (query.Take > 0)
            {
                entities = entities.Take(query.Take);
            }

            return entities;
        }
    }
}