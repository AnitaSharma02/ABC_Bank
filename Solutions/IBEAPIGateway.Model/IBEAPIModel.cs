using CB.IBE.Platform.Car.ClientEntities;
using CB.IBE.Platform.ClientEntities;
using CB.IBE.Platform.Entities;
using CB.IBE.Platform.Hotels.ClientEntities;
using CB.IBE.Platform.IBEClient;
using CB.IBE.Platform.Masters.Entities;
using Core.Platform.Member.Entites;
using Framework.EnterpriseLibrary.Adapters;
using Framework.Integrations.Hotels.Entities;
using IBEAPI.ClientEntities;
using IBEAPI.ClientHelper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace IBEAPIGateway.Model
{
    public class IBEAPIModel
    {
        public RefererDetails GetRefererData()
        {
            try
            {
                RefererDetails lobjRefererDetails = new RefererDetails();
                lobjRefererDetails.Id = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["RefererId"]);
                lobjRefererDetails.UserName = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["RefererName"]);
                string refUserName = Convert.ToString(lobjRefererDetails.UserName);
                lobjRefererDetails.Password = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["RefererPassword"]);
                string refPassword = Convert.ToString(lobjRefererDetails.Password);
                return lobjRefererDetails;
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("RefererData -" + ex.Message + Environment.NewLine + "Stack Trace-" + ex.StackTrace);
                return null;
            }
        }

        #region Flight API Call

        public List<AirField> GetAllAirfields()
        {
            try
            {
                IBEAPIClientHelper lobjIBEAPIClientHelper = new IBEAPIClientHelper();
                return lobjIBEAPIClientHelper.GetAllAirfields();
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("GetAllAirfields -" + ex.Message + Environment.NewLine + "Stack Trace-" + ex.StackTrace);
                return null;
            }

        }

        public List<Carrier> GetAllCarriers()
        {
            try
            {
                IBEAPIClientHelper lobjIBEAPIClientHelper = new IBEAPIClientHelper();
                return lobjIBEAPIClientHelper.GetAllCarriers();
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("GetAllCarriers -" + ex.Message + Environment.NewLine + "Stack Trace-" + ex.StackTrace);
                return null;
            }

        }

        public List<AirCraftDetails> GetAllAirCraftDetails()
        {
            try
            {
                IBEAPIClientHelper lobjIBEAPIClientHelper = new IBEAPIClientHelper();
                return lobjIBEAPIClientHelper.GetAllAirCraftDetails();
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("GetAllAirCraftDetails -" + ex.Message + Environment.NewLine + "Stack Trace-" + ex.StackTrace);
                return null;
            }
        }

        public SearchResponse AirSearchRequest(AirSearchRequest pobjSearchRequest)
        {
            SearchResponse lobjSearchResponse = new SearchResponse();
            try
            {
                IBEAPIClientHelper lobjIBEAPIClientHelper = new IBEAPIClientHelper();
                lobjSearchResponse = lobjIBEAPIClientHelper.AirSearchRequest(pobjSearchRequest);
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("AirSearchRequest -" + ex.Message + Environment.NewLine + "Stack Trace-" + ex.StackTrace);
                return null;
            }
            return lobjSearchResponse;
        }

        public CreateItineraryResponse CreateItinerary(CreateItineraryRequest pobjCreateItineraryRequest)
        {
            CreateItineraryResponse lobjCreateItineraryResponse = new CreateItineraryResponse();
            try
            {
                IBEAPIClientHelper lobjIBEAPIClientHelper = new IBEAPIClientHelper();
                lobjCreateItineraryResponse = lobjIBEAPIClientHelper.CreateItinerary(pobjCreateItineraryRequest);
            }
            catch (Exception ex) { LoggingAdapter.WriteLog("CreateItinerary - " + ex.Message + Environment.NewLine + "Stack Trace-" + ex.StackTrace); }
            return lobjCreateItineraryResponse;
        }

        public BookingResponse CreateBooking(BookingRequest pobjBookingRequest)
        {
            BookingResponse lobjCreateItineraryResponse = new BookingResponse();
            try
            {
                IBEAPIClientHelper lobjIBEAPIClientHelper = new IBEAPIClientHelper();
                lobjCreateItineraryResponse = lobjIBEAPIClientHelper.CreateBooking(pobjBookingRequest);
            }
            catch (Exception ex) { LoggingAdapter.WriteLog("CreateBooking - " + ex.Message + Environment.NewLine + "Stack Trace-" + ex.StackTrace); }
            return lobjCreateItineraryResponse;
        }

        public List<ItineraryDetails> GetFlightBookingListForMember(string pstrMemberId)
        {
            List<ItineraryDetails> lobjItineraryDetails = new List<ItineraryDetails>();
            try
            {
                IBEAPIClientHelper lobjIBEAPIClientHelper = new IBEAPIClientHelper();
                lobjItineraryDetails = lobjIBEAPIClientHelper.GetFlightBookingListForMember(pstrMemberId);
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("GetFlightBookingListForMember -" + ex.Message + Environment.NewLine + "Stack Trace-" + ex.StackTrace);
                return null;
            }
            return lobjItineraryDetails;
        }

        public RetriveItineraryDetails GetBookedFlightItinerary(string pstrTripId)
        {
            RetriveItineraryDetails lobjCreateItineraryResponse = new RetriveItineraryDetails();
            try
            {
                IBEAPIClientHelper lobjIBEAPIClientHelper = new IBEAPIClientHelper();
                lobjCreateItineraryResponse = lobjIBEAPIClientHelper.GetBookedFlightItinerary(pstrTripId);
            }
            catch (Exception ex) { LoggingAdapter.WriteLog("GetBookedFlightItinerary - " + ex.Message + Environment.NewLine + "Stack Trace-" + ex.StackTrace); }
            return lobjCreateItineraryResponse;

        }

        #endregion

        #region Hotel API Call

        public List<string> GetAllHotelCities()
        {
            try
            {
                IBEAPIClientHelper lobjIBEAPIClientHelper = new IBEAPIClientHelper();
                return lobjIBEAPIClientHelper.GetAllHotelCities();
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("GetAllHotelCities -" + ex.Message + Environment.NewLine + "Stack Trace-" + ex.StackTrace);
                return null;
            }
        }

        public HotelSearchResponse GetHotelSearchResponse(HotelsSearchRequest pobjSearchRequest)
        {
            HotelSearchResponse lobjHotelSearchResponse = new HotelSearchResponse();
            try
            {
                IBEAPIClientHelper lobjIBEAPIClientHelper = new IBEAPIClientHelper();
                lobjHotelSearchResponse = lobjIBEAPIClientHelper.GetHotelSearchResponse(pobjSearchRequest);
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("GetHotelSearchResponse -" + ex.Message + Environment.NewLine + "Stack Trace-" + ex.StackTrace);
            }
            return lobjHotelSearchResponse;
        }

        public HotelInformationResponse GetHotelInformation(string pstrHotelId)
        {
            HotelInformationResponse lobjHotelInformationResponse = null;
            try
            {
                IBEAPIClientHelper lobjIBEAPIClientHelper = new IBEAPIClientHelper();
                lobjHotelInformationResponse = lobjIBEAPIClientHelper.GetHotelInformation(pstrHotelId);
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("GetHotelInformation - " + ex.Message + Environment.NewLine + "Stack Trace-" + ex.StackTrace);
            }
            return lobjHotelInformationResponse;
        }

        public HotelRepriceResponse HotelReprice(HotelRepriceRequest pobjHotelRepriceRequest)
        {
            try
            {
                IBEAPIClientHelper lobjIBEAPIClientHelper = new IBEAPIClientHelper();
                return lobjIBEAPIClientHelper.HotelReprice(pobjHotelRepriceRequest);
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("HotelReprice :" + ex.Message + Environment.NewLine + "Stack Trace :" + ex.StackTrace + Environment.NewLine + "InnerException:" + ex.InnerException);
                return null;
            }
        }

        public HotelBookingResponse GetHotelBookingResponse(MemberDetails pobjMemberDetails, Hotel pobjHotel, HotelsSearchRequest pobjSearchRequest, Customer pobjCustomer, CB.IBE.Platform.Masters.Entities.BookingPaymentDetails pobjBookingPaymentDetails)
        {
            try
            {
                List<int> lobjListOfAdult = new List<int>();
                string[] arrayAdultPerRoom = pobjSearchRequest.AdultPerRoom.Split(',');
                for (int i = 0; i < arrayAdultPerRoom.Count(); i++)
                {
                    lobjListOfAdult.Add(Convert.ToInt32(arrayAdultPerRoom[i]));
                }
                List<int> lobjListOfChild = new List<int>();
                string[] arrayChildPerRoom = pobjSearchRequest.ChildrenPerRoom.Split(',');
                for (int i = 0; i < arrayChildPerRoom.Count(); i++)
                {
                    lobjListOfChild.Add(Convert.ToInt32(arrayChildPerRoom[i]));
                }
                HotelBookRequest lobjBookingRequest = new HotelBookRequest();
                lobjBookingRequest.Customer = pobjCustomer;
                lobjBookingRequest.CheckInDate = Convert.ToString(pobjSearchRequest.CheckInDate);
                lobjBookingRequest.CheckOutDate = Convert.ToString(pobjSearchRequest.CheckOutDate);
                lobjBookingRequest.NoOfRooms = pobjSearchRequest.NoOfRooms;
                lobjBookingRequest.SearchId = pobjSearchRequest.SearchId;
                lobjBookingRequest.AdultPerRoom = Convert.ToString(lobjListOfAdult.Count());
                lobjBookingRequest.ChildrenPerRoom = Convert.ToString(lobjListOfChild.Count());
                //lobjBookingRequest.bookingcode = pobjHotel.roomrates.RoomRate[0].bookingcode;
                //lobjBookingRequest.roomtypecode = pobjHotel.roomrates.RoomRate[0].roomtype.roomtypecode;
                //lobjBookingRequest.TotalPoints = pobjHotel.roomrates.RoomRate[0].TotalPoints;
                //lobjBookingRequest.customeripaddress = pobjSearchRequest.IpAddress;
                lobjBookingRequest.Hotel = pobjHotel;
                //lobjBookingRequest.bookingamount = Convert.ToDouble(pobjHotel.roomrates.RoomRate[0].TotalDefaultAmount);
                //lobjBookingRequest.totalBaseFare = Convert.ToDouble(pobjHotel.roomrates.RoomRate[0].TotalBaseAmount);
                //lobjBookingRequest.totalDefaulFare = Convert.ToDouble(pobjHotel.roomrates.RoomRate[0].TotalDefaultAmount);
                //lobjBookingRequest.BookingPaymentDetails = pobjBookingPaymentDetails;
                pobjSearchRequest.MembershipReference = pobjMemberDetails.MemberRelationsList[0].RelationReference;

                IBEAPIClientHelper lobjIBEAPIClientHelper = new IBEAPIClientHelper();
                return lobjIBEAPIClientHelper.GetHotelBookingResponse(lobjBookingRequest);

            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("Model GetHotelBookingResponse Ex-:" + ex.Message + Environment.NewLine + "Stack Trace-" + ex.StackTrace);
                throw new ApplicationException(ex.Message);
            }
        }

        public GetHotelInfoDetails GetMemberBookedHotelInfoList(string pstrMembershipReference)
        {
            GetHotelInfoDetails lobjGetHotelInfoDetails = new GetHotelInfoDetails();
            try
            {
                IBEAPIClientHelper lobjIBEAPIClientHelper = new IBEAPIClientHelper();
                lobjGetHotelInfoDetails = lobjIBEAPIClientHelper.GetMemberBookedHotelInfoList(pstrMembershipReference);
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("GetMemberBookedHotelInfoList - " + ex.Message + Environment.NewLine + "Stack Trace-" + ex.StackTrace);
            }
            return lobjGetHotelInfoDetails;
        }

        public HotelItineraryResponse GetMemberBookedHotelInfo(string pstrTransactionRef)
        {
            HotelItineraryResponse lobjHotelItineraryResponse = new HotelItineraryResponse();
            try
            {
                IBEAPIClientHelper lobjIBEAPIClientHelper = new IBEAPIClientHelper();
                lobjHotelItineraryResponse = lobjIBEAPIClientHelper.GetMemberBookedHotelInfo(pstrTransactionRef);
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("GetMemberBookedHotelInfo -" + ex.Message + Environment.NewLine + "Stack Trace-" + ex.StackTrace);
            }
            return lobjHotelItineraryResponse;
        }

        #endregion

        #region Car API Call

        public CarCountryResponse GetCountriesList()
        {
            CarCountryResponse lobjResponse = null;
            IBEAPIClientHelper lobjIBEAPIClientHelper = new IBEAPIClientHelper();
            try
            {
                lobjResponse = lobjIBEAPIClientHelper.GetCountriesList();
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("Model GetCountriesList Ex-:" + ex.InnerException + ex.StackTrace + ex.Message);
            }
            return lobjResponse;
        }

        public AvailabilityResponse GetAvailability(GetAvailabilityRequest lobjGetAvailabilityRequest)
        {
            AvailabilityResponse lobjResponse = null;
            IBEAPIClientHelper lobjIBEAPIClientHelper = new IBEAPIClientHelper();
            try
            {
                lobjResponse = lobjIBEAPIClientHelper.GetAvailability(lobjGetAvailabilityRequest);
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("Model GetAvailability Ex-:" + ex.InnerException + ex.StackTrace + ex.Message);
            }
            return lobjResponse;
        }

        public BulkResponse GetLocations()
        {
            BulkResponse lobjResponse = null;
            IBEAPIClientHelper lobjIBEAPIClientHelper = new IBEAPIClientHelper();
            try
            {
                lobjResponse = lobjIBEAPIClientHelper.GetLocation();
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("Model GetLocations Ex-:" + ex.InnerException + ex.StackTrace + ex.Message);
            }
            return lobjResponse;
        }

        public RateResponse GetRates(RateRequest lobjRateRequest)
        {
            RateResponse lobjResponse = null;
            IBEAPIClientHelper lobjIBEAPIClientHelper = new IBEAPIClientHelper();
            try
            {
                lobjResponse = lobjIBEAPIClientHelper.GetRates(lobjRateRequest);
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("Model GetRates Ex-:" + ex.InnerException + ex.StackTrace + ex.Message);
            }
            return lobjResponse;
        }

        public CarBookingResponse CreateCarBooking(CarBookingRequest lobjCarBookingRequest)
        {
            CarBookingResponse lobjResponse = null;
            IBEAPIClientHelper lobjIBEAPIClientHelper = new IBEAPIClientHelper();
            try
            {
                lobjResponse = lobjIBEAPIClientHelper.CreateCarBooking(lobjCarBookingRequest);
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("Model CreateCarBooking Ex-:" + ex.InnerException + ex.StackTrace + ex.Message);
            }
            return lobjResponse;
        }

        public UserBookingResponse GetUserBookings(UserBookingRequest lobjUserBookingRequest)
        {
            UserBookingResponse lobjResponse = null;
            IBEAPIClientHelper lobjIBEAPIClientHelper = new IBEAPIClientHelper();
            try
            {
                lobjResponse = lobjIBEAPIClientHelper.GetUserBookings(lobjUserBookingRequest);
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("Model GetUserBookings Ex-:" + ex.InnerException + ex.StackTrace + ex.Message);
            }
            return lobjResponse;
        }

        public CarBookingDetails GetCarBookingDetailsbyRefId(string pstrBookingRefId, string EnjoyTraveldisplayCurrency)
        {
            try
            {

                IBEAPIClientHelper lobjClient = new IBEAPIClientHelper();
                return lobjClient.GetMemberBookedCar(pstrBookingRefId, EnjoyTraveldisplayCurrency);
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("GetBookedHotelInfo -" + ex.Message + Environment.NewLine + "Stack Trace-" + ex.StackTrace);
                return null;
            }
        }

        #endregion

        public DateTime StringToDateTime(string pstrDate)
        {
            string lstrDateTotime = ConfigurationManager.AppSettings["IBEDateTimeFormat"];

            DateTime dt = DateTime.ParseExact(pstrDate, lstrDateTotime, null);
            return dt;
        }

    }
}