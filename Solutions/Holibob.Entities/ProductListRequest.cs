namespace Holibob.Entities
{
    public class ProductListRequest
    {
        public string schemaName { get; set; }
        public bool readCacheFirst { get; set; }
        public int cacheExpireInSec { get; set; }
        public int page { get; set; }
        public int limit { get; set; }
        public string placeName { get; set; }
        public string isRecommended { get; set; }
        public string guidePrice { get; set; }
        public string currency { get; set; }
    }
}
