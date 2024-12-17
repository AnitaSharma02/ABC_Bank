using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BeMyGuest.Entities
{
    public class ExperiencesResponse
    {
        [DataMember]
        public int success { get; set; }
        [DataMember]
        public List<ExperiencesData> data { get; set; }
        [DataMember]
        public ProductMeta meta { get; set; }
        [DataMember]
        public ExperiencesCriteria ExperiencesCriteria { get; set; }


    }
}
