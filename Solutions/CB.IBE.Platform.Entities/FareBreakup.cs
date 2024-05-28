using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace CB.IBE.Platform.Entities
{
    [DataContract]
    [Serializable]
    public class FareBreakup
    {
        #region Private Variables

        int mintId = 0;
        int mintPassengerId = 0;
        string mstrCategory = string.Empty;
        string mstrCode = string.Empty;
        double mdblAmount = 0;
        
        #endregion

        #region Properties
        /// <summary>
        /// Displays FareBreakup Id
        /// </summary>
        [DataMember]
        public int Id
        {
            get { return mintId; }
            set { mintId = value; }
        }
        /// <summary>
        /// Displays Passenger Id
        /// </summary>
        [DataMember]
        public int PassengerId
        {
            get { return mintPassengerId; }
            set { mintPassengerId = value; }
        }
        /// <summary>
        /// Displays Category
        /// </summary>
        [DataMember]
        public string Category
        {
            get { return mstrCategory; }
            set { mstrCategory = value; }
        }
        /// <summary>
        /// Displays Code
        /// </summary>
        [DataMember]
        public string Code
        {
            get { return mstrCode; }
            set { mstrCode = value; }
        }
    
        /// <summary>
        /// Displays Amount
        /// </summary>
        [DataMember]
        public double Amount
        {
            get { return mdblAmount; }
            set { mdblAmount = value; }
        }

        
        #endregion
    }
}
