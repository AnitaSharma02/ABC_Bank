using ABC.Model;
using Core.Platform.Member.Entites;
using Framework.EnterpriseLibrary.Adapters;
using Giift.ShopGateway.Client.Entities;
using GiiftShopGateway.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.Services;
public partial class OrderHistory : Page
{
    private static readonly int PageSize = 10;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                if (HttpContext.Current.Session["MemberDetails"] == null)
                {
                    string CallbackUrl = HttpUtility.UrlEncode(Encrypt("OrderHistory.aspx"));
                    Response.Redirect("Index.aspx?CallbackUrl=" + CallbackUrl, false);
                }
            }
        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("OrderHistory.aspx Page_Load Exception: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
        }
    }
    [WebMethod]
    public static string BindCustomerOrders(int skip)
    {
        string list = string.Empty;
        try
        {
            string pstrMemberId = "";
            int TotalPagecount = 0;
            ShopModel lobjModel = new ShopModel();
            MemberDetails lobjMemberDetails = new MemberDetails();
            ABCModel lobjBNIModel = new ABCModel();
            List<string> lobjstringList = new List<string>();
            lobjMemberDetails = HttpContext.Current.Session["MemberDetails"] as MemberDetails;
            pstrMemberId = lobjMemberDetails.MemberRelationsList.Find(lobj => lobj.RelationType.Equals(RelationType.LBMS)).RelationReference;
            if (skip <= 0)
            {
                skip = 0;
            }
            else
            {
                skip--;
            }
            CustomerOrderSearchResult lobjListOfCustomerOrder = new CustomerOrderSearchResult();
            lobjListOfCustomerOrder = lobjModel.GetCustomerOrders((skip) * PageSize, 10, pstrMemberId);
            if (lobjListOfCustomerOrder != null)
            {
                TotalPagecount = Convert.ToInt32(Math.Ceiling((double)lobjListOfCustomerOrder.TotalCount / (double)10));
                list = PrintCustomerOrder(lobjListOfCustomerOrder, skip * PageSize, 10, skip + 1);
            }
            else
            {
                list = PrintCustomerOrder(lobjListOfCustomerOrder, skip * PageSize, 10, skip + 1);
            }
        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("OrderHistory.aspx BindCustomerOrders Exception: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
        }
        return list;
    }

    public static string PrintCustomerOrder(CustomerOrderSearchResult lobjOrders, int Skip, int PageSize, int PageNo)
    {
        string lstrHtmlContent = string.Empty;
        string lstrPaginationHtml = string.Empty;
        try
        {
            if (lobjOrders != null)
            {
                if (lobjOrders.CustomerOrders != null && lobjOrders.CustomerOrders.Count > 0)
                {
                    ABCModel lobjModel = new ABCModel();
                    int rownum = (Skip / PageSize) * PageSize;
                    for (int i = 0; i < lobjOrders.CustomerOrders.Count; i++)
                    {
                        lstrHtmlContent += "<div class=\"row mb-1\"><div class=\"col-12\"><div class=\"bg-colour6 p-3\">"
                                + "<div class=\"row align-items-lg-center justify-content-between\">"
                                + "<div class=\"col-12 col-sm-6 col-lg-2 mb-1\"><p><span class=\"h7 d-block heading-bold text-colour7\">Order No.</span> <span class=\"h6 d-block\">" + lobjOrders.CustomerOrders[i].Number + "</span></p></div>"
                                + "<div class=\"col-12 col-sm-6 col-lg-3 mb-1\"><p><span class=\"h7 d-block heading-bold text-colour7\">Order Date</span><span class=\"h6 d-block\">" + DateTime.Parse(lobjOrders.CustomerOrders[i].CreatedDate.ToString()).ToLocalTime() + "</span></p></div>"
                                + "<div class=\"col-12 col-lg-2 col-sm-6 mb-1\"><p class=\"\"><span class=\"h7 d-block heading-bold text-colour7\">Points</span><span class=\"h6 d-block\">" + lobjModel.FormatPoints(Math.Ceiling(lobjOrders.CustomerOrders[i].Price.Total.Amount), "Points") + "</span></p></div>"
                                + "<div class=\"col-6 col-lg-1 col-sm-3 mb-1\"><p><span class=\"h7 d-block heading-bold text-colour7 pr-2\">Qty:</span><span class=\"h6 d-block\">" + lobjOrders.CustomerOrders[i].Items.Count + "</span></p></div>"
                                + "<div class=\"col-6 col-lg-2 col-sm-3 mb-1 text-right\"><p class=\"h6 heading-semibold text-success\">" + lobjOrders.CustomerOrders[i].Status + "</p></div>"
                                + "<div class=\"col-12 col-lg-2 text-lg-right mt-2 mt-lg-0\"><a href = \"OrderDetails.aspx?OrderNumber=" + lobjOrders.CustomerOrders[i].Number + "\" class=\"btn btn-one w-100\">View Details</a></div>"
                                + "</div></div>"
                                + "</div></div>";
                    }

                    StringBuilder sbPage = new StringBuilder();
                    if (lobjOrders.TotalCount > PageSize)
                    {
                        sbPage.Append("<div class='row mx-0 dvPagination scroll-hoz mt-3 justify-content-sm-center'> <nav><ul class='pagination justify-content-center'>");

                        if (PageNo == 1)
                        {
                            sbPage.Append("<li class='page-item disabled'> <span class='page-link'>Previous</span> </li>");
                        }
                        else
                        {
                            sbPage.Append(string.Format("<li class='page-item'> <span class='page-link' onclick='BindCustomerOrders({0});return false;'>Previous</span> </li>", (PageNo - 1)));
                        }
                        int totalPages = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(lobjOrders.TotalCount) / PageSize));
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
                                sbPage.Append(string.Format("<li class='page-item'> <span class='page-link' onclick='BindCustomerOrders({0});return false;'> {0} </span> </li>", i));
                            }
                        }
                        if (PageNo < totalPages)
                        {
                            sbPage.Append(string.Format("<li class='page-item'> <span class='page-link' onclick='BindCustomerOrders({0});return false;'>Next</span> </li> </ul> </nav></div>", (PageNo + 1)));
                        }
                        else
                        {
                            sbPage.Append("<li class='page-item disabled'> <span class='page-link'>Next</span> </li> </ul> </nav>");
                        }
                        sbPage.ToString();
                    }

                    lstrPaginationHtml = sbPage.ToString();
                }
                else
                {
                    lstrHtmlContent += "<span data-i18n='managebooking-norecords-label' >No Records Found.</span>";
                }
            }
            else
            {

                lstrHtmlContent += "<span data-i18n='managebooking-norecords-label'>No Records Found.</span>";
            }
        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("OrderHistory.aspx PrintCustomerOrder Exception: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
        }
        return lstrHtmlContent += lstrPaginationHtml;
    }
    private static string Encrypt(string clearText)
    {
        try
        {
            string EncryptionKey = "MAKV2SPNIC99212";
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
        }
        catch (Exception ex) { LoggingAdapter.WriteLog("OrderHistory.aspx Encrypt Exception: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace); }
        return clearText;
    }
}