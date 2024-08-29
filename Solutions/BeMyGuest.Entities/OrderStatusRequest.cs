namespace BeMyGuest.Entities
{
    public class OrderStatusRequest
    {
        public OrderStatusInput input { get; set; }
        public string bookId { get; set; }
        public string currency { get; set; }
    }
    public class OrderStatusInput
    {
        public string leadPassengerName { get; set; }
    }
}
