using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BeMyGuest.Implementor;
using BeMyGuest.Entities;
using Framework.EnterpriseLibrary.Adapters;
using Core.Platform.Transactions.Entites;

namespace BeMyGuest.ClientHelper
{
    public class BeMyGuestClientHelper
    {
        BeMyGuestImplementor lobjImplementor = new BeMyGuestImplementor();
        public ExperiencesResponse GetProductList(int pintPage, int pintPageSize, ExperiencesRequest lobjProductListRequest)
        {
            ExperiencesResponse lobjResponse = null;
            try
            {
                lobjResponse = lobjImplementor.GetProductList(pintPage, pintPageSize, lobjProductListRequest);
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("BeMyGuestClientHelper GetProductList Ex - " + Environment.NewLine + ex.Message + Environment.NewLine + ex.InnerException +
                    Environment.NewLine + ex.StackTrace + Environment.NewLine + "DateTime - " + DateTime.Now);
            }
            return lobjResponse;
        }

        public ProductInfoResponse GetProductInfo(ProductInfoRequest productInfoRequest)
        {
            ProductInfoResponse lobjResponse = null;
            try
            {
                lobjResponse = lobjImplementor.GetProductInfo(productInfoRequest);
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("BeMyGuestClientHelper GetProductInfo Ex - " + Environment.NewLine + ex.Message + Environment.NewLine + ex.InnerException +
                    Environment.NewLine + ex.StackTrace + Environment.NewLine + "DateTime - " + DateTime.Now);
            }
            return lobjResponse;
        }

        public ProductTypesPriceByDateResponse GetProductTypesPriceByDate(ProductTypesPriceByDateRequest productTypesPriceByDateRequest)
        {
            ProductTypesPriceByDateResponse lobjResponse = null;
            try
            {
                lobjResponse = lobjImplementor.GetProductTypesPriceByDate(productTypesPriceByDateRequest);
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("BeMyGuestClientHelper GetProductTypesPriceByDate Ex - " + Environment.NewLine + ex.Message + Environment.NewLine + ex.InnerException +
                    Environment.NewLine + ex.StackTrace + Environment.NewLine + "DateTime - " + DateTime.Now);
            }
            return lobjResponse;
        }

        public ExperiencesTypesAndCategory GetTypesAndCategory()
        {
            ExperiencesTypesAndCategory lobjResponse = null;
            try
            {
                lobjResponse = lobjImplementor.GetTypesAndCategory();
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("BeMyGuestClientHelper GetTypesAndCategory Ex - " + Environment.NewLine + ex.Message + Environment.NewLine + ex.InnerException +
                    Environment.NewLine + ex.StackTrace + Environment.NewLine + "DateTime - " + DateTime.Now);
            }
            return lobjResponse;
        }

        public BookingResponse ExperienceBooking(BookingRequest bookingRequest)
        {
            BookingResponse lobjResponse = null;
            try
            {
                lobjResponse = lobjImplementor.ExperienceBooking(bookingRequest);
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("BeMyGuestClientHelper ExperienceBooking Ex - " + Environment.NewLine + ex.Message + Environment.NewLine + ex.InnerException +
                    Environment.NewLine + ex.StackTrace + Environment.NewLine + "DateTime - " + DateTime.Now);
            }
            return lobjResponse;
        }
        public BookingInfoByUUIDResponse GetBookingInfoByUUID(BookingInfoByUUIDRequest lobjbookingInfoByUUIDRequest)
        {
            BookingInfoByUUIDResponse lobjResponse = null;
            try
            {
                lobjResponse = lobjImplementor.GetBookingInfoByUUID(lobjbookingInfoByUUIDRequest);
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("BeMyGuestClientHelper GetBookingInfoByUUID Ex - " + Environment.NewLine + ex.Message + Environment.NewLine + ex.InnerException +
                    Environment.NewLine + ex.StackTrace + Environment.NewLine + "DateTime - " + DateTime.Now);
            }
            return lobjResponse;
        }
        public BookingByUserResponse GetBookingByUser(BookingByUserRequest bookingByUserRequest)
        {
            BookingByUserResponse lobjResponse = null;
            try
            {
                lobjResponse = lobjImplementor.GetBookingByUser(bookingByUserRequest);
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("BeMyGuestClientHelper GetBookingByUser Ex - " + Environment.NewLine + ex.Message + Environment.NewLine + ex.InnerException +
                    Environment.NewLine + ex.StackTrace + Environment.NewLine + "DateTime - " + DateTime.Now);
            }
            return lobjResponse;
        }

    }
}
