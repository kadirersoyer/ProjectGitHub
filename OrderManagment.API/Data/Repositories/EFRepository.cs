using OrderManagment.API.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace OrderManagment.API.Data.Repositories
{
    public class EFRepository<T> : IRepository<T> where T: class
    {
        private readonly DbContext _Context;
        private readonly DbSet<T> _DBSet;

        public EFRepository(OrderManagmentContext Context)
        {
            this._Context = Context;
            this._DBSet = Context.Set<T>(); 
        }

        public void Delete(int id)
        {
            var entity = GetById(id);
            _DBSet.Attach(entity);
            _DBSet.Remove(entity);

        }

        public T Get(Expression<Func<T, bool>> predicate)
        {
            return _DBSet.Where(predicate).SingleOrDefault();
        }

        public IQueryable<T> GetAll()
        {
            return _DBSet;
        }

        public IQueryable<T> GetAll(Expression<Func<T, bool>> predicate)
        {
            return _DBSet.Where(predicate);
        }

        public T GetById(int id)
        {
            return _DBSet.Find(id);
        }

        public void Insert(T entity)
        {
            _DBSet.Add(entity);
        }

        public void Update(T entity)
        {
            _DBSet.Attach(entity);
            _Context.Entry(entity).State = EntityState.Modified;
        }
    }
}