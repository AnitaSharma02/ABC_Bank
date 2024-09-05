using BeMyGuest.Entities;
using Core.Platform.Transactions.Entites;
using Framework.EnterpriseLibrary.Adapters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace BeMyGuest.Implementor
{
    public class BeMyGuestImplementor
    {
        string ExperienceUsername = Convert.ToString(ConfigurationManager.AppSettings["ExperiencesAPIUserName"]);
        string ExperiencePassword = Convert.ToString(ConfigurationManager.AppSettings["ExperiencesAPIPassword"]);
        public ExperiencesResponse GetProductList(int pintPage, int pageSize, ExperiencesRequest lobjProductListRequest)
        {
            ExperiencesResponse lobjResponse = null;
            try
            {             
                lobjResponse = JsonConvert.DeserializeObject<ExperiencesResponse>(DataPostHelper.PostData(BeMyGuestConstants.Search, "POST", "search", JsonConvert.SerializeObject(lobjProductListRequest), ExperienceUsername, ExperiencePassword));
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("PostData - GetProductList - Ex -" + ex.Message + Environment.NewLine + "Stack Trace-" + ex.StackTrace + ex.InnerException);
            }
            return lobjResponse;
        }

        public ProductInfoResponse GetProductInfo(ProductInfoRequest productInfoRequest)
        {
            ProductInfoResponse lobjResponse = null;
            try
            {
                lobjResponse = JsonConvert.DeserializeObject<ProductInfoResponse>(DataPostHelper.PostData(BeMyGuestConstants.GetProductInfo, "POST", "getproductinfo", JsonConvert.SerializeObject(productInfoRequest), ExperienceUsername, ExperiencePassword));
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("PostData - GetProductInfo - Ex -" + ex.Message + Environment.NewLine + "Stack Trace-" + ex.StackTrace + ex.InnerException);
            }
            return lobjResponse;
        }

        public ProductTypesPriceByDateResponse GetProductTypesPriceByDate(ProductTypesPriceByDateRequest productTypesPriceByDateRequest)
        {
            ProductTypesPriceByDateResponse lobjResponse = null;
            try
            {
                lobjResponse = JsonConvert.DeserializeObject<ProductTypesPriceByDateResponse>(DataPostHelper.PostData(BeMyGuestConstants.GetProductTypesPriceByDate, "POST", "getproducttypespricebydate", JsonConvert.SerializeObject(productTypesPriceByDateRequest), ExperienceUsername, ExperiencePassword));
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("PostData - GetProductTypesPriceByDate - Ex -" + ex.Message + Environment.NewLine + "Stack Trace-" + ex.StackTrace + ex.InnerException);
            }
            return lobjResponse;
        }

        public ExperiencesTypesAndCategory GetTypesAndCategory()
        {
            ExperiencesTypesAndCategory lobjResponse = null;
            try
            {
                lobjResponse = JsonConvert.DeserializeObject<ExperiencesTypesAndCategory>(DataPostHelper.PostData(BeMyGuestConstants.GetTypesAndCategory, "POST", "gettypesandcategory", ExperienceUsername, ExperiencePassword));
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("PostData - GetTypesAndCategory - Ex -" + ex.Message + Environment.NewLine + "Stack Trace-" + ex.StackTrace + ex.InnerException);
            }
            return lobjResponse;
        }

        public BookingResponse ExperienceBooking(BookingRequest bookingRequest)
        {
            BookingResponse lobjResponse = null;
            try
            {
               lobjResponse = JsonConvert.DeserializeObject<BookingResponse>(DataPostHelper.PostData(BeMyGuestConstants.ExperienceBooking, "POST", "bookings", JsonConvert.SerializeObject(bookingRequest), ExperienceUsername, ExperiencePassword));
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("PostData - ExperienceBooking - Ex -" + ex.Message + Environment.NewLine + "Stack Trace-" + ex.StackTrace + ex.InnerException);
            }
            return lobjResponse;
        }

        public BookingInfoByUUIDResponse GetBookingInfoByUUID(BookingInfoByUUIDRequest lobjbookingInfoByUUIDRequest)
        {
            BookingInfoByUUIDResponse lobjResponse = null;
            try
            {
                lobjResponse = JsonConvert.DeserializeObject<BookingInfoByUUIDResponse>(DataPostHelper.PostData(BeMyGuestConstants.GetBookingInfoByUUID, "POST", "bookings", JsonConvert.SerializeObject(lobjbookingInfoByUUIDRequest), ExperienceUsername, ExperiencePassword));
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("PostData - GetBookingInfoByUUID - Ex -" + ex.Message + Environment.NewLine + "Stack Trace-" + ex.StackTrace + ex.InnerException);
            }
            return lobjResponse;
        }

        public BookingByUserResponse GetBookingByUser(BookingByUserRequest bookingByUserRequest)
        {
            BookingByUserResponse lobjResponse = null;
            try
            {
                lobjResponse = JsonConvert.DeserializeObject<BookingByUserResponse>(DataPostHelper.PostData(BeMyGuestConstants.GetAllExperienceBooking, "POST", "bookings", JsonConvert.SerializeObject(bookingByUserRequest), ExperienceUsername, ExperiencePassword));
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("PostData - GetBookingByUser - Ex -" + ex.Message + Environment.NewLine + "Stack Trace-" + ex.StackTrace + ex.InnerException);
            }
            return lobjResponse;
        }
        public string GenerateQuery(string pstrQueryName, string pstrQueryType, string pstrSubSelection)
        {
            string query = string.Empty;
            if (!string.IsNullOrEmpty(pstrSubSelection))
            {
                query = "query { " + pstrQueryName + " ( " + pstrQueryType + ":\"";
                if (pstrSubSelection.Length > 0)
                {
                    query += pstrSubSelection.Replace("\"", "\\\"");
                }
                query += "\")}";
            }
            return query;
        }

    }
}
