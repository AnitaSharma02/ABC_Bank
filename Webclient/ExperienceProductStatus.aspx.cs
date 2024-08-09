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
                        divMessage.InnerHtml += "<div class=\"border dvCongrat bg-white p-5 text-center\">"
                            + "<p class=\"h5 heading-bold\">Congratulations!</p><p class=\"heading-light pt-2\">Your Order is placed successfully, an email confirmation will be sent on your registered email id.</p></div>";
                    }
                    else
                    {
                        divMessage.InnerHtml = "<div class='border dvCongrat bg-white p-5 text-center'>We could not process your request.<p class=\"heading-light pt-2\">Please<a href=\"Index.aspx\" target=\"_self\"> click here</a> and try again.<p></div>";
                    }
                }
                else
                {
                    divMessage.InnerHtml = "<div class='border dvCongrat bg-white p-5 text-center'>We could not process your request.<p class=\"heading-light pt-2\">Please<a href=\"Index.aspx\" target=\"_self\"> click here</a> and try again.</p></div>";
                }
            }
        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("ExperienceProductStatus.aspx Pageload Exception: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
        }
    }
}