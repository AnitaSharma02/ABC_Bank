using System;
using System.Collections.Generic;
using System.Linq;
using CB.IBE.Platform.Hotels.ClientEntities;
using Core.Platform.Member.Entites;
using System.Web;
using Framework.Integrations.Hotels.Entities;
using CB.IBE.Platform.HotelClientModel;
using CB.IBE.Platform.Masters.Entities;
using Core.Platform.MemberActivity.Entities;
using Core.Platform.Transactions.Entites;
using Framework.EnterpriseLibrary.Security;
using Framework.EnterpriseLibrary.PasswordReset.Entities;
using Framework.EnterpriseLibrary.Security.Constants;
using CB.IBE.Platform.AirClientModel;
using Core.Platform.OTP.Entities;
using CB.IBE.Platform.Entities;
using CB.IBE.Platform.ClientEntities;
using Framework.EnterpriseLibrary.Adapters;
using CB.IBE.Platform.Car.ClientEntities;
using CB.IBE.Platform.CarClientModel;
using CB.IBE.Platform.Car.Entities;
using CB.IBE.Platform.IBEClient;
using Core.Platform.ProgramMaster.Entities;
using Core.Platform.ProgramInterface.ClientHelper;
using Core.Platform.Booking.Entities;
using CB.IBE.Platform.IBECarClient;
using Framework.EnterpriseLibrary.CommunicationEngine.Helper;
using Framework.EnterpriseLibrary.CommunicationEngine.Entity;
using System.Globalization;
using Core.Platform.ExpirySchedule.Entities;
using CB.IBE.Platform.Transactions.Entites;
using InfiVoucher.Platform.Entities;
using Core.Platform.InfiVoucher.Entities;
using Core.Platform.Authentication.Entities;
using Core.Platform.LoyaltyManagement.ClientHelper;
using Core.Platform.InfiVoucher.ClientHelper;
using System.Configuration;
using Core.Platform.TransactionSummary.Entites;
using LoyaltyManagement.Request;
using Core.WebAPI.ClientHelper;
using Core.Framework.Booking.Facade;
using System.Security.Cryptography;
using System.Text;
using Core.Platform.RedemptionAuditTrail.Entities;
using Holibob.Entities;
using Holibob.ClientHelper;
using Core.Platform.TransactionManagement.BusinessFacade;
using CB.IBE.DomesticFlight.Entities;
using CE.Entities;
using System.Dynamic;
using Newtonsoft.Json;
using KhaltiInsurance.Entities;
using KhaltiISP.Entities;
using TransactionDetailsAdditionalInfo.Entities;
using GiiftPaymentGateway.Entities;

namespace ABC.Model
{
    public class ABCModel
    {
        APIClientHelper lobjAPIClientHelper = new APIClientHelper();

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

        public List<AirField> GetAllAirfields()
        {
            try
            {
                IBEModel lobjIBEModel = new IBEModel();
                return lobjIBEModel.GetAllAirField();
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("GetAllAirfields -" + ex.Message + Environment.NewLine + "Stack Trace-" + ex.StackTrace);
                return null;
            }

        }

