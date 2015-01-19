using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TestPortal.DAL.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private DbContext _context;
        private readonly IDbSet<TEntity> _objectSet;

        public Repository(DbContext context)
        {
            _context = context;
            _objectSet = _context.Set<TEntity>();
            //_context.ContextOptions.LazyLoadingEnabled = true; Default!
        }

        public Repository(DbContext context, bool lazyLoading)
        {
            _context = context;
            _objectSet = _context.Set<TEntity>();
            _context.Configuration.LazyLoadingEnabled = lazyLoading;
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public void Add(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            _objectSet.Add(entity);
        }

        public void Edit(TEntity entity)
        {
            _objectSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            _objectSet.Remove(entity);
        }

        public void Delete(Expression<Func<TEntity, bool>> predicate)
        {
            var records = Find(predicate);

            foreach (var record in records)
            {
                Delete(record);
            }
        }

        public void DeleteRelatedEntities(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            var releatedEntities =
                ((IEntityWithRelationships)entity).RelationshipManager.GetAllRelatedEnds().SelectMany(
                    e => e.CreateSourceQuery().OfType<TEntity>()).ToList();
            foreach (var releatedEntity in releatedEntities)
            {
                _objectSet.Remove(releatedEntity);
            }
            _objectSet.Remove(entity);

        }

        public IEnumerable<TEntity> GetAll()
        {
            return _objectSet.AsEnumerable();
        }

        public IEnumerable<TEntity> GetAllPaged(int page, int pageSize)
        {
            return _objectSet.AsEnumerable().Skip(page * pageSize).Take(pageSize);
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return _objectSet.Where(predicate).AsEnumerable();
        }

        public IEnumerable<TEntity> FindPaged(int page, int pageSize, Expression<Func<TEntity, bool>> predicate, string sort, string sortDirection)
        {
            var items = _objectSet.Where(predicate);
            string command = sortDirection == "DESC" ? "OrderByDescending" : "OrderBy";

            var type = typeof(TEntity);

            var property = type.GetProperty(sort);

            var parameter = Expression.Parameter(type, "p");

            var propertyAccess = Expression.MakeMemberAccess(parameter, property);

            var orderByExpression = Expression.Lambda(propertyAccess, parameter);

            var resultExpression = Expression.Call(typeof(Queryable), command, new Type[] { type, property.PropertyType },

                                   items.Expression, Expression.Quote(orderByExpression));

            items = items.Provider.CreateQuery<TEntity>(resultExpression);

            return items.Skip(page * pageSize).Take(pageSize).AsEnumerable();
        }

        public TEntity Single(Expression<Func<TEntity, bool>> predicate)
        {
            return _objectSet.Single(predicate);
        }

        public TEntity First(Expression<Func<TEntity, bool>> predicate)
        {
            return _objectSet.First(predicate);
        }

        public int Count()
        {
            return _objectSet.Count();
        }

        public int Count(Expression<Func<TEntity, bool>> criteria)
        {
            return _objectSet.Count(criteria);
        }

        #region IDisposable
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposing) return;
            if (_context == null) return;
            _context.Dispose();
            _context = null;
        }
        #endregion
    }
}
