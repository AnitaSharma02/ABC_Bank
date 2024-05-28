using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace CB.IBE.Platform.Entities
{
    [DataContract]
    [Serializable]
    public class Section
    {
        private string mstrTitle = string.Empty;
        private string mstrText = string.Empty;
        [DataMember]
        public string Title { get { return mstrTitle; } set { mstrTitle = value; } }
        [DataMember]
        public string Text { get { return mstrText; } set { mstrText = value; } }
    }
}

