using System;
using System.Web;
using Core.Platform.Member.Entites;
using ABC.Model;
using Core.Platform.MemberActivity.Entities;
using Framework.EnterpriseLibrary.Adapters;

public partial class SiteMaster : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (HttpContext.Current.Session["MemberDetails"] != null)
            {
                MemberDetails lobjMemberDetails = Session["MemberDetails"] as MemberDetails;               
                lnkLogin.Visible = false;
                lnkRegister.Visible = false;
                txtWelcome.Visible = false;
                limyaccount.Visible = true;
                dvPoints.Visible = true;
                HeaderTot.Visible = true;              
                totAvbPointDiv.Visible = true;
                lblMemberName.Text = "<span>" + lobjMemberDetails.FullName + "</span>";
                if (!string.IsNullOrEmpty(Convert.ToString(HttpContext.Current.Session["FromSSOLogin"]))
                    && Convert.ToString(HttpContext.Current.Session["FromSSOLogin"]) == "1")
                {
                    //lnkloginlogout.Attributes.Add("style", "display:none");
                    liLogout.Visible = true;
                }
                else
                {
                    liLogout.Visible = true;
                    if (!Request.Path.Contains("ForceChangePassword.aspx"))
                    {
                        if (lobjMemberDetails.MemberRelationsList.Find(lobj => lobj.RelationType.Equals(RelationType.LBMS)).ForceChangePassword)
                        {
                            Response.Redirect("ForceChangePassword.aspx", false);
                        }
                    }
                }
                if (HttpContext.Current.Session["MobIndex"] != null)
                {
                    lnkRegister.Visible = false;
                    //mob_lnkRegister.Visible = false;
                    mob_lnkLogin.Visible=false;
                    lnkloginlogout.Visible = false;
                    mob_lnkloginlogout.Visible = false;
                }
            }
            else
            {
                dvPoints.Visible = false;
                HeaderTot.Visible = false;
                //lnkloginlogout.Attributes.Add("style", "display:block");
                liwelcome.Visible = true;
            }
        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("SiteMaster.master - Page_Load Exception: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
        }
    }
    public void SignOut(object sender, EventArgs e)
    {
        try
        {
            string confirmValue = Request.Form["confirm_value"];
            if (confirmValue == "Yes")
            {
                ABCModel lobjModel = new ABCModel();
                lobjModel.LogActivity(string.Format("Member Logout"), ActivityType.Logout);
                Session.Abandon();
                Response.Redirect("Index.aspx", false);
            }
            else
            {
                ABCModel lobjModel = new ABCModel();
                lobjModel.LogActivity(string.Format("Member not Logout"), ActivityType.Logout);
            }
        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("SiteMaster.master - SignOut Exception: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
        }
    }
}
