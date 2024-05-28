using Framework.EnterpriseLibrary.Adapters;
using Giift.ShopGateway.Client.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ABC.Model;
using GiiftShopGateway.Model;

public partial class redeem : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //string redeemoptions = GetRedemptionOptions();
    }
    /*
    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    
    public  string GetRedemptionOptions()
    {
        string strResponse = string.Empty;
        try
        {
            ABCModel model = new ABCModel();
            ShopModel lobjmodel = new ShopModel();
            List<Giift.ShopGateway.Client.Entities.Category> listOfCategories = lobjmodel.SearchCategories();
            if (listOfCategories != null && listOfCategories.Count > 0)
            {
                StringBuilder sb = new StringBuilder();

                List<CatalogProperty> lobjCatelogue = null;
                int count = 0;
                foreach (var category in listOfCategories.FindAll(lobj => lobj.ParentId.IsNullOrEmpty() && lobj.IsActive).OrderBy(o => o.Priorty).ToList())
                {

                    var PageURL = category.Properties.ToList().Find(lobj => lobj.Name.Equals("PageUrl")).Value.Replace("dotaspx", ".aspx");

                    if (category.Name.ToLower().Contains("vouchers"))
                    {

                        voucherurl.HRef = PageURL;

                    }
                    else if (category.Name.ToLower().Contains("shop"))
                    {
                        shopurl.HRef = PageURL;
                    }
                    else if (category.Name.ToLower().Contains("top-up"))
                    {
                        topupurl.HRef = PageURL;
                    }
                    else if (category.Name.ToLower().Contains("flight"))
                    {
                        flighturl.HRef = PageURL;
                    }
                    else if (category.Name.ToLower().Contains("hotel"))
                    {
                        hotelurl.HRef= PageURL;
                    }
                    else if (category.Name.ToLower().Contains("miles exchange")) {
                        milesexchangeurl.HRef = PageURL;
                    }
                    else if (category.Name.ToLower().Contains("offers"))
                    {
                        offersurl.HRef = PageURL;
                    }

                }
                strResponse = sb.ToString();
            }
        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("GetRedemptionOptions Exception: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
        }

        return strResponse;
    }
    */
}
