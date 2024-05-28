using System;
using System.Collections.Generic;
using Framework.EnterpriseLibrary.Adapters;
using Holibob.Entities;
using Holibob.Implementor;

namespace Holibob.ClientHelper
{
    public class HolibobHelper
    {
        HolibobImplementor lobjImplementor = new HolibobImplementor();
        public ProductListResponse GetProductList(int pintPage, int pintLimit, string pstrSort, string pstrPlaceName, string pstrGuidePrice)
        {
            ProductListResponse lobjResponse = null;
            try
            {
                lobjResponse = lobjImplementor.GetProductList(pintPage, pintLimit, pstrSort, pstrPlaceName, pstrGuidePrice);
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("HolibobHelper GetProductList Ex - " + Environment.NewLine + ex.Message + Environment.NewLine + ex.InnerException +
                    Environment.NewLine + ex.StackTrace + Environment.NewLine + "DateTime - " + DateTime.Now);
            }
            return lobjResponse;
        }
        public ProductInfoResponse GetProductInfo(string pstrProductId)
        {
            ProductInfoResponse lobjResponse = null;
            try
            {
                lobjResponse = lobjImplementor.GetProductInfo(pstrProductId);
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("HolibobHelper GetProductInfo Ex - " + Environment.NewLine + ex.Message + Environment.NewLine + ex.InnerException +
                    Environment.NewLine + ex.StackTrace + Environment.NewLine + "DateTime - " + DateTime.Now);
            }
            return lobjResponse;
        }
        public ProductStatusResponse GetProductStatus(string pstrProductId, string pstrStartDate, string pstrEndDate, string pstrAvailabilityType)
        {
            ProductStatusResponse lobjResponse = null;
            try
            {
                lobjResponse = lobjImplementor.GetProductStatus(pstrProductId, pstrStartDate, pstrEndDate, pstrAvailabilityType);
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("HolibobHelper GetProductStatus Ex - " + Environment.NewLine + ex.Message + Environment.NewLine + ex.InnerException +
                    Environment.NewLine + ex.StackTrace + Environment.NewLine + "DateTime - " + DateTime.Now);
            }
            return lobjResponse;
        }
        public FetchAvailabilityResponse FetchAvailability(string pstrAvailabilityId)
        {
            FetchAvailabilityResponse lobjResponse = null;
            try
            {
                lobjResponse = lobjImplementor.FetchAvailability(pstrAvailabilityId);
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("HolibobHelper FetchAvailability Ex - " + Environment.NewLine + ex.Message + Environment.NewLine + ex.InnerException +
                    Environment.NewLine + ex.StackTrace + Environment.NewLine + "DateTime - " + DateTime.Now);
            }
            return lobjResponse;
        }
        public FetchAvailabilityResponse FetchAvailabilityWithOptionList(string pstrAvailabilityId, string pstrInputId, string pstrInputValue)
        {
            FetchAvailabilityResponse lobjResponse = null;
            try
            {
                lobjResponse = lobjImplementor.FetchAvailabilityWithOptionList(pstrAvailabilityId, pstrInputId, pstrInputValue);
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("HolibobHelper FetchAvailabilityWithOptionList Ex - " + Environment.NewLine + ex.Message + Environment.NewLine + ex.InnerException +
                    Environment.NewLine + ex.StackTrace + Environment.NewLine + "DateTime - " + DateTime.Now);
            }
            return lobjResponse;
        }
        public FetchAvailabilityResponse FetchAvailabilityWithOptionList(string pstrAvailabilityId, List<FetchAvailabilityWithPricingCategoryOptionList> plstobjFetchAvailabilityWithPricingCategoryOptionList)
        {
            FetchAvailabilityResponse lobjResponse = null;
            try
            {
                lobjResponse = lobjImplementor.FetchAvailabilityWithOptionList(pstrAvailabilityId, plstobjFetchAvailabilityWithPricingCategoryOptionList);
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("HolibobHelper FetchAvailabilityWithOptionList Ex - " + Environment.NewLine + ex.Message + Environment.NewLine + ex.InnerException +
                    Environment.NewLine + ex.StackTrace + Environment.NewLine + "DateTime - " + DateTime.Now);
            }
            return lobjResponse;
        }
        public CreateBookingResponse CreateBooking()
        {
            CreateBookingResponse lobjResponse = null;
            try
            {
                lobjResponse = lobjImplementor.CreateBooking();
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("HolibobHelper CreateBooking Ex - " + Environment.NewLine + ex.Message + Environment.NewLine + ex.InnerException +
                    Environment.NewLine + ex.StackTrace + Environment.NewLine + "DateTime - " + DateTime.Now);
            }
            return lobjResponse;
        }
        public AddAvailabilityToBookingResponse AddAvailabilityToBooking(string pstrAvailabilityId, string pstrBookId)
        {
            AddAvailabilityToBookingResponse lobjResponse = null;
            try
            {
                lobjResponse = lobjImplementor.AddAvailabilityToBooking(pstrAvailabilityId, pstrBookId);
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("HolibobHelper AddAvailabilityToBooking Ex - " + Environment.NewLine + ex.Message + Environment.NewLine + ex.InnerException +
                    Environment.NewLine + ex.StackTrace + Environment.NewLine + "DateTime - " + DateTime.Now);
            }
            return lobjResponse;
        }
        public OrderStatusResponse GetOrderStatus(string pstrBookId, string pstrLeadPassengerName)
        {
            OrderStatusResponse lobjResponse = null;
            try
            {
                lobjResponse = lobjImplementor.GetOrderStatus(pstrBookId, pstrLeadPassengerName);
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("HolibobHelper GetOrderStatus Ex - " + Environment.NewLine + ex.Message + Environment.NewLine + ex.InnerException +
                    Environment.NewLine + ex.StackTrace + Environment.NewLine + "DateTime - " + DateTime.Now);
            }
            return lobjResponse;
        }
        public OrderStatusResponse GetOrderStatusByBookingId(string pstrBookId)
        {
            OrderStatusResponse lobjResponse = null;
            try
            {
                lobjResponse = lobjImplementor.GetOrderStatusByBookingId(pstrBookId);
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("HolibobHelper GetOrderStatusByBookingId Ex - " + Environment.NewLine + ex.Message + Environment.NewLine + ex.InnerException +
                    Environment.NewLine + ex.StackTrace + Environment.NewLine + "DateTime - " + DateTime.Now);
            }
            return lobjResponse;
        }
        
