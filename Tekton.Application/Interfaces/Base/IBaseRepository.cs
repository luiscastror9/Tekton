using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Tekton.Application.Base
{
    public interface IBaseRepository<T> where T : class
    {
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        IEnumerable<T> GetAll(params Expression<Func<T, object>>[] includes);
        IEnumerable<T> Filter(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] includes);
        int SaveChanges();

    }
}