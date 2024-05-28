using Core.Platform.Helper.ProgramName;
using Core.Platform.Member.Entites;
using Core.Platform.MemberActivity.Constants;
using Core.Platform.MemberActivity.Entities;
using Core.Platform.ProgramMaster.Entities;
using Framework.EnterpriseLibrary.Adapters;
using Holibob.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class InsuranceProductStatus : System.Web.UI.Page
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
                        divMessage.InnerHtml += "<h2 class='h2 heading-semibold text-center text-colour1 mb-3'>Congratulations!</h2>"
                            + "<h2 class='h6 heading-regular text-center mb-3'>Your Order is placed successfully, an email confirmation will be sent on your registered email id.</h2>";
                        //SendInsuranceEmail();
                    }
                    else
                    {
                        divMessage.InnerHtml = "<h2 class='h6 heading-regular text-center mb-3'>Could not process your request.</h2><h2 class='h6 heading-regular text-center'>Your order didn't go through, please <a href=\"Index.aspx\" class=\"text-underline\" target=\"_self\">try again</a> after some time.<h2>";
                    }
                }
                else
                {
                    divMessage.InnerHtml = "<h2 class='h6 heading-regular text-center mb-3'>Could not process your request.</h2><h2 class='h6 heading-regular text-center'>Your order didn't go through, please <a href=\"Index.aspx\" class=\"text-underline\" target=\"_self\">try again</a> after some time.</h2>";
                }
            }
        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("InsuranceProductStatus.aspx Pageload Exception: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
        }
    }
}