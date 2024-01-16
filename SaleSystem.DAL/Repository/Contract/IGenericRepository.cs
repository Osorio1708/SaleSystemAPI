﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Linq.Expressions;

namespace SaleSystem.DAL.Repository.Contract
{
    public interface IGenericRepository<TModel> where TModel : class
    {
        Task<TModel> Get(Expression<Func<TModel, bool>> predicate);
        Task<TModel> Create(TModel model);
        Task<bool> Update(TModel model);
        Task<bool> Delete(TModel model);
        Task<IQueryable<TModel>> GetAll(Expression<Func<TModel, bool>> predicate = null);
    }
}
