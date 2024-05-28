using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace CB.IBE.Platform.Entities
{
    [DataContract]
    [Serializable]
    public class Sections
    {
        private List<Section> mobjListOfSection = new List<Section>();
        [DataMember]
        public List<Section> Section 
        {
            get { return mobjListOfSection; }
            set { mobjListOfSection = value; }
        }
    }
}
