using Framework.EnterpriseLibrary.Adapters;
using IBEAPI.ClientEntities;
using IBEAPIGateway.Model;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;

public partial class CarSearch : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["CategoryName"] = "carrental";
    }

    [WebMethod]
    public static List<string> GetPickupLocation(string prefixText)
    {
        try
        {
            List<string> lobjListOfLocations = new List<string>();
            string lstrLocation = string.Empty;
            if (!string.IsNullOrEmpty(prefixText))
            {
                lstrLocation = HttpUtility.UrlDecode(prefixText);
            }
            IBEAPIModel lobjmodel = new IBEAPIModel();
            BulkResponse lobjBulkResponse = null;
            if (HttpContext.Current.Application["CarLocations"] != null)
            {
                lobjBulkResponse = HttpContext.Current.Application["CarLocations"] as BulkResponse;
            }
            if (lobjBulkResponse != null && lobjBulkResponse.data.Count > 0)
            {
                BulkResponse lobjfilterBulkResponse = new BulkResponse();
                lobjfilterBulkResponse.data = lobjBulkResponse.data.FindAll(x => x.name.ToLower().Contains(prefixText.ToLower()));

                if (lobjfilterBulkResponse != null && lobjfilterBulkResponse.data.Count > 0)
                {
                    foreach (var location in lobjfilterBulkResponse.data)
                    {
                        lobjListOfLocations.Add(location.name + "|" + location.id);
                    }
                }
                else
                {
                    lobjListOfLocations.Add("Location not found.");
                }
            }
            else
            {
                lobjListOfLocations.Add("Location not found.");
            }
            return lobjListOfLocations;
        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("CarSearch GetPickupLocation Ex-: " + ex.Message + Environment.NewLine + "Stack Trace-" + ex.StackTrace + Environment.NewLine + "Inner Exception-:" + ex.InnerException);
            return null;
        }
    }
}