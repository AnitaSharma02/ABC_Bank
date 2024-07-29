using System;
using System.Configuration;

namespace IBEAPI.ClientHelper
{
    public class IBEAPIConstant
    {
        public static string IBEWebAPIUrl = Convert.ToString(ConfigurationManager.AppSettings["IBEWebAPIUrl"]);
        public static string EnjoyTravelAPIUrl = Convert.ToString(ConfigurationManager.AppSettings["EnjoyTravelAPIUrl"]);

        #region Flight API

        //GetAllAirField API//
        public static string GetAllAirField = string.Format("{0}flight/GetAllAirField", IBEWebAPIUrl);

        //GetAllCarriers API//
        public static string GetAllCarriers = string.Format("{0}flight/GetAllCarriers", IBEWebAPIUrl);

        //GetAllAirCraftDetails API//
        public static string GetAllAirCraftDetails = string.Format("{0}flight/GetAllAirCraftDetails", IBEWebAPIUrl);

        //AirSearchRequest API//
        public static string AirSearchRequest = string.Format("{0}flight/AirSearchRequest", IBEWebAPIUrl);

        //CreateItinerary API//
        public static string CreateItinerary = string.Format("{0}flight/CreateItinerary", IBEWebAPIUrl);

        //CreateBooking API//
        public static string CreateBooking = string.Format("{0}flight/CreateBooking", IBEWebAPIUrl);

        //GetFlightBookingListForMember API//
        public static string GetFlightBookingListForMember = string.Format("{0}flight/GetFlightBookingListForMember", IBEWebAPIUrl);

        //GetBookedFlightItinerary API//
        public static string GetBookedFlightItinerary = string.Format("{0}flight/GetBookedFlightItinerary", IBEWebAPIUrl);

        #endregion

        #region Hotel API

        //GetAllHotelCities API//
        public static string GetAllHotelCities = string.Format("{0}Hotel/GetAllHotelCities", IBEWebAPIUrl);

        //GetHotelSearchResponse API//
        public static string GetHotelSearchResponse = string.Format("{0}Hotel/GetHotelSearchResponse", IBEWebAPIUrl);

        //HotelReprice API//
        public static string HotelReprice = string.Format("{0}Hotel/HotelReprice", IBEWebAPIUrl);

        //GetHotelInformation API//
        public static string GetHotelInformation = string.Format("{0}Hotel/GetHotelInformation/", IBEWebAPIUrl);

        //GetHotelBookingResponse API//
        public static string GetHotelBookingResponse = string.Format("{0}Hotel/GetHotelBookingResponse", IBEWebAPIUrl);

        //GetMemberBookedHotelInfoList API//
        public static string GetMemberBookedHotelInfoList = string.Format("{0}Hotel/GetMemberBookedHotelInfoList", IBEWebAPIUrl);

        //GetMemberBookedHotelInfo API//
        public static string GetMemberBookedHotelInfo = string.Format("{0}Hotel/GetMemberBookedHotelInfo", IBEWebAPIUrl);
        #endregion

        #region Car API

        public static string GetCountriesList = string.Format("{0}getCountriesList", EnjoyTravelAPIUrl);

        //Bulk API//
        public static string GetLocation = string.Format("{0}bulk", EnjoyTravelAPIUrl);

        public static string GetAvailability = string.Format("{0}availability", EnjoyTravelAPIUrl);
        //availability API//

        //rate API//
        public static string GetRates = string.Format("{0}rate", EnjoyTravelAPIUrl);

        //bookings API//
        public static string CreateCarBooking = string.Format("{0}bookings", EnjoyTravelAPIUrl);
        public static string GetUserBookings= string.Format("{0}getUserBookings", EnjoyTravelAPIUrl);
        public static string GetUserBookingdetails= string.Format("{0}getBookings", EnjoyTravelAPIUrl);
        //bookings API//
        #endregion 
    }
}