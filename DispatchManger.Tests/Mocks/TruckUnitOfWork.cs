using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;
using DispatchManager.Infrastructure.Repository;

namespace DispatchManger.Services.Tests.Mocks
{
    public class TruckUnitOfWork : IUnitOfWork
    {
        public DbContext Context
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public void Commit()
        {
             
        }

        public System.Transactions.TransactionScope CreateTransaction(System.Transactions.IsolationLevel isolationLevel, int timeoutInSeconds)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public IRepository<T> Reposiotry<T>() where T : class
        {
            return new MockRepository<T>();
        }
    }

    public class MockRepository<T> : IRepository<T>
    {
        private List<T> _data = new List<T>();
        public void Add(T entity)
        {
            _data.Add(entity);
        }

        public void Attach(T entity)
        {
            _data.Add(entity);
        }

        public void Delete(T entity)
        {
            _data.Remove(entity);
        }

        public void Detach(T entity)
        {
            throw new NotImplementedException();
        }

        public void DetachAll()
        {
            throw new NotImplementedException();
        }

        public T Get(params object[] keyValues)
        {
            throw new NotImplementedException();
        }

        public IList<string> GetEntityModifiedProperties(T entity)
        {
            throw new NotImplementedException();
        }

        public IList<TValue> GetEntityValues<TValue>(T entity, string propertyName)
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> GetPage(IQueryable<T> query, int pageNumber, int pageSize)
        {
            throw new NotImplementedException();
        }

        public ObservableCollection<T> Local()
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> Query()
        {
            return _data.AsQueryable<T>();
        }

        public IQueryable<T> Query(Expression<Func<T, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public void Reload(IEnumerable<T> entities)
        {
            throw new NotImplementedException();
        }

        public void Reload(T entity)
        {
            throw new NotImplementedException();
        }

        public void Reload(IEnumerable<T> entities, RefreshMode mode)
        {
            throw new NotImplementedException();
        }

        public void Reload(T entity, RefreshMode mode)
        {
            throw new NotImplementedException();
        }

        public void Update(T entity)
        {
            throw new NotImplementedException();
        }

        public List<DbValidationError> ValidateModel(T model)
        {
            return null;
        }
    }
}
