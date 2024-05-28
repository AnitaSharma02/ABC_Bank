using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;

namespace ABC.Model
{
    public class CustomerDetail
    {
        public string PersonalTitle { get; set; }
        public string FirstName { get; set; }
        public string Lastname { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Address { get; set; }
        public string Address2 { get; set; }
        public string Country { get; set; }
        public string PostalCode { get; set; }
        public string PhoneNo { get; set; }
        public string MobileNo { get; set; }
        public string EmailID { get; set; }
    }

    [Serializable]
    [DataContract]
    public class MemberLocalAttrDetails
    {
        [JsonProperty("National Identity")]
        public string NationalIdentity { get; set; } = string.Empty;
        [JsonProperty("Passport Number")]
        public string PassportNumber { get; set; } = string.Empty;
        [JsonProperty("Citizenship No")]
        public string CitizenshipNo { get; set; } = string.Empty;
        [JsonProperty("Registration No")]
        public string RegistrationNo { get; set; } = string.Empty;
        [JsonProperty("Pan No")]
        public string PanNo { get; set; } = string.Empty;
        [JsonProperty("Province")]
        public string Province { get; set; } = string.Empty;
        [JsonProperty("District")]
        public string District { get; set; } = string.Empty;
        public string Branch { get; set; } = string.Empty;
        [JsonProperty("Preferred Language")]
        public string PreferredLanguage { get; set; } = string.Empty;
        [JsonProperty("Additional fields 1")]
        public string Additionalfields1 { get; set; } = string.Empty;
        [JsonProperty("Additional field")]
        public string Additionalfield { get; set; } = string.Empty;
        [JsonProperty("Execution Date")]
        public string ExecutionDate { get; set; } = string.Empty;
        public string CustomerSegment { get; set; } = string.Empty;
        public string CustomerType { get; set; } = string.Empty;
        [JsonProperty("Nationality")]
        public string Nationality { get; set; } = string.Empty;

        [JsonProperty("User Name")]
        public string UserName { get; set; } = string.Empty;
    }
}