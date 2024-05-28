using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace SBL.ETL.WebAPI.Helper.Response
{
    [DataContract]
    [Serializable]
    internal class TokenAPIResponse
    {
        [DataMember]
        public TokenModel results { get; set; }
    }

    [DataContract]
    [Serializable]
    public class TokenModel
    {
        [DataMember]
        public string Token { get; set; } = string.Empty;

        [DataMember]
        public DateTime TokenExpiresOn { get; set; }

        //public string RefreshToken { get; set; } = string.Empty;
    }
}
