using System.Collections.Generic;
namespace Holibob.Entities
{
    public class SearchProductListResponse
    {
        public SearchProductListResponseData data { get; set; }
    }
    public class SearchProductListResponseData
    {
        public string getProductListByPlace { get; set; }
    }
    public class ProductListByPlace
    {
        public string status { get; set; }
        public string message { get; set; }
        public List<SearchProductListByPlaceData> data { get; set; }
        public List<SearchProductListCategoryTree> categoryTree { get; set; }
        public List<SearchProductListAttributeTree> attributeTree { get; set; }
    }
    public class SearchProductListCategoryTree
    {
        public string label { get; set; }
        public string type { get; set; }
        public bool isSelected { get; set; }
        public List<SearchProductListBranch> branches { get; set; }
        public int selectedCount { get; set; }
    }
    public class SearchProductListAttributeTree
    {
        public string label { get; set; }
        public string type { get; set; }
        public bool isSelected { get; set; }
        public List<SearchProductListBranch> branches { get; set; }
        public int selectedCount { get; set; }
    }
    public class SearchProductListBranch
    {
        public string label { get; set; }
        public string type { get; set; }
        public bool isSelected { get; set; }
        public List<SearchProductListBranch> branches { get; set; }
        public int selectedCount { get; set; }
        public string id { get; set; }
        public int? count { get; set; }
    }
    public class SearchProductListPlace
    {
        public string cityName { get; set; }
        public string countryName { get; set; }
    }
    public class SearchProductListNode
    {
        public string id { get; set; }
        public string name { get; set; }
        public string level1 { get; set; }
        public string level2 { get; set; }
    }
    public class SearchProductListAttributeList
    {
        public List<SearchProductListNode> nodes { get; set; }
    }
    public class SearchProductListCategoryList
    {
        public List<SearchProductListNode> nodes { get; set; }
    }
    public class SearchProductListPreviewImage
    {
        public string id { get; set; }
        public string urlTiny { get; set; }
        public string urlSmall { get; set; }
        public string urlMedium { get; set; }
        public string urlLarge { get; set; }
        public string url { get; set; }
    }
    public class SearchProductListByPlaceData
    {
        public string id { get; set; }
        public string name { get; set; }
        public SearchProductListCategoryList categoryList { get; set; }
        public SearchProductListAttributeList attributeList { get; set; }
        public SearchProductListPlace place { get; set; }
        public string description { get; set; }
        public float guidePriceAmount { get; set; }
        public string guidePriceCurrency { get; set; }
        public string guidePriceFormattedText { get; set; }
        public string defaultImageId { get; set; }
        public SearchProductListPreviewImage previewImage { get; set; }
    }
}