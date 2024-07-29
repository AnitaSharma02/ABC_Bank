using Framework.Integrations.Hotels.Entities;

namespace IBEAPI.ClientEntities
{
    public class HotelBookRequest
    {
        public string CheckInDate { get; set; }
        public string CheckOutDate { get; set; }
        public string AdultPerRoom { get; set; }
        public string ChildrenPerRoom { get; set; }
        public Customer Customer { get; set; }
        public Hotel Hotel { get; set; }
        public string IpAddress { get; set; }
        public string MembershipReference { get; set; }
        public int NoOfRooms { get; set; }
        public double RedemptionRate { get; set; }
        public string ReferenceId { get; set; }
        public int SearchId { get; set; }
    }
}