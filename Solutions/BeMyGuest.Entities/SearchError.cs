using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeMyGuest.Entities
{
    public class SearchError
    {
        public string code { get; set; }
        public string message { get; set; }
        public int http_code { get; set; }
    }
}
