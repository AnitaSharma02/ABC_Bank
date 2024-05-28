using System.Collections.Generic;
namespace Holibob.Entities
{
    public class SearchProductListRequest
    {
        public string placeId { get; set; }
        public bool isPrivate { get; set; }
        public bool isNew { get; set; }
        public string isRecommended { get; set; }
        public string guidePrice { get; set; }
        public List<string> categoryIds { get; set; }
        public List<string> attributeIds { get; set; }
        public string search { get; set; }
        public string currency { get; set; }
    }
}
