using ABC.Model;
using Core.Platform.Helper.ProgramName;
using Core.Platform.Member.Entites;
using Core.Platform.PointGateway.Service.Helper;
using Core.Platform.ProgramInterface.ClientHelper;
using Core.Platform.ProgramMaster.Entities;
using Core.WebAPI.ClientHelper;
using Framework.EnterpriseLibrary.Adapters;
using Framework.EnterpriseLibrary.CommunicationEngine.Entity;
using Framework.EnterpriseLibrary.CommunicationEngine.Helper;
using Giift.ShopGateway.Client.Entities;
using Giift.ShopGateway.Client.Helper;
using LoyaltyManagement.Request;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;
using CB.IBE.Platform.Masters.Entities;

namespace GiiftShopGateway.Model
{
    public class ShopModel
    {
        #region GiftShop Gateway
        public string GetAuthToken()
        {
            string AccessToken = string.Empty;
            try
            {
                AuthTokenResponse authTokenResponse = null;
                try
                {
                    if (HttpContext.Current.Session["AccessToken"] != null && HttpContext.Current.Session["AccessTokenTime"] != null)
                    {
                        authTokenResponse = HttpContext.Current.Session["AccessToken"] as AuthTokenResponse;
                        DateTime dtAccessTokenTime = Convert.ToDateTime(HttpContext.Current.Session["AccessTokenTime"]);
                        TimeSpan dt = DateTime.Now - dtAccessTokenTime;
                        if (dt.TotalMinutes > 25)
                            authTokenResponse = null;
                    }
                }
                catch { }
                if (authTokenResponse == null)
                {
                    AuthTokenRequest authTokenRequest = new AuthTokenRequest
                    {
                        client_id = ConfigurationManager.AppSettings["ClientId"],
                        client_secret = ConfigurationManager.AppSettings["ClientSecretKey"],
                        grant_type = ConfigurationManager.AppSettings["GrantType"]
                    };
                    authTokenResponse = CHelper.GetAuthToken(authTokenRequest);
                    try
                    {
                        HttpContext.Current.Session["AccessToken"] = authTokenResponse;
                        HttpContext.Current.Session["AccessTokenTime"] = DateTime.Now;
                    }
                    catch { }
                }
                AccessToken = authTokenResponse.token_type + " " + authTokenResponse.access_token;
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("ShopModel GetAuthToken Exception: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
            }
            return AccessToken;
        }
        public Store GetStoreDetails()
        {
            Store store = null;
            try
            {
                string StoreId = ConfigurationManager.AppSettings["StoreId"];
                string AccessToken = GetAuthToken();
                try
                {
                    if (HttpContext.Current.Application["Store"] != null)
                    {
                        store = HttpContext.Current.Application["Store"] as Store;
                    }
                }
                catch { }
                if (store == null)
                {
                    store = CHelper.GetStoreDetails(AccessToken, StoreId);
                    try
                    {
                        HttpContext.Current.Application["Store"] = store;
                    }
                    catch { }
                }
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("ShopModel GetStoreDetails Exception: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
            }
            return store;
        }
        public Language GetShopLanguage()
        {
            return GetStoreDetails()?.DefaultLanguage;
        }
        public Currency GetShopCurrency()
        {
            try
            {
                var store = GetStoreDetails();
                var allCurrencies = store.AvailableCurrencies;
                return allCurrencies.Find(x => x.Code.Equals(store.DefaultCurrencyCode));
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("ShopModel GetShopCurrency Exception: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
                return null;
            }
        }
        public List<Giift.ShopGateway.Client.Entities.Category> SearchCategories()
        {
            List<Giift.ShopGateway.Client.Entities.Category> lobjCategory = new List<Giift.ShopGateway.Client.Entities.Category>();
            try
            {
                if (HttpContext.Current.Application["SearchCategories"] != null)
                {
                    lobjCategory = HttpContext.Current.Application["SearchCategories"] as List<Giift.ShopGateway.Client.Entities.Category>;
                }
                else
                {
                    string AccessToken = GetAuthToken();
                    Store store = GetStoreDetails();
                    CategorySearchCriteria criteria = new CategorySearchCriteria();
                    criteria.StoreId = store.Id;
                    criteria.CatalogId = store.Catalog;
                    criteria.LanguageCode = store.DefaultLanguage.CultureName;
                    criteria.ResponseGroup = CategoryResponseGroup.Full;
                    lobjCategory = CHelper.SearchCategories(AccessToken, criteria);
                    HttpContext.Current.Application["SearchCategories"] = lobjCategory;
                }
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("ShopModel SearchCategories Exception: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
            }
            return lobjCategory;
        }
        public ProductSearchResult SearchProducts(ProductSearchCriteria productSearchCriteria)
        {
            ProductSearchResult lobjProductSearchResult = new ProductSearchResult();
            try
            {
                string AccessToken = GetAuthToken();
                lobjProductSearchResult = CHelper.SearchProducts(AccessToken, productSearchCriteria);

            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("ShopModel SearchProducts Exception: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
            }
            return lobjProductSearchResult;
        }
        public ProductSearchCriteria BuildProductSearchCriteria(string CategoryId, List<string> CategoryIds, ItemResponseGroup ResponseGroup, double LowerPrice, double UpperPrice, string SearchPhrase, string Sort, int Skip, int Take, List<string> Terms)
        {
            ProductSearchCriteria lobjProductSearchCriteria = new ProductSearchCriteria();
            try
            {
                Store store = GetStoreDetails();
                lobjProductSearchCriteria.CatalogId = store.Catalog;
                lobjProductSearchCriteria.Currency = store.DefaultCurrencyCode;
                lobjProductSearchCriteria.LanguageCode = store.DefaultLanguage.CultureName;
                NumericRange priceRange = null;
                if (LowerPrice > 0 || UpperPrice > 0)
                {
                    priceRange = new NumericRange
                    {
                        Lower = LowerPrice > 0 ? LowerPrice : 0,
                        Upper = UpperPrice > 0 ? UpperPrice : 0,
                        IncludeLower = LowerPrice > 0 ? true : false,
                        IncludeUpper = UpperPrice > 0 ? true : false
                    };
                }
                lobjProductSearchCriteria.Outline = CategoryId;
                lobjProductSearchCriteria.PriceRange = priceRange;
                lobjProductSearchCriteria.ResponseGroup = ResponseGroup;
                lobjProductSearchCriteria.SearchPhrase = SearchPhrase;
                lobjProductSearchCriteria.Skip = Skip;
                lobjProductSearchCriteria.Sort = Sort;
                lobjProductSearchCriteria.StoreId = store.Id;
                lobjProductSearchCriteria.Take = Take;
                lobjProductSearchCriteria.Terms = Terms;
                lobjProductSearchCriteria.SortSearchPhaseResponse = !string.IsNullOrEmpty(SearchPhrase);
            }
            catch (Exception ex) { LoggingAdapter.WriteLog("ShopModel BuildProductSearchCriteria Exception: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace); }
            return lobjProductSearchCriteria;
        }
        public Product GetProductById(string id)
        {
            try
            {
                Store store = GetStoreDetails();
                string AccessToken = GetAuthToken();
                return CHelper.GetProductById(AccessToken, id, store.Id, store.Catalog, store.DefaultLanguage.CultureName, store.DefaultCurrencyCode);
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("ShopModel GetProductById Exception: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
                return null;
            }
        }
        public List<Product> GetProductByPlentyId(List<string> ids)
        {
            try
            {
                Store store = GetStoreDetails();
                ProductDetailsRequest productDetailsRequest = new ProductDetailsRequest
                {
                    CatalogId = store.Catalog,
                    CurrencyCode = store.DefaultCurrencyCode,
                    Ids = ids,
                    LanguageCode = store.DefaultLanguage.CultureName,
                    ResponseGroup = ItemResponseGroup.ItemLarge,
                    StoreId = store.Id
                };
                string AccessToken = GetAuthToken();
                return CHelper.GetProductByPlentyId(AccessToken, productDetailsRequest);
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("ShopModel GetProductByPlentyId Exception: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
                return null;
            }
        }
        public List<MenuLinkList> GetMenuLinkList()
        {
            try
            {
                Store store = GetStoreDetails();
                string AccessToken = GetAuthToken();
                return CHelper.GetMenuLinkList(AccessToken, store.Id, store.DefaultLanguage.CultureName);
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("ShopModel GetMenuLinkList Exception: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
                return null;
            }
        }
        public ShoppingCart LoadOrCreateNewTransientCart(MemberDetails memberDetails)
        {
            ShoppingCart cart = null;
            try
            {
                if (memberDetails != null)
                {
                    Store store = GetStoreDetails();
                    string AccessToken = GetAuthToken();
                    CartSearchCriteria cartSearchCriteria = new CartSearchCriteria
                    {
                        StoreId = store.Id,
                        CustomerId = memberDetails?.MemberRelationsList.Find(x => x.RelationType.Equals(RelationType.LBMS)).RelationReference ?? Convert.ToString(Guid.NewGuid()),
                        Name = "default",
                        CurrencyCode = store.DefaultCurrencyCode
                    };
                    cart = CHelper.SearchCart(AccessToken, cartSearchCriteria);
                }
            }
            catch (Exception ex) { LoggingAdapter.WriteLog("ShopModel LoadOrCreateNewTransientCart Exception: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace); return null; }
            return cart;
        }
        public ShoppingCart UpdateItemQuantity(ShoppingCart Cart, string ProductId, int Quantity, string UserInputMetas)
        {
            try
            {
                Cart = AddOrUpdateItemInCart(Cart.Id, ProductId, Quantity, UserInputMetas);
            }
            catch (Exception ex) { LoggingAdapter.WriteLog("ShopModel UpdateItemQuantity Exception: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace); return null; }
            return Cart;
        }
        public ShoppingCart RemoveItemFromCart(ShoppingCart Cart, string ProductId)
        {
            try
            {
                Cart = AddOrUpdateItemInCart(Cart.Id, ProductId, 0, string.Empty);
            }
            catch (Exception ex) { LoggingAdapter.WriteLog("ShopModel RemoveItemFromCart Exception: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace); return null; }
            return Cart;
        }
        public ShoppingCart AddOrUpdateItemInCart(string CartId, string ProductId, int Quantity, string UserInputMetas)
        {
            ShoppingCart lobjShoppingCart = new ShoppingCart();
            try
            {
                Store store = GetStoreDetails();
                UpdateCartItemRequest pobjUpdateCartItemRequest = new UpdateCartItemRequest
                {
                    CartId = CartId,
                    CatalogId = store.Catalog
                };
                List<CartItemRequest> items = new List<CartItemRequest>();
                CartItemRequest item = new CartItemRequest
                {
                    ProductId = ProductId,
                    Quantity = Quantity,
                    UserInputMetas = UserInputMetas
                };
                items.Add(item);
                pobjUpdateCartItemRequest.Items = items;
                pobjUpdateCartItemRequest.StoreId = store.Id;
                string AccessToken = GetAuthToken();
                lobjShoppingCart = CHelper.AddOrUpdateItemInCart(AccessToken, pobjUpdateCartItemRequest);
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("ShopModel AddOrUpdateItemInCart Exception: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
            }
            return lobjShoppingCart;
        }
        public List<ShippingMethod> GetAvailableShippingRates(string cartId)
        {
            try
            {
                string AccessToken = GetAuthToken();
                return CHelper.GetAvailableShippingRates(AccessToken, cartId, GetShopLanguage().CultureName, GetShopCurrency().Code);
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("ShopModel GetAvailableShippingRates Exception: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
                return null;
            }
        }
        public List<PaymentMethod> GetAvailablePaymentMethods(string cartId)
        {
            try
            {
                string AccessToken = GetAuthToken();
                return CHelper.GetAvailablePaymentMethods(AccessToken, cartId, GetShopLanguage().CultureName, GetShopCurrency().Code);
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("ShopModel GetAvailablePaymentMethods Exception: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
                return null;
            }
        }
        public ShoppingCart UpdateCartShipment(ShoppingCart Cart, ShippingMethod shippingMethod, Giift.ShopGateway.Client.Entities.Address address)
        {
            try
            {
                UpdateCartShipmentRequest updateCartShipment = new UpdateCartShipmentRequest();
                updateCartShipment.CartId = Cart.Id;
                updateCartShipment.DeliveryAddress = address;
                updateCartShipment.ShipmentMethodCode = shippingMethod.ShipmentMethodCode;
                updateCartShipment.OptionName = shippingMethod.OptionName;
                updateCartShipment.Price = shippingMethod.Price.Amount;
                updateCartShipment.DiscountAmount = shippingMethod.DiscountAmount.Amount;
                updateCartShipment.TaxType = shippingMethod.TaxType;
                updateCartShipment.TaxPercentRate = shippingMethod.TaxPercentRate;
                updateCartShipment.CurrencyCode = Cart.Currency.Code;
                string AccessToken = GetAuthToken();
                Cart = CHelper.UpdateCartShipment(AccessToken, updateCartShipment);
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("ShopModel UpdateCartShipment Exception: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
            }
            return Cart;
        }

        public ShoppingCart UpdateCartPayment(ShoppingCart Cart, PaymentMethod paymentMethod, string OuterId, StripePaymentDetails pobjpaymentDetails)
        {
            try
            {
                UpdateCartPaymentRequest updateCartPayment = new UpdateCartPaymentRequest
                {
                    CartId = Cart.Id,
                    PaymentDetails = new List<PaymentDetails>()
                };
                Giift.ShopGateway.Client.Entities.PaymentDetails paymentDetails = new Giift.ShopGateway.Client.Entities.PaymentDetails();

                if (pobjpaymentDetails != null)
                {
                    if (pobjpaymentDetails.PaymentType == PaymentType.CashPoints)
                    {
                        PaymentDetails paymentDetailsCard = new PaymentDetails();
                        paymentDetailsCard.Amount = Convert.ToDecimal(pobjpaymentDetails.ReqRedeemAmount);
                        paymentDetailsCard.CurrencyCode = Cart.Currency.Code;
                        paymentDetailsCard.DiscountAmount = paymentMethod.DiscountAmount.Amount;
                        paymentDetailsCard.OuterId = OuterId;
                        paymentDetailsCard.PaymentGatewayCode = paymentMethod.Code;
                        paymentDetailsCard.Price = paymentMethod.Price.Amount;
                        paymentDetailsCard.TaxPercentRate = paymentMethod.TaxPercentRate;

                        updateCartPayment.PaymentDetails.Add(paymentDetailsCard);


                        Giift.ShopGateway.Client.Entities.PaymentDetails paymentDetailsPoints = new Giift.ShopGateway.Client.Entities.PaymentDetails();
                        paymentDetailsPoints.Amount = Convert.ToDecimal(pobjpaymentDetails.ReqRedeemPoint);
                        paymentDetailsPoints.CurrencyCode = ConfigurationManager.AppSettings["VCCaptureCurrencry"];
                        paymentDetailsPoints.DiscountAmount = paymentMethod.DiscountAmount.Amount;
                        paymentDetailsPoints.OuterId = OuterId;
                        paymentDetailsPoints.PaymentGatewayCode = paymentMethod.Code;
                        paymentDetailsPoints.Price = paymentMethod.Price.Amount;
                        paymentDetailsPoints.TaxPercentRate = paymentMethod.TaxPercentRate;

                        // LoggingAdapter.WriteLog("Shop Model UpdateCartPayment Rquest " + JsonConvert.SerializeObject(updateCartPayment), "VCPaymentTrace");
                        updateCartPayment.PaymentDetails.Add(paymentDetailsPoints);
                        // LoggingAdapter.WriteLog("Shop Model UpdateCartPayment response " + JsonConvert.SerializeObject(Cart), "VCPaymentTrace");
                    }
                    else if (pobjpaymentDetails.PaymentType == PaymentType.Points)
                    {
                        updateCartPayment.CartId = Cart.Id;
                        paymentDetails.Amount = Convert.ToDecimal(pobjpaymentDetails.ReqRedeemPoint);
                        paymentDetails.CurrencyCode = ConfigurationManager.AppSettings["VCCaptureCurrencry"];
                        paymentDetails.DiscountAmount = paymentMethod.DiscountAmount.Amount;
                        paymentDetails.OuterId = OuterId;
                        paymentDetails.PaymentGatewayCode = paymentMethod.Code;
                        paymentDetails.Price = paymentMethod.Price.Amount;
                        paymentDetails.TaxPercentRate = paymentMethod.TaxPercentRate;
                        updateCartPayment.PaymentDetails.Add(paymentDetails);
                    }
                    else
                    {
                        updateCartPayment.CartId = Cart.Id;
                        paymentDetails.Amount = Convert.ToDecimal(pobjpaymentDetails.ReqRedeemAmount);
                        paymentDetails.CurrencyCode = Cart.Currency.Code;
                        paymentDetails.DiscountAmount = paymentMethod.DiscountAmount.Amount;
                        paymentDetails.OuterId = OuterId;
                        paymentDetails.PaymentGatewayCode = paymentMethod.Code;
                        paymentDetails.Price = paymentMethod.Price.Amount;
                        paymentDetails.TaxPercentRate = paymentMethod.TaxPercentRate;
                        updateCartPayment.PaymentDetails.Add(paymentDetails);
                    }
                }

                //PaymentDetails paymentDetails = new PaymentDetails
                //{
                //    Amount = Cart.Price.Total.Amount,
                //    CurrencyCode = Cart.Currency.Code,
                //    DiscountAmount = paymentMethod.DiscountAmount.Amount,
                //    OuterId = OuterId,
                //    PaymentGatewayCode = paymentMethod.Code,
                //    Price = paymentMethod.Price.Amount,
                //    TaxPercentRate = paymentMethod.TaxPercentRate
                //};
                //updateCartPayment.PaymentDetails.Add(paymentDetails);
                string AccessToken = GetAuthToken();
                Cart = CHelper.UpdateCartPayment(AccessToken, updateCartPayment);
            }
            catch (Exception ex) { LoggingAdapter.WriteLog("ShopModel UpdateCartPayment Exception: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace); }
            return Cart;
        }
        public bool DeleteCart(string cartId)
        {
            try
            {
                string AccessToken = GetAuthToken();
                return CHelper.DeleteCart(AccessToken, cartId);
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("ShopModel DeleteCart Exception: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
                return false;
            }
        }
        public CustomerOrder CreateOrderFromCart(ShoppingCart Cart,StripePaymentDetails pobjpaymentDetails)
        {
            CustomerOrder order = null;
            try
            {
                string AccessToken = GetAuthToken();
                order = CHelper.CreateOrderFromCart(AccessToken, Cart.Id);
                if (order != null)
                {
                    LoggingAdapter.WriteLog("CreateOrderFromCart response Order " + JsonConvert.SerializeObject(order), "VCPaymentTrace");
                    UpdateCustomerOrder(order, "Placed", pobjpaymentDetails);
                }
                else
                {
                    LoggingAdapter.WriteLog("CreateOrderFromCart Order Null", "VCPaymentTrace");
                }
                //bool lblnResult = UpdateCustomerOrder(order, "Placed");
                //LoggingAdapter.WriteLog("ShopModel CreateOrderFromCart lblnResult: " + lblnResult);
            }
            catch (Exception ex) { LoggingAdapter.WriteLog("ShopModel CreateOrderFromCart Exception: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace); }
            return order;
        }
        public ProcessPaymentResult ProcessorderPayment(string orderId, string paymentId)
        {
            try
            {
                string AccessToken = GetAuthToken();
                return CHelper.ProcessorderPayment(AccessToken, orderId, paymentId);
            }
            catch (Exception ex) { LoggingAdapter.WriteLog("ShopModel ProcessorderPayment Exception: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace); return null; }
        }
        public CustomerOrder GetOrderByNumber(string orderNumber)
        {
            try
            {
                string AccessToken = GetAuthToken();
                return CHelper.GetOrderByNumber(AccessToken, orderNumber);
            }
            catch (Exception ex) { LoggingAdapter.WriteLog("ShopModel GetOrderByNumber Exception: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace); return null; }
        }
        public CustomerOrderSearchResult GetCustomerOrders(int skip, int take, string CustomerId)
        {
            try
            {
                CustomerOrderSearchResult customerOrderSearchResult = new CustomerOrderSearchResult();
                Store store = GetStoreDetails();
                string AccessToken = GetAuthToken();
                CustomerOrderSearchCriteria pobjCustomerOrderSearchCriteria = new CustomerOrderSearchCriteria();
                pobjCustomerOrderSearchCriteria.CustomerId = CustomerId;
                pobjCustomerOrderSearchCriteria.StoreId = store.Id;
                pobjCustomerOrderSearchCriteria.Skip = skip;
                pobjCustomerOrderSearchCriteria.Take = take;
                customerOrderSearchResult = CHelper.GetCustomerOrders(AccessToken, pobjCustomerOrderSearchCriteria);
                LoggingAdapter.WriteLog("Orders History Request" + JsonConvert.SerializeObject(pobjCustomerOrderSearchCriteria));
                LoggingAdapter.WriteLog("Orders History " + JsonConvert.SerializeObject(customerOrderSearchResult));
                return customerOrderSearchResult;
            }
            catch (Exception ex) { LoggingAdapter.WriteLog("ShopModel GetCustomerOrders Exception: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace); return null; }
        }
        public CustomerOrderSearchResult GetCustomerOrders(string CustomerId)
        {
            try
            {
                CustomerOrderSearchCriteria lobjcustomerOrderSearchCriteria = new CustomerOrderSearchCriteria();
                Store store = GetStoreDetails();
                string AccessToken = GetAuthToken();
                lobjcustomerOrderSearchCriteria.CustomerId = CustomerId;
                lobjcustomerOrderSearchCriteria.StoreId = store.Id;
                return CHelper.GetCustomerOrders(AccessToken, lobjcustomerOrderSearchCriteria);
            }
            catch (Exception ex) { LoggingAdapter.WriteLog("ShopModel GetCustomerOrders Exception: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace); return null; }
        }
        public CustomerReviewSearchResult GetCustomerReviews(string ProductId, string Sort, int Skip, int Take)
        {
            try
            {
                string AccessToken = GetAuthToken();
                Store store = GetStoreDetails();
                CustomerReviewSearchCriteria reviewSearchCriteria = new CustomerReviewSearchCriteria
                {
                    LanguageCode = store.DefaultLanguage.CultureName,
                    ProductId = ProductId,
                    Skip = Skip,
                    Take = Take,
                    Sort = Sort,
                    StoreId = store.Id
                };
                return CHelper.GetCustomerReviews(AccessToken, reviewSearchCriteria);
            }
            catch (Exception ex) { LoggingAdapter.WriteLog("ShopModel GetCustomerReviews Exception: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace); return null; }
        }
        public bool AddCustomerReview(string ProductId, int Rating, string Title, string Review, string UserName)
        {
            try
            {
                string AccessToken = GetAuthToken();
                Store store = GetStoreDetails();
                List<CustomerReview> reviews = new List<CustomerReview>();
                CustomerReview review = new CustomerReview
                {
                    CreatedDate = DateTime.Now,
                    ProductId = ProductId,
                    Rating = Rating,
                    Review = Review,
                    StoreId = store.Id,
                    Title = Title,
                    UserName = UserName
                };
                return CHelper.AddCustomerReview(AccessToken, review);
            }
            catch (Exception ex) { LoggingAdapter.WriteLog("ShopModel AddCustomerReview Exception: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace); return false; }
        }
        public bool UpdateCustomerOrder(CustomerOrder order, string Status, StripePaymentDetails pobjpaymentDetails)
        {
            LoggingAdapter.WriteLog("Shop Model UpdateCustomerOrder Status: " + Status + " Requested order " + JsonConvert.SerializeObject(order) + " Rquested PaymentDetails " + JsonConvert.SerializeObject(pobjpaymentDetails), "VCPaymentTrace");
            try
            {
                string AccessToken = GetAuthToken();
                UpdateOrderRequest updateOrderRequest = new UpdateOrderRequest();
                updateOrderRequest.OrderNumber = order.Number;
                updateOrderRequest.Status = Status;
                updateOrderRequest.NotificationUrl = Convert.ToString(ConfigurationManager.AppSettings["OrderNotificationUrl"]);

                List<OrderPayment> payments = new List<OrderPayment>();
                foreach (var payment in order.InPayments)
                {                   
                    if (pobjpaymentDetails != null)
                    {
                        if (pobjpaymentDetails.PaymentType == PaymentType.CashPoints)
                        {
                            List<OrderPayment> orderPayments = new List<OrderPayment>();
                            OrderPayment orderPaymentCards = new OrderPayment();
                            orderPaymentCards.PaymentSource = pobjpaymentDetails.PaymentType.ToString();
                            CardDetails cardDetails = new CardDetails();
                            cardDetails.CardType = pobjpaymentDetails.PaymentType.ToString();
                            cardDetails.NameOnCard = pobjpaymentDetails.ReqRedeemAmount;
                            orderPaymentCards.CardDetails = cardDetails;
                            orderPaymentCards.OuterId = pobjpaymentDetails.orderId;
                            orderPaymentCards.PaymentStatus = Status.Equals("Placed") ? "Paid" : "Failed";
                            orderPayments.Add(orderPaymentCards);
                            //payments.Add(orderPaymentCards);
                           

                            OrderPayment orderPaymentCardsNew = new OrderPayment();
                            orderPaymentCardsNew.PaymentSource = "Points";
                            CardDetails cardDetailsw = new CardDetails();
                            cardDetailsw.CardType = "Points";
                            cardDetailsw.NameOnCard = pobjpaymentDetails.ReqRedeemPoint;
                            orderPaymentCardsNew.CardDetails = cardDetailsw;
                            orderPaymentCardsNew.OuterId = pobjpaymentDetails.orderId;
                            orderPaymentCardsNew.PaymentStatus = Status.Equals("Placed") ? "Paid" : "Failed";
                            //payments.Add(orderPaymentCardsNew);
                            orderPayments.Add(orderPaymentCardsNew);

                            payments.AddRange(orderPayments);

                        }
                        else if (pobjpaymentDetails.PaymentType == PaymentType.Points)
                        {
                            OrderPayment orderPaymentCardsNew = new OrderPayment();
                            orderPaymentCardsNew.PaymentSource = "Points";
                            CardDetails cardDetailsw = new CardDetails();
                            cardDetailsw.CardType = "Points";
                            cardDetailsw.NameOnCard = pobjpaymentDetails.ReqRedeemPoint;
                            orderPaymentCardsNew.CardDetails = cardDetailsw;
                            orderPaymentCardsNew.OuterId = payment.OuterId;
                            orderPaymentCardsNew.PaymentStatus = Status.Equals("Placed") ? "Paid" : "Failed";
                            payments.Add(orderPaymentCardsNew);
                        }
                        else if(pobjpaymentDetails.PaymentType == PaymentType.Cash)
                        {
                            OrderPayment orderPaymentCards = new OrderPayment();
                            orderPaymentCards.PaymentSource = pobjpaymentDetails.PaymentType.ToString();
                            CardDetails cardDetails = new CardDetails();
                            cardDetails.CardType = pobjpaymentDetails.PaymentType.ToString();
                            cardDetails.NameOnCard = pobjpaymentDetails.ReqRedeemAmount;
                            orderPaymentCards.CardDetails = cardDetails;
                            orderPaymentCards.OuterId = pobjpaymentDetails.orderId;
                            orderPaymentCards.PaymentStatus = Status.Equals("Placed") ? "Paid" : "Failed";
                            payments.Add(orderPaymentCards);
                        }
                    }
                    //OrderPayment orderPayment = new OrderPayment();
                    //orderPayment.OuterId = payment.OuterId;
                    //orderPayment.PaymentSource = "Points";
                    //orderPayment.PaymentStatus = Status.Equals("Placed") ? "Paid" : "Failed";
                    //payments.Add(orderPayment);
                }
                updateOrderRequest.Payments = payments;
                return CHelper.UpdateCustomerOrder(AccessToken, updateOrderRequest);
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("UpdateCustomerOrder Shop Model -" + ex.Message + Environment.NewLine + "Stack Trace-" + ex.StackTrace);
                return false;
            }
        }
        public void SendEmail(List<string> pstrEmailparameter, MemberDetails pobjMemberDetails, string pstrTemplateCode)
        {
            try
            {
                EmailDetails lobjEmailDetail = new EmailDetails();
                List<string> lstAttachment = new List<string>();
                ABCModel lobjVerveModel = new ABCModel();
                string lstrToken = lobjVerveModel.GetAuthTokenforWebAPI();
                List<Attachments> lstAttachments = new List<Attachments>();
                APIClientHelper lobjcehelper = new APIClientHelper();
                lobjEmailDetail.TemplateCode = pstrTemplateCode;
                lobjEmailDetail.ListParameter = pstrEmailparameter;
                lobjEmailDetail.ProgramId = Convert.ToString(pobjMemberDetails.ProgramId);
                lobjEmailDetail.MemberId = Convert.ToString(pobjMemberDetails.MemberRelationsList.Find(lobj => lobj.RelationType.Equals(RelationType.LBMS)).RelationReference);
                lobjEmailDetail.AttachmentList = lstAttachment;
                lobjEmailDetail.To = pobjMemberDetails.Email;
                bool lblnResult = lobjcehelper.InsertEmailDetails(lobjEmailDetail, lstAttachments, lstrToken);
                LoggingAdapter.WriteLog("ShopModel SendEmail Exception: " + lblnResult);
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("Model SendEmail Ex-" + ex.InnerException + ex.StackTrace + ex.Message);
            }
        }
        public ShoppingCart ExpressCheckout(string ProductId, int Quantity, MemberDetails memberDetails, string pstrUserInputMetas)
        {
            ShoppingCart Cart = new ShoppingCart();
            try
            {
                Store store = GetStoreDetails();
                ExpressCheckoutRequest expressCheckout = new ExpressCheckoutRequest
                {
                    CurrencyCode = store.DefaultCurrencyCode,
                    CustomerId = memberDetails.MemberRelationsList.Find(x => x.RelationType.Equals(RelationType.LBMS)).RelationReference,
                    CustomerName = memberDetails.LastName,
                    LanguageCode = store.DefaultLanguage.CultureName,
                    ProductId = ProductId,
                    Quantity = Quantity,
                    StoreId = store.Id,
                    UserInputMetas = pstrUserInputMetas
                };
                string AccessToken = GetAuthToken();
                Cart = CHelper.ExpressCheckout(AccessToken, expressCheckout);
            }
            catch (Exception ex) { LoggingAdapter.WriteLog("ShopModel ExpressCheckout Exception: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace); }
            return Cart;
        }
        public StoreAssets GetStoreAssets(string pstrPath)
        {
            StoreAssets storeAssets = null;
            try
            {
                string StoreId = ConfigurationManager.AppSettings["StoreId"];
                string AccessToken = GetAuthToken();
                storeAssets = CHelper.GetStoreAssets(AccessToken, StoreId, pstrPath);
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("ShopModel GetStoreAssets Exception: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
            }
            return storeAssets;
        }
        #endregion
        public int CheckAvailbility(string pstrRelationReference, int pintRelationType, string pstrCurrency, int pintProgramId)
        {
            PGAvailabilityRequest lobjPGRequest = new PGAvailabilityRequest();
            try
            {
                ABCModel lobjDBSModel = new ABCModel();
                string lstrToken = lobjDBSModel.GetAuthTokenforWebAPI();

                lobjPGRequest.ProgramId = pintProgramId;
                lobjPGRequest.RelationReference = pstrRelationReference;
                lobjPGRequest.RelationType = pintRelationType;
                lobjPGRequest.TransactionCurrency = pstrCurrency;
                APIClientHelper lobjAPIClientHelper = new APIClientHelper();
                return lobjAPIClientHelper.CheckAvailability(lobjPGRequest, lstrToken);
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("ShopModel CheckAvailbility -" + ex.Message + Environment.NewLine + "Stack Trace-" + ex.StackTrace);
                return 0;
            }
        }
        public bool RollBackTransaction(string pstrExternalReference, string pstrRelationReference, string pstrMerchantName)
        {
            bool lboolRollBackResponse = false;
            try
            {
                ABCModel lobjDBSModel = new ABCModel();
                string lstrToken = lobjDBSModel.GetAuthTokenforWebAPI();
                APIClientHelper lobjAPIClientHelper = new APIClientHelper();
                PGReversalRequest lobjPGReversalRequest = new PGReversalRequest();
                lobjPGReversalRequest.ExternalReference = pstrExternalReference;
                lobjPGReversalRequest.RelationReference = pstrRelationReference;
                lobjPGReversalRequest.MerchantName = pstrMerchantName;
                lboolRollBackResponse = lobjAPIClientHelper.ReversalPoints(lobjPGReversalRequest, lstrToken);
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("ShopModel RollBackTransaction -" + ex.Message + Environment.NewLine + "Stack Trace-" + ex.StackTrace);
            }
            return lboolRollBackResponse;
        }
        public string RedeemPoints(float pfltAmount, int pintPoints, string pstrRelationReference, string pstrPassword, string pstrNarration, int pintLoyaltyTxnType, string pstrProgramCurrency, string pstrMerchectId)
        {
            string strRedeemMilesResponse = string.Empty;
            try
            {
                ABCModel lobjDBSModel = new ABCModel();
                string lstrToken = lobjDBSModel.GetAuthTokenforWebAPI();
                ProgramDefinition lobjProgramMaster = GetProgramMaster();
                PGRedeemRequest lobjPGRedeemRequest = new PGRedeemRequest
                {
                    RelationReference = pstrRelationReference,
                    Amount = Convert.ToDecimal(pfltAmount),
                    Points = pintPoints,
                    TransactionCurrency = pstrProgramCurrency,
                    LoyaltyTxnType = pintLoyaltyTxnType,
                    MerchantName = pstrNarration,
                    MerchantId = pstrMerchectId,
                    ProgramId = lobjProgramMaster.ProgramId,
                    RelationType = Convert.ToInt32(RelationType.LBMS)

                };
                APIClientHelper lobjAPIClientHelper = new APIClientHelper();
                strRedeemMilesResponse = lobjAPIClientHelper.RedeemPoints(lobjPGRedeemRequest, lstrToken);
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("ShopModel RedeemPoints Exception: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
            }
            return strRedeemMilesResponse;
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
                    HttpContext.Current.Application["DefaultCurrency"] = lstrCurrency;
                }
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("ShopModel GetDefaultCurrency Exception: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
            }
            return lstrCurrency;
        }
        public ProgramDefinition GetProgramMaster()
        {
            ProgramDefinition lobjProgramMaster = null;
            try
            {
                string lstrProgramName = string.Empty;
                lstrProgramName = ConfigurationManager.AppSettings["ProgramName"].ToString();
                try
                {
                    lobjProgramMaster = GetProgramDetails(lstrProgramName);
                }
                catch (Exception ex)
                {
                    LoggingAdapter.WriteLog("ShopModel GetProgramDetails Exception: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
                }
            }
            catch (Exception ex) { LoggingAdapter.WriteLog("ShopModel GetProgramMaster Exception: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace); }
            return lobjProgramMaster;
        }
        public ProgramDefinition GetProgramDetails(string pstrProgramName)
        {
            ProgramDefinition lobjProgramDefination = null;
            try
            {
                if (HttpContext.Current.Application["ProgramMaster"] != null)
                {
                    lobjProgramDefination = HttpContext.Current.Application["ProgramMaster"] as ProgramDefinition;
                }
                else
                {
                    APIClientHelper lobjAPIClientHelper = new APIClientHelper();
                    lobjProgramDefination = lobjAPIClientHelper.GetProgramDefinition(pstrProgramName, string.Empty);
                }
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("ShopModel GetProgramDetails :" + ex.Message + Environment.NewLine + "Stack Trace :" + ex.StackTrace);
            }
            return lobjProgramDefination;
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
                    APIClientHelper lobjAPIClientHelper = new APIClientHelper();
                    lobjListOfProgramCurrencyDefinition = lobjAPIClientHelper.GetProgramCurrencyDefinitionList(pintProgramId, string.Empty);
                    HttpContext.Current.Application["ProgramCurrency"] = lobjListOfProgramCurrencyDefinition;
                }
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("ShopModel GetProgramCurrencyDefinition :" + ex.Message + Environment.NewLine + "Stack Trace :" + ex.StackTrace);
            }
            return lobjListOfProgramCurrencyDefinition;
        }       
    }
}
