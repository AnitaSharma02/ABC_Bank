using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CE.Entities
{
    public class ABCEmailDetailsResponse
    {
        public EmailResults results { get; set; }
    }
    public class EmailResults
    {
        public bool IsSucessful { get; set; }
        public string ErrorCode { get; set; }
        public string ExceptionMessage { get; set; }
        public string ReturnObject { get; set; }
        public int Count { get; set; } = 0;
    }
}
