using Framework.Integrations.Hotels.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBEAPI.ClientEntities
{
    public class HotelsSearchRequest
    {
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public string AdultPerRoom { get; set; }
        public string ChildrenPerRoom { get; set; }
        public string IpAddress { get; set; }
        public string MembershipReference { get; set; }
        public int NoOfRooms { get; set; }
        public double RedemptionRate { get; set; }
        public string ReferenceId { get; set; }
        public string CountryISOCode { get; set; }
        public string Country { get; set; }
        public string CityName { get; set; }
        public string StarRating { get; set; }
        public string OrderBy { get; set; }
        public int ResultCount { get; set; }
        public int SearchId { get; set; }
    }
}
