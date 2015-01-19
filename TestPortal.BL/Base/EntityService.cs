using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestPortal.DAL.Repository;
using TestPortal.DAL.UnitOfWork;

namespace TestPortal.BL.Base
{
    public abstract class EntityService<T> : IEntityService<T> where T : class
    {
        IUnitOfWork _unitOfWork;
        IRepository<T> _repository;

        public EntityService(IUnitOfWork unitOfWork, IRepository<T> repository)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
        }

        public virtual IEnumerable<T> GetAll()
        {
            return _repository.GetAll();
        }


        public IEnumerable<T> GetPaged(int page, int pageSize, System.Linq.Expressions.Expression<Func<T, bool>> predicate, string sort, string sortDirection, out int total)
        {
            total = _repository.Count();
            return _repository.FindPaged(page, pageSize, predicate, sort, sortDirection);
        }


        public T GetSingle(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            return _repository.Single(predicate);
        }
    }
}
