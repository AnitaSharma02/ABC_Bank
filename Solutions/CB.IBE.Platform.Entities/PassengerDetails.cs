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
        private int mintId = 0;

        private int mintAirBookingId = 0;

        private int mintPNRId = 0;

        private string mstrPrefix = string.Empty;

        private string mstrFirstName = string.Empty;

        private string mstrMiddleName = string.Empty;

        private string mstrLastName = string.Empty;

        private string mstrFFPNo = string.Empty;

        private DateTime mdatePassportIssueDate = DateTime.MinValue.ToUniversalTime();

        private string mstrNationality = string.Empty;

        private DateTime mdatePassportExpiryDate = DateTime.MinValue.ToUniversalTime();

        private string mstrEmailId = string.Empty;

        private string mstrMobileNo = string.Empty;

        private DateTime mdateDOB = DateTime.MinValue.ToUniversalTime();

        private string mstrTicketNo = string.Empty;

        private string mstrPaxType = string.Empty;

        private string mstrMealPref = string.Empty;

        private string mstrSeatPref = string.Empty;

        private DateTime mdateCreatedDate = DateTime.MinValue.ToUniversalTime();

        private List<FareBreakup> mListOfFareBreakup = new List<FareBreakup>();

        private string mstrPassportNumber = string.Empty;

        private string mstrTelephoneNo = string.Empty;

        private int mintAge = 0;

        private string mstrAddress = string.Empty;

        private string mstrCountry = string.Empty;

        private string mstrGender = string.Empty;

        private string mstrCityName = string.Empty;

        private double mdblBaseFare = 0.0;

        private double mdblMarkup = 0.0;

        private double mdblTax = 0.0;

        private double mdblTotalBaseFare = 0.0;

        private double mdblIBE_Markup = 0.0;

        private double mdblProcessing_fee = 0.0;

        private double mdblTotalDefaultFare = 0.0;

        private string mstrCountryCode = string.Empty;

        [DataMember]
        public int Id
        {
            get
            {
                return mintId;
            }
            set
            {
                mintId = value;
            }
        }

        [DataMember]
        public int AirBookingId
        {
            get
            {
                return mintAirBookingId;
            }
            set
            {
                mintAirBookingId = value;
            }
        }

        [DataMember]
        public int PNRId
        {
            get
            {
                return mintPNRId;
            }
            set
            {
                mintPNRId = value;
            }
        }

        [DataMember]
        public string Prefix
        {
            get
            {
                return mstrPrefix;
            }
            set
            {
                mstrPrefix = value;
            }
        }

        [DataMember]
        public string FirstName
        {
            get
            {
                return mstrFirstName;
            }
            set
            {
                mstrFirstName = value;
            }
        }

        [DataMember]
        public string MiddleName
        {
            get
            {
                return mstrMiddleName;
            }
            set
            {
                mstrMiddleName = value;
            }
        }

        [DataMember]
        public string LastName
        {
            get
            {
                return mstrLastName;
            }
            set
            {
                mstrLastName = value;
            }
        }

        [DataMember]
        public string FFPNo
        {
            get
            {
                return mstrFFPNo;
            }
            set
            {
                mstrFFPNo = value;
            }
        }

        [DataMember]
        public DateTime PassportIssueDate
        {
            get
            {
                return mdatePassportIssueDate;
            }
            set
            {
                mdatePassportIssueDate = value;
            }
        }

        [DataMember]
        public string Nationality
        {
            get
            {
                return mstrNationality;
            }
            set
            {
                mstrNationality = value;
            }
        }

        [DataMember]
        public DateTime PassportExpiryDate
        {
            get
            {
                return mdatePassportExpiryDate;
            }
            set
            {
                mdatePassportExpiryDate = value;
            }
        }

        [DataMember]
        public string EmailId
        {
            get
            {
                return mstrEmailId;
            }
            set
            {
                mstrEmailId = value;
            }
        }

        [DataMember]
        public string MobileNo
        {
            get
            {
                return mstrMobileNo;
            }
            set
            {
                mstrMobileNo = value;
            }
        }

        [DataMember]
        public DateTime DOB
        {
            get
            {
                return mdateDOB;
            }
            set
            {
                mdateDOB = value;
            }
        }

        [DataMember]
        public string TicketNo
        {
            get
            {
                return mstrTicketNo;
            }
            set
            {
                mstrTicketNo = value;
            }
        }

        [DataMember]
        public string PaxType
        {
            get
            {
                return mstrPaxType;
            }
            set
            {
                mstrPaxType = value;
            }
        }

        [DataMember]
        public string MealPref
        {
            get
            {
                return mstrMealPref;
            }
            set
            {
                mstrMealPref = value;
            }
        }

        [DataMember]
        public string SeatPref
        {
            get
            {
                return mstrSeatPref;
            }
            set
            {
                mstrSeatPref = value;
            }
        }

        [DataMember]
        public DateTime CreatedDate
        {
            get
            {
                return mdateCreatedDate;
            }
            set
            {
                mdateCreatedDate = value;
            }
        }

        [DataMember]
        public List<FareBreakup> FareBreakup
        {
            get
            {
                return mListOfFareBreakup;
            }
            set
            {
                mListOfFareBreakup = value;
            }
        }

        [DataMember]
        public string PassportNumber
        {
            get
            {
                return mstrPassportNumber;
            }
            set
            {
                mstrPassportNumber = value;
            }
        }

        [DataMember]
        public string TelephoneNo
        {
            get
            {
                return mstrTelephoneNo;
            }
            set
            {
                mstrTelephoneNo = value;
            }
        }

        [DataMember]
        public string Address
        {
            get
            {
                return mstrAddress;
            }
            set
            {
                mstrAddress = value;
            }
        }

        [DataMember]
        public string Country
        {
            get
            {
                return mstrCountry;
            }
            set
            {
                mstrCountry = value;
            }
        }

        [DataMember]
        public string Gender
        {
            get
            {
                return mstrGender;
            }
            set
            {
                mstrGender = value;
            }
        }

        [DataMember]
        public int Age
        {
            get
            {
                return mintAge;
            }
            set
            {
                mintAge = value;
            }
        }

        [DataMember]
        public string CityName
        {
            get
            {
                return mstrCityName;
            }
            set
            {
                mstrCityName = value;
            }
        }

        [DataMember]
        public double BaseFare
        {
            get
            {
                return mdblBaseFare;
            }
            set
            {
                mdblBaseFare = value;
            }
        }

        [DataMember]
        public double Markup
        {
            get
            {
                return mdblMarkup;
            }
            set
            {
                mdblMarkup = value;
            }
        }

        [DataMember]
        public double Tax
        {
            get
            {
                return mdblTax;
            }
            set
            {
                mdblTax = value;
            }
        }

        [DataMember]
        public double TotalBaseFare
        {
            get
            {
                return mdblTotalBaseFare;
            }
            set
            {
                mdblTotalBaseFare = value;
            }
        }

        [DataMember]
        public double IBE_Markup
        {
            get
            {
                return mdblIBE_Markup;
            }
            set
            {
                mdblIBE_Markup = value;
            }
        }

        [DataMember]
        public double Processing_fee
        {
            get
            {
                return mdblProcessing_fee;
            }
            set
            {
                mdblProcessing_fee = value;
            }
        }

        [DataMember]
        public double TotalDefaultFare
        {
            get
            {
                return mdblTotalDefaultFare;
            }
            set
            {
                mdblTotalDefaultFare = value;
            }
        }

        [DataMember]
        public string CountryCode
        {
            get
            {
                return mstrCountryCode;
            }
            set
            {
                mstrCountryCode = value;
            }
        }
    }
}
