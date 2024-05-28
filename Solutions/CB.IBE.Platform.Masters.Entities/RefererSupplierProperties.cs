using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace CB.IBE.Platform.Masters.Entities
{
    [DataContract]
    [Serializable]
    public class RefererSupplierProperties
    {

        int mintId = 0;
        int mintRefererId = 0;
        int mintSupplierId = 0;
        ServiceType menumServiceType;
        float mdecROE = 0.0f;
        float mfltDiscount = 0.0f;
        bool mboolIsPercentage = false;
        DiscountType menumDiscountType;
        MarkupType menumMarkupType; 
        bool mboolIsActive = false;
        /// <summary>
        /// Displays Id
        /// </summary>
        [DataMember]
        public int Id
        {
            get { return mintId; }
            set { mintId = value; }
        }
        /// <summary>
        /// Displays Discount
        /// </summary>
        [DataMember]
        public float Discount
        {
            get { return mfltDiscount; }
            set { mfltDiscount = value; }
        }

        /// <summary>
        /// Displays Is Percentage 
        /// </summary>
        [DataMember]
        public bool IsPercentage
        {
            get { return mboolIsPercentage; }
            set { mboolIsPercentage = value; }
        }
        /// <summary>
        ///  Discount Type 
        /// </summary>
        [DataMember]
        public DiscountType DiscountType
        {
            get { return menumDiscountType; }
            set { menumDiscountType = value; }
        }
        /// <summary>
        /// Displays RefererId
        /// </summary>
        [DataMember]
        public int RefererId
        {
            get { return mintRefererId; }
            set { mintRefererId = value; }
        }
        /// <summary>
        /// Displays SupplierId
        /// </summary>
        [DataMember]
        public int SupplierId
        {
            get { return mintSupplierId; }
            set { mintSupplierId = value; }
        }
        /// <summary>
        /// Service Type
        /// </summary>
        [DataMember]
        public ServiceType ServiceType
        {
            get { return menumServiceType; }
            set { menumServiceType = value; }
        }
        /// <summary>
        /// Displays ROE
        /// </summary>
        [DataMember]
        public float ROE
        {
            get { return mdecROE; }
            set { mdecROE = value; }
        }
        /// <summary>
        /// Displays Is Active
        /// </summary>
        [DataMember]
        public bool IsActive
        {
            get { return mboolIsActive; }
            set { mboolIsActive = value; }
        }
        /// <summary>
        /// Markup Type
        /// </summary>
        [DataMember]
        public MarkupType MarkupType
        {
            get { return menumMarkupType; }
            set { menumMarkupType = value; }
        }
    }
}
