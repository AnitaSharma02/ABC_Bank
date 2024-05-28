using System;
using System.Configuration;
using System.Text;
using System.Web.UI;
using Core.Framework.PostHelper;
using Framework.EnterpriseLibrary.Adapters;

public partial class TestExtSSORequest : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            StringBuilder sb = new StringBuilder();
            string lstrExtLoginSSOTestCIF = string.IsNullOrEmpty(Convert.ToString(Request.QueryString["CIF"]))
                ? ConfigurationManager.AppSettings["ExtLoginSSOTestCIF"]
                : Convert.ToString(Request.QueryString["CIF"]);
            MemberLogin lobjMemberLogin = new MemberLogin();
            Token lobjToken = lobjMemberLogin.GenerateToken("['LOGIN','" + lstrExtLoginSSOTestCIF + "']");
            if (!string.IsNullOrEmpty(lobjToken.AccessToken))
            {
                Session["MemberDetails"] = null;
                string lstrRedirectURL = Convert.ToString(ConfigurationManager.AppSettings["ExtLoginSSOTestRedirect"]);
                sb.Append("<html>");
                sb.Append(@"<body onload='document.forms[""form""].submit()'>");
                sb.Append("<form name='form' action='" + lstrRedirectURL + "' method='post'>");
                sb.Append("<input type='hidden' name='Token' value='" + lobjToken.AccessToken + "' />");
                sb.Append("</form>");
                sb.Append("</body>");
                sb.Append("</html>");
            }
            Response.Clear();
            Response.Write(sb.ToString());
        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("TestExtSSORequest Ex: " + ex.Message + Environment.NewLine + "Stack Trace: " + ex.StackTrace);
            updProgress.Visible = false;
        }
    }
}