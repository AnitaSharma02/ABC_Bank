using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBEAPI.ClientEntities
{
    public class City
    {
        public int id { get; set; }
        public string name { get; set; }
    }

    public class Coords
    {
        public double longitude { get; set; }
        public double latitude { get; set; }
    }

    public class Country
    {
        public int id { get; set; }
        public string name { get; set; }
        public string code { get; set; }
    }
    public class Bulk
    {
        public Coords coords { get; set; }
        public int id { get; set; }
        public string name { get; set; }
        public bool isAirport { get; set; }
        public bool isRailway { get; set; }
        public bool isPort { get; set; }
        public bool isBus { get; set; }
        public City city { get; set; }
        public Country country { get; set; }
        public string code { get; set; }
    }

    public class BulkResponse
    {
        public int success { get; set; }
        public List<Bulk> data { get; set; }
    }
}
