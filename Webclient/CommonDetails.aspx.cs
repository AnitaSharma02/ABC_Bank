using CB.IBE.Platform.Entities;
using Framework.EnterpriseLibrary.Adapters;
using Framework.EnterpriseLibrary.Common.SerializationHelper;
using ABC.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CommonDetails : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            LoggingAdapter.WriteLog("CommonDetails Start = " + DateTime.Now);
            string lstrBookingId = string.Empty;
            LoggingAdapter.WriteLog("CommonDetails Request count greater than zero");
            using (StreamReader sr = new StreamReader(Request.InputStream))
            {
                lstrBookingId = sr.ReadToEnd();
            }
            LoggingAdapter.WriteLog("CommonDetails Booking Id = " + lstrBookingId);
            ItineraryDetails lobjItineraryDetails = null;

            string lstrResponse = string.Empty;

            if (!string.IsNullOrEmpty(lstrBookingId))
            {
                lobjItineraryDetails = new ItineraryDetails();
                ABCModel lobjModel = new ABCModel();
                lobjItineraryDetails = lobjModel.GetFlightReceipt(Convert.ToInt32(lstrBookingId.Trim()));

                if (!string.IsNullOrEmpty(lobjItineraryDetails.MemberId))
                {
                    LoggingAdapter.WriteLog("CommonDetails Siddhartha ID not null = " + lobjItineraryDetails.MemberId);
                    lstrResponse = JSONSerialization.Serialize(lobjItineraryDetails);
                    LoggingAdapter.WriteLog("CommonDetails Response = " + lstrResponse);
                    Response.Write(lstrResponse);
                }
                else
                {
                    Response.Write(lstrResponse);
                }
            }
            else
            {
                Response.Write(lstrResponse);
            }

            LoggingAdapter.WriteLog("CommonDetails End = " + DateTime.Now);
        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("CommonDetails.aspx Pageload Exception: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
        }
    }
}