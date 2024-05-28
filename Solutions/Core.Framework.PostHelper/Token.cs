using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Framework.PostHelper
{
    public class Token
    {
        [JsonProperty("token")]
        public string AccessToken { get; set; }

        [JsonProperty("refreshToken")]
        public string RefreshToken { get; set; }

        [JsonProperty("tokenExpiresOn")]
        public string ExpiresOn { get; set; }
    }
}
