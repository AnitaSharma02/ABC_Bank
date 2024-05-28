using Core.Platform.Helper.ProgramName;
using Core.Platform.Member.Entites;
using Core.Platform.MemberActivity.Constants;
using Core.Platform.MemberActivity.Entities;
using Core.Platform.ProgramMaster.Entities;
using Framework.EnterpriseLibrary.Adapters;
using Holibob.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.Globalization;
using ABC.Model;

public partial class ExperienceProductStatus : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                divMessage.InnerHtml = "";
                if (Request.QueryString["Success"] != null)
                {
                    bool lblnStatus = Convert.ToBoolean(Request.QueryString["Success"]);
                    if (lblnStatus)
                    {
                        divMessage.InnerHtml += "<h4 class='giift_card_purchase_success_congratulations'>Congratulations!</h4><br/><br/><h4>"
                            + "<div class='giift_card_purchase_success_note_1'>Your booking is successful and the booking details will be emailed to your registered mail id shortly.</div><br/>";
                        SendExperienceEmail();
                    }
                    else
                    {
                        divMessage.InnerHtml = "<div class='giift_card_purchase_failure'>Card Buzz could not process your request.</div><div class='LangSwitch_try_again'><a href=\"Index.aspx\" target=\"_self\">Please try again</a><div>";
                    }
                }
                else
                {
                    divMessage.InnerHtml = "<div class='giift_card_purchase_failure'>Card Buzz could not process your request.</div><div class='LangSwitch_try_again'><a href=\"Index.aspx\" target=\"_self\">Please try again</a></div>";
                }
            }
        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("ExperienceProductStatus.aspx Pageload Exception: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
        }
    }
    public void SendExperienceEmail()
    {
        try
        {
            MemberDetails lobjMemberDetails = Session["MemberDetails"] as MemberDetails;
            if (lobjMemberDetails != null)
            {
                string lstrBookId = Convert.ToString(Session["PackageBookingId"]);
                Session["PackageBookingId"] = null;
                string lstrOrderStatusResponse = GetOrderStatus(lstrBookId);
                HolibobOrderStatus lobjOrderStatus = JsonConvert.DeserializeObject<HolibobOrderStatus>(lstrOrderStatusResponse);
                TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
                List<string> lstrEmailParameters = new List<string>
                {
                    lobjOrderStatus.data.code,//0
                    textInfo.ToTitleCase(lobjOrderStatus.data.leadPassengerName.ToLower()),//1
                    lobjOrderStatus.data.rawData.data.booking.availabilityList.nodes[0].product.name//2
                };

                StringBuilder lsbPersonListHtmlContent = new StringBuilder();
                List<PersonListNode> llstobjPersonListNode = new List<PersonListNode>();
                int m = 0;
                for (int i = 0; i < lobjOrderStatus.data.rawData.data.booking.availabilityList.nodes[0].personList.nodes.Count; i++)
                {
                    PersonListNode lobjPersonListNode = new PersonListNode();
                    if (llstobjPersonListNode.FindAll(x => x.pricingCategoryLabel.ToLower() == lobjOrderStatus.data.rawData.data.booking.availabilityList.nodes[0].personList.nodes[i].pricingCategoryLabel.ToLower()).Count == 0)
                    {
                        m = 1;
                        lobjPersonListNode.pricingCategoryLabel = lobjOrderStatus.data.rawData.data.booking.availabilityList.nodes[0].personList.nodes[i].pricingCategoryLabel;
                        lobjPersonListNode.count = m;
                        string lstrLabelName = string.Empty;
                        for (var o = 0; o < lobjOrderStatus.data.rawData.data.booking.availabilityList.nodes[0].personList.nodes[i].questionList.nodes.Count; o++)
                        {
                            lstrLabelName += lobjOrderStatus.data.rawData.data.booking.availabilityList.nodes[0].personList.nodes[i].questionList.nodes[o].label + ",";
                        }
                        lstrLabelName = lstrLabelName.TrimEnd(',');
                        lobjPersonListNode.labelName = lstrLabelName;
                        llstobjPersonListNode.Add(lobjPersonListNode);
                    }
                    else if (llstobjPersonListNode.FindAll(x => x.pricingCategoryLabel.ToLower() == lobjOrderStatus.data.rawData.data.booking.availabilityList.nodes[0].personList.nodes[i].pricingCategoryLabel.ToLower()).Count > 0)
                    {
                        llstobjPersonListNode.FindAll(x => x.pricingCategoryLabel.ToLower() == lobjOrderStatus.data.rawData.data.booking.availabilityList.nodes[0].personList.nodes[i].pricingCategoryLabel.ToLower())[0].count++;
                    }
                }
                lsbPersonListHtmlContent.Append("<tr bgcolor=\"#FFFFFF\"  width=\"100%\">");
                for (int n = 0; n < llstobjPersonListNode.Count; n++)
                {
                    lsbPersonListHtmlContent.Append("<td bgcolor=\"#FFFFFF\" style=\"font-family: Arial; font-size: 13px;\">" + llstobjPersonListNode[n].pricingCategoryLabel + " : " + Convert.ToString(llstobjPersonListNode[n].count) + "</td>");
                }
                lsbPersonListHtmlContent.Append("</tr>");
                lsbPersonListHtmlContent.Append("<tr bgcolor=\"#FFFFFF\"  width=\"100%\">");
                lsbPersonListHtmlContent.Append("<td bgcolor=\"#FFFFFF\" style=\"font-family: Arial; font-size: 13px; font-weight: bold;\"> TOTAL " + lobjOrderStatus.data.rawData.data.booking.availabilityList.nodes[0].totalPrice.grossFormattedText + "</td>");
                lsbPersonListHtmlContent.Append("</tr>");
                lstrEmailParameters.Add(Convert.ToString(lsbPersonListHtmlContent));//3

                lstrEmailParameters.Add(Convert.ToDateTime(lobjOrderStatus.data.rawData.data.booking.availabilityList.nodes[0].date).ToString("dddd dd MMMM yyyy"));//4

                StringBuilder lsbQuestionListHtmlContent = new StringBuilder();
                lsbQuestionListHtmlContent.Append("<tbody>");
                int p = 0;
                for (int j = 0; j < lobjOrderStatus.data.questionList.Count; j++)
                {
                    if (lobjOrderStatus.data.questionList[j].properties.ToLower() == "bookingavailabilityquestions" || lobjOrderStatus.data.questionList[j].properties.ToLower() == "bookingquestions")
                    {
                        lsbQuestionListHtmlContent.Append("<tr>");
                        lsbQuestionListHtmlContent.Append("<td colspan=\"2\" style=\"font-family: Arial; font-size:13px;\">" + lobjOrderStatus.data.questionList[j].label + ":</td>");
                        lsbQuestionListHtmlContent.Append("<td colspan=\"2\" style=\"font-family:Arial; font-size:13px; font-weight: bold;\">" + lobjOrderStatus.data.questionList[j].answerValue + "</td>");
                    }
                    else if (lobjOrderStatus.data.questionList[j].properties.ToLower() == "bookingpersonquestions")
                    {
                        if (lobjOrderStatus.data.questionList[j].pricingCategoryLabel != lobjOrderStatus.data.questionList[j - 1].pricingCategoryLabel)
                        {
                            p++;
                            lsbQuestionListHtmlContent.Append("<tr><td colspan=\"2\" width=\"21%\" height=\"30\" style=\"font-family: Arial; font-size:13px;font-weight: bold;\">Participants " + p + ": " + lobjOrderStatus.data.questionList[j].pricingCategoryLabel + "</td></tr>");
                        }
                        else if (lobjOrderStatus.data.questionList[j].pricingCategoryLabel == lobjOrderStatus.data.questionList[j - 1].pricingCategoryLabel)
                        {
                            for (var q = 0; q < llstobjPersonListNode.Count; q++)
                            {
                                if (llstobjPersonListNode[q].pricingCategoryLabel == lobjOrderStatus.data.questionList[j].pricingCategoryLabel)
                                {
                                    if (llstobjPersonListNode[q].labelName.Split(',')[0] == lobjOrderStatus.data.questionList[j].label)
                                    {
                                        p++;
                                        lsbQuestionListHtmlContent.Append("<tr><td colspan=\"2\" width=\"21%\" height=\"30\" style=\"font-family: Arial; font-size:13px;font-weight: bold;\">Participants " + p + ": " + lobjOrderStatus.data.questionList[j].pricingCategoryLabel + "</td></tr>");
                                    }
                                }
                            }
                        }
                        lsbQuestionListHtmlContent.Append("<tr>");
                        lsbQuestionListHtmlContent.Append("<td colspan=\"2\" style=\"font-family: Arial; font-size:13px;\">" + lobjOrderStatus.data.questionList[j].label + ":</td>");
                        lsbQuestionListHtmlContent.Append("<td colspan=\"2\" style=\"font-family:Arial; font-size:13px; font-weight: bold;\">" + lobjOrderStatus.data.questionList[j].answerValue + "</td>");
                    }
                }
                lsbQuestionListHtmlContent.Append("</tbody>");
                lstrEmailParameters.Add(Convert.ToString(lsbQuestionListHtmlContent));//5

                StringBuilder lsbOptionListHtmlContent = new StringBuilder();
                lsbOptionListHtmlContent.Append("<tbody>");
                for (int l = 0; l < lobjOrderStatus.data.rawData.data.booking.availabilityList.nodes[0].optionList.nodes.Count; l++)
                {
                    lsbOptionListHtmlContent.Append("<tr>");
                    lsbOptionListHtmlContent.Append("<td colspan=\"2\" style=\"font-family:Arial; font-size:13px;\">" + lobjOrderStatus.data.rawData.data.booking.availabilityList.nodes[0].optionList.nodes[l].label + ":</td>");
                    lsbOptionListHtmlContent.Append("<td colspan=\"2\" style=\"font-family:Arial; font-size: 13px; font-weight: bold;\">" + lobjOrderStatus.data.rawData.data.booking.availabilityList.nodes[0].optionList.nodes[l].answerFormattedText + "</td>");
                    lsbOptionListHtmlContent.Append("</tr>");
                }
                lsbOptionListHtmlContent.Append("</tbody>");
                lstrEmailParameters.Add(Convert.ToString(lsbOptionListHtmlContent));//6

                StringBuilder lsbCancellationPolicyHtmlContent = new StringBuilder();
                if (lobjOrderStatus.data.rawData.data.booking.availabilityList.nodes[0].product.cancellationPolicy.isCancellable)
                {
                    for (int k = 0; k < lobjOrderStatus.data.rawData.data.booking.availabilityList.nodes[0].product.cancellationPolicy.penaltyList.nodes.Count; k++)
                    {
                        lsbCancellationPolicyHtmlContent.Append("<p>• ");
                        lsbCancellationPolicyHtmlContent.Append(lobjOrderStatus.data.rawData.data.booking.availabilityList.nodes[0].product.cancellationPolicy.penaltyList.nodes[k].formattedText + " <br/>"); ;
                        lsbCancellationPolicyHtmlContent.Append("</p>");
                    }
                }
                else
                {
                    lsbCancellationPolicyHtmlContent.Append("<p>No Cancellation Policy.</p>");
                }
                lstrEmailParameters.Add(Convert.ToString(lsbCancellationPolicyHtmlContent));//7

                ABCModel lobjModel = new ABCModel();
                dynamic dynamicCls = new System.Dynamic.ExpandoObject();
                dynamicCls.event_name = "Package_Booking";
                dynamicCls.relation_reference = Convert.ToString(lobjMemberDetails.MemberRelationsList.Find(lobj => lobj.RelationType.Equals(RelationType.LBMS)).RelationReference);
                dynamicCls.program_id = Convert.ToInt32(lobjMemberDetails.ProgramId); ;
                dynamicCls.to_email = lobjMemberDetails.Email;
                dynamicCls.full_name = lobjMemberDetails.FullName;
                dynamicCls.BookingRef = lobjOrderStatus.data.code;
                dynamicCls.PassengerName = textInfo.ToTitleCase(lobjOrderStatus.data.leadPassengerName.ToLower());
                dynamicCls.ProductName = lobjOrderStatus.data.rawData.data.booking.availabilityList.nodes[0].product.name;
                dynamicCls.Personlistdetails = Convert.ToString(lsbPersonListHtmlContent);
                dynamicCls.BookingDate = Convert.ToDateTime(lobjOrderStatus.data.rawData.data.booking.availabilityList.nodes[0].date).ToString("dddd dd MMMM yyyy");
                dynamicCls.QuestionList = Convert.ToString(lsbQuestionListHtmlContent);
                dynamicCls.OptionList = Convert.ToString(lsbOptionListHtmlContent);
                dynamicCls.CancellationPolicy = Convert.ToString(lsbCancellationPolicyHtmlContent);
                dynamicCls.to_mobile = lobjMemberDetails.MobileNumber;
                dynamicCls.CreditsConsumed = lobjOrderStatus.data.rawData.data.booking.availabilityList.nodes[0].totalPrice.grossFormattedText;
                Dictionary<string, dynamic> lobjDictionary = new Dictionary<string, dynamic>();
                IDictionary<string, object> dict = (IDictionary<string, object>)dynamicCls;
                foreach (var key in dict)
                {
                    lobjDictionary.Add(key.Key, key.Value);
                }
                string jsonParameters = JsonConvert.SerializeObject(lobjDictionary);
                lobjModel.SendEmails(jsonParameters, lobjMemberDetails);
               // lobjModel.SendEmail(lstrEmailParameters, lobjMemberDetails, "PackageBooked");
            }
        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("ExperienceProductStatus SendExperienceEmail Exception: " + ex.Message + Environment.NewLine + ex.StackTrace);
        }
    }
    public class PersonListNode
    {
        public string pricingCategoryLabel { get; set; }
        public int count { get; set; }
        public string labelName { get; set; }
    }
    public string GetOrderStatus(string pstrBookId)
    {
        string lstrResponse = string.Empty;
        StringBuilder lNICogRequestResponse = new StringBuilder();
        ABCModel lobjModel = new ABCModel();
        try
        {
            MemberDetails lobjMemberDetails = Session["MemberDetails"] as MemberDetails;
            if (lobjMemberDetails != null && !string.IsNullOrEmpty(lobjMemberDetails.FullName))
            {
                lNICogRequestResponse.Append(string.Format("GetOrderStatus Request: pstrBookId - {0}", pstrBookId));
                OrderStatusResponse lobjOrderStatusResponse = lobjModel.GetOrderStatusByBookingId(pstrBookId);
                lNICogRequestResponse.Append(string.Format(" GetOrderStatus Response: {0}", JsonConvert.SerializeObject(lobjOrderStatusResponse)));
                if (lobjOrderStatusResponse != null
                    && lobjOrderStatusResponse.data != null
                    && !string.IsNullOrEmpty(lobjOrderStatusResponse.data.getOrderStatus))
                {
                    HolibobOrderStatus lobjOrderStatus = JsonConvert.DeserializeObject<HolibobOrderStatus>(lobjOrderStatusResponse.data.getOrderStatus);
                    lobjModel.LogActivity(string.Format("GetOrderStatus; ActivityType-:{0}; BookingId-:{1}; Response-: {2};", ActivityConstants.BookPackage, pstrBookId, lobjOrderStatus.status.ToLower()), ActivityType.PackageBooking);
                    if (lobjOrderStatus != null && lobjOrderStatus.data != null && lobjOrderStatus.status.ToLower() == "success")
                    {
                        string lstrCurrency = lobjModel.GetDefaultCurrency();
                        string lstrProgramName = ProgramHelper.ProgramName();
                        ProgramDefinition lobjProgramDefinition = lobjModel.GetProgramDetails(lstrProgramName);
                        if (lobjOrderStatus.data.rawData.data.booking.availabilityList.nodes != null)
                        {
                            foreach (var lobjAvailabilityListNode in lobjOrderStatus.data.rawData.data.booking.availabilityList.nodes)
                            {
                                string pstrGrossFormattedText = string.Empty;
                                lobjAvailabilityListNode.totalPrice.gross = lobjModel.ConvertToPointsOrCurrency(lobjAvailabilityListNode.totalPrice.gross, lstrCurrency, lobjProgramDefinition.ProgramId, out pstrGrossFormattedText);
                                lobjAvailabilityListNode.totalPrice.grossFormattedText = pstrGrossFormattedText;
                                foreach (var lobjPersonListNode in lobjAvailabilityListNode.personList.nodes)
                                {
                                    string pstrFormattedText = string.Empty;
                                    lobjPersonListNode.totalPrice = lobjModel.ConvertToPointsOrCurrency(lobjPersonListNode.totalPrice, lstrCurrency, lobjProgramDefinition.ProgramId, out pstrFormattedText);
                                }
                            }
                            lobjOrderStatusResponse.data.getOrderStatus = JsonConvert.SerializeObject(lobjOrderStatus);
                        }
                        lstrResponse = JsonConvert.SerializeObject(lobjOrderStatus);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("ExperienceProductBooking GetOrderStatus Exception: " + ex.Message + Environment.NewLine + ex.StackTrace);
        }
        finally
        {
            lobjModel.LogActivity(string.Format("GetOrderStatus; ActivityType: {0}; - RequestResponse: {1};", ActivityConstants.BookPackage, lNICogRequestResponse), ActivityType.PackageBooking);
        }
        return lstrResponse;
    }
}