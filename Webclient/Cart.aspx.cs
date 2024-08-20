using Core.Platform.Member.Entites;
using Giift.ShopGateway.Client.Entities;
using GiiftShopGateway.Model;
using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using Core.Platform.ProgramMaster.Entities;
using Framework.EnterpriseLibrary.Adapters;
using ABC.Model;
using CB.IBE.Platform.AirClientModel;
using Core.Platform.MemberActivity.Entities;
using Core.Platform.MemberActivity.Constants;

public partial class Cart : Page
{
    public static ShoppingCart lobjShoppingCart = null;
    public string pstrCategoryID = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                MemberDetails lobjMemberDetails = null;
                lobjMemberDetails = HttpContext.Current.Session["MemberDetails"] as MemberDetails;
                if (Session["MemberDetails"] != null)
                {
                    divCartContents.InnerHtml = "";
                    divCartContents.InnerHtml = GetCartContents();
                    lobjShoppingCart = HttpContext.Current.Session["ShoppingCart"] as ShoppingCart;
                    if (string.IsNullOrEmpty(lobjMemberDetails.Email))
                    {
                        divEmailErrorMsg.Visible = true;
                        btnCheckout.Attributes.Remove("class");
                        btnCheckout.Attributes.Add("class", "blue_button btn-lg");
                        btnCheckout.Attributes.Add("style", "display:none");
                    }
                    else
                    {
                        divEmailErrorMsg.Visible = false;
                        if (lobjShoppingCart != null && lobjShoppingCart.Items.Count > 0 && lobjShoppingCart.IsValid)
                        {
                            btnCheckout.Attributes.Remove("class");
                            btnCheckout.Attributes.Add("class", "btn btn-two w-100");

                            bool available = Cart.CheckAvailability();
                            if (available)
                            {
                                btnCheckout.Attributes.Remove("style");
                                dvErrorMsg.Visible = false;
                            }
                            else
                            {
                                btnCheckout.Attributes.Add("style", "display:none");
                                dvErrorMsg.Visible = true;
                            }
                        }
                        else
                        {
                            btnCheckout.Attributes.Remove("class");
                            btnCheckout.Attributes.Add("class", "blue_button btn-lg");
                            btnCheckout.Attributes.Add("style", "display:none");
                        }
                    }
                }
                else
                {
                    string CallbackUrl = HttpUtility.UrlEncode(Encrypt(Request.RawUrl));
                    HttpContext.Current.Session["CallbackUrl"] = CallbackUrl;
                    Response.Redirect("Login.aspx?CallbackUrl=" + CallbackUrl, false);
                }
                if (Session["CategoryId"] != null)
                {
                    pstrCategoryID = "Shop.aspx?CategoryId=" + Session["CategoryId"].ToString() + "&ProductType=Physical";
                }
                ABCModel lobjmodel = new ABCModel();
                lobjmodel.LogActivity(string.Format("Visited Cart.aspx; MemberId-:{0}", lobjMemberDetails.MemberRelationsList[0].RelationReference), ActivityType.PageLoad);
            }
        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("Cart.aspx Page_Load Exception: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
        }
    }
    public string GetCartContents()
    {
        string lstrHtmlContent = string.Empty;
        try
        {
            ABCModel lobjModel = new ABCModel();
            ShopModel model = new ShopModel();
            MemberDetails memberDetails = Session["MemberDetails"] as MemberDetails;
            lobjShoppingCart = model.LoadOrCreateNewTransientCart(memberDetails);
            Session["ShoppingCart"] = lobjShoppingCart;
            if (lobjShoppingCart.ItemsCount > 0)
            {
                //lstrHtmlContent += "<table class=\"table table-striped\"><thead><tr><th scope = \"col\"></th><th scope=\"col\">Product</th><th scope= \"col\" class=\"text-left\">Quantity</th><th scope = \"col\" class=\"text-left\">Price</th><th></th></tr></thead><tbody>";
                for (int i = 0; i < lobjShoppingCart.ItemsCount; i++)
                {
                    lstrHtmlContent += "<div class=\"row align-items-center justify-content-between\"><div class=\"pr-0 col-3 col-sm-2 col-lg-1\"><div class=\"img-container\"><img class=\"\" src=\"" + lobjShoppingCart.Items[i].ImageUrl + "\"/></div></div>"
                        + "<div class=\"col-9 col-sm-7 col-lg-6 col-xl-6\"> <p><span>Product:</span> " + "<span class=\"h6 heading-bold\">" + lobjShoppingCart.Items[i].Name + "</span></p>" + " </div>"
                        + "<div class=\"col-12 col-sm-3 col-lg-2 my-1\"> <p class=\"d-md-flex align-items-center\"><span class=\"pb-1 d-inline-block mr-1\" data-i18n=\"shopcart-qty\">Qty: </span> " + " <span class=\"heading-bold\"> <input class=\"form-control\"id=\"" + lobjShoppingCart.Items[i].ProductId + "\" type=\"text\" value=\"" + lobjShoppingCart.Items[i].Quantity + "\" onchange=\"var varReturn = UpdateLineItemQty('" + lobjShoppingCart.Items[i].ProductId + "','" + lobjShoppingCart.Items[i].MinQuantity + "','" + lobjShoppingCart.Items[i].MaxQuantity + "','" + lobjShoppingCart.Items[i].ProductType + "',this.value);event.returnValue = varReturn; (event.preventDefault) ? event.preventDefault() : event.returnValue = false; return varReturn;\"/> </span></p></div>"
                        + "<div class=\"col-12 col-lg-3 col-xl-3\">"
                        + "<div class=\"row align-items-center justify-content-sm-end\">"
                        + "<div class=\"col-9 col-sm-auto\"> <p class=\"heading-bold\">" + "<span class=\"heading-regular\">" + lobjModel.FormatPoints(Math.Ceiling(lobjShoppingCart.Items[i].Price.SalePriceWithTax.Amount), "Points") + " </span>" + "</p></div>"
                        + "<div class=\"col-3 col-sm-auto text-right cart\">"
                        + "<p class=\"btn btn-one bg-transparent p-0 border-0\" onclick=\"var varReturn = RemoveLineItem(\'" + lobjShoppingCart.Items[i].ProductId + "\'); event.returnValue = varReturn; (event.preventDefault) ? event.preventDefault() : event.returnValue = false; return varReturn;\"><i class=\"fa fa-trash text-colour1\"></i></p></div></div>"
                        + "</div>"
                        + "</div>"
                        + "<div class=\"row dvBorderBottom my-3\">"
                        + "<div class=\"col-12 border-bottom\">"
                        + "</div>"
                        + "</div>";


                    if (!lobjShoppingCart.Items[i].IsValid)
                    {
                        if (lobjShoppingCart.Items[i].ValidationErrors.Any(lobj => lobj.ErrorCode.ToLower().Equals("unavailableerror")))
                        {
                            lstrHtmlContent += "<div class=\"col-12\"><p class=\"text-colour1\" data-i18n='shopcart-productoutofstock'>Product Out Of Stock</p></div>";
                        }
                        else
                        {
                            for (int j = 0; j < lobjShoppingCart.Items[i].ValidationErrors.Count; j++)
                            {
                                if (lobjShoppingCart.Items[i].ValidationErrors[j].ErrorCode.ToLower().Equals("quantityerror"))
                                {
                                    lstrHtmlContent += "<div class=\"col-12\"><p class=\"text-colour7\" data-i18n='shopcart-productquantity'>Product Quantity Changed</p></div>";
                                }
                                else if (lobjShoppingCart.Items[i].ValidationErrors[j].ErrorCode.ToLower().Equals("priceerror"))
                                {
                                    lstrHtmlContent += "<div class=\"col-12\"><p class=\"text-colour7\" data-i18n='shopcart-productprice'>Product price changed.</p></div>";
                                }
                            }
                        }
                    }
                }
                lstrHtmlContent += "<div class=\"row align-items-lg-center justify-content-between my-1\"><div class=\"col-6 col-md-3 offset-md-6 text-md-right\"><p data-i18n=\"shopcart-subtotal\">Sub-Total</p></div> <div class=\"col-6 col-md-3 text-right\"><p class=\"heading-regular\">" + lobjModel.FormatPoints(Math.Ceiling(lobjShoppingCart.Price.SubTotal.Amount), "Points") + "</p></div></div>"
                         + "<div class=\"row align-items-lg-center justify-content-between my-1\"><div class=\"col-6 col-md-3 offset-md-6 text-md-right\"><p data-i18n=\"shopcart-shipping\">Shipping</p></div> <div class=\"col-6 col-md-3 text-right\"><p class=\"heading-regular\">" + lobjModel.FormatPoints(Math.Ceiling(lobjShoppingCart.Price.ShippingPrice.Amount), "Points") + "</p></div></div>"
                         + "<div class=\"row align-items-lg-center justify-content-between my-1\"><div class=\"col-6 col-md-3 offset-md-6 text-md-right\"><p class=\"heading-bold\" data-i18n=\"shopcart-subtotal\">Total</p></div> <div class=\"col-6 col-md-3 text-right\"><p class=\"heading-bold\">" + lobjModel.FormatPoints(Math.Ceiling(lobjShoppingCart.Price.Total.Amount), "Points") + "</p></div></div>";
               // lstrHtmlContent += "</tbody></table>";
            }
            else
            {
                lstrHtmlContent = "<div class='heading-regular h6 text-colour7' data-i18n=\'shopcart-cartempty\'>Cart Empty</div>";
            }
        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("Cart.aspx GetCartContents Exception: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
        }
        return lstrHtmlContent;
    }
    [WebMethod]
    public static string RemoveLineItem(string lstrProductId)
    {
        string lstrHtml = string.Empty;
        try
        {
            ShopModel model = new ShopModel();
            if (lobjShoppingCart != null && lobjShoppingCart.ItemsCount > 0)
            {
                lobjShoppingCart = model.RemoveItemFromCart(lobjShoppingCart, lstrProductId);
                HttpContext.Current.Session["ShoppingCart"] = lobjShoppingCart;
                Cart lobjCart = new Cart();
                lstrHtml = lobjCart.GetCartContents();
            }
            ABCModel lobjmodel = new ABCModel();
            lobjmodel.LogActivity(string.Format(ActivityConstants.RemoveLineItem, lstrProductId), ActivityType.RemoveItem);
        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("Cart.aspx RemoveLineItem Exception: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
        }
        return lstrHtml;
    }
    [WebMethod]
    public static string UpdateLineItemQty(string lstrProductId, int lintQty)
    {
        string lstrHtml = string.Empty;
        try
        {
            ShopModel model = new ShopModel();
            if (lobjShoppingCart != null && lobjShoppingCart.ItemsCount > 0)
            {
                lobjShoppingCart = model.UpdateItemQuantity(lobjShoppingCart, lstrProductId, lintQty, string.Empty);
                HttpContext.Current.Session["ShoppingCart"] = lobjShoppingCart;
                Cart lobjCart = new Cart();
                lstrHtml = lobjCart.GetCartContents();
            }
            ABCModel lobjmodel = new ABCModel();
            lobjmodel.LogActivity(string.Format(ActivityConstants.UpdateLineItemQty, lstrProductId, lintQty), ActivityType.UpdateLineItemQty);
        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("Cart.aspx UpdateLineItemQty Exception: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
        }
        return lstrHtml;
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
            LoggingAdapter.WriteLog("Cart.aspx Encrypt Exception: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
        }
        return clearText;
    }
    [WebMethod]
    public static bool CheckAvailability()
    {
        bool blnResult = true;
        try
        {
            ShopModel model = new ShopModel();
            ShoppingCart Cart = HttpContext.Current.Session["ShoppingCart"] as ShoppingCart;
            ProgramDefinition lobjProgramMaster = model.GetProgramMaster();
            string lstrCurrency = model.GetDefaultCurrency();
            MemberDetails lobjMemberDetails = HttpContext.Current.Session["MemberDetails"] as MemberDetails;
            int MemberMiles = model.CheckAvailbility(lobjMemberDetails.MemberRelationsList.Find(l => l.RelationType.Equals(RelationType.LBMS)).RelationReference, Convert.ToInt32(RelationType.LBMS), lstrCurrency, lobjProgramMaster.ProgramId);
            if (lobjShoppingCart.Price.Total.Amount > MemberMiles)
            {
                blnResult = false;
            }
        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("Cart.aspx CheckAvailability Exception: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
        }
        return blnResult;
    }
}