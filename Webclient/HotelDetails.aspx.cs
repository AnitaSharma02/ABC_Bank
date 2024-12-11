using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ABC.Model;
using Framework.EnterpriseLibrary.Common.SerializationHelper;
using CB.IBE.Platform.Hotels.ClientEntities;
using Framework.Integrations.Hotels.Entities;
using Core.Platform.MemberActivity.Entities;
using System.Text;
using Framework.EnterpriseLibrary.Adapters;
using System.Security.Cryptography;
using System.IO;
using System.Net.NetworkInformation;
using CB.IBE.Platform.AirClientModel;
using IBEAPIGateway.Model;

public partial class HotelDetails : Page
{
    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static string GetHotelInfo()
    {
        string pstrHotelId = Convert.ToString(HttpContext.Current.Session["hotelId"]);
        IBEAPIModel lobjIBEAPIModel = new IBEAPIModel();
        //HotelInformationRequest lobjHotelInformationRequest = new HotelInformationRequest();
        //lobjHotelInformationRequest.InformationRequest.hotelid = Convert.ToInt32(pstrHotelId);
        HotelInformationResponse lobjReturn = lobjIBEAPIModel.GetHotelInformation(pstrHotelId);
        return JSONSerialization.Serialize(lobjReturn.HotelInformation);
    }
    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static string[] BindHotelDetails()
    {
        string[] lobjListOfData = new string[2];
        string lstrResult = string.Empty;
        string lstrSearchData = string.Empty;
        try
        {
            string pstrHotelId = Convert.ToString(HttpContext.Current.Session["hotelId"]);
            HotelSearchResponse lobjSearchResponse = HttpContext.Current.Session["hotels"] as HotelSearchResponse;
            List<Hotel> objHotelList = new List<Hotel>();
            objHotelList = lobjSearchResponse.SearchResponse.hotels.hotel.Where(abc => abc.hotelid.Equals(pstrHotelId)).ToList();
            lstrSearchData = lobjSearchResponse.SearchResponse.searchcriteria.numberofnights;
            HttpContext.Current.Session["SelectedRoomType"] = objHotelList;
            lstrResult = JSONSerialization.Serialize(objHotelList);
            lobjListOfData[0] = lstrResult;
            lobjListOfData[1] = lstrSearchData;
            ABCModel lobjModel = new ABCModel();
            lobjModel.LogActivity(string.Format("Selected HotelDetails; HotelId-:{0}; SelectedRoomType-:{1};", pstrHotelId, lstrResult), ActivityType.HotelSearch);
        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("HotelDetails.aspx- BindHotelDetails Ex: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
        }
        return lobjListOfData;
    }
    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static string Bookroom(string pstrroomtypecode)
    {
        ABCModel lobjModel = new ABCModel();
        string urlLink = string.Empty;
        try
        {
            lobjModel.LogActivity(string.Format("Hotel selected; proceed for booking"), ActivityType.HotelBooking);
            List<Hotel> objHotelList = HttpContext.Current.Session["SelectedRoomType"] as List<Hotel>;
            urlLink = "HotelBookingDetails.aspx?hotelid=" + objHotelList[0].hotelid + "&roomtypecode=" + HttpUtility.UrlEncode(pstrroomtypecode);
        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("HotelDetails.aspx- Bookroom Ex: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
        }
        return urlLink;
    }
    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<Hotel> BindNextHotel()
    {
        string pstrHotelId = Convert.ToString(HttpContext.Current.Session["hotelId"]);
        HotelSearchResponse lobjSearchResponse = HttpContext.Current.Session["hotels"] as HotelSearchResponse;
        List<Hotel> objHotelList = new List<Hotel>();
        objHotelList = lobjSearchResponse.SearchResponse.hotels.hotel.ToList();
        List<Hotel> LobjHotelsToPaint = ProcessHotels(objHotelList, Convert.ToInt32(pstrHotelId));
        return LobjHotelsToPaint;
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["MemberDetails"] == null)
            {
                string CallbackUrl = HttpUtility.UrlEncode(Encrypt(Request.RawUrl));
                HttpContext.Current.Session["CallbackUrl"] = CallbackUrl;
                Response.Redirect("Login.aspx", false);
            }
        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("HotelDetails.aspx- Page_Load Ex: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
        }
    }
    private static string Encrypt(string clearText)
    {
        try
        {
            string EncryptionKey = "MAKV2SPNIC99212";
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("Cart.aspx Encrypt Exception: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
        }
        return clearText;
    }
    private static List<Hotel> ProcessHotels(List<Hotel> lobjHotelList, int pintHotelID)
    {
        List<Hotel> lobjHotelListToPaint = new List<Hotel>();
        int mintMaxItemsToDisplay = 6;
        int mintCurrentHotelID = pintHotelID;
        int lintCounter = 0;
        try
        {
            //Is the current Hotel the Last Hotel in the list..
            bool lblnLastHotel = IsCurrentHotelLastHotel(lobjHotelList, mintCurrentHotelID);
            if (lblnLastHotel) //Last Hotel .. and subsequent traversal
            {
                for (int h = 0; h < lobjHotelList.Count; h++)
                {
                    if (lintCounter == mintMaxItemsToDisplay)
                        break;
                    lobjHotelListToPaint.Add(lobjHotelList[h]);
                    lintCounter++;
                }
            }
            else //Not the Last Hotel..that was selected..
            {
                Hotels lobjHotels = null;
                int lintNoOfHotelsToBeConsidered = 0;
                int lintCounterForInsufficientHotels = 0;
                for (int h = 0; h < lobjHotelList.Count; h++)
                {
                    if (Convert.ToInt32(lobjHotelList[h].hotelid) == mintCurrentHotelID)
                    {
                        lintNoOfHotelsToBeConsidered = ((lobjHotelList.Count - 1) - h);
                        //We have more hotels in the list than MaxRange of hotels to be displayed(ie.3 per grid).
                        if (mintMaxItemsToDisplay < lintNoOfHotelsToBeConsidered)
                        {
                            h++;
                            for (int i = h; i < lobjHotelList.Count; i++)
                            {
                                if (lintCounterForInsufficientHotels == mintMaxItemsToDisplay)
                                    break;
                                lobjHotelListToPaint.Add(lobjHotelList[i]);
                                lintCounterForInsufficientHotels++;
                            }
                        }
                        //We have less hotels in the list than MaxRange of hotels to be displayed(ie.3 per grid).
                        else
                        {
                            // Check No of items in the list ,and Display subsequent hotels in succession.
                            int lintRemainingHotelsToDisplay = 0;
                            h++;
                            for (int i = h; i < lobjHotelList.Count; i++)
                            {
                                lobjHotelListToPaint.Add(lobjHotelList[i]);
                                lintRemainingHotelsToDisplay++;
                            }
                            int hotelsToDisplayFromTheBeginning = mintMaxItemsToDisplay - lintRemainingHotelsToDisplay;
                            for (int x = 0; x < hotelsToDisplayFromTheBeginning; x++)
                            {
                                lobjHotelListToPaint.Add(lobjHotelList[x]);
                            }
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("HotelDetails.aspx- ProcessHotels Ex: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
        }
        return lobjHotelListToPaint;
    }

    private static bool IsCurrentHotelLastHotel(List<Hotel> pobjHotelList, int pintCurrentHotelID)
    {
        bool lblnIsLastHotel = Convert.ToInt32(pobjHotelList[pobjHotelList.Count - 1].hotelid) == pintCurrentHotelID ? true : false;
        return lblnIsLastHotel;
    }

}