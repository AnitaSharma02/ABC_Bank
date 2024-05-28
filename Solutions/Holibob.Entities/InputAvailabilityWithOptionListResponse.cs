namespace Holibob.Entities
{
    public class InputAvailabilityWithOptionListResponse
    {
        public InputAvailabilityWithOptionListResponseData data { get; set; }
    }
    public class InputAvailabilityWithOptionListResponseData
    {
        public string inputAvailabilityWithOptionList { get; set; }
    }
    public class InputAvailabilityWithOptionListNode
    {
        public string id { get; set; }
        public string answerValue { get; set; }
        public string answerFormattedText { get; set; }
    }
    public class InputAvailabilityOptionList
    {
        public bool isComplete
        {
            get; set;
        }
    }
    public class InputAvailabilityWithOptionListData
    {
        public string id
        {
            get; set;
        }
    }
    public class InputAvailabilityWithOptionList
    {
        public string status
        {
            get; set;
        }
    }
}