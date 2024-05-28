using CB.IBE.DomesticFlight.Entities;
using Core.Platform.Member.Entites;
using Framework.EnterpriseLibrary.Adapters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserControl_UCCTItinerayDetails_Domestic : System.Web.UI.UserControl
{
    FinalFlightResult lobjItineraryDetails = new FinalFlightResult();
    
    CreateDomesticBookingResponse lobjCreateDomesticBookingResponse = new CreateDomesticBookingResponse();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if(Session["FinalBookedFlightDetails"] != null)
            {
                lobjItineraryDetails = (FinalFlightResult)Session["FinalBookedFlightDetails"];
                lobjCreateDomesticBookingResponse = Session["DomesticFlightBookingResponse"] as CreateDomesticBookingResponse;
                List<FlightDetails> SelectedFlightsOutbound = (List<FlightDetails>)Session["SelectedFlightsDetailsOutbound"];
                List<FlightDetails> SelectedFlightsInbound = (List<FlightDetails>)Session["SelectedFlightsDetailsInbound"];
                if (lobjItineraryDetails != null && SelectedFlightsOutbound[0] != null)
                {
                    if (Session["MemberDetails"] != null)
                    {
                        MemberDetails lobjMemberDetails = Session["MemberDetails"] as MemberDetails;
                    }
                    lblOutboundPNR.Text = lobjCreateDomesticBookingResponse.Outbound.Pnrno;
                    rptDeparture.DataSource = SelectedFlightsOutbound;
                    rptDeparture.DataBind();
                }
                if (Session["SelectedFlightsDetailsInbound"] != null)
                {
                    if (lobjItineraryDetails != null && SelectedFlightsInbound[0] != null)
                    {
                        dvReturnFlight.Visible = true;
                        lblInboundPNR.Text = lobjCreateDomesticBookingResponse.Inbound.Pnrno;
                        rptArrival.DataSource = SelectedFlightsInbound;
                        rptArrival.DataBind();

                    }
                }
                if(Session["DomesticFlightBookingRequest"] != null)
                {
                    BookingDetailsRequest lobjBookingDetailsRequest = new BookingDetailsRequest();
                    lobjBookingDetailsRequest= Session["DomesticFlightBookingRequest"] as BookingDetailsRequest;
                    if (lobjBookingDetailsRequest.PassengersInfo != null)
                    {
                        rptPassanger.DataSource = lobjBookingDetailsRequest.PassengersInfo;
                        rptPassanger.DataBind();
                    }
                }
            }
           

        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("UserControl_UCCTItinerayDetails_Domestic.ascx Pageload Exception: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
        }
    }
}