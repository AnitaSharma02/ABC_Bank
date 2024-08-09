using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BeMyGuest.Entities
{
    public class SearchExperiencesModel
    {
        [DataMember]
        public ExperiencesTypesAndCategory TypesAndCategory { get; set; }
        [DataMember]
        public string searchTerm { get; set; }
    }
}
