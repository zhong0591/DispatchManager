
using DispatchManager.Data.Models;
using DispatchManager.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;


namespace DispatchManager.Services.Common
{
    public class ManagerBase: DisposableBase, IManagerBase
    {
        public ManagerBase()
           : this(null)
        { }

        public ManagerBase(IUnitOfWork uow)
        {
            _uow = uow;
            AutoDisposeUow = _uow == null;
        }
         

        private IUnitOfWork _uow;
        public IUnitOfWork Uow
        {
            get { return _uow ?? (_uow = CreateUow()); }
            set { _uow = value; }
        }

        protected bool AutoDisposeUow { get; set; }

        protected virtual IUnitOfWork CreateUow()
        {
            return new UnitOfWork(ApplicationDbContext.Create());
        }

        protected override void DisposeManagedResources()
        {
            base.DisposeManagedResources();

            if (_uow != null && AutoDisposeUow)
            {
                ((IDisposable)_uow).Dispose();
            }
            _uow = null;
        }
        //protected string GetNextNewNumberByKey(string seedKey)
        //{
        //    using (var trans = Uow.CreateTransaction(System.Transactions.IsolationLevel.ReadCommitted, 60))
        //    {
        //        var rep = Uow.Reposiotry<XatSSeed>();
        //        var seed = rep.Query().FirstOrDefault(o => o.SeedKey == seedKey);
        //        if (seed == null)
        //        {
        //            seed = new XatSSeed();
        //            seed.SeedKey = seedKey;
        //            seed.SeedValue = 1;
        //            rep.Add(seed);
        //        }
        //        else
        //        {
        //            seed.SeedValue++;

        //        }
        //        Uow.Commit();
        //        trans.Complete();
        //        string seedNumber = seed.SeedValue.ToString();
        //        return seedNumber;
        //    }
        //}

      
        
    }
}
