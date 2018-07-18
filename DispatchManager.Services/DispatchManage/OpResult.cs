using System.Collections.Generic;

namespace DispatchManager.Services.DispatchManage
{
    public class OpResult
    {
        private bool _ok = true;
        public bool Ok { get { return _ok; } set { this._ok = value; } }

        private List<string> _errs = null;// new List<string>();
        public List<string> Errors
        {
            get
            {
                return _errs = _errs ?? new List<string>();
            }
            set
            {
                _errs = value;
            }
        }
    }
}