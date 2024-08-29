using System.Collections.Generic;
namespace BeMyGuest.Entities
{
    public class OrderStatusResponse
    {
        public OrderStatusResponseData data { get; set; }
    }
    public class OrderStatusResponseData
    {
        public string getOrderStatus { get; set; }
    }
    public class OrderStatusAvailableOption
    {
        public string label { get; set; }
        public string value { get; set; }
    }
    public class OrderStatusQuestionList
    {
        public string properties { get; set; }
        public string id { get; set; }
        public string label { get; set; }
        public string dataFormat { get; set; }
        public string dataType { get; set; }
        public List<OrderStatusAvailableOption> availableOptions { get; set; }
        public string answerValue { get; set; }
        public string pricingCategoryId { get; set; }
        public string pricingCategoryLabel { get; set; }
        public List<OrderStatusNode> nodes { get; set; }
        public bool isRequired { get; set; }
    }
    public class OrderStatusPersonListNode
    {
        public string id { get; set; }
        public string pricingCategoryLabel { get; set; }
        public OrderStatusQuestionList questionList { get; set; }
        public float totalPrice { get; set; }
    }
    public class OrderStatusNode
    {
        public string id { get; set; }
        public string label { get; set; }
        public string dataType { get; set; }
        public string dataFormat { get; set; }
        public string answerValue { get; set; }
        public bool isRequired { get; set; }
        public string answerFormattedText { get; set; }
        public List<OrderStatusAvailableOption> availableOptions { get; set; }
        public string formattedText { get; set; }
        public bool isAnswered { get; set; }
        public string pricingCategoryLabel { get; set; }
        public bool isQuestionsComplete { get; set; }
        public OrderStatusQuestionList questionList { get; set; }
        public string date { get; set; }
        public OrderStatusProduct product { get; set; }
        public OrderStatusOptionList optionList { get; set; }
        public OrderStatusPersonList personList { get; set; }
        public string state { get; set; }
        public OrderStatusTotalPrice totalPrice { get; set; }
        public string bookingId { get; set; }
        public string code { get; set; }
    }
    public class OrderStatusPenaltyList
    {
        public List<OrderStatusNode> nodes { get; set; }
    }
    public class OrderStatusCancellationPolicy
    {
        public bool hasFreeCancellation { get; set; }
        public bool isCancellable { get; set; }
        public OrderStatusPenaltyList penaltyList { get; set; }
    }
    public class OrderStatusProduct
    {
        public string id { get; set; }
        public string name { get; set; }
        public OrderStatusCancellationPolicy cancellationPolicy { get; set; }
        public List<OrderStatusImageList> imageList { get; set; }
    }
    public class OrderStatusImageList
    {
        public string id { get; set; }
        public string urlTiny { get; set; }
        public string urlSmall { get; set; }
        public string urlMedium { get; set; }
        public string urlLarge { get; set; }
        public string url { get; set; }
    }
    public class OrderStatusOptionList
    {
        public List<OrderStatusNode> nodes { get; set; }
    }
    public class OrderStatusPersonList
    {
        public List<OrderStatusPersonListNode> nodes { get; set; }
    }
    public class OrderStatusTotalPrice
    {
        public float commission { get; set; }
        public string commissionFormattedText { get; set; }
        public string currency { get; set; }
        public float gross { get; set; }
        public string grossFormattedText { get; set; }
        public float net { get; set; }
        public string netFormattedText { get; set; }
        public object pricingData { get; set; }
    }
    public class OrderStatusAvailabilityList
    {
        public List<OrderStatusNode> nodes { get; set; }
    }
    public class OrderStatusBooking
    {
        public string id { get; set; }
        public string code { get; set; }
        public string leadPassengerName { get; set; }
        public object partnerExternalReference { get; set; }
        public string state { get; set; }
        public bool isSandboxed { get; set; }
        public string paymentState { get; set; }
        public string partnerChannelBookingUrl { get; set; }
        public OrderStatusQuestionList questionList { get; set; }
        public OrderStatusAvailabilityList availabilityList { get; set; }
        public bool canCancel { get; set; }
        public bool canCommit { get; set; }
        public object cancellationEffectiveRefundAmount { get; set; }
        public bool isComplete { get; set; }
        public bool isQuestionsComplete { get; set; }
        public object name { get; set; }
    }
    public class OrderStatusData
    {
        public OrderStatusBooking booking { get; set; }
        public string id { get; set; }
        public string code { get; set; }
        public string state { get; set; }
        public string leadPassengerName { get; set; }
        public bool isSandboxed { get; set; }
        public string partnerChannelBookingUrl { get; set; }
        public string paymentState { get; set; }
        public List<OrderStatusQuestionList> questionList { get; set; }
        public OrderStatusRawData rawData { get; set; }
    }
    public class OrderStatusRawData
    {
        public OrderStatusData data { get; set; }
    }
    public class BeMyGuestOrderStatus
    {
        public string status { get; set; }
        public string message { get; set; }
        public OrderStatusData data { get; set; }
        public string errors { get; set; }
    }
}