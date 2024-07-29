using CB.IBE.Platform.Car.ClientEntities;
using CB.IBE.Platform.ClientEntities;
using CB.IBE.Platform.Entities;
using CB.IBE.Platform.Hotels.ClientEntities;
using Framework.EnterpriseLibrary.Adapters;
using IBEAPI.ClientEntities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace IBEAPI.ClientHelper
{
    public class IBEAPIClientHelper
    {
        public string lstrFlightLogCategory = "IBE Flight Tracing";
        public string lstrHotelLogCategory = "IBE Hotel Tracing";

        #region Flight API Call

        public List<AirField> GetAllAirfields()
        {
            string lobjResponse = string.Empty;
            List<AirField> lobjResult = null;
            try
            {
                lobjResponse = IBEAPIPostHelper.PostData(IBEAPIConstant.GetAllAirField, "GET", "GetAllAirField", string.Empty, string.Empty, lstrFlightLogCategory);
                if (!string.IsNullOrEmpty(lobjResponse))
                {
                    lobjResult = JsonConvert.DeserializeObject<List<AirField>>((JObject.Parse(lobjResponse)["results"]).ToString());
                }
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("IBEAPIClientHelper GetAllAirField Exception-: " + ex.Message + Environment.NewLine + ex.StackTrace + Environment.NewLine + ex.InnerException, lstrFlightLogCategory);
            }

            return lobjResult;
        }

        public List<Carrier> GetAllCarriers()
        {
            string lobjResponse = string.Empty;
            List<Carrier> lobjResult = null;
            try
            {
                lobjResponse = IBEAPIPostHelper.PostData(IBEAPIConstant.GetAllCarriers, "GET", "GetAllCarriers", string.Empty, string.Empty, lstrFlightLogCategory);
                if (!string.IsNullOrEmpty(lobjResponse))
                {
                    lobjResult = JsonConvert.DeserializeObject<List<Carrier>>((JObject.Parse(lobjResponse)["results"]).ToString());
                }
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("IBEAPIClientHelper GetAllCarriers Exception-: " + ex.Message + Environment.NewLine + ex.StackTrace + Environment.NewLine + ex.InnerException, lstrFlightLogCategory);
            }

            return lobjResult;
        }

        public List<AirCraftDetails> GetAllAirCraftDetails()
        {
            string lobjResponse = string.Empty;
            List<AirCraftDetails> lobjResult = null;
            try
            {
                lobjResponse = IBEAPIPostHelper.PostData(IBEAPIConstant.GetAllAirCraftDetails, "GET", "GetAllAirCraftDetails", string.Empty, string.Empty, lstrFlightLogCategory);
                if (!string.IsNullOrEmpty(lobjResponse))
                {
                    lobjResult = JsonConvert.DeserializeObject<List<AirCraftDetails>>((JObject.Parse(lobjResponse)["results"]).ToString());
                }
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("IBEAPIClientHelper GetAllAirCraftDetails Exception-: " + ex.Message + Environment.NewLine + ex.StackTrace + Environment.NewLine + ex.InnerException, lstrFlightLogCategory);
            }

            return lobjResult;
        }

        public SearchResponse AirSearchRequest(AirSearchRequest pobjSearchRequest)
        {
            string lobjResponse = string.Empty;
            SearchResponse lobjResult = null;

            try
            {
                lobjResponse = IBEAPIPostHelper.PostData(IBEAPIConstant.AirSearchRequest, "POST", "AirSearchRequest", JsonConvert.SerializeObject(pobjSearchRequest), string.Empty, lstrFlightLogCategory);
                if (!string.IsNullOrEmpty(lobjResponse))
                {
                    lobjResult = JsonConvert.DeserializeObject<SearchResponse>((JObject.Parse(lobjResponse)["results"]).ToString());
                }
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("IBEAPIClientHelper AirSearchRequest Exception-: " + ex.Message + Environment.NewLine + ex.StackTrace + Environment.NewLine + ex.InnerException, lstrFlightLogCategory);
            }
            return lobjResult;
        }

        public CreateItineraryResponse CreateItinerary(CreateItineraryRequest pobjCreateItineraryRequest)
        {
            string lobjResponse = string.Empty;
            CreateItineraryResponse lobjResult = null;

            try
            {
                lobjResponse = IBEAPIPostHelper.PostData(IBEAPIConstant.CreateItinerary, "POST", "CreateItinerary", JsonConvert.SerializeObject(pobjCreateItineraryRequest), string.Empty, lstrFlightLogCategory);

                if (!string.IsNullOrEmpty(lobjResponse))
                {
                    lobjResult = JsonConvert.DeserializeObject<CreateItineraryResponse>((JObject.Parse(lobjResponse)["results"]).ToString());
                }
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("IBEAPIClientHelper CreateItinerary Exception-: " + ex.Message + Environment.NewLine + ex.StackTrace + Environment.NewLine + ex.InnerException, lstrFlightLogCategory);
            }

            return lobjResult;
        }

        public BookingResponse CreateBooking(BookingRequest pobjBookingRequest)
        {
            string lobjResponse = string.Empty;
            BookingResponse lobjResult = null;

            try
            {
                lobjResponse = IBEAPIPostHelper.PostData(IBEAPIConstant.CreateBooking, "POST", "CreateBooking", JsonConvert.SerializeObject(pobjBookingRequest), string.Empty, lstrFlightLogCategory);
                if (!string.IsNullOrEmpty(lobjResponse))
                {
                    lobjResult = JsonConvert.DeserializeObject<BookingResponse>((JObject.Parse(lobjResponse)["results"]).ToString());
                }
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("IBEAPIClientHelper CreateBooking Exception-: " + ex.Message + Environment.NewLine + ex.StackTrace + Environment.NewLine + ex.InnerException, lstrFlightLogCategory);
            }
            return lobjResult;
        }

        public List<ItineraryDetails> GetFlightBookingListForMember(string pstrMemberId)
        {
            string lobjResponse = string.Empty;
            string lstrpostdata = string.Empty;
            List<ItineraryDetails> lobjResult = null;

            try
            {
                lstrpostdata = "{\"MemberId\": \"" + pstrMemberId + "\"}";

                lobjResponse = IBEAPIPostHelper.PostData(IBEAPIConstant.GetFlightBookingListForMember, "POST", "GetFlightBookingListForMember", lstrpostdata, string.Empty, lstrFlightLogCategory);
                if (!string.IsNullOrEmpty(lobjResponse))
                {
                    lobjResult = JsonConvert.DeserializeObject<List<ItineraryDetails>>((JObject.Parse(lobjResponse)["results"]).ToString());
                }
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("IBEAPIClientHelper GetFlightBookingListForMember Exception-: " + ex.Message + Environment.NewLine + ex.StackTrace + Environment.NewLine + ex.InnerException, lstrFlightLogCategory);
            }
            return lobjResult;
        }

        public RetriveItineraryDetails GetBookedFlightItinerary(string pstrTripId)
        {
            string lobjResponse = string.Empty;
            string lstrpostdata = string.Empty;
            RetriveItineraryDetails lobjResult = null;

            try
            {
                lstrpostdata = "{\"BookingReference\": \"" + pstrTripId + "\"}";

                lobjResponse = IBEAPIPostHelper.PostData(IBEAPIConstant.GetBookedFlightItinerary, "POST", "GetBookedFlightItinerary", lstrpostdata, string.Empty, lstrFlightLogCategory);
                if (!string.IsNullOrEmpty(lobjResponse))
                {
                    lobjResult = JsonConvert.DeserializeObject<RetriveItineraryDetails>((JObject.Parse(lobjResponse)["results"]).ToString());
                }
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("IBEAPIClientHelper GetBookedFlightItinerary Exception-: " + ex.Message + Environment.NewLine + ex.StackTrace + Environment.NewLine + ex.InnerException, lstrFlightLogCategory);
            }
            return lobjResult;
        }

        #endregion

        #region Hotel API Call

        public List<string> GetAllHotelCities()
        {
            string lobjResponse = string.Empty;
            List<string> lobjResult = null;
            try
            {
                lobjResponse = IBEAPIPostHelper.PostData(IBEAPIConstant.GetAllHotelCities, "GET", "GetAllHotelCities", string.Empty, string.Empty, lstrHotelLogCategory);
                if (!string.IsNullOrEmpty(lobjResponse))
                {
                    lobjResult = JsonConvert.DeserializeObject<List<string>>((JObject.Parse(lobjResponse)["results"]).ToString());
                }
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("IBEAPIClientHelper GetAllHotelCities Exception-: " + ex.Message + Environment.NewLine + ex.StackTrace + Environment.NewLine + ex.InnerException, lstrHotelLogCategory);
            }

            return lobjResult;
        }

        public HotelSearchResponse GetHotelSearchResponse(HotelsSearchRequest pobjSearchRequest)
        {
            string lobjResponse = string.Empty;
            HotelSearchResponse lobjResult = null;

            try
            {
                lobjResponse = IBEAPIPostHelper.PostData(IBEAPIConstant.GetHotelSearchResponse, "POST", "GetHotelSearchResponse", JsonConvert.SerializeObject(pobjSearchRequest), string.Empty, lstrHotelLogCategory);
                if (!string.IsNullOrEmpty(lobjResponse))
                {
                    lobjResult = JsonConvert.DeserializeObject<HotelSearchResponse>((JObject.Parse(lobjResponse)["results"]).ToString());
                }
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("IBEAPIClientHelper GetHotelSearchResponse Exception-: " + ex.Message + Environment.NewLine + ex.StackTrace + Environment.NewLine + ex.InnerException, lstrHotelLogCategory);
            }
            return lobjResult;
        }

        public HotelInformationResponse GetHotelInformation(string pstrHotelId)
        {
            string lobjResponse = string.Empty;
            string posturl = string.Empty;
            HotelInformationResponse lobjResult = null;
            try
            {
                posturl = IBEAPIConstant.GetHotelInformation + pstrHotelId;

                lobjResponse = IBEAPIPostHelper.PostData(posturl, "GET", "GetHotelInformation", string.Empty, string.Empty, lstrHotelLogCategory);
                if (!string.IsNullOrEmpty(lobjResponse))
                {
                    lobjResult = JsonConvert.DeserializeObject<HotelInformationResponse>((JObject.Parse(lobjResponse)["results"]).ToString());
                }
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("IBEAPIClientHelper GetHotelInformation Exception-: " + ex.Message + Environment.NewLine + ex.StackTrace + Environment.NewLine + ex.InnerException, lstrHotelLogCategory);
            }

            return lobjResult;
        }

        public HotelRepriceResponse HotelReprice(HotelRepriceRequest pobjHotelRepriceRequest)
        {
            string lobjResponse = string.Empty;
            HotelRepriceResponse lobjResult = null;

            try
            {
                lobjResponse = IBEAPIPostHelper.PostData(IBEAPIConstant.HotelReprice, "POST", "HotelReprice", JsonConvert.SerializeObject(pobjHotelRepriceRequest), string.Empty, lstrHotelLogCategory);
                if (!string.IsNullOrEmpty(lobjResponse))
                {
                    lobjResult = JsonConvert.DeserializeObject<HotelRepriceResponse>((JObject.Parse(lobjResponse)["results"]).ToString());
                }
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("IBEAPIClientHelper HotelReprice Exception-: " + ex.Message + Environment.NewLine + ex.StackTrace + Environment.NewLine + ex.InnerException, lstrHotelLogCategory);
            }
            return lobjResult;
        }

        public HotelBookingResponse GetHotelBookingResponse(HotelBookRequest pobjHotelBookingRequest)
        {
            string lobjResponse = string.Empty;
            HotelBookingResponse lobjResult = null;

            try
            {
                lobjResponse = IBEAPIPostHelper.PostData(IBEAPIConstant.GetHotelBookingResponse, "POST", "GetHotelBookingResponse", JsonConvert.SerializeObject(pobjHotelBookingRequest), string.Empty, lstrHotelLogCategory);

                if (!string.IsNullOrEmpty(lobjResponse))
                {
                    lobjResult = JsonConvert.DeserializeObject<HotelBookingResponse>((JObject.Parse(lobjResponse)["results"]).ToString());
                }
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("IBEAPIClientHelper GetHotelBookingResponse Exception-: " + ex.Message + Environment.NewLine + ex.StackTrace + Environment.NewLine + ex.InnerException, lstrHotelLogCategory);
            }
            return lobjResult;
        }

        public GetHotelInfoDetails GetMemberBookedHotelInfoList(string pstrMembershipReference)
        {
            string lobjResponse = string.Empty;
            string lstrpostdata = string.Empty;
            GetHotelInfoDetails lobjResult = null;

            try
            {
                lstrpostdata = "{\"MembershipReference\": \"" + pstrMembershipReference + "\"}";

                lobjResponse = IBEAPIPostHelper.PostData(IBEAPIConstant.GetMemberBookedHotelInfoList, "POST", "GetMemberBookedHotelInfoList", lstrpostdata, string.Empty, lstrHotelLogCategory);

                if (!string.IsNullOrEmpty(lobjResponse))
                {
                    lobjResult = JsonConvert.DeserializeObject<GetHotelInfoDetails>((JObject.Parse(lobjResponse)["results"]).ToString());
                }
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("IBEAPIClientHelper GetMemberBookedHotelInfoList Exception-: " + ex.Message + Environment.NewLine + ex.StackTrace + Environment.NewLine + ex.InnerException, lstrHotelLogCategory);
            }
            return lobjResult;
        }

        public HotelItineraryResponse GetMemberBookedHotelInfo(string pstrTransactionRef)
        {
            string lobjResponse = string.Empty;
            string lstrpostdata = string.Empty;
            HotelItineraryResponse lobjResult = null;

            try
            {
                lstrpostdata = "{\"TransactionRef\": \"" + pstrTransactionRef + "\"}";

                lobjResponse = IBEAPIPostHelper.PostData(IBEAPIConstant.GetMemberBookedHotelInfo, "POST", "GetMemberBookedHotelInfo", lstrpostdata, string.Empty, lstrHotelLogCategory);
                if (!string.IsNullOrEmpty(lobjResponse))
                {
                    lobjResult = JsonConvert.DeserializeObject<HotelItineraryResponse>((JObject.Parse(lobjResponse)["results"]).ToString());
                }
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("IBEAPIClientHelper GetMemberBookedHotelInfo Exception-: " + ex.Message + Environment.NewLine + ex.StackTrace + Environment.NewLine + ex.InnerException, lstrHotelLogCategory);
            }
            return lobjResult;
        }

        #endregion

        #region Car API Call

        public CarCountryResponse GetCountriesList()
        {
            string lobjResponse = string.Empty;
            CarCountryResponse lobjCarCountryResponse = null;
            try
            {
                lobjResponse = IBEAPIPostHelper.PostDataAPI(IBEAPIConstant.GetCountriesList, "GET", "GetLocation", string.Empty, string.Empty);
                if (!string.IsNullOrEmpty(lobjResponse))
                {
                    lobjCarCountryResponse = JsonConvert.DeserializeObject<CarCountryResponse>(lobjResponse);
                }
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("IBEAPIClientHelper GetCountriesList Exception: " + ex.Message + Environment.NewLine + ex.StackTrace);
            }
            return lobjCarCountryResponse;
        }

        public BulkResponse GetLocation()
        {
            string lobjResponse = string.Empty;
            BulkResponse lobjBulkResponse = null;
            try
            {
                lobjResponse = IBEAPIPostHelper.PostDataAPI(IBEAPIConstant.GetLocation, "GET", "GetLocation", string.Empty, string.Empty);
                if (!string.IsNullOrEmpty(lobjResponse))
                {
                    lobjBulkResponse = JsonConvert.DeserializeObject<BulkResponse>(lobjResponse);
                }
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("IBEAPIClientHelper GetLocation Exception: " + ex.Message + Environment.NewLine + ex.StackTrace);
            }
            return lobjBulkResponse;
        }

        public AvailabilityResponse GetAvailability(GetAvailabilityRequest pobjGetAvailabilityRequest)
        {
            string lobjResponse = string.Empty;
            string lstrpostdata = string.Empty;
            AvailabilityResponse lobjAvailabilityResponse = null;
            try
            {
                lstrpostdata = JsonConvert.SerializeObject(pobjGetAvailabilityRequest);

                lobjResponse = IBEAPIPostHelper.PostDataAPI(IBEAPIConstant.GetAvailability, "POST", "GetAvailability", lstrpostdata, string.Empty);
                if (!string.IsNullOrEmpty(lobjResponse))
                {
                    lobjAvailabilityResponse = JsonConvert.DeserializeObject<AvailabilityResponse>(lobjResponse);
                }
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("IBEAPIClientHelper GetAvailability Exception: " + ex.Message + Environment.NewLine + ex.StackTrace);
            }
            return lobjAvailabilityResponse;
        }

        public RateResponse GetRates(RateRequest pobjRateRequest)
        {
            string lobjResponse = string.Empty;
            RateResponse lobjRateResponse = null;
            try
            {
                lobjResponse = IBEAPIPostHelper.PostDataAPI(IBEAPIConstant.GetRates, "POST", "GetRates", JsonConvert.SerializeObject(pobjRateRequest), string.Empty);
                if (!string.IsNullOrEmpty(lobjResponse))
                {
                    lobjRateResponse = JsonConvert.DeserializeObject<RateResponse>(lobjResponse);
                }
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("IBEAPIClientHelper GetRates Exception: " + ex.Message + Environment.NewLine + ex.StackTrace);
            }
            return lobjRateResponse;
        }

        public CarBookingResponse CreateCarBooking(CarBookingRequest pobjCarBookingRequest)
        {
            string lobjResponse = string.Empty;
            CarBookingResponse lobjCarBookingResponse = null;

            try
            {                
                lobjResponse = IBEAPIPostHelper.PostDataAPI(IBEAPIConstant.CreateCarBooking, "POST", "CreateCarBooking", JsonConvert.SerializeObject(pobjCarBookingRequest), string.Empty);
                if (!string.IsNullOrEmpty(lobjResponse))
                {
                    lobjCarBookingResponse = JsonConvert.DeserializeObject<CarBookingResponse>(lobjResponse);
                }
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("IBEAPIClientHelper CreateCarBooking Exception: " + ex.Message + Environment.NewLine + ex.StackTrace);
            }
            return lobjCarBookingResponse;
        }

        public UserBookingResponse GetUserBookings(UserBookingRequest pobjUserBookingRequest)
        {
            string lobjResponse = string.Empty;
            UserBookingResponse lobjUserBookingResponse = null;

            try
            {
                lobjResponse = IBEAPIPostHelper.PostDataAPI(IBEAPIConstant.GetUserBookings, "POST", "GetUserBookings", JsonConvert.SerializeObject(pobjUserBookingRequest), string.Empty);
                if (!string.IsNullOrEmpty(lobjResponse))
                {
                    lobjUserBookingResponse = JsonConvert.DeserializeObject<UserBookingResponse>(lobjResponse);
                }
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("IBEAPIClientHelper GetUserBookings Exception: " + ex.Message + Environment.NewLine + ex.StackTrace);
            }
            return lobjUserBookingResponse;
        }


        public CarBookingDetails GetMemberBookedCar(string pobjUserBookingRequest, string EnjoyTraveldisplayCurrency)
        {
            string lobjResponse = string.Empty;
            string lstrpostdata = string.Empty;
            string language = "en-gb";

            CarBookingDetails lobjUserBookingResponse = null;
            try
            {
                lstrpostdata = "{\"reference_id\": \"" + pobjUserBookingRequest + ",displayCurrency:\"" + EnjoyTraveldisplayCurrency + ",lang:\"" + language + "\"}";
                
                lobjResponse = IBEAPIPostHelper.PostDataAPI(IBEAPIConstant.GetUserBookingdetails, "POST", "GetUserBookingdetails", lstrpostdata, string.Empty);
                if (!string.IsNullOrEmpty(lobjResponse))
                {
                    lobjUserBookingResponse = JsonConvert.DeserializeObject<CarBookingDetails>(lobjResponse);
                }
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("EnjoyTravelAPIClientHelper GetUserBookings Exception: " + ex.Message + Environment.NewLine + ex.StackTrace);
            }
            return lobjUserBookingResponse;
        }

        #endregion
    }
}