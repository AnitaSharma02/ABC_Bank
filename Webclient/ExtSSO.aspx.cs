using ABC.Model;
using Core.Framework.PostHelper;
using Core.Platform.Member.Entites;
using Core.Platform.MemberActivity.Constants;
using Core.Platform.MemberActivity.Entities;
using Core.Platform.ProgramMaster.Entities;
using Framework.EnterpriseLibrary.Adapters;
using Newtonsoft.Json;
using System;
using System.Web;
using System.Web.UI;
using System.Configuration;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Core.Platform.Transactions.Entites;

public partial class ExtSSO : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session["AvailablePoints"] = null;
            string lstrMessage = string.Empty;
            bool lblStatus = false;
            string strMD5password = string.Empty;
            string strActivationPoints= ConfigurationManager.AppSettings["ActivationPoints"];
            try
            {
                string lstrHostUrl = string.Empty;
                string lstrSourceUrl = string.Empty;
                Uri lobjUri = new Uri(Request.UrlReferrer.AbsoluteUri);
                lstrHostUrl = lobjUri.Host;
                lstrSourceUrl = HttpContext.Current.Request.UrlReferrer.ToString();
                LoggingAdapter.WriteLog("Source URL: " + lstrSourceUrl + " Host Address: " + lstrHostUrl);
            }
            catch
            {
            }
            string lstrToken = string.Empty;
            if (Request.QueryString.Count > 0)
            {
                lstrToken = Convert.ToString(Request.QueryString["Token"]);
            }
            else if (Request.Form.Count > 0)
            {
                lstrToken = Request.Form["Token"];
            }
            if (!string.IsNullOrEmpty(lstrToken))
            {
                ABCModel lobjModel = new ABCModel();
                try
                {
                    LoggingAdapter.WriteLog("ExtSSO_oAuth - AccessToken Data: " + lstrToken + Environment.NewLine + "DateTime -- " + DateTime.Now);
                    if (!string.IsNullOrEmpty(lstrToken))
                    {
                        MemberLogin lobjMemberLogin = new MemberLogin();
                        lobjModel.LogActivity(string.Format("ExtSSO_oAuth; AccessToken Data:  {0}: DateTime {1}", lstrToken, DateTime.Now), ActivityType.Login);
                        ProgramDefinition lobjProgramDefinition = lobjModel.GetProgramMaster();
                        var lobjDynamic = JsonConvert.DeserializeObject<Root>(Convert.ToString(lobjMemberLogin.GetMemberProfile(lobjProgramDefinition.ProgramId.ToString(), (int)RelationType.LBMS, lstrToken)));
                        Results lobjResults = JsonConvert.DeserializeObject<Results>(lobjDynamic.results.ToString());
                        if (lobjResults.IsSucessful)
                        {
                            MemberDetails lobjMemberDetails = JsonConvert.DeserializeObject<MemberDetails>(lobjResults.ReturnObject.ToString());
                            MemberRelation lobjMemberRelation = lobjMemberDetails.MemberRelationsList.Find(lobj => lobj.RelationType.Equals(RelationType.LBMS));
                            if (lobjMemberDetails != null && !string.IsNullOrEmpty(lobjMemberDetails.FullName))
                            {
                                LoggingAdapter.WriteLog("ExtSSO_oAuth - Member Validation: TRUE Member Status: " + lobjMemberRelation.Status.ToString());
                                MemberActivitySession lobjMemberActivitySession = HttpContext.Current.Session["MemberActivitySession"] as MemberActivitySession;
                                lobjMemberActivitySession.ReferenceNumber = lobjMemberRelation.RelationReference;
                                lobjMemberActivitySession.ActivityType = ActivityType.loginsuccessful;
                                lobjMemberActivitySession.ProgramId = lobjProgramDefinition.ProgramId;
                                lobjModel.UpdateMemberShipActivitySession(lobjMemberActivitySession);
                                string LoginRedirectionUrl = ConfigurationManager.AppSettings["LoginRedirectionUrl"];
                                if (lobjMemberRelation.Status == Status.Active && lobjMemberRelation.IsAccountActivated)
                                {
                                    lobjMemberRelation.Id = lobjMemberDetails.Id;
                                    lobjModel.ResetLoginAttempt(lobjMemberDetails.MemberRelationsList.Find(x => x.RelationType.Equals(RelationType.LBMS)));
                                    lobjModel.LogActivity(string.Format(ActivityConstants.Login, lobjMemberRelation.RelationReference, "ExtSSO_oAuth login successful"), ActivityType.loginsuccessful);
                                    Session["MemberDetails"] = lobjMemberDetails;
                                    Session["FromSSOLogin"] = "1";
                                    LoggingAdapter.WriteLog("ExtSSO_oAuth - Member Account Activated: TRUE");
                                    divErrorMsg.Style.Add("Display", "None");
                                    if (lobjMemberDetails.MemberRelationsList.Find(lobj => lobj.RelationType.Equals(RelationType.LBMS)).ForceChangePassword)
                                    {
                                        HttpContext.Current.Session["MemberDetails"] = lobjMemberDetails;
                                        Response.Redirect("ForceChangePassword.aspx", false);
                                    }
                                    else
                                    {
                                        Response.Redirect(LoginRedirectionUrl, false);
                                    }
                                }
                                else if (lobjMemberRelation.Status == Status.Cancelled)
                                {
                                    lstrMessage = "Your account status is cancelled. Please contact to admin.";
                                    LoggingAdapter.WriteLog("ExtSSO_oAuth; " + lstrMessage);
                                    lobjModel.LogActivity(string.Format("ExtSSO_oAuth; ErrorMsg {0}", lstrMessage), ActivityType.loginFail);
                                    divErrorMsg.Style.Add("Display", "Block");
                                    lblMessage.Text = lstrMessage;
                                }
                                else if (lobjMemberRelation.Status == Status.Suspended)
                                {
                                    lstrMessage = "Your account status is suspended. Please contact to admin.";
                                    LoggingAdapter.WriteLog("ExtSSO_oAuth; " + lstrMessage);
                                    lobjModel.LogActivity(string.Format("ExtSSO_oAuth; ErrorMsg {0}", lstrMessage), ActivityType.loginFail);
                                    divErrorMsg.Style.Add("Display", "Block");
                                    lblMessage.Text = lstrMessage;
                                }
                                else
                                {
                                    SearchMember lobjSearchMember = new SearchMember();
                                    lobjSearchMember.UniquerefID = lobjMemberDetails.Email;
                                    lobjSearchMember.ProgramId = lobjMemberDetails.ProgramId;
                                    lobjSearchMember.RelationType = Convert.ToInt32(RelationType.LBMS);
                                    lobjSearchMember.Password = lobjMemberDetails.MemberRelationsList.Find(x => x.RelationType.Equals(RelationType.LBMS)).WebPassword.Trim().ToUpper();
                                    lblStatus = lobjModel.ActivateAccount(lobjSearchMember);
                                    LoggingAdapter.WriteLog(string.Format("Activation Activation{0}", lblStatus));

                                    if (lblStatus)
                                    {
                                        TransactionDetails transactionDetails = new TransactionDetails()
                                        {
                                            TransactionType = (TransactionType)1,
                                            RelationReference = lobjMemberRelation.RelationReference,
                                            Amounts = 1000,
                                            Points =Convert.ToInt32(strActivationPoints),
                                            LoyaltyTxnType = (LoyaltyTxnType)2,
                                            ProgramId = lobjProgramDefinition.ProgramId,
                                            TransactionCurrency = "DEFAULT",
                                            RelationType = RelationType.LBMS,
                                            TransactionDate = DateTime.Now,
                                            ProcessingDate = DateTime.Now,
                                            ExpiryDate = DateTime.Now,
                                            ReconciledPoints = 0,
                                            ReconciledType = 1,
                                            Narration = "Bonus Points",
                                            MerchantName = "",
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
                                        bool response = lobjModel.InsertManualTransactionDetails(transactionDetails, lstrToken);
                                        if (response == true)
                                        {
                                            Response.Redirect(LoginRedirectionUrl, false);
                                        }
                                    }
                                }
                            }
                            else
                            {
                                lstrMessage = "Incorrect member details.";
                                lobjModel.LogActivity(string.Format("ExtSSO_oAuth; ErrorMsg {0}", lstrMessage), ActivityType.loginFail);
                                LoggingAdapter.WriteLog("ExtSSO_oAuth - Member Validation: FALSE");
                                divErrorMsg.Style.Add("Display", "Block");
                                lblMessage.Text = lstrMessage;
                            }
                        }
                        else
                        {
                            lstrMessage = "Incorrect member details.";
                            lobjModel.LogActivity(string.Format("ExtSSO_oAuth; ErrorMsg {0}", lstrMessage), ActivityType.loginFail);
                            LoggingAdapter.WriteLog("ExtSSO_oAuth - Member Validation: FALSE");
                            divErrorMsg.Style.Add("Display", "Block");
                            lblMessage.Text = lstrMessage;
                        }
                    }
                    else
                    {
                        lstrMessage = "Invalid Request.";
                        lobjModel.LogActivity(string.Format("ExtSSO_oAuth; ErrorMsg {0}", lstrMessage), ActivityType.loginFail);
                        divErrorMsg.Style.Add("Display", "Block");
                        lblMessage.Text = lstrMessage;
                        LoggingAdapter.WriteLog("ExtSSO_oAuth - Decrypte Data: NULL");
                    }
                }
                catch (Exception ex)
                {
                    lobjModel.LogActivity(string.Format("ExtSSO_oAuth; ErrorMsg {0}", ex.Message), ActivityType.loginFail);
                    LoggingAdapter.WriteLog("ExtSSO_oAuth Ex: " + ex.Message + Environment.NewLine + "Stack Trace: " + ex.StackTrace);
                }
            }
            else
            {
                lstrMessage = "Invalid Request.";
                divErrorMsg.Style.Add("Display", "Block");
                lblMessage.Text = lstrMessage;
                LoggingAdapter.WriteLog("ExtSSO_oAuth - Request form count less than zero");
            }
            if (!string.IsNullOrEmpty(lstrMessage))
            {
                updProgress.Visible = false;
            }
        }
    }
    private static string Encrypt(string clearText)
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
        return clearText;
    }
}

