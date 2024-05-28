using System.Collections.Generic;
namespace Holibob.Entities
{
    public class FetchAvailabilityWithPricingCategoryRequest
    {
        public string availabilityId { get; set; }
        public string currency { get; set; }
        public FetchAvailabilityWithPricingCategoryInput input { get; set; }
    }
    public class FetchAvailabilityWithPricingCategoryOptionList
    {
        public string id { get; set; }
        public int value { get; set; }
    }
    public class FetchAvailabilityWithPricingCategoryInput
    {
        public List<FetchAvailabilityWithPricingCategoryOptionList> pricingCategoryList { get; set; }
    }
}