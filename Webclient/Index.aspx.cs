using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ABC.Model;
using Giift.ShopGateway.Client.Entities;
using System.Text;
using Framework.EnterpriseLibrary.Adapters;
using System.Web.Script.Services;
using System.Web.Services;
using GiiftShopGateway.Model;
using Newtonsoft.Json;
using CB.IBE.Platform.AirClientModel;
using Core.Platform.Member.Entites;
using Core.Platform.ProgramMaster.Entities;

public partial class Index : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            Session["CategoryName"] = "home";
            System.Web.UI.HtmlControls.HtmlGenericControl sitemap = (System.Web.UI.HtmlControls.HtmlGenericControl)Master.FindControl("sitemap");
            sitemap.Attributes.Add("Style", "display:none");
            Session["BredcrumCategoryId"] = null;
        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("Index.aspx Page_Load Exception: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
        }
    }
    [ScriptMethod()]
    [WebMethod]
    public static string GetHomeBanner(string path)
    {
        string strResponse = string.Empty;
        try
        {
            if (string.IsNullOrEmpty(Convert.ToString(HttpContext.Current.Application[path])))
            {
                ABCModel model = new ABCModel();
                ShopModel lobjmodel = new ShopModel();
                StoreAssets storeAssets = lobjmodel.GetStoreAssets(path);
                if (storeAssets != null)
                {
                    StringBuilder sb = new StringBuilder();
                    int count = 0;
                    sb.Append("<div class=\"swiper-wrapper\">");
                    foreach (var asset in storeAssets.AssetDetails)
                    {
                        if (count == 0)
                        {
                            sb.Append("<div class=\"swiper-slide active\">");
                            sb.Append("<img src=\"" + asset.Url + "\" alt=\"\" width=\"100%\" class=\"w-100\"/>");
                            sb.Append("</div>");
                        }
                        else
                        {
                            sb.Append("<div class=\"swiper-slide\">");
                            sb.Append("<img src=\"" + asset.Url + "\" alt=\"\" width=\"100%\" class=\"w-100\"/>");
                            sb.Append("</div>");
                        }
                        count += 1;

                    }
                    sb.Append("</div>");
                    sb.Append("<div class=\"swiper-pagination container-xl p-0 text-lg-right\"></div>");
                    sb.Append("<div class=\"swiper-button-prev\">");
                    sb.Append("<img src=\"images/icons/arrows/left-yellow-arrow-2.svg\" alt=\"\" />");
                    sb.Append("</div>");
                    sb.Append("<div class=\"swiper-button-next\">");
                    sb.Append("<img src=\"images/icons/arrows/right-yellow-arrow-2.svg\" alt=\"\" />");
                    sb.Append("</div>");
                    sb.Append("");
                    strResponse = sb.ToString();
                    HttpContext.Current.Application[path] = strResponse;

                }
            }
            else
            {
                strResponse = Convert.ToString(HttpContext.Current.Application[path]);
            }
        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("Index.aspx GetHomeBanner Exception: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
        }
        return strResponse;
    }
    [ScriptMethod()]
    [WebMethod]
    public static string GetRedemptionOptions()
    {
        string strResponse = string.Empty;
        try
        {
            if (string.IsNullOrEmpty(Convert.ToString(HttpContext.Current.Application["HomeRedemptionOptions"])))
            {
                ABCModel model = new ABCModel();
                ShopModel lobjmodel = new ShopModel();
                List<Category> listOfCategories = lobjmodel.SearchCategories();
                if (listOfCategories != null && listOfCategories.Count > 0)
                {
                    StringBuilder sb = new StringBuilder();
                    //sb.Append("<div class=\"col-6 col-sm-4 col-lg-2 text-center d-flex flex-column align-items-center mb-3\">");
                    foreach (var category in listOfCategories.FindAll(lobj => lobj.ParentId.IsNullOrEmpty() && lobj.IsActive).OrderBy(o => o.Priorty).ToList())
                    {
                        try
                        {
                            var PageURL = category.Properties.ToList().Find(lobj => lobj.Name.Equals("PageUrl")).Value.Replace("dotaspx", ".aspx");
                            sb.Append("<div class=\"col-auto mb-md-3\"><a class=\"text-center d-flex flex-column align-items-center " + category.Name.Replace(" ", "").Replace("-", "").ToLower() + "redemption redemptionoptions\" href=\"" + PageURL + "\"><div class=\"d-flex flex-column align-items-center justify-content-center p-2 p-lg-2 p-xl-4 rounded-circle imageBox\"><img src=\"" + (category.PrimaryImage != null && category.PrimaryImage.Url != null ? category.PrimaryImage.Url : string.Empty) + "\" /></div><p class=\"mt-2\">" + category.Name + "</p></a></div>");
                        }
                        catch (Exception)
                        {
                            LoggingAdapter.WriteLog("category :" + category.Name);
                        }
                    }
                    //sb.Append("</div>");
                    strResponse = sb.ToString();
                    HttpContext.Current.Application["HomeRedemptionOptions"] = strResponse;
                }
            }
            else
            {
                strResponse = Convert.ToString(HttpContext.Current.Application["HomeRedemptionOptions"]);
            }
        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("Index.aspx GetRedemptionOptions Exception: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
        }
        return strResponse;
    }

    //[ScriptMethod()]
    //[WebMethod]
    //public static string GetShopBestDeals()
    //{
    //    string result = string.Empty;
    //    ABCModel lobjModel = new ABCModel();
    //    ShopModel model = new ShopModel();
    //    List<Category> listOfCategories = model.SearchCategories();
    //    try
    //    {
    //        if (string.IsNullOrEmpty(Convert.ToString(HttpContext.Current.Application["ShopBestDeals"])))
    //        {
    //            StringBuilder sb = new StringBuilder();
    //            if (listOfCategories != null && listOfCategories.Count > 0)
    //            {
    //               //listOfCategories = listOfCategories.FindAll(lobj => lobj.ParentId.IsNullOrEmpty() && lobj.IsActive && lobj.Name.ToLower() == "online shop");
    //                listOfCategories = listOfCategories.FindAll(lobj => lobj.ParentId.IsNullOrEmpty() && lobj.IsActive && lobj.Name.ToLower() == "shop");
    //                try
    //                {
    //                    string PageURL = listOfCategories[0].Properties.ToList().Find(lobj => lobj.Name.Equals("PageUrl")).Value.Replace("dotaspx", ".aspx");
    //                    var uriBuilder = new UriBuilder(PageURL);
    //                    var paramValues = HttpUtility.ParseQueryString(uriBuilder.Query);
    //                    string categoryId = HttpUtility.ParseQueryString(uriBuilder.Query).Get("CategoryId");
    //                    List<Category> categories = model.SearchCategories();
    //                    if (categories != null && categories.Count > 0)
    //                    {
    //                        List<Category> mainmenus = categories.FindAll(x => x.ParentId == categoryId);
    //                        sb.Append("<div class=\"swiper-wrapper pb-3\">");
    //                        foreach (var mainMenu in mainmenus)
    //                        {
    //                            if (categories.Exists(x => mainMenu.Id.Equals(x.ParentId)))
    //                            {
    //                                string pstrProductType = "physical";
    //                                sb.Append(string.Format("<div class=\"swiper-slide shadow-sm\"><a href=\"Shop.aspx?CategoryId={2}&ProductType={3}\"><div class=\"img-container\"><img src=\"{0}\"  alt=\"\" /></div><div class=\"d-flex flex-column px-3 py-4 dvCardName\"><h2 class=\"h6 heading-bold text-truncate mb-2 text-white\">{1}<img src=\"images/icons/arrows/right-arrow.svg\" alt=\"\"></h2></div></a></div>"
    //                                , mainMenu.Properties.ToList().Find(lobj => lobj.Name.Equals("ImageUrl")).Value, mainMenu.Name.Contains("'") ? mainMenu.Name.Replace("'", "&#39") : mainMenu.Name, categoryId, pstrProductType));
    //                            }
    //                        }                          
    //                        sb.Append(" <div class=\"swiper-button-prev\">");
    //                        sb.Append("<img src=\"images/icons/arrows/left-yellow-arrow-2.svg\" alt=\"\" />");
    //                        sb.Append("</div>");
    //                        sb.Append("<div class=\"swiper-button-next\">");
    //                        sb.Append("<img src=\"images/icons/arrows/right-yellow-arrow-2.svg\" alt=\"\" />");
    //                        sb.Append("</div>");
    //                        sb.Append("</div>");
    //                        sb.Append("<div class=\"d-flex justify-content-between align-items-center mt-4\">");
    //                        sb.Append(string.Format("<a href=\"Shop.aspx?CategoryId={0}&ProductType=Physical\" class=\"btn btn-one\">View All</a>", categoryId));
    //                        sb.Append("</div>");                         
    //                    }
    //                    result += sb.ToString();
    //                    HttpContext.Current.Application["ShopBestDeals"] = result;
    //                }
    //                catch (Exception ex)
    //                {
    //                    LoggingAdapter.WriteLog("category :" + listOfCategories[0].Name);
    //                }
    //            }
    //        }
    //        else
    //        {
    //            result = Convert.ToString(HttpContext.Current.Application["ShopBestDeals"]);
    //        }

    //    }
    //    catch (Exception ex)
    //    {
    //        LoggingAdapter.WriteLog("Index.aspx GetOffers Exception: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
    //    }
    //    return result;
    //}
    [ScriptMethod()]
    [WebMethod]
    public static string GetVouchers()
    {
        string result = string.Empty;
        try
        {
            if (string.IsNullOrEmpty(Convert.ToString(HttpContext.Current.Application["Vouchers"])))
            {
                ABCModel lobjModel = new ABCModel();
                ShopModel model = new ShopModel();
                Store store = model.GetStoreDetails();
                List<Category> listOfCategories = model.SearchCategories();
                if (listOfCategories != null && listOfCategories.Count > 0)
                {
                    listOfCategories = listOfCategories.FindAll(lobj => lobj.Name == "Global Gift Vouchers");
                    //listOfCategories = listOfCategories.FindAll(lobj => lobj.ParentId.IsNullOrEmpty() && lobj.IsActive && lobj.Name.ToLower() == "vouchers");
                    try
                    {
                        string PageURL = listOfCategories[0].Properties.ToList().Find(lobj => lobj.Name.Equals("PageUrl")).Value.Replace("dotaspx", ".aspx");
                        var uriBuilder = new UriBuilder(PageURL);
                        var paramValues = HttpUtility.ParseQueryString(uriBuilder.Query);
                        string categoryId = HttpUtility.ParseQueryString(uriBuilder.Query).Get("CategoryId");
                        if (!string.IsNullOrEmpty(categoryId))
                        {
                            HttpContext.Current.Session["CategoryId"] = categoryId;
                            HttpContext.Current.Session["BredcrumCategoryId"] = categoryId;
                        }
                        else if (HttpContext.Current.Session["BredcrumCategoryId"] != null)
                        {
                            categoryId = HttpContext.Current.Session["BredcrumCategoryId"].ToString();
                            HttpContext.Current.Session["BredcrumCategoryId"] = categoryId;
                            HttpContext.Current.Session["CategoryId"] = categoryId;
                        }
                        StringBuilder sb = new StringBuilder();
                        if (store.DynamicProperties != null && store.DynamicProperties.Find(x => x.Name.Equals("Current Offers")) != null)
                        {
                            DynamicProperty dynamicProperty = store.DynamicProperties.Find(x => x.Name.Equals("Current Offers"));
                            int index = 0;
                            foreach (var item in dynamicProperty.Values)
                            {
                                sb = new StringBuilder();
                                List<string> Terms = new List<string>();
                                List<string> offers = item.Value.Split('|').ToList();
                                if (offers[0].ToLower().Contains("giftcards"))
                                {
                                    var idName = offers[offers.Count - 1].ToLower() == "featured giftcards" ? "featured giftcards" : "top_offerBox";
                                   
                                    Terms.Add("Tags:" + offers[0]);
                                  
                                    ProductSearchCriteria searchCriteria = model.BuildProductSearchCriteria("", null, ItemResponseGroup.ItemWithPrices, 0, 0, "", null, 0, 6, Terms);
                                    var json = JsonConvert.SerializeObject(searchCriteria);
                                    ProductSearchResult searchResult = model.SearchProducts(searchCriteria);
                                    index++;
                                    if (searchResult != null && searchResult.Products != null && searchResult.Products.Count > 0)
                                    {
                                        sb.Append("<div class=\"dvSwiperCard swiper row\">");
                                        sb.Append("<div class=\"swiper-wrapper pb-3\">");
                                        foreach (var product in searchResult.Products)
                                        {                                          
                                            string pstrProductType = "physical";
                                            if (!string.IsNullOrEmpty(product.ProductType) && product.ProductType.Equals("Digital"))
                                            {
                                                pstrProductType = "digital";
                                            }
                                            sb.Append(string.Format("<div class=\"swiper-slide shadow-sm\"><div class=\"dvProductCard bg-colour6\"><div class=\"dvItem\"><a class=\"anchor\" href=\"ProductDetails.aspx?ProductId={4}&ProductType={2}\"><div class=\"img-container\"><img src=\"{0}\"  alt=\"\" /></div><h2 class=\"px-3 pt-3 pb-2\">{1}</h2><div class=\"d-flex flex-wrap justify-content-between px-3 pb-3\"><p class=\"points\">{3}</p><p class=\"points\">{2}</p></div></a></div></div></div>", product.PrimaryImage.Url, product.Name, pstrProductType, lobjModel.FormatPoints(Math.Ceiling(product.Price.SalePriceWithTax.Amount), "Points"), product.Id));                                            
                                        }
                                        sb.Append("</div>");//swiper wrapper close 
                                        sb.Append("<div class=\"swiper-buttons\">");
                                        sb.Append("<div class=\"swiper-button-prev\">");
                                        sb.Append("<img src=\"images/icons/arrows/left-yellow-arrow-2.svg\" alt=\"\" />");
                                        sb.Append("</div>");
                                        sb.Append("<div class=\"swiper-button-next\">");
                                        sb.Append("<img src=\"images/icons/arrows/right-yellow-arrow-2.svg\" alt=\"\" />");
                                        sb.Append("</div>");
                                        sb.Append("</div>");//swiper buttons close
                                        sb.Append("</div>");//swiper row close
                                        sb.Append("<div class=\"d-flex justify-content-start align-items-center mt-4\">");
                                        sb.Append(string.Format("<a href=\"ShopList.aspx?CategoryId={0}&ProductType=digital\" class=\"btn btn-two\">View All</a>", categoryId));
                                        sb.Append("</div>");
                                    }

                                }
                                result += sb.ToString();

                            }
                            HttpContext.Current.Application["Vouchers"] = result;
                        }
                    }
                    catch (Exception ex)
                    {
                        LoggingAdapter.WriteLog("Index.aspx GetVoucher Exception: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
                    }
                }
            }
            else
            {
                result = Convert.ToString(HttpContext.Current.Application["Vouchers"]);
            }
        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("Index.aspx GetVoucher Exception: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
        }
        return result;
    }

    [ScriptMethod()]
    [WebMethod]
    public static string GetTravelOptions()
    {
        List<Giift.ShopGateway.Client.Entities.Category> listOfCategories = new List<Category>();
        var jsonlistOfCategories = string.Empty;
        try
        {
            ABCModel model = new ABCModel(); 
            ShopModel lobjmodel = new ShopModel();
            listOfCategories = lobjmodel.SearchCategories();
            if (string.IsNullOrEmpty(Convert.ToString(HttpContext.Current.Application["GetTravelOptions"])))
            {
                if (listOfCategories != null && listOfCategories.Count > 0)
                {
                    int count = 0;
                    listOfCategories = listOfCategories.FindAll(lobj => lobj.ParentId.IsNullOrEmpty() && lobj.IsActive).OrderBy(o => o.Priorty).ToList();

                    jsonlistOfCategories = JsonConvert.SerializeObject(listOfCategories);
                    HttpContext.Current.Application["GetTravelOptions"] = jsonlistOfCategories;
                }
            }
        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("GetTravelOptions Exception: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
        }
        return jsonlistOfCategories;
    }

    [ScriptMethod()]
    [WebMethod]
    public static string GetHomeRedemptionOptions()
    {
        List<Giift.ShopGateway.Client.Entities.Category> listOfCategories = new List<Category>();
        var jsonlistOfCategories = string.Empty;
        try
        {
            ABCModel model = new ABCModel();
            ShopModel lobjmodel = new ShopModel();
            listOfCategories = lobjmodel.SearchCategories();
            if (string.IsNullOrEmpty(Convert.ToString(HttpContext.Current.Application["GetHomeRedemptionOptions"])))
            {
                if (listOfCategories != null && listOfCategories.Count > 0)
                {
                    int count = 0;
                    listOfCategories = listOfCategories.FindAll(lobj => lobj.ParentId.IsNullOrEmpty() && lobj.IsActive).OrderBy(o => o.Priorty).ToList();

                    jsonlistOfCategories = JsonConvert.SerializeObject(listOfCategories);
                    HttpContext.Current.Application["GetHomeRedemptionOptions"] = jsonlistOfCategories;
                }
            }
        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("GetRedemptionOptions Exception: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
        }
        Console.WriteLine(listOfCategories);
        return jsonlistOfCategories;
    }
    [ScriptMethod()]
    [WebMethod]
    public static string CheckPointsAvailability()
    {
        string availpoints = string.Empty;
        try
        {
            MemberDetails lobjMemberDetails = HttpContext.Current.Session["MemberDetails"] as MemberDetails;
            //if (string.IsNullOrEmpty(Convert.ToString(HttpContext.Current.Session["AvailablePoints"])))
            //{
            if(lobjMemberDetails != null)
            {
                ABCModel lobjModel = new ABCModel();
                if (HttpContext.Current.Session["Currency"] == null)
                {
                    HttpContext.Current.Session["Currency"] = lobjModel.GetDefaultCurrency();
                }
                ProgramDefinition lobjProgramDefinition = lobjModel.GetProgramMaster();
                MemberRelation lobjMemberRelation = lobjMemberDetails.MemberRelationsList.Find(lobj => lobj.RelationType.Equals(RelationType.LBMS));
                var availablePoints = Convert.ToString(lobjModel.FloatToThousandSeperated(lobjModel.CheckAvailbility(lobjMemberRelation.RelationReference, Convert.ToInt32(RelationType.LBMS), Convert.ToString(HttpContext.Current.Session["Currency"]), lobjProgramDefinition.ProgramId)));
                //HttpContext.Current.Session["AvailablePoints"] = availablePoints;
                availpoints = availablePoints;
            }
            //}
            //else
            //{
            //    availpoints = Convert.ToString(HttpContext.Current.Session["AvailablePoints"]);
            //}
        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("CheckPointsAvailability Exception: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
        }
        return availpoints;
    }
}