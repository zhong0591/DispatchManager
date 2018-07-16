using System; 
using System.ComponentModel.DataAnnotations.Schema; 
using System.Xml.Serialization;

namespace DispatchManager.Services.Common
{
   public  class DisposableBase:IDisposable
    {
        #region IDisposable

        [XmlIgnore]
        [NotMapped]
        public bool Disposed { get; private set; }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!Disposed)
            {
                if (disposing)
                {
                    DisposeManagedResources();
                }

                DisposeUnmanagedResources();
                Disposed = true;
            }
        }

        protected virtual void DisposeManagedResources()
        { }

        protected virtual void DisposeUnmanagedResources()
        { }

        #endregion

        #region Destructor

        ~DisposableBase()
        {
            Dispose(false);
        }

        #endregion
    }
}
