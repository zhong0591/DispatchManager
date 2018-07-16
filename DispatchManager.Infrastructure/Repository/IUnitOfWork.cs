using System; 
using System.Data.Entity; 
using System.Transactions;

namespace DispatchManager.Infrastructure.Repository
{
    public interface IUnitOfWork:IDisposable
    {
        DbContext Context { get; set; }
        TransactionScope CreateTransaction(IsolationLevel isolationLevel, int timeoutInSeconds);
        void Commit();

        IRepository<T> Reposiotry<T>() where T : class;
    }
}
