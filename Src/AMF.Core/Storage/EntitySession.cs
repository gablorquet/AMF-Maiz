using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using AMF.Core.Model;

namespace AMF.Core.Storage
{
    public class EntitySession : ISession
    {
        private readonly AMFDbContext _context;

        public EntitySession(AMFDbContext context)
        {
            _context = context;
        }

        public IDbSet<T> Set<T>() where T : Entity
        {
            return _context.Set<T>();
        }

        public T SingleById<T>(int id) where T : Entity
        {
            return _context.Set<T>().FirstOrDefault(x => x.Id == id);
        }

        public T Single<T>(Func<T, bool> predicate) where T : Entity
        {
            return _context.Set<T>().FirstOrDefault(predicate);
        }

        public void Add<T>(T model) where T : Entity
        {
            _context.Set<T>().Add(model);
        }

        public void Detach<T>(T model) where T : Entity
        {
            _context.Entry(model).State = EntityState.Detached;
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public void Delete<T>(T model) where T : Entity
        {
            _context.Set<T>().Remove(model);
        }

        public void Delete<T>(Func<T, bool> predicate) where T : Entity
        {
            var model = Single(predicate);

            if (model != null)
                _context.Set<T>().Remove(model);
        }

        public void Attach<T>(T entity) where T : Entity
        {
            _context.Set<T>().Attach(entity);
        }

        public T Load<T, TElement>(T entity, Expression<Func<T, ICollection<TElement>>> func)
            where T : Entity
            where TElement : Entity
        {
            _context.Entry(entity).Collection(func).Load();
            return entity;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }

}