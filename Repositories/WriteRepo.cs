using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EzRepo.Models;
using EzRepo.Services;
using Microsoft.EntityFrameworkCore;

namespace EzRepo.Repositories
{
    public interface IWriteRepo<TEntity, TModel>
    {
        Task<TEntity> AddAsync(TModel model);
        Task<TEntity> UpdateAsync(TModel model);
        Task<TEntity> DeleteAsync(int key);
    }
    public class WriteRepo<TEntity, TModel> : IWriteRepo<TEntity, TModel> 
        where TEntity : class, IBaseEntity
        where TModel : IBaseEntity
    {
        #region Constructor
        private readonly DbContext _context;
        private readonly IMapper _mapper;
        private readonly IIncludeService<TEntity> _includeService;
        public WriteRepo(DbContext context, IMapper mapper, IIncludeService<TEntity> includeService)
        {
            _context = context;
            _mapper = mapper;
            _includeService = includeService;
        }
        #endregion

        #region Public Methods
        public async Task<TEntity> AddAsync(TModel model)
        {
            var entity = _mapper.Map<TEntity>(model);
            await _context.Set<TEntity>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<TEntity> DeleteAsync(int key)
        {
            var entity = await _context.Set<TEntity>().FirstOrDefaultAsync(entity => entity.ID == key);
            _context.Set<TEntity>().Remove(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<TEntity> UpdateAsync(TModel model)
        {
            var entities = GetInclusiveEntities();
            var entity = await entities.FirstOrDefaultAsync(entity => entity.ID == model.ID);
            _mapper.Map(model, entity);
            await _context.SaveChangesAsync();
            return entity;
        }
        #endregion

        #region Private Methods
        private IQueryable<TEntity> GetInclusiveEntities()
        {
            var entities = _context.Set<TEntity>();
            return _includeService.GetIncluiveEntities(entities);
        }
        #endregion
    }
}