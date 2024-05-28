using System.Collections.Generic;
namespace Holibob.Entities
{
    public class ProductStatusResponse
    {
        public ProductStatusResponseData data { get; set; }
        public string availabilityType { get; set; }
    }
    public class ProductStatusResponseData
    {
        public string getProductStatus { get; set; }
    }
    public class ProductStatusData
    {
        public string id { get; set; }
        public string date { get; set; }
        public bool soldOut { get; set; }
        public string guidePriceFormattedText { get; set; }
    }
    public class ProductStatus
    {
        public string status { get; set; }
        public string active { get; set; }
        public string message { get; set; }
        public List<ProductStatusData> data { get; set; }
    }
}