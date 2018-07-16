 
using DispatchManager.Data.Models;
using DispatchManager.Infrastructure.Repository;

namespace DispatchManager.Services.Common
{
    interface IManagerBase
    {
        IUnitOfWork Uow { get; }
     
    }
}
