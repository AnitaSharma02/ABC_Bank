using System.Collections.Generic;
namespace Holibob.Entities
{
    public class ProductSearchResponse
    {
        public ProductSearchResponseData data { get; set; }
    }
    public class ProductSearchResponseData
    {
        public string getSearchList { get; set; }
    }
    public class ProductSearch
    {
        public string status { get; set; }
        public string message { get; set; }
        public List<ProductSearchData> data { get; set; }
    }
    public class ProductSearchData
    {
        public string id { get; set; }
        public object subtitle { get; set; }
        public string title { get; set; }
        public string type { get; set; }
    }
}