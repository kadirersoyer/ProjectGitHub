using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using OrderManagment.API.Data.Repositories;
using OrderManagment.API.Models;

namespace OrderManagment.API.UnitOfWork
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private readonly OrderManagmentContext _Context;

        public EFUnitOfWork(OrderManagmentContext context)
        {
            Database.SetInitializer<OrderManagmentContext>(null);

            _Context = context;

        }

        public IRepository<T> GetRepository<T>() where T : class
        {
            return new EFRepository<T>(_Context);
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _Context.Dispose();
                }
            }

            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public int SaveChanges()
        {
            try
            {
                // Transaction işlemleri burada ele alınabilir veya Identity Map kurumsal tasarım kalıbı kullanılarak
                // sadece değişen alanları güncellemeyide sağlayabiliriz.
                return _Context.SaveChanges();
            }
            catch
            {
                // Burada DbEntityValidationException hatalarını handle edebiliriz.
                throw;
            }
        }
    }
}