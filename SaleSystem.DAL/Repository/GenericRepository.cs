using SaleSystem.DAL.DBContext;
using SaleSystem.DAL.Repository.Contract;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace SaleSystem.DAL.Repository
{
    public class GenericRepository<TModel> : IGenericRepository<TModel> where TModel : class
    {
        private readonly DbsaleContext _dbContext;

        public GenericRepository(DbsaleContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<TModel> Create(TModel model)
        {
            try
            {
                _dbContext.Set<TModel>().Add(model);
                await _dbContext.SaveChangesAsync();
                return model;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> Delete(TModel model)
        {
            try
            {
                _dbContext.Set<TModel>().Remove(model);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<TModel> Get(Expression<Func<TModel, bool>> predicate)
        {
            try
            {
                return await _dbContext.Set<TModel>().FirstOrDefaultAsync(predicate);

            }catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<IQueryable<TModel>> GetAll(Expression<Func<TModel, bool>> predicate = null)
        {
            try
            {
                IQueryable<TModel> queryModel = predicate == null 
                    ? _dbContext.Set<TModel>()
                    : _dbContext.Set<TModel>().Where(predicate);
                return queryModel;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> Update(TModel model)
        {
            try
            {
                _dbContext.Set<TModel>().Update(model);
                await _dbContext.SaveChangesAsync();
                return true;

            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
