namespace Holibob.Entities
{
    public class AddAvailabilityToBookingRequest
    {
        public string availabilityId { get; set; }
        public string bookId { get; set; }
        public string currency { get; set; }
    }
}
