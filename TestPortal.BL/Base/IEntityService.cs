using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TestPortal.BL.Base
{
    public interface IEntityService<T>
    {
        IEnumerable<T> GetAll();

        IEnumerable<T> GetPaged(int page, int pageSize, Expression<Func<T, bool>> predicate, string sort, string sortDirection, out int total);

        T GetSingle(Expression<Func<T, bool>> predicate);
    }
}
