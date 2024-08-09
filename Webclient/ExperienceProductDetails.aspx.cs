
using BeMyGuest.Entities;
using ABC.Model;
using Framework.EnterpriseLibrary.Adapters;

using Newtonsoft.Json;
using Stripe.Checkout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ExperienceProductDetails : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    [WebMethod]
    public static string GetProductInfo(string uuid)
    {
        string lstrProductInfo = string.Empty;
        ABCModel lobjModel = new ABCModel();
        try
        {
            if (!string.IsNullOrEmpty(uuid))
            {
                ProductInfoRequest productInfoRequest = new ProductInfoRequest();
                productInfoRequest.uuid = uuid;
                ProductInfoResponse productInfoResponse = lobjModel.GetProductInfo(productInfoRequest);
                if (productInfoResponse != null && productInfoResponse.success == 1)
                {
                    HttpContext.Current.Session["ProductInfo"] = productInfoResponse;
                    lstrProductInfo = JsonConvert.SerializeObject(productInfoResponse);
                }
                else
                {
                    lstrProductInfo = "ErrorPage.aspx";
                }
            }

        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("ExperienceProductDetails GetProductInfo Ex - " + Environment.NewLine + ex.Message + Environment.NewLine + ex.InnerException +
                    Environment.NewLine + ex.StackTrace + Environment.NewLine + "DateTime - " + DateTime.Now);
        }
        return lstrProductInfo;
    }

    [WebMethod]
    public static string GetProductTypesPriceByDate(string date, string uuid)
    {
        string lstrProductInfo = string.Empty;
        ABCModel lobjModel = new ABCModel();
        try
        {
            if (date != null && !string.IsNullOrEmpty(uuid))
            {
                ProductInfoResponse productInfoResponse = new ProductInfoResponse();
                if (HttpContext.Current.Session["ProductInfo"] != null)
                {
                    productInfoResponse = HttpContext.Current.Session["ProductInfo"] as ProductInfoResponse;
                }
                else
                {
                    ProductInfoRequest productInfoRequest = new ProductInfoRequest();
                    productInfoRequest.uuid = uuid;
                    productInfoResponse = lobjModel.GetProductInfo(productInfoRequest);
                }
                if (productInfoResponse != null)
                {
                    foreach (var productTypeItem in productInfoResponse.producttypedetails.item_uuid)
                    {
                        ProductTypesPriceByDateRequest productTypesPriceByDateRequest = new ProductTypesPriceByDateRequest();
                        productTypesPriceByDateRequest.uuid = productTypeItem.uuid;
                        DateTime enteredDate = DateTime.ParseExact(date, "dd-MM-yyyy", null);
                        productTypesPriceByDateRequest.date = enteredDate.ToString("yyyy-MM-dd");
                        ProductTypesPriceByDateResponse productTypesPriceByDateResponse = new ProductTypesPriceByDateResponse();
                        productTypesPriceByDateResponse = lobjModel.GetProductTypesPriceByDate(productTypesPriceByDateRequest);
                        if (productTypesPriceByDateResponse != null && productTypesPriceByDateResponse.success == 1 && productTypesPriceByDateResponse.data != null)
                        {
                            productTypeItem.typePriceByDate = productTypesPriceByDateResponse.data;
                        }
                    }
                    HttpContext.Current.Session["ProductInfo"] = productInfoResponse;
                    lstrProductInfo = JsonConvert.SerializeObject(productInfoResponse);
                }
                else
                {
                    lstrProductInfo = "ErrorPage.aspx";
                }
            }

        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("ExperienceProductDetails GetProductTypesPriceByDate Ex - " + Environment.NewLine + ex.Message + Environment.NewLine + ex.InnerException +
                    Environment.NewLine + ex.StackTrace + Environment.NewLine + "DateTime - " + DateTime.Now);
        }
        return lstrProductInfo;
    }
}