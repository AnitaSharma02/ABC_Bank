using Core.Platform.Helper.ProgramName;
using Core.Platform.Member.Entites;
using Core.Platform.MemberActivity.Constants;
using Core.Platform.MemberActivity.Entities;
using Core.Platform.ProgramMaster.Entities;
using Framework.EnterpriseLibrary.Adapters;
using Holibob.Entities;
using Newtonsoft.Json;
using ABC.Model;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Services;

public partial class ExperienceProductBookingDetails : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string lstrBookId = Convert.ToString(Request.QueryString["Id"]);
            if (!string.IsNullOrEmpty(lstrBookId))
            {
                if (!Page.IsPostBack)
                {
                    ABCModel lobjModel = new ABCModel();
                    MemberDetails lobjMemberDetails = Session["MemberDetails"] as MemberDetails;
                    if (lobjMemberDetails == null)
                    {
                        Response.Redirect("SessionTimeout.aspx", false);
                    }
                    else
                    {
                        lobjModel.LogActivity(string.Format(" Visit ExperienceProductBookingDetails; BookId: {0}", lstrBookId), ActivityType.PageLoad);
                    }
                }
            }
            else
            {
                Response.Redirect("ManageBooking.aspx", false);
            }
        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("ExperienceProductBookingDetails.aspx Pageload Exception: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
        }
    }
    [WebMethod]
    public static string GetProductBookingDetails(string pstrBookId)
    {
        string lstrResponse = string.Empty;
        StringBuilder lNICogRequestResponse = new StringBuilder();
        ABCModel lobjModel = new ABCModel();
        try
        {
            MemberDetails lobjMemberDetails = HttpContext.Current.Session["MemberDetails"] as MemberDetails;
            if (lobjMemberDetails != null && !string.IsNullOrEmpty(lobjMemberDetails.FullName))
            {
                lNICogRequestResponse.Append(string.Format("GetProductBookingDetails Request: pstrBookId - {0}", pstrBookId));
                OrderStatusResponse lobjOrderStatusResponse = lobjModel.GetOrderStatusByBookingId(pstrBookId);

                if (lobjOrderStatusResponse != null
                    && lobjOrderStatusResponse.data != null
                    && !string.IsNullOrEmpty(lobjOrderStatusResponse.data.getOrderStatus))
                {
                    lobjModel.LogActivity(string.Format(" GetProductBookingDetails; BookId:{0}; Status: {1};", pstrBookId, "Success"), ActivityType.PackageBooking);

                    HolibobOrderStatus lobjOrderStatus = JsonConvert.DeserializeObject<HolibobOrderStatus>(lobjOrderStatusResponse.data.getOrderStatus);
                    if (lobjOrderStatus != null && lobjOrderStatus.data != null && lobjOrderStatus.status.ToLower() == "success")
                    {
                        string lstrCurrency = lobjModel.GetDefaultCurrency();
                        string lstrProgramName = ProgramHelper.ProgramName();
                        ProgramDefinition lobjProgramDefinition = lobjModel.GetProgramDetails(lstrProgramName);
                        if (lobjOrderStatus.data.rawData.data.booking.availabilityList.nodes != null)
                        {
                            foreach (var item in lobjOrderStatus.data.rawData.data.booking.availabilityList.nodes)
                            {
                                string pstrGrossFormattedText = string.Empty;
                                item.totalPrice.gross = lobjModel.ConvertToPointsOrCurrency(item.totalPrice.gross, lstrCurrency, lobjProgramDefinition.ProgramId, out pstrGrossFormattedText);
                                item.totalPrice.grossFormattedText = pstrGrossFormattedText;
                            }
                            lobjOrderStatusResponse.data.getOrderStatus = JsonConvert.SerializeObject(lobjOrderStatus);
                        }
                        lstrResponse = JsonConvert.SerializeObject(lobjOrderStatus);
                        lNICogRequestResponse.Append(string.Format("| GetProductBookingDetails Response: pstrBookId - {0}", lstrResponse));

                        LoggingAdapter.WriteLog("ExperienceProductBooking GetProductBookingDetails Request_Response: "+ lNICogRequestResponse);
                    }
                }
                else
                {
                    lobjModel.LogActivity(string.Format(" GetProductBookingDetails; BookId:{0}; Status: {1};", pstrBookId, "Failed"), ActivityType.PackageBooking);
                }
            }
        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("ExperienceProductBooking GetProductBookingDetails Exception: " + ex.Message + Environment.NewLine + ex.StackTrace);
        }
        finally
        {
            // lobjModel.LogActivity(string.Format("ActivityType: {0} - RequestResponse: {1}", ActivityConstants.BookPackage, lNICogRequestResponse), ActivityType.PackageBooking);
        }
        return lstrResponse;
    }
    private static string Encrypt(string clearText)
    {
        string EncryptionKey = "MAKV2SPBMI99212";
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
        return clearText;
    }
}