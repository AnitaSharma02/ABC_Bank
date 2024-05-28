namespace Holibob.Entities
{
    public class PlaceOrderResponse
    {
        public PlaceOrderResponseData data { get; set; }
    }
    public class PlaceOrderResponseData
    {
        public string placeOrder
        {
            get; set;
        }
    }
    public class PlaceOrderBookingCommit
    {
        public string id
        {
            get; set;
        }
    }
    public class PlaceOrderData
    {
        public PlaceOrderBookingCommit bookingCommit
        {
            get; set;
        }
    }
    public class OrigSupplierResponse
    {
        public PlaceOrderData data
        {
            get; set;
        }
    }
    public class PlaceOrderRawData
    {
        public string trackingId
        {
            get; set;
        }
    }
    public class PlaceOrder
    {
        public string status
        {
            get; set;
        }
    }
}