using Framework.EnterpriseLibrary.Adapters;
using Giift.ShopGateway.Client.Entities;
using GiiftShopGateway.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ABC.Model;
using Zen.Barcode;
using GiftCardDetails = Giift.ShopGateway.Client.Entities.GiftCardDetails;
//using GiiftOfferDetails = Giift.ShopGateway.Client.Entities.Offerdetails;
//using GiiftboxOffers = GiiftShopGateway.Model.Root;
public partial class OrderDetails : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string lstrOrderNumber = string.Empty;
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["OrderNumber"] != null)
                {
                    lstrOrderNumber = Convert.ToString(Request.QueryString["OrderNumber"]);
                    if (HttpContext.Current.Session["MemberDetails"] != null)
                    {
                        ShopModel lobjModel = new ShopModel();
                        ABCModel lobjVerveModel = new ABCModel();
                        CustomerOrder lobjCustomerOrder = null;
                        string lstrHtmlContent = string.Empty;
                        lobjCustomerOrder = lobjModel.GetOrderByNumber(lstrOrderNumber);
                        if (lobjCustomerOrder != null && lobjCustomerOrder.Items.Count > 0)
                        {
                            //Digital Product
                            if (lobjCustomerOrder.Addresses != null & lobjCustomerOrder.Addresses.Count == 0)
                            {
                                divOrderCard.Visible = false;
                                if (lobjCustomerOrder.Shipments[0].Status == "ReadyToSend")
                                {
                                    divOrderConfirmed.Attributes.Add("class", "step active");
                                    divOrderPicked.Visible = false;
                                    divOtw.Visible = false;
                                }
                                if (lobjCustomerOrder.Shipments[0].Status == "Delivered")
                                {
                                    divOrderConfirmed.Attributes.Add("class", "step active");
                                    divOrderPicked.Visible = false;
                                    divOtw.Visible = false;
                                    divDelivered.Attributes.Add("class", "step active");
                                }
                                Product lobjProduct = new Product();
                                lobjProduct = lobjModel.GetProductById(lobjCustomerOrder.Items[0].ProductId);
                                if (lobjProduct != null)
                                {
                                    spanOrderId.InnerText = lobjCustomerOrder.Number;
                                    lstrHtmlContent += "<div class=\"bg-white p-3\">";
                                    for (int i = 0; i < lobjCustomerOrder.Items.Count; i++)
                                    {
                                        lstrHtmlContent += "<div class=\"row pb-2 border-bottom align-items-sm-center justify-content-between\"><div class=\"col-3 col-sm-2 col-lg-1\"><div class=\"img-container\"><img src=\"" + lobjCustomerOrder.Items[i].ImageUrl + "\"/></div></div>"
                                            + "<div class=\"col-12 col-sm-5 col-lg-6\"><p><span>Product</span> <span class=\"h6 heading-semibold\">" + lobjCustomerOrder.Items[i].Name + "</span></p></div>"
                                            + "<div class=\"col-12 col-sm-2 text-sm-right\"><p>Qty <span class=\"h6 heading-semibold\">" + lobjCustomerOrder.Items[i].Quantity + "</span></p></div>"
                                            + "<div class=\"col-12 col-sm-3 text-sm-right\"><p class=\"heading-semibold\">" + lobjVerveModel.FormatPoints(Math.Ceiling(lobjCustomerOrder.Items[i].Price.ListPrice.Amount), "Points") + "</p></div></div>";
                                    }
                                    lstrHtmlContent += "<div class=\"row align-items-lg-center justify-content-between\"><div class=\"col-12 mt-2\"><div class=\"row my-1\"><div class=\"col-6 col-md-3 offset-md-6 text-md-right\"><p class=\"\">Sub-Total</p></div>"
                                     + "<div class=\"col-6 col-md-3 text-right\"><p class=\"\">" + lobjVerveModel.FormatPoints(Math.Ceiling(lobjCustomerOrder.Price.SubTotal.Amount), "Points") + "</p></div></div></div>"
                                     + "<div class=\"col-12\"><div class=\"row my-1\"><div class=\"col-6 col-md-3 offset-md-6 text-md-right\"><p class=\"\">Shipping</p></div>"
                                     + "<div class=\"col-6 col-md-3 text-right\"><p class=\"\">" + lobjVerveModel.FormatPoints(Math.Ceiling(lobjCustomerOrder.Price.ShippingPrice.Amount), "Points") + "</p></div></div></div>"
                                     + "<div class=\"col-12\"><div class=\"row my-1\"><div class=\"col-6 col-md-3 offset-md-6 text-md-right\"><p class=\"heading-semibold\">Total</p></div>"
                                     + "<div class=\"col-6 col-md-3 text-right\"><p class=\"heading-semibold\">" + lobjVerveModel.FormatPoints(Math.Ceiling(lobjCustomerOrder.Price.Total.Amount), "Points") + "</p></div></div></div></div>"
                                     + "<div class=\"row align-items-lg-center justify-content-end\"><div class=\"col-12 mt-2 mt-lg-3 mt-lg-0 col-lg-auto text-left text-sm-right\"><button type=\"button\" class=\"btn btn-one\" data-toggle=\"modal\" data-target=\"#dvOrderDetailsModal\" onclick=\"ViewDetails();\">View Details</button></div></div>";
                                    lstrHtmlContent += "</div>";
                                    divOrderDetails.InnerHtml = lstrHtmlContent;
                                    string lstrDigitalProductType = lobjProduct.Properties.ToList().Find(lobj => lobj.Name.Equals("Type")).Value;
                                    if (lstrDigitalProductType.ToLower().Equals("giftcard"))
                                    {
                                        GiftCardDetails giftCardDetails = null;
                                        try
                                        {
                                            giftCardDetails = JsonConvert.DeserializeObject<GiftCardDetails>(lobjCustomerOrder.Shipments[0].DynamicProperties.Find(lobj => lobj.Name.Equals("ResponseMetas")).Values[0].Value);
                                            if (giftCardDetails != null)
                                            {
                                                List<string> lstrEmailParameters = new List<string>();
                                                lstrEmailParameters.Add(lobjProduct.PrimaryImage.Url);
                                                string lstrHtml = string.Empty;
                                                
                                                for (int i = 0; i < giftCardDetails.GiftCardInfo.Count; i++)
                                                {
                                                    string ImageUrl = string.Empty;
                                                    if (!string.IsNullOrEmpty(giftCardDetails.GiftCardInfo[i].Code))
                                                    {
                                                        int maxheight = 40;
                                                        Code128BarcodeDraw barcode128 = BarcodeDrawFactory.Code128WithChecksum;
                                                        System.Drawing.Image img = barcode128.Draw(giftCardDetails.GiftCardInfo[i].Code, maxheight);
                                                        Bitmap bm = new Bitmap(img);
                                                        string filePath = HttpContext.Current.Server.MapPath("~/Barcodes/") + giftCardDetails.GiftCardInfo[i].ExternalReference + ".png";
                                                        if (!File.Exists(filePath))
                                                        {
                                                            try
                                                            {
                                                                bm.Save(filePath, System.Drawing.Imaging.ImageFormat.Png);
                                                            }
                                                            catch (Exception ex)
                                                            {
                                                                LoggingAdapter.WriteLog("Error GiftCards Barcode" + ex.Message + Environment.NewLine + ex.StackTrace);
                                                            }
                                                        }
                                                        ImageUrl = Convert.ToString(ConfigurationManager.AppSettings["GCBarcodeUrl"]) + giftCardDetails.GiftCardInfo[i].ExternalReference + ".png";
                                                    }
                                                    lstrHtml += "<div class=\"row align-items-sm-center justify-content-between\">";
                                                    lstrHtml += "<div class=\"col-12 col-sm-6 col-lg-auto\"><p class=\"\">GiftCard No: <span class=\"heading-semibold text-break\">" + giftCardDetails.GiftCardInfo[i].Code + "</span></p>";
                                                    if (!string.IsNullOrEmpty(giftCardDetails.GiftCardInfo[i].RedirectionUrl))
                                                    {
                                                        lstrHtml += "for detail please click : <a href=\"" + giftCardDetails.GiftCardInfo[i].RedirectionUrl + "\" target=\"_blank\" style=\"font-family: Helvetica, Arial, sans-serif;font-size: 13px; color: #000000;display: inline-block;margin-left\">" + giftCardDetails.GiftCardInfo[i].RedirectionUrl + "</a>";
                                                    }
                                                    lstrHtml += "</div>";
                                                    lstrHtml += "<div class=\"col-12 col-sm-6 text-sm-right col-lg-auto mb-2\"><p class=\"\">GiftCard Value: <span class=\"heading-semibold\">" + giftCardDetails.GiftCardInfo[i].Value + "</span></p></div>";
                                                    if (giftCardDetails.GiftCardInfo[i].ExpiryDate != null)
                                                    {
                                                        try
                                                        {
                                                            lstrHtml += "<div class=\"col-12 col-sm-6 col-lg-auto col-sm-auto mb-2\"><p class=\"\">Date: <span class=\"heading-semibold\">" + Convert.ToDateTime(giftCardDetails.GiftCardInfo[i].ExpiryDate).ToString("dd/MM/yyyy") + "</span></p></div>";
                                                        }
                                                        catch (Exception ex)
                                                        {
                                                            lstrHtml += "<div class=\"col-12 col-sm-6 col-lg-auto col-sm-auto mb-2\"><p class=\"\">Date: <span class=\"heading-semibold\">" + giftCardDetails.GiftCardInfo[i].ExpiryDate + "</span></p></div>";
                                                            LoggingAdapter.WriteLog("Error GiftCards Expiry Date - " + ex.Message + Environment.NewLine + ex.StackTrace);
                                                        }
                                                    }
                                                    if (!string.IsNullOrEmpty(giftCardDetails.GiftCardInfo[i].Pin))
                                                    {
                                                        lstrHtml += "<<div class=\"col-12 col-sm-6 col-lg-auto col-sm-auto\"><p class=\"\">CGiftCard Info: <span class=\"heading-semibold\">" + Convert.ToString(giftCardDetails.GiftCardInfo[i].Pin) + "</span></p></div>";
                                                    }
                                                    lstrHtml += "<div class=\"col-12 col-sm-6 text-sm-right mt-2 mt-md-3 mt-lg-0 col-lg-auto\"><p class=\"\">CGiftCard Info: <img style=\"height: 40px\" src=\"" + ImageUrl + "\"/></p></div>";
                                                    lstrHtml += "</div>";
                                                }
                                                LoggingAdapter.WriteLog("View Details html -:" + lstrHtml);
                                                hdfViewdetailsInfo.Value = lstrHtml;
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            LoggingAdapter.WriteLog("OrderDetails.aspx|giftcard|exception: " + ex.Message + Environment.NewLine + ex.StackTrace);
                                        }
                                    }
                                    else if (lstrDigitalProductType.ToLower().Equals("topup"))
                                    {
                                        TopUpDetails topUpDetails = null;
                                        try
                                        {
                                            topUpDetails = JsonConvert.DeserializeObject<TopUpDetails>(lobjCustomerOrder.Shipments[0].DynamicProperties.Find(lobj => lobj.Name.Equals("ResponseMetas")).Values[0].Value);
                                            if (topUpDetails != null)
                                            {
                                                List<string> lstrEmailParameters = new List<string>();
                                                lstrEmailParameters.Add(lobjProduct.PrimaryImage.Url);
                                                string lstrHtml = string.Empty;
                                                lstrHtml += "<tr>";
                                                if (topUpDetails.ExternalReference != null)
                                                {
                                                    lstrHtml += "<td width=\"15%\" height=\"35\" bgcolor=\"#006677\"><p style=\"color:#fff;\"><strong>TopUp Purchased</strong></p></td>";
                                                }
                                                if (topUpDetails.ExternalReference != null)
                                                {
                                                    lstrHtml += "<td width=\"15%\" height=\"25\" bgcolor=\"#FFFFFF\"><p>" + topUpDetails.ExternalReference + "</p></td>";
                                                }
                                                lstrHtml += "</tr>";
                                                hdfViewdetailsInfo.Value = lstrHtml;
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            LoggingAdapter.WriteLog("OrderDetails.aspx|giftcard|exception: " + ex.Message + Environment.NewLine + ex.StackTrace);
                                        }
                                    }
                                    else if (lstrDigitalProductType.ToLower().Equals("lounge"))
                                    {
                                        LoungeDetails giftCardDetails = null;
                                        try
                                        {
                                            giftCardDetails = JsonConvert.DeserializeObject<LoungeDetails>(lobjCustomerOrder.Shipments[0].DynamicProperties.Find(lobj => lobj.Name.Equals("ResponseMetas")).Values[0].Value);

                                            if (giftCardDetails != null)
                                            {
                                                List<string> lstrEmailParameters = new List<string>();
                                                lstrEmailParameters.Add(lobjProduct.PrimaryImage.Url);


                                                string lstrHtml = string.Empty;
                                                lstrHtml += "<tr>";

                                                if (giftCardDetails.ExternalReference != null)
                                                {
                                                    lstrHtml += "<td width=\"15%\" height=\"35\" bgcolor=\"#006677\"><p style=\"color:#fff;\"><strong style=\"color:#fff;\">Lounge Code</strong></p></td>";
                                                }
                                                if (giftCardDetails.LoungeInfo.ExpiryDate != null)
                                                {
                                                    lstrHtml += "<td width=\"15%\" height=\"35\" bgcolor=\"#006677\"><p style=\"color:#fff;\"><strong style=\"color:#fff;\">Expiry Date</strong></p></td>";
                                                }


                                                lstrHtml += "</tr>";
                                                lstrHtml += "<tr>";
                                                if (giftCardDetails.ExternalReference != null)
                                                {
                                                    lstrHtml += "<td width=\"15%\" height=\"25\" bgcolor=\"#FFFFFF\">" + giftCardDetails.ExternalReference + "</td>";
                                                }
                                                if (giftCardDetails.LoungeInfo.ExpiryDate != null)
                                                {
                                                    lstrHtml += "<td width=\"15%\" height=\"25\" bgcolor=\"#FFFFFF\">" + giftCardDetails.LoungeInfo.ExpiryDate.ToString("dd-MM-yyyy") + "</td>";
                                                }
                                                lstrHtml += "</tr>";


                                                LoggingAdapter.WriteLog("View Details html -:" + lstrHtml);

                                                hdfViewdetailsInfo.Value = lstrHtml;
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            LoggingAdapter.WriteLog("OrderDetails.aspx|giftcard|exception: " + ex.Message + Environment.NewLine + ex.StackTrace);
                                        }
                                    }
                                    else if (lstrDigitalProductType.ToLower().Equals("miles exchange"))
                                    {
                                        MilesExchangeDetails giftCardDetails = null;
                                        try
                                        {
                                            giftCardDetails = JsonConvert.DeserializeObject<MilesExchangeDetails>(lobjCustomerOrder.Shipments[0].DynamicProperties.Find(lobj => lobj.Name.Equals("ResponseMetas")).Values[0].Value);

                                            if (giftCardDetails != null)
                                            {
                                                List<string> lstrEmailParameters = new List<string>();
                                                lstrEmailParameters.Add(lobjProduct.PrimaryImage.Url);


                                                string lstrHtml = string.Empty;
                                                lstrHtml += "<tr>";
                                                //lstrHtml += "<td width=\"40%\" height=\"35\" bgcolor=\"#006677\"><p style=\"color:#fff;\"><strong style=\"color:#fff;>GiftCard No/GiftCard Link</strong></p></td>";
                                                //lstrHtml += "<td width=\"15%\" height=\"35\" bgcolor=\"#006677\"><p style=\"color:#fff;\"><strong style=\"color:#fff;>GiftCard Value</strong></p></td>";
                                                if (giftCardDetails.ExternalReference != null)
                                                {
                                                    lstrHtml += "<td width=\"15%\" height=\"35\" bgcolor=\"#006677\"><p style=\"color:#fff;\"><strong style=\"color:#fff;\">Receipt No</strong></p></td>";
                                                }
                                                if (giftCardDetails.TimeStamp != null)
                                                {
                                                    lstrHtml += "<td width=\"15%\" height=\"35\" bgcolor=\"#006677\"><p style=\"color:#fff;\"><strong style=\"color:#fff;\">Processed Date</strong></p></td>";
                                                }
                                                // lstrHtml += "<td width=\"15%\" height=\"35\" bgcolor=\"#006677\"><p style=\"color:#fff;\"><strong style=\"color:#fff;\">Barcode</strong></p></td>";
                                                //if (!string.IsNullOrEmpty(giftCardDetails.GiftCardInfo[0].RedirectionUrl))
                                                //{
                                                //    lstrHtml += "<td width=\"15%\" height=\"35\" bgcolor=\"#006677\"><p style=\"color:#fff;\"><strong style=\"color:#fff;>RedirectionUrl</strong></p></td>";
                                                //}

                                                lstrHtml += "</tr>";
                                                lstrHtml += "<tr>";
                                                if (giftCardDetails.ExternalReference != null)
                                                {
                                                    lstrHtml += "<td width=\"15%\" height=\"25\" bgcolor=\"#FFFFFF\">" + giftCardDetails.ExternalReference + "</td>";
                                                }
                                                if (giftCardDetails.TimeStamp != null)
                                                {
                                                    lstrHtml += "<td width=\"15%\" height=\"25\" bgcolor=\"#FFFFFF\">" + giftCardDetails.TimeStamp.ToString("dd-MM-yyyy") + "</td>";
                                                }
                                                lstrHtml += "</tr>";

                                                //  for (int i = 0; i < giftCardDetails.ExternalReference.Count; i++)
                                                //{
                                                //    string ImageUrl = string.Empty;

                                                //    if (!string.IsNullOrEmpty(giftCardDetails.GiftCardInfo[i].Code))
                                                //    {
                                                //        int maxheight = 40;
                                                //        Code128BarcodeDraw barcode128 = BarcodeDrawFactory.Code128WithChecksum;
                                                //        System.Drawing.Image img = barcode128.Draw(giftCardDetails.GiftCardInfo[i].Code, maxheight);
                                                //        Bitmap bm = new Bitmap(img);
                                                //        string filePath = HttpContext.Current.Server.MapPath("~/Barcodes/") + giftCardDetails.GiftCardInfo[i].ExternalReference + ".png";
                                                //        if (!File.Exists(filePath))
                                                //        {
                                                //            try
                                                //            {
                                                //                bm.Save(filePath, System.Drawing.Imaging.ImageFormat.Png);
                                                //            }
                                                //            catch (Exception ex)
                                                //            {
                                                //                LoggingAdapter.WriteLog("Error GiftCards Barcode" + ex.Message + Environment.NewLine + ex.StackTrace);
                                                //            }
                                                //        }
                                                //        ImageUrl = Convert.ToString(ConfigurationManager.AppSettings["GCBarcodeUrl"]) + giftCardDetails.GiftCardInfo[i].ExternalReference + ".png";
                                                //    }
                                                //    lstrHtml += "<tr>";
                                                //    lstrHtml += "<td width=\"40%\" height=\"25\" bgcolor=\"#FFFFFF\"><p><strong>" + giftCardDetails.GiftCardInfo[i].Code + "</strong></p>";

                                                //    if (!string.IsNullOrEmpty(giftCardDetails.GiftCardInfo[i].RedirectionUrl))
                                                //    {
                                                //        lstrHtml += "for detail please click : <a href=\"" + giftCardDetails.GiftCardInfo[i].RedirectionUrl + "\" target=\"_blank\" style=\"font-family: Helvetica,'Nunito', sans-serif, sans-serif;font-size: 13px; color: #000000;display: inline-block;margin-left\">" + giftCardDetails.GiftCardInfo[i].RedirectionUrl + "</a>";
                                                //    }

                                                //    lstrHtml += "</td>";

                                                //    lstrHtml += "<td width=\"15%\" height=\"25\" bgcolor=\"#FFFFFF\"><p>" + giftCardDetails.GiftCardInfo[i].Value + "</p></td>";
                                                //    if (giftCardDetails.GiftCardInfo[i].ExpiryDate != null)
                                                //    {
                                                //        try
                                                //        {
                                                //            lstrHtml += "<td width=\"15%\" height=\"25\" bgcolor=\"#FFFFFF\">" + giftCardDetails.GiftCardInfo[i].ExpiryDate.Value.ToString("dd-MM-yyyy") + "</td>";
                                                //        }
                                                //        catch (Exception ex)
                                                //        {
                                                //            lstrHtml += "<td width=\"15%\" height=\"25\" bgcolor=\"#FFFFFF\">" + giftCardDetails.GiftCardInfo[i].ExpiryDate + "</td>";
                                                //            LoggingAdapter.WriteLog("Error GiftCards Expiry Date - " + ex.Message + Environment.NewLine + ex.StackTrace);
                                                //        }
                                                //    }
                                                //    if (!string.IsNullOrEmpty(giftCardDetails.GiftCardInfo[i].Pin))
                                                //    {
                                                //        lstrHtml += "<td width=\"15%\" height=\"25\" bgcolor=\"#FFFFFF\">" + Convert.ToString(giftCardDetails.GiftCardInfo[i].Pin) + "</td>";
                                                //    }
                                                //    lstrHtml += "<td width=\"15%\" height=\"25\" bgcolor=\"#FFFFFF\"><img src=\"" + ImageUrl + "\"/></td>";

                                                //    lstrHtml += "</tr>";
                                                //}
                                                LoggingAdapter.WriteLog("View Details html -:" + lstrHtml);

                                                hdfViewdetailsInfo.Value = lstrHtml;
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            LoggingAdapter.WriteLog("OrderDetails.aspx|giftcard|exception: " + ex.Message + Environment.NewLine + ex.StackTrace);
                                        }
                                    }
                                }
                            }
                            //Physical Product
                            else
                            {
                                divOrderCard.Visible = true;
                                divOrderPicked.Visible = true;
                                divOtw.Visible = true;
                                spanOrderId.InnerText = lobjCustomerOrder.Number;
                                spanOrderStatus.InnerText = lobjCustomerOrder.Status;
                                spanOrderNotes.InnerText = lobjCustomerOrder.Comment;
                                //Populate shipment details
                                DateTime? ldtShippingDateTime = null;
                                for (int i = 0; i < lobjCustomerOrder.Shipments[0].DynamicProperties.Count; i++)
                                {
                                    if (lobjCustomerOrder.Shipments[0].DynamicProperties[i].Name == "DeliveryDate")
                                    {
                                        ldtShippingDateTime = Convert.ToDateTime(lobjCustomerOrder.Shipments[0].DynamicProperties[i].Values[0].Value);
                                    }
                                    if (lobjCustomerOrder.Shipments[0].DynamicProperties[i].Name == "TrackingDetails")
                                    {
                                        spanTrackingNo.InnerText = Convert.ToString(lobjCustomerOrder.Shipments[0].DynamicProperties[i].Values[0].Value);
                                    }
                                }
                                if (ldtShippingDateTime == null)
                                    spanDeliveryTimeEst.InnerText = "-";
                                else
                                    spanDeliveryTimeEst.InnerText = ldtShippingDateTime.ToString();
                                spanShippingBy.InnerText = lobjCustomerOrder.Shipments[0].EmployeeName;
                                lstrHtmlContent += "<table class=\"table table-striped\"><thead><tr><th scope=\"col\"></th><th scope =\"col\">Product</th><th scope=\"col\">Quantity</th><th class=\"text-right\" scope=\"col\">Price</th></tr></thead><tbody>";
                                for (int i = 0; i < lobjCustomerOrder.Items.Count; i++)
                                {
                                    lstrHtmlContent += "<tr><td><img style=\"height:100px;\" src=\"" + lobjCustomerOrder.Items[i].ImageUrl + "\"/></td>"
                                        + "<td>" + lobjCustomerOrder.Items[i].Name + "</td>"
                                        + "<td>" + lobjCustomerOrder.Items[i].Quantity + "</td>"
                                        + "<td class=\"text-right\">" + lobjVerveModel.FormatPoints(Math.Ceiling(lobjCustomerOrder.Items[i].Price.ListPrice.Amount), "Points") + "</td></tr>";
                                }
                                lstrHtmlContent += "<tr><td></td><td></td><td>Sub-Total</td><td class=\"text-right\">" + lobjVerveModel.FormatPoints(Math.Ceiling(lobjCustomerOrder.Price.SubTotal.Amount), "Points") + "</td></tr>"
                                 + "<tr><td></td><td></td><td>Shipping</td><td class=\"text-right\">" + lobjVerveModel.FormatPoints(Math.Ceiling(lobjCustomerOrder.Price.ShippingPrice.Amount), "Points") + "</td></tr>"
                                 + "<tr><td></td><td></td><td><strong>Total</strong></td><td class=\"text-right\"><strong>" + lobjVerveModel.FormatPoints(Math.Ceiling(lobjCustomerOrder.Price.Total.Amount), "Points") + "</strong></td></tr>";
                                lstrHtmlContent += "</tbody></table>";
                                divOrderDetails.InnerHtml = lstrHtmlContent;
                                if (lobjCustomerOrder.Shipments[0].Status == "ReadyToSend")
                                    divOrderConfirmed.Attributes.Add("class", "step active");
                                if (lobjCustomerOrder.Shipments[0].Status == "PackagePickedUp")
                                {
                                    divOrderConfirmed.Attributes.Add("class", "step active");
                                    divOrderPicked.Attributes.Add("class", "step active");
                                }
                                if (lobjCustomerOrder.Shipments[0].Status == "Dispatched")
                                {
                                    divOrderConfirmed.Attributes.Add("class", "step active");
                                    divOrderPicked.Attributes.Add("class", "step active");
                                    divOtw.Attributes.Add("class", "step active");
                                }
                                if (lobjCustomerOrder.Shipments[0].Status == "Delivered")
                                {
                                    divOrderConfirmed.Attributes.Add("class", "step active");
                                    divOrderPicked.Attributes.Add("class", "step active");
                                    divOtw.Attributes.Add("class", "step active");
                                    divDelivered.Attributes.Add("class", "step active");
                                }
                            }
                        }
                    }
                    else
                    {
                        string CallbackUrl = HttpUtility.UrlEncode(Encrypt("OrderDetails.aspx?OrderNumber=" + lstrOrderNumber));
                        HttpContext.Current.Session["CallbackUrl"] = CallbackUrl;
                        Response.Redirect("Login.aspx?CallbackUrl=" + CallbackUrl, false);
                    }
                }
                else
                {
                    Response.Redirect("Shop.aspx", false);
                }
            }
        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("OrderDetails.aspx Page_Load Exception: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
        }
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
            LoggingAdapter.WriteLog("OrderDetails.aspx Encrypt Exception: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
        }
        return clearText;
    }
}