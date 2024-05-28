using System.Collections.Generic;
namespace Holibob.Entities
{
    public class FetchAvailabilityWithOptionListRequest
    {
        public string availabilityId { get; set; }
        public string currency { get; set; }
        public OptionListInput input { get; set; }
    }
    public class OptionList
    {
        public string id { get; set; }
        public string value { get; set; }
    }
    public class OptionListInput
    {
        public List<OptionList> optionList { get; set; }
    }
}