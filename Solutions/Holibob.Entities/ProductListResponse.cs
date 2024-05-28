using System.Collections.Generic;
namespace Holibob.Entities
{
    public class ProductListResponse
    {
        public ProductListResponseData data { get; set; }
    }
    public class ProductListResponseData
    {
        public string getProductList { get; set; }
    }
    public class ProductListSupplier
    {
        public string name { get; set; }
    }
    public class ProductListHolibobGuidePrice
    {
        public string currency { get; set; }
        public float gross { get; set; }
        public string grossFormattedText { get; set; }
    }
    public class ProductListPreviewImage
    {
        public string urlSmall { get; set; }
    }
    public class ProductListCancellationPolicy
    {
        public bool hasFreeCancellation { get; set; }
    }
    public class ProductListData
    {
        public string id { get; set; }
        public string code { get; set; }
        public string name { get; set; }
        public ProductListHolibobGuidePrice holibobGuidePrice { get; set; }
        public ProductListPreviewImage previewImage { get; set; }
        public string availabilityType { get; set; }
        public string maxDuration { get; set; }
        public string minDuration { get; set; }
        public ProductListCancellationPolicy cancellationPolicy { get; set; }
        public ProductListPlace place { get; set; }
    }
    public class ProductListPlace
    {
        public string cityName { get; set; }
        public string countryName { get; set; }
    }
    public class ProductList
    {
        public string status { get; set; }
        public string message { get; set; }
        public int totalCount { get; set; }
        public int pages { get; set; }
        public int page { get; set; }
        public int limit { get; set; }
        public List<ProductListData> data { get; set; }
    }
}