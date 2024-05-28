using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
namespace CB.IBE.Platform.Masters.Entities
{
    [DataContract]
    [Serializable]
    public class RefererDetails
    {

        #region Private Variables

        int mintId = 0;
        string mstrUserName = string.Empty;
        string mstrPassword = string.Empty;

        bool mboolIsActive = false;
        DateTime mdateCreatedDate = DateTime.MinValue.ToUniversalTime();
        string mstrCreatedBy = string.Empty;
        DateTime mdateUpdatedDate = DateTime.MinValue.ToUniversalTime();
        string mstrUpdatedBy = string.Empty;
        string mstrBaseCurrency = string.Empty;
        int msintAccount_Id = 0;
        float mdecPointRate = 0.0f;
        float mdecVirtualCredit = 0.0f;
        bool mboolIsDepositAccountConfigured = false;
        List<RefererMarkup> lobjListRefererMarkup = new List<RefererMarkup>();
        RefererSupplierProperties lobjRefererSupplierProperties = new RefererSupplierProperties();
        double mdblInvoiceROE = 0.0f;
        double mdblInvoiceRateUSD = 0.0f;
        int mintMID = 0;
        string mstrC2BShortCode = string.Empty;

        #endregion

        #region Poperties
        /// <summary>
        /// Displays ListRefererMarkup Details
        /// </summary>
        [DataMember]
        public List<RefererMarkup> ListRefererMarkup
        {
            get { return lobjListRefererMarkup; }
            set { lobjListRefererMarkup = value; }
        }

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
        /// Displays User Name
        /// </summary>
        [DataMember]
        public string UserName
        {
            get { return mstrUserName; }
            set { mstrUserName = value; }
        }
        /// <summary>
        /// Displays Password
        /// </summary>
        [DataMember]
        public string Password
        {
            get { return mstrPassword; }
            set { mstrPassword = value; }
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
        /// Displays Created Date
        /// </summary>
        [DataMember]
        public DateTime CreatedDate
        {
            get { return mdateCreatedDate; }
            set { mdateCreatedDate = value; }
        }
        /// <summary>
        /// Displays Created By
        /// </summary>
        [DataMember]
        public string CreatedBy
        {
            get { return mstrCreatedBy; }
            set { mstrCreatedBy = value; }
        }
        /// <summary>
        /// Displays Updated Date
        /// </summary>
        [DataMember]
        public DateTime UpdatedDate
        {
            get { return mdateUpdatedDate; }
            set { mdateUpdatedDate = value; }
        }
        /// <summary>
        /// Displays Updated By
        /// </summary>
        [DataMember]
        public string UpdatedBy
        {
            get { return mstrUpdatedBy; }
            set { mstrUpdatedBy = value; }
        }
        /// <summary>
        /// Displays Base Currency
        /// </summary>
        [DataMember]
        public string BaseCurrency
        {
            get { return mstrBaseCurrency; }
            set { mstrBaseCurrency = value; }
        }
        /// <summary>
        /// Displays Account_Id
        /// </summary>
        [DataMember]
        public int Account_Id
        {
            get { return msintAccount_Id; }
            set { msintAccount_Id = value; }
        }

        /// <summary>
        /// Displays Point Rate
        /// </summary>
        [DataMember]
        public float PointRate
        {
            get { return mdecPointRate; }
            set { mdecPointRate = value; }
        }

        /// <summary>
        /// Displays Virtual Credit
        /// </summary>
        [DataMember]
        public float VirtualCredit
        {
            get { return mdecVirtualCredit; }
            set { mdecVirtualCredit = value; }
        }

        /// <summary>
        /// Displays Is Deposit Account Configured
        /// </summary>
        [DataMember]
        public bool IsDepositAccountConfigured
        {
            get { return mboolIsDepositAccountConfigured; }
            set { mboolIsDepositAccountConfigured = value; }
        }

        /// <summary>
        /// Displays Invoice ROE
        /// </summary>
        [DataMember]
        public double InvoiceROE
        {
            get { return mdblInvoiceROE; }
            set { mdblInvoiceROE = value; }
        }

        /// <summary>
        /// Displays Invoice Rate USD
        /// </summary>
        [DataMember]
        public double InvoiceRateUSD
        {
            get { return mdblInvoiceRateUSD; }
            set { mdblInvoiceRateUSD = value; }
        }
        /// <summary>
        /// Displays RefererSupplierProperties Details
        /// </summary>
        [DataMember]
        public RefererSupplierProperties RefererSupplierProperties
        {
            get { return lobjRefererSupplierProperties; }
            set { lobjRefererSupplierProperties = value; }
        }

        /// <summary>
        /// Displays MID
        /// </summary>
        [DataMember]
        public int MID
        {
            get { return mintMID; }
            set { mintMID = value; }
        }
        /// <summary>
        /// Displays C2BShortCode
        /// </summary>
        [DataMember]
        public string C2BShortCode
        {
            get { return mstrC2BShortCode; }
            set { mstrC2BShortCode = value; }
        }

        #endregion

    }
}
