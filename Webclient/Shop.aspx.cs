using Giift.ShopGateway.Client.Entities;
using ABC.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Framework.EnterpriseLibrary.Adapters;
using CB.IBE.Platform.AirClientModel;
using Core.Platform.Member.Entites;
using Core.Platform.MemberActivity.Entities;
using GiiftShopGateway.Model;
public partial class Shop : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            Session["CategoryName"] = "shop";
            if (!Page.IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["CategoryId"]))
                {
                    HttpContext.Current.Session["CategoryId"] = Request.QueryString["CategoryId"].ToString();
                    HttpContext.Current.Session["BredcrumCategoryId"] = Request.QueryString["CategoryId"].ToString();

                    try
                    {
                        ABCModel lobjmodel = new ABCModel();
                        ShopModel model = new ShopModel();
                        MemberDetails lobjMemberDetails = null;
                        lobjMemberDetails = HttpContext.Current.Session["MemberDetails"] as MemberDetails;

                        List<Category> categories = model.SearchCategories();
                        List<Category> mainmenus = categories.FindAll(x => x.Id == Request.QueryString["CategoryId"].ToString());

                        lobjmodel.LogActivity(string.Format("Visit Shop.aspx page; CategoryId-:{0}; CategoryName-:{1};", Request.QueryString["CategoryId"].ToString(), mainmenus[0].Name), ActivityType.PageLoad);
                    }
                    catch { }
                }
            }
        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("Shop.aspx Page_Load Exception: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
        }
    }
    [WebMethod]
    public static string GetOffers()
    {
        string result = string.Empty;
        try
        {
            ABCModel lobjModel = new ABCModel();
            ShopModel model = new ShopModel();
            Store store = model.GetStoreDetails();
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
                    if (offers[0].ToLower().Contains("shop"))
                    {
                        var idName = offers[offers.Count - 1] == "Featured Offers" ? "featured_offers" : "top_offerBox";
                        sb.Append("<div class=\"" + idName + " dvBg" + index + "\">");
                        sb.Append("<div class=\"container- shop-\" dir=\"ltr\" style=\"direction: ltr;\">");
                        sb.Append("<div class=\"container\">");
                        sb.Append("<div id=\"div_" + idName + "\" class=\"OfferNICk " + idName + "\">");
                        Terms.Add("Tags:" + offers[0]);
                        ProductSearchCriteria searchCriteria = model.BuildProductSearchCriteria("", null, ItemResponseGroup.ItemWithPrices, 0, 0, "", null, 0, 6, Terms);
                        ProductSearchResult searchResult = model.SearchProducts(searchCriteria);
                        index++;
                        if (searchResult != null && searchResult.Products != null && searchResult.Products.Count > 0)
                        {
                            sb.Append(string.Format("<h3 class=\"heading2 text-colour7 mb-3 mb-lg-4 text-center\"><span>{0}</span></h3>", offers[offers.Count - 1]));
                            sb.Append("<div class='latestArrBlk customizedButtons swiper swiper-initialized swiper-horizontal' id='divshopswiper'>");
                            sb.Append("<div class='swiper-wrapper'>");
                            foreach (var product in searchResult.Products)
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
                                if (!string.IsNullOrEmpty(product.ProductType) && product.ProductType.Equals("Digital"))
                                {
                                    pstrProductType = "digital";
                                }
                                sb.Append(string.Format("<div class='swiper-slide shadow-sm'><a href='ProductDetails.aspx?ProductId={2}&ProductType={5}'> <div class='img-container'><img style='width:100%;' src='{0}' /></div> <div class='bg-white d-flex flex-column p-2'><p class='card-text'> <p class='h6 heading-semibold text-colour7 text-truncate col-12 mb-2' >{1}</p> {4} </p> <p class='h8 heading-regular'>{5}</p>  <p class='h7 heading-regular text-colour7 text-truncate pt-2'>{3}</p> </div> </a></div>"
                                    , product.PrimaryImage.Url, product.Name, product.Id, lobjModel.FormatPoints(Math.Ceiling(product.Price.SalePriceWithTax.Amount), "NPoints"), string.Format("<div class='starRat'>{0}</div>", starratings), pstrProductType));
                            }
                            sb.Append("</div>");
                            sb.Append(string.Format("<div class='d-flex justify-content-between align-items-center mt-4'><a href='ShopList.aspx?terms=Tags:{0}&ProductType=Physical' class='btn btn-one' data-i18n='home-view-btn'>View all</a> <div class='swiper-buttons'><div class='d-flex justify-content-center'><div class='swiper-button-prev pr-3' id='shop_swiper-button-prev'><img src ='images/icons/arrows/left-black-arrow.svg' /></div><div class='swiper-button-next' id='shop_swiper-button-next'><img src ='images/icons/arrows/right-black-arrow.svg' /></div></div></div></div>", offers[0]));
                            sb.Append("</div>");
                            
                        }
                        sb.Append("</div></div></div></div>");
                    }
                    result += sb.ToString();
                }
            }
        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("Shop.aspx GetOffers Exception: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
        }
        return result;
    }
}