        public AirFieldsForDomestic GetAllAirFieldsForDomestic()
        {
            try
            {
                APIClientHelper lobjAPIClientHelper = new APIClientHelper();
                return lobjAPIClientHelper.GetAllAirFieldsForDomestic();
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("GetAllAirFieldsForDomestic -" + ex.Message + Environment.NewLine + "Stack Trace-" + ex.StackTrace);
                return null;
            }

        }
        public List<Carrier> GetAllCarriers()
        {
            try
            {
                IBEModel lobjIBEModel = new IBEModel();
                return lobjIBEModel.GetAllCarriers();
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("GetAllCarriers -" + ex.Message + Environment.NewLine + "Stack Trace-" + ex.StackTrace);
                return null;
            }

        }
        public List<string> GetAllHotelCities()
        {
            try
            {
                IBECTHotelClient hotelClient = new IBECTHotelClient();
                RefererDetails refererDetails = GetRefererData();
                return hotelClient.GetAllHotelCities(refererDetails);
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("GetAllHotelCities -" + ex.Message + Environment.NewLine + "Stack Trace-" + ex.StackTrace);
                return null;
            }

        }
        public DateTime StringToDateTime(string pstrDate)
        {
            DateTime dt = DateTime.ParseExact(pstrDate, "dd/MM/yyyy", System.Globalization.CultureInfo.GetCultureInfo("ar-AE"));
            return dt;
        }
        public RequestHotelSearch CreateHotelSearchRequest(string pstrCitySearchText, string pstrCheckInDate, string pstrCheckoutDate, string pstrRoomString)
        {
            RequestHotelSearch lobjSearchRequest = new RequestHotelSearch();
            try
            {
                lobjSearchRequest.CheckInDate = StringToDateTime(pstrCheckInDate);
                lobjSearchRequest.CheckOutDate = StringToDateTime(pstrCheckoutDate);
                string[] arrayRoomPersonType = pstrRoomString.TrimEnd(':').Split(':');
                lobjSearchRequest.NoOfRooms = arrayRoomPersonType[0].TrimEnd(',').Split(',').Count();
                lobjSearchRequest.AdultPerRoom = arrayRoomPersonType[0].TrimEnd(',');//adult
                lobjSearchRequest.ChildrenPerRoom = arrayRoomPersonType[1].TrimEnd(',');//child
                lobjSearchRequest.CityName = HttpUtility.UrlDecode(pstrCitySearchText.Split(',')[2].ToString());
                lobjSearchRequest.CountryISOCode = pstrCitySearchText.Split(',')[0].ToString();
                lobjSearchRequest.Country = HttpUtility.UrlDecode(pstrCitySearchText.Split(',')[1].ToString());
                RefererDetails lobjRefererDetails = HttpContext.Current.Application["RefererData"] as RefererDetails;
                lobjSearchRequest.RefererDetails = lobjRefererDetails;
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("CreateHotelSearchRequest -" + ex.Message + Environment.NewLine + "Stack Trace-" + ex.StackTrace);
            }
            return lobjSearchRequest;
        }
        public HotelSearchResponse SearchHotel(HotelSearchRequest pobjSearchRequest)
        {
            HotelSearchResponse lobjHotelSearchResponse = new HotelSearchResponse();
            try
            {
                IBEHotelModel lobjModel = new IBEHotelModel();
                lobjHotelSearchResponse = lobjModel.GetHotels(pobjSearchRequest);
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("SearchHotel -" + ex.Message + Environment.NewLine + "Stack Trace-" + ex.StackTrace);
            }
            return lobjHotelSearchResponse;
        }
        public HotelInformationResponse GetHotelInformation(HotelInformationRequest pobjHotelInformationRequest)
        {
            HotelInformationResponse lobjHotelInformationResponse = new HotelInformationResponse();
            try
            {
                IBEHotelModel lobjHotelModel = new IBEHotelModel();
                RefererDetails lobjRefererDetails = HttpContext.Current.Application["RefererData"] as RefererDetails;
                pobjHotelInformationRequest.RefererDetails = lobjRefererDetails;
                lobjHotelInformationResponse = lobjHotelModel.GetHotelInformation(pobjHotelInformationRequest);
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("GetHotelInformation - " + ex.Message + Environment.NewLine + "Stack Trace-" + ex.StackTrace);
            }
            return lobjHotelInformationResponse;
        }
        public Customer CustomerInfo(CustomerDetail objCustomerInfo)
        {
            Customer lobjCustomer = new Customer();
            try
            {
                lobjCustomer.title = objCustomerInfo.PersonalTitle;
                lobjCustomer.firstname = objCustomerInfo.FirstName;
                lobjCustomer.lastname = objCustomerInfo.Lastname;
                lobjCustomer.city = objCustomerInfo.City;
                lobjCustomer.country = objCustomerInfo.Country;
                lobjCustomer.landline = objCustomerInfo.PhoneNo;
                lobjCustomer.mobile = objCustomerInfo.MobileNo;
                lobjCustomer.streetaddress1 = objCustomerInfo.Address;
                lobjCustomer.streetaddress2 = objCustomerInfo.Address2;
                lobjCustomer.postalcode = objCustomerInfo.PostalCode;
                lobjCustomer.state = objCustomerInfo.State;
                lobjCustomer.email = objCustomerInfo.EmailID;
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("CustomerInfo - " + ex.Message + Environment.NewLine + "Stack Trace-" + ex.StackTrace);
            }
            return lobjCustomer;
        }
        public string EncryptPassword(string pstrPassword)
        {
            return RSAEncryptor.EncryptString(pstrPassword, RSASecurityConstant.KeySize, RSASecurityConstant.RSAPublicKey);
        }
        public SearchResponse MapSearchResponse(SearchRequest pobjSearchRequest)
        {
            SearchResponse lobjSearchResponse = new SearchResponse();
            try
            {
                IBEModel lobjIBEModel = new IBEModel();
                lobjSearchResponse = lobjIBEModel.MapSearchResponse(pobjSearchRequest);
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("MapSearchResponse -" + ex.Message + Environment.NewLine + "Stack Trace-" + ex.StackTrace);
            }
            return lobjSearchResponse;
        }
        public int InsertSearchFlight(SearchRequest pobjSearchFlight)
        {
            int lintResult = 0;
            try
            {
                IBEModel lobjIBEModel = new IBEModel();
                lintResult = lobjIBEModel.InsertSearchFlight(pobjSearchFlight);
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("InsertSearchFlight - " + ex.Message + Environment.NewLine + "Stack Trace-" + ex.StackTrace);
            }
            return lintResult;
        }
        public GetHotelInfoDetails GetHotelDetails(string pstrMembershipReference)
        {
            GetHotelInfoDetails lobjGetHotelInfoDetails = new GetHotelInfoDetails();
            try
            {
                RefererDetails lobjRefererDetails = HttpContext.Current.Application["RefererData"] as RefererDetails;
                IBECTHotelClient lobjClient = new IBECTHotelClient();
                lobjGetHotelInfoDetails = lobjClient.GetMemberBookedHotelInfoList(pstrMembershipReference, lobjRefererDetails.Id);
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("GetHotelDetails - " + ex.Message + Environment.NewLine + "Stack Trace-" + ex.StackTrace);
            }
            return lobjGetHotelInfoDetails;
        }
        public bool CancelBookingRequest(HotelCancellationRequest pobjHotelCancellationRequest, string pstrMembershipReference)
        {
            bool lblnResult = false;
            try
            {
                IBEHotelModel lobjModel = new IBEHotelModel();
                RefererDetails lobjRefererDetails = HttpContext.Current.Application["RefererData"] as RefererDetails;
                pobjHotelCancellationRequest.CancellationRequest.RefererDetails = lobjRefererDetails;
                HotelCancellationResponse lobjHotelCancellationResponse = new HotelCancellationResponse();
                lobjHotelCancellationResponse = lobjModel.CancelBooking(pobjHotelCancellationRequest, pstrMembershipReference);
                lblnResult = lobjHotelCancellationResponse.Status;
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("CancelBookingRequest - " + ex.Message + Environment.NewLine + "Stack Trace-" + ex.StackTrace);
            }
            return lblnResult;
        }
        public List<ItineraryDetails> GetFlightBookingDetails(string pstrMemberId)
        {
            List<ItineraryDetails> lobjItineraryDetailsList = new List<ItineraryDetails>();
            try
            {
                IBECTClient lobjClient = new IBECTClient();
                RefererDetails lobjRefererDetails = HttpContext.Current.Application["RefererData"] as RefererDetails;
                lobjItineraryDetailsList = lobjClient.GetFlightBookingListForMember(pstrMemberId, lobjRefererDetails.Id);
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("GetFlightBookingDetails - " + ex.Message + Environment.NewLine + "Stack Trace-" + ex.StackTrace);
            }
            return lobjItineraryDetailsList;
        }
        public RetriveItineraryDetails RetriveItineraryDetails(string strTripId, int pintReferrId)
        {
            RetriveItineraryDetails lobjRetriveItineraryDetails = new RetriveItineraryDetails();
            try
            {
                IBEModel lobjIBEModel = new IBEModel();
                return lobjIBEModel.GetBookedFlightInformation(strTripId, pintReferrId);
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("RetriveItineraryDetails - " + ex.Message + Environment.NewLine + "Stack Trace-" + ex.StackTrace);
            }
            return lobjRetriveItineraryDetails;
        }
        public List<ExceptionMessage> GetAllExceptionMessages(int pintProgramId)
        {
            try
            {
                List<ExceptionMessage> lobjListOfExceptionMessage = new List<ExceptionMessage>();
                return lobjListOfExceptionMessage;
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("GetAllExceptionMessages -" + ex.Message + Environment.NewLine + "Stack Trace-" + ex.StackTrace);
                return null;
            }
        }
        public BookingResponse BookForFlight(BookingRequest pobjBookingRequest, MemberDetails pobjMemberDetails, List<RedemptionDetails> pobjListOfRedemptionDetails)
        {
            try
            {
                BookingResponse lobjBookingResponse = null;
                try
                {
                    BookingIntegrationFacade lobjBookingIntegrationFacade = new BookingIntegrationFacade();
                    lobjBookingResponse = lobjBookingIntegrationFacade.BookFlight(pobjBookingRequest, pobjMemberDetails, pobjListOfRedemptionDetails);
                }
                catch (Exception ex)
                {
                    LoggingAdapter.WriteLog("Model BookFlight Ex-" + ex.InnerException + ex.StackTrace + ex.Message);
                }
                return lobjBookingResponse;
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("Model Book For Flight Ex" + ex.Message);
                LoggingAdapter.WriteLog("Model Book For Flight Ex stack" + ex.StackTrace);
                throw ex;
            }
        }
        public HotelBookingResponse BookForHotel(MemberDetails pobjMemberDetails, Hotel pobjHotel, HotelSearchRequest pobjSearchRequest, Customer pobjCustomer, List<RedemptionDetails> pobjListOfRedemptionDetails)
        {
            HotelBookingResponse lobjHotelBookingResponse = null;
            try
            {
                List<int> lobjListOfAdult = new List<int>();
                string[] arrayAdultPerRoom = pobjSearchRequest.SearchRequest.AdultPerRoom.Split(',');
                for (int i = 0; i < arrayAdultPerRoom.Count(); i++)
                {
                    lobjListOfAdult.Add(Convert.ToInt32(arrayAdultPerRoom[i]));
                }
                List<int> lobjListOfChild = new List<int>();
                string[] arrayChildPerRoom = pobjSearchRequest.SearchRequest.ChildrenPerRoom.Split(',');
                for (int i = 0; i < arrayChildPerRoom.Count(); i++)
                {
                    lobjListOfChild.Add(Convert.ToInt32(arrayChildPerRoom[i]));
                }
                HotelBookingRequest lobjBookingRequest = new HotelBookingRequest();
                lobjBookingRequest.BookRequest.customer = pobjCustomer;
                lobjBookingRequest.BookRequest.checkindate = pobjSearchRequest.SearchRequest.CheckInDate;
                lobjBookingRequest.BookRequest.checkoutdate = pobjSearchRequest.SearchRequest.CheckOutDate;
                lobjBookingRequest.BookRequest.numberofrooms = pobjSearchRequest.SearchRequest.NoOfRooms;
                lobjBookingRequest.BookRequest.nri = false;
                lobjBookingRequest.BookRequest.adultsperroom = lobjListOfAdult.ToArray();
                lobjBookingRequest.BookRequest.childrenperroom = lobjListOfChild.ToArray();
                lobjBookingRequest.BookRequest.bookingcode = pobjHotel.roomrates.RoomRate[0].bookingcode;
                lobjBookingRequest.BookRequest.roomtypecode = pobjHotel.roomrates.RoomRate[0].roomtype.roomtypecode;
                lobjBookingRequest.BookRequest.TotalPoints = pobjHotel.roomrates.RoomRate[0].TotalPoints;
                lobjBookingRequest.BookRequest.customeripaddress = pobjSearchRequest.SearchRequest.IpAddress;
                lobjBookingRequest.BookRequest.hotelid = pobjHotel.hotelid;
                lobjBookingRequest.BookRequest.bookingamount = Convert.ToDouble(pobjHotel.roomrates.RoomRate[0].TotalDefaultAmount);
                lobjBookingRequest.BookRequest.totalBaseFare = Convert.ToDouble(pobjHotel.roomrates.RoomRate[0].TotalBaseAmount);
                lobjBookingRequest.BookRequest.totalDefaulFare = Convert.ToDouble(pobjHotel.roomrates.RoomRate[0].TotalDefaultAmount);
                pobjSearchRequest.SearchRequest.MembershipReference = pobjMemberDetails.MemberRelationsList[0].RelationReference;
                try
                {
                    BookingIntegrationFacade lobjBookingIntegrationFacade = new BookingIntegrationFacade();
                    lobjHotelBookingResponse = lobjBookingIntegrationFacade.BookHotel(lobjBookingRequest, pobjHotel, pobjSearchRequest, pobjMemberDetails, pobjCustomer, pobjListOfRedemptionDetails);
                }
                catch (Exception ex)
                {
                    LoggingAdapter.WriteLog("Model BookHotel Ex-" + ex.InnerException + ex.StackTrace + ex.Message);
                }
            }
            catch (Exception ex) { LoggingAdapter.WriteLog("BookForHotel - " + ex.Message + Environment.NewLine + "Stack Trace-" + ex.StackTrace); }
            return lobjHotelBookingResponse;
        }
        public bool SendApplyNowEmailMsg(string pstrFullName, string pstrCountry, string pstEmail, string pstrMobno, string pstrAccountStatus, string pstrApplyStatus, string pstrMessage)
        {
            bool lboolStatus = false;
            try
            {
                CEHelper lobjcehelper = new CEHelper();
                EmailDetails lobjEmailDetail = new EmailDetails();
                List<string> lstEmailparameter = new List<string>();
                List<string> lstAttachment = new List<string>();
                lobjEmailDetail.TemplateCode = "ApplyNow";
                lstEmailparameter.Add(pstrFullName);
                lstEmailparameter.Add(pstrCountry);
                lstEmailparameter.Add(pstEmail);
                lstEmailparameter.Add(pstrMobno);
                lstEmailparameter.Add(pstrAccountStatus);
                lstEmailparameter.Add(pstrApplyStatus);
                lstEmailparameter.Add(pstrMessage);
                lobjEmailDetail.ListParameter = lstEmailparameter;
                lobjEmailDetail.AttachmentList = lstAttachment;
                lobjEmailDetail.To = string.Empty;
                lboolStatus = lobjcehelper.InsertEmailDetails(lobjEmailDetail);
            }
            catch (Exception ex) { LoggingAdapter.WriteLog("SendApplyNowEmailMsg - " + ex.Message + Environment.NewLine + "Stack Trace-" + ex.StackTrace); }
            return lboolStatus;
        }
        public bool SendContactUsEmailMsg(string pstrFullName, string pstrCountry, string pstEmail, string pstrMobno, string pstrAccountStatus, string pstrApplyStatus, string pstrMessage, string pstrContactTime)
        {
            bool lboolStatus = false;
            try
            {
                CEHelper lobjcehelper = new CEHelper();
                EmailDetails lobjEmailDetail = new EmailDetails();
                List<string> lstEmailparameter = new List<string>();
                List<string> lstAttachment = new List<string>();
                lobjEmailDetail.TemplateCode = "ContactUs";
                lstEmailparameter.Add(pstrFullName);
                lstEmailparameter.Add(pstrCountry);
                lstEmailparameter.Add(pstEmail);
                lstEmailparameter.Add(pstrMobno);
                lstEmailparameter.Add(pstrAccountStatus);
                lstEmailparameter.Add(pstrApplyStatus);
                lstEmailparameter.Add(pstrMessage);
                lstEmailparameter.Add(pstrContactTime);
                lobjEmailDetail.ListParameter = lstEmailparameter;
                lobjEmailDetail.AttachmentList = lstAttachment;
                lobjEmailDetail.To = string.Empty;
                lboolStatus = lobjcehelper.InsertEmailDetails(lobjEmailDetail);
            }
            catch (Exception ex) { LoggingAdapter.WriteLog("SendContactUsEmailMsg - " + ex.Message + Environment.NewLine + "Stack Trace-" + ex.StackTrace); }
            return lboolStatus;
        }
        public CreateItineraryResponse GetItineraryResponse(CreateItineraryRequest pobjCreateItineraryRequest)
        {
            CreateItineraryResponse lobjCreateItineraryResponse = new CreateItineraryResponse();
            try
            {
                IBEModel lobjIBEModel = new IBEModel();
                lobjCreateItineraryResponse = lobjIBEModel.CreateItinerary(pobjCreateItineraryRequest);
            }
            catch (Exception ex) { LoggingAdapter.WriteLog("GetItineraryResponse - " + ex.Message + Environment.NewLine + "Stack Trace-" + ex.StackTrace); }
            return lobjCreateItineraryResponse;
        }
        public string createReceiptForFailCar(CarMakeBookingResponse pobjCarMakeBookingResponse, CarMakeBookingRequest pobjCarMakeBookingRequest, MemberDetails pobjMemberDetails, CarSearchRequest pobjCarSearchRequest, Match pobjMatch, CarExtrasListResponse pobjCarExtrasListResponse, string pstrMailBody)
        {
            string lstrResult = string.Empty;
            try
            {
                string pstrEmailTemplate = pstrMailBody;
                object[] lobjArray = new object[20];
                lobjArray[0] = Convert.ToString(pobjMemberDetails.MemberRelationsList.Find(lobj => lobj.RelationType.Equals(Core.Platform.Member.Entites.RelationType.LBMS)).RelationReference);
                lobjArray[1] = Convert.ToString(pobjMemberDetails.LastName);
                lobjArray[2] = Convert.ToString(pobjMemberDetails.MobileNumber);
                lobjArray[3] = Convert.ToString(pobjCarMakeBookingResponse.BookingReferenceId);
                lobjArray[4] = Convert.ToString(pobjMatch.Price[0].TotalPoints + pobjCarExtrasListResponse.ExtrasListRS.Items[0].ExtraInfo[0].Price[0].ExtraTotalPoints);
                lobjArray[15] = Convert.ToString(pobjMatch.Vehicle[0].Name);
                lobjArray[5] = Convert.ToString(pobjMatch.Route[0].PickUp[0].locName);
                lobjArray[6] = Convert.ToString(pobjCarSearchRequest.SearchRequest.PickUp[0].Location[0].city);
                lobjArray[7] = Convert.ToString(pobjCarSearchRequest.SearchRequest.PickUp[0].Location[0].country);
                lobjArray[8] = Convert.ToString(pobjCarMakeBookingRequest.MakeBookingRQ.Booking.PickUp[0].Date[0].day + "/" + pobjCarMakeBookingRequest.MakeBookingRQ.Booking.PickUp[0].Date[0].month + "/" + pobjCarMakeBookingRequest.MakeBookingRQ.Booking.PickUp[0].Date[0].year);
                lobjArray[9] = Convert.ToString(pobjCarMakeBookingRequest.MakeBookingRQ.Booking.PickUp[0].Date[0].hour + ":" + pobjCarMakeBookingRequest.MakeBookingRQ.Booking.PickUp[0].Date[0].minute);
                lobjArray[10] = Convert.ToString(pobjMatch.Route[0].DropOff[0].locName);
                lobjArray[11] = Convert.ToString(pobjCarSearchRequest.SearchRequest.DropOff[0].Location[0].city);
                lobjArray[12] = Convert.ToString(pobjCarSearchRequest.SearchRequest.DropOff[0].Location[0].country);
                lobjArray[13] = Convert.ToString(pobjCarMakeBookingRequest.MakeBookingRQ.Booking.DropOff[0].Date[0].day + "/" + pobjCarMakeBookingRequest.MakeBookingRQ.Booking.DropOff[0].Date[0].month + "/" + pobjCarMakeBookingRequest.MakeBookingRQ.Booking.DropOff[0].Date[0].year);
                lobjArray[14] = Convert.ToString(pobjCarMakeBookingRequest.MakeBookingRQ.Booking.PickUp[0].Date[0].hour + ":" + pobjCarMakeBookingRequest.MakeBookingRQ.Booking.PickUp[0].Date[0].minute);
                lobjArray[16] = Convert.ToString(pobjCarMakeBookingRequest.MakeBookingRQ.Booking.DriverInfo[0].DriverName[0].title);
                lobjArray[17] = Convert.ToString(pobjCarMakeBookingRequest.MakeBookingRQ.Booking.DriverInfo[0].DriverName[0].firstname);
                lobjArray[18] = Convert.ToString(pobjCarMakeBookingRequest.MakeBookingRQ.Booking.DriverInfo[0].DriverName[0].lastname);
                lstrResult = string.Format(pstrEmailTemplate, lobjArray);
            }
            catch (Exception ex) { LoggingAdapter.WriteLog("createReceiptForFailCar - " + ex.Message + Environment.NewLine + "Stack Trace-" + ex.StackTrace); }
            return lstrResult;
        }
        public CarPickUpCityListResponce GetAllPickUpCity(CarPickUpCityListRequest lobjCarPickUpCityListRequest)
        {
            CarPickUpCityListResponce lobjCarPickUpCityListResponce = new CarPickUpCityListResponce();
            try
            {
                IBECarModel lobjModel = new IBECarModel();
                lobjCarPickUpCityListResponce = lobjModel.CarPickUpCityListResponse(lobjCarPickUpCityListRequest);
            }
            catch (Exception ex) { LoggingAdapter.WriteLog("GetAllPickUpCity - " + ex.Message + Environment.NewLine + "Stack Trace-" + ex.StackTrace); }
            return lobjCarPickUpCityListResponce;
        }
        public CarPickUpLocationListResponce GetAllPickUpLocation(CarPickUpLocationListRequest lobjCarPickUpLocationListRequest)
        {
            CarPickUpLocationListResponce lobjCarPickUpLocationListResponce = new CarPickUpLocationListResponce();
            try
            {
                IBECarModel lobjModel = new IBECarModel();
                lobjCarPickUpLocationListResponce = lobjModel.CarPickUpLocationListResponse(lobjCarPickUpLocationListRequest);
            }
            catch (Exception ex) { LoggingAdapter.WriteLog("GetAllPickUpLocation - " + ex.Message + Environment.NewLine + "Stack Trace-" + ex.StackTrace); }
            return lobjCarPickUpLocationListResponce;
        }
        public CarDropOffCountryListResponse GetAllDropOffCountry(CarDropOffCountryListRequest pobjCarDropOffCountryListRequest)
        {
            CarDropOffCountryListResponse lobjCarDropOffCountryListResponse = new CarDropOffCountryListResponse();
            try
            {
                IBECarModel lobjModel = new IBECarModel();
                lobjCarDropOffCountryListResponse = lobjModel.CarDropOffCountryListResponse(pobjCarDropOffCountryListRequest);
            }
            catch (Exception ex) { LoggingAdapter.WriteLog("GetAllDropOffCountry - " + ex.Message + Environment.NewLine + "Stack Trace-" + ex.StackTrace); }
            return lobjCarDropOffCountryListResponse;
        }
        public CarDropOffCityListResponce GetAllDropOffCity(CarDropOffCityListRequest pobjCarDropOffCityListRequest)
        {
            CarDropOffCityListResponce lobjCarDropOffCityListResponce = new CarDropOffCityListResponce();
            try
            {
                IBECarModel lobjModel = new IBECarModel();
                lobjCarDropOffCityListResponce = lobjModel.CarDropOffCityListResponce(pobjCarDropOffCityListRequest);
            }
            catch (Exception ex) { LoggingAdapter.WriteLog("GetAllDropOffCity - " + ex.Message + Environment.NewLine + "Stack Trace-" + ex.StackTrace); }
            return lobjCarDropOffCityListResponce;
        }
        public CarDropOffLocationListResponce GetAllDropOffLocation(CarDropOffLocationListRequest pobjCarDropOffLocationListRequest)
        {
            CarDropOffLocationListResponce lobjCarDropOffLocationListResponce = new CarDropOffLocationListResponce();
            try
            {
                IBECarModel lobjModel = new IBECarModel();
                lobjCarDropOffLocationListResponce = lobjModel.CarDropOffLocationListResponce(pobjCarDropOffLocationListRequest);
            }
            catch (Exception ex) { LoggingAdapter.WriteLog("GetAllDropOffLocation - " + ex.Message + Environment.NewLine + "Stack Trace-" + ex.StackTrace); }
            return lobjCarDropOffLocationListResponce;
        }
        public CarPickUpOpenTimeResponce GetCarPickUpOpenTime(CarPickUpOpenTimeRequest lobjCarPickUpOpenTimeRequest)
        {
            CarPickUpOpenTimeResponce lobjCarPickUpOpenTimeResponce = new CarPickUpOpenTimeResponce();
            try
            {
                IBECarModel lobjModel = new IBECarModel();
                lobjCarPickUpOpenTimeResponce = lobjModel.CarPickUpOpenTimeResponce(lobjCarPickUpOpenTimeRequest);
            }
            catch (Exception ex) { LoggingAdapter.WriteLog("GetCarPickUpOpenTime - " + ex.Message + Environment.NewLine + "Stack Trace-" + ex.StackTrace); }
            return lobjCarPickUpOpenTimeResponce;
        }
        public string[] DateFormat(string date)
        {
            string[] Date = date.Split('/');
            return Date;
        }
        public string[] TimeFormat(string time)
        {
            string[] Time = time.Split(':');
            return Time;
        }
        public CarPickUpCountryListResponse GetAllCountry(CarPickUpCountryListRequest pobjPickUpCountryListRQ)
        {
            try
            {
                CarPickUpCountryListResponse lobjCarPickUpCountryListResponse = new CarPickUpCountryListResponse();
                try
                {
                    IBECarModel lobjModel = new IBECarModel();
                    lobjCarPickUpCountryListResponse = lobjModel.CarPickUpCountryListResponse(pobjPickUpCountryListRQ);
                }
                catch (Exception ex) { LoggingAdapter.WriteLog("GetAllPickUpCity - " + ex.Message + Environment.NewLine + "Stack Trace-" + ex.StackTrace); }
                return lobjCarPickUpCountryListResponse;
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("GetAllCountry -" + ex.Message + Environment.NewLine + "Stack Trace-" + ex.StackTrace);
                return null;
            }
        }
        public CarPickUpCityListResponce GetAllCities(string countryname)
        {
            CarPickUpCityListResponce lobjCarPickUpCityListResponce = new CarPickUpCityListResponce();
            try
            {
                CarPickUpCityListRequest lobjCarPickUpCityListRequest = new CarPickUpCityListRequest();
                lobjCarPickUpCityListRequest.PickUpCityListRequest.Country = countryname;
                IBECarModel lobjModel = new IBECarModel();
                lobjCarPickUpCityListResponce = lobjModel.CarPickUpCityListResponse(lobjCarPickUpCityListRequest);
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("GetAllCities -" + ex.Message + Environment.NewLine + "Stack Trace-" + ex.StackTrace);
            }
            return lobjCarPickUpCityListResponce;
        }
        public CarRentalTermsResponse CarRentalTermsResponse(CarRentalTermsRequest lobjCarRentalTermsRequest)
        {
            CarRentalTermsResponse lobjCarRentalTermsResponse = new CarRentalTermsResponse();
            try
            {
                IBECarModel lobjModel = new IBECarModel();
                lobjCarRentalTermsResponse = lobjModel.CarRentalTermsResponse(lobjCarRentalTermsRequest);
            }
            catch (Exception ex) { LoggingAdapter.WriteLog("CarRentalTermsResponse - " + ex.Message + Environment.NewLine + "Stack Trace-" + ex.StackTrace); }
            return lobjCarRentalTermsResponse;
        }
        public CarPickUpOpenTimeResponce CarPickUpOpenTimeResponce(string Pickupdate, string LocationId)
        {
            CarPickUpOpenTimeResponce lobjCarPickUpOpenTimeResponce = new CarPickUpOpenTimeResponce();
            try
            {
                CarPickUpOpenTimeRequest pstrPickUpOpenTimeRQ = new CarPickUpOpenTimeRequest();
                string[] Date = DateFormat(Pickupdate);
                List<Date> lobjDateList = new List<Date>();
                Date lobjDate = new Date
                {
                    day = Date[0],
                    month = Date[1],
                    year = Date[2]
                };
                lobjDateList.Add(lobjDate);
                List<Location> lobjListOfLocation = new List<Location>();
                Location lobjLocation = new Location();
                lobjLocation.id = LocationId;
                lobjListOfLocation.Add(lobjLocation);
                pstrPickUpOpenTimeRQ.PickUpOpenTimeRequest.Location = lobjListOfLocation.ToArray();
                pstrPickUpOpenTimeRQ.PickUpOpenTimeRequest.Date = lobjDateList.ToArray();
                IBECarModel lobjModel = new IBECarModel();
                lobjCarPickUpOpenTimeResponce = lobjModel.CarPickUpOpenTimeResponce(pstrPickUpOpenTimeRQ);
            }
            catch (Exception ex) { LoggingAdapter.WriteLog("CarPickUpOpenTimeResponce - " + ex.Message + Environment.NewLine + "Stack Trace-" + ex.StackTrace); }
            return lobjCarPickUpOpenTimeResponce;
        }
        public CarDropOffOpenTimeResponce CarDropOffOpenTimeResponce(string DropOffDate, string LocationId)
        {
            CarDropOffOpenTimeResponce CarDropOffOpenTimeResponce = new CarDropOffOpenTimeResponce();
            try
            {
                CarDropOffOpenTimeRequest CarDropOffOpenTimeRequest = new CarDropOffOpenTimeRequest();
                string[] Date = DateFormat(DropOffDate);
                List<Date> lobjDateList = new List<Date>();
                Date lobjDate = new Date
                {
                    day = Date[0],
                    month = Date[1],
                    year = Date[2]
                };
                lobjDateList.Add(lobjDate);
                List<Location> lobjListOfLocation = new List<Location>();
                Location lobjLocation = new Location
                {
                    id = LocationId
                };
                lobjListOfLocation.Add(lobjLocation);
                CarDropOffOpenTimeRequest.DropOffOpenTimeRequest.Location = lobjListOfLocation.ToArray();
                CarDropOffOpenTimeRequest.DropOffOpenTimeRequest.Date = lobjDateList.ToArray();
                IBECarModel lobjModel = new IBECarModel();
                CarDropOffOpenTimeResponce = lobjModel.CarDropOffOpenTimeResponce(CarDropOffOpenTimeRequest);
            }
            catch (Exception ex) { LoggingAdapter.WriteLog("CarDropOffOpenTimeResponce - " + ex.Message + Environment.NewLine + "Stack Trace-" + ex.StackTrace); }
            return CarDropOffOpenTimeResponce;
        }
        public Match GetCarVehiclDetails(List<Match> list, string pstrref)
        {
            Match lobjMatchResult = new Match();
            try
            {
                lobjMatchResult = list.ToList().Find(lobjMatch => lobjMatch.Vehicle[0].id.Equals(pstrref));
            }
            catch (Exception ex) { LoggingAdapter.WriteLog("GetCarVehiclDetails - " + ex.Message + Environment.NewLine + "Stack Trace-" + ex.StackTrace); }
            return lobjMatchResult;
        }
        public CarExtrasListResponse CarExtrasListResponse(CarExtrasListRequest pobjCarExtrasListRequest, CarSearchRequest pobjCarSearchRequest)
        {
            CarExtrasListResponse lobjCarExtrasListResponse = new CarExtrasListResponse();
            try
            {
                IBECarModel lobjModel = new IBECarModel();
                lobjCarExtrasListResponse = lobjModel.CarExtrasListResponse(pobjCarExtrasListRequest, pobjCarSearchRequest);
            }
            catch (Exception ex) { LoggingAdapter.WriteLog("CarExtrasListResponse - " + ex.Message + Environment.NewLine + "Stack Trace-" + ex.StackTrace); }
            return lobjCarExtrasListResponse;
        }
        public CarSearchResponse CarSearchResponse(CarSearchRequest pstrSearchRQ)
        {
            CarSearchResponse lobjCarSearchResponse = new CarSearchResponse();
            try
            {
                IBECarModel lobjIBECarModel = new IBECarModel();
                lobjCarSearchResponse = lobjIBECarModel.CarSearchResponse(pstrSearchRQ);
            }
            catch (Exception ex) { LoggingAdapter.WriteLog("CarSearchResponse - " + ex.Message + Environment.NewLine + "Stack Trace-" + ex.StackTrace); }
            return lobjCarSearchResponse;
        }
        public CarMakeBookingResponse BookForCar(CarMakeBookingRequest pobjCarMakeBookingRequest, CarSearchRequest pobjCarSearchRequest, Match pobjMatch, CarExtrasListResponse pobjCarExtrasListResponse, MemberDetails pobjMemberDetails, List<RedemptionDetails> pobjListOfRedemptionDetails)
        {
            CarMakeBookingResponse lobjCarMakeBookingResponse = null;
            try
            {
                BookingIntegrationFacade lobjBookingIntegrationFacade = new BookingIntegrationFacade();
                lobjCarMakeBookingResponse = lobjBookingIntegrationFacade.BookCar(pobjCarMakeBookingRequest, pobjCarSearchRequest, pobjMatch, pobjCarExtrasListResponse, pobjMemberDetails, pobjListOfRedemptionDetails);
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("Model BookCar Ex-" + ex.InnerException + ex.StackTrace + ex.Message);
            }
            return lobjCarMakeBookingResponse;
        }

