using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using Core.Platform.Member.Entites;
using ABC.Model;
using Core.Platform.ProgramMaster.Entities;
using System.Text.RegularExpressions;
using Core.Platform.MemberActivity.Constants;
using Core.Platform.MemberActivity.Entities;
using System.Web.Script.Services;
using System.Web.Services;
using Framework.EnterpriseLibrary.Adapters;
using Core.Framework.PostHelper;
using Newtonsoft.Json;
using CB.IBE.Platform.AirClientModel;

public partial class ViewMemberProfile : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                MemberDetails lobjMemberDetails = Session["MemberDetails"] as MemberDetails;
                if (lobjMemberDetails == null)
                {
                    Response.Redirect("Index.aspx", false);
                }
                PopulateProfile();
            }
        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("ViewMemberProfile.aspx Page_Load Exception: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
        }
    }

    private void PopulateProfile()
    {
        try
        {
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
            labelAddressValue.Text = lobjMemberDetails.Address;
            labelMobileNo.Text = lobjMemberDetails.MobileNumber;
            labelEmailValue.Text = lobjMemberDetails.Email;
            labelMemberNameValue.Text = lobjMemberDetails.FullName;
            hfRelationRef.Value = lobjMemberDetails.FullName;
            lblGender.Text = lobjMemberDetails.Gender.ToLower() == "m" ? "Male" : "Female";
            lblNationality.Text = string.IsNullOrEmpty(lobjMemberDetails.Nationality) ? "NA" : lobjMemberDetails.Nationality;
        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("ViewMemberProfile.aspx PopulateProfile Exception: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
        }
    }

    public void ClearTextBoxes(TextBox[] pobjTextBoxArray)
    {
        foreach (TextBox t in pobjTextBoxArray)
        {
            t.Text = string.Empty;
        }
    }

    [ScriptMethod()]
    [WebMethod]
    public static string ChangePassword(string pstrOldPassword, string pstrNewPassword)
    {
        string mstrRedirectEmptyURL = string.Empty;
        ABCModel lobjModel = new ABCModel();
        MemberDetails lobjMemberDetails = HttpContext.Current.Session["MemberDetails"] as MemberDetails;
        try
        {
            pstrNewPassword = lobjModel.GenerateSHA256(lobjMemberDetails.MemberRelationsList[0].RelationReference + pstrNewPassword);
            pstrOldPassword = lobjModel.GenerateSHA256(lobjMemberDetails.MemberRelationsList[0].RelationReference + pstrOldPassword);
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
}