using Giift.ShopGateway.Client.Entities;
using ABC.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Framework.EnterpriseLibrary.Adapters;
using System.Globalization;
using GiiftShopGateway.Model;
using Core.Platform.MemberActivity.Entities;
using Core.Platform.MemberActivity.Constants;
using CB.IBE.Platform.Car.Entities;
using System.Web.Services.Description;

public partial class ShopList : Page
{
    static int PageSize = 15;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            ABCModel lobjModel = new ABCModel();
            ShopModel model = new ShopModel();
            List<Category> categories = model.SearchCategories();
            StringBuilder lstrHtmlContent = new StringBuilder();
            string categoryId = string.Empty;
            if (categories != null && categories.Count > 0)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["CategoryId"]))
                {
                    categoryId = Request.QueryString["CategoryId"].ToString();
                    Session["BredcrumCategoryId"] = categoryId;
                    Session["CategoryId"] = categoryId;
                }
                else if (Session["BredcrumCategoryId"] != null)
                {
                    categoryId = Session["BredcrumCategoryId"].ToString();
                    Session["BredcrumCategoryId"] = categoryId;
                    Session["CategoryId"] = categoryId;
                }
                string producttype = string.Empty;
                if (!string.IsNullOrEmpty(Request.QueryString["ProductType"]))
                {
                    producttype = Request.QueryString["ProductType"].ToString();
                }
                if (producttype.ToLower() == "physical")
                {
                    List<Category> mainmenus = categories.FindAll(x => x.Id == categoryId);
                    foreach (var mainMenu in mainmenus)
                    {
                        lstrHtmlContent.Append("<ul class=\"breadcrumb px-0 py-3\"><li class=\"mr-3\"><a href=\"\\\"><img src=\"images/icons/arrows/arrow-left.svg\" /></a></li><li class=\"breadcrumb-item\"><a href=\"Index.aspx\">Home</a></li>");
                        lstrHtmlContent.Append("<li class=\"breadcrumb-item active\"><a href =\"Shop.aspx?CategoryId=" + categoryId + "&ProductType=Physical" + "\">" + mainMenu.Name + "</a></li> </ul>");
                    }
                    lobjModel.LogActivity(string.Format("Visit Shoplist.aspx page; CategoryId-:{0}; CategoryName-:{1}; ProductType-:Physical", categoryId, mainmenus[0].Name), ActivityType.PageLoad);
                }
                else
                {
                    List<Category> mainmenus = categories.FindAll(x => x.Id == categoryId);
                    lstrHtmlContent.Append("<ul class=\"breadcrumb px-0 py-3\"><li class=\"mr-3\"><a href=\"\\\"><img src=\"images/icons/arrows/arrow-left.svg\" /></a></li><li class=\"breadcrumb-item\"><a  href=\"Index.aspx\">Home</a></li>");
                    lstrHtmlContent.Append("<li class=\"breadcrumb-item active\"><a href =\"ShopList.aspx?CategoryId=" + categoryId + "&ProductType=Digital" + "\"> " + mainmenus[0].Name + " </a></li> </ul>");
                    Session["CategoryName"] = Convert.ToString(string.Concat(mainmenus[0].Name.Where(c => !char.IsWhiteSpace(c))).Replace("-", "")).ToLower();
                    lobjModel.LogActivity(string.Format("Visit Shoplist.aspx page; CategoryId-:{0}; CategoryName-:{1}; ProductType-:digital", categoryId, (mainmenus.Count > 0 ? mainmenus[0].Name : string.Empty)), ActivityType.PageLoad);
                }
            }
            divBreadbrums.InnerHtml = lstrHtmlContent.ToString();
        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("ShopList.aspx Page_Load Exception: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
        }
    }

    [WebMethod]
    public static bool SearchProducts(string CategoryId, string ProductName, string Sort, string Terms, int PageNo)
    {
        try
        {
            string catagory = string.Empty;
            string sortString = string.Empty;
            LoggingAdapter.WriteLog("SearchProducts Parameter-:CategoryId-:" + CategoryId + " | ProductName-:" + ProductName + " | Sort-:" + Sort + " | Terms-:" + Terms + " | PageNo-:" + PageNo);
            if (Sort.ToLower().Equals("price_inr-ascending"))
            {
                Sort = "price_" + ConfigurationManager.AppSettings["ShopFilterCurrency"] + "-ascending";
            }
            else if (Sort.ToLower().Equals("price_inr-descending"))
            {
                Sort = "price_" + ConfigurationManager.AppSettings["ShopFilterCurrency"] + "-descending";
            }
            if (!string.IsNullOrEmpty(CategoryId))
            {
                catagory = HttpUtility.UrlDecode(CategoryId);
            }
            else if (HttpContext.Current.Session["BredcrumCategoryId"] != null)
            {
                catagory = HttpContext.Current.Session["BredcrumCategoryId"].ToString();
                CategoryId = catagory;
            }
            string productName = string.Empty;
            if (!string.IsNullOrEmpty(ProductName))
            {
                productName = HttpUtility.UrlDecode(ProductName);
            }
            string sort = string.Empty;
            if (!string.IsNullOrEmpty(Sort))
            {
                sort = HttpUtility.UrlDecode(Sort);
            }
            List<string> terms = new List<string>();
            if (!string.IsNullOrEmpty(Terms))
            {
                terms = HttpUtility.UrlDecode(Terms).Split(';').ToList();
            }
            ShopModel model = new ShopModel();
            string categoryName = string.Empty;
            List<Category> categories = model.SearchCategories();
            ABCModel lobjmodel = new ABCModel();
            try
            {
                if (!string.IsNullOrEmpty(CategoryId))
                {
                   
                    List<Category> mainmenus = categories.FindAll(x => x.Id == CategoryId);
                    categoryName = mainmenus[0].Name;
                    lobjmodel.LogActivity(string.Format(ActivityConstants.SearchProducts, CategoryId, mainmenus[0].Name, productName, sort, Terms, PageNo), ActivityType.SearchProduct);
                }
                else
                {
                    lobjmodel.LogActivity(string.Format(ActivityConstants.SearchProducts, CategoryId, string.Empty, productName, sort, Terms, PageNo), ActivityType.SearchProduct);
                }
            }
            catch (Exception)
            { }
            ProductSearchCriteria criteria = new ProductSearchCriteria();
            if (!string.IsNullOrEmpty(categoryName))
            {
                if (categoryName.ToLower() == "gift cards")
                {
                    criteria = model.BuildProductSearchCriteria(catagory, null, ItemResponseGroup.ItemLarge, 0, 0, productName, sort, (PageNo - 1) * PageSize, PageSize, terms);
                }
                else
                {
                    criteria = model.BuildProductSearchCriteria(catagory, null, ItemResponseGroup.ItemWithPrices, 0, 0, productName, sort, (PageNo - 1) * PageSize, PageSize, terms);
                }
            }
            ProductSearchResult result = model.SearchProducts(criteria);
            lobjmodel.LogActivity(string.Format(ActivityConstants.SearchProducts, CategoryId, productName, sort, Terms, PageNo), ActivityType.SearchProduct);
            List<Product> orderedproducts = null;
            if (Sort.ToLower().Equals("price_" + ConfigurationManager.AppSettings["ShopFilterCurrency"] + "-ascending"))
            {
                var values = from p in result.Products
                             orderby p.Price.SalePrice.Amount
                             select p;
                orderedproducts = new List<Product>(values);
                var orderedproductsnew = new List<Product>(values);
                result.Products = orderedproducts;
            }
            else if (Sort.ToLower().Equals("price_" + ConfigurationManager.AppSettings["ShopFilterCurrency"] + "-descending"))
            {
                var values = from p in result.Products
                             orderby p.Price.SalePrice.Amount descending
                             select p;
                orderedproducts = new List<Product>(values);
                var orderedproductsnew = new List<Product>(values);
                result.Products = orderedproducts;
            }
            if (result != null)
            {
                HttpContext.Current.Session["ProductList"] = result;
                return true;
            }
            else
            {
                HttpContext.Current.Session["ProductList"] = null;
                LoggingAdapter.WriteLog("SearchProducts Response Null");
                return false;
            }
        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("ShopList.aspx SearchProducts Ex-" + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
            return false;
        }
    }
    [WebMethod]
    public static string GetProductList(int PageNo)
    {
        string html = string.Empty;
        try
        {
            ProductSearchResult result = HttpContext.Current.Session["ProductList"] as ProductSearchResult;
            if (result != null)
            {
                StringBuilder sb = new StringBuilder();
                string Template = "<div class=\"col-sm-6 col-md-4 mb-3\">"
                    + "<a href = \"{3}\" class=\"d-block shadow-sm\"><div class=\"bg-white border h-100 d-flex flex-column\">"
                    + "<div class=\"img-container\">"
                    + "<img src = \"{0}\">"
                    + "</div>"
                    + "<div class=\"d-flex flex-wrap bg-white p-2\">"
                    + "<p class='h6 heading-bold text-truncate mb-1 w-100'>{1}</p>"
                    + "{4}"
                    + "<p class=\"h8 heading-regular w-100 mt-auto\">{5}</p>"
                    + "<p class=\"h7 heading-semibold w-100 pt-2\">{2}</p>"
                    + "<p class=\"h7 heading-regular w-100 pt-1 mt-auto\"></p>"
                    + "</div>"
                    + "<div class=\"cart-button mt-0 px-2 d-flex justify-content-between align-items-center\">"
                    + "</div>"
                    + "</div></a>"
                    + "</div>";
                sb.Append("<div class='row'>");
                foreach (var product in result.Products)
                {
                    string starratings = string.Empty;
                    if (product.Ratings.Equals(1))
                    {
                        starratings = "<img src='Images/blackstar.png'/>";
                    }
                    else if (product.Ratings > 1 && product.Ratings < 2)
                    {
                        starratings = "<img src='Images/blackstar.png'/><img src='Images/blackhalfstar.png'/>";
                    }
                    else if (product.Ratings.Equals(2))
                    {
                        starratings = "<img src='Images/blackstar.png'/><img src='Images/blackstar.png'/>";
                    }
                    else if (product.Ratings > 2 && product.Ratings < 3)
                    {
                        starratings = "<img src='Images/blackstar.png'/><img src='Images/blackstar.png'/><img src='Images/blackhalfstar.png'/>";
                    }
                    else if (product.Ratings.Equals(3))
                    {
                        starratings = "<img src='Images/blackstar.png'/><img src='Images/blackstar.png'/><img src='Images/blackstar.png'/>";
                    }
                    else if (product.Ratings > 3 && product.Ratings < 4)
                    {
                        starratings = "<img src='Images/blackstar.png'/><img src='Images/blackstar.png'/><img src='Images/blackstar.png'/><img src='Images/blackhalfstar.png'/>";
                    }
                    else if (product.Ratings.Equals(4))
                    {
                        starratings = "<img src='Images/blackstar.png'/><img src='Images/blackstar.png'/><img src='Images/blackstar.png'/><img src='Images/blackstar.png'/>";
                    }
                    else if (product.Ratings > 4 && product.Ratings < 5)
                    {
                        starratings = "<img src='Images/blackstar.png'/><img src='Images/blackstar.png'/><img src='Images/blackstar.png'/><img src='Images/blackstar.png'/><img src='Images/blackhalfstar.png'/>";
                    }
                    else if (product.Ratings.Equals(5))
                    {
                        starratings = "<img src='Images/blackstar.png'/><img src='Images/blackstar.png'/><img src='Images/blackstar.png'/><img src='Images/blackstar.png'/><img src='Images/blackstar.png'/>";
                    }
                    string pstrProductType = "physical";
                    string countryName = string.Empty;
                    if (!string.IsNullOrEmpty(product.ProductType) && product.ProductType.Equals("Digital"))
                    {
                        pstrProductType = "digital";
                        if (product.Properties.Count > 0)
                        {
                            if (product.Properties.ToList().Find(lobj => lobj.Name.Equals("Type")).Value == "GiftCard")
                            {
                                countryName = product.Properties.ToList().Find(lobj => lobj.Name.Equals("Country")).Value;
                            }
                        }
                    }
                    ABCModel lobjModel = new ABCModel();
                    sb.Append(string.Format(Template, product.PrimaryImage.Url, product.Name, lobjModel.FormatPoints(Math.Ceiling(product.Price.SalePriceWithTax.Amount), "NPoints"), "ProductDetails.aspx?ProductId=" + product.Id + "&ProductType=" + pstrProductType, string.Format("<div class='starRat'>{0}</div>", starratings), countryName));
                }
                sb.Append("</div>");
                if (result.totalCount > PageSize)
                {
                    StringBuilder sbPage = new StringBuilder();
                    sbPage.Append("<div class='d-flex dvPagination scroll-hoz'> <nav><ul class='pagination justify-content-center'>");
                    if (PageNo == 1)
                    {
                        sbPage.Append("<li class='page-item disabled'> <span class='page-link'>Previous</span> </li>");
                    }
                    else
                    {
                        sbPage.Append(string.Format("<li class='page-item'> <span class='page-link' onclick='BindProducts({0});return false;'>Previous</span> </li>", (PageNo - 1)));
                    }
                    int totalPages = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(result.totalCount) / PageSize));
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
                            sbPage.Append(string.Format("<li class='page-item'> <span class='page-link' onclick='BindProducts({0});return false;'> {0} </span> </li>", i));
                        }
                    }
                    if (PageNo < totalPages)
                    {
                        sbPage.Append(string.Format("<li class='page-item'> <span class='page-link' onclick='BindProducts({0});return false;'>Next</span> </li> </ul> </nav></div>", (PageNo + 1)));
                    }
                    else
                    {
                        sbPage.Append("<li class='page-item disabled'> <span class='page-link'>Next</span> </li> </ul> </nav>");
                    }
                    sb.Append(sbPage.ToString());
                }
                html = string.Format("{0}||{1}", Convert.ToString(sb), result.totalCount);
            }
        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("ShopList.aspx GetProductList Ex-" + ex.Message + ex.InnerException + ex.StackTrace);
        }
        return html;
    }
    [WebMethod]
    public static string Filter()
    {
        string html = string.Empty;
        try
        {
            ProductSearchResult result = HttpContext.Current.Session["ProductList"] as ProductSearchResult;
            if (result != null)
            {
                StringBuilder filterHtml = new StringBuilder();
                filterHtml.Append("");
                int i = 0;
                foreach (var filter in result.Aggregations)
                {
                    if (filter.Items.Count > 0)
                    {
                        if (i == 0)
                        {
                            filterHtml.Append(string.Format("<div id='{0}' class=\"card my-3\"><div class=\"card-header p-0\"><h2 class=\"mb-0\"><button class=\"btn btn-block text-left\" type=\"button\" data-toggle=\"collapse\" data-target=\"#collapse1\"><span class=\"h6 heading-semibold text-colour7 mb-2 text-capitalize\">{0}</span><span class=\"arrow-icon\"> <i class=\"fa fa-caret-up-\"></i> </span> </button></h2></div>", filter.Label));
                            filterHtml.Append("<div id=\"collapse1-\" class=\"collapse-\" data-parent=\"#filter-accordion\"><div class=\"card-body scroll-ver px-3 pt-1 pb-2\">");
                        }
                        else
                        {
                            filterHtml.Append(string.Format("<div id='{0}' class=\"card my-3\"><div class=\"card-header p-0\"><h2 class=\"mb-0\"><button class=\"btn btn-block text-left collapsed\" type=\"button\" data-toggle=\"collapse\" data-target=\"#collapse2\" ><span class=\"h6 heading-semibold text-colour7 mb-2 text-capitalize\">{0}</span><span class=\"arrow-icon\"> <i class=\"fa fa-caret-up-\"></i> </span> </button></h2></div>", filter.Label));
                            filterHtml.Append("<div id=\"collapse2-\" class=\"collapse-\" data-parent=\"#filter-accordion\"><div class=\"card-body scroll-ver px-3 pt-1 pb-2\">");
                        }
                        i++;
                        foreach (var item in filter.Items)
                        {
                            string lstrChecked = string.Empty;
                            if (item.IsApplied)
                            {
                                lstrChecked = "checked";
                            }
                            TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
                            filterHtml.Append("<div class=\"dvLabel d-flex justify-content-between\">"
                                + "<label class=\"checkbox-container d-flex\">"
                                + "<span class=\"d-inline-block\">"
                                + "<input class=\"form-check-input\" type=\"checkbox\" value=\"" + item.Value + "\" onchange='FilterProducts();return false;' id=\"" + item.Value + "\" " + lstrChecked + ">"
                                + "<span class=\"checkmark\"></span>"
                                + "</span>"
                                + "<span class=\"d-inline-block ml-2\" for=\"flexCheckChecked\">" + textInfo.ToTitleCase(item.Label) + "</span>"
                                + "</label>"
                                + "<span class=\"d-inline-block ml-2\">" + item.Count + "</span>"
                                + "</div>");
                        }
                        filterHtml.Append("</div></div></div></div><div class=\"dvBorderBottom\">\r\n<div class=\"col-12\">\r\n<div class=\"border-bottom my-3\"></div>\r\n</div>\r\n</div>");
                    }
                }
                html = filterHtml.ToString();
            }
        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("ShopList.aspx Filter Exception: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
        }
        return html;
    }
    [WebMethod]
    public static List<string> GetAutocompleteProductName(string prefixText, string productType, string terms)
    {       
        try
        {
            List<string> lstTerms = new List<string>();
            List<string> lobjListOfProductName = new List<string>();
            string productName = string.Empty;
            if (!string.IsNullOrEmpty(prefixText))
            {
                productName = HttpUtility.UrlDecode(prefixText);
            }
            if (!string.IsNullOrEmpty(terms))
            {
                lstTerms = HttpUtility.UrlDecode(terms).Split(';').ToList();
            }
            ShopModel model = new ShopModel();
            string categoryId = Convert.ToString(HttpContext.Current.Session["CategoryId"]);
            ProductSearchCriteria criteria = model.BuildProductSearchCriteria(categoryId, null, ItemResponseGroup.ItemInfo, 0, 0, productName, string.Empty, 0, 5, lstTerms);
            var result = model.SearchProducts(criteria);
            if (result != null)
            {
                foreach (var product in result.Products.Distinct())
                {
                    if (productType.ToLower() == "physical")
                    {
                        if (!lobjListOfProductName.Contains(product.Name))
                        {
                            lobjListOfProductName.Add(product.Name);
                        }

                    }
                    else
                    {
                        if (product.ProductType.ToLower() == productType.ToLower() && !lobjListOfProductName.Contains(product.Name))
                        {
                            lobjListOfProductName.Add(product.Name);
                        }
                    }

                }
            }
            else
            {
                lobjListOfProductName.Add("No product found.");
            }
            return lobjListOfProductName;
        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("GetAutocompleteProductName Ex : " + ex.Message + Environment.NewLine + "Stack Trace-" + ex.StackTrace);
            return null;
        }
    }
}