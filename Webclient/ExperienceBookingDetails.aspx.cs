using BeMyGuest.Entities;
using ABC.Model;
using Framework.EnterpriseLibrary.Adapters;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ExperienceBookingDetails : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                string uuid = string.Empty;
                if (Request.QueryString.Count > 0)
                {
                    uuid = Convert.ToString(Request.QueryString["uuid"]);
                    ABCModel lobjModel = new ABCModel();
                    BookingInfoByUUIDResponse bookingInfoByUUIDResponse = lobjModel.GetBookingInfoByUUID(uuid);
                    if (bookingInfoByUUIDResponse != null)
                    {
                        BindExperienceBookingDetails(bookingInfoByUUIDResponse);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("ExperienceBookingDetails.aspx PageLoad Exception: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
        }
    }

    public void BindExperienceBookingDetails(BookingInfoByUUIDResponse bookingInfoByUUIDResponse)
    {
        try
        {
            if (bookingInfoByUUIDResponse != null)
            {
                ErrorDiv.Visible = false;
                OrderDetailsDiv.Visible = true;
                bookingCode.InnerHtml = bookingInfoByUUIDResponse.data.code;
                productName.InnerHtml = bookingInfoByUUIDResponse.data.prodtitle;
                productTypeTitle.InnerHtml = bookingInfoByUUIDResponse.data.productTypeTitle;
                Address.InnerHtml = bookingInfoByUUIDResponse.data.prodavailaddress;
                bookingdate.InnerHtml = Convert.ToDateTime(bookingInfoByUUIDResponse.data.completedAt).ToLocalTime().ToString("dd/MM/yyyy hh:mm tt");
                arrivaldate.InnerHtml = Convert.ToDateTime(bookingInfoByUUIDResponse.data.arrivalDate).ToString("dd/MM/yyyy");
                if (!string.IsNullOrEmpty(bookingInfoByUUIDResponse.data.timeSlot))
                {
                    timeslotdiv.Visible = true;
                    timeslot.InnerHtml = bookingInfoByUUIDResponse.data.timeSlot;
                }
                else
                {
                    timeslotdiv.Visible = false;
                }
                if (bookingInfoByUUIDResponse.data.amountBreakdown.Find(obj => obj.name.ToLower() == "adult").convertedAmount != null
                                                   && bookingInfoByUUIDResponse.data.amountBreakdown.Find(obj => obj.name.ToLower() == "adult").convertedAmount > 0)
                {
                    adultCount.InnerHtml = bookingInfoByUUIDResponse.data.adults + " x " + FormatCurrency(Math.Ceiling(bookingInfoByUUIDResponse.data.amountBreakdown.Find(obj => obj.name.ToLower() == "adult").convertedAmount), bookingInfoByUUIDResponse.data.convertedCurrency);
                }
                else
                {
                    adultCount.InnerHtml = bookingInfoByUUIDResponse.data.adults + " x " + FormatCurrency(0, bookingInfoByUUIDResponse.data.convertedCurrency);
                }

                if (bookingInfoByUUIDResponse.data.children != null && bookingInfoByUUIDResponse.data.children > 0)
                {
                    ChildCountDiv.Visible = true;
                    if (bookingInfoByUUIDResponse.data.amountBreakdown.Find(obj => obj.name.ToLower() == "child").convertedAmount != null
                                                  && bookingInfoByUUIDResponse.data.amountBreakdown.Find(obj => obj.name.ToLower() == "child").convertedAmount > 0)
                    {
                        childCount.InnerHtml = bookingInfoByUUIDResponse.data.children + " x " + FormatCurrency(Math.Ceiling(bookingInfoByUUIDResponse.data.amountBreakdown.Find(obj => obj.name.ToLower() == "child").convertedAmount), bookingInfoByUUIDResponse.data.convertedCurrency);
                    }
                    else
                    {
                        childCount.InnerHtml = bookingInfoByUUIDResponse.data.children + " x " + FormatCurrency(0, bookingInfoByUUIDResponse.data.convertedCurrency);
                    }
                }
                else
                {
                    ChildCountDiv.Visible = false;
                }

                if (bookingInfoByUUIDResponse.data.seniors != null && bookingInfoByUUIDResponse.data.seniors > 0)
                {
                    SeniorCountDiv.Visible = true;
                    if (bookingInfoByUUIDResponse.data.amountBreakdown.Find(obj => obj.name.ToLower() == "senior").convertedAmount != null
                                                  && bookingInfoByUUIDResponse.data.amountBreakdown.Find(obj => obj.name.ToLower() == "senior").convertedAmount > 0)
                    {
                        seniorCount.InnerHtml = bookingInfoByUUIDResponse.data.seniors + " x " + FormatCurrency(Math.Ceiling(bookingInfoByUUIDResponse.data.amountBreakdown.Find(obj => obj.name.ToLower() == "senior").convertedAmount), bookingInfoByUUIDResponse.data.convertedCurrency);
                    }
                    else
                    {
                        seniorCount.InnerHtml = bookingInfoByUUIDResponse.data.seniors + " x " + FormatCurrency(0, bookingInfoByUUIDResponse.data.convertedCurrency);
                    }
                }
                else
                {
                    SeniorCountDiv.Visible = false;
                }

                if (bookingInfoByUUIDResponse.data.convertedAmount != null && bookingInfoByUUIDResponse.data.convertedAmount > 0)
                {
                    totalPrice.InnerHtml = FormatCurrency(Math.Ceiling(bookingInfoByUUIDResponse.data.grandTotalAmount), bookingInfoByUUIDResponse.data.convertedCurrency);
                }
                else
                {
                    totalPrice.InnerHtml = FormatCurrency(0, bookingInfoByUUIDResponse.data.convertedCurrency);
                }

                Name.InnerHtml = string.Format("{0} {1} {2}", bookingInfoByUUIDResponse.data.salutation, bookingInfoByUUIDResponse.data.firstName, bookingInfoByUUIDResponse.data.lastName);
                EmailId.InnerHtml = bookingInfoByUUIDResponse.data.email;
                Phone.InnerHtml = bookingInfoByUUIDResponse.data.phone;

                StringBuilder sb = new StringBuilder();
                if (bookingInfoByUUIDResponse.data.options != null && bookingInfoByUUIDResponse.data.options.Count > 0)
                {
                    foreach (var optionsItem in bookingInfoByUUIDResponse.data.options)
                    {
                        sb.Append("<div class=\"row\">");
                        sb.Append("<div class=\"col-6 col-md-6 col-lg-5 my-2\">");
                        sb.Append("<p class=\"h6 heading-bold\">" + optionsItem.label + ":</p>");
                        sb.Append("</div>");
                        sb.Append("<div class=\"col-6 col-md-6 col-lg-7 my-2 text-start\">");
                        sb.Append("<p class=\"h6 heading-regular\">");
                        if (optionsItem.label.Replace(" ", "").Replace("/", "").ToLower().Contains("datetime"))
                        {
                            sb.Append(Convert.ToDateTime(optionsItem.value).ToString("dd/MM/yyyy HH:mm tt"));
                        }
                        else if (optionsItem.label.Replace(" ", "").Replace("/", "").ToLower().Contains("time")
                        && !optionsItem.label.Replace(" ", "").Replace("/", "").ToLower().Contains("select")
                        && !optionsItem.label.Replace(" ", "").Replace("/", "").ToLower().Contains("date"))
                        {
                            sb.Append(Convert.ToDateTime(optionsItem.value).ToString("HH:mm tt"));
                        }
                        else if (optionsItem.label.Replace(" ", "").Replace("/", "").ToLower().Contains("date"))
                        {
                            sb.Append(Convert.ToDateTime(optionsItem.value).ToString("dd/MM/yyyy"));
                        }
                        else if (optionsItem.label.Replace(" ", "").Replace("/", "").ToLower().Equals("freelunch"))
                        {
                            if (Convert.ToInt32(optionsItem.value) == 1)
                            {
                                sb.Append("<span>Yes</span>");
                            }
                            else
                            {
                                sb.Append("<span>No</span>");
                            }
                        }
                        else
                        {
                            sb.Append(optionsItem.value);
                        }
                        sb.Append("</p>");
                        sb.Append("</div>");
                        sb.Append("</div>");
                    }
                }
                else
                {
                    sb.Append("<div class=\"row\">");
                    sb.Append("<div class=\"col-12 my-2\">");
                    sb.Append("<p class=\"h6 heading-regular\">No additional info available.</p>");
                    sb.Append("</div>");
                    sb.Append("</div>");
                }
                AdditionalInfo.InnerHtml = sb.ToString();


                if (!string.IsNullOrEmpty(bookingInfoByUUIDResponse.data.meetingTime)
                                   || !string.IsNullOrEmpty(bookingInfoByUUIDResponse.data.meetingAddress)
                                   || !string.IsNullOrEmpty(bookingInfoByUUIDResponse.data.meetingLocation))
                {
                    PickUpMeetingErrorDiv.Visible = false;
                    if (!string.IsNullOrEmpty(bookingInfoByUUIDResponse.data.meetingTime))
                    {
                        MeetingTimeDiv.Visible = true;
                        MeetingTime.InnerHtml = bookingInfoByUUIDResponse.data.meetingTime;
                    }
                    else
                    {
                        MeetingTimeDiv.Visible = false;
                    }
                    if (!string.IsNullOrEmpty(bookingInfoByUUIDResponse.data.meetingAddress))
                    {
                        MeetingAddressDiv.Visible = true;
                        MeetingAddress.InnerHtml = bookingInfoByUUIDResponse.data.meetingAddress;
                    }
                    else
                    {
                        MeetingAddressDiv.Visible = false;
                    }
                    if (!string.IsNullOrEmpty(bookingInfoByUUIDResponse.data.meetingLocation))
                    {
                        MeetingLocationDiv.Visible = true;
                        MeetingLocation.InnerHtml = bookingInfoByUUIDResponse.data.meetingLocation;
                    }
                    else
                    {
                        MeetingLocationDiv.Visible = false;
                    }
                }
                else
                {
                    MeetingTimeDiv.Visible = false;
                    MeetingAddressDiv.Visible = false;
                    MeetingLocationDiv.Visible = false;
                    PickUpMeetingErrorDiv.Visible = true;
                }
            }
            else
            {
                OrderDetailsDiv.Visible = false;
                ErrorDiv.Visible = true;
            }
        }
        catch (Exception ex)
        {
            OrderDetailsDiv.Visible = false;
            ErrorDiv.Visible = true;
            LoggingAdapter.WriteLog("ExperienceBookingDetails.aspx BindExperienceBookingDetails Exception: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
        }
    }

    public string FormatCurrency(decimal decValue, string currencyCode, bool requiredDecimal = false)
    {
        NumberFormatInfo nfo = new CultureInfo("en-US", false).NumberFormat;
        if (requiredDecimal)
        {
            nfo.NumberDecimalDigits = 2;
            if (!string.IsNullOrEmpty(currencyCode))
            {
                return string.Format("{0} {1}", currencyCode, decValue.ToString("N", nfo));
            }
            else
            {
                return string.Format("{0}", decValue.ToString("N", nfo));
            }
        }
        else
        {
            if (!string.IsNullOrEmpty(currencyCode))
            {
                return string.Format("{0} {1}", currencyCode, decValue.ToString("N", nfo).Split('.')[0]);
            }
            else
            {
                return string.Format("{0}", decValue.ToString("N", nfo).Split('.')[0]);
            }
        }
    }
}