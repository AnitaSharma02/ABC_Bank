using Newtonsoft.Json;
using System.Collections.Generic;
namespace Holibob.Entities
{
    public class ProductInfoResponse
    {
        public ProductInfoResponseData data { get; set; }
    }
    public class ProductInfoResponseData
    {
        public string getProductInfo { get; set; }
    }
    public class ProductInfoMessage
    {
        [JsonProperty("en-us")]
        public string EnUs { get; set; }
    }
    public class ProductInfoNode
    {
        public string type { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string formattedText { get; set; }
        public string id { get; set; }
        public int ordinalPosition { get; set; }
        public string productId { get; set; }
        public int refundPercentage { get; set; }
        public string relativeTo { get; set; }
        public string startTime { get; set; }
        public string level1 { get; set; }
        public object nameMapping { get; set; }
        public ProductInfoNameTranslationList nameTranslationList { get; set; }
    }
    public class ProductInfoContentList
    {
        public List<ProductInfoNode> nodes { get; set; }
    }
    public class ProductInfoPenaltyList
    {
        public List<ProductInfoNode> nodes { get; set; }
    }
    public class ProductInfoCancellationPolicy
    {
        public bool hasFreeCancellation { get; set; }
        public bool isCancellable { get; set; }
        public ProductInfoPenaltyList penaltyList { get; set; }
    }
    public class ProductInfoStartTimeList
    {
        public List<StartTimeListNode> nodes { get; set; }
    }
    public class StartTimeListNode
    {
        public string startTime { get; set; }
        public string duration { get; set; }
        public string pickupTime { get; set; }
    }
    public class ProductInfoPreviewImage
    {
        public string urlTiny { get; set; }
        public string urlSmall { get; set; }
        public string urlMedium { get; set; }
        public string urlLarge { get; set; }
        public string url { get; set; }
        public string id { get; set; }
    }
    public class ProductInfoImageList
    {
        public string id { get; set; }
        public string urlTiny { get; set; }
        public string urlSmall { get; set; }
        public string urlMedium { get; set; }
        public string urlLarge { get; set; }
        public string url { get; set; }
    }
    public class ProductInfoHolibobGuidePrice
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
    public class ProductInfoNameTranslationList
    {
        public List<object> nodes { get; set; }
    }
    public class ProductInfoAttributeList
    {
        public List<ProductInfoNode> nodes { get; set; }
    }
    public class ProductInfoGuideLanguageList
    {
        public List<ProductInfoNode> nodes { get; set; }
        public int recordCount { get; set; }
    }
    public class ProductInfoProduct
    {
        public string availabilityType { get; set; }
        public string id { get; set; }
        public string code { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public ProductInfoContentList contentList { get; set; }
        public bool hasStartTime { get; set; }
        public string minDuration { get; set; }
        public ProductInfoCancellationPolicy cancellationPolicy { get; set; }
        public ProductInfoStartTimeList startTimeList { get; set; }
        public ProductInfoPreviewImage previewImage { get; set; }
        public List<ProductInfoImageList> imageList { get; set; }
        public ProductInfoHolibobGuidePrice holibobGuidePrice { get; set; }
        public string @abstract { get; set; }
        public string originGuidePrice { get; set; }
        public ProductInfoAttributeList attributeList { get; set; }
        public string difficultyLevel { get; set; }
        public ProductInfoGuideLanguageList guideLanguageList { get; set; }
        public string maxDuration { get; set; }
        public int startTimeCount { get; set; }
    }
    public class ProductInfoOrigSupplierResponse
    {
        public ProductInfoProduct product { get; set; }
        public int status { get; set; }
    }
    public class ProductInfoRawData
    {
        public ProductInfoOrigSupplierResponse origSupplierResponse { get; set; }
    }
    public class ProductInfo
    {
        public string status { get; set; }
        public string productType { get; set; }
        public string productCode { get; set; }
        public string productName { get; set; }
        public string customerId { get; set; }
        public string timeStamp { get; set; }
        public ProductInfoMessage message { get; set; }
        public ProductInfoRawData rawData { get; set; }
    }
}