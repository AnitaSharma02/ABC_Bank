using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Framework.PostHelper
{
    public class Results
    {
        public bool IsSucessful { get; set; }
        public string ErrorCode { get; set; }
        public string ExceptionMessage { get; set; }
        public dynamic ReturnObject { get; set; }
    }
    public class Root
    {
        public object results { get; set; }
    }
}
