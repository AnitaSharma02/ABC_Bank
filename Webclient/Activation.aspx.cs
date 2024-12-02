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
using System.Web;
using System.Web.UI;

public partial class Activation : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
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
                    if (!lobjMemberRelation.IsAccountActivated)
                    {
                        if (lobjMemberRelation.Status.Equals(Status.InActive))
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
                        }
                        else
                        {
                            mstrRedirectEmptyURL = "Your Account is " + Convert.ToString(lobjMemberRelation.Status);
                            lobjModel.LogActivity(string.Format(ActivityConstants.ActivationOTP, pstrMemberId, mstrRedirectEmptyURL), ActivityType.ActivationOTPFailed);
                        }
                    }
                    else
                    {
                        mstrRedirectEmptyURL = "Already Activated";
                        lobjModel.LogActivity(string.Format(ActivityConstants.ActivationOTP, pstrMemberId, mstrRedirectEmptyURL), ActivityType.ActivationOTPFailed);
                    }

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
    public static string GetPasswordPolicy()
    {
        LoggingAdapter.WriteLog("GetPasswordPolicy Program", "test");
        string lstrPwdPolicy = string.Empty;
        try
        {
            ABCModel lobjModel = new ABCModel();
            ProgramDefinition lobjProgramDefinition = lobjModel.GetProgramMaster();
            LoggingAdapter.WriteLog("GetPasswordPolicy Program", lobjProgramDefinition.ProgramName);
            SystemParameter lobjPasswordPolicy = lobjModel.GetSystemParametres(lobjProgramDefinition.ProgramId);
            if (lobjPasswordPolicy != null)
            {
                lstrPwdPolicy = lobjPasswordPolicy.PasswordPolicy;
                LoggingAdapter.WriteLog(string.Format("GetPasswordPolicy {0}", lstrPwdPolicy));

            }
        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("Activation.aspx GetPasswordPolicy Exception: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
        }
        return lstrPwdPolicy;
    }

    protected void lnkBtnRefresh_Click(object sender, EventArgs e)
    {
        try
        {
            //  ImgCaptcha.ImageUrl = string.Format("~/captcha.ashx?refresh={0}", Guid.NewGuid());
        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("Activation.aspx lnkBtnRefresh_Click Exception: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
        }
    }

    protected void BtnActivationValidation_Click(object sender, EventArgs e)
    {
        try
        {
            ABCModel lobjModel = new ABCModel();
            MemberDetails lobjMemberDetails = null;
            bool lblStatus = false;
            string mstrRedirectEmptyURL = string.Empty;
            string pstrOTP = txtOTP.Text;
            string pstrPwd = txtPassword.Text;
            string pstrMemberId = txtMemberId.Text;
            // string pstrSecurityCode = txtSecurityCode.Text;
            string strMD5password = string.Empty;
            string lstrToken = string.Empty;
            string strActivationPoints = ConfigurationManager.AppSettings["ActivationPoints"];
            int strActivationPointsExpiry = Convert.ToInt32(ConfigurationManager.AppSettings["ActivationPointsExpiry"]);
            string strActivationPointsAwarding = ConfigurationManager.AppSettings["ActivationPointsAwarding"];
            try
            {
                ProgramDefinition lobjProgramDefinition = lobjModel.GetProgramMaster();
                SystemParameter lobjPasswordPolicy = lobjModel.GetSystemParametres(lobjProgramDefinition.ProgramId);
                if (lobjPasswordPolicy != null)
                {
                    lobjMemberDetails = lobjModel.GetMemberDetailsByUniqueAttribute(lobjProgramDefinition.ProgramId, pstrMemberId.Trim());
                    LoggingAdapter.WriteLog(string.Format("Activation OtpValidation {0}", lobjMemberDetails.MemberRelationsList[0].RelationReference));

                    if (lobjMemberDetails != null && lobjMemberDetails.MemberRelationsList.Count > 0)
                    {
                        MemberRelation lobjMemberRelation = lobjMemberDetails.MemberRelationsList.Find(x => x.RelationType.Equals(RelationType.LBMS));
                        LoggingAdapter.WriteLog(string.Format("Activation {0}", lobjMemberRelation));

                        if (lobjMemberRelation.IsAccountActivated)
                        {
                            mstrRedirectEmptyURL = "Account_Activated";
                        }
                    }
                    else
                    {
                        mstrRedirectEmptyURL = "Invalid_MemberId";
                    }
                    LoggingAdapter.WriteLog(string.Format("Activation {0}", mstrRedirectEmptyURL));

                    if (mstrRedirectEmptyURL.Equals(string.Empty))
                    {
                        LoggingAdapter.WriteLog(string.Format("Password {0}", pstrPwd));
                        LoggingAdapter.WriteLog(string.Format("RelationRef {0}", lobjMemberDetails.MemberRelationsList[0].RelationReference.Trim()));
                        strMD5password = lobjModel.GenerateSHA256(lobjMemberDetails.MemberRelationsList[0].RelationReference.Trim() + pstrPwd);
                        LoggingAdapter.WriteLog(string.Format("Activation {0}", strMD5password));

                        lobjMemberDetails.MemberRelationsList.Find(x => x.RelationType.Equals(RelationType.LBMS)).WebPassword = strMD5password.Trim().ToUpper();
                        if (CheckOTP(lobjMemberDetails.MemberRelationsList.Find(x => x.RelationType.Equals(RelationType.LBMS)).RelationReference, pstrOTP))
                        {
                            SearchMember lobjSearchMember = new SearchMember();
                            lobjSearchMember.UniquerefID = lobjMemberDetails.Email;
                            lobjSearchMember.ProgramId = lobjMemberDetails.ProgramId;
                            lobjSearchMember.RelationType = Convert.ToInt32(RelationType.LBMS);
                            lobjSearchMember.Password = strMD5password.Trim().ToUpper();
                            lblStatus = lobjModel.ActivateAccount(lobjSearchMember);
                            LoggingAdapter.WriteLog(string.Format("Activation CheckOTP{0}", lblStatus));

                            if (lblStatus)
                            {
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
                                    bool response = lobjModel.InsertManualTransactionDetails(transactionDetails, lstrToken);

                                    LoggingAdapter.WriteLog("ExtSSO_oAuth - Activation Bonus Awarded : " + response);
                                }

                                mstrRedirectEmptyURL = "Success";
                                HttpContext.Current.Session["loginMsg"] = "1";
                                // HttpContext.Current.Session["ActivationMemberDetails"] = null;
                            }
                            else
                            {
                                mstrRedirectEmptyURL = "Invalid_Credentials";
                            }
                        }
                        else
                        {
                            mstrRedirectEmptyURL = "Invalid_OTP";
                        }
                    }
                }
                else
                {
                    mstrRedirectEmptyURL = "Invalid_Program";
                }

                lobjModel.LogActivity(string.Format("Member Activation {0}: {1}", pstrMemberId, mstrRedirectEmptyURL), ActivityType.Activation);
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("Activation.aspx BtnActivationValidation_Click Exception:" + ex.Message + Environment.NewLine + "InnerException:" + ex.InnerException + Environment.NewLine + "StackTrace:" + ex.StackTrace);
            }
            mstrRedirectEmptyURL = mstrRedirectEmptyURL + "|" + pstrMemberId;

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "javascript:MemberActivationCodeBehind('" + mstrRedirectEmptyURL + "')", true);
        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("Activation.aspx BtnActivationValidation_Click Exception: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
        }
    }
}