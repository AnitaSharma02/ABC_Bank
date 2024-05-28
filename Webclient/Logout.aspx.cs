using Core.Platform.MemberActivity.Entities;
using Framework.EnterpriseLibrary.Adapters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ABC.Model;
using Core.Platform.Member.Entites;

public partial class Logout : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            MemberDetails lobjMemberDetails = HttpContext.Current.Session["MemberDetails"] as MemberDetails;
            string lstrName = lobjMemberDetails != null ? lobjMemberDetails.FullName : string.Empty;
            ABCModel objQIBRewards = new ABCModel();
            objQIBRewards.LogActivity(string.Format("Member Logout Page;"+ lstrName), ActivityType.Logout);

            Session.Abandon();
            Response.Redirect("Index.aspx", false);
        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("LogOut Exception  : " + ex.Message + ex.StackTrace);
            Session.Abandon();
            Response.Redirect("Index.aspx", false);
        }
    }
}