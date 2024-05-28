using Framework.EnterpriseLibrary.Adapters;
using Holibob.Entities;
using Holibob.Helper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Holibob.Implementor
{
    public class HolibobImplementor
    {
        DataPostHelper lobjDataPostHelper = new DataPostHelper();
        public ProductListResponse GetProductList(int pintPage, int pintLimit, string pstrSort, string pstrPlaceName, string pstrGuidePrice)
        {
            ProductListResponse lobjResponse = null;
            try
            {
                ProductListRequest lobjProductListRequest = new ProductListRequest
                {
                    schemaName = HolibobConstant.HolibobSchemaName,
                    page = pintPage,
                    limit = pintLimit,
                    readCacheFirst = HolibobConstant.HolibobReadCacheFirst,
                    cacheExpireInSec = HolibobConstant.HolibobCacheExpireInSec,
                    placeName = pstrPlaceName,
                    isRecommended = pstrSort,
                    guidePrice = pstrGuidePrice,
                    currency = HolibobConstant.HolibobCurrency
                };
                PostRequest lobjPostRequest = new PostRequest
                {
                    vendor = HolibobConstant.HolibobVendorName,
                    query = GenerateQuery("getProductList", "request", JsonConvert.SerializeObject(lobjProductListRequest))
                };
                lobjResponse = JsonConvert.DeserializeObject<ProductListResponse>(lobjDataPostHelper.PostData(HolibobConstant.HolibobEndPoint, "POST", "getProductList", JsonConvert.SerializeObject(lobjPostRequest)));
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("PostData - GetProductList - Ex -" + ex.Message + Environment.NewLine + "Stack Trace-" + ex.StackTrace + ex.InnerException);
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
        public ProductInfoResponse GetProductInfo(string pstrProductId)
        {
            ProductInfoResponse lobjResponse = null;
            try
            {
                ProductInfoRequest lobjProductInfoRequest = new ProductInfoRequest
                {
                    program = new Program
                    {
                        references = new References
                        {
                            Holibob = pstrProductId,
                            currency = HolibobConstant.HolibobCurrency
                        }
                    }
                };
                PostRequest lobjPostRequest = new PostRequest
                {
                    vendor = HolibobConstant.HolibobVendorName,
                    query = GenerateQuery("getProductInfo", "request", JsonConvert.SerializeObject(lobjProductInfoRequest))
                };
                lobjResponse = JsonConvert.DeserializeObject<ProductInfoResponse>(lobjDataPostHelper.PostData(HolibobConstant.HolibobEndPoint, "POST", "getProductInfo", JsonConvert.SerializeObject(lobjPostRequest)));
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("PostData - GetProductInfo - Ex -" + ex.Message + Environment.NewLine + "Stack Trace-" + ex.StackTrace + ex.InnerException);
            }
            return lobjResponse;
        }
        public ProductStatusResponse GetProductStatus(string pstrProductId, string pstrStartDate, string pstrEndDate, string pstrAvailabilityType)
        {
            ProductStatusResponse lobjResponse = null;
            try
            {
                if (string.IsNullOrEmpty(pstrAvailabilityType))
                {
                    ProductInfoResponse lobjProductInfoResponse = GetProductInfo(pstrProductId);
                    if (lobjProductInfoResponse != null)
                    {
                        ProductInfo lobjProductInfo = JsonConvert.DeserializeObject<ProductInfo>(lobjProductInfoResponse.data.getProductInfo);
                        if (lobjProductInfo != null && lobjProductInfo.rawData != null && lobjProductInfo.status.ToLower() == "success")
                        {
                            ProductStatusRequest lobjProductStatusRequest = new ProductStatusRequest
                            {
                                program = new Program
                                {
                                    references = new References
                                    {
                                        Holibob = pstrProductId,
                                        start_date = pstrStartDate,
                                        end_date = pstrEndDate,
                                        availabilityType = lobjProductInfo.rawData.origSupplierResponse.product.availabilityType,
                                        currency = HolibobConstant.HolibobCurrency
                                    }
                                }
                            };
                            PostRequest lobjPostRequest = new PostRequest
                            {
                                vendor = HolibobConstant.HolibobVendorName,
                                query = GenerateQuery("getProductStatus", "request", JsonConvert.SerializeObject(lobjProductStatusRequest))
                            };
                            lobjResponse = JsonConvert.DeserializeObject<ProductStatusResponse>(lobjDataPostHelper.PostData(HolibobConstant.HolibobEndPoint, "POST", "getProductStatus", JsonConvert.SerializeObject(lobjPostRequest)));
                            lobjResponse.availabilityType = lobjProductInfo.rawData.origSupplierResponse.product.availabilityType;
                        }
                    }
                }
                else if (!string.IsNullOrEmpty(pstrAvailabilityType))
                {
                    ProductStatusRequest lobjProductStatusRequest = new ProductStatusRequest
                    {
                        program = new Program
                        {
                            references = new References
                            {
                                Holibob = pstrProductId,
                                start_date = pstrStartDate,
                                end_date = pstrEndDate,
                                availabilityType = pstrAvailabilityType,
                                currency = HolibobConstant.HolibobCurrency
                            }
                        }
                    };
                    PostRequest lobjPostRequest = new PostRequest
                    {
                        vendor = HolibobConstant.HolibobVendorName,
                        query = GenerateQuery("getProductStatus", "request", JsonConvert.SerializeObject(lobjProductStatusRequest))
                    };
                    lobjResponse = JsonConvert.DeserializeObject<ProductStatusResponse>(lobjDataPostHelper.PostData(HolibobConstant.HolibobEndPoint, "POST", "getProductStatus", JsonConvert.SerializeObject(lobjPostRequest)));
                    lobjResponse.availabilityType = pstrAvailabilityType;
                }
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("PostData - GetProductStatus - Ex -" + ex.Message + Environment.NewLine + "Stack Trace-" + ex.StackTrace + ex.InnerException);
            }
            return lobjResponse;
        }
        public FetchAvailabilityResponse FetchAvailability(string pstrAvailabilityId)
        {
            FetchAvailabilityResponse lobjResponse = null;
            try
            {
                FetchAvailabilityRequest lobjFetchAvailabilityResponse = new FetchAvailabilityRequest
                {
                    availabilityId = pstrAvailabilityId,
                    currency = HolibobConstant.HolibobCurrency
                };
                PostRequest lobjPostRequest = new PostRequest
                {
                    vendor = HolibobConstant.HolibobVendorName,
                    query = GenerateQuery("fetchAvailabilityWithOptionList", "request", JsonConvert.SerializeObject(lobjFetchAvailabilityResponse))
                };
                lobjResponse = JsonConvert.DeserializeObject<FetchAvailabilityResponse>(lobjDataPostHelper.PostData(HolibobConstant.HolibobEndPoint, "POST", "fetchAvailabilityWithOptionList", JsonConvert.SerializeObject(lobjPostRequest)));
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("PostData - FetchAvailability - Ex -" + ex.Message + Environment.NewLine + "Stack Trace-" + ex.StackTrace + ex.InnerException);
            }
            return lobjResponse;
        }
        public FetchAvailabilityResponse FetchAvailabilityWithOptionList(string pstrAvailabilityId, string pstrInputId, string pstrInputValue)
        {
            FetchAvailabilityResponse lobjResponse = null;
            try
            {
                FetchAvailabilityWithOptionListRequest lobjFetchAvailabilityWithOptionListRequest = new FetchAvailabilityWithOptionListRequest
                {
                    availabilityId = pstrAvailabilityId,
                    currency = HolibobConstant.HolibobCurrency

                };
                OptionList lobjOptionList = new OptionList
                {
                    id = pstrInputId,
                    value = pstrInputValue
                };
                lobjFetchAvailabilityWithOptionListRequest.input = new OptionListInput
                {
                    optionList = new List<OptionList>
                    {
                        lobjOptionList
                    }
                };
                PostRequest lobjInputPostRequest = new PostRequest
                {
                    vendor = HolibobConstant.HolibobVendorName,
                    query = GenerateQuery("inputAvailabilityWithOptionList", "request", JsonConvert.SerializeObject(lobjFetchAvailabilityWithOptionListRequest))
                };
                InputAvailabilityWithOptionListResponse lobjInputAvailabilityWithOptionListResponse = JsonConvert.DeserializeObject<InputAvailabilityWithOptionListResponse>(lobjDataPostHelper.PostData(HolibobConstant.HolibobEndPoint, "POST", "inputAvailabilityWithOptionList", JsonConvert.SerializeObject(lobjInputPostRequest)));
                if (lobjInputAvailabilityWithOptionListResponse != null && lobjInputAvailabilityWithOptionListResponse.data != null && !string.IsNullOrEmpty(lobjInputAvailabilityWithOptionListResponse.data.inputAvailabilityWithOptionList))
                {
                    PostRequest lobjPostRequest = new PostRequest
                    {
                        vendor = HolibobConstant.HolibobVendorName,
                        query = GenerateQuery("fetchAvailabilityWithOptionList", "request", JsonConvert.SerializeObject(lobjFetchAvailabilityWithOptionListRequest))
                    };
                    lobjResponse = JsonConvert.DeserializeObject<FetchAvailabilityResponse>(lobjDataPostHelper.PostData(HolibobConstant.HolibobEndPoint, "POST", "fetchAvailabilityWithOptionList", JsonConvert.SerializeObject(lobjPostRequest)));
                }
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("PostData - FetchAvailabilityWithOptionList - Ex -" + ex.Message + Environment.NewLine + "Stack Trace-" + ex.StackTrace + ex.InnerException);
            }
            return lobjResponse;
        }
        public FetchAvailabilityResponse FetchAvailabilityWithOptionList(string pstrAvailabilityId, List<FetchAvailabilityWithPricingCategoryOptionList> plstobjFetchAvailabilityWithPricingCategoryOptionList)
        {
            FetchAvailabilityResponse lobjResponse = null;
            try
            {
                FetchAvailabilityWithPricingCategoryRequest lobjFetchAvailabilityWithPricingCategoryRequest = new FetchAvailabilityWithPricingCategoryRequest
                {
                    availabilityId = pstrAvailabilityId,
                    currency = HolibobConstant.HolibobCurrency
                };
                lobjFetchAvailabilityWithPricingCategoryRequest.input = new FetchAvailabilityWithPricingCategoryInput
                {
                    pricingCategoryList = plstobjFetchAvailabilityWithPricingCategoryOptionList
                };
                PostRequest lobjInputPostRequest = new PostRequest
                {
                    vendor = HolibobConstant.HolibobVendorName,
                    query = GenerateQuery("inputAvailabilityWithOptionList", "request", JsonConvert.SerializeObject(lobjFetchAvailabilityWithPricingCategoryRequest))
                };
                InputAvailabilityWithOptionListResponse lobjInputAvailabilityWithOptionListResponse = JsonConvert.DeserializeObject<InputAvailabilityWithOptionListResponse>(lobjDataPostHelper.PostData(HolibobConstant.HolibobEndPoint, "POST", "inputAvailabilityWithOptionList", JsonConvert.SerializeObject(lobjInputPostRequest)));
                if (lobjInputAvailabilityWithOptionListResponse != null && lobjInputAvailabilityWithOptionListResponse.data != null && !string.IsNullOrEmpty(lobjInputAvailabilityWithOptionListResponse.data.inputAvailabilityWithOptionList))
                {
                    PostRequest lobjPostRequest = new PostRequest
                    {
                        vendor = HolibobConstant.HolibobVendorName,
                        query = GenerateQuery("fetchAvailabilityWithOptionList", "request", JsonConvert.SerializeObject(lobjFetchAvailabilityWithPricingCategoryRequest))
                    };
                    lobjResponse = JsonConvert.DeserializeObject<FetchAvailabilityResponse>(lobjDataPostHelper.PostData(HolibobConstant.HolibobEndPoint, "POST", "fetchAvailabilityWithOptionList", JsonConvert.SerializeObject(lobjPostRequest)));
                }
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("PostData - FetchAvailabilityWithOptionList - Ex -" + ex.Message + Environment.NewLine + "Stack Trace-" + ex.StackTrace + ex.InnerException);
            }
            return lobjResponse;
        }
        public CreateBookingResponse CreateBooking()
        {
            CreateBookingResponse lobjResponse = null;
            try
            {
                PostRequest lobjPostRequest = new PostRequest
                {
                    vendor = HolibobConstant.HolibobVendorName,
                    query = "query { createBooking ( request: \"{\\\"currency\\\":\\\"" + HolibobConstant.HolibobCurrency + "\\\"}\" )}"
                };
                lobjResponse = JsonConvert.DeserializeObject<CreateBookingResponse>(lobjDataPostHelper.PostData(HolibobConstant.HolibobEndPoint, "POST", "createBooking", JsonConvert.SerializeObject(lobjPostRequest)));
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("PostData - CreateBooking - Ex -" + ex.Message + Environment.NewLine + "Stack Trace-" + ex.StackTrace + ex.InnerException);
            }
            return lobjResponse;
        }
        public AddAvailabilityToBookingResponse AddAvailabilityToBooking(string pstrAvailabilityId, string pstrBookId)
        {
            AddAvailabilityToBookingResponse lobjResponse = null;
            try
            {
                AddAvailabilityToBookingRequest lobjAddAvailabilityToBookingRequest = new AddAvailabilityToBookingRequest
                {
                    availabilityId = pstrAvailabilityId,
                    bookId = pstrBookId,
                    currency = HolibobConstant.HolibobCurrency
                };
                PostRequest lobjPostRequest = new PostRequest
                {
                    vendor = HolibobConstant.HolibobVendorName,
                    query = GenerateQuery("addAvailabilityToBooking", "request", JsonConvert.SerializeObject(lobjAddAvailabilityToBookingRequest))
                };
                lobjResponse = JsonConvert.DeserializeObject<AddAvailabilityToBookingResponse>(lobjDataPostHelper.PostData(HolibobConstant.HolibobEndPoint, "POST", "addAvailabilityToBooking", JsonConvert.SerializeObject(lobjPostRequest)));
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("PostData - AddAvailabilityToBooking - Ex -" + ex.Message + Environment.NewLine + "Stack Trace-" + ex.StackTrace + ex.InnerException);
            }
            return lobjResponse;
        }
        public OrderStatusResponse GetOrderStatus(string pstrBookId, string pstrLeadPassengerName)
        {
            OrderStatusResponse lobjResponse = null;
            try
            {
                OrderStatusRequest lobjOrderStatusRequest = new OrderStatusRequest
                {
                    bookId = pstrBookId,
                    currency = HolibobConstant.HolibobCurrency,
                    input = new OrderStatusInput()
                    {
                        leadPassengerName = pstrLeadPassengerName
                    }
                };
                PostRequest lobjPostRequest = new PostRequest
                {
                    vendor = HolibobConstant.HolibobVendorName,
                    query = GenerateQuery("getOrderStatus", "request", JsonConvert.SerializeObject(lobjOrderStatusRequest))
                };
                lobjResponse = JsonConvert.DeserializeObject<OrderStatusResponse>(lobjDataPostHelper.PostData(HolibobConstant.HolibobEndPoint, "POST", "getOrderStatus", JsonConvert.SerializeObject(lobjPostRequest)));
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("PostData - GetOrderStatus - Ex -" + ex.Message + Environment.NewLine + "Stack Trace-" + ex.StackTrace + ex.InnerException);
            }
            return lobjResponse;
        }
        public OrderStatusResponse GetOrderStatusByBookingId(string pstrBookId)
        {
            OrderStatusResponse lobjResponse = null;
            try
            {
                FetchBookingDetailsRequest lobjFetchBookingDetailsRequest = new FetchBookingDetailsRequest
                {
                    bookId = pstrBookId,
                    currency = HolibobConstant.HolibobCurrency
                };
                PostRequest lobjPostRequest = new PostRequest
                {
                    vendor = HolibobConstant.HolibobVendorName,
                    query = GenerateQuery("getOrderStatus", "request", JsonConvert.SerializeObject(lobjFetchBookingDetailsRequest))
                };
                lobjResponse = JsonConvert.DeserializeObject<OrderStatusResponse>(lobjDataPostHelper.PostData(HolibobConstant.HolibobEndPoint, "POST", "getOrderStatus", JsonConvert.SerializeObject(lobjPostRequest)));
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("PostData - GetOrderStatusByBookingId - Ex -" + ex.Message + Environment.NewLine + "Stack Trace-" + ex.StackTrace + ex.InnerException);
            }
            return lobjResponse;
        }
        public OrderStatusResponse SubmitBookingAnswer(string pstrBookId, List<ExperienceBookingAnswerList> plstobjAnswerList)
        {
            OrderStatusResponse lobjResponse = null;
            try
            {
                ExperienceBookingAnswerRequest lobjExperienceBookingAnswerRequest = new ExperienceBookingAnswerRequest
                {
                    bookId = pstrBookId,
                    input = new ExperienceBookingInput()
                    {
                        answerList = plstobjAnswerList
                    }
                };
                PostRequest lobjPostRequest = new PostRequest
                {
                    vendor = HolibobConstant.HolibobVendorName,
                    query = GenerateQuery("getOrderStatus", "request", JsonConvert.SerializeObject(lobjExperienceBookingAnswerRequest))
                };
                lobjResponse = JsonConvert.DeserializeObject<OrderStatusResponse>(lobjDataPostHelper.PostData(HolibobConstant.HolibobEndPoint, "POST", "getOrderStatus", JsonConvert.SerializeObject(lobjPostRequest)));
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("PostData - SubmitBookingAnswer - Ex -" + ex.Message + Environment.NewLine + "Stack Trace-" + ex.StackTrace + ex.InnerException);
            }
            return lobjResponse;
        }
        public PlaceOrderResponse PlaceOrder(string pstrBookId)
        {
            PlaceOrderResponse lobjResponse = null;
            try
            {
                PlaceOrderRequest lobjPlaceOrderRequest = new PlaceOrderRequest
                {
                    currency = HolibobConstant.HolibobCurrency,
                    metadatas = new PlaceOrderMetadatas
                    {
                        book_id = pstrBookId
                    }
                };
                PostRequest lobjPostRequest = new PostRequest
                {
                    vendor = HolibobConstant.HolibobVendorName,
                    query = GenerateQuery("placeOrder", "request", JsonConvert.SerializeObject(lobjPlaceOrderRequest))
                };
                lobjResponse = JsonConvert.DeserializeObject<PlaceOrderResponse>(lobjDataPostHelper.PostData(HolibobConstant.HolibobEndPoint, "POST", "placeOrder", JsonConvert.SerializeObject(lobjPostRequest)));
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("PostData - PlaceOrder - Ex -" + ex.Message + Environment.NewLine + "Stack Trace-" + ex.StackTrace + ex.InnerException);
            }
            return lobjResponse;
        }
        public ProductSearchResponse GetSearchList(string pstrSearchText)
        {
            ProductSearchResponse lobjResponse = null;
            try
            {
                ProductSearchRequest lobjProductSearchRequest = new ProductSearchRequest
                {
                    search_query = pstrSearchText
                };
                PostRequest lobjPostRequest = new PostRequest
                {
                    vendor = HolibobConstant.HolibobVendorName,
                    query = GenerateQuery("getSearchList", "request", JsonConvert.SerializeObject(lobjProductSearchRequest))
                };
                lobjResponse = JsonConvert.DeserializeObject<ProductSearchResponse>(lobjDataPostHelper.PostData(HolibobConstant.HolibobEndPoint, "POST", "getSearchList", JsonConvert.SerializeObject(lobjPostRequest)));
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("PostData - GetSearchList - Ex -" + ex.Message + Environment.NewLine + "Stack Trace-" + ex.StackTrace + ex.InnerException);
            }
            return lobjResponse;
        }
        public SearchProductListResponse GetExperienceProductListByPlaceId(string pstrPlaceId, bool pblnIsPrivate, bool pblnIsNew,
            string pstrIsRecommended, string pstrGuidePrice, string pstrSearch, List<string> plstCategoryIds, List<string> plstAttributeIds)
        {
            SearchProductListResponse lobjResponse = null;
            try
            {
                SearchProductListRequest lobjSearchProductListRequest = new SearchProductListRequest
                {
                    placeId = pstrPlaceId,
                    isNew = pblnIsNew,
                    isPrivate = pblnIsPrivate,
                    isRecommended = pstrIsRecommended,
                    guidePrice = pstrGuidePrice,
                    categoryIds = plstCategoryIds,
                    attributeIds = plstAttributeIds,
                    search = pstrSearch,
                    currency = HolibobConstant.HolibobCurrency
                };
                PostRequest lobjPostRequest = new PostRequest
                {
                    vendor = HolibobConstant.HolibobVendorName,
                    query = GenerateQuery("getProductListByPlace", "request", JsonConvert.SerializeObject(lobjSearchProductListRequest))
                };
                lobjResponse = JsonConvert.DeserializeObject<SearchProductListResponse>(lobjDataPostHelper.PostData(HolibobConstant.HolibobEndPoint, "POST", "getProductListByPlace", JsonConvert.SerializeObject(lobjPostRequest)));
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("PostData - GetExperienceProductListByPlaceId - Ex -" + ex.Message + Environment.NewLine + "Stack Trace-" + ex.StackTrace + ex.InnerException);
            }
            return lobjResponse;
        }
    }
}