        public List<CarBookingDetails> GetCarBookingDetails(string pstrMemberId, int pstrRefererId)
        {
            try
            {
                IBERCCarClient lobjClient = new IBERCCarClient();
                return lobjClient.GetMemberBookedCarList(pstrMemberId, pstrRefererId);
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("GetCarBookingDetails -" + ex.Message + Environment.NewLine + "Stack Trace-" + ex.StackTrace);
                return null;
            }
        }
        public CarBookingDetails GetCarInfoDetailsByBookingRefId(string pstrBookingRefId)
        {
            try
            {
                RefererDetails lobjRefererDetails = HttpContext.Current.Application["RefererData"] as RefererDetails;
                IBERCCarClient lobjClient = new IBERCCarClient();
                return lobjClient.GetMemberBookedCar(pstrBookingRefId, lobjRefererDetails.Id);
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("GetBookedHotelInfo -" + ex.Message + Environment.NewLine + "Stack Trace-" + ex.StackTrace);
                return null;
            }
        }
        public RefererDetails GetRefererDetailsWithSupplier(RefererDetails lobjRefererDetails)
        {
            try
            {
                IBECTClient lobjIBECTClient = new IBECTClient();
                return lobjIBECTClient.GetRefererDetailsWithSupplier(lobjRefererDetails);
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("GetRefererDetailsWithSupplier -" + ex.Message + Environment.NewLine + "Stack Trace-" + ex.StackTrace);
                return null;
            }
        }
        public List<AirCraftDetails> GetAllAirCraftDetails()
        {
            try
            {
                IBECTClient lobjIBECTClient = new IBECTClient();
                return lobjIBECTClient.GetAllAirCraftDetails();
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("GetAllAirCraftDetails -" + ex.Message + Environment.NewLine + "Stack Trace-" + ex.StackTrace);
                return null;
            }
        }
        public string GetMemberCurrency(MemberDetails pobjMemberDetails)
        {
            string lstrCurrency = string.Empty;
            try
            {
                ProgramDefinition lobjProgramDefinition = GetProgramMaster();
                if (pobjMemberDetails != null)
                {
                    List<CustomerSegment> lobjListOfCustomerSegment = new List<CustomerSegment>();
                    lobjListOfCustomerSegment = GetAllCustomerSegmentlst(lobjProgramDefinition.ProgramId);
                    CustomerSegment lobjCustomerSegment = new CustomerSegment();
                    lobjCustomerSegment = lobjListOfCustomerSegment.Find(lobj => lobj.SegmentCode.Equals(pobjMemberDetails.CustomerSegment));
                    lstrCurrency = lobjCustomerSegment.Currency;
                }
                else
                {
                    if (HttpContext.Current.Application["DefaultCurrency"] != null)
                    {
                        lstrCurrency = HttpContext.Current.Application["DefaultCurrency"] as string;
                    }
                    else
                    {
                        List<ProgramCurrencyDefinition> lobjListOfProgramCurrencyDefinition = new List<ProgramCurrencyDefinition>();
                        lobjListOfProgramCurrencyDefinition = GetProgramCurrencyDefinition(lobjProgramDefinition.ProgramId);
                        lstrCurrency = lobjListOfProgramCurrencyDefinition.Find(lobj => lobj.IsDefault.Equals(true)).Currency;
                        HttpContext.Current.Application["ProgramCurrencyDefinition"] = lobjListOfProgramCurrencyDefinition;
                        HttpContext.Current.Application["DefaultCurrency"] = lstrCurrency;
                    }
                }
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("GetMemberCurrency -" + ex.Message + Environment.NewLine + "Stack Trace-" + ex.StackTrace);
            }
            return lstrCurrency;
        }
        public HotelItineraryResponse GetBookedHotelInfo(string pstrTransactionRef)
        {
            HotelItineraryResponse lobjHotelItineraryResponse = new HotelItineraryResponse();
            try
            {
                IBEHotelModel lobjIBEHotelModel = new IBEHotelModel();
                lobjHotelItineraryResponse = lobjIBEHotelModel.GetBookedHotelInfo(pstrTransactionRef);
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("GetBookedHotelInfo -" + ex.Message + Environment.NewLine + "Stack Trace-" + ex.StackTrace);
            }
            return lobjHotelItineraryResponse;
        }
        public string FloatToThousandSeperated(float pfltValue)
        {
            NumberFormatInfo nfo = new CultureInfo("en-US", false).NumberFormat;
            string pstrString = pfltValue.ToString("N", nfo);
            string[] lstrArray = pstrString.Split('.');
            return lstrArray[0];
        }

