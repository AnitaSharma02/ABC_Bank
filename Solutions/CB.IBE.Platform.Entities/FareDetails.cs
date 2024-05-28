using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace CB.IBE.Platform.Entities
{
    [DataContract]
    [Serializable]
    public class FareDetails
    {
        #region Private Variables

        int mintTotalDefaultFare = 0;
        int mintTotalBaseFare = 0;
        int mintTotalPoints = 0;
        int mintMarkup = 0;
        int mintTaxes = 0;
        int mintBasFare = 0;
        int mintFees = 0;
        //int mintDiscount = 0;
        int mintCashBack = 0;
        float mfltROE = 0.0f;
        float mfltDiscountedAmount = 0.0f;
        float mfltDiscount = 0.0f;
        float mfltTotalMarkup = 0;
        int mintActualPoints = 0;
        #endregion

        #region Properties

        /// <summary>
        /// Displays Total Default Fare
        /// </summary>
        [DataMember]
        public int TotalDefaultFare
        {
            get { return mintTotalDefaultFare; }
            set { mintTotalDefaultFare = value; }
        }
        /// <summary>
        /// Displays Total Base Fare
        /// </summary>
        [DataMember]
        public int TotalBaseFare
        {
            get { return mintTotalBaseFare; }
            set { mintTotalBaseFare = value; }
        }
        /// <summary>
        /// Displays Total Points
        /// </summary>
        [DataMember]
        public int TotalPoints
        {
            get { return mintTotalPoints; }
            set { mintTotalPoints = value; }
        }

        /// <summary>
        /// Displays Total Markup
        /// </summary>
        [DataMember]
        public int Markup
        {
            get { return mintMarkup; }
            set { mintMarkup = value; }
        }

        /// <summary>
        /// Displays Taxes
        /// </summary>
        [DataMember]
        public int Taxes
        {
            get { return mintTaxes; }
            set { mintTaxes = value; }
        }

        /// <summary>
        /// Displays Base Fare
        /// </summary>
        [DataMember]
        public int BaseFare
        {
            get { return mintBasFare; }
            set { mintBasFare = value; }
        }

        /// <summary>
        /// Displays Fees
        /// </summary>
        [DataMember]
        public int Fees
        {
            get { return mintFees; }
            set { mintFees = value; }
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
        /// Displays CashBack
        /// </summary>
        [DataMember]
        public int CashBack
        {
            get { return mintCashBack; }
            set { mintCashBack = value; }
        }
        /// <summary>
        /// Displays ROE
        /// </summary>
        [DataMember]
        public float ROE
        {
            get { return mfltROE; }
            set { mfltROE = value; }
        }
        /// <summary>
        /// Displays Discounted Amount
        /// </summary>
        [DataMember]
        public float DiscountedAmount
        {
            get { return (TotalBaseFare - Discount); }
            set { }
        }
        /// <summary>
        /// Displays Total Default Fare
        /// </summary>
        [DataMember]
        public float TotalMarkup
        {
            get { return mfltTotalMarkup; }
            set { mfltTotalMarkup = value; }
        }
        /// <summary>
        /// Displays CashBack
        /// </summary>
        [DataMember]
        public int ActualPoints
        {
            get { return mintActualPoints; }
            set { mintActualPoints = value; }
        }
        #endregion
    }
}
