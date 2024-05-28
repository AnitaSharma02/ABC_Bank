using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CB.IBE.DomesticFlight.Entities
{
    public class TicketDownloadRequest
    {
        public int LogId { get; set; }
        public string Token { get; set; }
        public bool isBase64 { get; set; } = false;
    }
}
