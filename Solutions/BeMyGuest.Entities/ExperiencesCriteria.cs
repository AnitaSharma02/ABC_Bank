using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BeMyGuest.Entities
{
    public class ExperiencesCriteria
    {
        [DataMember]
        public string searchTerm { get; set; }
        [DataMember]
        public List<string> categoryNames { get; set; }
        [DataMember]
        public List<string> typeNames { get; set; }
    }
}
