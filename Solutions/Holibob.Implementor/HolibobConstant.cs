using System;
using System.Configuration;

namespace Holibob.Implementor
{
    public class HolibobConstant
    {
        public static string HolibobEndPoint = Convert.ToString(ConfigurationSettings.AppSettings["HolibobEndPoint"]);
        public static string HolibobSchemaName = Convert.ToString(ConfigurationSettings.AppSettings["HolibobSchemaName"]);
        public static bool HolibobReadCacheFirst = Convert.ToBoolean(Convert.ToInt32(ConfigurationSettings.AppSettings["HolibobReadCacheFirst"]));
        public static int HolibobCacheExpireInSec = Convert.ToInt32(ConfigurationSettings.AppSettings["HolibobCacheExpireInSec"]);
        public static string HolibobVendorName = Convert.ToString(ConfigurationSettings.AppSettings["HolibobVendorName"]);
        public static string HolibobCurrency = Convert.ToString(ConfigurationSettings.AppSettings["HolibobCurrency"]);
    }
}
