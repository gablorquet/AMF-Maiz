using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq.Expressions;
using AMF.Core.Model;

namespace AMF.Core.Storage
{
    public interface ISession : IDisposable
    {
        IDbSet<T> Set<T>() where T : Entity;
        T SingleById<T>(int id) where T : Entity;
        T Single<T>(Func<T, bool> predicate) where T : Entity;
        void Add<T>(T model) where T : Entity;
        void Commit();
        void Delete<T>(T model) where T : Entity;
        void Delete<T>(Func<T, bool> predicate) where T : Entity;
        void Attach<T>(T entity) where T : Entity;
        T Load<T, TElement>(T entity, Expression<Func<T, ICollection<TElement>>> func)
            where T : Entity
            where TElement : Entity;

        void Detach<T>(T model) where T : Entity;


        void PURGEDATABASE();
    }
}