using ABC.Model; 
using Core.Platform.Member.Entites;
using Core.Platform.ProgramMaster.Entities;
using Framework.EnterpriseLibrary.Adapters;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Framework.EnterpriseLibrary.Security.Constants;
using Framework.EnterpriseLibrary.Security;
using Core.Platform.MemberActivity.Constants;
using Core.Platform.MemberActivity.Entities;
using System.Web.Script.Services;
using System.Web.Services;
using Core.Framework.PostHelper;
using Newtonsoft.Json;

public partial class ForceChangePassword : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //if (Session["MemberDetails"] == null)
            //{
            //    Response.Redirect("Login.aspx");
            //}
            //else
            //{
            if (Convert.ToString(Session["PasswordExpiry"]) == "PasswordExpired")
            {
                lblError.Text = "Your password have been expired";
            }
            else
            {
                lblError.Text = "";
            }
            MemberDetails lobjMemberDetails = Session["MemberDetails"] as MemberDetails;
            MemberLogin lobjMemberLogin = new MemberLogin();
            ABCModel lobjModel = new ABCModel();
            List<MemberLocalAttrDetails> objMemberLocalAttrDetails = new List<MemberLocalAttrDetails>();
            if (Session["MemberLocalAttrDetails"] == null)
            {
                ProgramDefinition lobjProgramDefinition = lobjModel.GetProgramMaster();
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
            hfRelationRef.Value = objMemberLocalAttrDetails[0].UserName;
            //}
        }
    }

    //[System.Web.Script.Services.ScriptMethod()]
    //[System.Web.Services.WebMethod]
    //public static string ForceChangePasswordForMember(string pstrOldPassword, string pstrNewPassword, string pstrConfirmPassword)
    //{
    //    bool lblChangePasswordStatus = false;
    //    string lstrMsg = string.Empty;
    //    string lstrMemberId = string.Empty;
    //    string lstrDecryptedPassword = string.Empty;
    //    string lstrEncryptedPassword = string.Empty;
    //    string lstrPasswordPolicy = string.Empty;
    //    string lstrProgramName = ConfigurationManager.AppSettings["ProgramName"].ToString();
    //    int lintPasswordReuseAttempt = Convert.ToInt32(ConfigurationManager.AppSettings["PasswordReuseAttempt"]);
    //    bool lblnInsertResult = false;
    //    bool lblnDeleteResult = false;
    //    int lintReuseAttempt = 0;
    //    ABCModel lobjModel = new ABCModel();
    //    MemberDetails lobjMemberDetails = null;
    //    MemberRelation lobjMemberRelation = null;
    //    ProgramDefinition lobjProgramDefinition = null;
    //    SystemParameter lobjSystemParameter = null;
    //    MemberPasswordHistory lobjMemberPasswordHistory = new MemberPasswordHistory();
    //    List<MemberPasswordHistory> lobjListOfMemberPasswordHistory = new List<MemberPasswordHistory>();
    //    List<MemberPasswordHistory> lobjListOfActivePasswordHistory = new List<MemberPasswordHistory>();

    //    try
    //    {
    //        lobjMemberDetails = HttpContext.Current.Session["MemberDetails"] as MemberDetails;
    //        lobjMemberRelation = lobjMemberDetails.MemberRelationsList.Find(lobj => lobj.RelationType.Equals(RelationType.LBMS));
    //        lobjProgramDefinition = lobjModel.GetProgramDetails(lstrProgramName);
    //        lobjSystemParameter = lobjModel.GetSystemParametres(lobjProgramDefinition.ProgramId);
    //        //lstrPasswordPolicy = ConfigurationSettings.AppSettings["PasswordPolicy"].ToString();
    //        Regex regex = new Regex((lobjSystemParameter.PasswordPolicy));
    //        //Regex regex = new Regex((lstrPasswordPolicy));

    //        lstrDecryptedPassword = DecryptPassword(lobjMemberDetails.MemberRelationsList.Find(lobj => lobj.RelationType.Equals(RelationType.LBMS)).WebPassword);
    //        if (pstrOldPassword.Equals(lstrDecryptedPassword))
    //        {
    //            if (pstrNewPassword.Trim().Equals(pstrConfirmPassword.Trim()))
    //            {
    //                if (regex.Match(pstrConfirmPassword).Success)
    //                {
    //                    int lintLength = Convert.ToInt32(ConfigurationManager.AppSettings["PartOfLengthForPassword"]);
    //                    bool lblnStatus = AllPartsOfLength(lobjMemberDetails.LastName.ToLower(), lintLength).Any(part => pstrNewPassword.ToLower().Contains(part));
    //                    if (!lblnStatus)
    //                    {
    //                        lstrEncryptedPassword = lobjModel.EncryptPassword(pstrConfirmPassword);
    //                        DateTime ldtCurrentDate = DateTime.Now;
    //                        DateTime ldtPasswordExpirationDate;
    //                        int lintAddDays = Convert.ToInt32(ConfigurationManager.AppSettings["LBMSPasswordExpiration"]);
    //                        ldtPasswordExpirationDate = ldtCurrentDate.AddDays(lintAddDays);
    //                        lobjListOfMemberPasswordHistory = lobjModel.GetMemberPasswordHistory(lobjMemberRelation.RelationReference);
    //                        lobjListOfActivePasswordHistory = lobjListOfMemberPasswordHistory.FindAll(lobj => lobj.IsActive.Equals(true));

    //                        if (lobjListOfActivePasswordHistory != null && lobjListOfActivePasswordHistory.Count > 0)
    //                        {
    //                            for (int i = 0; i < lobjListOfActivePasswordHistory.Count; i++)
    //                            {
    //                                if (DecryptPassword(lobjListOfActivePasswordHistory[i].MemberPassword).Equals(pstrConfirmPassword))
    //                                {
    //                                    lintReuseAttempt = lintReuseAttempt + 1;
    //                                }
    //                            }
    //                        }


    //                        if (lintReuseAttempt >= 1)
    //                        {
    //                            lstrMsg = "ResetPassword";
    //                        }
    //                        else
    //                        {
    //                            lobjMemberPasswordHistory.MemberId = lobjMemberRelation.RelationReference;
    //                            lobjMemberPasswordHistory.MemberPassword = lstrEncryptedPassword;
    //                            lobjMemberPasswordHistory.PasswordExpiryDate = ldtPasswordExpirationDate;
    //                            lobjMemberPasswordHistory.CreatedBy = lobjMemberRelation.RelationReference;
    //                            lobjMemberPasswordHistory.AdditionalDetails1 = "ForceChangePassword";

    //                            lblChangePasswordStatus = lobjModel.ChangePasswordForMember(lobjMemberRelation.RelationReference, lstrEncryptedPassword);

    //                            //lobjListOfActivePasswordHistory = lobjListOfMemberPasswordHistory.FindAll(lobj => lobj.IsActive.Equals(true));
    //                            if (lblChangePasswordStatus)
    //                            {
    //                                lstrMsg = "Success";
    //                                HttpContext.Current.Session["PasswordExpiry"] = "";
    //                                if (lobjListOfActivePasswordHistory != null && lobjListOfActivePasswordHistory.Count <= lintPasswordReuseAttempt)
    //                                {
    //                                    lobjMemberPasswordHistory.IsActive = true;
    //                                    lblnInsertResult = lobjModel.InsertMemberPasswordHistory(lobjMemberPasswordHistory);
    //                                }
    //                                else
    //                                {
    //                                    lobjMemberPasswordHistory.IsActive = false;
    //                                    lobjMemberPasswordHistory.UpdatedBy = lobjMemberRelation.RelationReference;
    //                                    lblnDeleteResult = lobjModel.DeleteMemberPasswordHistory(lobjListOfActivePasswordHistory[0].Id);
    //                                    if (lblnDeleteResult)
    //                                    {
    //                                        lobjMemberPasswordHistory.IsActive = true;
    //                                        lblnInsertResult = lobjModel.InsertMemberPasswordHistory(lobjMemberPasswordHistory);
    //                                    }
    //                                }
    //                                HttpContext.Current.Session.Abandon();
    //                            }
    //                            else
    //                            {
    //                                lstrMsg = "Failed";
    //                            }
    //                        }
    //                    }
    //                    else
    //                    {
    //                        lstrMsg = "InvalidPassword";
    //                    }
    //                }
    //                else
    //                {
    //                    lstrMsg = "InvalidFormat";
    //                }
    //            }
    //            else
    //            {
    //                lstrMsg = "NotMatched";
    //            }
    //        }
    //        else
    //        {
    //            lstrMsg = "Invalid";
    //        }
    //    }
    //    catch (Exception Ex)
    //    {
    //        LoggingAdapter.WriteLog("ForceChangePasswordForMember Exception: " + Ex.Message + Environment.NewLine + "Stack Trace: " + Ex.StackTrace);
    //        return lstrMsg;
    //    }
    //    return lstrMsg;
    //}

    [ScriptMethod()]
    [WebMethod]
    public static string ChangePassword(string pstrOldPassword, string pstrNewPassword)
    {
        string mstrRedirectEmptyURL = string.Empty;
        ABCModel lobjModel = new ABCModel();
        MemberDetails lobjMemberDetails = HttpContext.Current.Session["MemberDetails"] as MemberDetails;
        try
        {


            bool lobjchangepwd = false;
            if (pstrOldPassword.ToUpper().Equals(lobjMemberDetails.MemberRelationsList.Find(lobj => lobj.RelationType.Equals(RelationType.LBMS)).WebPassword))
            {
                lobjchangepwd = lobjModel.ChangePasswordForMember(lobjMemberDetails.MemberRelationsList.Find(lobj => lobj.RelationType.Equals(RelationType.LBMS)).RelationReference, pstrNewPassword.ToUpper());
                if (lobjchangepwd)
                {
                    lobjMemberDetails.MemberRelationsList.Find(lobj => lobj.RelationType.Equals(RelationType.LBMS)).WebPassword = pstrNewPassword.ToUpper();
                    HttpContext.Current.Session["MemberDetails"] = lobjMemberDetails;
                    mstrRedirectEmptyURL = "Success";
                }
                else
                {
                    mstrRedirectEmptyURL = "Failure";
                }
            }
            else
            {
                mstrRedirectEmptyURL = "Invalid password";
            }
            lobjModel.LogActivity(string.Format(ActivityConstants.ChangePassword, lobjMemberDetails.MemberRelationsList.Find(lobj => lobj.RelationType.Equals(RelationType.LBMS)).RelationReference, mstrRedirectEmptyURL), ActivityType.ResetPassword);
        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("ViewMemberProfile.aspx ChangePassword Exception: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
        }
        lobjModel.LogActivity(string.Format(ActivityConstants.ChangePassword, lobjMemberDetails.MemberRelationsList.Find(lobj => lobj.RelationType.Equals(RelationType.LBMS)).RelationReference, mstrRedirectEmptyURL), ActivityType.ResetPassword);
        return mstrRedirectEmptyURL;
    }
    public static IEnumerable<string> AllPartsOfLength(string value, int length)
    {
        for (int startPos = 0; startPos <= value.Length - length; startPos++)
        {
            yield return value.Substring(startPos, length);
        }
        yield break;
    }

    private static string DecryptPassword(string pstrPassword)
    {
        return RSAEncryptor.DecryptString(pstrPassword, RSASecurityConstant.KeySize, RSASecurityConstant.PrivateKey);
    }

}