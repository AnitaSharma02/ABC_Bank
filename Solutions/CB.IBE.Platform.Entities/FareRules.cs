using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace CB.IBE.Platform.Entities
{
    [DataContract]
    [Serializable]
    public class FareRules
    {
        private Sections mobjSections = new Sections();
        [DataMember]
        public Sections Sections 
        {
            get { return mobjSections; }
            set { mobjSections = value; }
        }
    }
}
