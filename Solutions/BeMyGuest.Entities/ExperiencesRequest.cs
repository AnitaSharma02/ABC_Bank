using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BeMyGuest.Entities
{
    public class ExperiencesRequest
    {
        [DataMember]
        public string search_term { get; set; }
        [DataMember]
        public List<string> type_name { get; set; }
        [DataMember]
        public List<string> category { get; set; }
        [DataMember]
        public int page { get; set; }
        [DataMember]
        public int per_page { get; set; }
    }
}
