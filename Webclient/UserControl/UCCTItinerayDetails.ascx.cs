using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CB.IBE.Platform.Entities;
using Core.Platform.Member.Entites;
using CB.IBE.Platform.ClientEntities;

public partial class UserControl_UCItinerayDetails : System.Web.UI.UserControl
{
    ItineraryDetails lobjItineraryDetails = new ItineraryDetails();
    CreateItineraryResponse lobjCreateItineraryResponse = new CreateItineraryResponse();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["FlightBooked"] != null || Session["RetriveBookingInfo"] != null || Session["ReviewFlightDetails"] != null)
        {
            if (Session["FlightBooked"] != null)
                lobjItineraryDetails = (ItineraryDetails)Session["FlightBooked"];
            else if (Session["RetriveBookingInfo"] != null)
                lobjItineraryDetails = (ItineraryDetails)Session["RetriveBookingInfo"];
            else if (Session["ReviewFlightDetails"] != null)
            {
                lobjItineraryDetails = (ItineraryDetails)Session["ReviewFlightDetails"];
            }

            if (lobjItineraryDetails != null && lobjItineraryDetails.ListOfFlightDetails[0] != null)
            {

                LabelClass.Text = lobjItineraryDetails.CabinType;
                if (Session["MemberDetails"] != null)
                {
                    MemberDetails lobjMemberDetails = Session["MemberDetails"] as MemberDetails;
                    //lblMemberId.Text = lobjMemberDetails.MemberRelationsList.Find(lobj => lobj.RelationType.Equals(RelationType.LBMS)).RelationReference;
                }
                rptDeparture.DataSource = lobjItineraryDetails.ListOfFlightDetails[0].ListOfFlightSegments;
                rptDeparture.DataBind();
            }
            if (lobjItineraryDetails != null && lobjItineraryDetails.ListOfFlightDetails.Count > 1)
            {
                //dvReturnFlight.Visible = true;
                //lblReturnClass.Text = lobjItineraryDetails.CabinType;
                rptArrival.DataSource = lobjItineraryDetails.ListOfFlightDetails[1].ListOfFlightSegments;
                rptArrival.DataBind();
            }

            if (lobjItineraryDetails != null && lobjItineraryDetails.TravelerInfo != null)
            {
                rptPassanger.DataSource = lobjItineraryDetails.TravelerInfo;
                rptPassanger.DataBind();
            }
        }
    }

    protected void rptArrival_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        

        if (e.Item.ItemType == ListItemType.Header)
        {
            Label headerLabel = (Label)e.Item.FindControl("lblReturnClass");
            if(headerLabel != null)
            {
                // Set the text of the label here
                headerLabel.Text = lobjItineraryDetails.CabinType;
            }
        }
    }
}
