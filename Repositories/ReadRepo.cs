using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EzRepo.Models;
using EzRepo.Services;
using Microsoft.EntityFrameworkCore;

namespace EzRepo.Repositories
{
    public interface IReadRepo<TEntity, TIndex, TModel>
    {
        Task<TIndex> BrowseAsync(BrowseQuery query);
        Task<TIndex> BrowseAsync(BrowseQuery<IQueryable<TEntity>> query);
        Task<TModel> ReadAsync(int key);
    }
    public class ReadRepo<TEntity, TIndex, TModel> : IReadRepo<TEntity, TIndex, TModel> where TEntity : class, IBaseEntity
    {
        #region Constructor
        private readonly DbContext _context;
        private readonly IMapper _mapper;
        private readonly IIncludeService<TEntity> _includeService;
        private readonly ISearchService<TEntity> _searchService;

        public ReadRepo(DbContext context, IMapper mapper, IIncludeService<TEntity> includeService, ISearchService<TEntity> searchService)
        {
            _context = context;
            _mapper = mapper;
            _includeService = includeService;
            _searchService = searchService;
        }
        #endregion

        #region Public Methods
        public async Task<TIndex> BrowseAsync(BrowseQuery query)
        {
            var entities = _searchService.Browse(GetInclusiveEntities(), query);
            return await GetEntityModelsAsTaskAsync(entities);
        }

        public async Task<TIndex> BrowseAsync(BrowseQuery<IQueryable<TEntity>> query)
        {
            var entities = _searchService.Browse(GetInclusiveEntities(), query);
            return await GetEntityModelsAsTaskAsync(entities);
        }

        public async Task<TModel> ReadAsync(int key)
        {
            var entities =  GetInclusiveEntities();
            var entity = await entities.FirstOrDefaultAsync(entity => entity.ID == key);
            return _mapper.Map<TModel>(entity);
        }
        #endregion

        #region Private Methods
        private IQueryable<TEntity> GetInclusiveEntities()
        {
            var entities = _context.Set<TEntity>();
            return _includeService.GetIncluiveEntities(entities);
        }

        private async Task<TIndex> GetEntityModelsAsTaskAsync(IQueryable<TEntity> entities)
        {
            return await Task.Run(() => {
                return _mapper.Map<TIndex>(entities);
            });
        }
        #endregion
    }
}
