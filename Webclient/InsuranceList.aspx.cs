using Core.Platform.MemberActivity.Constants;
using Core.Platform.MemberActivity.Entities;
using Framework.EnterpriseLibrary.Adapters;
using Giift.ShopGateway.Client.Entities;
using GiiftShopGateway.Model;
using System;
using System.Configuration;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Web.Services;
using System.Web.UI;
using KhaltiInsurance.Entities;
using ABC.Model;
using System.Web;

public partial class InsuranceList :Page
{
    static int PageSize = 10;   
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            ABCModel lobjModel = new ABCModel();
            StringBuilder lstrHtmlContent = new StringBuilder();      
            lobjModel.LogActivity(string.Format("Visit InsuranceList.aspx page"), ActivityType.PageLoad);
            Session["PageName"] = Title;
        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("InsuranceList.aspx Page_Load Exception: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
        }
    }
    [WebMethod]
    public static string GetProductList(int PageNo)
    {
        string html = string.Empty;
        try
        {
            string UserName = string.Empty;
            string PageName = string.Empty;
            UserName = Convert.ToString(ConfigurationManager.AppSettings["KhaltiInsuranceUserName"]);
            PageName = "Insurance List";
            ABCModel lobjmodel = new ABCModel();
            InsuranceServiceProvidersResponse result = lobjmodel.SearchInsuranceProducts(UserName, PageName);
            if (result != null && result.results != null)
            {
                StringBuilder sb = new StringBuilder();
                string Template = "<div class=\"col-sm-6 col-md-4 px-2 px-sm-3 mb-3\">"
                    + "<a href = \"{1}\"><div class=\"bg-white border h-100 d-flex flex-column\">"
                    + "<div class=\"img-container\">"
                    + "<img src = \"{2}\">"
                    + "</div>"
                    + "<div class='bg-white d-flex flex-column p-2'>"
                    + "<div class=\"d-flex justify-content-between\">"
                    + "<p class='h6 heading-semibold text-colour7 text-truncate'>{0}</p>"
                    + "</div>"
                    + "</div>"
                    + "<div class=\"cart-button mt-0 px-2 d-flex justify-content-between align-items-center\">"
                    + "</div>"
                    + "</div></a>"
                    + "</div>";
                sb.Append("<div class='row align-items-center'>");
                foreach (var product in result.results.Skip((PageNo - 1) * PageSize).Take(PageSize))
                { 
                    sb.Append(string.Format(Template, product.ServiceName, "InsuranceListDetails.aspx?code=" + product.ServiceCode, product.ImageUrl));
                }
                sb.Append("</div>");
                if (result.results.Count > PageSize)
                {
                    StringBuilder sbPage = new StringBuilder();
                    sbPage.Append("<div class='dvPagination'><nav><ul class='pagination justify-content-center'>");
                    if (PageNo == 1)
                    {
                        sbPage.Append("<li class='page-item disabled'> <span class='page-link'>Previous</span> </li>");
                    }
                    else
                    {
                        sbPage.Append(string.Format("<li class='page-item'> <span class='page-link' onclick='GetProductList({0});return false;'>Previous</span> </li>", (PageNo - 1)));
                    }
                    int totalPages = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(result.results.Count) / PageSize));
                    int PagingSize = 15;
                    int Pager = 1;
                    if (PageNo >= PagingSize)
                    {
                        if (totalPages >= PageNo + PagingSize - 2)
                        {
                            Pager = PageNo - 1;
                        }
                        else
                        {
                            Pager = totalPages - 14;
                        }
                    }
                    int j = 1;
                    for (int i = Pager; i <= totalPages && j <= PagingSize; i++, j++)
                    {
                        if (i == PageNo)
                        {
                            sbPage.Append(string.Format("<li class='page-item active'> <span class='page-link'> {0} <span class='sr-only'>(current)</span> </span></li>", i));
                        }
                        else
                        {
                            sbPage.Append(string.Format("<li class='page-item'> <span class='page-link' onclick='GetProductList({0});return false;'> {0} </span> </li>", i));
                        }
                    }
                    if (PageNo < totalPages)
                    {
                        sbPage.Append(string.Format("<li class='page-item'> <span class='page-link' onclick='GetProductList({0});return false;'>Next</span> </li> </ul> </nav>", (PageNo + 1)));
                    }
                    else
                    {
                        sbPage.Append("<li class='page-item disabled'> <span class='page-link'>Next</span> </li> </ul> </nav></div>");
                    }
                    sb.Append(sbPage.ToString());
                }
                html = string.Format("{0}||{1}", Convert.ToString(sb), result.results.Count);
            }
            else
            {
                html = string.Format("{0}||{1}", "No Insurance List found.", 0);
            }
        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("InsuranceList.aspx GetProductList Ex-" + ex.Message + ex.InnerException + ex.StackTrace);
        }
        return html;
    }
}