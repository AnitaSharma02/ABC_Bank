using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace CB.IBE.Platform.Entities
{
    [DataContract]
    [Serializable]
    public class PaxInfo
    {
        #region private variables

        string mstrFirstName = string.Empty;
        string mstrLastName = string.Empty;
        string mstrTitle = string.Empty;
        string mstrType = string.Empty;
        string mstrPassportNumber = string.Empty;

        #endregion

        #region Properties
        /// <summary>
        /// Displays First Name
        /// </summary>
        [DataMember]
        public string FirstName
        {
            get { return mstrFirstName; }
            set { mstrFirstName = value; }
        }

        /// <summary>
        /// Displays Last Name
        /// </summary>
        [DataMember]
        public string LastName
        {
            get { return mstrLastName; }
            set { mstrLastName = value; }
        }
        /// <summary>
        /// Displays Title
        /// </summary>
        [DataMember]
        public string Title
        {
            get { return mstrTitle; }
            set { mstrTitle = value; }
        }
        /// <summary>
        /// Displays Type
        /// </summary>
        [DataMember]
        public string Type
        {
            get { return mstrType; }
            set { mstrType = value; }
        }
        /// <summary>
        /// Displays PassportNumber
        /// </summary>
        [DataMember]
        public string PassportNumber
        {
            get { return mstrPassportNumber; }
            set { mstrPassportNumber = value; }
        }
        #endregion

    }
}
