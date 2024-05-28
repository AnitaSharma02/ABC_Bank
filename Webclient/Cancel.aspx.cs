using Core.Platform.Member.Entites;
using Core.WebAPI.ClientHelper;
using Framework.EnterpriseLibrary.Adapters;
using GiiftShopGateway.Model;
using ABC.Model;
using Stripe;
using Stripe.Checkout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Cancel : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //if (Request.QueryString["session_id"] != null || Request.QueryString["session_id "] != string.Empty)
            //{
            //    var url = Request.QueryString["session_id"];
            //    var parsedUrl = url.Split('?')[0];
            //    var paramsCollection = HttpUtility.ParseQueryString(parsedUrl);
            //    string session_id = paramsCollection[0].ToString();

                SuccessPayment();
            //}
        }
    }

    private void SuccessPayment()
    {
        string lstrClientReferenceId = string.Empty;
        string lstrBookingFlag = string.Empty;
        try
        {
            PaymentGatewayDetails lobjTransactionDetails = null;
            bool lblUpdateStatus = false;
            StripePaymentDetails lobjStripePaymentDetails = Session["StripePaymentDetails"] as StripePaymentDetails;
            // var sessionService = new SessionService();
            //Session session = sessionService.Get(session_id);
            //string status = session.PaymentStatus;
            //string checkoutstatus = session.Status;
            //string transactionId = session.PaymentIntentId;
            //StripeResponse striperesponse = session.StripeResponse;
            lstrClientReferenceId = lobjStripePaymentDetails.ClientReferenceId;

           // LoggingAdapter.WriteLog(string.Format("Stripe Cancel Response TxnId -:{0}; PaymentStatus-:{1}; Status-:{2}; ClientReferenceId-:{3} ", transactionId, status, checkoutstatus, lstrClientReferenceId));

            List<object> lobjDict = CachingAdapter.Get(lstrClientReferenceId) as List<object>;
            Session["StripePaymentDetails"] = lobjDict[1] as StripePaymentDetails;

            LoggingAdapter.WriteLog("PaymentResponse PaymentStatus Failed. Rollback redeem points start");
            ABCModel lobjModel = new ABCModel();
            MemberDetails lobjMemberDetails = HttpContext.Current.Session["MemberDetails"] as MemberDetails;

            if (!string.IsNullOrEmpty(lobjStripePaymentDetails.RedeemMilesResponse) && lobjStripePaymentDetails.ReqRedeemPoint != "0")
            {
                lobjModel.RollBackTransaction(lobjStripePaymentDetails.RedeemMilesResponse, lobjMemberDetails.MemberRelationsList.Find(lobj => lobj.RelationType.Equals(RelationType.LBMS)).RelationReference, "Redemption Merchant");
            }          
        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("PaymentResponse SuccessPayment Ex-:" + ex.InnerException + ex.StackTrace + ex.Message);
        }
    }
}