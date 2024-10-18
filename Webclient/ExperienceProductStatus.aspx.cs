using Framework.EnterpriseLibrary.Adapters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ExperienceProductStatus : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                divMessage.InnerHtml = "";
                if (Request.QueryString["Success"] != null)
                {
                    bool lblnStatus = Convert.ToBoolean(Request.QueryString["Success"]);
                    if (lblnStatus)
                    {
                        divMessage.InnerHtml += "<div class=\"border bg-colour2 p-5 text-center\">"
                            + "<p class=\"\">Congratulations!</p>" +
                            "<p>Your Order is placed successfully, an email confirmation will be sent on your registered email id.</p>" +
                            "</div>";
                    }
                    else
                    {
                        divMessage.InnerHtml = "<div class='border bg-colour2 p-5 text-center'>" +
                            "<p>We could not process your request.</p>" +
                            "<p>Please <a href=\"Index.aspx\" target=\"_self\" class='link1'> click here</a> and try again.</p>" +
                            "</div>";
                    }
                }
                else
                {
                    divMessage.InnerHtml = "<div class='border bg-colour2 p-5 text-center'>" +
                        "<p>We could not process your request.</p>" +
                        "<p>Please </span><a href=\"Index.aspx\" target=\"_self\" class=\"link1\"> click here</a> <span> and try again.</p>" +
                        "</div>";
                }
            }
        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("ExperienceProductStatus.aspx Pageload Exception: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
        }
    }
}