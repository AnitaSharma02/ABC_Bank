using ABC.Model;
using Core.Framework.PostHelper;
using Core.Platform.Member.Entites;
using Core.Platform.MemberActivity.Constants;
using Core.Platform.MemberActivity.Entities;
using Core.Platform.OTP.Entities;
using Core.Platform.ProgramMaster.Entities;
using Core.Platform.Transactions.Entites;
using Framework.EnterpriseLibrary.Adapters;
using System;
using System.Configuration;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;

public partial class Login : Page
{
    public string pstrMembershipRef { get; set; }
    public string ResendOTPEnableTime { get; set; }
    public static int lntCounterLimit = Convert.ToInt32(ConfigurationManager.AppSettings["CounterLimitForOTP"].ToString());

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            ResendOTPEnableTime = ConfigurationManager.AppSettings["ResendOTPEnableTime"].ToString();

            if (!IsPostBack)
            {
                Session["OTPCount"] = 0;
                HttpContext.Current.Session["IsForgetPasswordOTPValidated"] = "false";
                HttpContext.Current.Session["ResendOTPEnableTime"] = ResendOTPEnableTime;
                HttpContext.Current.Session["ResendOTPRequestTime"] = DateTime.Now;
            }
        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("ValidateOTP.aspx Page_Load Exception: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
        }
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
        string lstrMemberID = txtMemberId.Text;
        string lstrOTP = txtOTP.Text;
        bool pboolRememberMe = chkRememberMe.Checked;
        int lstrOTPCount = Convert.ToInt32(HttpContext.Current.Session["OTPCount"]);
        lstrOTPCount += 1;
        HttpContext.Current.Session["OTPCount"] = lstrOTPCount;
        string LoginRedirectionUrl = ConfigurationManager.AppSettings["LoginRedirectionUrl"];
        string strActivationPoints = ConfigurationManager.AppSettings["ActivationPoints"];
        int strActivationPointsExpiry = Convert.ToInt32(ConfigurationManager.AppSettings["ActivationPointsExpiry"]);
        string strActivationPointsAwarding = ConfigurationManager.AppSettings["ActivationPointsAwarding"];
        string strMD5password = string.Empty;
        try
        {
            ProgramDefinition lobjProgramDefinition = lobjModel.GetProgramMaster();
            MemberDetails lobjMemberDetails = lobjModel.GetMemberDetailsByUniqueAttribute(lobjProgramDefinition.ProgramId, lstrMemberID);
            string CallbackUrl = Request.QueryString["CallbackUrl"];
            strMD5password = lobjModel.GenerateSHA256(lobjMemberDetails.MemberRelationsList[0].RelationReference.Trim() + lstrOTP);
            MemberRelation lobjMemberRelations = lobjMemberDetails.MemberRelationsList.Find(x => x.RelationType.Equals(RelationType.LBMS));
            string lstrToken = string.Empty;
            if (lstrOTPCount < lntCounterLimit)
            {
                if (lobjMemberDetails != null)
                {
                    if (CheckOTP(lobjMemberDetails.MemberRelationsList.Find(x => x.RelationType.Equals(RelationType.LBMS)).RelationReference, lstrOTP))
                    {
                        mstrRedirectEmptyURL = "Success";
                        HttpContext.Current.Session["loginMsg"] = "1";
                        if (!lobjMemberDetails.MemberRelationsList.Find(x => x.RelationType.Equals(RelationType.LBMS)).IsAccountActivated)
                        {
                            if (lobjMemberDetails.MemberRelationsList.Find(x => x.RelationType.Equals(RelationType.LBMS)).Status.Equals(Status.InActive))
                            {
                                SearchMember lobjSearchMember = new SearchMember();
                                lobjSearchMember.UniquerefID = lobjMemberDetails.Email;
                                lobjSearchMember.ProgramId = lobjMemberDetails.ProgramId;
                                lobjSearchMember.RelationType = Convert.ToInt32(RelationType.LBMS);
                                lobjSearchMember.Password = strMD5password.Trim().ToUpper();
                                bool lblStatus = lobjModel.ActivateAccount(lobjSearchMember);
                                LoggingAdapter.WriteLog(string.Format("Activation CheckOTP{0}", lblStatus));
                                if (lblStatus)
                                {
                                    lobjMemberDetails.MemberRelationsList.Find(x => x.RelationType.Equals(RelationType.LBMS)).IsAccountActivated = true;
                                    lobjMemberDetails.MemberRelationsList.Find(x => x.RelationType.Equals(RelationType.LBMS)).Status = Status.Active;
                                    if (!string.IsNullOrEmpty(strActivationPointsAwarding) && strActivationPointsAwarding.ToUpper().ToString() == "YES")
                                    {
                                        TransactionDetails transactionDetails = new TransactionDetails()
                                        {
                                            TransactionType = (TransactionType)1,
                                            RelationReference = lobjMemberDetails.MemberRelationsList[0].RelationReference,
                                            Amounts = 0,
                                            Points = Convert.ToInt32(strActivationPoints),
                                            LoyaltyTxnType = (LoyaltyTxnType)2,
                                            ProgramId = lobjProgramDefinition.ProgramId,
                                            TransactionCurrency = "DEFAULT",
                                            RelationType = RelationType.LBMS,
                                            TransactionDate = DateTime.Now,
                                            ProcessingDate = DateTime.Now,
                                            ExpiryDate = DateTime.Now.AddMonths(strActivationPointsExpiry),
                                            ReconciledPoints = 0,
                                            ReconciledType = 1,
                                            Narration = "Bonus Points",
                                            MerchantName = "Activation Bonus Points",
                                            ExternalReference = "",
                                            AdditionalDetail = "",
                                            AdditionalDetails1 = ""
                                        };
                                        TransactionDetailsBreakage transactionDetailsBreakage = new TransactionDetailsBreakage()
                                        {
                                            IsBillable = true,
                                            SourceAmount = 0,
                                            SourceCurrency = "",
                                            TxnCurrency = "",
                                            TransactionSource = ""
                                        };
                                        transactionDetails.TransactionDetailBreakage = transactionDetailsBreakage;
                                        bool strresponse = lobjModel.InsertManualTransactionDetails(transactionDetails, lstrToken);

                                        LoggingAdapter.WriteLog("ExtSSO_oAuth - Activation Bonus Awarded : " + strresponse);
                                    }
                                }
                                else
                                {
                                    mstrRedirectEmptyURL = "Invalid_Credentials";
                                }
                            }
                        }
                        //............//
                        updateSessionId(HttpContext.Current);
                        if (pboolRememberMe)
                        {
                            HttpCookie cookie = new HttpCookie("ABCLogin");
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
                            //Session["FromSSOLogin"] = "0";

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
                                        Response.Redirect(LoginRedirectionUrl, false);
                                    }
                                }
                            }
                        }

                        lobjModel.LogActivity(string.Format("Login ;", lstrMemberID), ActivityType.Login);
                    }
                    else
                    {
                        mstrRedirectEmptyURL = "Invalid_OTP";
                    }
                    if (lobjMemberDetails != null)
                    {
                        Attempt = Convert.ToString(lobjMemberRelations.LoginAttempt) + ":" + lobjMemberRelations.Status;
                    }
                    else
                    {
                        Attempt = Convert.ToString(0) + ":" + "Active";
                    }
                }
                else
                {
                    mstrRedirectEmptyURL = "Invalid_MemberId";
                }
            }
            else
            {
                mstrRedirectEmptyURL = "Exceed OTP limit";
                string otp = Convert.ToString(HttpContext.Current.Session["ForgotPwdOTP"]);
                CheckOTP(lobjMemberDetails.MemberRelationsList.Find(x => x.RelationType.Equals(RelationType.LBMS)).RelationReference, otp);

                lobjModel.LogActivity("Login CheckOTP: " + mstrRedirectEmptyURL, ActivityType.loginFail);
                HttpContext.Current.Session.Abandon();
            }
            lobjModel.LogActivity(string.Format("Member Login {0}: {1}", lstrMemberID, mstrRedirectEmptyURL), ActivityType.Login);
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
    public static bool CheckOTP(string pstrmemberid, string pstrotp)
    {
        bool mblnstatus = false;
        try
        {
            ABCModel lobjmodel = new ABCModel();
            ProgramDefinition lobjprogramdefinition = lobjmodel.GetProgramMaster();
            string lstrdestipaddress = HttpContext.Current.Request.UserHostAddress;
            string lstrdestaddress = "web";
            OTPDetails lobjotpdetails = new OTPDetails
            {
                UniquerefID = pstrmemberid,
                OTP = Convert.ToInt32(pstrotp),
                DestinationAddress = lstrdestipaddress,
                Destination = lstrdestaddress,
                OtpType = OTPEnumTypes.ACTIVATION.ToString(),
                OtpEnumTypes = OTPEnumTypes.ACTIVATION,
                ProgramId = lobjprogramdefinition.ProgramId,
                RelationType = Convert.ToInt32(RelationType.LBMS)
            };
            mblnstatus = lobjmodel.CheckOTPExist(lobjotpdetails);
        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("Activation.aspx CheckOTP Exception: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
        }
        return mblnstatus;
    }

    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static string GenerateOTP(string pstrMemberId)
    {
        ABCModel lobjModel = new ABCModel();
        MemberDetails lobjMemberDetails = null;
        string mstrRedirectEmptyURL = string.Empty;
        bool lblstatus = false;
        try
        {
            lobjModel.LogActivity(string.Format("Generate Otp For ", pstrMemberId), ActivityType.Activation);
            MemberLogin lobjMemberLogin = new MemberLogin();
            ProgramDefinition lobjProgramDefinition = lobjModel.GetProgramMaster();
            lobjMemberDetails = lobjModel.GetMemberDetailsByUniqueAttribute(lobjProgramDefinition.ProgramId, pstrMemberId.Trim());

            if (lobjMemberDetails == null)
            {
                mstrRedirectEmptyURL = "Invalid Email";
            }
            else
            {
                MemberRelation lobjMemberRelation = lobjMemberDetails.MemberRelationsList.Find(x => x.RelationType.Equals(RelationType.LBMS));
                if (lobjMemberDetails != null && lobjMemberDetails.MemberRelationsList.Count > 0)
                {
                    string lstrSourceIpAddress = HttpContext.Current.Request.UserHostAddress;

                    SystemParameter lobjSystemParameter = lobjModel.GetSystemParametres(lobjProgramDefinition.ProgramId);
                    if (lobjProgramDefinition != null)
                    {
                        OTPDetails lobjOTPDetails = new OTPDetails
                        {
                            UniquerefID = lobjMemberRelation.RelationReference,
                            SourceAddress = lstrSourceIpAddress,
                            SourceCode = Core.Platform.OTP.ConfigurationConstants.SourceCode.Web,
                            ProgramId = lobjProgramDefinition.ProgramId,
                            RelationType = Convert.ToInt32(RelationType.LBMS),
                            OtpType = OTPEnumTypes.ACTIVATION.ToString(),
                            OtpEnumTypes = OTPEnumTypes.ACTIVATION,
                            AddExpirationTimeInMinutes = Convert.ToString(lobjSystemParameter.OTPExpirationTime)
                        };
                        lblstatus = lobjModel.SendOTPEmailAndSMS(lobjMemberDetails, "activation_otp", lobjOTPDetails, "");
                    }
                    if (lblstatus)
                    {
                        mstrRedirectEmptyURL = "Success";

                        lobjModel.LogActivity(string.Format(ActivityConstants.ActivationOTP, pstrMemberId, "Activation OTP Success"), ActivityType.ActivationOTPSuccess);
                    }
                    else
                    {
                        mstrRedirectEmptyURL = "OTPFAILED";
                        lobjModel.LogActivity(string.Format(ActivityConstants.ActivationOTP, pstrMemberId, "Activation OTP Failed"), ActivityType.ActivationOTPFailed);
                    }
                    //}
                    //else
                    //{
                    //    mstrRedirectEmptyURL = "Your Account is " + Convert.ToString(lobjMemberRelation.Status);
                    //    lobjModel.LogActivity(string.Format(ActivityConstants.ActivationOTP, pstrMemberId, mstrRedirectEmptyURL), ActivityType.ActivationOTPFailed);
                    //}
                    //}
                    //else
                    //{
                    //    mstrRedirectEmptyURL = "Already Activated";
                    //    lobjModel.LogActivity(string.Format(ActivityConstants.ActivationOTP, pstrMemberId, mstrRedirectEmptyURL), ActivityType.ActivationOTPFailed);
                    //}

                }
                else
                {
                    mstrRedirectEmptyURL = "Invalid Email";
                    lobjModel.LogActivity(string.Format(ActivityConstants.ActivationOTP, pstrMemberId, mstrRedirectEmptyURL), ActivityType.ActivationOTPFailed);
                }
            }

        }
        catch (Exception ex)
        {
            lobjModel.LogActivity(string.Format(ActivityConstants.ActivationOTP, pstrMemberId, "Exception Occurred"), ActivityType.ActivationOTPFailed);
            LoggingAdapter.WriteLog("Activation.aspx GenerateOTP Exception:" + ex.Message + Environment.NewLine + "InnerException:" + ex.InnerException + Environment.NewLine + "StackTrace:" + ex.StackTrace);
        }
        return mstrRedirectEmptyURL;
    }
}