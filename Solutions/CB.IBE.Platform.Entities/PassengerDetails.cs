using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace CB.IBE.Platform.Entities
{
    [DataContract]
    [Serializable]
    public class PassengerDetails
    {

        #region Private Variables

        int mintId = 0;
        int mintAirBookingId = 0;
        int mintPNRId = 0;
        string mstrPrefix = string.Empty;
        string mstrFirstName = string.Empty;
        string mstrMiddleName = string.Empty;
        string mstrLastName = string.Empty;
        string mstrFFPNo = string.Empty;
        DateTime mdatePassportIssueDate = DateTime.MinValue.ToUniversalTime();
        string mstrNationality = string.Empty;
        DateTime mdatePassportExpiryDate = DateTime.MinValue.ToUniversalTime();
        string mstrEmailId = string.Empty;
        string mstrMobileNo = string.Empty;
        DateTime mdateDOB = DateTime.MinValue.ToUniversalTime();
        string mstrTicketNo = string.Empty;
        string mstrPaxType = string.Empty;
        string mstrMealPref = string.Empty;
        string mstrSeatPref = string.Empty;
        DateTime mdateCreatedDate = DateTime.MinValue.ToUniversalTime();
        List<FareBreakup> mListOfFareBreakup = new List<FareBreakup>();
        string mstrPassportNumber = string.Empty;
        string mstrTelephoneNo = string.Empty;
        int mintAge = 0;
        string mstrAddress = string.Empty;
        string mstrCountry = string.Empty;
        string mstrGender = string.Empty;
        string mstrCityName = string.Empty;
        double mdblBaseFare = 0;
        double mdblMarkup = 0;
        double mdblTax = 0;
        double mdblTotalBaseFare = 0;
        double mdblIBE_Markup = 0;
        double mdblProcessing_fee = 0;
        double mdblTotalDefaultFare = 0;
        #endregion

        #region Properties
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
        /// Displays AirBookingId
        /// </summary>  
        [DataMember]
        public int AirBookingId
        {
            get { return mintAirBookingId; }
            set { mintAirBookingId = value; }
        }

        /// <summary>
        /// Displays PNRId
        /// </summary>
        [DataMember]
        public int PNRId
        {
            get { return mintPNRId; }
            set { mintPNRId = value; }
        }

        /// <summary>
        /// Displays Prefix
        /// </summary>
        [DataMember]
        public string Prefix
        {
            get { return mstrPrefix; }
            set { mstrPrefix = value; }
        }

        /// <summary>
        /// Displays FirstName
        /// </summary>
        [DataMember]
        public string FirstName
        {
            get { return mstrFirstName; }
            set { mstrFirstName = value; }
        }

        /// <summary>
        /// Displays MiddleName
        /// </summary>
        [DataMember]
        public string MiddleName
        {
            get { return mstrMiddleName; }
            set { mstrMiddleName = value; }
        }

        /// <summary>
        /// Displays LastName
        /// </summary>
        [DataMember]
        public string LastName
        {
            get { return mstrLastName; }
            set { mstrLastName = value; }
        }

        /// <summary>
        /// Displays FFPNo
        /// </summary>
        [DataMember]
        public string FFPNo
        {
            get { return mstrFFPNo; }
            set { mstrFFPNo = value; }
        }

        /// <summary>
        /// Displays PassportIssueDate
        /// </summary>
        [DataMember]
        public DateTime PassportIssueDate
        {
            get { return mdatePassportIssueDate; }
            set { mdatePassportIssueDate = value; }
        }

        /// <summary>
        /// Displays Nationality
        /// </summary>
        [DataMember]
        public string Nationality
        {
            get { return mstrNationality; }
            set { mstrNationality = value; }
        }

        /// <summary>
        /// Displays Passport Expiry Date
        /// </summary>
        [DataMember]
        public DateTime PassportExpiryDate
        {
            get { return mdatePassportExpiryDate; }
            set { mdatePassportExpiryDate = value; }
        }

        /// <summary>
        /// Displays Email Id
        /// </summary>
        [DataMember]
        public string EmailId
        {
            get { return mstrEmailId; }
            set { mstrEmailId = value; }
        }

        /// <summary>
        /// Displays Mobile No
        /// </summary>
        [DataMember]
        public string MobileNo
        {
            get { return mstrMobileNo; }
            set { mstrMobileNo = value; }
        }

        /// <summary>
        /// Displays Date Of Birth
        /// </summary>
        [DataMember]
        public DateTime DOB
        {
            get { return mdateDOB; }
            set { mdateDOB = value; }
        }

        /// <summary>
        /// Displays TicketNo
        /// </summary>
        [DataMember]
        public string TicketNo
        {
            get { return mstrTicketNo; }
            set { mstrTicketNo = value; }
        }

        /// <summary>
        /// Displays PaxType
        /// </summary>
        [DataMember]
        public string PaxType
        {
            get { return mstrPaxType; }
            set { mstrPaxType = value; }
        }

        /// <summary>
        /// Displays MealPref
        /// </summary>
        [DataMember]
        public string MealPref
        {
            get { return mstrMealPref; }
            set { mstrMealPref = value; }
        }

        /// <summary>
        /// Displays SeatPref
        /// </summary>
        [DataMember]
        public string SeatPref
        {
            get { return mstrSeatPref; }
            set { mstrSeatPref = value; }
        }

        /// <summary>
        /// Displays CreatedDate
        /// </summary>
        [DataMember]
        public DateTime CreatedDate
        {
            get { return mdateCreatedDate; }
            set { mdateCreatedDate = value; }
        }

        /// <summary>
        /// Displays Fare Breakup
        /// </summary>
        [DataMember]
        public List<FareBreakup> FareBreakup
        {
            get { return mListOfFareBreakup; }
            set { mListOfFareBreakup = value; }
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
        /// <summary>
        /// Displays Telephone No
        /// </summary>
        [DataMember]
        public string TelephoneNo
        {
            get { return mstrTelephoneNo; }
            set { mstrTelephoneNo = value; }
        }
        /// <summary>
        /// Displays Address
        /// </summary>
        [DataMember]
        public string Address
        {
            get { return mstrAddress; }
            set { mstrAddress = value; }
        }
        /// <summary>
        /// Displays Country
        /// </summary>
        [DataMember]
        public string Country
        {
            get { return mstrCountry; }
            set { mstrCountry = value; }
        }
        /// <summary>
        /// Displays Gender
        /// </summary>
        [DataMember]
        public string Gender
        {
            get { return mstrGender; }
            set { mstrGender = value; }
        }

        /// <summary>
        /// Displays Age
        /// </summary>
        [DataMember]
        public int Age
        {
            get { return mintAge; }
            set { mintAge = value; }
        }

        /// <summary>
        /// Displays CityName
        /// </summary>
        [DataMember]
        public string CityName
        {
            get { return mstrCityName; }
            set { mstrCityName = value; }
        }
        /// <summary>
        /// Displays Base Fare
        /// </summary>
        [DataMember]
        public double BaseFare
        {
            get { return mdblBaseFare; }
            set { mdblBaseFare = value; }
        }
        /// <summary>
        /// Displays Markup Amount
        /// </summary>
        [DataMember]
        public double Markup
        {
            get { return mdblMarkup; }
            set { mdblMarkup = value; }
        }

        /// <summary>
        /// Displays Tax
        /// </summary>
        [DataMember]
        public double Tax
        {
            get { return mdblTax; }
            set { mdblTax = value; }
        }
        /// <summary>
        /// Displays TotalBaseFare
        /// </summary>
        [DataMember]
        public double TotalBaseFare
        {
            get { return mdblTotalBaseFare; }
            set { mdblTotalBaseFare = value; }
        }
        /// <summary>
        /// Displays IBE_Markup
        /// </summary>
        [DataMember]
        public double IBE_Markup
        {
            get { return mdblIBE_Markup; }
            set { mdblIBE_Markup = value; }
        }
        /// <summary>
        /// Displays Processing_fee
        /// </summary>
        [DataMember]
        public double Processing_fee
        {
            get { return mdblProcessing_fee; }
            set { mdblProcessing_fee = value; }
        }
        /// <summary>
        /// Displays TotalDefaultFare
        /// </summary>
        [DataMember]
        public double TotalDefaultFare
        {
            get { return mdblTotalDefaultFare; }
            set { mdblTotalDefaultFare = value; }
        }
        #endregion
    }
}
