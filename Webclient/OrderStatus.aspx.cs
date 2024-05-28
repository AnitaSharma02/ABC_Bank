using Framework.EnterpriseLibrary.Adapters;
using System;
using System.Web.UI;

public partial class OrderStatus : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                bool lblnStatus = false;
                if (Request.QueryString["Status"] != null)
                {
                    lblnStatus = bool.Parse((Request.QueryString["Status"]));
                    if (lblnStatus)
                    {
                        divSuccess.Visible = true;
                        divFailed.Visible = false;
                    }
                    else
                    {
                        divSuccess.Visible = false;
                        divFailed.Visible = true;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("OrderStatus.aspx Page_Load Exception: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
        }
    }
}