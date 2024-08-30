using Microsoft.EntityFrameworkCore;
using Tekton.Application.Base;
using System.Linq.Expressions;
using System;

namespace Tekton.Infrastructure
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly TektonContext _context;

        public BaseRepository(TektonContext context)
        {
            this._context = context;
        }
        public virtual void Add(T entity) => _context.Set<T>().Add(entity);

        public virtual void Update(T entity) => _context.Entry(entity).State = EntityState.Modified;

        public virtual void Delete(T entity) => _context.Remove(entity);

        public IEnumerable<T> GetAll(params Expression<Func<T, object>>[] includes)
        {
            var query = _context.Set<T>().AsQueryable();
            foreach (Expression<Func<T, object>> i in includes)
            {
                query = query.Include(i);
            }
            return query.ToList();
        }

        public IEnumerable<T> Filter(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
        {
            var query = _context.Set<T>().Where(predicate);
            foreach (Expression<Func<T, object>> i in includes)
            {
                query = query.Include(i);
            }
            return query.ToList();
        }
        public int SaveChanges() => _context.SaveChanges();
    }

}