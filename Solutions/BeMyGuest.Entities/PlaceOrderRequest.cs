namespace BeMyGuest.Entities
{
    public class PlaceOrderRequest
    {
        public string currency { get; set; }
        public PlaceOrderMetadatas metadatas { get; set; }
    }
    public class PlaceOrderMetadatas
    {
        public string book_id { get; set; }
    }
}
