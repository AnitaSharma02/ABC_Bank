using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace CB.IBE.Platform.Entities
{
    [DataContract]
    [Serializable]
    public class PaxInfoList
    {
        #region private variables
        List<PaxInfo> lobjPaxInfo = new List<PaxInfo>();
        #endregion

        #region Properties
        /// <summary>
        /// Displays Listof PaxInfo
        /// </summary>
        [DataMember]
        public List<PaxInfo> ListofPaxInfo
        {
            get { return lobjPaxInfo; }
            set { lobjPaxInfo = value; }
        }
        #endregion
    }
}
