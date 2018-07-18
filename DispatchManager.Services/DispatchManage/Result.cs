using System;

namespace DispatchManager.Services.DispatchManage
{
    [Serializable()]
    public class Result
    {
        public string Value { get; set; }
        public string ValueId { get; set; }
        public string Variable { get; set; }
        public string VariableId { get; set; }

    }
}
