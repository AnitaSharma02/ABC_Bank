namespace Holibob.Entities
{
    public class CreateBookingResponse
    {
        public CreateBookingResponseData data { get; set; }
    }
    public class CreateBookingResponseData
    {
        public string createBooking { get; set; }
    }
    public class CreateBookingData
    {
        public string id { get; set; }
        public string state { get; set; }
        public bool isComplete { get; set; }
        public string paymentState { get; set; }
        public string status { get; set; }
    }
    public class CreateBooking
    {
        public string status { get; set; }
        public string message { get; set; }
        public CreateBookingData data { get; set; }
    }
}
