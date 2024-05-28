namespace Holibob.Entities
{
    public class AddAvailabilityToBookingResponse
    {
        public AddAvailabilityToBookingResponseData data { get; set; }
    }
    public class AddAvailabilityToBookingResponseData
    {
        public string addAvailabilityToBooking { get; set; }
    }
    public class AddAvailabilityToBookingData
    {
        public string id { get; set; }
        public bool isComplete { get; set; }
        public bool isSandboxed { get; set; }
    }
    public class AddAvailabilityToBooking
    {
        public string status { get; set; }
        public string message { get; set; }
        public AddAvailabilityToBookingData data { get; set; }
    }
}
