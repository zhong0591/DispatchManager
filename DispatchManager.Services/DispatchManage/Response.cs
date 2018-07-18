using System;
using System.Collections.Generic;

namespace DispatchManager.Services.DispatchManage
{
    [Serializable()]
    public class Response
    {
        public int Count { get; set; }
        public string Message { get; set; }
        public string SearchCriteria { get; set; }
        public List<Result> Results { get; set; }
    }
}
