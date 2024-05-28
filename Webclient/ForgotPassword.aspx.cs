using System;
using System.Web;
using ABC.Model;
using Core.Platform.MemberActivity.Entities;
using Core.Platform.Member.Entites;
using Framework.EnterpriseLibrary.Adapters;
using Core.Platform.OTP.Entities;
using System.Web.UI;
using Core.Platform.ProgramMaster.Entities;
using Core.Framework.PostHelper;
using Newtonsoft.Json;
using System.Collections.Generic;

public partial class ForgotPassword : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static string SendForgotPasswordOTP(string pstrMemberId)
    {
        ABCModel lobjModel = new ABCModel();
        bool lblnStatus = false;
        string lstrResponse = string.Empty;
        MemberDetails lobjMemberDetails = new MemberDetails();
        MemberRelation lobjMemberRelation = null;
        try
        {
            lobjModel.LogActivity(string.Format("Send ForgotPassword; OTP Request: #{0}", pstrMemberId), ActivityType.Activation);           
            ProgramDefinition lobjProgramDefinition = lobjModel.GetProgramMaster();
            //lobjMemberDetails = lobjModel.GetMemberDetails(pstrMemberId);
            lobjMemberDetails = lobjModel.GetMemberDetailsByUniqueAttribute(lobjProgramDefinition.ProgramId, pstrMemberId);
            if (lobjMemberDetails != null)
            {
                MemberLogin lobjMemberLogin = new MemberLogin();
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
                    lobjMemberRelation = lobjMemberDetails.MemberRelationsList.Find(lobj => lobj.RelationType.Equals(RelationType.LBMS));
                    if (lobjMemberRelation != null && lobjMemberRelation.Status.Equals(Status.Active) && lobjMemberRelation.IsAccountActivated.Equals(true))
                    {
                        string lstrSourceIpAddress = HttpContext.Current.Request.UserHostAddress;
                        lblnStatus = lobjModel.GenerateOTPForgotPassword(lobjMemberRelation.RelationReference, lstrSourceIpAddress);
                        if (lblnStatus.Equals(true))
                        {
                            lstrResponse = "Success";
                        }
                        else
                        {
                            lstrResponse = "Failure";
                        }
                    }
                    else
                    {
                        lstrResponse = "Not_Activated";
                    }
                }
                else
                {
                    lstrResponse = "CaseSensitive_MemberId";
                }
                    
            }
            else
            {
                lstrResponse = "Invalid_Member";
            }
            lobjModel.LogActivity(string.Format("Send ForgotPassword; OTP Response: #{0}-#{1}: ", pstrMemberId, lstrResponse), ActivityType.Activation);
        }
        catch (ApplicationException Appex)
        {
            LoggingAdapter.WriteLog("SendForgotPasswordOTP Appex-" + Appex.Message + Environment.NewLine + Appex.StackTrace + Environment.NewLine + Appex.InnerException);
            lstrResponse = "Failure";
        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("SendForgotPasswordOTP ex-" + ex.Message + Environment.NewLine + ex.StackTrace + Environment.NewLine + ex.InnerException);
            lstrResponse = "Failure";
        }
        return lstrResponse;
    }

    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static string ValidateForgotPasswordOTP(string pstrMemberId, string pstrOTP, string pstrPassword)
    {
        bool blnStatus = false;
        string lstrResponse = string.Empty;
        string lstrRelationReference = string.Empty;
        try
        {
            ABCModel lobjModel = new ABCModel();
            lobjModel.LogActivity(string.Format("Validate ForgotPassword; OTP Request: #{0}", pstrMemberId), ActivityType.ForgotPassword);
            //MemberDetails lobjMemberDetails = lobjModel.GetMemberDetails(pstrMemberId);
            MemberDetails lobjMemberDetails = new MemberDetails();
            ProgramDefinition lobjProgramDefinition = lobjModel.GetProgramMaster();
            lobjMemberDetails  = lobjModel.GetMemberDetailsByUniqueAttribute(lobjProgramDefinition.ProgramId, pstrMemberId);
            lstrRelationReference = lobjMemberDetails.MemberRelationsList.Find(lobj => lobj.RelationType.Equals(RelationType.LBMS)).RelationReference;
            if (lstrRelationReference != string.Empty)
            {
                if (CheckOTP(lstrRelationReference, pstrOTP))
                {
                    blnStatus = lobjModel.ChangePasswordForMember(lobjMemberDetails.MemberRelationsList.Find(lobj => lobj.RelationType.Equals(RelationType.LBMS)).RelationReference, pstrPassword.ToUpper());
                    if (blnStatus)
                    {
                        lstrResponse = "Success";
                    }
                    else
                    {
                        lstrResponse = "Failure";
                    }
                }
                else
                {
                    lstrResponse = "Invalid_OTP";
                }
            }
            else
            {
                lstrResponse = "NotExist";
            }

            lobjModel.LogActivity(string.Format("Validate ForgotPassword; OTP Response: #{0}-#{1}: ", pstrMemberId, lstrResponse), ActivityType.Activation);
        }
        catch (ApplicationException Appex)
        {
            LoggingAdapter.WriteLog("ValidateForgotPasswordOTP Appex-" + Appex.Message + Environment.NewLine + Appex.StackTrace + Environment.NewLine + Appex.InnerException);
            lstrResponse = "Failure";
        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("ValidateForgotPasswordOTP ex-" + ex.Message + Environment.NewLine + ex.StackTrace + Environment.NewLine + ex.InnerException);
            lstrResponse = "Failure";
        }
        return lstrResponse;
    }

    public static bool CheckOTP(string pstrMemberId, string pstrOTP)
    {
        try
        {
            bool mblnStatus = false;
            ABCModel lobjModel = new ABCModel();
            OTPDetails lobjOTPDetails = new OTPDetails();
            string lstrDestIpAddress = HttpContext.Current.Request.UserHostAddress;
            string lstrDestAddress = "Web";
            lobjOTPDetails.UniquerefID = pstrMemberId;
            lobjOTPDetails.OTP = Convert.ToInt32(pstrOTP);
            lobjOTPDetails.DestinationAddress = lstrDestIpAddress;
            lobjOTPDetails.Destination = lstrDestAddress;
            lobjOTPDetails.OtpType = OTPEnumTypes.FORGOTPWD.ToString();
            lobjOTPDetails.OtpEnumTypes = OTPEnumTypes.FORGOTPWD;
            lobjOTPDetails.SourceCode = Core.Platform.OTP.ConfigurationConstants.SourceCode.Web;
            ProgramDefinition lobjProgramDefinition = lobjModel.GetProgramMaster();
            lobjOTPDetails.ProgramId = lobjProgramDefinition.ProgramId;
            lobjOTPDetails.RelationType = Convert.ToInt32(RelationType.LBMS);
            mblnStatus = lobjModel.CheckOTPExist(lobjOTPDetails);
            return mblnStatus;
        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("ForgotPassword - CheckOTP Ex: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
            return false;
        }
    }
}