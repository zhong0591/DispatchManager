using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;



namespace DispatchManager.Infrastructure.Repository
{
    public class GenericRepository<T> : IRepository<T> where T : class
    {
        private DbSet<T> _dbSet;
        private DbContext _dbContext;

        public GenericRepository(DbContext context)
        {
            _dbContext = context;
            _dbSet = context.Set<T>();
        }

        public IQueryable<T> Query()
        {
            return Query(null);
        }

        public IQueryable<T> Query(Expression<Func<T, bool>> predicate)
        {
            return predicate != null ? _dbSet.Where(predicate) : _dbSet;
        }

        public IQueryable<T> GetPage(IQueryable<T> query, int pageNumber, int pageSize)
        {
            return query.Skip((pageNumber - 1) * pageSize).Take(pageSize);
        }

        public T Get(params object[] keyValues)
        {
            return _dbSet.Find(keyValues);
        }

        public ObservableCollection<T> Local()
        {
            return _dbSet.Local;
        }

        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public void Attach(T entity)
        {
            _dbSet.Attach(entity);
        }

        public void Detach(T entity)
        {
            _dbContext.Entry<T>(entity).State = EntityState.Detached;
        }

        public void DetachAll()
        {
            IList<T> entityToDetached = _dbSet.Local.ToList();
            foreach (var entity in entityToDetached)
            {
                _dbContext.Entry<T>(entity).State = EntityState.Detached;
            }
        }

        public void Update(T entity)
        {
            if (_dbContext.Entry<T>(entity).State == EntityState.Detached)
            {
                _dbContext.Entry<T>(entity).State = EntityState.Modified;
            }
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public void Reload(T entity)
        {
            Reload(entity, RefreshMode.StoreWins);
        }

        public void Reload(T entity, RefreshMode mode)
        {
            ((IObjectContextAdapter)_dbContext).ObjectContext.Refresh(mode, entity);
        }

        public void Reload(IEnumerable<T> entities)
        {
            Reload(entities, RefreshMode.StoreWins);
        }

        public void Reload(IEnumerable<T> entities, RefreshMode mode)
        {
            ((IObjectContextAdapter)_dbContext).ObjectContext.Refresh(mode, entities);
        }

        public IList<string> GetEntityModifiedProperties(T entity)
        {
            var stateEntry = ((IObjectContextAdapter)_dbContext).ObjectContext.ObjectStateManager.GetObjectStateEntry(entity);
            return stateEntry.GetModifiedProperties().ToList();
        }

        public IList<TValue> GetEntityValues<TValue>(T entity, string propertyName)
        {
            IList<TValue> values = new List<TValue>();
            var entityEntry = _dbContext.Entry<T>(entity);
            values.Add(entityEntry.OriginalValues.GetValue<TValue>(propertyName));
            values.Add(entityEntry.CurrentValues.GetValue<TValue>(propertyName));
            return values;
        }
    }
}
