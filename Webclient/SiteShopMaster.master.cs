using ABC.Model;
using System;
using System.Collections.Generic;
using System.Web.UI;
using Giift.ShopGateway.Client.Entities;
using Core.Platform.Member.Entites;
using System.Text;
using Framework.EnterpriseLibrary.Adapters;
using GiiftShopGateway.Model;
using System.Web;

public partial class SiteShopMaster : MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                string lstrHostUrl = string.Empty;
                string lstrSourceUrl = string.Empty;
                string lstrLocalPath = string.Empty;
                Uri lobjUri = new Uri(Request.Url.AbsoluteUri);
                lstrHostUrl = lobjUri.Host;
                lstrSourceUrl = HttpContext.Current.Request.UrlReferrer.ToString();
                lstrLocalPath = lobjUri.LocalPath.ToString();
                LoggingAdapter.WriteLog("Source URL: " + lstrSourceUrl + " Host Address: " + lstrHostUrl);
                if (!string.IsNullOrEmpty(Request.QueryString["ProductType"]))
                {
                    if (Convert.ToString(Request.QueryString["ProductType"]).ToLower().Equals("physical"))
                    {
                        aCartBtn.Visible = true;
                        //divShopMenuBar.Visible = true;
                    }
                    else
                    {
                        aCartBtn.Visible = false;
                      //  divShopMenuBar.Visible = false;
                    }
                }
                else
                {
                    aCartBtn.Visible = false;
                    if (lstrLocalPath.Contains("Internet") || lstrLocalPath.Contains("Insurance"))
                    {
                        divShopMenuBar.Visible = false;
                    }
                }
                ShoppingCart lobjShoppingCart = null;
                if (Session["MemberDetails"] != null && Convert.ToString(Request.QueryString["ProductType"]).ToLower().Equals("physical"))
                {
                    ABCModel lobjModel = new ABCModel();
                    ShopModel model = new ShopModel();
                    MemberDetails memberDetails = Session["MemberDetails"] as MemberDetails;
                    lobjShoppingCart = model.LoadOrCreateNewTransientCart(memberDetails);
                    Session["ShoppingCart"] = lobjShoppingCart;
                   // divShopAccount.Visible = true;
                }
                else
                {
                    //divShopAccount.Visible = false;
                }
                if (lobjShoppingCart != null)
                {
                    spanCartItems.InnerText = Convert.ToString(lobjShoppingCart.Items.Count);
                }
            }
            if (Session["CategoryId"] != null && !string.IsNullOrEmpty(Request.QueryString["ProductType"]) && Convert.ToString(Request.QueryString["ProductType"]).ToLower().Equals("physical"))
            {
                BindMenu(Session["CategoryId"].ToString());
            }
        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("SiteShopMaster Page_Load Exception: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
        }
    }
    public void GetMenuLinkList()
    {
        try
        {
            StringBuilder lstrHtmlContent = new StringBuilder();
            ABCModel lobjModel = new ABCModel();
            ShopModel model = new ShopModel();
            List<MenuLinkList> lobjListOfMenuLinkList = new List<MenuLinkList>();
            lobjListOfMenuLinkList = model.GetMenuLinkList();
            if (lobjListOfMenuLinkList != null && lobjListOfMenuLinkList.Count > 0)
            {
                List<MenuLinkList> lobjMainMenuLinkList = new List<MenuLinkList>();
                lstrHtmlContent.Append("<ul class=\"navbar-nav mr-auto\">");
                lobjMainMenuLinkList.Add(lobjListOfMenuLinkList.Find(lobj => lobj.Name.Equals("main-menu")));
                for (int i = 0; i < lobjMainMenuLinkList[0].MenuLinks.Count; i++)
                {
                    MenuLinkList lobjMenuLinkList = new MenuLinkList();
                    lobjMenuLinkList = lobjListOfMenuLinkList.Find(lobj => lobj.Title.ToLower().Equals(lobjMainMenuLinkList[0].MenuLinks[i].Title.ToLower()));
                    if (lobjMenuLinkList != null && lobjMenuLinkList.MenuLinks.Count > 0)
                    {
                        lstrHtmlContent.Append("<li class=\"nav-item dropdown\">"
                            + "<a class=\"nav-link dropdown-toggle d-flex align-items-center justify-content-between text-uppercase\" href=\"#\" id=\"navbarDropdown\" role=\"button\" data-toggle=\"dropdown\" aria-haspopup=\"true\" aria-expanded=\"false\">" + char.ToUpper(lobjMenuLinkList.Title[0]) + lobjMenuLinkList.Title.Substring(1) + "</a>"
                            + "<div class=\"dropdown-menu w-100 py-0\" aria-labelledby=\"navbarDropdown\">");
                        for (int j = 0; j < lobjMenuLinkList.MenuLinks.Count; j++)
                        {
                            lstrHtmlContent.Append("<a class=\"dropdown-item\" href=\"" + lobjMenuLinkList.MenuLinks[j].Url + "\">" + lobjMenuLinkList.MenuLinks[j].Title + "</a>");
                        }
                        lstrHtmlContent.Append("</div></li>");
                    }
                    else
                    {
                        lstrHtmlContent.Append("<li class=\"nav-item dropdown\"><a class=\"nav-link dropdown-toggle d-flex align-items-center justify-content-between text-uppercase\" href=\"" + lobjMainMenuLinkList[0].MenuLinks[i].Url + "\">" + lobjMainMenuLinkList[0].MenuLinks[i].Title + "</a></li>");
                    }
                }
                lstrHtmlContent.Append("</ul>");
            }
            divMenuContent.InnerHtml = lstrHtmlContent.ToString();
        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("SiteShopMaster GetMenuLinkList Exception: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
        }
    }
    public void BindMenu(string categoryId)
    {
        try
        {
            StringBuilder lstrHtmlContent = new StringBuilder();
            ABCModel lobjModel = new ABCModel();
            ShopModel model = new ShopModel();
            List<Category> categories = model.SearchCategories();
            if (categories != null && categories.Count > 0)
            {
                lstrHtmlContent.Append("<div class=\"collapse navbar-collapse\" id=\"dvMenu\"><ul class=\"navbar-nav mr-auto\">");
                List<Category> mainmenus = categories.FindAll(x => x.ParentId == categoryId);
                foreach (var mainMenu in mainmenus)
                {

                    if (categories.Exists(x => mainMenu.Id.Equals(x.ParentId)))
                    {
                        lstrHtmlContent.Append("<li class=\"nav-item dropdown\">" +
                            "<a class=\"nav-link dropdown-toggle d-flex align-items-center justify-content-between text-uppercase\" role=\"button\" data-toggle=\"dropdown\" aria-expanded=\"false\" href=\"#\">" + mainMenu.Name + "</a>");
                        lstrHtmlContent.Append(BindSubMenu(mainMenu.Id, categories));
                    }
                    else
                    {
                        lstrHtmlContent.Append("<li class=\"nav-item dropdown\"><a class=\"nav-link dropdown-toggle d-flex align-items-center justify-content-between text-uppercase\" role=\"button\" data-toggle=\"dropdown\" aria-expanded=\"false\" href=\"" +
                            string.Format("ShopList.aspx?CategoryId={0}", mainMenu.Id + "&ProductType=Physical") + "\">" + mainMenu.Name + "</a>");
                    }
                    lstrHtmlContent.Append("</li>");
                }
                lstrHtmlContent.Append("</ul></div>");
                divMenuContent.InnerHtml = lstrHtmlContent.ToString();
            }
        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("SiteShopMaster BindMenu Exception: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
        }
    }
    public string BindSubMenu(string categoryId, List<Category> categories)
    {
        StringBuilder result = new StringBuilder();
        try
        {
            List<Category> menus = categories.FindAll(x => categoryId.Equals(x.ParentId) && x.IsActive == true);
            if (menus != null && menus.Count > 0)
            {
                result.Append("<div class=\"dropdown-menu w-100 py-0\">");
                foreach (var menu in menus)
                {
                    if (menu.IsActive)
                    {
                        if (categories.Exists(x => menu.Id.Equals(x.ParentId)))
                        {
                            result.Append("<a class=\"dropdown-item\" href=\"#\">" + menu.Name + "</a>");
                            result.Append(BindSubMenu(menu.Id, categories));
                          
                        }
                        else
                        {
                            result.Append("<a class=\"dropdown-item\" href=\"" +
                            string.Format("ShopList.aspx?CategoryId={0}", menu.Id + "&ProductType=Physical") + "\">" + menu.Name + "</a> ");
                        }
                    }
                }
                result.Append("</div>");
            }
        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("SiteShopMaster BindSubMenu Exception: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
        }
        return result.ToString();
    }
}