        public string IntToThousandSeperated(int pfltValue)
        {
            NumberFormatInfo nfo = new CultureInfo("en-US", false).NumberFormat;
            string pstrString = pfltValue.ToString("N", nfo);
            string[] lstrArray = pstrString.Split('.');
            return lstrArray[0];
        }
        public string FormatPoints(decimal pdecValue, string pstrCurrency)
        {
            NumberFormatInfo nfo = new CultureInfo("en-US", false).NumberFormat;
            string pstrString = pdecValue.ToString("N", nfo);
            string[] lstrArray = pstrString.Split('.');
            return string.Format("{0} {1}", lstrArray[0], pstrCurrency);
        }
        public string CurrencyDisplayText(string pstrCurrency)
        {
            string lstrCurrency = string.Empty;
            try
            {
                ProgramDefinition lobjProgramDefinition = GetProgramMaster();
                List<CustomerSegment> lobjListOfCustomerSegment = new List<CustomerSegment>();
                lobjListOfCustomerSegment = GetAllCustomerSegmentlst(lobjProgramDefinition.ProgramId);
                CustomerSegment lobjCustomerSegment = new CustomerSegment();
                lstrCurrency = lobjListOfCustomerSegment.Find(lobj => lobj.Currency.Equals(pstrCurrency)).Description;
                return lstrCurrency;
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("CurrencyDisplayText-" + ex.Message + ex.StackTrace);
                return lstrCurrency;
            }
        }
        public List<TransactionDetails> GetVoucherSummary(int pintProgramId, string pstrRelationReference, int pintRelationType)
        {
            VoucherHelper lobjVoucherHelper = new VoucherHelper();
            List<TransactionDetails> lobjTransactionDetails = null;
            try
            {
                lobjTransactionDetails = lobjVoucherHelper.GetVoucherSummary(pintProgramId, pstrRelationReference, pintRelationType);
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("GetVoucherSummary :" + ex.Message + Environment.NewLine + "Stack Trace :" + ex.StackTrace);
            }
            return lobjTransactionDetails;
        }
        public List<VoucherSummary> CreateVoucher(MemberDetails pobjMemberDetails, IBEAccountTransactionDetails pobjIBEAccountTransactionDetails, List<VoucherDetails> pobjListVoucherDetails, string pstrCurrency)
        {
            List<VoucherSummary> lobjVoucherSummary = new List<VoucherSummary>();
            try
            {
                BookingIntegrationFacade lobjBookingIntegrationFacade = new BookingIntegrationFacade();
                List<string> lstrVoucherNoList = new List<string>();
                lobjVoucherSummary = lobjBookingIntegrationFacade.CreateVoucher(pobjMemberDetails, pobjIBEAccountTransactionDetails, pobjListVoucherDetails, pstrCurrency);
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("ABQModel CreateVoucher : " + Environment.NewLine + ex.StackTrace);
            }
            return lobjVoucherSummary;
        }
        public bool CheckLoginOTP(string pstrMemberId, string pstrOTP)
        {
            bool mblnStatus = false;
            try
            {
                ABCModel lobjModel = new ABCModel();
                OTPDetails lobjOTPDetails = new OTPDetails();
                string lstrDestIpAddress = HttpContext.Current.Request.UserHostAddress;
                string lstrDestAddress = "Web";
                lobjOTPDetails.UniquerefID = pstrMemberId;
                lobjOTPDetails.OTP = Convert.ToInt32(pstrOTP);
                lobjOTPDetails.DestinationAddress = lstrDestIpAddress;
                lobjOTPDetails.Destination = lstrDestAddress;
                lobjOTPDetails.OtpType = OTPEnumTypes.LOGIN.ToString();
                lobjOTPDetails.OtpEnumTypes = OTPEnumTypes.LOGIN;
                ProgramDefinition lobjProgramDefinition = GetProgramMaster();
                lobjOTPDetails.ProgramId = lobjProgramDefinition.ProgramId;
                lobjOTPDetails.RelationType = Convert.ToInt32(RelationType.LBMS);
                mblnStatus = lobjModel.CheckOTPExist(lobjOTPDetails);
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("CheckLoginOTP -" + ex.Message + Environment.NewLine + "Stack Trace-" + ex.StackTrace);
            }
            return mblnStatus;
        }
        public ItineraryDetails GetFlightReceipt(int pintBookingId)
        {
            IBECTClient lobjIBECTClient = new IBECTClient();
            try
            {
                return lobjIBECTClient.GetFlightReceipt(pintBookingId);
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("GetFlightReceipt - Model - Exception :" + ex.InnerException.Message);
                return null;
            }
        }
        public HotelRepriceResponse HotelReprice(HotelRepriceRequest pobjHotelRepriceRequest)
        {
            try
            {
                IBECTHotelClient client = new IBECTHotelClient();
                return client.HotelReprice(pobjHotelRepriceRequest);
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("HotelReprice :" + ex.Message + Environment.NewLine + "Stack Trace :" + ex.StackTrace + Environment.NewLine + "InnerException:" + ex.InnerException);
                return null;
            }
        }
        public double CalculateAmount(int pintPonits, double pintPointRate)
        {
            //double ldecAmount = Math.Ceiling(pintPonits * pintPointRate);
            double ldecAmount = Convert.ToDouble(string.Format("{0:0.00}", Convert.ToDecimal(pintPonits * pintPointRate)));
            return ldecAmount;
        }
        public int CalculatePoints(decimal pfltAmount, float pintPointRate)
        {
            int lintPoint = Convert.ToInt32(Math.Ceiling(pfltAmount / (decimal)pintPointRate));
            return lintPoint;
        }
        public bool GenerateOTPForgotPassword(string pstrMemberid, string pstrSourceIpAddress)
        {
            ProgramDefinition lobjProgramDefinition = GetProgramMaster();
            if (lobjProgramDefinition != null)
            {
                SystemParameter lobjSystemParameter = GetSystemParametres(lobjProgramDefinition.ProgramId);
                OTPDetails lobjOTPDetails = new OTPDetails
                {
                    UniquerefID = pstrMemberid,
                    SourceAddress = pstrSourceIpAddress,
                    SourceCode = Core.Platform.OTP.ConfigurationConstants.SourceCode.Web,
                    ProgramId = lobjProgramDefinition.ProgramId,
                    RelationType = Convert.ToInt32(RelationType.LBMS),
                    OtpType = OTPEnumTypes.FORGOTPWD.ToString(),
                    OtpEnumTypes = OTPEnumTypes.FORGOTPWD,
                    AddExpirationTimeInMinutes = Convert.ToString(lobjSystemParameter.OTPExpirationTime)
                };
                bool lboolResponse = false;
                try
                {
                    string lstrToken = GetAuthTokenforWebAPI();
                    lboolResponse = lobjAPIClientHelper.GenerateOTPByRelationReference(lobjOTPDetails, lstrToken);
                }
                catch (Exception ex)
                {
                    LoggingAdapter.WriteLog("Model GenerateOTPForgotPassword Ex-" + ex.InnerException + ex.StackTrace + ex.Message);
                }
                return lboolResponse;
            }
            else
            {
                LoggingAdapter.WriteLog("MODEL GenerateOTPForgortPassword program Detail Invalid");
                return false;
            }
        }
        public bool ForgetPassword(MemberRelation pobjMemberRelation)
        {
            bool lboolResponse = false;
            try
            {
                ProgramDefinition lobjProgramDefinition = GetProgramMaster();
                if (lobjProgramDefinition != null)
                {
                    try
                    {
                        string lstrToken = GetAuthTokenforWebAPI();
                        lboolResponse = lobjAPIClientHelper.ChangePasswordForMember(pobjMemberRelation, lstrToken);
                    }
                    catch (Exception ex)
                    {
                        LoggingAdapter.WriteLog("Model ForgetPassword Ex-" + ex.InnerException + ex.StackTrace + ex.Message);
                    }
                }
                else
                {
                    LoggingAdapter.WriteLog("MODEL ForgetPassword (Activation Facade) program Detail Invalid");
                }
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("ForgetPassword -" + ex.Message + Environment.NewLine + "Stack Trace-" + ex.StackTrace);
            }
            return lboolResponse;
        }
        public string GenerateMD5(string source)
        {
            string lstrMD5string = string.Empty;
            using (var md5Hash = MD5.Create())
            {
                var sourceBytes = Encoding.UTF8.GetBytes(source);
                var hashBytes = md5Hash.ComputeHash(sourceBytes);
                var hash = BitConverter.ToString(hashBytes).Replace("-", string.Empty);
                lstrMD5string = hash;

            }
            return lstrMD5string;
        }
        #region Core WebAPI Call
        public string GetAuthTokenforWebAPI()
        {
            string AccessToken = string.Empty;
            try
            {
                TokenModel authTokenResponse = null;
                try
                {
                    if (HttpContext.Current.Session["AccessTokenWebAPI"] != null && HttpContext.Current.Session["AccessTokenTimeWebAPI"] != null)
                    {
                        authTokenResponse = HttpContext.Current.Session["AccessTokenWebAPI"] as TokenModel;
                        DateTime dtAccessTokenTime = Convert.ToDateTime(HttpContext.Current.Session["AccessTokenTimeWebAPI"]);
                        TimeSpan dt = DateTime.Now - dtAccessTokenTime;
                        if (dt.TotalMinutes > 25)
                            authTokenResponse = null;
                    }
                }
                catch { }
                if (authTokenResponse == null)
                {
                    try
                    {
                        CoreAuthTokenRequest authTokenRequest = new CoreAuthTokenRequest();
                        authTokenRequest.client_id = ConfigurationManager.AppSettings["AuthAPIClientId"];
                        authTokenRequest.client_secret = ConfigurationManager.AppSettings["AuthAPIClientSecret"];
                        authTokenRequest.grant_type = ConfigurationManager.AppSettings["AuthAPIGrantType"];
                        authTokenResponse = lobjAPIClientHelper.GetAuthTokenforWebAPI(authTokenRequest);
                        try
                        {
                            HttpContext.Current.Session["AccessTokenWebAPI"] = authTokenResponse;
                            HttpContext.Current.Session["AccessTokenTimeWebAPI"] = DateTime.Now;
                        }
                        catch { }
                    }
                    catch (Exception ex)
                    {
                        LoggingAdapter.WriteLog("GetAuthTokenforWebAPI AccessToken null");
                        LoggingAdapter.WriteLog("Model GetAuthTokenforWebAPI Ex-" + ex.InnerException + ex.StackTrace + ex.Message);
                    }
                }
                AccessToken = authTokenResponse.results.token;
            }
            catch (Exception ex) { LoggingAdapter.WriteLog("NICModel GetAuthTokenforWebAPI Exception: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace); }
            return AccessToken;
        }
        public bool ResetLoginAttempt(MemberRelation pobjMemberRelation)
        {
            bool lboolResponse = false;
            try
            {
                string lstrToken = GetAuthTokenforWebAPI();
                lboolResponse = lobjAPIClientHelper.ResetLoginAttempt(pobjMemberRelation, lstrToken);
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("ResetLoginAttempt Ex-" + ex.InnerException + ex.Message + ex.StackTrace);
            }
            return lboolResponse;
        }
        public bool UpdateMemberShipActivitySession(MemberActivitySession pobjMemberActivitySession)
        {

            bool lboolResponse = false;
            try
            {
                string lstrToken = GetAuthTokenforWebAPI();
                lboolResponse = lobjAPIClientHelper.UpdateMemberShipActivitySession(pobjMemberActivitySession, lstrToken);
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("Model UpdateMemberShipActivitySession Ex-" + ex.InnerException + ex.StackTrace + ex.Message);
            }
            return lboolResponse;
        }
        public SystemParameter GetSystemParametres(int pintProgramId)
        {
            SystemParameter lobjSystemParameter = null;
            try
            {
                if (HttpContext.Current.Application["SystemParameters"] != null)
                {
                    lobjSystemParameter = HttpContext.Current.Application["SystemParameters"] as SystemParameter;
                }
                else
                {
                    string lstrToken = GetAuthTokenforWebAPI();
                    lobjSystemParameter = lobjAPIClientHelper.GetSystemParameter(pintProgramId, lstrToken);
                    HttpContext.Current.Application["SystemParameters"] = lobjSystemParameter;
                }
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("GetSystemParametres :" + ex.Message + Environment.NewLine + "Stack Trace :" + ex.StackTrace);
            }
            return lobjSystemParameter;
        }
        public bool ChangePasswordForMember(string pstrMembershipref, string pstrMemberPassword)
        {
            ProgramDefinition lobjProgramDefinition = GetProgramMaster();
            MemberRelation lobjMemberRelation = new MemberRelation
            {
                RelationReference = pstrMembershipref,
                WebPassword = pstrMemberPassword,
                RelationType = RelationType.LBMS,
                ProgramId = lobjProgramDefinition.ProgramId
            };
            bool lboolResponse = false;
            try
            {
                string lstrToken = GetAuthTokenforWebAPI();
                lboolResponse = lobjAPIClientHelper.ChangePasswordForMember(lobjMemberRelation, lstrToken);
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("Model ChangePasswordForMember Ex-" + ex.InnerException + ex.StackTrace + ex.Message);
            }
            return lboolResponse;
        }
        public ProgramDefinition GetProgramMaster()
        {
            ProgramDefinition lobjProgramMaster = null;
            try
            {
                try
                {
                    if (HttpContext.Current.Application["ProgramMaster"] != null)
                    {
                        lobjProgramMaster = HttpContext.Current.Application["ProgramMaster"] as ProgramDefinition;
                    }
                }
                catch { }
                if (lobjProgramMaster == null)
                {
                    string lstrProgramName = string.Empty;
                    lstrProgramName = ConfigurationManager.AppSettings["ProgramName"].ToString();
                    lobjProgramMaster = GetProgramDetails(lstrProgramName);
                    try
                    {
                        HttpContext.Current.Application["ProgramMaster"] = lobjProgramMaster;
                    }
                    catch { }
                }
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("NICModel GetProgramMaster Exception: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
            }
            return lobjProgramMaster;
        }
        public List<ProgramDefinition> GetProgramDefinitionList()
        {
            List<ProgramDefinition> lobjlistProgramDefinition = null;
            try
            {
                if (HttpContext.Current.Application["ProgramDefinitionList"] != null)
                {
                    lobjlistProgramDefinition = HttpContext.Current.Application["ProgramDefinitionList"] as List<ProgramDefinition>;
                }
                else
                {
                    string lstrToken = GetAuthTokenforWebAPI();
                    lobjlistProgramDefinition = lobjAPIClientHelper.GetProgramDefinitionList(lstrToken);
                }
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("Model GetProgramDefinitionList Ex-" + ex.InnerException + ex.StackTrace + ex.Message);
            }
            return lobjlistProgramDefinition;
        }
        public ProgramDefinition GetProgramDetails(string pstrProgramName)
        {
            ProgramDefinition lobjProgramDefination = null;
            try
            {
                string lstrToken = GetAuthTokenforWebAPI();
                lobjProgramDefination = lobjAPIClientHelper.GetProgramDefinition(pstrProgramName, lstrToken);
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("GetProgramDetails :" + ex.Message + Environment.NewLine + "Stack Trace :" + ex.StackTrace);
            }
            return lobjProgramDefination;
        }
        public List<ProgramProductCode> GetProgramProductCodeList(int pintProgramId)
        {
            LoggingAdapter.WriteLog("GetProgramProductCodeList pintProgramId-" + pintProgramId);
            List<ProgramProductCode> lobjListProgramProductCode = null;
            try
            {
                if (HttpContext.Current.Application["ProgramProductCode"] != null)
                {
                    lobjListProgramProductCode = HttpContext.Current.Application["ProgramProductCode"] as List<Core.Platform.ProgramMaster.Entities.ProgramProductCode>;
                }
                else
                {
                    string lstrToken = GetAuthTokenforWebAPI();
                    lobjListProgramProductCode = lobjAPIClientHelper.GetProgramProductCodeList(pintProgramId, lstrToken);
                }
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("GetProgramProductCodeList :" + ex.Message + Environment.NewLine + "Stack Trace :" + ex.StackTrace);
            }
            return lobjListProgramProductCode;
        }
        public List<ProgramCurrencyDefinition> GetAllCurrencyDefinition(int pintProductId)
        {
            try
            {
                string lstrToken = GetAuthTokenforWebAPI();
                return lobjAPIClientHelper.GetProgramCurrencyDefinitionList(pintProductId, lstrToken);
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("GetAllCurrencyDefinition :" + ex.Message + Environment.NewLine + "Stack Trace :" + ex.StackTrace);
                return null;
            }
        }
        public List<ProgramCurrencyDefinition> GetProductProgramCurrencyDefinition(MemberDetails pobjMemberDetails)
        {
            List<ProgramCurrencyDefinition> lobjreturnProgramCurrencyDefinitionlst = new List<ProgramCurrencyDefinition>();
            try
            {
                if (HttpContext.Current.Application["ProgramCurrencyDefinition"] != null)
                {
                    lobjreturnProgramCurrencyDefinitionlst = HttpContext.Current.Application["ProgramCurrencyDefinition"] as List<ProgramCurrencyDefinition>;
                }
                else
                {
                    List<MemberSubRelation> lobjMemberSubRelationlst = pobjMemberDetails.MemberRelationsList.Find(lobj => lobj.RelationType.Equals(Core.Platform.Member.Entites.RelationType.LBMS)).MemberSubRelationList;
                    string lstrProgramName = ConfigurationManager.AppSettings["ProgramName"].ToString();

                    ProgramDefinition lobjProgramDefinition = null;
                    lobjProgramDefinition = GetProgramDetails(lstrProgramName);

                    List<ProgramProductCode> lobjListProgramProductCode = null;
                    List<ProgramCurrencyDefinition> lobjListProgramCurrencyDefinition = null;
                    lobjListProgramProductCode = GetProgramProductCodeList(lobjProgramDefinition.ProgramId);
                    lobjListProgramCurrencyDefinition = GetAllCurrencyDefinition(lobjProgramDefinition.ProgramId);
                    string lstrCurrency = GetDefaultCurrency();
                    if (lobjListProgramProductCode != null && lobjListProgramCurrencyDefinition != null)
                    {
                        List<ProgramProductCode> lobjListfindProgramProductCode = new List<ProgramProductCode>();
                        ProgramProductCode lobjProgramProductCode = null;
                        for (int i = 0; i < lobjMemberSubRelationlst.Count; i++)
                        {
                            lobjProgramProductCode = new ProgramProductCode();
                            lobjProgramProductCode = lobjListProgramProductCode.Find(lobj => lobj.ProductCode.Equals(lobjMemberSubRelationlst[i].ProductCode));

                            if (lobjProgramProductCode != null)
                            {
                                if (lobjListfindProgramProductCode.FindAll(lobj => lobj.Currency.Equals(lobjProgramProductCode.Currency)).Count == 0)
                                {
                                    lobjListfindProgramProductCode.Add(lobjProgramProductCode);
                                }
                            }
                        }

                        if (lobjListfindProgramProductCode != null && lobjListfindProgramProductCode.Count > 0)
                        {
                            lobjListfindProgramProductCode = lobjListfindProgramProductCode.OrderByDescending(x => x.Currency.Equals(lstrCurrency)).ToList();
                        }

                        for (int j = 0; j < lobjListfindProgramProductCode.Count; j++)
                        {
                            lobjreturnProgramCurrencyDefinitionlst.Add(lobjListProgramCurrencyDefinition.Find(lobj => lobj.Currency.Equals(lobjListfindProgramProductCode[j].Currency)));
                        }
                    }

                    if (lobjreturnProgramCurrencyDefinitionlst.Count == 0 && lobjListProgramCurrencyDefinition != null)
                    {
                        lobjreturnProgramCurrencyDefinitionlst.Add(lobjListProgramCurrencyDefinition.Find(lobj => lobj.Currency.Equals(lstrCurrency)));
                    }
                    HttpContext.Current.Application["ProgramCurrencyDefinition"] = lobjreturnProgramCurrencyDefinitionlst;
                }
            }
            catch (Exception Ex)
            {
                LoggingAdapter.WriteLog("GetProductProgramCurrencyDefinition Ex-" + Ex.Message + Ex.InnerException + Ex.StackTrace);
            }
            return lobjreturnProgramCurrencyDefinitionlst;
        }
        public string GetDefaultCurrency()
        {
            string lstrCurrency = string.Empty;
            try
            {
                ProgramDefinition lobjProgramDefinition = GetProgramMaster();
                if (HttpContext.Current.Application["DefaultCurrency"] != null)
                {
                    lstrCurrency = HttpContext.Current.Application["DefaultCurrency"] as string;
                }
                else
                {
                    List<ProgramCurrencyDefinition> lobjListOfProgramCurrencyDefinition = new List<ProgramCurrencyDefinition>();
                    lobjListOfProgramCurrencyDefinition = GetProgramCurrencyDefinition(lobjProgramDefinition.ProgramId);
                    lstrCurrency = lobjListOfProgramCurrencyDefinition.Find(lobj => lobj.IsDefault.Equals(true)).Currency;
                    HttpContext.Current.Application["ProgramCurrencyDefinition"] = lobjListOfProgramCurrencyDefinition;
                    HttpContext.Current.Application["DefaultCurrency"] = lstrCurrency;
                }
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("NICModel GetDefaultCurrency Exception: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
            }
            return lstrCurrency;
        }
        public List<ProgramCurrencyDefinition> GetProgramCurrencyDefinition(int pintProgramId)
        {
            List<ProgramCurrencyDefinition> lobjListOfProgramCurrencyDefinition = null;
            try
            {
                if (HttpContext.Current.Application["ProgramCurrency"] != null)
                {
                    lobjListOfProgramCurrencyDefinition = HttpContext.Current.Application["ProgramCurrency"] as List<ProgramCurrencyDefinition>;
                }
                else
                {
                    string lstrToken = GetAuthTokenforWebAPI();
                    lobjListOfProgramCurrencyDefinition = lobjAPIClientHelper.GetProgramCurrencyDefinitionList(pintProgramId, lstrToken);
                    HttpContext.Current.Application["ProgramCurrency"] = lobjListOfProgramCurrencyDefinition;
                }
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("GetProgramCurrencyDefinition :" + ex.Message + Environment.NewLine + "Stack Trace :" + ex.StackTrace);
            }
            return lobjListOfProgramCurrencyDefinition;
        }
        public List<CustomerSegment> GetAllCustomerSegmentlst(int pintProgramId)
        {
            List<CustomerSegment> lobjListCustomerSegment = null;
            try
            {
                if (HttpContext.Current.Application["CustomerSegment"] != null)
                {
                    lobjListCustomerSegment = HttpContext.Current.Application["CustomerSegment"] as List<CustomerSegment>;
                }
                else
                {
                    string lstrToken = GetAuthTokenforWebAPI();
                    lobjListCustomerSegment = lobjAPIClientHelper.GetAllCustomerSegmentlst(pintProgramId, lstrToken);
                }
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("GetAllCustomerSegmentlst :" + ex.Message + Environment.NewLine + "Stack Trace :" + ex.StackTrace);
            }
            return lobjListCustomerSegment;
        }
        public List<RedemptionKeys> GetAllRedemptionKeys(int pintProgramId)
        {
            List<RedemptionKeys> lobjListRedemptionKeys = null;
            try
            {
                if (HttpContext.Current.Application["RedemptionKeys"] != null)
                {
                    lobjListRedemptionKeys = HttpContext.Current.Application["RedemptionKeys"] as List<RedemptionKeys>;
                }
                else
                {
                    string lstrToken = GetAuthTokenforWebAPI();
                    lobjListRedemptionKeys = lobjAPIClientHelper.GetRedemptionKeyslst(pintProgramId, lstrToken);
                    HttpContext.Current.Application["RedemptionKeys"] = lobjListRedemptionKeys;
                }
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("GetAllRedemptionKeys :" + ex.Message + Environment.NewLine + "Stack Trace :" + ex.StackTrace);
            }
            return lobjListRedemptionKeys;
        }
        public float GetProgramRedemptionRate(string pstrProgramCurrency, string pstrRedemtionCode, int pintProgramId)
        {
            float lftRedemptionRate = 0.0f;
            ProgramRedemptionRate lobjProgramRedemptionRate = new ProgramRedemptionRate();
            try
            {
                string lstrKey = "RedemptionRate" + pstrProgramCurrency + pstrRedemtionCode;
                if (HttpContext.Current.Application[lstrKey] != null)
                {
                    lftRedemptionRate = Convert.ToSingle(HttpContext.Current.Application[lstrKey]);
                }
                else
                {
                    lobjProgramRedemptionRate.ProgramId = pintProgramId;
                    lobjProgramRedemptionRate.ProgramCurrency = pstrProgramCurrency;
                    lobjProgramRedemptionRate.RedemptionCode = pstrRedemtionCode;
                    string lstrToken = GetAuthTokenforWebAPI();
                    lftRedemptionRate = lobjAPIClientHelper.GetProgramRedemptionRate(lobjProgramRedemptionRate, lstrToken);
                    HttpContext.Current.Application[lstrKey] = lftRedemptionRate;
                }
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("GetProgramRedemptionRate :" + ex.Message + Environment.NewLine + "Stack Trace :" + ex.StackTrace);
            }
            return lftRedemptionRate;
        }
        public TxnSummaryDetails GetMemberStatementSummary(SearchTransactions pobjSearchTransactions)
        {
            try
            {
                string lstrToken = GetAuthTokenforWebAPI();
                return lobjAPIClientHelper.GetMemberStatementSummary(pobjSearchTransactions, lstrToken);
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("Model GetMemberStatementSummary Ex: " + ex.Message + Environment.NewLine + ex.StackTrace + Environment.NewLine + ex.InnerException);
                return null;
            }
        }
        public List<string> PageDataRangeList(int TotalRowCount, int Perpagedata)
        {
            List<string> objPageNoList = new List<string>();
            try
            {
                List<int> objPerpagedatainfo = new List<int>
                {
                    TotalRowCount,
                    Perpagedata
                };
                int pobjTotalRowCount = objPerpagedatainfo[0];
                List<string> mObjPageDataRangeList = new List<string>();
                Double dblpgno = (Convert.ToDouble(TotalRowCount) / Perpagedata);
                dblpgno = Math.Ceiling(dblpgno);
                int TotalPages = Convert.ToInt32(dblpgno);
                if (TotalRowCount < Perpagedata)
                {
                    objPageNoList.Add("0" + "-" + pobjTotalRowCount);
                }
                else
                {
                    for (int i = 0; i < TotalPages; i++)
                    {
                        string objBaseTransactionReportObject = string.Empty;
                        string[] objpgdata;
                        if (TotalRowCount < Perpagedata && i == 0)
                        {
                            objBaseTransactionReportObject = "0" + "-" + TotalRowCount;
                        }
                        else if (TotalRowCount < Perpagedata && i > 0)
                        {
                            objpgdata = mObjPageDataRangeList[i - 1].Split('-');
                            objBaseTransactionReportObject = Convert.ToString(Convert.ToInt32(objpgdata[1]) + 1) + "-" + pobjTotalRowCount;
                        }
                        else
                        {
                            if (mObjPageDataRangeList.Count > 0 && i > 0)
                            {
                                objpgdata = mObjPageDataRangeList[i - 1].Split('-');
                                objBaseTransactionReportObject = Convert.ToString(Convert.ToInt32(objpgdata[1]) + 1) + "-" + Convert.ToString(Convert.ToInt32(objpgdata[1]) + Perpagedata);
                            }
                            else if (mObjPageDataRangeList.Count > 0 && i == TotalPages - 1)
                            {
                                objpgdata = mObjPageDataRangeList[i - 1].Split('-');
                                objBaseTransactionReportObject = Convert.ToString(Convert.ToInt32(objpgdata[1]) + 1) + "-" + pobjTotalRowCount;
                            }
                            else
                            {
                                objBaseTransactionReportObject = Convert.ToInt32(0).ToString() + "-" + Perpagedata;
                            }
                        }
                        mObjPageDataRangeList.Add(objBaseTransactionReportObject);
                        objPageNoList.Add(objBaseTransactionReportObject);
                        TotalRowCount -= Perpagedata;
                    }
                }
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("Model PageDataRangeList Ex-" + ex.InnerException + ex.StackTrace + ex.Message);
            }
            return objPageNoList;
        }
        public ScheduleExpiry GetExpirySchedule(int pintYear, MemberDetails pobjMemberDetails)
        {
            try
            {
                ScheduleExpiry pobjScheduleExpiry = new ScheduleExpiry();
                ScheduleExpiry lobjScheduleExpiry = null;
                try
                {
                    string lstrToken = GetAuthTokenforWebAPI();
                    MemberRelation lobjMemberRelation = pobjMemberDetails.MemberRelationsList.Find(lobj => lobj.RelationType.Equals(RelationType.LBMS));

                    pobjScheduleExpiry.Year = pintYear;
                    pobjScheduleExpiry.RelationReference = lobjMemberRelation.RelationReference;
                    pobjScheduleExpiry.RelationType = Convert.ToInt32(RelationType.LBMS);
                    pobjScheduleExpiry.ProgramId = pobjMemberDetails.ProgramId;
                    pobjScheduleExpiry.TransactionCurrency = GetDefaultCurrency();

                    lobjScheduleExpiry = lobjAPIClientHelper.GetExpirySchedule(pobjScheduleExpiry, lstrToken);
                }
                catch (Exception ex)
                {
                    LoggingAdapter.WriteLog("Model GetExpirySchedule Ex-" + ex.InnerException + ex.StackTrace + ex.Message);
                }
                return lobjScheduleExpiry;
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("GetExpirySchedule -" + ex.Message + Environment.NewLine + "Stack Trace-" + ex.StackTrace);
                return null;
            }
        }
        public ScheduleExpiry GetTransactionExpirySchedule(ScheduleExpiry lobjScheduleExpiry)
        {
            try
            {
                ScheduleExpiry lobjNewScheduleExpiry = null;
                try
                {
                    string lstrToken = GetAuthTokenforWebAPI();
                    lobjNewScheduleExpiry = lobjAPIClientHelper.GetTransactionExpirySchedule(lobjScheduleExpiry, lstrToken);
                }
                catch (Exception ex)
                {
                    LoggingAdapter.WriteLog("Model GetTransactionExpirySchedule Ex-" + ex.InnerException + ex.StackTrace + ex.Message);
                }
                return lobjNewScheduleExpiry;

            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("GetTransactionExpirySchedule -" + ex.Message + Environment.NewLine + "Stack Trace-" + ex.StackTrace);
                return null;
            }
        }
        public ScheduleExpiry GetNextExpiredPointsOnDate(ScheduleExpiry lobjScheduleExpiry, int pintYear)
        {
            try
            {
                lobjScheduleExpiry.Year = pintYear;
                ScheduleExpiry lobjNewScheduleExpiry = null;
                try
                {
                    string lstrToken = GetAuthTokenforWebAPI();
                    lobjNewScheduleExpiry = lobjAPIClientHelper.GetNextExpiredPointsOnDate(lobjScheduleExpiry, lstrToken);
                }
                catch (Exception ex)
                {
                    LoggingAdapter.WriteLog("Model GetNextExpiredPointsOnDate Ex-" + ex.InnerException + ex.StackTrace + ex.Message);
                }
                return lobjNewScheduleExpiry;

            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("GetTransactionExpirySchedule -" + ex.Message + Environment.NewLine + "Stack Trace-" + ex.StackTrace);
                return null;
            }
        }
        public ScheduleExpiry GetNextExpirySchedule(ScheduleExpiry lobjScheduleExpiry, int pintYear)
        {
            try
            {
                lobjScheduleExpiry.Year = pintYear;
                ScheduleExpiry lobjNewScheduleExpiry = null;
                try
                {
                    string lstrToken = GetAuthTokenforWebAPI();
                    lobjNewScheduleExpiry = lobjAPIClientHelper.GetNextYearExpirySchedule(lobjScheduleExpiry, lstrToken);
                }
                catch (Exception ex)
                {
                    LoggingAdapter.WriteLog("Model GetNextYearExpirySchedule Ex-" + ex.InnerException + ex.StackTrace + ex.Message);
                }
                return lobjNewScheduleExpiry;
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("GetNextExpirySchedule -" + ex.Message + Environment.NewLine + "Stack Trace-" + ex.StackTrace);
                return null;
            }
        }
        public int LogActivity(string pstrActivity, ActivityType pobjActivityType)
        {
            int lintActivityID = 0;
            try
            {
                if (HttpContext.Current.Session["MemberActivitySession"] != null)
                {
                    string lstrToken = GetAuthTokenforWebAPI();
                    string pstrPageUrl = HttpContext.Current.Request.Url.OriginalString;
                    MemberActivitySession lobjMemberActivitySession = HttpContext.Current.Session["MemberActivitySession"] as MemberActivitySession;
                    MemberActivityMaster lobjMemberActivity = new MemberActivityMaster
                    {
                        Activity = pstrActivity,
                        PageURL = pstrPageUrl,
                        ActivityType = pobjActivityType,
                        ProgramId = lobjMemberActivitySession.ProgramId
                    };
                    List<MemberActivityMaster> lobjMemberActivityList = new List<MemberActivityMaster>
                    {
                        lobjMemberActivity
                    };
                    try
                    {
                        MemberActivityRequest lobjMemberActivityRequest = new MemberActivityRequest
                        {
                            ID = lobjMemberActivitySession.Id,
                            MemberActivityLst = lobjMemberActivityList,
                            MembershipReference = lobjMemberActivitySession.ReferenceNumber
                        };
                        lintActivityID = lobjAPIClientHelper.InsertMemberActivity(lobjMemberActivityRequest, lstrToken);
                    }
                    catch (Exception ex)
                    {
                        LoggingAdapter.WriteLog("Model LogActivity Ex-" + ex.InnerException + ex.StackTrace + ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("NICModel LogActivity Exception: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
            }
            return lintActivityID;
        }
        public MemberDetails CheckMembershipCredentialsByNationalID(string pstrMemberid, string pstrPassword)
        {
            MemberDetails lobjMemberDetails;
            try
            {
                ProgramDefinition lobjProgramDefinition = null;
                lobjProgramDefinition = GetProgramMaster();
                string lstrToken = GetAuthTokenforWebAPI();
                LoginDetails lobjLoginDetails = new LoginDetails
                {
                    UniquerefID = pstrMemberid,
                    RelationType = Convert.ToInt32(RelationType.LBMS),
                    ProgramId = lobjProgramDefinition.ProgramId,
                    Password = pstrPassword.ToUpper()
                };
                lobjMemberDetails = lobjAPIClientHelper.CheckMembershipCredentialsByNationalID(lobjLoginDetails, lstrToken);
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("Model CheckMembershipCredentialsByNationalID Ex: " + ex.Message + Environment.NewLine + ex.StackTrace + Environment.NewLine + ex.InnerException);
                lobjMemberDetails = null;
            }
            return lobjMemberDetails;
        }
        public bool CheckOTPExist(OTPDetails pobjDetails)
        {
            try
            {
                bool lboolResponse = false;
                try
                {
                    string lstrToken = GetAuthTokenforWebAPI();
                    lboolResponse = lobjAPIClientHelper.VerifyOTP(pobjDetails, lstrToken);
                }
                catch (Exception ex)
                {
                    LoggingAdapter.WriteLog("Model CheckOTPExist Ex-" + ex.InnerException + ex.StackTrace + ex.Message);
                }
                return lboolResponse;
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("CheckOTPExist Exception:" + ex.Message + Environment.NewLine + "InnerException:" + ex.InnerException + Environment.NewLine + "StackTrace:" + ex.StackTrace);
                return false;
            }
        }
        public bool GenerateLoginOTP(string pstrMemberid, string pstrSourceIpAddress)
        {
            bool lboolResponse = false;
            try
            {
                ProgramDefinition lobjProgramDefinition = GetProgramMaster();
                SystemParameter lobjSystemParameter = GetSystemParametres(lobjProgramDefinition.ProgramId);
                if (lobjProgramDefinition != null)
                {
                    OTPDetails lobjOTPDetails = new OTPDetails
                    {
                        UniquerefID = pstrMemberid,
                        SourceAddress = pstrSourceIpAddress,
                        SourceCode = Core.Platform.OTP.ConfigurationConstants.SourceCode.Web,
                        ProgramId = lobjProgramDefinition.ProgramId,
                        RelationType = Convert.ToInt32(RelationType.LBMS),
                        OtpType = Convert.ToString(OTPEnumTypes.LOGIN),
                        OtpEnumTypes = OTPEnumTypes.LOGIN,
                        AddExpirationTimeInMinutes = Convert.ToString(lobjSystemParameter.OTPExpirationTime)
                    };
                    try
                    {
                        string lstrToken = GetAuthTokenforWebAPI();
                        lboolResponse = lobjAPIClientHelper.GenerateOTPByRelationReference(lobjOTPDetails, lstrToken);
                    }
                    catch (Exception ex)
                    {
                        LoggingAdapter.WriteLog("Model GenerateLoginOTP Ex-" + ex.InnerException + ex.StackTrace + ex.Message);
                    }
                }
                else
                {
                    LoggingAdapter.WriteLog("MODEL GenerateLoginOTP program Detail Invalid");
                }
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("MODEL GenerateLoginOTP " + ex.Message + "StackTrace" + ex.StackTrace);
            }
            return lboolResponse;
        }
        public MemberDetails GetMemberDetails(string pstrMemberId)
        {
            MemberDetails lobjMemberDetails = null;
            try
            {
                ProgramDefinition lobjProgramDefinition = GetProgramMaster();
                if (lobjProgramDefinition != null)
                {
                    try
                    {
                        string lstrToken = GetAuthTokenforWebAPI();
                        lobjMemberDetails = lobjAPIClientHelper.GetMemberDetails(pstrMemberId, lobjProgramDefinition.ProgramId, (int)RelationType.LBMS, lstrToken);
                    }
                    catch (Exception ex)
                    {
                        LoggingAdapter.WriteLog("Model GetMemberDetails Ex-" + ex.InnerException + ex.StackTrace + ex.Message);
                    }
                }
                else
                {
                    LoggingAdapter.WriteLog("Program Detail Null");
                }
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("NICModel GetMemberDetails Exception: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
            }
            return lobjMemberDetails;
        }
        public MemberDetails GetMemberDetailsUsingNationalID(string pstrNationalID)
        {
            MemberDetails lobjMemberDetails = null;
            try
            {
                ProgramDefinition lobjProgramDefinition = null;
                lobjProgramDefinition = GetProgramMaster();
                string lstrToken = GetAuthTokenforWebAPI();
                lobjMemberDetails = lobjAPIClientHelper.GetMemberDetailsByNationalID(pstrNationalID, lobjProgramDefinition.ProgramId, Convert.ToInt32(RelationType.LBMS), lstrToken);
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("Model GetMemberDetailsUsingNationalID Ex: " + ex.Message + Environment.NewLine + ex.StackTrace + Environment.NewLine + ex.InnerException);
            }
            return lobjMemberDetails;
        }
        public MemberDetails GetMemberDetailsByEmailId(string pstrEmailId)
        {
            MemberDetails lobjMemberDetails = null;
            try
            {
                ProgramDefinition lobjProgramDefinition = null;
                lobjProgramDefinition = GetProgramMaster();
                if (lobjProgramDefinition != null)
                {
                    SearchMember lobjParameters = new SearchMember
                    {
                        EmailID = pstrEmailId,
                        ProgramId = lobjProgramDefinition.ProgramId,
                        RelationType = Convert.ToInt32(RelationType.LBMS)
                    };
                    try
                    {
                        string lstrToken = GetAuthTokenforWebAPI();
                        lobjMemberDetails = lobjAPIClientHelper.GetMemberDetailsByEmailID(pstrEmailId, lobjProgramDefinition.ProgramId, Convert.ToInt32(RelationType.LBMS), lstrToken);
                    }
                    catch (Exception ex)
                    {
                        LoggingAdapter.WriteLog("Model GetMemberLoginDetailsByEmailId Ex-" + ex.InnerException + ex.StackTrace + ex.Message);
                    }
                }
                else
                {
                    LoggingAdapter.WriteLog("Program Detail Null");
                    return null;
                }
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("NICModel GetMemberDetailsByEmailId Exception: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
            }
            return lobjMemberDetails;
        }
        public bool GenerateOTPByMemberId(string pstrMemberid, string pstrSourceIpAddress)
        {
            bool lboolResponse = false;
            try
            {
                ProgramDefinition lobjProgramDefinition = GetProgramMaster();
                SystemParameter lobjSystemParameter = GetSystemParametres(lobjProgramDefinition.ProgramId);
                if (lobjProgramDefinition != null)
                {
                    OTPDetails lobjOTPDetails = new OTPDetails
                    {
                        UniquerefID = pstrMemberid,
                        SourceAddress = pstrSourceIpAddress,
                        SourceCode = Core.Platform.OTP.ConfigurationConstants.SourceCode.Web,
                        ProgramId = lobjProgramDefinition.ProgramId,
                        RelationType = Convert.ToInt32(RelationType.LBMS),
                        OtpType = OTPEnumTypes.ACTIVATION.ToString(),
                        OtpEnumTypes = OTPEnumTypes.ACTIVATION,
                        AddExpirationTimeInMinutes = Convert.ToString(lobjSystemParameter.OTPExpirationTime)
                    };
                    try
                    {
                        string lstrToken = GetAuthTokenforWebAPI();
                        lboolResponse = lobjAPIClientHelper.GenerateOTPByRelationReference(lobjOTPDetails, lstrToken);
                    }
                    catch (Exception ex)
                    {
                        LoggingAdapter.WriteLog("Model GenerateOTPDetails Ex-" + ex.InnerException + ex.StackTrace + ex.Message);
                    }
                }
                else
                {
                    LoggingAdapter.WriteLog("MODEL GenerateOTP (Activation Facade) program Detail Invalid");
                }
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("NICModel GenerateOTPByMemberId Exception: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
            }
            return lboolResponse;
        }
        public bool CheckMemberExistsUsingEmailID(string pstrEmailID)
        {
            try
            {
                bool lboolResponse = false;
                try
                {
                    string lstrToken = GetAuthTokenforWebAPI();
                    lboolResponse = lobjAPIClientHelper.CheckMemberExistsUsingEmailID(pstrEmailID, lstrToken);
                }
                catch (Exception ex)
                {
                    LoggingAdapter.WriteLog("Model CheckMemberExistsUsingEmailID Ex-" + ex.InnerException + ex.StackTrace + ex.Message);
                }
                return lboolResponse;
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("CheckMemberExistsUsingEmailID Exception:" + ex.Message + Environment.NewLine + "InnerException:" + ex.InnerException + Environment.NewLine + "StackTrace:" + ex.StackTrace);
                return true;
            }
        }
        public int GetTotalMemberTransaction(SearchTransactions pobjSearchTransactions)
        {
            int lintResponse = 0;
            try
            {
                ProgramDefinition lobjProgramDefinition = null;
                lobjProgramDefinition = GetProgramMaster();
                if (lobjProgramDefinition != null)
                {
                    try
                    {
                        string lstrToken = GetAuthTokenforWebAPI();
                        lintResponse = lobjAPIClientHelper.GetTotalMemberTransaction(pobjSearchTransactions, lstrToken);
                    }
                    catch (Exception ex)
                    {
                        LoggingAdapter.WriteLog("Model GetTotalMemberTransaction Ex-" + ex.InnerException + ex.StackTrace + ex.Message);
                    }
                }
                else
                {
                    LoggingAdapter.WriteLog("Program Detail Null");
                }
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("NICModel GetTotalMemberTransaction Exception: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
            }
            return lintResponse;
        }
        public int GetTotalMemberTransactionByDate(SearchTransactions pobjSearchTransactions)
        {
            int lintResponse = 0;
            try
            {
                ProgramDefinition lobjProgramDefinition = GetProgramMaster();
                if (lobjProgramDefinition != null)
                {
                    List<ProgramCurrencyDefinition> lobjListProgramCurrencyDefinition = GetAllCurrencyDefinition(lobjProgramDefinition.ProgramId);
                }
                else
                {
                    LoggingAdapter.WriteLog("Program Detail Null");
                }
                try
                {
                    string lstrToken = GetAuthTokenforWebAPI();
                    lintResponse = lobjAPIClientHelper.GetTotalMemberTransactionByDate(pobjSearchTransactions, lstrToken);
                }
                catch (Exception ex)
                {
                    LoggingAdapter.WriteLog("Model GetTotalMemberTransactionByDate Ex-" + ex.InnerException + ex.StackTrace + ex.Message);
                }
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("NICModel GetTotalMemberTransactionByDate Exception: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
            }
            return lintResponse;
        }
        public List<TransactionDetails> GetMemberTransactionSummary(SearchTransactions pobjSearchTransactions)
        {
            List<TransactionDetails> lobjListTransactionDetails = null;
            try
            {
                ProgramDefinition lobjProgramDefinition = null;
                lobjProgramDefinition = GetProgramMaster();
                if (lobjProgramDefinition != null)
                {

                    pobjSearchTransactions.ProgramId = lobjProgramDefinition.ProgramId;
                    pobjSearchTransactions.RelationType = Convert.ToInt32(RelationType.LBMS);
                    try
                    {
                        string lstrToken = GetAuthTokenforWebAPI();
                        lobjListTransactionDetails = lobjAPIClientHelper.GetMemberTransactionSummary(pobjSearchTransactions, lstrToken);
                    }
                    catch (Exception ex)
                    {
                        LoggingAdapter.WriteLog("Model GetMemberTransactionSummary Ex-" + ex.InnerException + ex.StackTrace + ex.Message);
                    }
                }
                else
                {
                    LoggingAdapter.WriteLog(" GetMemberTransactionSummary Program Detail Null");
                }
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("NICModel GetMemberTransactionSummary Exception: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
            }
            return lobjListTransactionDetails;
        }
        public List<TransactionDetails> GetMemberTransactionSummaryByDate(SearchTransactions pobjSearchTransactions)
        {
            List<TransactionDetails> lobjListTransactionDetails = null;
            try
            {
                ProgramDefinition lobjProgramDefinition = null;
                lobjProgramDefinition = GetProgramMaster();
                if (lobjProgramDefinition != null)
                {
                    pobjSearchTransactions.ProgramId = lobjProgramDefinition.ProgramId;
                    pobjSearchTransactions.RelationType = Convert.ToInt32(RelationType.LBMS);
                    try
                    {
                        string lstrToken = GetAuthTokenforWebAPI();
                        lobjListTransactionDetails = lobjAPIClientHelper.GetMemberTransactionSummaryByDate(pobjSearchTransactions, lstrToken);
                    }
                    catch (Exception ex)
                    {
                        LoggingAdapter.WriteLog("Model GetMemberTransactionSummaryByDate Ex-" + ex.InnerException + ex.StackTrace + ex.Message);
                    }
                }
                else
                {
                    LoggingAdapter.WriteLog(" GetTotalMemberAccrualTransaction Program Detail Null");
                }
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("NICModel GetMemberTransactionSummaryByDate Exception: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
            }
            return lobjListTransactionDetails;
        }
        public List<TransactionAdditionalInfo> GetTransactionAdditionalInfo(int pintProgramId, string pstrTransactionId)
        {
            List<TransactionAdditionalInfo> lobjListTransactionDetails = null;
            try
            {
                ProgramDefinition lobjProgramDefinition = null;
                lobjProgramDefinition = GetProgramMaster();
                if (lobjProgramDefinition != null)
                {
                    try
                    {
                        string lstrToken = GetAuthTokenforWebAPI();
                        lobjListTransactionDetails = lobjAPIClientHelper.GetTransactionAdditionalInfo(pintProgramId, pstrTransactionId, lstrToken);
                    }
                    catch (Exception ex)
                    {
                        LoggingAdapter.WriteLog("Model GetTransactionAdditionalInfo Ex-" + ex.InnerException + ex.StackTrace + ex.Message);
                    }
                }
                else
                {
                    LoggingAdapter.WriteLog(" GetTransactionAdditionalInfo Program Detail Null");
                }
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("SBLModel GetTransactionAdditionalInfo Exception: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
            }
            return lobjListTransactionDetails;
        }
        public int CheckAvailbility(string pstrRelationReference, int pintRelationType, string pstrCurrency, int pintProgramId)
        {
            PGAvailabilityRequest lobjPGRequest = new PGAvailabilityRequest();
            try
            {
                string lstrToken = GetAuthTokenforWebAPI();
                lobjPGRequest.ProgramId = pintProgramId;
                lobjPGRequest.RelationReference = pstrRelationReference;
                lobjPGRequest.RelationType = pintRelationType;
                lobjPGRequest.TransactionCurrency = pstrCurrency;
                return lobjAPIClientHelper.CheckAvailability(lobjPGRequest, lstrToken);
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("CheckAvailbility -" + ex.Message + Environment.NewLine + "Stack Trace-" + ex.StackTrace);
                return 0;
            }
        }
        public string RedeemPoints(float pfltAmount, int pintPoints, string pstrRelationReference, string pstrPassword, string pstrNarration, int pintLoyaltyTxnType, string pstrProgramCurrency, string pstrMerchectId)
        {
            string strRedeemMilesResponse = string.Empty;
            try
            {
                ProgramDefinition lobjProgramMaster = GetProgramMaster();
                string lstrToken = GetAuthTokenforWebAPI();
                PGRedeemRequest lobjPGRedeemRequest = new PGRedeemRequest
                {
                    RelationReference = pstrRelationReference,
                    Amount = Convert.ToDecimal(pfltAmount),
                    Points = pintPoints,
                    TransactionCurrency = pstrProgramCurrency,
                    LoyaltyTxnType = pintLoyaltyTxnType,
                    MerchantName = pstrNarration,
                    ProgramId = lobjProgramMaster.ProgramId,
                    MerchantId = pstrMerchectId,
                    RelationType = Convert.ToInt32(RelationType.LBMS)
                };
                APIClientHelper lobjAPIClientHelper = new APIClientHelper();
                strRedeemMilesResponse = lobjAPIClientHelper.RedeemPoints(lobjPGRedeemRequest, lstrToken);
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("NICModel RedeemPoints Exception: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
            }
            return strRedeemMilesResponse;
        }
        public bool RollBackTransaction(string pstrExternalReference, string pstrRelationReference, string pstrMerchantName)
        {
            bool lboolRollBackResponse = false;
            try
            {
                string lstrToken = GetAuthTokenforWebAPI();
                APIClientHelper lobjAPIClientHelper = new APIClientHelper();
                PGReversalRequest lobjPGReversalRequest = new PGReversalRequest();
                lobjPGReversalRequest.ExternalReference = pstrExternalReference;
                lobjPGReversalRequest.RelationReference = pstrRelationReference;
                lobjPGReversalRequest.MerchantName = pstrMerchantName;
                lboolRollBackResponse = lobjAPIClientHelper.ReversalPoints(lobjPGReversalRequest, lstrToken);
            }
            catch (Exception ex) { LoggingAdapter.WriteLog("NICModel RollBackTransaction Exception: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace); }
            return lboolRollBackResponse;
        }
        public bool ActivateAccount(SearchMember pobjSearchMember)
        {
            bool lboolResponse = false;
            try
            {
                string lstrToken = GetAuthTokenforWebAPI();
                lboolResponse = lobjAPIClientHelper.MemberActivation(pobjSearchMember, lstrToken);
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("Model ActivateAccount Ex-" + ex.InnerException + ex.StackTrace + ex.Message);
            }
            return lboolResponse;
        }

        public bool ActivateAccountByRelationReference(SearchMember pobjSearchMember)
        {
            bool lboolResponse = false;
            try
            {
                string lstrToken = GetAuthTokenforWebAPI();
                lboolResponse = lobjAPIClientHelper.MemberActivationByRelationReference(pobjSearchMember, lstrToken);
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("Model ActivateAccount Ex-" + ex.InnerException + ex.StackTrace + ex.Message);
            }
            return lboolResponse;
        }

        public int InsertMemberActivityWithSessionID(MemberActivitySession lobjMemberActivitySession)
        {
            int lintResponse = 0;
            try
            {
                string lstrToken = GetAuthTokenforWebAPI();
                lintResponse = lobjAPIClientHelper.InsertMemberActivityWithSessionID(lobjMemberActivitySession, lstrToken);
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("Model InsertMemberActivityWithSessionID Ex-" + ex.InnerException + ex.StackTrace + ex.Message);
            }
            return lintResponse;
        }
        public bool ResetPassword(ResetPassword pobjResetPassword)
        {
            ProgramDefinition lobjProgramDefinition = GetProgramMaster();
            if (lobjProgramDefinition != null)
            {
                pobjResetPassword.ProgramId = lobjProgramDefinition.ProgramId;
                pobjResetPassword.RelationType = RelationType.LBMS;
                bool lboolResponse = false;
                try
                {
                    string lstrToken = GetAuthTokenforWebAPI();
                    lboolResponse = lobjAPIClientHelper.ResetPassword(pobjResetPassword, lstrToken);
                }
                catch (Exception ex)
                {
                    LoggingAdapter.WriteLog("Model ResetPassword Ex-" + ex.InnerException + ex.StackTrace + ex.Message);
                }
                return lboolResponse;
            }
            else
            {
                LoggingAdapter.WriteLog("MODEL ResetPassword (Activation Facade) program Detail Invalid");
                return false;
            }
        }
        public ResetPassword GetDetailsFromHashKey(ResetPassword pobjResetPassword)
        {
            ResetPassword lobjResetPassword = null;
            try
            {
                string lstrToken = GetAuthTokenforWebAPI();
                lobjResetPassword = lobjAPIClientHelper.GetDetailsFromHashKey(pobjResetPassword, lstrToken);
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("Model GetDetailsFromHashKey Ex-" + ex.InnerException + ex.StackTrace + ex.Message);
            }
            return lobjResetPassword;
        }
        public bool ValidateResetToken(ResetPassword pobjResetPassword)
        {
            bool lboolResponse = false;
            try
            {
                string lstrToken = GetAuthTokenforWebAPI();
                lboolResponse = lobjAPIClientHelper.ValidateResetToken(pobjResetPassword, lstrToken);
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("Model ValidateResetToken Ex-" + ex.InnerException + ex.StackTrace + ex.Message);
            }
            return lboolResponse;
        }
        public bool UpdatePassword(ResetPassword pobjResetPassword, MemberRelation pobjMemberRelation)
        {
            bool lboolResponse = false;
            try
            {
                string lstrToken = GetAuthTokenforWebAPI();
                lboolResponse = lobjAPIClientHelper.UpdatePassword(pobjResetPassword, pobjMemberRelation, lstrToken);
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("Model UpdatePassword Ex-" + ex.InnerException + ex.StackTrace + ex.Message);
            }
            return lboolResponse;

        }
        public bool InsertMemberSecurityDetails(List<SecurityDetails> lobjListSecurityDetails)
        {
            bool lboolResponse = false;
            try
            {
                string lstrToken = GetAuthTokenforWebAPI();
                lboolResponse = lobjAPIClientHelper.InsertMemberSecurityDetails(lobjListSecurityDetails, lstrToken);
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("Model InsertMemberSecurityDetails Ex-" + ex.InnerException + ex.StackTrace + ex.Message);
            }
            return lboolResponse;
        }
        public List<SecurityDetails> GetMemberSecurityDetails(SecurityDetails pobjSecurityDetails)
        {
            List<SecurityDetails> lobjListSecurityDetail = null;
            try
            {
                string lstrToken = GetAuthTokenforWebAPI();
                lobjListSecurityDetail = lobjAPIClientHelper.GetMemberSecurityDetails(pobjSecurityDetails, lstrToken);
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("Model GetMemberSecurityDetails Ex-" + ex.InnerException + ex.StackTrace + ex.Message);
            }
            return lobjListSecurityDetail;
        }
        public bool GenerateReviewnConfirmOTP(OTPDetails pobjOTPDetails, MemberDetails pobjMemberDetails, string redemptionType)
        {
            try
            {
                //bool lboolResponse = false;
                bool status = false;
                ProgramDefinition lobjProgramDefinition = null;
                lobjProgramDefinition = GetProgramMaster();
                SystemParameter lobjSystemParameter = null;
                lobjSystemParameter = GetSystemParametres(lobjProgramDefinition.ProgramId);
                if (lobjProgramDefinition != null)
                {
                    try
                    {
                        OTPDetails lobjOTPDetails = new OTPDetails();
                        lobjOTPDetails.UniquerefID = pobjMemberDetails.MemberRelationsList.Find(l => l.RelationType.Equals(Core.Platform.Member.Entites.RelationType.LBMS)).RelationReference;
                        lobjOTPDetails.SourceAddress = HttpContext.Current.Request.UserHostAddress;
                        lobjOTPDetails.SourceCode = Core.Platform.OTP.ConfigurationConstants.SourceCode.Web;
                        lobjOTPDetails.ProgramId = lobjProgramDefinition.ProgramId;
                        lobjOTPDetails.RelationType = Convert.ToInt32(Core.Platform.Member.Entites.RelationType.LBMS);
                        lobjOTPDetails.OtpType = Convert.ToString(pobjOTPDetails.OtpEnumTypes);
                        lobjOTPDetails.OtpEnumTypes = pobjOTPDetails.OtpEnumTypes;
                        lobjOTPDetails.AddExpirationTimeInMinutes = Convert.ToString(lobjSystemParameter.OTPExpirationTime);

                        BookingIntegrationFacade lobjBookingIntegrationFacade = new BookingIntegrationFacade();
                        status = lobjBookingIntegrationFacade.GenerateReviewnConfirmOTP(lobjOTPDetails, pobjMemberDetails, redemptionType);
                    }
                    catch (Exception ex)
                    {
                        LoggingAdapter.WriteLog("Model GenerateReviewnConfirmOTP Ex-" + ex.InnerException + ex.StackTrace + ex.Message);
                    }
                    return status;
                }
                else
                {
                    LoggingAdapter.WriteLog("MODEL GenerateReviewnConfirmOTP (Activation Facade) program Detail Invalid");
                    return false;
                }
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("GenerateReviewnConfirmOTP -" + ex.Message + Environment.NewLine + "Stack Trace-" + ex.StackTrace);
                return false;
            }
        }
        public bool CheckWhetherOTPExists(OTPDetails pobjOTPDetails)
        {
            try
            {
                pobjOTPDetails.DestinationAddress = HttpContext.Current.Request.UserHostAddress;
                pobjOTPDetails.Destination = Core.Platform.OTP.ConfigurationConstants.SourceCode.Web.ToString();
                bool lboolResponse = false;
                try
                {
                    string lstrToken = GetAuthTokenforWebAPI();
                    lboolResponse = lobjAPIClientHelper.CheckWhetherOTPExists(pobjOTPDetails, lstrToken);
                }
                catch (Exception ex)
                {
                    LoggingAdapter.WriteLog("Model CheckWhetherOTPExists Ex-" + ex.InnerException + ex.StackTrace + ex.Message);
                }
                return lboolResponse;
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("BookForCar -" + ex.Message + Environment.NewLine + "Stack Trace-" + ex.StackTrace);
                return false;
            }
        }
        public bool CheckRedemptionOTP(OTPDetails pobjOTPDetails)
        {
            try
            {
                pobjOTPDetails.DestinationAddress = HttpContext.Current.Request.UserHostAddress;
                pobjOTPDetails.Destination = Core.Platform.OTP.ConfigurationConstants.SourceCode.Web.ToString();
                bool lboolResponse = false;
                try
                {
                    string lstrToken = GetAuthTokenforWebAPI();
                    lboolResponse = lobjAPIClientHelper.CheckRedemptionOTP(pobjOTPDetails, lstrToken);
                }
                catch (Exception ex)
                {
                    LoggingAdapter.WriteLog("Model CheckWhetherOTPExists Ex-" + ex.InnerException + ex.StackTrace + ex.Message);
                }
                return lboolResponse;
            }

            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("BookForCar -" + ex.Message + Environment.NewLine + "Stack Trace-" + ex.StackTrace);
                return false;
            }
        }
        public void SendEmail(List<string> pstrEmailparameter, MemberDetails pobjMemberDetails, string pstrTemplateCode)
        {
            try
            {
                EmailDetails lobjEmailDetail = new EmailDetails();
                List<string> lstAttachment = new List<string>();
                string lstrToken = GetAuthTokenforWebAPI();
                List<Attachments> lstAttachments = new List<Attachments>();
                APIClientHelper lobjcehelper = new APIClientHelper();
                lobjEmailDetail.TemplateCode = pstrTemplateCode;
                lobjEmailDetail.ListParameter = pstrEmailparameter;
                lobjEmailDetail.ProgramId = Convert.ToString(pobjMemberDetails.ProgramId);
                lobjEmailDetail.MemberId = Convert.ToString(pobjMemberDetails.MemberRelationsList.Find(lobj => lobj.RelationType.Equals(RelationType.LBMS)).RelationReference);
                lobjEmailDetail.AttachmentList = lstAttachment;
                lobjEmailDetail.To = pobjMemberDetails.Email;
                lobjcehelper.InsertEmailDetails(lobjEmailDetail, lstAttachments, lstrToken);
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("Model SendEmail Ex-" + ex.InnerException + ex.StackTrace + ex.Message);
            }
        }
        public bool SendEmails(string Parameters, MemberDetails pobjMemberDetails)
        {
            bool lblnEmailSend = false;
            try
            {
                NICEmailDetailsResponse lobjEmailResponse = new NICEmailDetailsResponse();
                APIClientHelper lobjcehelper = new APIClientHelper();
                if (pobjMemberDetails.Email != string.Empty)
                {
                    string lstrToken = GetAuthTokenforWebAPI();
                    lobjEmailResponse = lobjcehelper.NICEmailDetails(Parameters, lstrToken);
                    if (lobjEmailResponse != null)
                    {
                        if (lobjEmailResponse.results.IsSucessful)
                        {
                            lblnEmailSend = true;
                        }
                        else
                        {
                            LoggingAdapter.WriteLog("Send Communication UnSuccessful :" + lobjEmailResponse.results.ExceptionMessage);
                            lblnEmailSend = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("Send Communication error :" + ex.Message);
            }
            return lblnEmailSend;
        }

        public bool SendOTPEmailAndSMS(MemberDetails lobjMemberDetails, string EventName, OTPDetails pobjOTPDetails, string redemptionType)
        {
            dynamic dynamicCls = new System.Dynamic.ExpandoObject();
            string lsrtTemplateLangCode = "";
            if (lobjMemberDetails.PreferredLanguage == "EN")
            {
                dynamicCls.event_name = EventName;
            }
            if (lobjMemberDetails.PreferredLanguage.ToUpper() != "EN")
            {
                lsrtTemplateLangCode = lobjMemberDetails.PreferredLanguage.ToUpper();
                dynamicCls.event_name = lsrtTemplateLangCode + EventName;
            }
            dynamicCls.relation_reference = Convert.ToString(lobjMemberDetails.MemberRelationsList.Find(lobj => lobj.RelationType.Equals(RelationType.LBMS)).RelationReference);
            dynamicCls.program_id = Convert.ToInt32(lobjMemberDetails.ProgramId); ;
            dynamicCls.to_email = lobjMemberDetails.Email;
            dynamicCls.full_name = lobjMemberDetails.FullName;
            dynamicCls.to_mobile = lobjMemberDetails.MobileNumber;
            dynamicCls.redemption_type = redemptionType;


            ProgramDefinition lobjProgramDefinition = null;
            lobjProgramDefinition = GetProgramMaster();
            SystemParameter lobjSystemParameter = null;
            lobjSystemParameter = GetSystemParametres(lobjProgramDefinition.ProgramId);

            OTPDetails lobjOTPDetails = new OTPDetails();
            lobjOTPDetails.UniquerefID = lobjMemberDetails.MemberRelationsList.Find(l => l.RelationType.Equals(Core.Platform.Member.Entites.RelationType.LBMS)).RelationReference;
            lobjOTPDetails.SourceAddress = HttpContext.Current.Request.UserHostAddress;
            lobjOTPDetails.SourceCode = Core.Platform.OTP.ConfigurationConstants.SourceCode.Web;
            lobjOTPDetails.ProgramId = lobjMemberDetails.ProgramId;
            lobjOTPDetails.RelationType = Convert.ToInt32(Core.Platform.Member.Entites.RelationType.LBMS);
            lobjOTPDetails.OtpType = Convert.ToString(pobjOTPDetails.OtpEnumTypes);
            lobjOTPDetails.OtpEnumTypes = pobjOTPDetails.OtpEnumTypes;
            lobjOTPDetails.AddExpirationTimeInMinutes = Convert.ToString(lobjSystemParameter.OTPExpirationTime);

            BookingIntegrationFacade lobjBookingIntegrationFacade = new BookingIntegrationFacade();
            dynamicCls.otp = lobjBookingIntegrationFacade.GenerateOTPDetails(lobjOTPDetails).OTP;

            Dictionary<string, dynamic> lobjDictionary = new Dictionary<string, dynamic>();
            IDictionary<string, object> dict = (IDictionary<string, object>)dynamicCls;
            foreach (var key in dict)
            {
                lobjDictionary.Add(key.Key, key.Value);
            }
            string jsonParameters = JsonConvert.SerializeObject(lobjDictionary);
            bool lblnEmailSend = false;
            try
            {
                NICEmailDetailsResponse lobjEmailResponse = new NICEmailDetailsResponse();
                APIClientHelper lobjcehelper = new APIClientHelper();
                if (lobjMemberDetails.Email != string.Empty)
                {
                    string lstrToken = GetAuthTokenforWebAPI();
                    lobjEmailResponse = lobjcehelper.NICEmailDetails(jsonParameters, lstrToken);
                    if (lobjEmailResponse != null)
                    {
                        if (lobjEmailResponse.results.IsSucessful)
                        {
                            lblnEmailSend = true;
                        }
                        else
                        {
                            LoggingAdapter.WriteLog("Send Communication UnSuccessful :" + lobjEmailResponse.results.ExceptionMessage);
                            lblnEmailSend = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("Send Communication error :" + ex.Message);
            }
            return lblnEmailSend;
        }
        public void SendSMS(List<string> pstrSMSparameter, MemberDetails pobjMemberDetails, string pstrTemplateCode)
        {
            try
            {
                SmsDetails lobjSMSDetail = new SmsDetails();
                string lstrToken = GetAuthTokenforWebAPI();
                List<Attachments> lstAttachments = new List<Attachments>();
                APIClientHelper lobjcehelper = new APIClientHelper();
                lobjSMSDetail.TemplateCode = pstrTemplateCode;
                lobjSMSDetail.ListParameter = pstrSMSparameter;
                lobjSMSDetail.ProgramId = Convert.ToString(pobjMemberDetails.ProgramId);
                lobjSMSDetail.MemberId = Convert.ToString(pobjMemberDetails.MemberRelationsList.Find(lobj => lobj.RelationType.Equals(RelationType.LBMS)).RelationReference);
                lobjSMSDetail.ReceiverMobile = pobjMemberDetails.MobileNumber;
                lobjcehelper.InsertSmsDetails(lobjSMSDetail, lstrToken);
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("Model SendSMS Ex-" + ex.InnerException + ex.StackTrace + ex.Message);
            }
        }
        public bool InsertRedemptionAuditTrail(AuditTrailForRedemption pobjAuditTrailForRedemption)
        {
            try
            {
                bool lboolResponse = false;
                try
                {
                    string lstrToken = GetAuthTokenforWebAPI();
                    lboolResponse = lobjAPIClientHelper.InsertRedemptionAuditTrail(pobjAuditTrailForRedemption, lstrToken);
                }
                catch (Exception ex)
                {
                    LoggingAdapter.WriteLog("Model InsertRedemptionAuditTrail Ex-" + ex.InnerException + ex.StackTrace + ex.Message);
                }
                return lboolResponse;
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("InsertRedemptionAuditTrail -" + ex.Message + Environment.NewLine + "Stack Trace-" + ex.StackTrace);
                return false;
            }
        }
        #endregion

        #region Experience
        public ProductListResponse GetExperienceProductList(int pintPage, int pintLimit, string pstrSort, string pstrPlaceName, string pstrGuidePrice)
        {
            ProductListResponse lstrResponse = null;
            try
            {
                HolibobHelper lobjHelper = new HolibobHelper();
                lstrResponse = lobjHelper.GetProductList(pintPage, pintLimit, pstrSort, pstrPlaceName, pstrGuidePrice);
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("Model GetExperienceProductList Ex: " + ex.Message + Environment.NewLine + ex.StackTrace + Environment.NewLine + ex.InnerException);
            }
            return lstrResponse;
        }
        public ProductInfoResponse GetExperienceProductInfo(string pstrProductId)
        {
            ProductInfoResponse lobjResponse = null;
            try
            {
                HolibobHelper lobjHelper = new HolibobHelper();
                lobjResponse = lobjHelper.GetProductInfo(pstrProductId);
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("Model GetExperienceProductList Ex: " + ex.Message + Environment.NewLine + ex.StackTrace + Environment.NewLine + ex.InnerException);
            }
            return lobjResponse;
        }
        public ProductStatusResponse GetExperienceProductStatus(string pstrProductId, string pstrStartDate, string pstrEndDate, string pstrAvailabilityType)
        {
            ProductStatusResponse lobjResponse = null;
            try
            {
                HolibobHelper lobjHelper = new HolibobHelper();
                lobjResponse = lobjHelper.GetProductStatus(pstrProductId, pstrStartDate, pstrEndDate, pstrAvailabilityType);
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("Model GetExperienceProductList Ex: " + ex.Message + Environment.NewLine + ex.StackTrace + Environment.NewLine + ex.InnerException);
            }
            return lobjResponse;
        }
        public FetchAvailabilityResponse FetchAvailability(string pstrAvailabilityId)
        {
            FetchAvailabilityResponse lobjResponse = null;
            try
            {
                HolibobHelper lobjHelper = new HolibobHelper();
                lobjResponse = lobjHelper.FetchAvailability(pstrAvailabilityId);
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("Model FetchAvailability Ex: " + ex.Message + Environment.NewLine + ex.StackTrace + Environment.NewLine + ex.InnerException);
            }
            return lobjResponse;
        }
        public FetchAvailabilityResponse FetchAvailabilityWithOptionList(string pstrAvailabilityId, string pstrInputId, string pstrInputValue)
        {
            FetchAvailabilityResponse lobjResponse = null;
            try
            {
                HolibobHelper lobjHelper = new HolibobHelper();
                lobjResponse = lobjHelper.FetchAvailabilityWithOptionList(pstrAvailabilityId, pstrInputId, pstrInputValue);
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("Model FetchAvailabilityWithOptionList Ex: " + ex.Message + Environment.NewLine + ex.StackTrace + Environment.NewLine + ex.InnerException);
            }
            return lobjResponse;
        }
        public FetchAvailabilityResponse FetchAvailabilityWithOptionList(string pstrAvailabilityId, List<FetchAvailabilityWithPricingCategoryOptionList> plstobjFetchAvailabilityWithPricingCategoryOptionList)
        {
            FetchAvailabilityResponse lobjResponse = null;
            try
            {
                HolibobHelper lobjHelper = new HolibobHelper();
                lobjResponse = lobjHelper.FetchAvailabilityWithOptionList(pstrAvailabilityId, plstobjFetchAvailabilityWithPricingCategoryOptionList);
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("Model FetchAvailabilityWithOptionList Ex: " + ex.Message + Environment.NewLine + ex.StackTrace + Environment.NewLine + ex.InnerException);
            }
            return lobjResponse;
        }
        public CreateBookingResponse CreateBooking()
        {
            CreateBookingResponse lobjResponse = null;
            try
            {
                HolibobHelper lobjHelper = new HolibobHelper();
                lobjResponse = lobjHelper.CreateBooking();
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("Model CreateBooking Ex: " + ex.Message + Environment.NewLine + ex.StackTrace + Environment.NewLine + ex.InnerException);
            }
            return lobjResponse;
        }
        public AddAvailabilityToBookingResponse AddAvailabilityToBooking(string pstrAvailabilityId, string pstrBookId)
        {
            AddAvailabilityToBookingResponse lobjResponse = null;
            try
            {
                HolibobHelper lobjHelper = new HolibobHelper();
                lobjResponse = lobjHelper.AddAvailabilityToBooking(pstrAvailabilityId, pstrBookId);
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("Model CreateBooking Ex: " + ex.Message + Environment.NewLine + ex.StackTrace + Environment.NewLine + ex.InnerException);
            }
            return lobjResponse;
        }
        public OrderStatusResponse GetOrderStatus(string pstrBookId, string pstrLeadPassengerName)
        {
            OrderStatusResponse lobjResponse = null;
            try
            {
                HolibobHelper lobjHelper = new HolibobHelper();
                lobjResponse = lobjHelper.GetOrderStatus(pstrBookId, pstrLeadPassengerName);
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("Model GetOrderStatus Ex: " + ex.Message + Environment.NewLine + ex.StackTrace + Environment.NewLine + ex.InnerException);
            }
            return lobjResponse;
        }
        public OrderStatusResponse GetOrderStatusByBookingId(string pstrBookId)
        {
            OrderStatusResponse lobjResponse = null;
            try
            {
                HolibobHelper lobjHelper = new HolibobHelper();
                lobjResponse = lobjHelper.GetOrderStatusByBookingId(pstrBookId);
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("Model GetOrderStatusByBookingId Ex: " + ex.Message + Environment.NewLine + ex.StackTrace + Environment.NewLine + ex.InnerException);
            }
            return lobjResponse;
        }
        public OrderStatusResponse SubmitBookingAnswer(string pstrBookId, List<ExperienceBookingAnswerList> plstobjAnswerList)
        {
            OrderStatusResponse lobjResponse = null;
            try
            {
                HolibobHelper lobjHelper = new HolibobHelper();
                lobjResponse = lobjHelper.SubmitBookingAnswer(pstrBookId, plstobjAnswerList);
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("Model SubmitBookingAnswer Ex: " + ex.Message + Environment.NewLine + ex.StackTrace + Environment.NewLine + ex.InnerException);
            }
            return lobjResponse;
        }
        public int ConvertToPoints(float pstrAmount, string pstrCurrency, int pintProgramId, string pstrRedemtionCode)
        {
            int lintResponse = 0;
            try
            {
                float pfltPointrate = 0.0f;
                pfltPointrate = GetProgramRedemptionRate(GetDefaultCurrency(), pstrRedemtionCode, pintProgramId);

                lintResponse = (int)Math.Ceiling(pstrAmount / pfltPointrate);
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("ExperienceProductList ConvertToPoints Exception: " + ex.Message + Environment.NewLine + ex.StackTrace);
            }
            return lintResponse;
        }

        public int ConvertToPointsOrCurrency(float pstrAmount, string pstrCurrency, int pintProgramId, out string pstrGrossFormattedText)
        {
            int lintResponse = 0;
            pstrGrossFormattedText = string.Empty;
            try
            {
                bool blnHolibobShowCurrency = Convert.ToBoolean(ConfigurationManager.AppSettings["HolibobShowCurrency"]);
                if (!blnHolibobShowCurrency)
                {
                    float pfltPointrate = 0.0f;
                    pfltPointrate = GetProgramRedemptionRate(GetDefaultCurrency(), "EXPERIENCE", pintProgramId);
                    lintResponse = (int)Math.Ceiling(pstrAmount / pfltPointrate);
                    pstrGrossFormattedText = string.Format("{0} Points", LongToThousandSeperated(Convert.ToInt64(lintResponse)));
                }
                else
                {
                    string lstrHolibobCurrency = Convert.ToString(ConfigurationManager.AppSettings["HolibobCurrency"]);
                    lintResponse = (int)Math.Round(pstrAmount);
                    pstrGrossFormattedText = string.Format("{0} {1}", LongToThousandSeperated(Convert.ToInt64(lintResponse)), lstrHolibobCurrency);
                }
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("ExperienceProductList ConvertToPointsOrCurrency Exception: " + ex.Message + Environment.NewLine + ex.StackTrace);
            }
            return lintResponse;
        }

        public PlaceOrderResponse PlaceOrder(string pstrBookId)
        {
            PlaceOrderResponse lobjResponse = null;
            try
            {
                HolibobHelper lobjHelper = new HolibobHelper();
                lobjResponse = lobjHelper.PlaceOrder(pstrBookId);
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("Model PlaceOrder Ex: " + ex.Message + Environment.NewLine + ex.StackTrace + Environment.NewLine + ex.InnerException);
            }
            return lobjResponse;
        }
        public ProductSearchResponse GetSearchList(string pstrSearchText)
        {
            ProductSearchResponse lobjResponse = null;
            try
            {
                HolibobHelper lobjHelper = new HolibobHelper();
                lobjResponse = lobjHelper.GetSearchList(pstrSearchText);
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("Model GetSearchList Ex: " + ex.Message + Environment.NewLine + ex.StackTrace + Environment.NewLine + ex.InnerException);
            }
            return lobjResponse;
        }
        public SearchProductListResponse GetExperienceProductListByPlaceId(string pstrPlaceId, bool pblnIsPrivate, bool pblnIsNew, string pstrIsRecommended,
            string pstrGuidePrice, string pstrSearch, List<string> plstCategoryIds, List<string> plstAttributeIds)
        {
            SearchProductListResponse lobjResponse = null;
            try
            {
                HolibobHelper lobjHelper = new HolibobHelper();
                lobjResponse = lobjHelper.GetExperienceProductListByPlaceId(pstrPlaceId, pblnIsPrivate, pblnIsNew, pstrIsRecommended, pstrGuidePrice, pstrSearch, plstCategoryIds, plstAttributeIds);
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("Model GetExperienceProductListByPlaceId Ex: " + ex.Message + Environment.NewLine + ex.StackTrace + Environment.NewLine + ex.InnerException);
            }
            return lobjResponse;
        }
        public string LongToThousandSeperated(long pfltValue)
        {
            NumberFormatInfo nfo = new CultureInfo("en-US", false).NumberFormat;
            string pstrString = pfltValue.ToString("N", nfo);
            string[] lstrArray = pstrString.Split('.');
            return lstrArray[0];
        }
        #endregion

        public List<TransactionDetails> GetAllTransactions(string lstrRelationReference)
        {
            LoyaltyHelper lobjHelper = new LoyaltyHelper();
            SearchTransactions lobjSearchTransactions = new SearchTransactions();
            List<TransactionDetails> lobjlstTransactionDetails = new List<TransactionDetails>();
            ProgramDefinition lobjProgramDefinition = GetProgramMaster();
            if (lobjProgramDefinition != null)
            {
                lobjSearchTransactions.ProgramId = lobjProgramDefinition.ProgramId;
                lobjSearchTransactions.RelationReference = lstrRelationReference;
                lobjSearchTransactions.RelationType = Convert.ToInt32(RelationType.LBMS);
                lobjSearchTransactions.MinimumRange = 0;
                lobjSearchTransactions.MaximumRange = 1000000;
                lobjlstTransactionDetails = GetMemberTransactionSummary(lobjSearchTransactions);
            }
            return lobjlstTransactionDetails;
        }
        public string StringToThousandSeperated(string pfltValue)
        {
            decimal value = Convert.ToDecimal(pfltValue);

            string[] lstrArray = null;
            string pstrString = string.Empty;
            NumberFormatInfo nfo = new CultureInfo("en-US", false).NumberFormat;
            pstrString = value.ToString("N", nfo);
            lstrArray = pstrString.Split('.');
            return lstrArray[0];

        }

        #region Khalti
        public SearchResponseForDomestic MapSearchResponseForDomesticFlights(SearchRequestForDomestic pobjSearchRequest)
        {
            SearchResponseForDomestic lobjSearchResponse = new SearchResponseForDomestic();
            try
            {
                APIClientHelper lobjAPIClientHelper = new APIClientHelper();
                lobjSearchResponse = lobjAPIClientHelper.MapSearchResponseForDomesticFlights(pobjSearchRequest);
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("MapSearchResponseForDomesticFlights -" + ex.Message + Environment.NewLine + "Stack Trace-" + ex.StackTrace);
            }
            return lobjSearchResponse;
        }

        public CreateDomesticItineraryResponse CreateItineraryForDomesticFlights(CreateDomesticItineraryRequest pobjSearchRequest)
        {
            CreateDomesticItineraryResponse lobjSearchResponse = new CreateDomesticItineraryResponse();
            try
            {
                APIClientHelper lobjAPIClientHelper = new APIClientHelper();
                lobjSearchResponse = lobjAPIClientHelper.CreateItineraryForDomesticFlights(pobjSearchRequest);
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("CreateItineraryForDomesticFlights -" + ex.Message + Environment.NewLine + "Stack Trace-" + ex.StackTrace);
            }
            return lobjSearchResponse;
        }

        public CreateDomesticBookingResponse GetDomesticBookingResponse(BookingDetailsRequest pobjSearchRequest)
        {
            CreateDomesticBookingResponse lobjSearchResponse = new CreateDomesticBookingResponse();
            try
            {
                APIClientHelper lobjAPIClientHelper = new APIClientHelper();
                lobjSearchResponse = lobjAPIClientHelper.CreateBookingForDomesticFlights(pobjSearchRequest);
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("GetDomesticBookingResponse -" + ex.Message + Environment.NewLine + "Stack Trace-" + ex.StackTrace);
            }
            return lobjSearchResponse;
        }

        public BookingStatusResponseForDomestic BookForKhaltiFlight(BookingStatusRequestForDomestic pobjSearchRequest)
        {
            BookingStatusResponseForDomestic lobjSearchResponse = new BookingStatusResponseForDomestic();
            try
            {
                APIClientHelper lobjAPIClientHelper = new APIClientHelper();
                lobjSearchResponse = lobjAPIClientHelper.BookingStatusForDomesticFlights(pobjSearchRequest);
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("BookForKhaltiFlight -" + ex.Message + Environment.NewLine + "Stack Trace-" + ex.StackTrace);
            }
            return lobjSearchResponse;
        }

        public TicketDownloadResponse TicketDownload(TicketDownloadRequest pobjSearchRequest)
        {
            TicketDownloadResponse lobjSearchResponse = new TicketDownloadResponse();
            try
            {
                APIClientHelper lobjAPIClientHelper = new APIClientHelper();
                lobjSearchResponse = lobjAPIClientHelper.TicketDownloadForDomesticFlights(pobjSearchRequest);
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("TicketDownload -" + ex.Message + Environment.NewLine + "Stack Trace-" + ex.StackTrace);
            }
            return lobjSearchResponse;
        }

        public List<DomesticItineraryDetails> GetDomesticFlightBookingDetails(string pstrMemberId)
        {
            List<DomesticItineraryDetails> lobjSearchResponse = new List<DomesticItineraryDetails>();
            try
            {
                APIClientHelper lobjAPIClientHelper = new APIClientHelper();
                lobjSearchResponse = lobjAPIClientHelper.GetDomesticFlightBookingDetails(pstrMemberId);
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("GetDomesticFlightBookingDetails -" + ex.Message + Environment.NewLine + "Stack Trace-" + ex.StackTrace);
            }
            return lobjSearchResponse;
        }

        public List<InsuranceBookingResponse> GetBookedInsuranceListForMember(string pstrMemberId)
        {
            List<InsuranceBookingResponse> lobjSearchResponse = new List<InsuranceBookingResponse>();
            try
            {
                APIClientHelper lobjAPIClientHelper = new APIClientHelper();
                lobjSearchResponse = lobjAPIClientHelper.GetBookedInsuranceListForMember(pstrMemberId);
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("GetBookedInsuranceListForMember -" + ex.Message + Environment.NewLine + "Stack Trace-" + ex.StackTrace);
            }
            return lobjSearchResponse;
        }

        public List<ISPBookingResponse> GetBookedISPListForMember(string pstrMemberId)
        {
            List<ISPBookingResponse> lobjSearchResponse = new List<ISPBookingResponse>();
            try
            {
                APIClientHelper lobjAPIClientHelper = new APIClientHelper();
                lobjSearchResponse = lobjAPIClientHelper.GetBookedISPListForMember(pstrMemberId);
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("GetBookedISPListForMember -" + ex.Message + Environment.NewLine + "Stack Trace-" + ex.StackTrace);
            }
            return lobjSearchResponse;
        }

        public InsuranceServiceProvidersResponse SearchInsuranceProducts(string UserName, string PageName)
        {
            InsuranceServiceProvidersResponse lobjSearchResult = new InsuranceServiceProvidersResponse();
            try
            {
                string Token = string.Empty;
                if (PageName.Contains("Internet"))
                {

                    Token = Convert.ToString(ConfigurationManager.AppSettings["KhaltiISPToken"]);
                    if (HttpContext.Current.Application["SearchISPProducts"] != null)
                    {
                        lobjSearchResult = HttpContext.Current.Application["SearchISPProducts"] as InsuranceServiceProvidersResponse;
                    }
                    else
                    {
                        APIClientHelper lobjAPIClientHelper = new APIClientHelper();
                        lobjSearchResult = lobjAPIClientHelper.GetInsuranceServiceProviders(Token, UserName, PageName);
                        HttpContext.Current.Application["SearchISPProducts"] = lobjSearchResult;
                    }
                }
                else
                {
                    Token = Convert.ToString(ConfigurationManager.AppSettings["KhaltiInsuranceToken"]);
                    if (HttpContext.Current.Application["SearchInsuranceProducts"] != null)
                    {
                        lobjSearchResult = HttpContext.Current.Application["SearchInsuranceProducts"] as InsuranceServiceProvidersResponse;
                    }
                    else
                    {
                        APIClientHelper lobjAPIClientHelper = new APIClientHelper();
                        lobjSearchResult = lobjAPIClientHelper.GetInsuranceServiceProviders(Token, UserName, PageName);
                        HttpContext.Current.Application["SearchInsuranceProducts"] = lobjSearchResult;
                    }
                }


            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("ShopModel SearchInsuranceProducts Exception: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
            }
            return lobjSearchResult;
        }

        public InsuranceRequiredDetailsResponse GetRequiredDetails(int code, string PageName)
        {
            InsuranceRequiredDetailsResponse lobjSearchResult = new InsuranceRequiredDetailsResponse();
            try
            {
                string Token = string.Empty;
                string UserName = string.Empty;
                if (PageName.Contains("ISPListDetails"))
                {
                    Token = Convert.ToString(ConfigurationManager.AppSettings["KhaltiISPToken"]);
                    UserName = Convert.ToString(ConfigurationManager.AppSettings["KhaltiISPUserName"]);
                }
                else
                {
                    Token = Convert.ToString(ConfigurationManager.AppSettings["KhaltiInsuranceToken"]);
                    UserName = Convert.ToString(ConfigurationManager.AppSettings["KhaltiInsuranceUserName"]);
                }

                APIClientHelper lobjAPIClientHelper = new APIClientHelper();
                lobjSearchResult = lobjAPIClientHelper.GetRequiredDetails(code, UserName, Token, PageName);
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("ShopModel GetRequiredDetails Exception: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
            }
            return lobjSearchResult;
        }

        public InsuranceUserDetailsResponse GetUserDetails(int code, string Policyno, string DOB, string MembershipReference)
        {
            InsuranceUserDetailsResponse lobjSearchResult = new InsuranceUserDetailsResponse();
            try
            {
                string Token = Convert.ToString(ConfigurationManager.AppSettings["KhaltiInsuranceToken"]);
                string UserName = Convert.ToString(ConfigurationManager.AppSettings["KhaltiInsuranceUserName"]);
                APIClientHelper lobjAPIClientHelper = new APIClientHelper();
                lobjSearchResult = lobjAPIClientHelper.GetUserDetails(code, Policyno, DOB, UserName, Token, MembershipReference);
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("ShopModel GetUserDetails Exception: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
            }
            return lobjSearchResult;
        }

        public InsurancePaymentRequestResponse InsurancePaymentRequest(string code, decimal Amount, int SessionId, string PolicyNo, string TransactionId, string ServiceName, string MembershipReference, string CustomerName)
        {
            InsurancePaymentRequestResponse lobjSearchResult = new InsurancePaymentRequestResponse();
            try
            {
                string Token = Convert.ToString(ConfigurationManager.AppSettings["KhaltiInsuranceToken"]);
                string UserName = Convert.ToString(ConfigurationManager.AppSettings["KhaltiInsuranceUserName"]);
                APIClientHelper lobjAPIClientHelper = new APIClientHelper();
                lobjSearchResult = lobjAPIClientHelper.InsurancePaymentRequest(code, Amount, SessionId, PolicyNo, TransactionId, UserName, Token, ServiceName, MembershipReference, CustomerName);

            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("ShopModel InsurancePaymentRequest Exception: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
            }
            return lobjSearchResult;
        }

        public ISPUserDetailsResponse GetISPUserDetails(int code, string UserId, string ServiceName, string MembershipReference)
        {
            ISPUserDetailsResponse lobjSearchResult = new ISPUserDetailsResponse();
            try
            {
                string Token = Convert.ToString(ConfigurationManager.AppSettings["KhaltiISPToken"]);
                string UserName = Convert.ToString(ConfigurationManager.AppSettings["KhaltiISPUserName"]);
                APIClientHelper lobjAPIClientHelper = new APIClientHelper();
                lobjSearchResult = lobjAPIClientHelper.GetISPUserDetails(code, UserId, ServiceName, UserName, Token, MembershipReference);
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("ShopModel GetUserDetails Exception: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
            }
            return lobjSearchResult;
        }
        public GetDiscountResponse GetDiscount(GetDiscountRequest pobjSearchRequest)
        {
            GetDiscountResponse lobjSearchResponse = new GetDiscountResponse();
            try
            {
                APIClientHelper lobjAPIClientHelper = new APIClientHelper();
                lobjSearchResponse = lobjAPIClientHelper.GetDiscount(pobjSearchRequest);
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("GetDiscount -" + ex.Message + Environment.NewLine + "Stack Trace-" + ex.StackTrace);
            }
            return lobjSearchResponse;
        }
        public ISPPaymentResponse ISPPaymentRequest(ISPPaymentRequest pobjSearchRequest)
        {
            ISPPaymentResponse lobjSearchResponse = new ISPPaymentResponse();
            try
            {
                APIClientHelper lobjAPIClientHelper = new APIClientHelper();
                lobjSearchResponse = lobjAPIClientHelper.ISPPaymentRequest(pobjSearchRequest);
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("ISPPaymentRequest -" + ex.Message + Environment.NewLine + "Stack Trace-" + ex.StackTrace);
            }
            return lobjSearchResponse;
        }
        #endregion
        #region Stripe psyment Gateway
        public bool InsertManualTransactionDetails(TransactionDetails pobjAuditTrailForRedemption)
        {
            try
            {
                bool lboolResponse = false;
                try
                {
                    string lstrToken = GetAuthTokenforWebAPI();
                    lboolResponse = lobjAPIClientHelper.InsertManualTransactionDetails(pobjAuditTrailForRedemption, lstrToken);
                }
                catch (Exception ex)
                {
                    LoggingAdapter.WriteLog("Model InsertManualTransactionDetails Ex-" + ex.InnerException + ex.StackTrace + ex.Message);
                }
                return lboolResponse;
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("InsertManualTransactionDetails -" + ex.Message + Environment.NewLine + "Stack Trace-" + ex.StackTrace);
                return false;
            }
        }
        #endregion
        // Only IBE Flight Hotel Booking without Redemption 
        public HotelBookingResponse BookForHotel(MemberDetails pobjMemberDetails, Hotel pobjHotel, HotelSearchRequest pobjSearchRequest, Customer pobjCustomer, CB.IBE.Platform.Masters.Entities.BookingPaymentDetails pobjBookingPaymentDetails)
        {
            try
            {
                List<int> lobjListOfAdult = new List<int>();
                string[] arrayAdultPerRoom = pobjSearchRequest.SearchRequest.AdultPerRoom.Split(',');
                for (int i = 0; i < arrayAdultPerRoom.Count(); i++)
                {
                    lobjListOfAdult.Add(Convert.ToInt32(arrayAdultPerRoom[i]));
                }
                List<int> lobjListOfChild = new List<int>();
                string[] arrayChildPerRoom = pobjSearchRequest.SearchRequest.ChildrenPerRoom.Split(',');
                for (int i = 0; i < arrayChildPerRoom.Count(); i++)
                {
                    lobjListOfChild.Add(Convert.ToInt32(arrayChildPerRoom[i]));
                }
                HotelBookingRequest lobjBookingRequest = new HotelBookingRequest();
                lobjBookingRequest.BookRequest.customer = pobjCustomer;
                lobjBookingRequest.BookRequest.checkindate = pobjSearchRequest.SearchRequest.CheckInDate;
                lobjBookingRequest.BookRequest.checkoutdate = pobjSearchRequest.SearchRequest.CheckOutDate;
                lobjBookingRequest.BookRequest.numberofrooms = pobjSearchRequest.SearchRequest.NoOfRooms;
                lobjBookingRequest.BookRequest.nri = false;
                lobjBookingRequest.BookRequest.adultsperroom = lobjListOfAdult.ToArray();
                lobjBookingRequest.BookRequest.childrenperroom = lobjListOfChild.ToArray();
                lobjBookingRequest.BookRequest.bookingcode = pobjHotel.roomrates.RoomRate[0].bookingcode;
                lobjBookingRequest.BookRequest.roomtypecode = pobjHotel.roomrates.RoomRate[0].roomtype.roomtypecode;
                lobjBookingRequest.BookRequest.TotalPoints = pobjHotel.roomrates.RoomRate[0].TotalPoints;
                lobjBookingRequest.BookRequest.customeripaddress = pobjSearchRequest.SearchRequest.IpAddress;
                lobjBookingRequest.BookRequest.hotelid = pobjHotel.hotelid;
                lobjBookingRequest.BookRequest.bookingamount = Convert.ToDouble(pobjHotel.roomrates.RoomRate[0].TotalDefaultAmount);
                lobjBookingRequest.BookRequest.totalBaseFare = Convert.ToDouble(pobjHotel.roomrates.RoomRate[0].TotalBaseAmount);
                lobjBookingRequest.BookRequest.totalDefaulFare = Convert.ToDouble(pobjHotel.roomrates.RoomRate[0].TotalDefaultAmount);
                lobjBookingRequest.BookingPaymentDetails = pobjBookingPaymentDetails;
                pobjSearchRequest.SearchRequest.MembershipReference = pobjMemberDetails.MemberRelationsList[0].RelationReference;

                BookingIntegrationFacade lobjBookingIntegrationFacade = new BookingIntegrationFacade();
                HotelBookingResponse lobjHotelBookingResponse = new HotelBookingResponse();
                lobjHotelBookingResponse = lobjBookingIntegrationFacade.BookForHotel(lobjBookingRequest, pobjHotel, pobjSearchRequest, pobjMemberDetails, pobjCustomer, pobjBookingPaymentDetails.PointsTxnRefererence);

                return lobjHotelBookingResponse;

            }
            catch (ApplicationException ex)
            {
                throw new ApplicationException(ex.Message);
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("Model BookForHotel Ex-:" + ex.Message + Environment.NewLine + "Stack Trace-" + ex.StackTrace);
                throw new ApplicationException(ex.Message);
            }
        }

        public BookingResponse BookForFlight(BookingRequest pobjBookingRequest, MemberDetails pobjMemberDetails)
        {
            try
            {
                BookingIntegrationFacade lobjBookingIntegrationFacade = new BookingIntegrationFacade();
                BookingResponse lobjBookingResponse = lobjBookingIntegrationFacade.BookForFlight(pobjBookingRequest, pobjMemberDetails);

                return lobjBookingResponse;
            }
            catch (ApplicationException ex)
            {
                LoggingAdapter.WriteLog("Model Book For Flight Application Exception" + ex.Message + Environment.NewLine + "Stack Trace-" + ex.StackTrace + Environment.NewLine + "Inner Exception-" + ex.InnerException);
                throw new ApplicationException(ex.Message);
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("Model Book For Flight Ex" + ex.Message + Environment.NewLine + "Stack Trace-" + ex.StackTrace + Environment.NewLine + "Inner Exception-" + ex.InnerException);
                throw new ApplicationException(ex.Message);

            }

        }

        // Only IBE Flight Hotel Booking without Redemption 

        #region PG
        public bool InsertTransactionDetails(string relationReference, decimal amount, int points,
            TransactionType transactionType, LoyaltyTxnType loyaltyTxnType, string externalReference,
            string merchantName, string additionalDetail, string additionalDetail1, decimal sourceAmount, string txnCurrency)
        {
            bool response = false;
            try
            {
                ProgramDefinition lobjProgramDefinition = GetProgramMaster();
                string lstrCurrency = GetDefaultCurrency();

                string lstrToken = GetAuthTokenforWebAPI();
                if (lobjProgramDefinition != null && lobjProgramDefinition.ProgramId > 0 && !string.IsNullOrEmpty(lstrCurrency))
                {
                    TransactionDetails transactionDetails = new TransactionDetails()
                    {
                        TransactionType = transactionType,
                        RelationReference = relationReference,
                        Amounts = amount,
                        Points = points,
                        LoyaltyTxnType = loyaltyTxnType,
                        ProgramId = lobjProgramDefinition.ProgramId,
                        TransactionCurrency = lstrCurrency,
                        RelationType = RelationType.LBMS,
                        TransactionDate = DateTime.Now,
                        ProcessingDate = DateTime.Now,
                        ExpiryDate = DateTime.Now,
                        ReconciledPoints = 0,
                        ReconciledType = 1,
                        Narration = string.Format("{0} {1}", "Redemption", Convert.ToString(loyaltyTxnType)),
                        MerchantName = merchantName,
                        ExternalReference = externalReference,
                        AdditionalDetail = additionalDetail,
                        AdditionalDetails1 = additionalDetail1
                    };
                    TransactionDetailsBreakage transactionDetailsBreakage = new TransactionDetailsBreakage()
                    {
                        IsBillable = true,
                        SourceAmount = sourceAmount,
                        SourceCurrency = txnCurrency,
                        TxnCurrency = txnCurrency,
                        TransactionSource = "GiiftLoyalty"
                    };
                    transactionDetails.TransactionDetailBreakage = transactionDetailsBreakage;
                    response = lobjAPIClientHelper.InsertManualTransactionDetails(transactionDetails, lstrToken);
                }
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("Model InsertTransactionDetails Ex-" + ex.InnerException + ex.StackTrace + ex.Message);
            }
            return response;
        }
        public bool InitiatePayment(PGRequest pgRequest)
        {
            bool lboolResponse = false;
            PGResponse lobjPGResponse = null;
            try
            {
                string lstrToken = GetPGAuthToken();

                lobjPGResponse = lobjAPIClientHelper.InitiatePayment(pgRequest, lstrToken);
                HttpContext.Current.Session["PGPaymentResponse"] = lobjPGResponse;
                if (lobjPGResponse != null)
                {
                    if (lobjPGResponse.data.Count > 0)
                    {
                        lboolResponse = true;
                    }
                }
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("Model InitiatePayment Ex-" + ex.InnerException + ex.StackTrace + ex.Message);
            }
            return lboolResponse;
        }
        public string GetPGAuthToken()
        {
            APIClientHelper lobjAPIClientHelper = new APIClientHelper();
            string AccessToken = string.Empty;
            PGTokenResponse authTokenResponse = null;
            try
            {
                if (HttpContext.Current.Session["PGAccessToken"] != null && HttpContext.Current.Session["PGAccessTokenTime"] != null)
                {
                    authTokenResponse = HttpContext.Current.Session["PGAccessToken"] as PGTokenResponse;
                    DateTime dtAccessTokenTime = Convert.ToDateTime(HttpContext.Current.Session["PGAccessTokenTime"]);
                    TimeSpan dt = DateTime.Now - dtAccessTokenTime;
                    if (dt.TotalMinutes > 25)
                        authTokenResponse = null;
                }
            }
            catch { }
            if (authTokenResponse == null)
            {
                try
                {
                    // CoreAuthTokenRequest authTokenRequest = new CoreAuthTokenRequest();
                    PGTokenRequest authTokenRequest = new PGTokenRequest();
                    authTokenRequest.username = ConfigurationManager.AppSettings["PGAPIUserName"];
                    authTokenRequest.password = ConfigurationManager.AppSettings["PGAPIPassword"];
                    authTokenResponse = lobjAPIClientHelper.GetAuthToken(authTokenRequest);
                    try
                    {
                        HttpContext.Current.Session["PGAccessToken"] = authTokenResponse;
                        HttpContext.Current.Session["PGAccessTokenTime"] = DateTime.Now;
                    }
                    catch { }
                }
                catch (Exception ex)
                {
                    LoggingAdapter.WriteLog("PaymentReviewConfirm GetPGAuthToken AccessToken null");
                    LoggingAdapter.WriteLog("PaymentReviewConfirm GetPGAuthToken Ex-" + ex.InnerException + ex.StackTrace + ex.Message);
                }
            }
            AccessToken = authTokenResponse.data[0].token.ToString();
            return AccessToken;
        }

        public PGDetails GetPaymentStatusByOrderId(string orderId)
        {
            PGDetails lobjPGDetails = null;
            try
            {
                string lstrToken = GetPGAuthToken();

                lobjPGDetails = lobjAPIClientHelper.GetPaymentStatusByOrderId(orderId, lstrToken);
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("Model GetPaymentStatusByOrderId Ex-" + ex.InnerException + ex.StackTrace + ex.Message);
            }
            return lobjPGDetails;
        }

        public bool InitiatePaymentRefund(string orderId, decimal refundAmount, string refundNote)
        {
            bool lboolResponse = false;
            PGRefundResponse pgRefundResponse = null;
            try
            {
                string lstrToken = GetPGAuthToken();
                PGRefundRequest pgRefundRequest = new PGRefundRequest()
                {
                    orderId = orderId,
                    refundAmount = Convert.ToInt32(refundAmount),
                    refundNote = refundNote
                };
                pgRefundResponse = lobjAPIClientHelper.InitiatePaymentRefund(pgRefundRequest, lstrToken);
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("Model InitiatePaymentRefund Ex-" + ex.InnerException + ex.StackTrace + ex.Message);
            }
            return lboolResponse;
        }


        public MemberDetails GetMemberDetailsByUniqueAttribute(int pintProgramId, string pstrUniqueAttributeValue)
        {
            MemberDetails lobjMemberDetails = null;
            try
            {
                string lstrToken = GetAuthTokenforWebAPI();
                lobjMemberDetails = lobjAPIClientHelper.GetMemberDetailsByUniqueAttribute(pintProgramId, pstrUniqueAttributeValue, lstrToken);
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("Model GetMemberDetailsByUniqueAttribute Ex-" + ex.InnerException + ex.StackTrace + ex.Message);
            }
            return lobjMemberDetails;
        }
        #endregion
    }
}
