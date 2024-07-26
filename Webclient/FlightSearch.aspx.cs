using Framework.EnterpriseLibrary.Adapters;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;

public partial class FlightSearch : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        System.Web.UI.HtmlControls.HtmlGenericControl sitemap = (System.Web.UI.HtmlControls.HtmlGenericControl)Master.FindControl("sitemap");
        sitemap.Attributes.Add("Style", "display:none");
        Session["CategoryName"] = "flights";
        if (Session["MemberDetails"] != null)
        {
            tabdomesticTab.Visible = true;
        }
        else
        {
            tabdomesticTab.Visible = false;            
        }
    }
}