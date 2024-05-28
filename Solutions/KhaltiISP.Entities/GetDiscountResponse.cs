using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KhaltiISP.Entities
{
    public class GetDiscountResponse
    {
        public DiscountResponse results { get; set; }
    }
    public class DiscountResponse
    {
        public bool Status { get; set; } = false;
        public decimal Amount { get; set; } = 0;
        public string Message { get; set; } = string.Empty;
        public string Package { get; set; }=string.Empty;
        public string ReferenceId { get; set; } = string.Empty;
        public int SessionId { get; set; }
    }
}
