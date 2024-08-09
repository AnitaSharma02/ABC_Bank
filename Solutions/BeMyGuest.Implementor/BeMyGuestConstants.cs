using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace BeMyGuest.Implementor
{
    public class BeMyGuestConstants
    {
        public static string BeMyGuestExperienceAPIURL = Convert.ToString(ConfigurationManager.AppSettings["BeMyGuestExperienceAPIURL"]);

        public static string Search = string.Format("{0}/search", BeMyGuestExperienceAPIURL);
        public static string GetProductInfo= string.Format("{0}/getproductinfo", BeMyGuestExperienceAPIURL);
        public static string GetProductTypesPriceByDate = string.Format("{0}/getproducttypespricebydate", BeMyGuestExperienceAPIURL);
        public static string GetTypesAndCategory = string.Format("{0}/gettypesandcategory", BeMyGuestExperienceAPIURL);
        public static string ExperienceBooking = string.Format("{0}/bookings", BeMyGuestExperienceAPIURL);
    }
}
