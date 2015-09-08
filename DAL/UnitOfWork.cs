using System;
using System.Data.Entity;
using System.Diagnostics;
using DAL.Interface.Repository;

namespace DAL.Concrete
{
    public class UnitOfWork : IUnitOfWork
    {
        private bool disposed;
        public DbContext Context { get; private set; }

        public UnitOfWork(DbContext context)
        {
            disposed = false;
            Context = context;
        }

        public void Commit()
        {
            if (Context != null)
            {
                Context.SaveChanges();
            }
        }

        public void Dispose()
        {
            /*
            if (Context != null)
            {
                Context.Dispose();
            }
             * */
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~UnitOfWork()
        {
            Dispose(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (Context != null)
                {
                    Context.Dispose();
                }
                disposed = true;
            }
        }
    }
}