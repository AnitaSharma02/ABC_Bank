using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WebAPI.ClientHelper
{
    public class TokenModel
    {
        public Results results { get; set; }
    }

    public class Results
    {
        public string token { get; set; }
        public DateTime tokenExpiresOn { get; set; }
        public string refreshToken { get; set; }
    }
}