        public OrderStatusResponse SubmitBookingAnswer(string pstrBookId, List<ExperienceBookingAnswerList> plstobjAnswerList)
        {
            OrderStatusResponse lobjResponse = null;
            try
            {
                lobjResponse = lobjImplementor.SubmitBookingAnswer(pstrBookId, plstobjAnswerList);
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("HolibobHelper SubmitBookingAnswer Ex - " + Environment.NewLine + ex.Message + Environment.NewLine + ex.InnerException +
                    Environment.NewLine + ex.StackTrace + Environment.NewLine + "DateTime - " + DateTime.Now);
            }
            return lobjResponse;
        }
        public PlaceOrderResponse PlaceOrder(string pstrBookId)
        {
            PlaceOrderResponse lobjResponse = null;
            try
            {
                lobjResponse = lobjImplementor.PlaceOrder(pstrBookId);
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("HolibobHelper PlaceOrder Ex - " + Environment.NewLine + ex.Message + Environment.NewLine + ex.InnerException +
                    Environment.NewLine + ex.StackTrace + Environment.NewLine + "DateTime - " + DateTime.Now);
            }
            return lobjResponse;
        }
        public ProductSearchResponse GetSearchList(string pstrSearchText)
        {
            ProductSearchResponse lobjResponse = null;
            try
            {
                lobjResponse = lobjImplementor.GetSearchList(pstrSearchText);
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("HolibobHelper GetSearchList Ex - " + Environment.NewLine + ex.Message + Environment.NewLine + ex.InnerException +
                    Environment.NewLine + ex.StackTrace + Environment.NewLine + "DateTime - " + DateTime.Now);
            }
            return lobjResponse;
        }
        public SearchProductListResponse GetExperienceProductListByPlaceId(string pstrPlaceId, bool pblnIsPrivate, bool pblnIsNew, string pstrIsRecommended,
            string pstrGuidePrice, string pstrSearch, List<string> plstCategoryIds, List<string> plstAttributeIds)
        {
            SearchProductListResponse lobjResponse = null;
            try
            {
                lobjResponse = lobjImplementor.GetExperienceProductListByPlaceId(pstrPlaceId, pblnIsPrivate, pblnIsNew, pstrIsRecommended, pstrGuidePrice, pstrSearch, plstCategoryIds, plstAttributeIds);
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("HolibobHelper GetExperienceProductListByPlaceId Ex - " + Environment.NewLine + ex.Message + Environment.NewLine + ex.InnerException +
                    Environment.NewLine + ex.StackTrace + Environment.NewLine + "DateTime - " + DateTime.Now);
            }
            return lobjResponse;
        }
    }
}
