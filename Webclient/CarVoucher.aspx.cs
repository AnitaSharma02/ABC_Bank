using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CB.IBE.Platform.Car.ClientEntities;
using CB.IBE.Platform.Car.Entities;
using Core.Platform.Member.Entites;
using CB.IBE.Platform.Masters.Entities;
using ABC.Model;
using IBEAPI.ClientEntities;

public partial class CarVoucher : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["CarSearchRequest"] != null && Session["CarBookingRequest"] != null && Session["SelectedCar"] != null && Session["CarBookingResponse"] != null)
            {
                ABCModel lobjModel = new ABCModel();
                GetAvailabilityRequest lobjCarSearchRequest = Session["CarSearchRequest"] as GetAvailabilityRequest;
                CarBookingRequest lobjCarBookingRequest = Session["CarBookingRequest"] as CarBookingRequest;
                Rate lobjSelectedCar = Session["SelectedCar"] as Rate;
                CarBookingResponse lobjCarBookingResponse = Session["CarBookingResponse"] as CarBookingResponse;
                MemberDetails lobjMemberDetails = Session["MemberDetails"] as MemberDetails;
                double RequierdRedeemPoint = Convert.ToDouble(HttpContext.Current.Session["CarTotalRedeemAmount"]);

                lblMemberName.Text = lobjMemberDetails.FullName;
                lblMembershipReference.Text = lobjMemberDetails.MemberRelationsList.Find(lobj => lobj.RelationType.Equals(RelationType.LBMS)).RelationReference; //lobjMemberDetails.MembershipReference;

                lblbookingReferenceNo.Text = lobjCarBookingResponse.data.reference_id;
                lblBookingStatus.Text = lobjCarBookingResponse.success == 1 ? "Success" : "Failed";

                lblcarName.Text = lobjSelectedCar.vehicle.name;
                lblTransmissionType.Text = lobjSelectedCar.vehicle.transmissionText;
                lblAirCondition.Text = lobjSelectedCar.vehicle.airco == true ? "Yes" : "No";

                lblpickUp.Text = lobjCarSearchRequest.pickUp.location.name;
                lbldropOff.Text = lobjCarSearchRequest.dropOff.location.name;
                lblpickUpDate.Text = lobjCarSearchRequest.pickUp.date;
                lblPickUpTime.Text = lobjCarSearchRequest.pickUp.Time;
                lbldropOffDate.Text = lobjCarSearchRequest.dropOff.date;
                lbldropOffTime.Text = lobjCarSearchRequest.dropOff.Time;


                lblDriverFName.Text = lobjCarBookingRequest.customer.firstName;
                lblDriverLName.Text = lobjCarBookingRequest.customer.lastName;

                lblAddress.Text = lobjMemberDetails.Address;
                lblPhoneNo.Text = lobjMemberDetails.MobileNumber;

             
                string lstrCurrency = lobjModel.GetDefaultCurrency();
                string lstrPaymentDetails = string.Empty;
                string lstrPaymentDetailsHTML = "{0}<span class='heading-bold'>{1}</span>";
                 lstrPaymentDetails = string.Format(lstrPaymentDetailsHTML, lstrCurrency + " : ",Math.Ceiling(RequierdRedeemPoint));

                lstrPaymentDetails = "<p>" + lstrPaymentDetails + "</p>";
                PaymentInfo.InnerHtml = lstrPaymentDetails;
            }
        }
        else
        {
            Response.Redirect("Index.aspx");
        }
    }

}