using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Tekton.Application.Base;
using Tekton.Domain.Entity;

namespace Tekton.Application.Interfaces
{
    public interface IProductRepository: IBaseRepository<Product>
    {
        IEnumerable<Product> GetAll(params Expression<Func<Product, object>>[] includes);
        IEnumerable<Product> Filter(Expression<Func<Product, bool>> where, params Expression<Func<Product, object>>[] includes);
    }
}
