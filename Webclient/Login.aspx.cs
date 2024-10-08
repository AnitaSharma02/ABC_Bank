using System;
using System.Web;
using System.Web.UI;
using ABC.Model;
using Core.Platform.Member.Entites;
using Core.Platform.ProgramMaster.Entities;
using Core.Platform.MemberActivity.Entities;
using Core.Platform.MemberActivity.Constants;
using System.Web.SessionState;
using System.Reflection;
using Framework.EnterpriseLibrary.Adapters;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Core.Framework.PostHelper;
using Newtonsoft.Json;
using System.Collections.Generic;

public partial class Login : Page
{
    public string pstrMembershipRef { get; set; }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            Session["AvailablePoints"] = null;
            System.Web.UI.HtmlControls.HtmlGenericControl sitemap = (System.Web.UI.HtmlControls.HtmlGenericControl)Master.FindControl("sitemap");
            sitemap.Attributes.Add("Style", "display:none");
            // lblLoginError.Text = "";
            if (Request.QueryString["id"] != null)
            {
                if (Request.QueryString["id"].Equals("1"))//Normal login
                {

                }
                else if (Request.QueryString["id"].Equals("2"))//Activation
                {
                    //  lblLoginError.Text = "Please enter your ID number and password that has been sent to your registered email and mobile number.";
                }
                else if (Request.QueryString["id"].Equals("3"))//Block
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Call my function", "showMyLoginDiv()", true);
                }
            }
            if (Request.Cookies["NICLogin"] != null)
            {
                pstrMembershipRef = Convert.ToString(Request.Cookies["NICLogin"].Values["MembershipRef"]);
            }

        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("Index.aspx Page_Load Ex-" + ex.Message + ex.InnerException + ex.StackTrace);
        }
    }
    private string Decrypt(string cipherText)
    {
        string EncryptionKey = "MAKV2SPNIC99212";
        cipherText = cipherText.Replace(" ", "+");
        byte[] cipherBytes = Convert.FromBase64String(cipherText);
        using (Aes encryptor = Aes.Create())
        {
            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
            encryptor.Key = pdb.GetBytes(32);
            encryptor.IV = pdb.GetBytes(16);
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(cipherBytes, 0, cipherBytes.Length);
                    cs.Close();
                }
                cipherText = Encoding.Unicode.GetString(ms.ToArray());
            }
        }
        return cipherText;
    }
    private static void updateSessionId(HttpContext Context)
    {
        try
        {
            SessionIDManager manager = new SessionIDManager();
            string oldId = manager.GetSessionID(Context);
            string newId = manager.CreateSessionID(Context);
            bool isAdd = false, isRedir = false;
            manager.SaveSessionID(Context, newId, out isRedir, out isAdd);
            HttpApplication ctx = HttpContext.Current.ApplicationInstance;
            HttpModuleCollection mods = ctx.Modules;
            SessionStateModule ssm = (SessionStateModule)mods.Get("Session");
            FieldInfo[] fields = ssm.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance);
            SessionStateStoreProviderBase store = null;
            FieldInfo rqIdField = null, rqLockIdField = null, rqStateNotFoundField = null;
            foreach (FieldInfo field in fields)
            {
                if (field.Name.Equals("_store")) store = (SessionStateStoreProviderBase)field.GetValue(ssm);
                if (field.Name.Equals("_rqId")) rqIdField = field;
                if (field.Name.Equals("_rqLockId")) rqLockIdField = field;
                if (field.Name.Equals("_rqSessionStateNotFound")) rqStateNotFoundField = field;
            }
            object lockId = rqLockIdField.GetValue(ssm);
            if ((lockId != null) && (oldId != null)) store.ReleaseItemExclusive(Context, oldId, lockId);
            rqStateNotFoundField.SetValue(ssm, true);
            rqIdField.SetValue(ssm, newId);
        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("Index.aspx- updateSessionId Ex: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
        }
    }
    protected void BtnLoginValidation_Click(object sender, EventArgs e)
    {
        ABCModel lobjModel = new ABCModel();
        string mstrRedirectEmptyURL = string.Empty;
        string Attempt = string.Empty;
        string strMD5Password = string.Empty;
        string lstrMemberID = txtMemberID.Value;
        string pstrSecurityCode = txtSecurityCode.Text;
        string lstrPassword = txtPassword.Value;
        bool pboolRememberMe = chkRememberMe.Checked;
        string errormsg = string.Empty;
        try
        {
            ProgramDefinition lobjProgramDefinition = lobjModel.GetProgramMaster();
            MemberDetails lobjMemberDetails = lobjModel.GetMemberDetailsByUniqueAttribute(lobjProgramDefinition.ProgramId, lstrMemberID);
            string CallbackUrl = Request.QueryString["CallbackUrl"];
            if (HttpContext.Current.Session["CAPTCHA"].ToString().Equals(pstrSecurityCode))
            {
                if (lobjMemberDetails != null)
                {
                    strMD5Password = lobjModel.GenerateMD5(lobjMemberDetails.MemberRelationsList[0].RelationReference.Trim() + lstrPassword);
                    MemberRelation lobjMemberRelations = lobjMemberDetails.MemberRelationsList.Find(x => x.RelationType.Equals(RelationType.LBMS));
                    if (lobjMemberRelations.WebPassword != strMD5Password.Trim().ToUpper())
                    {
                        lobjMemberDetails = null;
                    }
                    if (lobjMemberDetails != null)
                    {
                        Attempt = Convert.ToString(lobjMemberRelations.LoginAttempt) + ":" + lobjMemberRelations.Status;
                    }
                    else
                    {
                        Attempt = Convert.ToString(0) + ":" + "Active";
                    }
                    if (lobjMemberDetails != null)
                    {
                        updateSessionId(HttpContext.Current);
                        if (pboolRememberMe)
                        {
                            HttpCookie cookie = new HttpCookie("GIMLogin");
                            cookie.Values.Add("MembershipRef", lstrMemberID);
                            cookie.Expires = DateTime.Now.AddDays(15);
                            HttpContext.Current.Response.Cookies.Add(cookie);
                        }
                        MemberRelation lobjMemberRelation = lobjMemberDetails.MemberRelationsList.Find(x => x.RelationType.Equals(RelationType.LBMS));
                        if (lobjMemberRelation.Status.Equals(Status.Active) && lobjMemberRelation.IsAccountActivated)
                        {
                            MemberActivitySession lobjMemberActivitySession = HttpContext.Current.Session["MemberActivitySession"] as MemberActivitySession;
                            lobjMemberActivitySession.ReferenceNumber = lobjMemberDetails.MemberRelationsList.Find(lobj => lobj.RelationType.Equals(RelationType.LBMS)).RelationReference;
                            lobjMemberActivitySession.ActivityType = ActivityType.loginsuccessful;
                            lobjMemberActivitySession.ProgramId = lobjProgramDefinition.ProgramId;
                            bool isMembershipUpdated = lobjModel.UpdateMemberShipActivitySession(lobjMemberActivitySession);
                            lobjModel.LogActivity(string.Format(ActivityConstants.Login, lstrMemberID, "successful"), ActivityType.loginsuccessful);
                            HttpContext.Current.Session["MemberLoginDetails"] = null;
                            HttpContext.Current.Session["MemberDetails"] = lobjMemberDetails;
                            Session["FromSSOLogin"] = "0";

                            lobjMemberRelation.Id = lobjMemberDetails.Id;
                            lobjModel.ResetLoginAttempt(lobjMemberRelation);

                            if (HttpContext.Current.Session["SelectedItinerary"] != null)
                            {
                                HttpContext.Current.Session["MemberDetails"] = lobjMemberDetails;
                                Response.Redirect("FlightPassenger.aspx", false);
                            }
                            else if (HttpContext.Current.Session["CashBackClick"] != null)
                            {
                                HttpContext.Current.Session["CashBackClick"] = null;
                                HttpContext.Current.Session["CashBackLogin"] = "Login";
                                Response.Redirect("Index.aspx", false);
                            }
                            else if (HttpContext.Current.Session["HotelSelected"] != null)
                            {
                                HttpContext.Current.Session["MemberDetails"] = lobjMemberDetails;
                                Response.Redirect(HttpContext.Current.Session["HotelSelected"].ToString(), false);
                            }
                            else if (HttpContext.Current.Session["CarRefId"] != null)
                            {
                                HttpContext.Current.Session["MemberDetails"] = lobjMemberDetails;
                                mstrRedirectEmptyURL = HttpContext.Current.Session["CarRefId"].ToString();
                                Response.Redirect("CarDetails.aspx?id=" + ((MemberDetails)HttpContext.Current.Session["MemberDetails"]).MemberRelationsList[0].RelationReference, false);
                            }
                            else if (HttpContext.Current.Session["CallbackUrl"] != null)
                            {
                                HttpContext.Current.Session["MemberDetails"] = lobjMemberDetails;
                                Response.Redirect(HttpContext.Current.Session["CallbackUrl"].ToString(), false);
                            }
                            else if (HttpContext.Current.Session["MemberDetails"] != null)
                            {
                                if (lobjMemberDetails.MemberRelationsList.Find(lobj => lobj.RelationType.Equals(RelationType.LBMS)).ForceChangePassword)
                                {
                                    HttpContext.Current.Session["MemberDetails"] = lobjMemberDetails;
                                    Response.Redirect("ViewMemberProfile.aspx", false);
                                }
                                else if (lobjMemberDetails.MemberRelationsList[0].LoginAttempt >= 5)
                                {
                                    mstrRedirectEmptyURL = "AccountLock";
                                }
                                else if (HttpContext.Current.Session["CallbackUrl"] != null)
                                {
                                    mstrRedirectEmptyURL = Convert.ToString(HttpContext.Current.Session["CallbackUrl"]);
                                }
                                else
                                {
                                    HttpContext.Current.Session["MemberDetails"] = lobjMemberDetails;
                                    if (HttpContext.Current.Session["PurchaseMilesClick"] != null)
                                    {
                                        Response.Redirect("PurchasePoints.aspx", false);
                                    }
                                    else
                                    {
                                        Response.Redirect("StatementSummary.aspx", false);
                                    }
                                }
                            }
                        }
                        else if (lobjMemberRelation.Status.Equals(Status.InActive))
                        {
                            mstrRedirectEmptyURL = "InActive";
                        }
                        else if (lobjMemberRelation.Status.Equals(Status.Blocked))
                        {
                            mstrRedirectEmptyURL = "AccountLock";
                        }
                        else if (lobjMemberRelation.Status.Equals(Status.Cancelled))
                        {
                            mstrRedirectEmptyURL = "Cancelled";
                        }
                        else if (lobjMemberRelation.Status.Equals(Status.Suspended))
                        {
                            mstrRedirectEmptyURL = "Suspended";
                        }
                        else
                        {
                            mstrRedirectEmptyURL = "AuthenticationFailed";
                        }
                        lobjModel.LogActivity(string.Format("Login ;", lstrMemberID), ActivityType.Login);
                    }
                    else
                    {
                        lobjModel.LogActivity(string.Format(ActivityConstants.Login, lstrMemberID, "AccountLock"), ActivityType.loginFail);
                        mstrRedirectEmptyURL = "AuthenticationFailed";
                    }
                }
                else
                {
                    mstrRedirectEmptyURL = "Invalid_MemberId";
                }
            }
            else
            {
                mstrRedirectEmptyURL = "Invalid_SecurityCode";
            }
            lobjModel.LogActivity(string.Format("Member Activation {0}: {1}", lstrMemberID, mstrRedirectEmptyURL), ActivityType.Activation);

        }
        catch (ApplicationException ex)
        {
            LoggingAdapter.WriteLog("SigninUser AppEx-" + ex.Message + ex.InnerException + ex.StackTrace);
            if (ex.Message.Equals("AccountLock"))
            {
                lobjModel.LogActivity(string.Format(ActivityConstants.Login, lstrMemberID, "AccountLock"), ActivityType.loginFail);
                mstrRedirectEmptyURL = "AccountLock";
            }
            else
            {
                lobjModel.LogActivity(string.Format(ActivityConstants.Login, lstrMemberID, "AuthenticationFailed"), ActivityType.loginFail);
                mstrRedirectEmptyURL = "CatchAuthenticationFailed";
            }
        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("BtnLoginValidation_Click Ex-" + ex.Message + ex.InnerException + ex.StackTrace);
            if (ex.Message.Equals("AccountLock"))
            {
                lobjModel.LogActivity(string.Format(ActivityConstants.Login, lstrMemberID, "AccountLock"), ActivityType.loginFail);
                mstrRedirectEmptyURL = "AccountLock";
            }
            else if (ex.Message.Equals("Login Authentication Failed"))
            {
                lobjModel.LogActivity(string.Format(ActivityConstants.Login, lstrMemberID, "AuthenticationFailed"), ActivityType.loginFail);
                mstrRedirectEmptyURL = "CatchAuthenticationFailed";
            }
            else
            {
                lobjModel.LogActivity(string.Format(ActivityConstants.Login, lstrMemberID, "AuthenticationFailed"), ActivityType.loginFail);
                mstrRedirectEmptyURL = "CatchAuthenticationFailed";
            }
        }
        string response = mstrRedirectEmptyURL + "+" + Attempt;
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "javascript:MemberLoginCodeBehind('" + response + "')", true);
    }
    protected void lnkBtnRefresh_Click(object sender, EventArgs e)
    {
        try
        {
            ImgCaptcha.ImageUrl = string.Format("~/captcha.ashx?refresh={0}", Guid.NewGuid());
        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("Activation.aspx lnkBtnRefresh_Click Exception: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
        }
    }
}