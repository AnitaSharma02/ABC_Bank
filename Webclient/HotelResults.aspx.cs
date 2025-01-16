using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using CB.IBE.Platform.Hotels.ClientEntities;
using Framework.EnterpriseLibrary.Common.SerializationHelper;
using Core.Platform.Member.Entites;
using ABC.Model;
using Framework.Integrations.Hotels.Entities;
using CB.IBE.Platform.Masters.Entities;
using Core.Platform.MemberActivity.Entities;
using Framework.EnterpriseLibrary.Adapters;
using IBEAPI.ClientEntities;

public partial class HotelResults : System.Web.UI.Page
{
    private FilterRange lobjFilterRange = new FilterRange();

    private List<int> lobjListOfPrice = new List<int>();

    private List<string> lobjListOfChain = new List<string>();

    private List<string> lobjListOfProertyType = new List<string>();

    private List<string> lobjListOfLocations = new List<string>();

    private List<string> lobjListOfHotelAmenities = new List<string>();

    private List<string> lobjListOfBasicAmenities = new List<string>();

    private List<string> lobjListOfBussinessService = new List<string>();

    private List<string> lobjListOfRoomAmenities = new List<string>();

    private HotelInfoRequest lobjhotelinforequest = new HotelInfoRequest();

    private HotelInfo lobjHotelInfo = new HotelInfo();

    public int count = 0;
    public int countJson = 0;
    public string lstrCurrency = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if (Session["SearchDetails"] != null)
                {
                  
                    HotelsSearchRequest lobjSearchRequest = Session["SearchDetails"] as HotelsSearchRequest;
                    if (lobjSearchRequest != null)
                    {
                        MemberDetails lobjMemberDetails = Session["MemberDetails"] as MemberDetails;
                        txtCity.Value = lobjSearchRequest.CountryISOCode.ToString() + ", " + lobjSearchRequest.Country.ToString() + ", " + lobjSearchRequest.CityName.ToString();
                        string ChkinDate = SetviewDate(lobjSearchRequest.CheckInDate);
                        TextBoxCheckin.Value = ChkinDate;
                        string ChkOutDate = SetviewDate(lobjSearchRequest.CheckOutDate);
                        TextBoxCheckout.Value = ChkOutDate;
                        qtyValue.Value = Convert.ToString(lobjSearchRequest.NoOfRooms);

                        switch (Convert.ToString(lobjSearchRequest.NoOfRooms))
                        {
                            case "1":
                                qtyValueAdult1.Value = Convert.ToString(lobjSearchRequest.AdultPerRoom);
                                qtyValueChild1.Value = Convert.ToString(lobjSearchRequest.ChildrenPerRoom);
                                break;
                            case "2":
                            case "3":
                            case "4":
                                string[] SplitAdult = Convert.ToString(lobjSearchRequest.AdultPerRoom).Split(',');
                                string[] SplitChild = Convert.ToString(lobjSearchRequest.ChildrenPerRoom).Split(',');
                                if (Convert.ToString(lobjSearchRequest.NoOfRooms) == "2")
                                {
                                    qtyValueAdult1.Value = SplitAdult[0];
                                    qtyValueChild1.Value = SplitChild[0];
                                    qtyValueAdult2.Value = SplitAdult[1];
                                    qtyValueChild2.Value = SplitChild[1];
                                }
                                else if (Convert.ToString(lobjSearchRequest.NoOfRooms) == "3")
                                {
                                    qtyValueAdult1.Value = SplitAdult[0];
                                    qtyValueChild1.Value = SplitChild[0];
                                    qtyValueAdult2.Value = SplitAdult[1];
                                    qtyValueChild2.Value = SplitChild[1];
                                    qtyValueAdult3.Value = SplitAdult[2];
                                    qtyValueChild3.Value = SplitChild[2];
                                }
                                else if (Convert.ToString(lobjSearchRequest.NoOfRooms) == "4")
                                {
                                    qtyValueAdult1.Value = SplitAdult[0];
                                    qtyValueChild1.Value = SplitChild[0];
                                    qtyValueAdult2.Value = SplitAdult[1];
                                    qtyValueChild2.Value = SplitChild[1];
                                    qtyValueAdult3.Value = SplitAdult[2];
                                    qtyValueChild3.Value = SplitChild[2];
                                    qtyValueAdult4.Value = SplitAdult[3];
                                    qtyValueChild4.Value = SplitChild[3];
                                }
                                break;
                        }
                    }
                    hdnPaymentType.Value = "Points";
                }
               
