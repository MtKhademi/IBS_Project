//----------------------------------
//-- Creator : MrMohande3 Khademi --
//----------------------------------

using Common.Entities;
using Microsoft.EntityFrameworkCore;

namespace Core.DAL.Implementations
{
    public abstract class BaseRepository<TKeyModel, TModel>
      where TModel : class
    {
        private DbSet<TModel> _dbSet;
        private readonly DatabaseContext _context;
        public BaseRepository(DatabaseContext context)
        {
            _context = context;
            _dbSet = context.Set<TModel>();
        }


        #region Create 
        public virtual async Task CreateAsync(TModel model)
        {
            await _dbSet.AddAsync(model);
            await _context.SaveChangesAsync();
        }
        public virtual async Task CreatesArrangeAsync(IEnumerable<TModel> models)
        {
            await _dbSet.AddRangeAsync(models);
            await _context.SaveChangesAsync();
        }

        #endregion

        #region Update
        public async Task UpdateAsync(TModel model)
        {
            _dbSet.Update(model);
            await _context.SaveChangesAsync();
        }
        #endregion

        #region Get

        public async Task<TModel> GetByIDAsync(TKeyModel id) => await _dbSet.FindAsync(id);
        public virtual IQueryable<TModel> GetsQueryableTracker() => _dbSet.AsQueryable();
        public virtual IQueryable<TModel> GetsQueryableNoTracker() => _dbSet.AsNoTracking().AsQueryable();


        #endregion

        #region Delete
        public virtual async Task DeleteHardAsync(TModel model)
        {
            _dbSet.Remove(model);
            await _context.SaveChangesAsync();
        }
        public virtual async Task DeleteHardAsync(TKeyModel keyModel)
        {
            var model = await _dbSet.FindAsync(keyModel);
            if (model == null)
                return;
            _dbSet.Remove(model);
            await _context.SaveChangesAsync();
        }
        public virtual async Task DeletesHardByAsync(IEnumerable<TModel> models)
        {
            _dbSet.RemoveRange(models);
            await _context.SaveChangesAsync();
        }
        public virtual async Task DeletesHardByAsync(IEnumerable<TKeyModel> keys)
        {
            foreach (var key in keys)
            {
                var model = await _dbSet.FindAsync(key);
                if (model != null)
                    _dbSet.Remove(model);
            }

            await _context.SaveChangesAsync();
        }
        public virtual async Task DeletesAllHardAsync()
        {
            string cmd = $"DELETE FROM {AnnotationHelper.TableName(_dbSet)}";
            await _context.Database.ExecuteSqlRawAsync(cmd);
        }
        public virtual async Task TruncateAsync()
        {
            string cmd = $"TRUNCATE TABLE {AnnotationHelper.TableName(_dbSet)}";
            await _context.Database.ExecuteSqlRawAsync(cmd);
        }



        public virtual Task DeleteSoftAsync(TModel model)
        {
            throw new NotImplementedException();
        }
        public virtual Task DeleteSoftAsync(TKeyModel keyModel)
        {
            throw new NotImplementedException();
        }
        public virtual Task DeletesSoftAsync(IEnumerable<TKeyModel> keys)
        {
            throw new NotImplementedException();
        }
        public virtual Task DeletesSoftAsync(IEnumerable<TModel> models)
        {
            throw new NotImplementedException();
        }
        public virtual Task DeleteAllSoftAsync()
        {
            throw new NotImplementedException();
        }

        #endregion

    }
}
