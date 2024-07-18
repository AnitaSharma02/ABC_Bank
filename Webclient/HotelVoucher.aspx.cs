using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Framework.Integrations.Hotels.Entities;
using Framework.Integrations.Common.Constants;
using CB.IBE.Platform.Hotels.ClientEntities;
using Core.Platform.RedemptionAuditTrail.Entities;
using ABC.Model;
using Framework.EnterpriseLibrary.Adapters;

public partial class HotelVoucher : System.Web.UI.Page
{
    public int roomCount = 0;

    ABCModel lobjModel = new ABCModel();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if (Session["PageTitle"] != null)
                {
                    this.Title = Session["PageTitle"].ToString();
                }
                if (Session["HotelBooked"] != null && Session["CustomerDetails"] != null && Session["BookingResponse"] != null)
                {
                    HotelSearchResponse lobjSearchResponse = Session["HotelBooked"] as HotelSearchResponse;
                    lblHotelName.Text = lobjSearchResponse.SearchResponse.hotels.hotel[0].basicinfo.hotelname.ToString();
                    lblHotelPhone.Text = lobjSearchResponse.SearchResponse.hotels.hotel[0].basicinfo.communicationinfo.phone.ToString();
                    lblHotelFax.Text = lobjSearchResponse.SearchResponse.hotels.hotel[0].basicinfo.communicationinfo.fax.ToString();
                    lblAddress.Text = lobjSearchResponse.SearchResponse.hotels.hotel[0].basicinfo.address.ToString();
                    lblRating.Text = lobjSearchResponse.SearchResponse.hotels.hotel[0].basicinfo.starrating;
                    imgMap.ImageUrl = string.Format(CommonConstant.GOOGLEMAPSURL, lobjSearchResponse.SearchResponse.hotels.hotel[0].otherinfo.locationinfo.latitude, lobjSearchResponse.SearchResponse.hotels.hotel[0].otherinfo.locationinfo.longitude);

                    Customer lobjCustomer = Session["CustomerDetails"] as Customer;
                    lblPersonName.Text = lobjCustomer.title + " " + lobjCustomer.firstname.ToString() + " " + lobjCustomer.lastname.ToString();
                    lblPersonAddress.Text = lobjCustomer.city.ToString();
                    lblBookPersonName.Text = lobjCustomer.title + " " + lobjCustomer.firstname.ToString() + " " + lobjCustomer.lastname.ToString();
                    lblPersonEmail.Text = lobjCustomer.email;

                    lblTotalRoom.Text = lobjSearchResponse.SearchResponse.searchcriteria.numberofrooms.ToString() + " room(s), ";
                    lblTotalnight.Text = lobjSearchResponse.SearchResponse.searchcriteria.numberofnights.ToString() + " night(s)";

                    //lblCheckinDate.Text = lobjSearchResponse.SearchResponse.searchcriteria.checkindate;
                    //lblCheckoutDate.Text = lobjSearchResponse.SearchResponse.searchcriteria.checkoutdate;

                    lblWebsite.Text = lobjSearchResponse.SearchResponse.hotels.hotel[0].basicinfo.communicationinfo.website.ToString();
                    DateTime Chkin = Convert.ToDateTime(lobjSearchResponse.SearchResponse.searchcriteria.checkindate);
                    lblCheckinDate.Text = Chkin.ToString("dd/MM/yyyy");
                    DateTime ChkOut = Convert.ToDateTime(lobjSearchResponse.SearchResponse.searchcriteria.checkoutdate);
                    lblCheckoutDate.Text = ChkOut.ToString("dd/MM/yyyy");

                    lblRoomtype.Text = lobjSearchResponse.SearchResponse.hotels.hotel[0].roomrates.RoomRate[0].roomtype.roomdescription.ToString();
                    HotelBookingResponse lobjBookingResponse = Session["BookingResponse"] as HotelBookingResponse;
                    lblBookingID.Text = lobjBookingResponse.BookingResponse.bookingid;
                    lblExternalRefId.Text = lobjBookingResponse.BookingResponse.confirmationnumber;
                    lblSpecialRequest.Text = lobjSearchResponse.SearchResponse.hotels.hotel[0].SpecialRequest;
                    lblTransactionReference.Text = lobjBookingResponse.BookingResponse.TransactionRefCode;
                    string lstrPaymentDetails = string.Empty;
                    string lstrPaymentDetailsHTML = "<span>{1}</span>";

                    for (int i = 0; i < lobjBookingResponse.BookingPaymentDetails.BookingPaymentBreakageList.Count; i++)
                    {
                        if (lobjBookingResponse.BookingPaymentDetails.BookingPaymentBreakageList[i].Currency.Equals("OMR"))
                        {
                            if (i.Equals(0))
                            {
                                lstrPaymentDetails = string.Format(lstrPaymentDetailsHTML, lobjBookingResponse.BookingPaymentDetails.BookingPaymentBreakageList[i].Currency, lobjModel.FloatToThousandSeperated(lobjBookingResponse.BookingPaymentDetails.BookingPaymentBreakageList[i].Amount));
                            }
                            else
                            {
                                lstrPaymentDetails += string.Format(lstrPaymentDetailsHTML, lobjBookingResponse.BookingPaymentDetails.BookingPaymentBreakageList[i].Currency, lobjModel.FloatToThousandSeperated(lobjBookingResponse.BookingPaymentDetails.BookingPaymentBreakageList[i].Amount));
                            }
                        }
                        else
                        {
                            if (i.Equals(0))
                            {
                                lstrPaymentDetails = string.Format(lstrPaymentDetailsHTML, " Points: ", lobjModel.FloatToThousandSeperated(lobjBookingResponse.BookingPaymentDetails.BookingPaymentBreakageList[i].Amount));
                            }
                            else
                            {
                                lstrPaymentDetails += string.Format(lstrPaymentDetailsHTML, " Points: ", lobjModel.FloatToThousandSeperated(lobjBookingResponse.BookingPaymentDetails.BookingPaymentBreakageList[i].Amount));
                            }
                        }

                    }
                    if (lobjBookingResponse.BookingPaymentDetails.BookingPaymentBreakageList.Count == 0)
                    {
                        ABCModel verveModel = new ABCModel();
                        lstrPaymentDetails = string.Format(lstrPaymentDetailsHTML,  " Points: ", lobjModel.FloatToThousandSeperated(lobjBookingResponse.BookingPaymentDetails.Points));
                    }
                    divPaymentDetails.InnerHtml = lstrPaymentDetails;
                    divTotalMiles.InnerHtml = lstrPaymentDetails;
                }
                else
                {
                    Response.Redirect("Index.aspx", false);
                }
            }
        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("HotelVoucher.aspx- Page_Load Ex: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
        }
    }
}