                SetHotelTemplate();
            }
        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("HotelResults.aspx- Page_Load Ex: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
        }
    }

    private string SetviewDate(DateTime dateTime)
    {
        string CalendarDate;
        string Date = dateTime.ToString("dd");
        string Month = dateTime.ToString("MM");
        string year = dateTime.ToString("yyyy");
        CalendarDate = Date + "/" + Month + "/" + year;
        return CalendarDate;
    }

    public void SetHotelTemplate()
    {
        try
        {
            if (Session["Hotels"] != null)
            {
                HotelSearchResponse lobjSearchResponse = Session["Hotels"] as HotelSearchResponse;
                HotelsSearchRequest lobjSearchRequest = Session["SearchDetails"] as HotelsSearchRequest;
                if (lobjSearchResponse != null)
                {
                    lobjSearchResponse.SearchResponse.hotels.hotel = lobjSearchResponse.SearchResponse.hotels.hotel.OrderBy(lobj => lobj.roomrates.RoomRate[0].TotalPoints).ToArray<Hotel>();
                    rptHotelList.DataSource = lobjSearchResponse.SearchResponse.hotels.hotel;
                    rptHotelList.DataBind();
                    LoggingAdapter.WriteLog("HotelResults.aspx- Hotels Count" + lobjSearchResponse.SearchResponse.hotels.hotel.Count());
                }
                lblSearchSummary.Text += "<b class=\"heading-medium d-none\">Your Hotel Search: </b>" + lobjSearchResponse.SearchResponse.searchcriteria.city.ToString();
                lblSearchSummary.Text += " " + lobjSearchResponse.SearchResponse.searchcriteria.country.ToString() + ", ";
                DateTime Chkin = Convert.ToDateTime(lobjSearchResponse.SearchResponse.searchcriteria.checkindate);
                lblSearchSummary.Text += Chkin.ToString("ddd, MMM d");
                DateTime ChkOut = Convert.ToDateTime(lobjSearchResponse.SearchResponse.searchcriteria.checkoutdate);
                lblSearchSummary.Text += " - " + ChkOut.ToString("ddd, MMM d");
                lblSearchSummary.Text += ", " + lobjSearchRequest.NoOfRooms.ToString() + " Room(s)";
                HFNoOfRooms.Value = lobjSearchRequest.NoOfRooms.ToString();
                hdnNoAdult.Value = lobjSearchRequest.AdultPerRoom.ToString();
                hdnNoChild.Value = lobjSearchRequest.ChildrenPerRoom.ToString();
                for (int i = 0; i < lobjSearchResponse.SearchResponse.hotels.hotel.Count(); i++)
                {
                  
                    lobjListOfChain.Add(lobjSearchResponse.SearchResponse.hotels.hotel[i].basicinfo.chain);
                    lobjListOfLocations.Add(lobjSearchResponse.SearchResponse.hotels.hotel[i].basicinfo.locality);
                    if (lobjSearchResponse.SearchResponse.hotels.hotel[i].basicinfo.hotelamenities.Amenities != null)
                    {
                        for (int k = 0; k < lobjSearchResponse.SearchResponse.hotels.hotel[i].basicinfo.hotelamenities.Amenities.Count(); k++)
                        {
                            for (int l = 0; l < lobjSearchResponse.SearchResponse.hotels.hotel[i].basicinfo.hotelamenities.Amenities[k].Amenities.hotelamenity.Count(); l++)
                            {
                                if (lobjSearchResponse.SearchResponse.hotels.hotel[i].basicinfo.hotelamenities.Amenities[k].category.Equals("Basics"))
                                    lobjListOfBasicAmenities.Add(lobjSearchResponse.SearchResponse.hotels.hotel[i].basicinfo.hotelamenities.Amenities[k].Amenities.hotelamenity[l].Value);
                                else if (lobjSearchResponse.SearchResponse.hotels.hotel[i].basicinfo.hotelamenities.Amenities[k].category.Equals("Hotel Amenities"))
                                    lobjListOfHotelAmenities.Add(lobjSearchResponse.SearchResponse.hotels.hotel[i].basicinfo.hotelamenities.Amenities[k].Amenities.hotelamenity[l].Value);
                                else if (lobjSearchResponse.SearchResponse.hotels.hotel[i].basicinfo.hotelamenities.Amenities[k].category.Equals("Business Services"))
                                    lobjListOfBussinessService.Add(lobjSearchResponse.SearchResponse.hotels.hotel[i].basicinfo.hotelamenities.Amenities[k].Amenities.hotelamenity[l].Value);
                                else if (lobjSearchResponse.SearchResponse.hotels.hotel[i].basicinfo.hotelamenities.Amenities[k].category.Equals("Room Amenities"))
                                    lobjListOfRoomAmenities.Add(lobjSearchResponse.SearchResponse.hotels.hotel[i].basicinfo.hotelamenities.Amenities[k].Amenities.hotelamenity[l].Value);
                            }
                        }
                    }
                    lobjhotelinforequest.hotelid = Convert.ToInt32(lobjSearchResponse.SearchResponse.hotels.hotel[i].hotelid);

                    lobjListOfPrice.Add(Convert.ToInt32(Convert.ToSingle(lobjSearchResponse.SearchResponse.hotels.hotel[i].roomrates.RoomRate[0].TotalPoints.ToString("N0"))));
                    lobjFilterRange.MaxPrice = lobjListOfPrice.Max();
                    lobjFilterRange.MinPrice = lobjListOfPrice.Min();
                    lobjListOfLocations = lobjListOfLocations.Distinct().ToList();
                    lobjListOfChain = lobjListOfChain.SkipWhile(x => string.IsNullOrEmpty(x)).ToList();
                    lobjFilterRange.ListOfHotelChain = lobjListOfChain.Distinct().ToList();
                    lobjFilterRange.ListOfLocation = lobjListOfLocations;
                    lobjFilterRange.ListOfBasicAmenities = lobjListOfBasicAmenities.Distinct().ToList();
                    lobjFilterRange.ListOfBussinessAmenities = lobjListOfBussinessService.Distinct().ToList();
                    lobjFilterRange.ListOfHotelAmenities = lobjListOfHotelAmenities.Distinct().ToList();
                    lobjFilterRange.ListOfRoomAmenities = lobjListOfRoomAmenities.Distinct().ToList();
                    hdnHotelFilterRange.Value = JSONSerialization.Serialize(lobjFilterRange);
                    LoggingAdapter.WriteLog("max Price" + lobjFilterRange.MaxPrice + ",min Price" + lobjFilterRange.MinPrice);
                }
            }
            else
            {
                Response.Redirect("Index.aspx", false);
            }
        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("HotelResults.aspx- SetHotelTemplate Ex: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
        }
    }

    protected void rptHotelList_ItemDataBound(Object Sender, RepeaterItemEventArgs e)
    {
        try
        {

            if (Session["hotels"] != null)
            {
                HotelSearchResponse lobjSearchResponse = Session["hotels"] as HotelSearchResponse;
                HotelSearchRequest lobjSearchRequest = Session["SearchDetails"] as HotelSearchRequest;
                if (lobjSearchResponse != null)
                {
                    if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                    {
                        if (hdnPaymentType.Value.Equals(Convert.ToString(PaymentType.Points)))
                        {
                            (e.Item.FindControl("lblmiles") as Label).Text = lobjSearchResponse.SearchResponse.hotels.hotel[e.Item.ItemIndex].roomrates.RoomRate[0].TotalPoints.ToString("N0");
                        }
                    }
                }
                if (lobjSearchResponse != null)
                {
                    if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                    {
                        if (countJson <= lobjSearchResponse.SearchResponse.hotels.hotel.Count())
                        {
                            (e.Item.FindControl("hdnHotelJsonData") as HiddenField).Value = JSONSerialization.Serialize(lobjSearchResponse.SearchResponse.hotels.hotel[countJson]);
                            //Amenities Images
                            if (lobjSearchResponse.SearchResponse.hotels.hotel[countJson].basicinfo.hotelamenities.Amenities != null)
                            {
                                if (lobjSearchResponse.SearchResponse.hotels.hotel[countJson].basicinfo.hotelratings.HotelRating != null && lobjSearchResponse.SearchResponse.hotels.hotel[countJson].basicinfo.hotelratings.HotelRating.Count() >= 0 && lobjSearchResponse.SearchResponse.hotels.hotel[countJson].basicinfo.hotelratings.HotelRating[0] != null)
                                {
                                    if (Convert.ToString(lobjSearchResponse.SearchResponse.hotels.hotel[countJson].basicinfo.hotelratings.HotelRating[0].rating).Equals("5"))
                                    {
                                        (e.Item.FindControl("imgMeeting") as Image).ImageUrl = "Images/meet-icon.svg";
                                        (e.Item.FindControl("imgMeeting") as Image).ToolTip = "Meeting Facilities";

                                        (e.Item.FindControl("imgGym") as Image).ImageUrl = "Images/gym-icon.svg";
                                        (e.Item.FindControl("imgGym") as Image).ToolTip = "Gym/Spa";

                                        (e.Item.FindControl("imgInternet") as Image).ImageUrl = "Images/wifi-icon.svg";
                                        (e.Item.FindControl("imgInternet") as Image).ToolTip = "Internet/Wi-Fi";

                                        (e.Item.FindControl("imgResturent") as Image).ImageUrl = "Images/resto-icon.svg";
                                        (e.Item.FindControl("imgResturent") as Image).ToolTip = "Restaurant/Coffee Shop";

                                        (e.Item.FindControl("imgswimmingPool") as Image).ImageUrl = "Images/pool-icon.svg";
                                        (e.Item.FindControl("imgswimmingPool") as Image).ToolTip = "Swimming Pool";
                                    }
                                    else
                                    {
                                        List<HotelAmenity> lobjHotelAmenityList = lobjSearchResponse.SearchResponse.hotels.hotel[countJson].basicinfo.hotelamenities.Amenities.ToList<HotelAmenity>();
                                        (e.Item.FindControl("imgMeeting") as Image).ImageUrl = (lobjHotelAmenityList.Find(lobj => lobj.Amenities.hotelamenity.ToList<Amenity>().Find(lobjAmenity => lobjAmenity.Value.ToLower().IndexOf("business center") >= 0 || lobjAmenity.Value.ToLower().IndexOf("meeting rooms") >= 0 || lobjAmenity.Value.ToLower().IndexOf("meeting facilities") >= 0) != null)) != null ? "Images/meet-icon.svg" : "Images/meet-icon-inactive.svg";
                                        if ((e.Item.FindControl("imgMeeting") as Image).ImageUrl == "Images/meet-icon.svg")
                                        {
                                            (e.Item.FindControl("imgMeeting") as Image).ToolTip = "Meeting Facilities";
                                        }
                                        (e.Item.FindControl("imgGym") as Image).ImageUrl = (lobjHotelAmenityList.Find(lobj => lobj.Amenities.hotelamenity.ToList<Amenity>().Find(lobjAmenity => lobjAmenity.Value.ToLower().IndexOf("gym") >= 0 || lobjAmenity.Value.ToLower().IndexOf("spa") >= 0 || lobjAmenity.Value.ToLower().IndexOf("fitness center") >= 0 || lobjAmenity.Value.ToLower().IndexOf("health club") >= 0) != null)) != null ? "Images/gym-icon.svg" : "Images/gym-icon-inactive.svg";
                                        if ((e.Item.FindControl("imgGym") as Image).ImageUrl == "Images/gym-icon.svg")
                                        {
                                            (e.Item.FindControl("imgGym") as Image).ToolTip = "Gym/Spa";
                                        }

                                        (e.Item.FindControl("imgInternet") as Image).ImageUrl = (lobjHotelAmenityList.Find(lobj => lobj.Amenities.hotelamenity.ToList<Amenity>().Find(lobjAmenity => lobjAmenity.Value.ToLower().IndexOf("internet") >= 0 || lobjAmenity.Value.ToLower().IndexOf("wi-fi") >= 0) != null)) != null ? "Images/wifi-icon.svg" : "Images/wifi-icon-inactive.svg";
                                        if ((e.Item.FindControl("imgInternet") as Image).ImageUrl == "Images/wifi-icon.svg")
                                        {
                                            (e.Item.FindControl("imgInternet") as Image).ToolTip = "Internet/Wi-Fi";
                                        }
                                        (e.Item.FindControl("imgResturent") as Image).ImageUrl = (lobjHotelAmenityList.Find(lobj => lobj.Amenities.hotelamenity.ToList<Amenity>().Find(lobjAmenity => lobjAmenity.Value.ToLower().IndexOf("restau") >= 0 || lobjAmenity.Value.ToLower().IndexOf("coffee shop") >= 0) != null)) != null ? "Images/resto-icon.svg" : "Images/resto-icon-inactive.svg";
                                        if ((e.Item.FindControl("imgResturent") as Image).ImageUrl == "Images/resto-icon.svg")
                                        {
                                            (e.Item.FindControl("imgResturent") as Image).ToolTip = "Restaurant/Coffee Shop";
                                        }
                                        (e.Item.FindControl("imgswimmingPool") as Image).ImageUrl = (lobjHotelAmenityList.Find(lobj => lobj.Amenities.hotelamenity.ToList<Amenity>().Find(lobjAmenity => lobjAmenity.Value.ToLower().IndexOf("pool") >= 0) != null)) != null ? "Images/pool-icon.svg" : "Images/pool-icon-inactive.svg";
                                        if ((e.Item.FindControl("imgswimmingPool") as Image).ImageUrl == "Images/pool-icon.svg")
                                        {
                                            (e.Item.FindControl("imgswimmingPool") as Image).ToolTip = "Swimming Pool";
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                if (lobjSearchResponse != null)
                {
                    if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                    {
                        if (countJson <= lobjSearchResponse.SearchResponse.hotels.hotel.Count())
                        {
                            (e.Item.FindControl("hdnHotelJsonData") as HiddenField).Value = JSONSerialization.Serialize(lobjSearchResponse.SearchResponse.hotels.hotel[countJson]);
                            countJson++;
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("HotelResults.aspx- rptHotelList_ItemDataBound Ex: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
        }
    }

    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static bool SetHotelId(string pstrHotelId)
    {
        ABCModel lobjModel = new ABCModel();
        lobjModel.LogActivity(string.Format("Hotel selected for booking"), ActivityType.HotelBooking);
        HttpContext.Current.Session["hotelId"] = pstrHotelId;
        return true;
    }
}