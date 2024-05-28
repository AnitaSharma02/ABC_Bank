using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WebAPI.ClientHelper
{
    public class CoreAuthTokenRequest
    {
        public string client_id { get; set; } = string.Empty;
        public string client_secret { get; set; } = string.Empty;
        public string grant_type { get; set; } = string.Empty;
        public string scope { get; set; } = string.Empty;
    }
}
