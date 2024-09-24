using ABC.Model;
using Core.Platform.Member.Entites;
using Framework.EnterpriseLibrary.Adapters;
using Giift.ShopGateway.Client.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Zen.Barcode;
using System.Drawing;
using System.Web;
using System.Configuration;
using System.IO;
using GiiftShopGateway.Model;
using Core.Platform.Transactions.Entites;
using GiiftPaymentGateway.Entities;
using CB.IBE.Platform.Car.Entities;
using Core.Platform.ProgramMaster.Entities;

public partial class OrderNotification : Page
{
    string orderNumber = string.Empty;
    MemberDetails lobjMemberDetails = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            try
            {
                orderNumber = Convert.ToString(Request.Form["OrderNumber"]);
                LoggingAdapter.WriteLog("OrderNotification PageLoad Response OrderID-:" + orderNumber);
                if (!string.IsNullOrEmpty(orderNumber))
                {
                    ABCModel lobjModel = new ABCModel();
                    ShopModel model = new ShopModel();
                    Store store = null;
                    store = model.GetStoreDetails();
                    if (store != null)
                    {
                        CustomerOrder customerOrder = null;
                        customerOrder = model.GetOrderByNumber(orderNumber);
                        if (customerOrder != null && customerOrder.Shipments.Count > 0)
                        {
                            LoggingAdapter.WriteLog("OrderNotification Customer Order not null");
                            lobjMemberDetails = lobjModel.GetMemberDetails(customerOrder.CustomerId);
                            if (customerOrder.Addresses != null & customerOrder.Addresses.Count > 0)
                            {
                                LoggingAdapter.WriteLog("OrderNotification Order contains physical products");
                                //Order contains physical products
                                string lstrHtmlContent = string.Empty;
                                List<string> lstrEmailParameters = new List<string>();
                                if (lobjMemberDetails != null)
                                {
                                    lstrHtmlContent += "<table border=\"0\" style=\"width:100%;font-size:12px;font-family:arial;padding:2%;\">"
                                                    + "<tbody>"
                                                    + "<tr style=\"background-color:#b9babe;text-align:center\">"
                                                    + "<th>Name</th>"
                                                    + "<th>Price</th>"
                                                    + "<th>Quantity</th>"
                                                    + "<th>Total</th>"
                                                    + "</tr>";
                                    for (int i = 0; i < customerOrder.Items.Count; i++)
                                    {
                                        lstrHtmlContent += "<tr style=\"background-color:#ebecee; text-align:center\">"
                                                        + "<td style=\"padding:0.6em 0.4em; text-align:left\">" + customerOrder.Items[i].Name + "</td>"
                                                        + "<td style=\"padding:0.6em 0.4em; text-align:right\">" + lobjModel.FormatPoints(Math.Ceiling(customerOrder.Items[i].Price.ListPrice.Amount), "Points") + "</td>"
                                                        + "<td style=\"padding:0.6em 0.4em; text-align:center\">" + customerOrder.Items[i].Quantity + "</td>"
                                                        + "<td style=\"padding:0.6em 0.4em; text-align:right\">" + lobjModel.FormatPoints(Math.Ceiling(customerOrder.Items[i].Price.ExtendedPrice.Amount), "Points") + "</td>"
                                                        + "</tr>";
                                    }
                                    lstrHtmlContent += "<tr style=\"text-align:right\">"
                                                    + "<td></td>"
                                                    + "<td colspan=\"2\" style=\"background-color:#dde2e6; padding:0.6em 0.4 em\"><strong>Sub-Total:</strong></td>"
                                                    + "<td style=\"background-color:#dde2e6; padding:0.6em 0.4 em\"><strong>" + lobjModel.FormatPoints(Math.Ceiling(customerOrder.Price.SubTotal.Amount), "Points") + "</strong></td>"
                                                    + "</tr>"

                                                    + "<tr style=\"text-align:right\">"
                                                    + "<td></td>"
                                                    + "<td colspan = \"2\" style=\"background-color:#dde2e6; padding:0.6em 0.4 em\"><strong>Shipping:</strong></td>"
                                                    + "<td style = \"background-color:#dde2e6; padding:0.6em 0.4 em\"><strong>" + lobjModel.FormatPoints(Math.Ceiling(customerOrder.Price.ShippingTotal.Amount), "Points") + "</strong></td>"
                                                    + "</tr>"

                                                    + "<tr style=\"text-align:right\">"
                                                    + "<td></td>"
                                                    + "<td colspan = \"2\" style=\"background-color:#dde2e6; padding:0.6em 0.4 em\"><strong>Tax:</strong></td>"
                                                    + "<td style=\"background-color:#dde2e6; padding:0.6em 0.4 em\"><strong>" + lobjModel.FormatPoints(Math.Ceiling(customerOrder.Price.TaxTotal.Amount), "Points") + "</strong></td>"
                                                    + "</tr>"

                                                    + "<tr style=\"text-align:right\">"
                                                    + "<td></td>"
                                                    + "<td colspan=\"2\" style=\"background-color:#dde2e6; padding:0.6em 0.4 em\"><strong>Order Total:</strong></td>"
                                                    + "<td style=\"background-color:#dde2e6; padding:0.6em 0.4 em\"><strong>" + lobjModel.FormatPoints(Math.Ceiling(customerOrder.Price.Total.Amount), "Points") + "</strong></td>"
                                                    + "</tr>"

                                                    + "</tbody>"
                                                    + "</table>";
                                    lstrEmailParameters.Add(Convert.ToString(customerOrder.CreatedDate));//1
                                    lstrEmailParameters.Add(lstrHtmlContent);//2
                                    if (string.IsNullOrEmpty(customerOrder.CustomerName))
                                    {
                                        lstrEmailParameters.Add(Convert.ToString(lobjMemberDetails.FullName));//3
                                    }
                                    else
                                    {
                                        lstrEmailParameters.Add(Convert.ToString(customerOrder.CustomerName));//3
                                    }
                                    lstrEmailParameters.Add(Convert.ToString(customerOrder.ShippingAddress));//4
                                    lstrEmailParameters.Add(customerOrder.Number); //5
                                    string templateCode = "";
                                    if (lobjMemberDetails.PreferredLanguage == "EN")
                                    {
                                        templateCode = "Shop_Order";
                                        //lobjModel.SendEmail(lstrEmailParameters, lobjMemberDetails, "ShopOrderPlaced");
                                    }
                                    else if (lobjMemberDetails.PreferredLanguage == "AR")
                                    {
                                        templateCode = "ARShopOrderPlaced";
                                        //lobjModel.SendEmail(lstrEmailParameters, lobjMemberDetails, "ARShopOrderPlaced");
                                    }
                                    DateTime ldtCreatedDate = DateTime.Parse(Convert.ToString(customerOrder.CreatedDate));
                                    dynamic dynamicCls = new System.Dynamic.ExpandoObject();
                                    dynamicCls.event_name = templateCode;
                                    dynamicCls.relation_reference = Convert.ToString(lobjMemberDetails.MemberRelationsList.Find(lobj => lobj.RelationType.Equals(RelationType.LBMS)).RelationReference);
                                    dynamicCls.program_id = Convert.ToInt32(lobjMemberDetails.ProgramId); ;
                                    dynamicCls.to_email = lobjMemberDetails.Email;
                                    dynamicCls.full_name = lobjMemberDetails.FullName;
                                    dynamicCls.order_no = customerOrder.Number;
                                    dynamicCls.order_date = Convert.ToString(ldtCreatedDate.ToLocalTime());
                                    dynamicCls.order_details = lstrHtmlContent;
                                    dynamicCls.order_shipping_details = customerOrder.ShippingAddress;
                                    dynamicCls.to_mobile = lobjMemberDetails.MobileNumber;
                                    dynamicCls.point_issued = lobjModel.FormatPoints(Math.Ceiling(customerOrder.Price.Total.Amount), "Points");
                                    Dictionary<string, dynamic> lobjDictionary = new Dictionary<string, dynamic>();
                                    IDictionary<string, object> dict = (IDictionary<string, object>)dynamicCls;
                                    foreach (var key in dict)
                                    {
                                        lobjDictionary.Add(key.Key, key.Value);
                                    }
                                    string jsonParameters = JsonConvert.SerializeObject(lobjDictionary);
                                    lobjModel.SendEmails(jsonParameters, lobjMemberDetails);
                                    //else
                                    //{
                                    //    lobjModel.SendEmail(lstrEmailParameters, lobjMemberDetails, "ShopOrderPlaced");
                                    //}
                                }
                            }
                            else
                            {
                                LoggingAdapter.WriteLog("OrderNotification Order contains Digital products");
                                if (customerOrder.Status.ToLower().Equals("cancelled"))
                                {
                                    LoggingAdapter.WriteLog("OrderNotification Reversal Start");
                                    //reversal here
                                    //transaction type = 2, points=negative, loyalty_txn_type=8
                                    //send email for reversal to customer
                                    string lstrCurrency = lobjModel.GetDefaultCurrency();
                                    SearchTransactions objSearchTransactions = new SearchTransactions();
                                    objSearchTransactions.RelationReference = customerOrder.CustomerId;
                                    objSearchTransactions.MaximumRange = 100000;
                                    objSearchTransactions.DateFrom = DateTime.Parse(customerOrder.CreatedDate.ToString());
                                    objSearchTransactions.DateTo = DateTime.Parse(customerOrder.CreatedDate.ToString());
                                    List<TransactionDetails> lobjTransactionDetailList = lobjModel.GetMemberTransactionSummaryByDate(objSearchTransactions);
                                    TransactionDetails lobjTransactionDetails = lobjTransactionDetailList.FirstOrDefault(lobj => lobj.ExternalReference == customerOrder.InPayments[0].OuterId);
                                    Product lobjProduct = new Product();
                                    lobjProduct = model.GetProductById(customerOrder.Items[0].ProductId);
                                    if (lobjProduct != null)
                                    {
                                        string PaymentType = string.Empty;
                                        if (customerOrder.InPayments.Count > 0)
                                        {
                                            bool lblnReversalResult = false;
                                            PaymentType = customerOrder.InPayments[0].DynamicProperties.Find(x => Convert.ToString(x.Name) == "CardType").Values[0].Value.ToString();
                                            float lfltPointRate = 0.0f;
                                            List<ProgramCurrencyDefinition> lobjProgramCurrency = lobjModel.GetProgramCurrencyDefinition(lobjMemberDetails.ProgramId);
                                            lfltPointRate = lobjProgramCurrency[0].RedemptionRate;
                                            double ldblAmount = 0.00;
                                            PGDetails lobjPGDetails = null;
                                            switch (PaymentType)
                                            {
                                                case "Points":
                                                     ldblAmount = Convert.ToInt32(Math.Ceiling(lobjProduct.Price.SalePrice.TruncatedAmount)) * lfltPointRate;
                                                    lblnReversalResult = lobjModel.InsertTransactionDetails(lobjMemberDetails.MemberRelationsList.Find(lobj => lobj.RelationType.Equals(RelationType.LBMS)).RelationReference,
                                                        Convert.ToDecimal(ldblAmount),
                                   Convert.ToInt32(-lobjProduct.Price.SalePrice.TruncatedAmount), TransactionType.Debit, LoyaltyTxnType.Reversal, lobjTransactionDetails.Id.ToString(),
                                   lobjProduct.Name, string.Format("{0}|{1}", lobjMemberDetails.Email, lobjMemberDetails.MobileNumber), string.Format("OrderNumber:{0}|ProductName:{1}|ProductId:{2}", orderNumber, lobjProduct.Name, lobjProduct.Id),
                                   Convert.ToDecimal(0), lstrCurrency);
                                                   // lblnReversalResult = lobjModel.RollBackTransaction(customerOrder.InPayments[0].OuterId, lobjMemberDetails.MemberRelationsList.Find(lobj => lobj.RelationType.Equals(RelationType.LBMS)).RelationReference, lobjProduct.Name);
                                                    break;
                                                case "CashPoints":
                                                    lobjPGDetails = lobjModel.GetPaymentStatusByOrderId(customerOrder.InPayments[0].OuterId);
                                                    ldblAmount = Convert.ToInt32(Math.Ceiling(lobjProduct.Price.SalePrice.TruncatedAmount)) * lfltPointRate;
                                                    lblnReversalResult = lobjModel.InsertTransactionDetails(lobjMemberDetails.MemberRelationsList.Find(lobj => lobj.RelationType.Equals(RelationType.LBMS)).RelationReference,
                                                        Convert.ToDecimal(ldblAmount),
                                   Convert.ToInt32(-lobjTransactionDetails.Points), TransactionType.Debit, LoyaltyTxnType.Reversal, lobjTransactionDetails.Id.ToString(),
                                   lobjProduct.Name, string.Format("{0}|{1}", lobjMemberDetails.Email, lobjMemberDetails.MobileNumber), string.Format("OrderNumber:{0}|ProductName:{1}|ProductId:{2}", orderNumber, lobjProduct.Name, lobjProduct.Id),
                                   Convert.ToDecimal(lobjTransactionDetails.TransactionDetailBreakage.SourceAmount), lstrCurrency);
                                                    //lblnReversalResult = lobjModel.RollBackTransaction(customerOrder.InPayments[0].OuterId, lobjMemberDetails.MemberRelationsList.Find(lobj => lobj.RelationType.Equals(RelationType.LBMS)).RelationReference, lobjProduct.Name);
                                                    lblnReversalResult = lobjModel.InitiatePaymentRefund(customerOrder.InPayments[0].OuterId, Convert.ToDecimal(lobjPGDetails.data[0].orderAmount), "refund it please");
                                                    break;
                                                case "Cash":
                                                    lobjPGDetails = lobjModel.GetPaymentStatusByOrderId(customerOrder.InPayments[0].OuterId);
                                                     ldblAmount = Convert.ToInt32(Math.Ceiling(lobjProduct.Price.SalePrice.TruncatedAmount)) * lfltPointRate;
                                                    lblnReversalResult = lobjModel.InsertTransactionDetails(lobjMemberDetails.MemberRelationsList.Find(lobj => lobj.RelationType.Equals(RelationType.LBMS)).RelationReference,
                                                        Convert.ToDecimal(ldblAmount),
                                   Convert.ToInt32(0), TransactionType.Debit, LoyaltyTxnType.Reversal, lobjTransactionDetails.Id.ToString(),
                                   lobjProduct.Name, string.Format("{0}|{1}", lobjMemberDetails.Email, lobjMemberDetails.MobileNumber), string.Format("OrderNumber:{0}|ProductName:{1}|ProductId:{2}", orderNumber, lobjProduct.Name, lobjProduct.Id),
                                   Convert.ToDecimal(ldblAmount), lstrCurrency);
                                                    //lblnReversalResult = lobjModel.RollBackTransaction(customerOrder.InPayments[0].OuterId, lobjMemberDetails.MemberRelationsList.Find(lobj => lobj.RelationType.Equals(RelationType.LBMS)).RelationReference, lobjProduct.Name);
                                                    lblnReversalResult = lobjModel.InitiatePaymentRefund(customerOrder.InPayments[0].OuterId, Convert.ToDecimal(lobjPGDetails.data[0].orderAmount), "refund it please");
                                                    break;
                                            }

                                            LoggingAdapter.WriteLog("OrderNotification Reversal = " + lblnReversalResult);
                                            if (lblnReversalResult)
                                            {

                                                //send notification
                                                string templateCode = "Reversal_Redemption";
                                                dynamic dynamicCls = new System.Dynamic.ExpandoObject();
                                                dynamicCls.event_name = templateCode;
                                                dynamicCls.relation_reference = Convert.ToString(lobjMemberDetails.MemberRelationsList.Find(lobj => lobj.RelationType.Equals(RelationType.LBMS)).RelationReference);
                                                dynamicCls.program_id = Convert.ToInt32(lobjMemberDetails.ProgramId); ;
                                                dynamicCls.to_email = lobjMemberDetails.Email;
                                                dynamicCls.full_name = lobjMemberDetails.FullName;
                                                dynamicCls.to_mobile = lobjMemberDetails.MobileNumber;
                                                dynamicCls.point_issued = lobjModel.FormatPoints(lobjTransactionDetails.Points,"Points");
                                                dynamicCls.order_no = customerOrder.Number;
                                                DateTime ldtCreatedDate = DateTime.Parse(Convert.ToString(customerOrder.CreatedDate));
                                                dynamicCls.order_date = Convert.ToString(ldtCreatedDate.ToLocalTime());
                                                dynamicCls.productName = lobjProduct.Name.ToString();
                                                Dictionary<string, dynamic> lobjDictionary = new Dictionary<string, dynamic>();
                                                IDictionary<string, object> dict = (IDictionary<string, object>)dynamicCls;
                                                foreach (var key in dict)
                                                {
                                                    lobjDictionary.Add(key.Key, key.Value);
                                                }
                                                string jsonParameters = JsonConvert.SerializeObject(lobjDictionary);
                                                lobjModel.SendEmails(jsonParameters, lobjMemberDetails);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    Product lobjProduct = new Product();
                                    lobjProduct = model.GetProductById(customerOrder.Items[0].ProductId);
                                    string[] VendorId = ConfigurationManager.AppSettings["VendorId"].Split('|');
                                    string Vendorlogo = ConfigurationManager.AppSettings["Vendorlogo"];
                                    if (lobjProduct != null)
                                    {
                                        string lstrDigitalProductType = lobjProduct.Properties.ToList().Find(lobj => lobj.Name.Equals("Type")).Value;
                                        LoggingAdapter.WriteLog("OrderNotification DigitalProductType -:" + lstrDigitalProductType);
                                        if (lstrDigitalProductType.ToLower().Equals("giftcard"))
                                        {
                                            GiftCardDetails giftCardDetails = null;
                                            try
                                            {
                                                LoggingAdapter.WriteLog("OrderNotification Check for ResponseMetas");
                                                giftCardDetails = JsonConvert.DeserializeObject<GiftCardDetails>(customerOrder.Shipments[0].DynamicProperties.Find(lobj => lobj.Name.Equals("ResponseMetas")).Values[0].Value);
                                                LoggingAdapter.WriteLog("OrderNotification ResponseMetas found");
                                                if (giftCardDetails != null)
                                                {
                                                    List<string> lstrEmailParameters = new List<string>();
                                                    lstrEmailParameters.Add(lobjProduct.PrimaryImage.Url);//0
                                                    string lstrHtml = string.Empty;
                                                    if (!string.IsNullOrEmpty(giftCardDetails.GiftCardInfo[0].Code))
                                                    {
                                                        lstrHtml += "<tr>";
                                                        lstrHtml += "<td width=\"40%\" height=\"35\" bgcolor=\"#006677\"><p style=\"color:#fff;\"><strong>GiftCard No/GiftCard Link</strong></p></td>";
                                                        lstrHtml += "<td width=\"15%\" height=\"35\" bgcolor=\"#006677\"><p style=\"color:#fff;\"><strong>GiftCard Value</strong></p></td>";
                                                        //if (giftCardDetails.GiftCardInfo[0].ExpiryDate != null && !giftCardDetails.GiftCardInfo[0].ExpiryDate.Equals(DateTime.MinValue))
                                                        //{
                                                        lstrHtml += "<td width=\"15%\" height=\"35\" bgcolor=\"#006677\"><p style=\"color:#fff;\"><strong>Expiry Date</strong></p></td>";
                                                        //}
                                                        if (!string.IsNullOrEmpty(giftCardDetails.GiftCardInfo[0].Pin))
                                                        {
                                                            lstrHtml += "<td width=\"15%\" height=\"35\" bgcolor=\"#006677\"><p style=\"color:#fff;\"><strong>Pin</strong></p></td>";
                                                        }
                                                        if (!Uri.IsWellFormedUriString(giftCardDetails.GiftCardInfo[0].Code, UriKind.Absolute))
                                                        {
                                                            lstrHtml += "<td width=\"15%\" height=\"35\" bgcolor=\"#006677\"><p style=\"color:#fff;\"><strong>Barcode</strong></p></td>";
                                                        }
                                                        if (!string.IsNullOrEmpty(giftCardDetails.GiftCardInfo[0].RedirectionUrl))
                                                        {
                                                            lstrHtml += "<td width=\"15%\" height=\"35\" bgcolor=\"#006677\"><p style=\"color:#fff;\"><strong>RedirectionUrl</strong></p></td>";
                                                        }
                                                        lstrHtml += "</tr>";
                                                        for (int i = 0; i < giftCardDetails.GiftCardInfo.Count; i++)
                                                        {
                                                            string ImageUrl = string.Empty;
                                                            lstrHtml += "<tr>";
                                                            if (!Uri.IsWellFormedUriString(giftCardDetails.GiftCardInfo[i].Code, UriKind.Absolute))
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
                                                                lstrHtml += "<td width=\"40%\" height=\"25\" bgcolor=\"#FFFFFF\"><p><strong>" + giftCardDetails.GiftCardInfo[i].Code + "</strong></p></td>";
                                                            }
                                                            else
                                                            {
                                                                lstrHtml += "<td width=\"40%\" height=\"25\" bgcolor=\"#FFFFFF\"><a href=\"" + giftCardDetails.GiftCardInfo[i].Code + "\" target=\"_blank\" style=\"font-family: Helvetica, Arial, sans-serif;font-size: 13px; color: #000000;display: inline-block;margin-left\">" + "Click here" + "</a></td>";
                                                            }
                                                            lstrHtml += "<td width=\"15%\" height=\"25\" bgcolor=\"#FFFFFF\"><p>" + giftCardDetails.GiftCardInfo[i].Value + "</p></td>";
                                                            if (giftCardDetails.GiftCardInfo[i].ExpiryDate != null && !giftCardDetails.GiftCardInfo[0].ExpiryDate.Equals(DateTime.MinValue) || giftCardDetails.GiftCardInfo[0].ExpiryDate.ToString() != "")
                                                            {
                                                                try
                                                                {
                                                                    lstrHtml += "<td width=\"15%\" height=\"25\" bgcolor=\"#FFFFFF\">" + Convert.ToDateTime(giftCardDetails.GiftCardInfo[i].ExpiryDate).ToString("dd-MM-yyyy") + "</td>";
                                                                }
                                                                catch (Exception ex)
                                                                {
                                                                    lstrHtml += "<td width=\"15%\" height=\"25\" bgcolor=\"#FFFFFF\">" + giftCardDetails.GiftCardInfo[i].ExpiryDate + "</td>";
                                                                    LoggingAdapter.WriteLog("Error GiftCards Expiry Date - " + ex.Message + Environment.NewLine + ex.StackTrace);
                                                                }
                                                            }
                                                            else
                                                            {

                                                                try
                                                                {
                                                                    lstrHtml += "<td width=\"15%\" height=\"25\" bgcolor=\"#FFFFFF\">" + "N/A" + "</td>";
                                                                }
                                                                catch (Exception ex)
                                                                {
                                                                    lstrHtml += "<td width=\"15%\" height=\"25\" bgcolor=\"#FFFFFF\">" + giftCardDetails.GiftCardInfo[i].ExpiryDate + "</td>";
                                                                    LoggingAdapter.WriteLog("Error GiftCards Expiry Date - " + ex.Message + Environment.NewLine + ex.StackTrace);
                                                                }
                                                            }
                                                            if (!string.IsNullOrEmpty(giftCardDetails.GiftCardInfo[i].Pin))
                                                            {
                                                                lstrHtml += "<td width=\"15%\" height=\"25\" bgcolor=\"#FFFFFF\">" + Convert.ToString(giftCardDetails.GiftCardInfo[i].Pin) + "</td>";
                                                            }
                                                            if (!Uri.IsWellFormedUriString(giftCardDetails.GiftCardInfo[i].Code, UriKind.Absolute))
                                                            {
                                                                lstrHtml += "<td width=\"15%\" height=\"25\" bgcolor=\"#FFFFFF\"><img src=\"" + ImageUrl + "\"/></td>";
                                                            }
                                                            if (!string.IsNullOrEmpty(giftCardDetails.GiftCardInfo[i].RedirectionUrl))
                                                            {
                                                                lstrHtml += "<td width=\"15%\" height=\"25\" bgcolor=\"#FFFFFF\">for detail please click : <a href=\"" + giftCardDetails.GiftCardInfo[i].RedirectionUrl + "\" target=\"_blank\" style=\"font-family: Helvetica, Arial, sans-serif;font-size: 13px; color: #000000;display: inline-block;margin-left\">" + "here" + "</a></td>";
                                                            }
                                                            lstrHtml += "</tr>";
                                                        }
                                                        LoggingAdapter.WriteLog(lstrHtml);
                                                    }
                                                    else if (!string.IsNullOrEmpty(giftCardDetails.GiftCardInfo[0].RedirectionUrl))
                                                    {
                                                        // lstrHtml = "Your Voucher Details is Sent on your Registered Email ID/Mobile";
                                                        lstrHtml += "<tr>";
                                                        if (!string.IsNullOrEmpty(giftCardDetails.GiftCardInfo[0].RedirectionUrl))
                                                        {
                                                            lstrHtml += "<td width=\"15%\" height=\"35\" bgcolor=\"#006677\"><p style=\"color:#fff;\"><strong>Gift Card Redemption URL</strong></p></td>";
                                                        }

                                                        if (!string.IsNullOrEmpty(giftCardDetails.GiftCardInfo[0].Value))
                                                        {
                                                            lstrHtml += "<td width=\"15%\" height=\"35\" bgcolor=\"#006677\"><p style=\"color:#fff;\"><strong>Value</strong></p></td>";
                                                        }
                                                        //if (giftCardDetails.GiftCardInfo[0].ExpiryDate != null)
                                                        //{
                                                        lstrHtml += "<td width=\"15%\" height=\"35\" bgcolor=\"#006677\"><p style=\"color:#fff;\"><strong>Expiry Date</strong></p></td>";
                                                        //}

                                                        //lstrHtml += "<td width=\"15%\" height=\"35\" bgcolor=\"#006677\"><p style=\"color:#fff;\"><strong>Barcode</strong></p></td>";


                                                        lstrHtml += "</tr>";



                                                        lstrHtml += "<tr>";
                                                        if (!string.IsNullOrEmpty(giftCardDetails.GiftCardInfo[0].RedirectionUrl))
                                                        {
                                                            lstrHtml += "<td width=\"15%\" height=\"25\" bgcolor=\"#FFFFFF\"><p>" + giftCardDetails.GiftCardInfo[0].RedirectionUrl + "</p></td>";
                                                        }
                                                        if (!string.IsNullOrEmpty(giftCardDetails.GiftCardInfo[0].Value))
                                                        {
                                                            lstrHtml += "<td width=\"15%\" height=\"25\" bgcolor=\"#FFFFFF\">" + Convert.ToString(giftCardDetails.GiftCardInfo[0].Value) + "</td>";
                                                        }
                                                        if (giftCardDetails.GiftCardInfo[0].ExpiryDate != null || giftCardDetails.GiftCardInfo[0].ExpiryDate.ToString() != "")
                                                        {
                                                            try
                                                            {
                                                                lstrHtml += "<td width=\"15%\" height=\"25\" bgcolor=\"#FFFFFF\">" + Convert.ToDateTime(giftCardDetails.GiftCardInfo[0].ExpiryDate).ToString("dd-MM-yyyy") + "</td>";

                                                            }
                                                            catch (Exception ex)
                                                            {
                                                                lstrHtml += "<td width=\"15%\" height=\"25\" bgcolor=\"#FFFFFF\">" + giftCardDetails.GiftCardInfo[0].ExpiryDate + "</td>";
                                                                LoggingAdapter.WriteLog("Error GiftCards Expiry Date - " + ex.Message + Environment.NewLine + ex.StackTrace);
                                                            }
                                                        }
                                                        else
                                                        {
                                                            try
                                                            {
                                                                lstrHtml += "<td width=\"15%\" height=\"25\" bgcolor=\"#FFFFFF\">" + "N/A" + "</td>";

                                                            }
                                                            catch (Exception ex)
                                                            {
                                                                lstrHtml += "<td width=\"15%\" height=\"25\" bgcolor=\"#FFFFFF\">" + giftCardDetails.GiftCardInfo[0].ExpiryDate + "</td>";
                                                                LoggingAdapter.WriteLog("Error GiftCards Expiry Date - " + ex.Message + Environment.NewLine + ex.StackTrace);
                                                            }

                                                        }


                                                        lstrHtml += "</tr>";

                                                        LoggingAdapter.WriteLog(lstrHtml);
                                                    }
                                                    else
                                                    {
                                                        lstrHtml = "Your Voucher Details is Sent on your Registered Email ID/Mobile";
                                                    }
                                                    lstrEmailParameters.Add(lstrHtml);//1
                                                    var tncProp = lobjProduct.Properties.ToList().Find(lobj => lobj.Name.Equals("TermsAndCondition"));
                                                    string tnc = "";
                                                    if (tncProp != null && (!string.IsNullOrEmpty(tncProp.Value) || tncProp.LocalizedValues != null))
                                                    {
                                                        tnc = !string.IsNullOrEmpty(tncProp.Value) ? tncProp.Value : tncProp.LocalizedValues.ToList()[0].Value;
                                                        lstrEmailParameters.Add(tnc);//2
                                                    }
                                                    else
                                                    {
                                                        lstrEmailParameters.Add("");//2
                                                    }
                                                    lstrEmailParameters.Add(lobjMemberDetails.FullName);//3
                                                    string emailparalogo = string.Empty;
                                                    foreach (var it in VendorId.Select((x, i) => new { Value = x, Index = i }))
                                                    {
                                                        //foreach (var item in Vendorlogo.Select((y, j) => new { Value = y, Index = j }))
                                                        //{
                                                        //    if (it.Index == item.Index)
                                                        //    {
                                                        if (lobjProduct.VendorId.Contains(it.Value))
                                                        {
                                                            emailparalogo = "<tr><td align=\"left\" valign=\"top\" colspan=\"3\"><table cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" border=\"0\"><tbody><tr><td align=\"left\" valign=\"top\" width=\"100%\"><table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\"><tbody><tr><td style=\"text-align:center\" align=\"center\"><img width=\"133\" border=\"0\" height=\"156\" alt=\"\" src=\"" + Vendorlogo + "\" style=\"display:block;border:none;outline:0;text-decoration:none;display:block;margin-left:auto;margin-right:auto\"></td></tr></tbody></table></td></tr></tbody></table></td></tr>";//4
                                                        }

                                                        //    }
                                                        //}

                                                    }
                                                    lstrEmailParameters.Add(emailparalogo);

                                                    dynamic dynamicCls = new System.Dynamic.ExpandoObject();
                                                    dynamicCls.event_name = "Giift_Card_Redemption";
                                                    dynamicCls.relation_reference = Convert.ToString(lobjMemberDetails.MemberRelationsList.Find(lobj => lobj.RelationType.Equals(RelationType.LBMS)).RelationReference);
                                                    dynamicCls.program_id = Convert.ToInt32(lobjMemberDetails.ProgramId); ;
                                                    dynamicCls.to_email = lobjMemberDetails.Email;
                                                    dynamicCls.full_name = lobjMemberDetails.FullName;
                                                    dynamicCls.VoucherImg = lobjProduct.PrimaryImage.Url;
                                                    dynamicCls.voucher_details = lstrHtml;
                                                    dynamicCls.termsAndConditions = tnc;
                                                    dynamicCls.logo = emailparalogo;
                                                    dynamicCls.to_mobile = lobjMemberDetails.MobileNumber;
                                                    Dictionary<string, dynamic> lobjDictionary = new Dictionary<string, dynamic>();
                                                    IDictionary<string, object> dict = (IDictionary<string, object>)dynamicCls;
                                                    foreach (var key in dict)
                                                    {
                                                        lobjDictionary.Add(key.Key, key.Value);
                                                    }
                                                    string jsonParameters = JsonConvert.SerializeObject(lobjDictionary);
                                                    lobjModel.SendEmails(jsonParameters, lobjMemberDetails);

                                                    // lobjModel.SendEmail(lstrEmailParameters, lobjMemberDetails, "GiftVoucher");
                                                }
                                            }
                                            catch (Exception ex)
                                            {
                                                LoggingAdapter.WriteLog("OrderNotification.aspx|giftcard|exception: " + ex.Message + Environment.NewLine + ex.StackTrace);
                                            }
                                        }
                                        else if (lstrDigitalProductType.ToLower().Equals("milesexchange") || lstrDigitalProductType.ToLower().Equals("miles exchange"))
                                        {
                                            MilesExchangeDetails milesExchangeDetails = null;
                                            try
                                            {
                                                LoggingAdapter.WriteLog("OrderNotification MilesExchange Check for ResponseMetas");
                                                milesExchangeDetails = JsonConvert.DeserializeObject<MilesExchangeDetails>(customerOrder.Shipments[0].DynamicProperties.ToList().Find(lobj => lobj.Name.Equals("ResponseMetas")).Values[0].Value);
                                                LoggingAdapter.WriteLog("OrderNotification MilesExchange ResponseMetas Found");
                                                string CustomerName = string.Empty;
                                                if (milesExchangeDetails != null)
                                                {
                                                    List<string> lstrEmailParameters = new List<string>();
                                                    lstrEmailParameters.Add(lobjProduct.PrimaryImage.Url);//0
                                                    string lstrHtml = string.Empty;
                                                    if (milesExchangeDetails.Error != null)
                                                    {
                                                        lstrHtml = "<p><strong>Topup purchased</strong></p>";
                                                    }
                                                    else
                                                    {
                                                        lstrHtml = "<p><strong>" + (milesExchangeDetails.Error == null ? string.Empty : milesExchangeDetails.Error.Message) + "</strong></p>";
                                                    }
                                                    LoggingAdapter.WriteLog(lstrHtml);
                                                    lstrEmailParameters.Add(lstrHtml);//1
                                                    var tncProp = lobjProduct.Properties.ToList().Find(lobj => lobj.Name.Equals("TermsAndCondition"));
                                                    string tnc = "";
                                                    if (tncProp != null && (!string.IsNullOrEmpty(tncProp.Value) || tncProp.LocalizedValues != null))
                                                    {
                                                        tnc = !string.IsNullOrEmpty(tncProp.Value) ? tncProp.Value : tncProp.LocalizedValues.ToList()[0].Value;
                                                        lstrEmailParameters.Add(tnc);//2
                                                    }
                                                    else
                                                    {
                                                        lstrEmailParameters.Add("");//2
                                                    }
                                                    lstrEmailParameters.Add(lobjMemberDetails.FullName);//3

                                                    dynamic dynamicCls = new System.Dynamic.ExpandoObject();
                                                    dynamicCls.event_name = "Miles_Exchange";
                                                    dynamicCls.relation_reference = Convert.ToString(lobjMemberDetails.MemberRelationsList.Find(lobj => lobj.RelationType.Equals(RelationType.LBMS)).RelationReference);
                                                    dynamicCls.program_id = Convert.ToInt32(lobjMemberDetails.ProgramId); ;
                                                    dynamicCls.to_email = lobjMemberDetails.Email;
                                                    dynamicCls.full_name = lobjMemberDetails.FullName;
                                                    dynamicCls.to_mobile = lobjMemberDetails.MobileNumber;
                                                    //dynamicCls.VoucherImg = lobjProduct.PrimaryImage.Url;
                                                    //dynamicCls.voucher_details = lstrHtml;
                                                    //dynamicCls.termsAndConditions = tnc;
                                                    Dictionary<string, dynamic> lobjDictionary = new Dictionary<string, dynamic>();
                                                    IDictionary<string, object> dict = (IDictionary<string, object>)dynamicCls;
                                                    foreach (var key in dict)
                                                    {
                                                        lobjDictionary.Add(key.Key, key.Value);
                                                    }
                                                    string jsonParameters = JsonConvert.SerializeObject(lobjDictionary);
                                                    lobjModel.SendEmails(jsonParameters, lobjMemberDetails);
                                                    //lobjModel.SendEmail(lstrEmailParameters, lobjMemberDetails, "MilesExchangeVoucher");
                                                }
                                            }
                                            catch (Exception ex)
                                            {
                                                LoggingAdapter.WriteLog("OrderNotification.aspx|milesexchange|exception: " + ex.Message + Environment.NewLine + ex.StackTrace);
                                            }
                                        }
                                        else if (lstrDigitalProductType.ToLower().Equals("game"))
                                        {
                                            GameDetails gameDetails = null;
                                            try
                                            {
                                                LoggingAdapter.WriteLog("OrderNotification Check for ResponseMetas");
                                                gameDetails = JsonConvert.DeserializeObject<GameDetails>(customerOrder.Shipments[0].DynamicProperties.Find(lobj => lobj.Name.Equals("ResponseMetas")).Values[0].Value);
                                                LoggingAdapter.WriteLog("OrderNotification ResponseMetas found");
                                                string CustomerName = string.Empty;
                                                if (gameDetails != null)
                                                {
                                                    List<string> lstrEmailParameters = new List<string>();
                                                    lstrEmailParameters.Add(lobjProduct.PrimaryImage.Url);//0
                                                    string lstrHtml = string.Empty;
                                                    lstrHtml += "<tr>";
                                                    lstrHtml += "<td width=\"40%\" height=\"35\" bgcolor=\"#006677\"><p style=\"color:#fff;\"><strong>Game Link</strong></p></td>";
                                                    if (gameDetails.GameInfo.Pin != null)
                                                    {
                                                        lstrHtml += "<td width=\"15%\" height=\"35\" bgcolor=\"#006677\"><p style=\"color:#fff;\"><span>Pin</span></p></td>";
                                                    }
                                                    if (gameDetails.GameInfo.ExpiryDate != null && !gameDetails.GameInfo.ExpiryDate.Equals(DateTime.MinValue))
                                                    {
                                                        lstrHtml += "<td width=\"15%\" height=\"35\" bgcolor=\"#006677\"><p style=\"color:#fff;\"><span>Expiry Date</span></p></td>";
                                                    }
                                                    if (gameDetails.GameInfo.Message != null)
                                                    {
                                                        lstrHtml += "<td width=\"25%\" height=\"35\" bgcolor=\"#006677\"><p style=\"color:#fff;\"><span>Message</span></p></td>";
                                                    }
                                                    lstrHtml += "</tr>";
                                                    string ImageUrl = string.Empty;
                                                    lstrHtml += "<tr>";
                                                    lstrHtml += "<td width=\"40%\" height=\"25\" bgcolor=\"#FFFFFF\"><p><strong> For play games please click : <a href =\"" + gameDetails.GameInfo.Url + "\" target=\"_blank\" style=\"font-family: Helvetica, Arial, sans-serif;font-size: 13px; color: #000000;display: inline-block;margin-left\">Here</a></strong></p></td>";
                                                    if (gameDetails.GameInfo.Pin != null)
                                                    {
                                                        lstrHtml += "<td width=\"15%\" height=\"25\" bgcolor=\"#FFFFFF\">" + gameDetails.GameInfo.Pin + "</td>";
                                                    }
                                                    if (gameDetails.GameInfo.ExpiryDate != null && !gameDetails.GameInfo.ExpiryDate.Equals(DateTime.MinValue))
                                                    {
                                                        try
                                                        {
                                                            lstrHtml += "<td width=\"15%\" height=\"25\" bgcolor=\"#FFFFFF\">" + gameDetails.GameInfo.ExpiryDate.ToString("dd-MM-yyyy") + "</td>";
                                                        }
                                                        catch (Exception ex)
                                                        {
                                                            lstrHtml += "<td width=\"15%\" height=\"25\" bgcolor=\"#FFFFFF\">" + gameDetails.GameInfo.ExpiryDate + "</td>";
                                                            LoggingAdapter.WriteLog("Error Game Expiry Date - " + ex.Message + Environment.NewLine + ex.StackTrace);
                                                        }
                                                    }
                                                    if (gameDetails.GameInfo.Message != null)
                                                    {
                                                        lstrHtml += "<td width=\"25%\" height=\"25\" bgcolor=\"#FFFFFF\">" + gameDetails.GameInfo.Message + "</td>";
                                                    }
                                                    LoggingAdapter.WriteLog(lstrHtml);
                                                    lstrEmailParameters.Add(lstrHtml);//1
                                                    var tncProp = lobjProduct.Properties.ToList().Find(lobj => lobj.Name.Equals("TermsAndCondition"));
                                                    string tnc = "";
                                                    if (tncProp != null && (!string.IsNullOrEmpty(tncProp.Value) || tncProp.LocalizedValues != null))
                                                    {
                                                        tnc = !string.IsNullOrEmpty(tncProp.Value) ? tncProp.Value : tncProp.LocalizedValues.ToList()[0].Value;
                                                        lstrEmailParameters.Add(tnc);//2
                                                    }
                                                    else
                                                    {
                                                        lstrEmailParameters.Add("");//2
                                                    }
                                                    lstrEmailParameters.Add(lobjMemberDetails.FullName);//3

                                                    dynamic dynamicCls = new System.Dynamic.ExpandoObject();
                                                    dynamicCls.event_name = "GameVoucher";
                                                    dynamicCls.relation_reference = Convert.ToString(lobjMemberDetails.MemberRelationsList.Find(lobj => lobj.RelationType.Equals(RelationType.LBMS)).RelationReference);
                                                    dynamicCls.program_id = Convert.ToInt32(lobjMemberDetails.ProgramId); ;
                                                    dynamicCls.to_email = lobjMemberDetails.Email;
                                                    dynamicCls.full_name = lobjMemberDetails.FullName;
                                                    dynamicCls.VoucherImg = lobjProduct.PrimaryImage.Url;
                                                    dynamicCls.voucher_details = lstrHtml;
                                                    dynamicCls.termsAndConditions = tnc;
                                                    dynamicCls.to_mobile = lobjMemberDetails.MobileNumber;
                                                    Dictionary<string, dynamic> lobjDictionary = new Dictionary<string, dynamic>();
                                                    IDictionary<string, object> dict = (IDictionary<string, object>)dynamicCls;
                                                    foreach (var key in dict)
                                                    {
                                                        lobjDictionary.Add(key.Key, key.Value);
                                                    }
                                                    string jsonParameters = JsonConvert.SerializeObject(lobjDictionary);
                                                    lobjModel.SendEmails(jsonParameters, lobjMemberDetails);
                                                    //lobjModel.SendEmail(lstrEmailParameters, lobjMemberDetails, "GameVoucher");
                                                }
                                            }
                                            catch (Exception ex)
                                            {
                                                LoggingAdapter.WriteLog("OrderNotification.aspx|game|exception: " + ex.Message + Environment.NewLine + ex.StackTrace);
                                            }
                                        }
                                        else if (lstrDigitalProductType.ToLower().Equals("lounge"))
                                        {
                                            LoungeDetails loungeDetails = null;
                                            try
                                            {
                                                LoggingAdapter.WriteLog("OrderNotification Check for ResponseMetas");
                                                loungeDetails = JsonConvert.DeserializeObject<LoungeDetails>(customerOrder.Shipments[0].DynamicProperties.Find(lobj => lobj.Name.Equals("ResponseMetas")).Values[0].Value);
                                                LoggingAdapter.WriteLog("OrderNotification ResponseMetas found");
                                                string CustomerName = string.Empty;
                                                if (loungeDetails != null)
                                                {
                                                    List<string> lstrEmailParameters = new List<string>();
                                                    lstrEmailParameters.Add(lobjProduct.PrimaryImage.Url);//0
                                                    string lstrHtml = string.Empty;
                                                    lstrHtml += "<tr>";
                                                    lstrHtml += "<td width=\"40%\" height=\"35\" bgcolor=\"#006677\"><p style=\"color:#fff;\"><span>Lounge Code</span></p></td>";
                                                    if (!string.IsNullOrEmpty(loungeDetails.LoungeInfo.Value))
                                                    {
                                                        lstrHtml += "<td width=\"15%\" height=\"35\" bgcolor=\"#006677\"><p style=\"color:#fff;\"><span>Lounge Value</span></p></td>";
                                                    }
                                                    if (loungeDetails.LoungeInfo.ExpiryDate != null && !loungeDetails.LoungeInfo.ExpiryDate.Equals(DateTime.MinValue))
                                                    {
                                                        lstrHtml += "<td width=\"15%\" height=\"35\" bgcolor=\"#006677\"><p style=\"color:#fff;\"><span>Expiry Date</span></p></td>";
                                                    }
                                                    lstrHtml += "</tr>";
                                                    string ImageUrl = string.Empty;
                                                    lstrHtml += "<tr>";
                                                    lstrHtml += "<td width=\"40%\" height=\"25\" bgcolor=\"#FFFFFF\"><p><strong>" + loungeDetails.LoungeInfo.Code + "</strong></p>";
                                                    lstrHtml += "</td>";
                                                    if (!string.IsNullOrEmpty(loungeDetails.LoungeInfo.Value))
                                                    {
                                                        lstrHtml += "<td width=\"15%\" height=\"25\" bgcolor=\"#FFFFFF\"><p>" + loungeDetails.LoungeInfo.Value + "</p></td>";
                                                    }
                                                    if (loungeDetails.LoungeInfo.ExpiryDate != null && !loungeDetails.LoungeInfo.ExpiryDate.Equals(DateTime.MinValue))
                                                    {
                                                        try
                                                        {
                                                            lstrHtml += "<td width=\"15%\" height=\"25\" bgcolor=\"#FFFFFF\">" + loungeDetails.LoungeInfo.ExpiryDate.ToString("dd-MM-yyyy") + "</td>";
                                                        }
                                                        catch (Exception ex)
                                                        {
                                                            lstrHtml += "<td width=\"15%\" height=\"25\" bgcolor=\"#FFFFFF\">" + loungeDetails.LoungeInfo.ExpiryDate + "</td>";
                                                            LoggingAdapter.WriteLog("Error Lounge Expiry Date - " + ex.Message + Environment.NewLine + ex.StackTrace);
                                                        }
                                                    }
                                                    lstrHtml += "</tr>";
                                                    LoggingAdapter.WriteLog(lstrHtml);
                                                    lstrEmailParameters.Add(lstrHtml);//1
                                                    var tncProp = lobjProduct.Properties.ToList().Find(lobj => lobj.Name.Equals("TermsAndCondition"));
                                                    string tnc = "";
                                                    if (tncProp != null && (!string.IsNullOrEmpty(tncProp.Value) || tncProp.LocalizedValues != null))
                                                    {
                                                        tnc = !string.IsNullOrEmpty(tncProp.Value) ? tncProp.Value : tncProp.LocalizedValues.ToList()[0].Value;
                                                        lstrEmailParameters.Add(tnc);//2
                                                    }
                                                    else
                                                    {
                                                        lstrEmailParameters.Add("");//2
                                                    }
                                                    lstrEmailParameters.Add(lobjMemberDetails.FullName);//3
                                                    string emailparalogo = string.Empty;
                                                    dynamic dynamicCls = new System.Dynamic.ExpandoObject();
                                                    dynamicCls.event_name = "Lounges_Booked";
                                                    dynamicCls.relation_reference = Convert.ToString(lobjMemberDetails.MemberRelationsList.Find(lobj => lobj.RelationType.Equals(RelationType.LBMS)).RelationReference);
                                                    dynamicCls.program_id = Convert.ToInt32(lobjMemberDetails.ProgramId); ;
                                                    dynamicCls.to_email = lobjMemberDetails.Email;
                                                    dynamicCls.full_name = lobjMemberDetails.FullName;
                                                    dynamicCls.VoucherImg = lobjProduct.PrimaryImage.Url;
                                                    dynamicCls.voucher_details = lstrHtml;
                                                    dynamicCls.termsAndConditions = tnc;
                                                    dynamicCls.to_mobile = lobjMemberDetails.MobileNumber;
                                                    Dictionary<string, dynamic> lobjDictionary = new Dictionary<string, dynamic>();
                                                    IDictionary<string, object> dict = (IDictionary<string, object>)dynamicCls;
                                                    foreach (var key in dict)
                                                    {
                                                        lobjDictionary.Add(key.Key, key.Value);
                                                    }
                                                    string jsonParameters = JsonConvert.SerializeObject(lobjDictionary);
                                                    lobjModel.SendEmails(jsonParameters, lobjMemberDetails);
                                                    // lobjModel.SendEmail(lstrEmailParameters, lobjMemberDetails, "LoungeVoucher");
                                                }
                                            }
                                            catch (Exception ex)
                                            {
                                                LoggingAdapter.WriteLog("OrderNotification.aspx|lounge|exception: " + ex.Message + Environment.NewLine + ex.StackTrace);
                                            }
                                        }
                                        else if (lstrDigitalProductType.ToLower().Equals("topup"))
                                        {
                                            TopUpDetails topUpDetails = null;
                                            try
                                            {
                                                LoggingAdapter.WriteLog("OrderNotification Check for ResponseMetas");
                                                topUpDetails = JsonConvert.DeserializeObject<Giift.ShopGateway.Client.Entities.TopUpDetails>(customerOrder.Shipments[0].DynamicProperties.Find(lobj => lobj.Name.Equals("ResponseMetas")).Values[0].Value);
                                                LoggingAdapter.WriteLog("OrderNotification ResponseMetas found");
                                                string CustomerName = string.Empty;
                                                if (topUpDetails != null)
                                                {
                                                    List<string> lstrEmailParameters = new List<string>();
                                                    lstrEmailParameters.Add(lobjProduct.PrimaryImage.Url);//0
                                                    string lstrHtml = string.Empty;
                                                    if (topUpDetails.Error != null)
                                                    {
                                                        lstrHtml = "<p><strong>Topup purchased</strong></p>";
                                                    }
                                                    else
                                                    {
                                                        lstrHtml = "<p><strong>" + topUpDetails.Error.Message + "</strong></p>";
                                                    }
                                                    LoggingAdapter.WriteLog(lstrHtml);
                                                    lstrEmailParameters.Add(lstrHtml);//1
                                                    var tncProp = lobjProduct.Properties.ToList().Find(lobj => lobj.Name.Equals("TermsAndCondition"));
                                                    string tnc = "";
                                                    if (tncProp != null && (!string.IsNullOrEmpty(tncProp.Value) || tncProp.LocalizedValues != null))
                                                    {
                                                        tnc = !string.IsNullOrEmpty(tncProp.Value) ? tncProp.Value : tncProp.LocalizedValues.ToList()[0].Value;
                                                        lstrEmailParameters.Add(tnc);//2
                                                    }
                                                    else
                                                    {
                                                        lstrEmailParameters.Add("");//2
                                                    }
                                                    lstrEmailParameters.Add(lobjMemberDetails.FullName);//3
                                                    dynamic dynamicCls = new System.Dynamic.ExpandoObject();
                                                    dynamicCls.event_name = "TopUpVoucher";
                                                    dynamicCls.relation_reference = Convert.ToString(lobjMemberDetails.MemberRelationsList.Find(lobj => lobj.RelationType.Equals(RelationType.LBMS)).RelationReference);
                                                    dynamicCls.program_id = Convert.ToInt32(lobjMemberDetails.ProgramId); ;
                                                    dynamicCls.to_email = lobjMemberDetails.Email;
                                                    dynamicCls.full_name = lobjMemberDetails.FullName;
                                                    dynamicCls.VoucherImg = lobjProduct.PrimaryImage.Url;
                                                    dynamicCls.voucher_details = lstrHtml;
                                                    dynamicCls.termsAndConditions = tnc;
                                                    dynamicCls.to_mobile = lobjMemberDetails.MobileNumber;
                                                    Dictionary<string, dynamic> lobjDictionary = new Dictionary<string, dynamic>();
                                                    IDictionary<string, object> dict = (IDictionary<string, object>)dynamicCls;
                                                    foreach (var key in dict)
                                                    {
                                                        lobjDictionary.Add(key.Key, key.Value);
                                                    }
                                                    string jsonParameters = JsonConvert.SerializeObject(lobjDictionary);
                                                    lobjModel.SendEmails(jsonParameters, lobjMemberDetails);
                                                    // lobjModel.SendEmail(lstrEmailParameters, lobjMemberDetails, "TopUpVoucher");
                                                }
                                            }
                                            catch (Exception ex)
                                            {
                                                LoggingAdapter.WriteLog("OrderNotification.aspx|topup|exception: " + ex.Message + Environment.NewLine + ex.StackTrace);
                                            }
                                        }
                                        else if (lstrDigitalProductType.ToLower().Equals("offers"))
                                        {
                                            GiftCardDetails giftCardDetails = null;
                                            try
                                            {
                                                LoggingAdapter.WriteLog("OrderNotification Check for ResponseMetas");
                                                giftCardDetails = JsonConvert.DeserializeObject<GiftCardDetails>(customerOrder.Shipments[0].DynamicProperties.Find(lobj => lobj.Name.Equals("ResponseMetas")).Values[0].Value);
                                                LoggingAdapter.WriteLog("OrderNotification ResponseMetas found");
                                                if (giftCardDetails != null)
                                                {
                                                    List<string> lstrEmailParameters = new List<string>();
                                                    lstrEmailParameters.Add(lobjProduct.PrimaryImage.Url);//0
                                                    string lstrHtml = string.Empty;
                                                    if (!string.IsNullOrEmpty(giftCardDetails.GiftCardInfo[0].Code))
                                                    {
                                                        lstrHtml += "<tr>";

                                                        if (!string.IsNullOrEmpty(giftCardDetails.GiftCardInfo[0].RedirectionUrl))
                                                        {
                                                            lstrHtml += "<td width=\"15%\" height=\"35\" bgcolor=\"#006677\"><p style=\"color:#fff;\"><strong>RedirectionUrl</strong></p></td>";
                                                        }
                                                        lstrHtml += "</tr>";
                                                        for (int i = 0; i < giftCardDetails.GiftCardInfo.Count; i++)
                                                        {
                                                            string ImageUrl = string.Empty;
                                                            lstrHtml += "<tr>";
                                                            if (!string.IsNullOrEmpty(giftCardDetails.GiftCardInfo[i].RedirectionUrl))
                                                            {
                                                                lstrHtml += "<td width=\"15%\" height=\"25\" bgcolor=\"#FFFFFF\">for detail please click : <a href=\"" + giftCardDetails.GiftCardInfo[i].RedirectionUrl + "\" target=\"_blank\" style=\"font-family: Helvetica, Arial, sans-serif;font-size: 13px; color: #000000;display: inline-block;margin-left\">" + "here" + "</a></td>";
                                                            }
                                                            lstrHtml += "</tr>";
                                                        }
                                                        LoggingAdapter.WriteLog(lstrHtml);
                                                    }
                                                    else if (!string.IsNullOrEmpty(giftCardDetails.GiftCardInfo[0].RedirectionUrl))
                                                    {
                                                        // lstrHtml = "Your Voucher Details is Sent on your Registered Email ID/Mobile";
                                                        lstrHtml += "<tr>";
                                                        if (!string.IsNullOrEmpty(giftCardDetails.GiftCardInfo[0].RedirectionUrl))
                                                        {
                                                            lstrHtml += "<td width=\"15%\" height=\"35\" bgcolor=\"#006677\"><p style=\"color:#fff;\"><strong>Gift Card Redemption URL</strong></p></td>";
                                                        }

                                                        lstrHtml += "</tr>";

                                                        lstrHtml += "<tr>";
                                                        if (!string.IsNullOrEmpty(giftCardDetails.GiftCardInfo[0].RedirectionUrl))
                                                        {
                                                            lstrHtml += "<td width=\"15%\" height=\"25\" bgcolor=\"#FFFFFF\"><p>" + giftCardDetails.GiftCardInfo[0].RedirectionUrl + "</p></td>";
                                                        }
                                                        lstrHtml += "</tr>";

                                                        LoggingAdapter.WriteLog(lstrHtml);
                                                    }
                                                    else
                                                    {
                                                        lstrHtml = "Your Voucher Details is Sent on your Registered Email ID/Mobile";
                                                    }
                                                    lstrEmailParameters.Add(lstrHtml);//1
                                                    var tncProp = lobjProduct.Properties.ToList().Find(lobj => lobj.Name.Equals("TermsAndCondition"));
                                                    string tnc = "";
                                                    if (tncProp != null && (!string.IsNullOrEmpty(tncProp.Value) || tncProp.LocalizedValues != null))
                                                    {
                                                        tnc = !string.IsNullOrEmpty(tncProp.Value) ? tncProp.Value : tncProp.LocalizedValues.ToList()[0].Value;
                                                        lstrEmailParameters.Add(tnc);//2
                                                    }
                                                    else
                                                    {
                                                        lstrEmailParameters.Add("");//2
                                                    }
                                                    lstrEmailParameters.Add(lobjMemberDetails.FullName);//3
                                                    string emailparalogo = string.Empty;
                                                    foreach (var it in VendorId.Select((x, i) => new { Value = x, Index = i }))
                                                    {
                                                        //foreach (var item in Vendorlogo.Select((y, j) => new { Value = y, Index = j }))
                                                        //{
                                                        //    if (it.Index == item.Index)
                                                        //    {
                                                        if (lobjProduct.VendorId.Contains(it.Value))
                                                        {
                                                            emailparalogo = "<tr><td align=\"left\" valign=\"top\" colspan=\"3\"><table cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" border=\"0\"><tbody><tr><td align=\"left\" valign=\"top\" width=\"100%\"><table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\"><tbody><tr><td style=\"text-align:center\" align=\"center\"><img width=\"133\" border=\"0\" height=\"156\" alt=\"\" src=\"" + Vendorlogo + "\" style=\"display:block;border:none;outline:0;text-decoration:none;display:block;margin-left:auto;margin-right:auto\"></td></tr></tbody></table></td></tr></tbody></table></td></tr>";//4
                                                        }

                                                        //    }
                                                        //}

                                                    }
                                                    lstrEmailParameters.Add(emailparalogo);
                                                    dynamic dynamicCls = new System.Dynamic.ExpandoObject();
                                                    dynamicCls.event_name = "Giift_Card_Redemption";
                                                    dynamicCls.relation_reference = Convert.ToString(lobjMemberDetails.MemberRelationsList.Find(lobj => lobj.RelationType.Equals(RelationType.LBMS)).RelationReference);
                                                    dynamicCls.program_id = Convert.ToInt32(lobjMemberDetails.ProgramId); ;
                                                    dynamicCls.to_email = lobjMemberDetails.Email;
                                                    dynamicCls.full_name = lobjMemberDetails.FullName;
                                                    dynamicCls.VoucherImg = lobjProduct.PrimaryImage.Url;
                                                    dynamicCls.voucher_details = lstrHtml;
                                                    dynamicCls.termsAndConditions = tnc;
                                                    dynamicCls.logo = emailparalogo;
                                                    dynamicCls.to_mobile = lobjMemberDetails.MobileNumber;
                                                    Dictionary<string, dynamic> lobjDictionary = new Dictionary<string, dynamic>();
                                                    IDictionary<string, object> dict = (IDictionary<string, object>)dynamicCls;
                                                    foreach (var key in dict)
                                                    {
                                                        lobjDictionary.Add(key.Key, key.Value);
                                                    }
                                                    string jsonParameters = JsonConvert.SerializeObject(lobjDictionary);
                                                    lobjModel.SendEmails(jsonParameters, lobjMemberDetails);
                                                    // lobjModel.SendEmail(lstrEmailParameters, lobjMemberDetails, "GiftVoucher");
                                                }
                                            }
                                            catch (Exception ex)
                                            {
                                                LoggingAdapter.WriteLog("OrderNotification.aspx|giftcard|exception: " + ex.Message + Environment.NewLine + ex.StackTrace);
                                            }

                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("OrderNotification.aspx Pageload Ex-:" + ex.Message + ex.InnerException + ex.StackTrace);
            }
        }
    }
}