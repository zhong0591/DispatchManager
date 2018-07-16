using System; 
using System.Data.Entity; 
using System.Transactions;

namespace DispatchManager.Infrastructure.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        #region Constructor

        public UnitOfWork(DbContext context)
        {
            Context = context;
        }

        #endregion

        #region IUnitOfWork

        public DbContext Context { get; set; }

        public TransactionScope CreateTransaction(System.Transactions.IsolationLevel isolationLevel, int timeoutInSeconds)
        {
            var option = TransactionScopeOption.Required;
            var options = new TransactionOptions()
            {
                IsolationLevel = isolationLevel,
                Timeout = new TimeSpan(0, 0, timeoutInSeconds)
            };
            return new TransactionScope(option, options);
        }

        public void Commit()
        {
            Context.SaveChanges();
        }

        public IRepository<T> Reposiotry<T>() where T : class
        {
            return new GenericRepository<T>(this.Context);
        }

        #endregion

        #region IDisposable

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // free managed resources
                if (Context != null)
                {
                    Context.Dispose();
                    Context = null;
                }
            }
            // free native resources if there are any.
        }

        #endregion
    }
}
