using System;
using System.Web;
using System.Web.UI;
using ABC.Model;
using Core.Platform.Member.Entites;
using Core.Platform.ProgramMaster.Entities;
using Core.Platform.MemberActivity.Entities;
using Framework.EnterpriseLibrary.Adapters;
using Core.Platform.MemberActivity.Constants;
using Core.Platform.OTP.Entities;
using Core.Framework.PostHelper;
using Newtonsoft.Json;
using System.Collections.Generic;

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
            //lobjMemberDetails = lobjModel.GetMemberDetails(pstrMemberId.Trim());
            if (lobjMemberDetails == null)
            {
                mstrRedirectEmptyURL = "Invalid Email";
            }
            else
            {
                MemberRelation lobjMemberRelation = lobjMemberDetails.MemberRelationsList.Find(x => x.RelationType.Equals(RelationType.LBMS));
                if (lobjMemberDetails != null && lobjMemberDetails.MemberRelationsList.Count > 0)
                {
                    List<MemberLocalAttrDetails> objMemberLocalAttrDetails = new List<MemberLocalAttrDetails>();
                    if (HttpContext.Current.Session["MemberLocalAttrDetails"] == null)
                    {
                        Token lobjToken = lobjMemberLogin.GenerateToken("['LOGIN','" + lobjMemberDetails.MemberRelationsList[0].RelationReference + "']");
                        var lobjlocalattrDynamic = JsonConvert.DeserializeObject<Root>(Convert.ToString(lobjMemberLogin.GetMemberLocalAttrDetails(lobjMemberDetails.MemberRelationsList[0].RelationReference.ToString(), lobjProgramDefinition.ProgramId.ToString(), (int)RelationType.LBMS, lobjToken.AccessToken)));
                        Results lobjlocalAttrResults = JsonConvert.DeserializeObject<Results>(lobjlocalattrDynamic.results.ToString());
                        if (lobjlocalAttrResults.IsSucessful)
                        {
                            List<MemberLocalAttrDetails> lobjMemberLocalAttrDetails = JsonConvert.DeserializeObject<List<MemberLocalAttrDetails>>(lobjlocalAttrResults.ReturnObject.ToString());
                            HttpContext.Current.Session["MemberLocalAttrDetails"] = lobjMemberLocalAttrDetails;
                            objMemberLocalAttrDetails = HttpContext.Current.Session["MemberLocalAttrDetails"] as List<MemberLocalAttrDetails>;
                        }
                    }
                    else
                    {
                        objMemberLocalAttrDetails = HttpContext.Current.Session["MemberLocalAttrDetails"] as List<MemberLocalAttrDetails>;
                    }
                    string User_name = objMemberLocalAttrDetails[0].UserName;
                    if (User_name.EndsWith(pstrMemberId))
                    {
                        if (!lobjMemberRelation.IsAccountActivated)
                        {
                            if (lobjMemberRelation.Status.Equals(Status.InActive))
                            {
                                string lstrSourceIpAddress = HttpContext.Current.Request.UserHostAddress;
                                // lblstatus = lobjModel.GenerateOTPByMemberId(lobjMemberRelation.RelationReference, lstrSourceIpAddress);

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
                                    HttpContext.Current.Session["ActivationMemberDetails"] = lobjMemberDetails;
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
                        mstrRedirectEmptyURL = "CaseSensitive_MemberId";
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
            lobjModel.LogActivity(string.Format(ActivityConstants.ActivationOTP, pstrMemberId, "Invalid Security Code"), ActivityType.ActivationOTPFailed);
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
        string lstrPwdPolicy = string.Empty;
        try
        {
            ABCModel lobjModel = new ABCModel();
            ProgramDefinition lobjProgramDefinition = lobjModel.GetProgramMaster();
            SystemParameter lobjPasswordPolicy = lobjModel.GetSystemParametres(lobjProgramDefinition.ProgramId);
            if (lobjPasswordPolicy != null)
            {
                lstrPwdPolicy = lobjPasswordPolicy.PasswordPolicy;
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
            ImgCaptcha.ImageUrl = string.Format("~/captcha.ashx?refresh={0}", Guid.NewGuid());
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
            string pstrSecurityCode = txtSecurityCode.Text;
            string strMD5password = string.Empty;
            try
            {
                if (HttpContext.Current.Session["CAPTCHA"].ToString().Equals(pstrSecurityCode))
                {
                    ProgramDefinition lobjProgramDefinition = lobjModel.GetProgramMaster();
                    SystemParameter lobjPasswordPolicy = lobjModel.GetSystemParametres(lobjProgramDefinition.ProgramId);
                    if (lobjPasswordPolicy != null)
                    {
                        lobjMemberDetails = lobjModel.GetMemberDetailsByUniqueAttribute(lobjProgramDefinition.ProgramId, pstrMemberId.Trim());
                        if (lobjMemberDetails != null && lobjMemberDetails.MemberRelationsList.Count > 0)
                        {
                            List<MemberLocalAttrDetails> objMemberLocalAttrDetails = new List<MemberLocalAttrDetails>();
                            if (Session["MemberLocalAttrDetails"] == null)
                            {
                                MemberLogin lobjMemberLogin = new MemberLogin();
                                Token lobjToken = lobjMemberLogin.GenerateToken("['LOGIN','" + lobjMemberDetails.MemberRelationsList[0].RelationReference + "']");
                                var lobjlocalattrDynamic = JsonConvert.DeserializeObject<Root>(Convert.ToString(lobjMemberLogin.GetMemberLocalAttrDetails(lobjMemberDetails.MemberRelationsList[0].RelationReference.ToString(), lobjProgramDefinition.ProgramId.ToString(), (int)RelationType.LBMS, lobjToken.AccessToken)));
                                Results lobjlocalAttrResults = JsonConvert.DeserializeObject<Results>(lobjlocalattrDynamic.results.ToString());
                                if (lobjlocalAttrResults.IsSucessful)
                                {
                                    List<MemberLocalAttrDetails> lobjMemberLocalAttrDetails = JsonConvert.DeserializeObject<List<MemberLocalAttrDetails>>(lobjlocalAttrResults.ReturnObject.ToString());
                                    Session["MemberLocalAttrDetails"] = lobjMemberLocalAttrDetails;
                                    objMemberLocalAttrDetails = Session["MemberLocalAttrDetails"] as List<MemberLocalAttrDetails>;
                                }
                            }
                            else
                            {
                                objMemberLocalAttrDetails = Session["MemberLocalAttrDetails"] as List<MemberLocalAttrDetails>;
                            }
                            string User_name = objMemberLocalAttrDetails[0].UserName;
                            if (User_name.EndsWith(pstrMemberId))
                            {
                                strMD5password = lobjModel.GenerateMD5(pstrMemberId.Trim() + pstrPwd);
                                MemberRelation lobjMemberRelation = lobjMemberDetails.MemberRelationsList.Find(x => x.RelationType.Equals(RelationType.LBMS));
                                if (lobjMemberRelation.IsAccountActivated)
                                {
                                    mstrRedirectEmptyURL = "Account_Activated";
                                }
                            }
                            else
                            {
                                mstrRedirectEmptyURL = "CaseSensitive_MemberId";
                            }
                        }
                        else
                        {
                            mstrRedirectEmptyURL = "Invalid_MemberId";
                        }
                        if (mstrRedirectEmptyURL.Equals(string.Empty))
                        {
                            lobjMemberDetails.MemberRelationsList.Find(x => x.RelationType.Equals(RelationType.LBMS)).WebPassword = strMD5password.Trim().ToUpper();
                            if (CheckOTP(lobjMemberDetails.MemberRelationsList.Find(x => x.RelationType.Equals(RelationType.LBMS)).RelationReference, pstrOTP))
                            {
                                SearchMember lobjSearchMember = new SearchMember();
                                lobjSearchMember.UniquerefID = lobjMemberDetails.Email;
                                lobjSearchMember.ProgramId = lobjMemberDetails.ProgramId;
                                lobjSearchMember.RelationType = Convert.ToInt32(RelationType.LBMS);
                                lobjSearchMember.Password = strMD5password.Trim().ToUpper();
                                lblStatus = lobjModel.ActivateAccount(lobjSearchMember);
                                if (lblStatus)
                                {
                                    mstrRedirectEmptyURL = "Success";
                                    HttpContext.Current.Session["loginMsg"] = "1";
                                    HttpContext.Current.Session["ActivationMemberDetails"] = null;
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
                }
                else
                {
                    mstrRedirectEmptyURL = "Invalid_SecurityCode";
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