using System;
using System.Runtime.Serialization;

namespace SBL.ETL.Entities
{
    [Serializable]
    [DataContract]
    public class ETLDetails
    {
        [DataMember]
        public string StrData { get; set; }
    }
}
