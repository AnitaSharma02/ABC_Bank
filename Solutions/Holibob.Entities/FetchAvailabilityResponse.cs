using System.Collections.Generic;
namespace Holibob.Entities
{
    public class FetchAvailabilityWithOptionListData
    {
        public string fetchAvailabilityWithOptionList { get; set; }
    }
    public class FetchAvailabilityResponse
    {
        public FetchAvailabilityWithOptionListData data { get; set; }
    }
    public class FetchAvailabilityAvailableOption
    {
        public string label { get; set; }
        public string value { get; set; }
    }
    public class FetchAvailabilityNode
    {
        public string id { get; set; }
        public string label { get; set; }
        public string dataType { get; set; }
        public object dataFormat { get; set; }
        public List<FetchAvailabilityAvailableOption> availableOptions { get; set; }
        public string answerValue { get; set; }
        public string answerFormattedText { get; set; }
        public int? minParticipants { get; set; }
        public int? maxParticipants { get; set; }
        public FetchAvailabilityUnitPrice unitPrice { get; set; }
        public int units { get; set; }
        public bool discountsAvailable { get; set; }
        public bool isDiscounted { get; set; }
        public FetchAvailabilityUnitPrice totalDiscountApplied { get; set; }
    }
    public class FetchAvailabilityOptionList
    {
        public bool isComplete { get; set; }
        public List<FetchAvailabilityNode> nodes { get; set; }
    }
    public class FetchAvailabilityUnitPrice
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
    public class FetchAvailabilityTotalPrice
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
    public class PricingCategoryList
    {
        public List<FetchAvailabilityNode> nodes { get; set; }
        public List<object> errors { get; set; }
        public FetchAvailabilityTotalPrice totalPrice { get; set; }
    }
    public class FetchAvailabilityData
    {
        public string id { get; set; }
        public FetchAvailabilityOptionList optionList { get; set; }
        public PricingCategoryList pricingCategoryList { get; set; }
    }
    public class FetchAvailability
    {
        public string status { get; set; }
        public string message { get; set; }
        public FetchAvailabilityData data { get; set; }
    }
}