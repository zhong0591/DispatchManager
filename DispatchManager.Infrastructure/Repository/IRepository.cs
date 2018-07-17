using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;


namespace DispatchManager.Infrastructure.Repository
{
    public interface IRepository<T> 
    {
        IQueryable<T> Query();
        IQueryable<T> Query(Expression<Func<T, Boolean>> predicate);
        IQueryable<T> GetPage(IQueryable<T> query, int pageNumber, int pageSize);

        T Get(params object[] keyValues);
        ObservableCollection<T> Local();
        void Add(T entity);
        void Attach(T entity);
        void Detach(T entity);
        void DetachAll();
        void Update(T entity);
        void Delete(T entity);
        void Reload(T entity);
        void Reload(T entity, RefreshMode mode);
        void Reload(IEnumerable<T> entities);
        void Reload(IEnumerable<T> entities, RefreshMode mode);
        List<DbValidationError> ValidateModel(T model);

        IList<string> GetEntityModifiedProperties(T entity);
        IList<TValue> GetEntityValues<TValue>(T entity, string propertyName);
    }
}
