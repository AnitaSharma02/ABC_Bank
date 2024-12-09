using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ABC.Model;
using CB.IBE.Platform.Entities;
using Core.Platform.Member.Entites;
using Core.Platform.Master.Entities;
using CB.IBE.Platform.ClientEntities;
using Core.Platform.MemberActivity.Constants;
using Core.Platform.MemberActivity.Entities;
using Framework.EnterpriseLibrary.Adapters;
using System.IO;
using CB.IBE.Platform.Hotels.ClientEntities;
using Framework.Integrations.Hotels.Entities;
using Core.Platform.ProgramMaster.Entities;
using IBEAPI.ClientEntities;
using IBEAPIGateway.Model;
using System.Configuration;
using Newtonsoft.Json;


public partial class HotelsSearchWait : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static bool GetHotelSearchResponse(string pstrCity, string pCheckIn, string pCheckOut, string pRoomString, string pisRedeemMiles, string strRating)
    {
        try
        {
            ABCModel lobjModel = new ABCModel();
            IBEAPIModel lobjIBEAPIModel = new IBEAPIModel();
            lobjModel.LogActivity(string.Format("HotelSearch Request;City-:{0}; Checkin-:{1}; Checkout-:{2}; RoomString-:{3};  Rating-:{4};", pstrCity, pCheckIn, pCheckOut, pRoomString, strRating), ActivityType.HotelSearch);
            ProgramDefinition lobjProgramDefinition = lobjModel.GetProgramMaster();
            HotelsSearchRequest lobjSearchRequest = new HotelsSearchRequest();

            lobjSearchRequest.IpAddress = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            MemberDetails lobjMemberDetails = HttpContext.Current.Session["MemberDetails"] as MemberDetails;
            if (lobjMemberDetails != null)
            {
                lobjSearchRequest.MembershipReference = lobjMemberDetails.MemberRelationsList[0].RelationReference;
            }
            string lstrCurrency = lobjModel.GetDefaultCurrency();
            HttpContext.Current.Session["SearchCurrency"] = lstrCurrency;
            lobjSearchRequest.RedemptionRate = lobjModel.GetProgramRedemptionRate(lstrCurrency, RedemptionCodeKeys.HOT.ToString(), lobjProgramDefinition.ProgramId);
            lobjSearchRequest.CheckInDate = lobjIBEAPIModel.StringToDateTime(pCheckIn);
            lobjSearchRequest.CheckOutDate = lobjIBEAPIModel.StringToDateTime(pCheckOut);

            string[] arrayRoomPersonType = pRoomString.TrimEnd(':').Split(':');
            lobjSearchRequest.NoOfRooms = arrayRoomPersonType[0].TrimEnd(',').Split(',').Count();
            lobjSearchRequest.AdultPerRoom = arrayRoomPersonType[0].TrimEnd(',');//adult
            lobjSearchRequest.ChildrenPerRoom = arrayRoomPersonType[1].TrimEnd(',');//child

            lobjSearchRequest.CityName = HttpUtility.UrlDecode(pstrCity.Split(',')[2].ToString());
            lobjSearchRequest.CountryISOCode = pstrCity.Split(',')[0].ToString();
            lobjSearchRequest.Country = HttpUtility.UrlDecode(pstrCity.Split(',')[1].ToString());
            lobjSearchRequest.StarRating = "All";
            lobjSearchRequest.OrderBy = "PriceAsc";

            lobjSearchRequest.ResultCount = string.IsNullOrEmpty(Convert.ToString(ConfigurationManager.AppSettings["HotelResultCount"])) ? 500 : Convert.ToInt32(Convert.ToString(ConfigurationManager.AppSettings["HotelResultCount"]));
            LoggingAdapter.WriteLog("GetHotelSearchResponse " + JsonConvert.SerializeObject(lobjSearchRequest));
            HotelSearchResponse lobjHotelSearchResponse = lobjIBEAPIModel.GetHotelSearchResponse(lobjSearchRequest);
            LoggingAdapter.WriteLog("GetHotelSearchResponse " + JsonConvert.SerializeObject(lobjHotelSearchResponse));

            lobjSearchRequest.SearchId = lobjHotelSearchResponse.SearchId;
            HttpContext.Current.Session["SearchDetails"] = lobjSearchRequest;
            HttpContext.Current.Session["Hotels"] = lobjHotelSearchResponse;
            if (lobjHotelSearchResponse != null)
            {
                if (lobjHotelSearchResponse.SearchResponse.hotels.hotel != null && lobjHotelSearchResponse.SearchResponse.hotels.hotel.Count() > 0)
                {
                    if (strRating != "All")
                    {
                        List<Hotel> lobjListNewHotel = new List<Hotel>();

                        List<Hotel> lobjListHotel = new List<Hotel>();
                        lobjListHotel = lobjHotelSearchResponse.SearchResponse.hotels.hotel.ToList();
                        string[] tokens = strRating.TrimEnd(',').Split(',');
                        for (int i = 0; i < tokens.Count(); i++)
                        {
                            lobjListNewHotel.AddRange(lobjListHotel.FindAll(x => x.basicinfo.starrating.Equals(tokens[i])));
                        }
                        lobjHotelSearchResponse.SearchResponse.hotels.hotel = lobjListNewHotel.ToArray();
                    }
                    lobjModel.LogActivity(string.Format("HotelSearch : Success; Total Hotels Count-:{0}", lobjHotelSearchResponse.SearchResponse.hotels.hotel.Count()), ActivityType.HotelSearch);
                    return true;
                }
                else
                {
                    lobjModel.LogActivity(string.Format("HotelSearch : Failed;"), ActivityType.HotelSearch);
                    return false;
                }
            }
            else
            {
                lobjModel.LogActivity(string.Format("HotelSearch : Failed;"), ActivityType.HotelSearch);
                return false;
            }
        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("GetHotelSearchResponse ex-" + ex.Message + Environment.NewLine + ex.StackTrace + Environment.NewLine + ex.InnerException);
            return false;
        }
    }
}