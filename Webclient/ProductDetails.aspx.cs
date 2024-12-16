using ABC.Model;
using Core.Platform.Member.Entites;
using Giift.ShopGateway.Client.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Framework.EnterpriseLibrary.Adapters;
using Core.Platform.ProgramMaster.Entities;
using Core.Platform.OTP.Entities;
using Core.Platform.MemberActivity.Constants;
using Core.Platform.MemberActivity.Entities;
using GiiftShopGateway.Model;

public partial class ProductDetails : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string lstrProductId = string.Empty;
            string lstrProductType = string.Empty;
            string lstrcategoryId = string.Empty;
            if (!string.IsNullOrEmpty(Convert.ToString(Request.QueryString["ProductId"]))
                && !string.IsNullOrEmpty(Convert.ToString(Request.QueryString["ProductType"])))
            {
                lstrProductId = Convert.ToString(Request.QueryString["ProductId"]);
                lstrProductType = Convert.ToString(Request.QueryString["ProductType"]);
                if (Session["MemberDetails"] == null)
                {
                    string CallbackUrl = HttpUtility.UrlEncode(Encrypt("ProductDetails.aspx?ProductId=" + lstrProductId + "&ProductType=" + lstrProductType));
                    Response.Redirect("Login.aspx", false);
                }
                else
                {
                    ABCModel lobjModel = new ABCModel();
                    ShopModel model = new ShopModel();
                    lobjModel.LogActivity(string.Format("Visit ProductDetails.aspx; ProductId-:{0}; ProductType-:{1};", lstrProductId, lstrProductType), ActivityType.PageLoad);

                    BindProductDetails(lstrProductId);
                    StringBuilder lstrHtmlContent = new StringBuilder();

                    List<Category> categories = model.SearchCategories();
                    if (!Page.IsPostBack)
                    {
                        if (Session["BredcrumCategoryId"] != null)
                        {
                            lstrcategoryId = Session["BredcrumCategoryId"].ToString();
                            Session["BredcrumCategoryId"] = lstrcategoryId;
                        }
                        else if (Session["BredcrumCategoryVoucherId"] != null)
                        {
                            lstrcategoryId = Session["BredcrumCategoryVoucherId"].ToString();
                            Session["BredcrumCategoryVoucherId"] = lstrcategoryId;
                        }
                        if (!string.IsNullOrEmpty(Request.QueryString["ProductType"]))
                        {
                            lstrProductType = Request.QueryString["ProductType"].ToString();
                        }
                        if (lstrProductType.ToLower() == "physical")
                        {
                            List<Category> mainmenus = categories.FindAll(x => x.Id == lstrcategoryId);
                            foreach (var mainMenu in mainmenus)
                            {
                                lstrHtmlContent.Append("<ul class=\"breadcrumb px-0 py-3\">" +
                                    "<li class=\"mr-3\"><a href=\"\\\"><img class=\"\" src=\"images/icons/arrows/back-arrow.svg\"></a></</li>" +
                                    "<li class=\"breadcrumb-item\"><a href=\"Index.aspx\">Home</a></li>" +
                                    "<li class=\"breadcrumb-item \"><a href =\"Shop.aspx?CategoryId=" + lstrcategoryId + "&ProductType=Physical" + "\">Shop</a></li>" +
                                    "<li class=\"breadcrumb-item\">Product Detail</li>" +
                                    "</ul>");
                                btnBack.HRef = "Shop.aspx?CategoryId=" + lstrcategoryId + "&ProductType=Physical";
                            }
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(lstrcategoryId))
                            {
                                List<Category> mainmenus = categories.FindAll(x => x.Id == lstrcategoryId);
                                lstrHtmlContent.Append("<ul class=\"breadcrumb px-0 py-3\">" +
                                    "<li class=\"mr-3\"><a href=\"\\\"><img class=\"\" src=\"images/icons/arrows/back-arrow.svg\"></a></</li>" +
                                    "<li class=\"breadcrumb-item\"><a href=\"Index.aspx\">Home</a></li>" +
                                    "<li class=\"breadcrumb-item \"><a href =\"Shoplist.aspx?CategoryId=" + lstrcategoryId + "&ProductType=Digital" + "\">" + mainmenus[0].Name + "</a></li>" +
                                    "<li class=\"breadcrumb-item\">Product Detail</li>" +
                                    "</ul>");
                                btnBack.HRef = "Shoplist.aspx?CategoryId=" + lstrcategoryId + "&ProductType=Digital";
                            }
                        }
                        MemberDetails lobjMemberDetails = new MemberDetails();
                        lobjMemberDetails = HttpContext.Current.Session["MemberDetails"] as MemberDetails;
                        if (string.IsNullOrEmpty(lobjMemberDetails.Email))
                        {
                            divEmailErrorMsg.Visible = true;
                            btnredeem.Visible = false;
                        }
                        else
                        {
                            divEmailErrorMsg.Visible = false;
                            btnredeem.Visible = true;
                        }
                    }
                    divBreadbrums.InnerHtml = lstrHtmlContent.ToString();
                }
            }
            else
            {
                if (Session["MemberDetails"] == null)
                {
                    Response.Redirect("Login.aspx", false);
                }
                else
                {
                    Response.Redirect("Index.aspx", false);
                }
            }
        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("ProductDetails.aspx Page_Load Exception: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
        }
    }
    public void BindProductDetails(string pstrProductId)
    {
        try
        {
            HttpContext.Current.Session["Product"] = null;
            ABCModel lobjModel = new ABCModel();
            ShopModel model = new ShopModel();
            Product lobjProduct = new Product();
            Product lobjProductVariants = new Product();
            lobjProduct = model.GetProductById(pstrProductId);
            if (lobjProduct != null)
            {
                lobjModel.LogActivity(string.Format("BindProductDetails; ProductId-:{0}; ProductName-:{1};", pstrProductId, lobjProduct.Name), ActivityType.Merchant);
                HttpContext.Current.Session["Product"] = lobjProduct;
                lobjProductVariants = SystemExtension.Clone(lobjProduct);
                lobjProductVariants.Variations.Clear();
                lobjProduct.Variations.Add(lobjProductVariants);
                HttpContext.Current.Session["Product"] = lobjProduct;
                hfProductId.Value = lobjProduct.Id;
                hfProductType.Value = lobjProduct.ProductType;
                CatalogProperty lobjMetas = new CatalogProperty();
                lobjMetas = lobjProduct.Properties.FirstOrDefault(lobj => lobj.Name.Equals("Metas"));
                if (lobjMetas != null && !string.IsNullOrEmpty(lobjMetas.Value))
                {
                    hfUserInputMetasAvailable.Value = "true";
                    string lstrFormElement = lobjMetas.Value.Replace(@"\", "");
                    dynamic formContents = JsonConvert.DeserializeObject(lstrFormElement);
                    if (formContents != null)
                    {
                        string lstrFormJson = formContents.form.ToString();
                        lstrFormJson = lstrFormJson.TrimStart('{');
                        lstrFormJson = lstrFormJson.TrimEnd('}');
                        StringBuilder sb = new StringBuilder();
                        sb.Append("<script src = 'Jquery/Underscore.js' ></script>");
                        sb.Append("<script src = 'Jquery/jsonform.js' ></script>");
                        sb.Append("<script type = 'text/javascript'>");
                        sb.Append("$('#divDynamicContent').jsonForm({");
                        sb.Append(lstrFormJson);
                        sb.Append(",'form': [");
                        sb.Append("'*',{");
                        sb.Append("'type': 'button',");
                        sb.Append("'onClick': function(evt) {");
                        sb.Append("evt.preventDefault();");
                        sb.Append("}");
                        sb.Append("}");
                        sb.Append("]");
                        sb.Append(",\"validate\": false");
                        sb.Append("});");
                        sb.Append("</script>");
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", sb.ToString());
                    }
                }
                else
                {
                    hfUserInputMetasAvailable.Value = "false";
                }
                spanProductName.InnerText = lobjProduct.Name;
                spanPoints.InnerText = lobjModel.FormatPoints(Math.Ceiling(lobjProduct.Price.SalePriceWithTax.Amount), "Points");

                string lstrProductImgContent = string.Empty;
                for (int i = 0; i < lobjProduct.Images.Count; i++)
                {
                    lstrProductImgContent += "<div class=\"swiper-slide img-container\"><img src=\"" + lobjProduct.Images[i].Url + "\"></div>";
                }
                imgProductImageMain.InnerHtml = lstrProductImgContent;
                //imgProductImageMain.Src = lobjProduct.PrimaryImage.Url;
                if (!string.IsNullOrEmpty(lobjProduct.ProductType) && lobjProduct.ProductType == "Digital")
                {
                    quantity.Attributes.Add("min", Convert.ToString(ConfigurationManager.AppSettings["DigitalProductMinQty"]));
                    quantity.Attributes.Add("max", Convert.ToString(ConfigurationManager.AppSettings["DigitalProductMaxQty"]));
                    lbldivPoints.Visible = false;
                    divspanpoints.Visible = false;
                    divQuantity.Attributes.Add("Style", "display:none");
                }
                else
                {
                    quantity.Attributes.Add("min", Convert.ToString(lobjProduct.MinQuantity));
                    quantity.Attributes.Add("max", Convert.ToString(lobjProduct.MaxQuantity));
                    divspanpoints.Visible = true;
                    lbldivPoints.Visible = true;
                    divQuantity.Attributes.Add("Style", "display:block");
                    divspanpoints.InnerText = lobjModel.FloatToThousandSeperated((float)Math.Ceiling(lobjProduct.Price.SalePriceWithTax.Amount));
                }
                string lstrHtmlContent = string.Empty;
                for (int i = 0; i < lobjProduct.Images.Count; i++)
                {
                    lstrHtmlContent += "<div class=\"swiper-slide\"><img src=\"" + lobjProduct.Images[i].Url + "\"></div>";
                }
                List<string> lobjColorVariations = new List<string>();
                List<string> lobjSizeVariations = new List<string>();
                List<string> lobjStorageVariations = new List<string>();
                List<string> lobjValueVariations = new List<string>();
                if (lobjProduct.VariationProperties.Any(lobj => lobj.Name.Equals("Color")))
                {
                    lobjColorVariations.Add(lobjProduct.VariationProperties.Find(lobj => lobj.Name.Equals("Color")).Value);
                }
                if (lobjProduct.VariationProperties.Any(lobj => lobj.Name.Equals("Size")))
                {
                    lobjSizeVariations.Add(lobjProduct.VariationProperties.Find(lobj => lobj.Name.Equals("Size")).Value);
                }
                if (lobjProduct.VariationProperties.Any(lobj => lobj.Name.Equals("Storage")))
                {
                    lobjStorageVariations.Add(lobjProduct.VariationProperties.Find(lobj => lobj.Name.Equals("Storage")).Value);
                }
                if (lobjProduct.VariationProperties.Any(lobj => lobj.Name.Equals("Value")))
                {
                    lobjValueVariations.Add(lobjProduct.VariationProperties.Find(lobj => lobj.Name.Equals("Value")).Value);
                }
                foreach (Product variant in lobjProduct.Variations)
                {
                    if (variant.VariationProperties.Count > 0)
                    {
                        if (variant.VariationProperties.Any(lobj => lobj.Name.Equals("Color")))
                        {
                            lobjColorVariations.Add(variant.VariationProperties.Find(lobj => lobj.Name.Equals("Color")).Value);
                        }
                        if (variant.VariationProperties.Any(lobj => lobj.Name.Equals("Size")))
                        {
                            lobjSizeVariations.Add(variant.VariationProperties.Find(lobj => lobj.Name.Equals("Size")).Value);
                        }
                        if (variant.VariationProperties.Any(lobj => lobj.Name.Equals("Storage")))
                        {
                            lobjStorageVariations.Add(variant.VariationProperties.Find(lobj => lobj.Name.Equals("Storage")).Value);
                        }
                        if (variant.VariationProperties.Any(lobj => lobj.Name.Equals("Value")))
                        {
                            lobjValueVariations.Add(variant.VariationProperties.Find(lobj => lobj.Name.Equals("Value")).Value);
                        }
                    }
                }
                divColor.Visible = false;
                divSize.Visible = false;
                divStorage.Visible = false;
                divDenomination.Visible = false;
                if (lobjColorVariations != null && lobjColorVariations.Count > 0)
                {
                    lobjColorVariations = lobjColorVariations.OrderBy(a => a).ToList();
                    lobjColorVariations = lobjColorVariations.OrderBy(a => a.Length).ToList();
                    string lstrHtml = string.Empty;
                    lstrHtml += "<div class=\"row\">";
                    foreach (var color in lobjColorVariations.Distinct().ToList())
                    {
                        if (lobjProduct.VariationProperties.Find(lobj => lobj.Name.Equals("Color")).Value == color)
                        {
                            hfColor.Value = color;
                            lstrHtml += "<div class=\"col-6 col-sm-3\"><button type=\"button\" class=\"btn-denomination col-6 btn btnColorChange btn-success selected\" style=\"margin: 10px\" onclick=\"ColorChange('" + color + "', this)\">" + color + "</button></div>";
                        }
                        else
                        {
                            lstrHtml += "<div class=\"col-6 col-sm-3\"><button type=\"button\" class=\"btn-denomination col-6 btn btnColorChange btn-success\" style=\"margin: 10px\" onclick=\"ColorChange('" + color + "', this)\">" + color + "</button></div>";
                        }
                    }
                    lstrHtml += "</div>";
                    spanColor.InnerHtml = lstrHtml;
                    divColor.Visible = true;
                }
                if (lobjSizeVariations != null && lobjSizeVariations.Count > 0)
                {
                    lobjSizeVariations = lobjSizeVariations.OrderBy(a => a).ToList();
                    lobjSizeVariations = lobjSizeVariations.OrderBy(a => a.Length).ToList();
                    string lstrHtml = string.Empty;
                    lstrHtml += "<div class=\"row\">";
                    foreach (var size in lobjSizeVariations.Distinct().ToList())
                    {
                        if (lobjProduct.VariationProperties.Find(lobj => lobj.Name.Equals("Size")).Value == size)
                        {
                            hfSize.Value = size;
                            lstrHtml += "<div class=\"col-6 col-sm-3\"><button type=\"button\" class=\"btn-denomination col-6 btn btnSizeChange btn-success selected\" style=\"margin: 10px\" onclick=\"SizeChange('" + size + "', this)\">" + size + "</button></div>";
                        }
                        else
                        {
                            lstrHtml += "<div class=\"col-6 col-sm-3\"><button type=\"button\" class=\"btn-denomination col-6 btn btnSizeChange btn-success\" style=\"margin: 10px\" onclick=\"SizeChange('" + size + "', this)\">" + size + "</button></div>";
                        }
                    }
                    lstrHtml += "</div>";
                    spanSize.InnerHtml = lstrHtml;
                    divSize.Visible = true;
                }
                if (lobjStorageVariations != null && lobjStorageVariations.Count > 0)
                {
                    lobjStorageVariations = lobjStorageVariations.OrderBy(a => a).ToList();
                    lobjStorageVariations = lobjStorageVariations.OrderBy(a => a.Length).ToList();
                    string lstrHtml = string.Empty;
                    lstrHtml += "<div class=\"row\">";
                    foreach (var storage in lobjStorageVariations.Distinct().ToList())
                    {
                        if (lobjProduct.VariationProperties.Find(lobj => lobj.Name.Equals("Storage")).Value == storage)
                        {
                            hfStorage.Value = storage;
                            lstrHtml += "<div class=\"col-6 col-sm-3\"><button type=\"button\" class=\"btn-denomination col-6 btn btnStorageChange selected\" onclick=\"StorageChange('" + storage + "', this)\">" + storage + "</button></div>";
                        }
                        else
                        {
                            lstrHtml += "<div class=\"col-6 col-sm-3\"><button type=\"button\" class=\"btn-denomination col-6 btn btnStorageChange\" onclick=\"StorageChange('" + storage + "', this)\">" + storage + "</button></div>";
                        }
                    }
                    lstrHtml += "</div>";
                    spanStorage.InnerHtml = lstrHtml;
                    divStorage.Visible = true;
                }
                if (lobjValueVariations != null && lobjValueVariations.Count > 0)
                {
                    lobjValueVariations = lobjValueVariations.OrderBy(a => a).ToList();
                    lobjValueVariations = lobjValueVariations.OrderBy(a => a.Length).ToList();
                    string lstrHtml = string.Empty;
                    lstrHtml += "<div class=\"row\">";
                    foreach (var denomination in lobjValueVariations.Distinct().ToList())
                    {
                        if (lobjProduct.VariationProperties.Find(lobj => lobj.Name.Equals("Value")).Value == denomination)
                        {
                            hfValue.Value = denomination;
                            lstrHtml += "<div class=\"col-6 col-sm-2 mb-3\"><button type=\"button\" class=\"btn-denomination btn btnDenominationChange w-100 selected\" onclick=\"DenominationChange('" + denomination + "', this)\">" + denomination + "</button></div>";
                        }
                        else
                        {
                            lstrHtml += "<div class=\"col-6 col-sm-2 mb-3\"><button type=\"button\" class=\"btn-denomination btn btnDenominationChange w-100\" onclick=\"DenominationChange('" + denomination + "', this)\">" + denomination + "</button></div>";
                        }
                    }
                    lstrHtml += "</div>";
                    if (lobjProduct.Properties.ToList().Find(lobj => lobj.DisplayName.Equals("Type")).Value == "GiftCard")
                    {
                        if (lobjProduct.Properties.Any(lobj => lobj.DisplayName.Equals("CCY")))
                        {
                            string DenominationName = lobjProduct.Properties.FirstOrDefault(lobj => lobj.DisplayName.Equals("CCY")).Value;
                            lblDenomination.InnerHtml += " <b>(" + DenominationName + ")</b>";
                        }
                    }
                    spanDenomination.InnerHtml = lstrHtml;
                    divDenomination.Visible = true;
                }
                divThumbnailImages.InnerHtml = lstrHtmlContent;
                string longDescription = string.Empty;
                if (lobjProduct.Properties.ToList().Find(lobj => lobj.Name.Equals("LongDescription")) != null)
                {
                    var property = lobjProduct.Properties.ToList().Find(lobj => lobj.Name.Equals("LongDescription"));
                    if (!string.IsNullOrEmpty(lobjProduct.Properties.ToList().Find(lobj => lobj.Name.Equals("LongDescription")).Value))
                    {
                        longDescription = lobjProduct.Properties.ToList().Find(lobj => lobj.Name.Equals("LongDescription")).Value;
                    }
                    else if (property.LocalizedValues != null && property.LocalizedValues.Count > 0)
                    {
                        longDescription = lobjProduct.Properties.ToList().Find(lobj => lobj.Name.Equals("LongDescription")).LocalizedValues.ToList()[0].Value;
                    }
                }
                divDescription.InnerHtml = longDescription;
                string lstrTermsandCondtions = string.Empty;
                if (lobjProduct.Properties.ToList().Find(lobj => lobj.Name.Equals("TermsAndCondition")) != null)
                {
                    var property = lobjProduct.Properties.ToList().Find(lobj => lobj.Name.Equals("TermsAndCondition"));
                    if (!string.IsNullOrEmpty(lobjProduct.Properties.ToList().Find(lobj => lobj.Name.Equals("TermsAndCondition")).Value))
                    {
                        lstrTermsandCondtions = lobjProduct.Properties.ToList().Find(lobj => lobj.Name.Equals("TermsAndCondition")).Value;
                    }
                    else if (property.LocalizedValues != null && property.LocalizedValues.Count > 0)
                    {
                        lstrTermsandCondtions = lobjProduct.Properties.ToList().Find(lobj => lobj.Name.Equals("TermsAndCondition")).LocalizedValues.ToList()[0].Value;
                    }
                    if (string.IsNullOrEmpty(lstrTermsandCondtions))
                    {
                        litermsandcondition.Style.Add("display", "none");
                    }
                    else
                    {
                        litermsandcondition.Style.Add("display", "block");
                    }
                }
                divTermsandCondition.InnerHtml = lstrTermsandCondtions;
                string shortDescription = string.Empty;
                if (!string.IsNullOrEmpty(lobjProduct.Properties.ToList().Find(lobj => lobj.Name.Equals("ShortDescription")).Value))
                {
                    shortDescription = lobjProduct.Properties.ToList().Find(lobj => lobj.Name.Equals("ShortDescription")).Value;
                    divSpecificationtab.Visible= true;
                    divSpecification.InnerHtml = shortDescription;
                }
                string starratings = string.Empty;
                if (lobjProduct.Ratings.Equals(1))
                {
                    starratings = "<div class='starRat'><img src='Images/blackstar.png'/></div>";
                }
                else if (lobjProduct.Ratings > 1 && lobjProduct.Ratings < 2)
                {
                    starratings = "<div class='starRat'><img src='Images/blackstar.png'/><img src='Images/blackhalfstar.png'/></div>";
                }
                else if (lobjProduct.Ratings.Equals(2))
                {
                    starratings = "<div class='starRat'><img src='Images/blackstar.png'/><img src='Images/blackstar.png'/></div>";
                }
                else if (lobjProduct.Ratings > 2 && lobjProduct.Ratings < 3)
                {
                    starratings = "<div class='starRat'><img src='Images/blackstar.png'/><img src='Images/blackstar.png'/><img src='Images/blackhalfstar.png'/></div>";
                }
                else if (lobjProduct.Ratings.Equals(3))
                {
                    starratings = "<div class='starRat'><img src='Images/blackstar.png'/><img src='Images/blackstar.png'/><img src='Images/blackstar.png'/></div>";
                }
                else if (lobjProduct.Ratings > 3 && lobjProduct.Ratings < 4)
                {
                    starratings = "<div class='starRat'><img src='Images/blackstar.png'/><img src='Images/blackstar.png'/><img src='Images/blackstar.png'/><img src='Images/blackhalfstar.png'/></div>";
                }
                else if (lobjProduct.Ratings.Equals(4))
                {
                    starratings = "<div class='starRat'><img src='Images/blackstar.png'/><img src='Images/blackstar.png'/><img src='Images/blackstar.png'/><img src='Images/blackstar.png'/></div>";
                }
                else if (lobjProduct.Ratings > 4 && lobjProduct.Ratings < 5)
                {
                    starratings = "<div class='starRat'><img src='Images/blackstar.png'/><img src='Images/blackstar.png'/><img src='Images/blackstar.png'/><img src='Images/blackstar.png'/><img src='Images/blackhalfstar.png'/></div>";
                }
                else if (lobjProduct.Ratings.Equals(5))
                {
                    starratings = "<div class='starRat'><img src='Images/blackstar.png'/><img src='Images/blackstar.png'/><img src='Images/blackstar.png'/><img src='Images/blackstar.png'/><img src='Images/blackstar.png'/></div>";
                }
                ratings.InnerHtml = starratings;
                if (IsValidProduct(lobjProduct))
                {
                    divBuy.Style.Add("display", "block");
                    divOutOfStock.Style.Add("display", "none");
                    if (CheckAvailability(Convert.ToInt32(lobjProduct.Price.SalePrice.TruncatedAmount), 1))
                    {
                        divBuy.Style.Add("display", "block");
                        divInsufficient.Style.Add("display", "none");
                    }
                    else
                    {
                        divBuy.Style.Add("display", "none !important");
                        divInsufficient.Style.Add("display", "block");
                    }
                }
                else
                {
                    divBuy.Style.Add("display", "none !important");
                    divOutOfStock.Style.Add("display", "block");
                }
            }
        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("ProductDetails.aspx BindProductDetails Exception: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
        }
    }
    [WebMethod]
    public static string AddItemToCart(string lstrProductId, int lintQty, string lstrUserInputMetas)
    {
        string lstrResponse = string.Empty;
        try
        {
            ABCModel lobjModel = new ABCModel();
            ShopModel model = new ShopModel();
            if (HttpContext.Current.Session["MemberDetails"] != null)
            {
                Store lobjStore = model.GetStoreDetails();
                ShoppingCart lobjShoppingCart = null;
                MemberDetails lobjMemberDetails = new MemberDetails();
                lobjMemberDetails = HttpContext.Current.Session["MemberDetails"] as MemberDetails;
                if (HttpContext.Current.Session["ShoppingCart"] != null)
                {
                    lobjShoppingCart = HttpContext.Current.Session["ShoppingCart"] as ShoppingCart;
                }
                else
                {
                    lobjShoppingCart = model.LoadOrCreateNewTransientCart(lobjMemberDetails);
                }
                if (HttpContext.Current.Session["Product"] != null)
                {
                    Product lobjProduct = HttpContext.Current.Session["Product"] as Product;
                    if (IsValidProduct(lobjProduct))
                    {
                        int qty = 0;
                        if (lobjShoppingCart.Items.Any(lobj => lobj.ProductId.Equals(lobjProduct.Id)))
                        {
                            qty = lobjShoppingCart.Items.Find(lobj => lobj.ProductId.Equals(lobjProduct.Id)).Quantity + lintQty;
                        }
                        else
                        {
                            qty = lintQty;
                        }
                        if ((lobjProduct.ProductType != null && lobjProduct.ProductType.Equals("Digital")) || qty <= lobjProduct.MaxQuantity && qty >= lobjProduct.MinQuantity)
                        {
                            lobjShoppingCart = model.AddOrUpdateItemInCart(lobjShoppingCart.Id, lstrProductId, lintQty, lstrUserInputMetas);
                            if (lobjShoppingCart != null)
                            {
                                HttpContext.Current.Session["ShoppingCart"] = lobjShoppingCart;
                                lstrResponse = "SUCCESS";
                            }
                        }
                        else
                        {
                            lstrResponse = "QUANTITYERROR";
                            LoggingAdapter.WriteLog("Add to cart Failed for ID :-" + lstrProductId);
                        }
                        lobjModel.LogActivity(string.Format(ActivityConstants.AddToCart, lobjShoppingCart.Id, lstrResponse, lstrProductId, lintQty, lstrUserInputMetas), ActivityType.AddtoCart);
                    }
                    else
                    {
                        lstrResponse = "Invalid Product";

                    }
                }
            }
        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("ProductDetails.aspx AddItemToCart Exception: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
        }
        return lstrResponse;
    }
    [WebMethod]
    public static string CheckoutGenerateOTP(string lstrProductId, int lintQty, string lstrUserInputMetas)
    {
        string lstrResponse = string.Empty;
        ABCModel lobjModel = new ABCModel();
        ShopModel model = new ShopModel();
        try
        {
            if (HttpContext.Current.Session["MemberDetails"] != null)
            {

                MemberDetails lobjMemberDetails = HttpContext.Current.Session["MemberDetails"] as MemberDetails;
                if (HttpContext.Current.Session["Product"] != null)
                {
                    Product lobjProductDetails = new Product();
                    lobjProductDetails = HttpContext.Current.Session["Product"] as Product;
                    if (IsValidProduct(lobjProductDetails))
                    {
                        ShoppingCart lobjShoppingCart = model.ExpressCheckout(lstrProductId, lintQty, lobjMemberDetails, lstrUserInputMetas);
                        string lstrCurrency = lobjModel.GetDefaultCurrency();
                        MemberRelation lobjMemberRelation = lobjMemberDetails.MemberRelationsList.Find(lobj => lobj.RelationType.Equals(RelationType.LBMS));
                        int lintPoints = Convert.ToInt32(lobjShoppingCart.Price.Total.Amount);
                        ProgramDefinition lobjProgramDefinition = lobjModel.GetProgramMaster();
                        int pintAvailablePoints = lobjModel.CheckAvailbility(lobjMemberRelation.RelationReference, Convert.ToInt32(lobjMemberRelation.RelationType), lstrCurrency, lobjProgramDefinition.ProgramId);
                        List<RedemptionKeys> lobjRedemptionKeys = new List<RedemptionKeys>();
                        lobjRedemptionKeys = lobjModel.GetAllRedemptionKeys(lobjProgramDefinition.ProgramId);
                        HttpContext.Current.Session["ShoppingCart"] = lobjShoppingCart;
                        int ThreshouldValue = lobjRedemptionKeys == null ? -1 : lobjRedemptionKeys.Find(lobj => lobj.RedemptionCode.Equals(RedemptionCodeKeys.GIFTCARD.ToString()) && lobj.Currency.Equals(lstrCurrency)).OTPThreshold;
                        if ((pintAvailablePoints - lintPoints) <= 0)
                        {
                            lstrResponse = "INSUFFICIENT_POINTS";
                            //if (lobjProductDetails.ProductType == "Digital")
                            //{
                            //    HttpContext.Current.Session["BookingFlag"] = "digitalproduct";
                            //    lstrResponse = "/PaymentOptions.aspx";
                            //}
                            //else
                            //{
                            //    HttpContext.Current.Session["BookingFlag"] = "physicalproduct";
                            //    lstrResponse = "/Checkout.aspx";
                            //}
                        }
                        else
                        {
                            if (lobjProductDetails.ProductType == "Digital")
                            {
                                HttpContext.Current.Session["BookingFlag"] = "digitalproduct";

                                Dictionary<string, string> ldictobjParameter = new Dictionary<string, string>
                                    {
                                        { "lstrProductId", lstrProductId },
                                        { "lintQty", Convert.ToString(lintQty) },
                                        { "lstrUserInputMetas", lstrUserInputMetas }
                                    };
                                HttpContext.Current.Session["CheckoutMethodParameter"] = ldictobjParameter;
                                //lstrResponse = "/PaymentOptions.aspx";
                                if (ThreshouldValue <= lintPoints && !ThreshouldValue.Equals(-1) && HttpContext.Current.Session["RelationshipManager"] == null)
                                {
                                    bool Status = false;
                                    OTPDetails lobjOTPDetails = new OTPDetails
                                    {
                                        UniquerefID = lobjMemberDetails.MemberRelationsList.Find(l => l.RelationType.Equals(RelationType.LBMS)).RelationReference,
                                        OtpEnumTypes = OTPEnumTypes.SHOPDIGITALREVIEWNCONFIRM,
                                        OtpType = Convert.ToString(OTPEnumTypes.SHOPDIGITALREVIEWNCONFIRM),
                                        AdditionalDetails = lobjModel.FloatToThousandSeperated((float)lobjModel.CalculateAmount(lintPoints,
                                        Convert.ToDouble(lobjModel.GetAllCurrencyDefinition(lobjProgramDefinition.ProgramId).FindAll(x => x.Currency.ToLower() == lstrCurrency.ToLower())[0].RedemptionRate)))
                                    };
                                    HttpContext.Current.Session["OtpDetails"] = lobjOTPDetails as OTPDetails;
                                    //Status = lobjModel.GenerateReviewnConfirmOTP(lobjOTPDetails, lobjMemberDetails);
                                    Status = lobjModel.SendOTPEmailAndSMS(lobjMemberDetails, "redemption_otp", lobjOTPDetails, lobjProductDetails.Properties.ToList().Find(x => x.Name.Equals("Type")).Value);
                                    if (Status)
                                    {
                                        lobjModel.LogActivity(string.Format(ActivityConstants.ReviewConfirmOTP, "ShopDigital", lobjOTPDetails.UniquerefID, "Success"), ActivityType.ReviewConfirmOTPSuccess);
                                        //lstrResponse = "/ValidateOTP.aspx?flag=ShopDigital&redemptiontype=" + lobjProductDetails.Properties.ToList().Find(x => x.Name.Equals("Type")).Value;
                                        lstrResponse = "/ValidateOTP.aspx?flag=ShopDigital";
                                    }
                                    else
                                    {
                                        lobjModel.LogActivity(string.Format(ActivityConstants.ReviewConfirmOTP, "ShopDigital", lobjOTPDetails.UniquerefID, "Failed"), ActivityType.ReviewConfirmOTPFailed);
                                        lstrResponse = "/OrderStatus.aspx?Status=false";
                                    }
                                }
                                else
                                {
                                    lstrResponse = "/PointGateway.aspx?flag=ShopDigital";
                                }
                            }
                            else
                            {
                                lstrResponse = "/Checkout.aspx";
                            }
                        }
                    }
                    else
                    {
                        lstrResponse = "Invalid Product";
                    }
                }
            }
            else
            {
                lstrResponse = "SESSION_TIME_OUT";
            }
        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("ProductDetails.aspx CheckoutGenerateOTP Ex-" + ex.InnerException + Environment.NewLine + ex.StackTrace + Environment.NewLine + ex.Message);
        }
        lobjModel.LogActivity(string.Format("ProductDetails Checkout; ProductId-:{0}; Qty-:{1}; UserInputMetas-:{2}; Response-:{3}; ", lstrProductId, lintQty, lstrUserInputMetas, lstrResponse), ActivityType.Merchant);
        return lstrResponse;
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
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("ProductDetails.aspx Encrypt Exception: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
        }
        return clearText;
    }
    [WebMethod]
    public static string BindReviews(string ProductId, int PageNo)
    {
        string result = string.Empty;
        try
        {
            int PageSize = 5;
            ABCModel model = new ABCModel();
            ShopModel lobjmodel = new ShopModel();
            CustomerReviewSearchResult searchResult = lobjmodel.GetCustomerReviews(ProductId, "", (PageNo - 1) * PageSize, PageSize);
            if (searchResult != null)
            {
                string Template = "<div class='review'> <span class='glyphicon glyphicon-calendar' aria-hidden='true'></span> <meta itemprop='datePublished' content='01-01-2016'> {0} {1} <div class='userBold'> by {2}</div> <p class='blockquote'> <p class='mb-0'>{3}</p> </p> <hr> </div>";
                StringBuilder sb = new StringBuilder();
                foreach (var item in searchResult.Results)
                {
                    string starratings = string.Empty;
                    switch (item.Rating)
                    {
                        case 1:
                            starratings = "<div><span class='fa fa-star'></span></div>";
                            break;
                        case 2:
                            starratings = "<div><span class='fa fa-star'></span><span class='fa fa-star'></span></div>";
                            break;
                        case 3:
                            starratings = "<div><span class='fa fa-star'></span><span class='fa fa-star'></span><span class='fa fa-star'></span></div>";
                            break;
                        case 4:
                            starratings = "<div><span class='fa fa-star'></span><span class='fa fa-star'></span><span class='fa fa-star'></span><span class='fa fa-star'></span></div>";
                            break;
                        case 5:
                            starratings = "<div><span class='fa fa-star'></span><span class='fa fa-star'></span><span class='fa fa-star'></span><span class='fa fa-star'></span><span class='fa fa-star'></span></div>";
                            break;
                    }
                    sb.Append(string.Format(Template, item.CreatedDate.ToString("dddd, dd MMMM yyyy hh:mm tt"), starratings, item.UserName, item.Review));
                }
                if (searchResult.TotalCount > PageSize)
                {
                    StringBuilder sbPage = new StringBuilder();
                    sbPage.Append("<nav aria-label='...'><ul class='pagination justify-content-center'>");
                    if (PageNo == 1)
                    {
                        sbPage.Append("<li class='page-item disabled'> <span class='page-link'>Previous</span> </li>");
                    }
                    else
                    {
                        sbPage.Append(string.Format("<li class='page-item'> <span class='page-link' onclick='BindReviews({0});return false;'>Previous</span> </li>", (PageNo - 1)));
                    }
                    int totalPages = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(searchResult.TotalCount) / PageSize));
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
                    for (int i = Pager; i <= totalPages; i++)
                    {
                        if (i == PageNo)
                        {
                            sbPage.Append(string.Format("<li class='page-item active'> <span class='page-link'> {0} <span class='sr-only'>(current)</span> </span></li>", i));
                        }
                        else
                        {
                            sbPage.Append(string.Format("<li class='page-item'> <span class='page-link' onclick='BindReviews({0});return false;'> {0} </span> </li>", i));
                        }
                    }
                    if (PageNo < totalPages)
                    {
                        sbPage.Append(string.Format("<li class='page-item'> <span class='page-link' onclick='BindReviews({0});return false;'>Next</span> </li> </ul> </nav>", (PageNo + 1)));
                    }
                    else
                    {
                        sbPage.Append("<li class='page-item disabled'> <span class='page-link'>Next</span> </li> </ul> </nav>");
                    }
                    sb.Append(sbPage.ToString());
                }
                result = sb.ToString();
            }
        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("ProductDetails.aspx BindReviews Exception: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
        }
        return result;
    }
    [WebMethod]
    public static bool AddProductReviews(string ProductId, int Ratings, string Title, string Review)
    {
        bool result = true;
        try
        {
            MemberDetails member = HttpContext.Current.Session["MemberDetails"] as MemberDetails;
            string UserName = member.LastName;
            ABCModel model = new ABCModel();
            ShopModel lobjmodel = new ShopModel();
            result = lobjmodel.AddCustomerReview(ProductId, Ratings, Title, Review, UserName);
        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("ProductDetails.aspx AddProductReviews Exception: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
        }
        return result;
    }
    [WebMethod]
    public static List<string> BindProductByVariation(string pstrProductId, string pstrCOlor, string pstrSize, string pstrStorage, string pstrDenomination)
    {
        List<string> llstvalues = new List<string>();
        List<Product> lobjDenominationProduct = new List<Product>();
        string lstrAmount;
        try
        {
            ABCModel lobjModel = new ABCModel();
            Product lobjProduct = new Product();
            lobjProduct = (Product)HttpContext.Current.Session["Product"];
            List<string> lobjColorId = new List<string>();
            List<string> lobjSize = new List<string>();
            List<string> lobjStorage = new List<string>();
            List<string> lobjDenomination = new List<string>();
            ICollection<Product> lobjVariationsList = new List<Product>();
            lobjVariationsList = lobjProduct.Variations;
            List<Product> lobjResponseProduct = new List<Product>();
            List<Product> lobjColorProduct = new List<Product>();
            List<Product> lobjSizeProduct = new List<Product>();
            List<Product> lobjStorageProduct = new List<Product>();
            if (pstrCOlor != "")
            {
                lobjColorProduct = lobjVariationsList.ToList().FindAll(lobj => lobj.VariationProperties.Find(lobjProp => lobjProp.Name.Equals("Color")).Value == pstrCOlor);
            }
            else
            {
                lobjColorProduct = lobjVariationsList.ToList();
            }
            if (pstrSize != "")
            {
                lobjSizeProduct = lobjColorProduct.ToList().FindAll(lobj => lobj.VariationProperties.Find(lobjProp => lobjProp.Name.Equals("Size")).Value == pstrSize);
            }
            else
            {
                lobjSizeProduct = lobjColorProduct;
            }
            if (pstrStorage != "")
            {
                lobjStorageProduct = lobjSizeProduct.ToList().FindAll(lobj => lobj.VariationProperties.Find(lobjProp => lobjProp.Name.Equals("Storage")).Value == pstrStorage);
            }
            else
            {
                lobjStorageProduct = lobjSizeProduct;
            }
            if (pstrDenomination != "")
            {
                lobjDenominationProduct = lobjStorageProduct.ToList().FindAll(lobj => lobj.VariationProperties.Find(lobjProp => lobjProp.Name.Equals("Value")).Value == pstrDenomination);
            }
            else
            {
                lobjDenominationProduct = lobjStorageProduct;
            }
            llstvalues.Add(JsonConvert.SerializeObject(lobjDenominationProduct));
            lstrAmount = (Math.Ceiling(lobjDenominationProduct[0].Prices.FirstOrDefault(x => x.Currency.Code == ConfigurationManager.AppSettings["ShopCurrency"]).SalePriceWithTax.Amount)).ToString();
            //llstvalues.Add((Math.Ceiling(lobjDenominationProduct[0].Prices.FirstOrDefault(x => x.Currency.Code == ConfigurationManager.AppSettings["ShopCurrency"]).SalePriceWithTax.Amount)).ToString());
            llstvalues.Add(lstrAmount);
            llstvalues.Add(ConfigurationManager.AppSettings["ShopCurrency"]);
        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("ProductDetails.aspx BindProductByVariation Exception: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
        }
        return llstvalues;
    }
    [WebMethod]
    public static List<string> GetPointsonQuantityChange(int lintQty)
    {
        List<string> result = new List<string>();
        try
        {
            ABCModel model = new ABCModel();
            Product lobjProduct = null;
            lobjProduct = (Product)HttpContext.Current.Session["Product"];

            if (lobjProduct != null)
            {
                result.Add(Convert.ToString(Math.Ceiling(lobjProduct.Price.SalePrice.Amount) * lintQty));
            }
            result.Add((lobjProduct.Prices.FirstOrDefault(x => x.Currency.Code == ConfigurationManager.AppSettings["ShopCurrency"]).SalePriceWithTax.Amount * lintQty).ToString());
        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("ProductDetails.aspx GetPointsonQuantityChange Exception: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
        }
        return result;
    }

    [WebMethod]
    public static bool CheckAvailability(int pntamount, int pntQuantity)
    {
        ABCModel lobjmodel = new ABCModel();
        ShopModel lmodel = new ShopModel();
        string lstrCurrency = lobjmodel.GetDefaultCurrency();
        MemberDetails lobjMemberDetails = HttpContext.Current.Session["MemberDetails"] as MemberDetails;
        MemberRelation lobjMemberRelation = lobjMemberDetails.MemberRelationsList.Find(lobj => lobj.RelationType.Equals(RelationType.LBMS));
        ProgramDefinition lobjProgramDefinition = lobjmodel.GetProgramMaster();
        int MemberMiles = lobjmodel.CheckAvailbility(lobjMemberRelation.RelationReference, Convert.ToInt32(lobjMemberRelation.RelationType), lstrCurrency, lobjProgramDefinition.ProgramId);
        if ((pntamount * pntQuantity) > MemberMiles)
        {
            return false;
        }
        return true;
    }


    public static bool IsValidProduct(Giift.ShopGateway.Client.Entities.Product pobjProduct)
    {
        bool lblIsproductvalid = false;

        if (pobjProduct.IsBuyable && pobjProduct.Price.SalePrice.TruncatedAmount > 0)
        {
            lblIsproductvalid = true;
        }
        if (!lblIsproductvalid)
        {
            LoggingAdapter.WriteLog("Product Value is 0 , Product Id :-" + pobjProduct.Id);
        }
        return lblIsproductvalid;
    }
    [WebMethod]
    public static string ProductValiditydetails(string pstrProductId)
    {
        bool lblIsproductvalid = false;
        string productdetails = string.Empty;
        Product lobjProduct = new Product();
        ShopModel model = new ShopModel();
        lobjProduct = model.GetProductById(pstrProductId);
        if (lobjProduct.IsBuyable && lobjProduct.Price.SalePrice.TruncatedAmount > 0)
        {
            lblIsproductvalid = true;
            productdetails = JsonConvert.SerializeObject(lobjProduct);
        }
        if (!lblIsproductvalid)
        {
            LoggingAdapter.WriteLog("Product Value is 0 , Product Id :-" + lobjProduct.Id);
        }
        return productdetails;
    }
}
public static class SystemExtension
{
    public static t Clone<t>(this t original)
    {
        var serialized = JsonConvert.SerializeObject(original);
        return JsonConvert.DeserializeObject<t>(serialized);
    }
}