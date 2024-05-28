using Core.Platform.Helper.ProgramName;
using Core.Platform.MemberActivity.Constants;
using Core.Platform.MemberActivity.Entities;
using Core.Platform.ProgramMaster.Entities;
using Framework.EnterpriseLibrary.Adapters;
using Holibob.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Services;
using System.Web.UI;
using ABC.Model;
using Core.Platform.Member.Entites;
using System.Web;
using GiiftShopGateway.Model;
using CB.IBE.Platform.AirClientModel;

public partial class ExperienceProductDetails : Page
{
    string lstrProductId = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            lstrProductId = Convert.ToString(Request.QueryString["Id"]);
            ABCModel lobjModel = new ABCModel();
            MemberDetails lobjMemberDetails = Session["MemberDetails"] as MemberDetails;
            if (lobjMemberDetails != null)
            {
                if (string.IsNullOrEmpty(lobjMemberDetails.Email))
                {
                    divEmailErrorMsg.Attributes.Add("style", "display:block");
                    divEmailErrorMsg.InnerHtml = "<p style=\"color:red\">You cannot proceed for redemption since there is no email address updated , kindly contact bank to update the email address.</p>";
                }
                else
                {
                    divEmailErrorMsg.Attributes.Add("style", "display:none");
                    divEmailErrorMsg.InnerHtml = "";
                }
                if (string.IsNullOrEmpty(lstrProductId))
                {
                    Response.Redirect("ExperienceProductList.aspx", false);
                }
                else
                {
                    lobjModel.LogActivity(string.Format("Visit Experience Product Details; ProductId: {0};", lstrProductId), ActivityType.PageLoad);
                }
            }
            else
            {
                Response.Redirect("Login.aspx", false);
            }
        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("ExperienceProductDetails.aspx Pageload Exception: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
        }
    }
    public static DateTime GetLastDayOfMonth(DateTime value)
    {
        return value.Date.AddDays(DateTime.DaysInMonth(value.Year, value.Month) - value.Day);
    }
    [WebMethod]
    public static string GetExperienceProductDetails(string pstrProductId)
    {
        string lstrResponse = string.Empty;
        ABCModel lobjModel = new ABCModel();
        StringBuilder lNICogRequestResponse = new StringBuilder();
        try
        {
            MemberDetails lobjMemberDetails = HttpContext.Current.Session["MemberDetails"] as MemberDetails;
            lNICogRequestResponse.Append(string.Format("GetExperienceProductInfo Request: pstrProductId - {0}", pstrProductId));
            ProductInfoResponse lstrProductInfoResponse = lobjModel.GetExperienceProductInfo(pstrProductId);
            lNICogRequestResponse.Append(string.Format(" GetExperienceProductInfo Response: {0}", JsonConvert.SerializeObject(lstrProductInfoResponse)));
            StringBuilder lsbExperienceProductList = new StringBuilder();
            if (lstrProductInfoResponse.data != null && !string.IsNullOrEmpty(lstrProductInfoResponse.data.getProductInfo))
            {
                ProductInfo lobjProductInfo = JsonConvert.DeserializeObject<ProductInfo>(lstrProductInfoResponse.data.getProductInfo);
                if (lobjProductInfo != null && lobjProductInfo.rawData != null && lobjProductInfo.status.ToLower() == "success")
                {
                    lobjModel.LogActivity(string.Format("GetExperienceProductInfo; ProductId:{0}; Status:{1};", pstrProductId, "Success"), ActivityType.PackageBooking);
                    lstrResponse = JsonConvert.SerializeObject(lobjProductInfo.rawData);
                }
                else
                {
                    lobjModel.LogActivity(string.Format("GetExperienceProductInfo; ProductId:{0}; Status:{1};", lobjMemberDetails.MemberRelationsList[0].RelationReference, pstrProductId, "Failed"), ActivityType.PackageBooking);
                }
            }
        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("ExperienceProductDetails GetExperienceProductDetails Exception: " + ex.Message + Environment.NewLine + ex.StackTrace);
        }
        finally
        {
            LoggingAdapter.WriteLog("ExperienceProductDetails GetExperienceProductDetails RequestResponse: " + lNICogRequestResponse);
        }
        return lstrResponse;
    }

    [WebMethod]
    public static string[] GetExperienceProductStatus(string pstrProductId, string pstrStartDate, bool pblnCheckNextMonthAvailabilityIfNotFound = false, string pstrAvailabilityType = "")
    {
        string[] lstrResponse = new string[5];
        StringBuilder lNICogRequestResponse = new StringBuilder();
        ABCModel lobjModel = new ABCModel();
        MemberDetails lobjMemberDetails = HttpContext.Current.Session["MemberDetails"] as MemberDetails;
        try
        {
            string pstrEndDate = string.Empty;
            for (int i = 0; i < 12; i++)
            {
                if (i == 0)
                {
                    pstrEndDate = GetLastDayOfMonth(Convert.ToDateTime(pstrStartDate)).ToString("yyyy-MM-dd");
                    lstrResponse[0] = "<p class=\"hint\"><i class=\"bi bi-exclamation-circle-fill\"></i><span> No availability was found between </span>" + Convert.ToDateTime(pstrStartDate).ToString("dd MMMM yyyy") + "";
                }
                else
                {
                    pstrStartDate = new DateTime(Convert.ToDateTime(pstrStartDate).AddMonths(1).Year, Convert.ToDateTime(pstrStartDate).AddMonths(1).Month, 1).ToString("yyyy-MM-dd");
                    pstrEndDate = GetLastDayOfMonth(Convert.ToDateTime(pstrStartDate)).ToString("yyyy-MM-dd");
                }
                lstrResponse[2] = pstrStartDate;
                lNICogRequestResponse.Append(string.Format("GetExperienceProductStatus Request: pstrProductId - {0}, pstrStartDate - {1}, pstrEndDate - {2}", pstrProductId, pstrStartDate, pstrEndDate));
                ProductStatusResponse lobjProductStatusResponse = lobjModel.GetExperienceProductStatus(pstrProductId, pstrStartDate, pstrEndDate, pstrAvailabilityType);
                lNICogRequestResponse.Append(string.Format(" GetExperienceProductStatus Response: {0}", JsonConvert.SerializeObject(lobjProductStatusResponse)));
                if (lobjProductStatusResponse != null && lobjProductStatusResponse.data != null && !string.IsNullOrEmpty(lobjProductStatusResponse.data.getProductStatus))
                {
                    lobjModel.LogActivity(string.Format("GetExperienceProductStatus; Request:pstrProductId - {0};pstrStartDate - {1}; pstrEndDate - {2}; Status - {3};", pstrProductId, pstrStartDate, pstrEndDate, "Success"), ActivityType.PackageBooking);

                    if (lobjProductStatusResponse.availabilityType.ToLower() == "pass")
                    {
                        ProductStatus lobjProductStatus = JsonConvert.DeserializeObject<ProductStatus>(lobjProductStatusResponse.data.getProductStatus);
                        if (lobjProductStatus.data != null && lobjProductStatus.status.ToLower() == "success" && lobjProductStatus.data.Count > 0)
                        {
                            lstrResponse[0] = "<p class=\"hint\"><i class=\"bi bi-exclamation-circle-fill\"></i> <span>This product does not require travel date selection, therefore please select the number of guests for the booking below.</span></p>";
                            lstrResponse[1] = JsonConvert.SerializeObject(lobjProductStatus.data[0].id);
                            lstrResponse[3] = "false";
                            lstrResponse[4] = "pass";
                            break;
                        }
                    }
                    else if (lobjProductStatusResponse.availabilityType.ToLower() == "date")
                    {
                        ProductStatus lobjProductStatus = JsonConvert.DeserializeObject<ProductStatus>(lobjProductStatusResponse.data.getProductStatus);
                        if (lobjProductStatus.data != null && lobjProductStatus.status.ToLower() == "success" && lobjProductStatus.data.Count > 0)
                        {
                            lstrResponse[0] = string.Empty;
                            lstrResponse[1] = JsonConvert.SerializeObject(lobjProductStatus.data);
                            lstrResponse[3] = "true";
                            lstrResponse[4] = "date";
                            break;
                        }
                        else if (pblnCheckNextMonthAvailabilityIfNotFound && i == 12)
                        {
                            lstrResponse[0] += "- " + Convert.ToDateTime(pstrStartDate).AddMonths(1).ToString("dd MMMM yyyy") + "</p> <p class=\"hint\"><i class=\"bi bi-exclamation-circle-fill\"></i><span>Bookable until a day before start time</span></p>";
                            lstrResponse[1] = string.Empty;
                            lstrResponse[3] = "true";
                            lstrResponse[4] = "date";
                            break;
                        }
                        else if (lobjProductStatus.data != null
                            && lobjProductStatus.status.ToLower() == "success"
                            && lobjProductStatus.data.Count == 0
                            && !pblnCheckNextMonthAvailabilityIfNotFound)
                        {
                            lstrResponse[0] = string.Empty;
                            lstrResponse[1] = JsonConvert.SerializeObject(lobjProductStatus.data);
                            lstrResponse[3] = "true";
                            lstrResponse[4] = "date";
                            break;
                        }
                    }
                }
                else
                {
                    lobjModel.LogActivity(string.Format("GetExperienceProductStatus; Request:pstrProductId - {0}; pstrStartDate - {1}; pstrEndDate - {2}; Status - {3};", pstrProductId, pstrStartDate, pstrEndDate, "Failed"), ActivityType.PackageBooking);
                }
            }
        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("ExperienceProductDetails GetExperienceProductStatus Exception: " + ex.Message + Environment.NewLine + ex.StackTrace);
        }
        finally
        {
            LoggingAdapter.WriteLog("ExperienceProductDetails GetExperienceProductStatus RequestResponse: " + lNICogRequestResponse);
        }
        return lstrResponse;
    }
    [WebMethod]
    public static string FetchAvailability(string pstrAvailabilityId)
    {
        string lstrResponse = string.Empty;
        StringBuilder lNICogRequestResponse = new StringBuilder();
        ABCModel lobjModel = new ABCModel();
        MemberDetails lobjMemberDetails = HttpContext.Current.Session["MemberDetails"] as MemberDetails;
        try
        {
            lNICogRequestResponse.Append(string.Format("FetchAvailability Request: pstrAvailabilityId - {0}", JsonConvert.SerializeObject(pstrAvailabilityId)));
            FetchAvailabilityResponse lobjFetchAvailabilityResponse = lobjModel.FetchAvailability(pstrAvailabilityId);
            lNICogRequestResponse.Append(string.Format(" FetchAvailability Response: {0}", JsonConvert.SerializeObject(lobjFetchAvailabilityResponse)));
            if (lobjFetchAvailabilityResponse != null
                && lobjFetchAvailabilityResponse.data != null
                && !string.IsNullOrEmpty(lobjFetchAvailabilityResponse.data.fetchAvailabilityWithOptionList))
            {
                FetchAvailability lobjFetchAvailability = JsonConvert.DeserializeObject<FetchAvailability>(lobjFetchAvailabilityResponse.data.fetchAvailabilityWithOptionList);
                if (lobjFetchAvailability != null && lobjFetchAvailability.data != null && lobjFetchAvailability.status.ToLower() == "success")
                {
                    string lstrCurrency = lobjModel.GetDefaultCurrency();
                    string lstrProgramName = ProgramHelper.ProgramName();
                    ProgramDefinition lobjProgramDefinition = lobjModel.GetProgramDetails(lstrProgramName);
                    if (lobjFetchAvailability.data.pricingCategoryList.nodes != null)
                    {
                        lobjModel.LogActivity(string.Format("FetchAvailability Request;pstrAvailabilityId - {0}; Status - {1};", pstrAvailabilityId, "Success"), ActivityType.PackageBooking);

                        foreach (var item in lobjFetchAvailability.data.pricingCategoryList.nodes)
                        {
                            string pstrGrossFormattedText = string.Empty;
                            string pstrDisGrossFormattedText = string.Empty;
                            item.unitPrice.gross = lobjModel.ConvertToPointsOrCurrency(item.unitPrice.gross, lstrCurrency, lobjProgramDefinition.ProgramId, out pstrGrossFormattedText);
                            item.unitPrice.grossFormattedText = pstrGrossFormattedText;
                            item.totalDiscountApplied.gross = lobjModel.ConvertToPointsOrCurrency(item.totalDiscountApplied.gross, lstrCurrency, lobjProgramDefinition.ProgramId, out pstrDisGrossFormattedText);
                            item.totalDiscountApplied.grossFormattedText = pstrDisGrossFormattedText;
                        }
                        if (lobjFetchAvailability.data.pricingCategoryList.totalPrice != null)
                        {
                            string pstrGrossFormattedText = string.Empty;
                            lobjFetchAvailability.data.pricingCategoryList.totalPrice.gross = lobjModel.ConvertToPointsOrCurrency(lobjFetchAvailability.data.pricingCategoryList.totalPrice.gross, lstrCurrency, lobjProgramDefinition.ProgramId, out pstrGrossFormattedText);
                            lobjFetchAvailability.data.pricingCategoryList.totalPrice.grossFormattedText = pstrGrossFormattedText;
                        }

                    }
                    lstrResponse = JsonConvert.SerializeObject(lobjFetchAvailability.data);
                }
                else
                {
                    lobjModel.LogActivity(string.Format("FetchAvailability Request;pstrAvailabilityId - {0}; Status - {1};", pstrAvailabilityId, "Failed"), ActivityType.PackageBooking);
                }
            }
        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("ExperienceProductDetails FetchAvailability Exception: " + ex.Message + Environment.NewLine + ex.StackTrace);
        }
        finally
        {
            LoggingAdapter.WriteLog("ExperienceProductDetails FetchAvailability RequestResponse-: " + lNICogRequestResponse);

            // lobjModel.LogActivity(string.Format("ActivityType: {0} - RequestResponse: {1}", ActivityConstants.BookPackage, lNICogRequestResponse), ActivityType.PackageBooking);
        }
        return lstrResponse;
    }
    [WebMethod]
    public static string FetchAvailabilityWithOptionList(string pstrAvailabilityId, string pstrInputId, string pstrInputValue)
    {
        string lstrResponse = string.Empty;
        StringBuilder lNICogRequestResponse = new StringBuilder();
        ABCModel lobjModel = new ABCModel();
        MemberDetails lobjMemberDetails = HttpContext.Current.Session["MemberDetails"] as MemberDetails;
        try
        {
            lNICogRequestResponse.Append(string.Format("FetchAvailabilityWithOptionList Request: pstrAvailabilityId - {0}, pstrInputId - {1}, pstrInputValue - {2}", pstrAvailabilityId, pstrInputId, pstrInputValue));
            FetchAvailabilityResponse lobjFetchAvailabilityResponse = lobjModel.FetchAvailabilityWithOptionList(pstrAvailabilityId, pstrInputId, pstrInputValue);
            lNICogRequestResponse.Append(string.Format(" FetchAvailabilityWithOptionList Response: {0}", JsonConvert.SerializeObject(lobjFetchAvailabilityResponse)));
            if (lobjFetchAvailabilityResponse != null
                && lobjFetchAvailabilityResponse.data != null
                && !string.IsNullOrEmpty(lobjFetchAvailabilityResponse.data.fetchAvailabilityWithOptionList))
            {
                FetchAvailability lobjFetchAvailability = JsonConvert.DeserializeObject<FetchAvailability>(lobjFetchAvailabilityResponse.data.fetchAvailabilityWithOptionList);
                if (lobjFetchAvailability != null && lobjFetchAvailability.data != null && lobjFetchAvailability.status.ToLower() == "success")
                {
                    string lstrCurrency = lobjModel.GetDefaultCurrency();
                    string lstrProgramName = ProgramHelper.ProgramName();
                    ProgramDefinition lobjProgramDefinition = lobjModel.GetProgramDetails(lstrProgramName);
                    if (lobjFetchAvailability.data.pricingCategoryList.nodes != null)
                    {
                        lobjModel.LogActivity(string.Format("FetchAvailability Request;pstrAvailabilityId - {0};  pstrInputId - {1};  pstrInputValue - {2}; Status - {3};", pstrAvailabilityId, pstrInputId, pstrInputValue, "Success"), ActivityType.PackageBooking);
                        foreach (var item in lobjFetchAvailability.data.pricingCategoryList.nodes)
                        {
                            string pstrGrossFormattedText = string.Empty;
                            string pstrDisGrossFormattedText = string.Empty;
                            item.unitPrice.gross = lobjModel.ConvertToPointsOrCurrency(item.unitPrice.gross, lstrCurrency, lobjProgramDefinition.ProgramId, out pstrGrossFormattedText);
                            item.unitPrice.grossFormattedText = pstrGrossFormattedText;
                            item.totalDiscountApplied.gross = lobjModel.ConvertToPointsOrCurrency(item.totalDiscountApplied.gross, lstrCurrency, lobjProgramDefinition.ProgramId, out pstrDisGrossFormattedText);
                            item.totalDiscountApplied.grossFormattedText = pstrDisGrossFormattedText;
                        }
                        if (lobjFetchAvailability.data.pricingCategoryList.totalPrice != null)
                        {
                            string pstrGrossFormattedText = string.Empty;
                            lobjFetchAvailability.data.pricingCategoryList.totalPrice.gross = lobjModel.ConvertToPointsOrCurrency(lobjFetchAvailability.data.pricingCategoryList.totalPrice.gross, lstrCurrency, lobjProgramDefinition.ProgramId, out pstrGrossFormattedText);
                            lobjFetchAvailability.data.pricingCategoryList.totalPrice.grossFormattedText = pstrGrossFormattedText;
                        }

                    }
                    lstrResponse = JsonConvert.SerializeObject(lobjFetchAvailability.data);
                }
                else
                {
                    lobjModel.LogActivity(string.Format("FetchAvailability Request;pstrAvailabilityId - {0};  pstrInputId - {1};  pstrInputValue - {2}; Status - {3};", pstrAvailabilityId, pstrInputId, pstrInputValue, "Failed"), ActivityType.PackageBooking);
                }
            }
        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("ExperienceProductDetails FetchAvailabilityWithOptionList Exception: " + ex.Message + Environment.NewLine + ex.StackTrace);
        }
        finally
        {
            LoggingAdapter.WriteLog("ExperienceProductDetails FetchAvailabilityWithOptionList RequestResponse: " + lNICogRequestResponse);
            //lobjModel.LogActivity(string.Format("ActivityType: {0} - RequestResponse: {1}", ActivityConstants.BookPackage, lNICogRequestResponse), ActivityType.PackageBooking);
        }
        return lstrResponse;
    }
    [WebMethod]
    public static string FetchAvailabilityWithPricingCategory(string pstrAvailabilityId, List<FetchAvailabilityWithPricingCategoryOptionList> plstobjFetchAvailabilityWithPricingCategoryOptionList)
    {
        string lstrResponse = string.Empty;
        ABCModel lobjModel = new ABCModel();
        StringBuilder lNICogRequestResponse = new StringBuilder();
        MemberDetails lobjMemberDetails = HttpContext.Current.Session["MemberDetails"] as MemberDetails;
        try
        {
            lNICogRequestResponse.Append(string.Format("FetchAvailabilityWithOptionList Request: pstrAvailabilityId - {0}, plstobjFetchAvailabilityWithPricingCategoryOptionList - {1}", pstrAvailabilityId, JsonConvert.SerializeObject(plstobjFetchAvailabilityWithPricingCategoryOptionList)));
            FetchAvailabilityResponse lobjFetchAvailabilityResponse = lobjModel.FetchAvailabilityWithOptionList(pstrAvailabilityId, plstobjFetchAvailabilityWithPricingCategoryOptionList);
            lNICogRequestResponse.Append(string.Format(" FetchAvailabilityWithOptionList Response: {0}", JsonConvert.SerializeObject(lobjFetchAvailabilityResponse)));
            if (lobjFetchAvailabilityResponse != null
                && lobjFetchAvailabilityResponse.data != null
                && !string.IsNullOrEmpty(lobjFetchAvailabilityResponse.data.fetchAvailabilityWithOptionList))
            {
                FetchAvailability lobjFetchAvailability = JsonConvert.DeserializeObject<FetchAvailability>(lobjFetchAvailabilityResponse.data.fetchAvailabilityWithOptionList);
                if (lobjFetchAvailability != null && lobjFetchAvailability.data != null && lobjFetchAvailability.status.ToLower() == "success")
                {
                    lobjModel.LogActivity(string.Format("FetchAvailabilityWithOptionList Request;pstrAvailabilityId - {0};  plstobjFetchAvailabilityWithPricingCategoryOptionList - {1};  Status - {2};", pstrAvailabilityId, JsonConvert.SerializeObject(plstobjFetchAvailabilityWithPricingCategoryOptionList), "Success"), ActivityType.PackageBooking);
                    if (lobjFetchAvailability.data.pricingCategoryList != null)
                    {
                        string lstrCurrency = lobjModel.GetDefaultCurrency();
                        string lstrProgramName = ProgramHelper.ProgramName();
                        ProgramDefinition lobjProgramDefinition = lobjModel.GetProgramDetails(lstrProgramName);
                        if (lobjFetchAvailability.data.pricingCategoryList.totalPrice != null)
                        {
                            foreach (var item in lobjFetchAvailability.data.pricingCategoryList.nodes)
                            {
                                string pstrGrossFormattedText = string.Empty;
                                string pstrDisGrossFormattedText = string.Empty;
                                item.unitPrice.gross = lobjModel.ConvertToPointsOrCurrency(item.unitPrice.gross, lstrCurrency, lobjProgramDefinition.ProgramId, out pstrGrossFormattedText);
                                item.unitPrice.grossFormattedText = pstrGrossFormattedText;
                                item.totalDiscountApplied.gross = lobjModel.ConvertToPointsOrCurrency(item.totalDiscountApplied.gross, lstrCurrency, lobjProgramDefinition.ProgramId, out pstrDisGrossFormattedText);
                                item.totalDiscountApplied.grossFormattedText = pstrDisGrossFormattedText;
                            }
                            string pstrFormattedText = string.Empty;
                            lobjFetchAvailability.data.pricingCategoryList.totalPrice.gross = lobjModel.ConvertToPointsOrCurrency(lobjFetchAvailability.data.pricingCategoryList.totalPrice.gross, lstrCurrency, lobjProgramDefinition.ProgramId, out pstrFormattedText);
                            lobjFetchAvailability.data.pricingCategoryList.totalPrice.grossFormattedText = pstrFormattedText;

                        }
                        lstrResponse = JsonConvert.SerializeObject(lobjFetchAvailability.data);
                    }
                }
                else
                {
                    lobjModel.LogActivity(string.Format("FetchAvailabilityWithOptionList Request; pstrAvailabilityId - {0};  plstobjFetchAvailabilityWithPricingCategoryOptionList - {1};  Status - {2};", pstrAvailabilityId, JsonConvert.SerializeObject(plstobjFetchAvailabilityWithPricingCategoryOptionList), "Failed"), ActivityType.PackageBooking);
                }
            }
        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("ExperienceProductDetails FetchAvailabilityWithPricingCategory Exception: " + ex.Message + Environment.NewLine + ex.StackTrace);
        }
        finally
        {
            LoggingAdapter.WriteLog("ExperienceProductDetails FetchAvailabilityWithPricingCategory RequestResponse: " + lNICogRequestResponse);
            //lobjModel.LogActivity(string.Format("ActivityType: {0} - RequestResponse: {1}", ActivityConstants.BookPackage, lNICogRequestResponse), ActivityType.PackageBooking);
        }
        return lstrResponse;
    }
    [WebMethod]
    public static string CreateBooking(string pstrAvailabilityId)
    {
        string lstrResponse = string.Empty;
        StringBuilder lNICogRequestResponse = new StringBuilder();
        ABCModel lobjModel = new ABCModel();
        MemberDetails lobjMemberDetails = HttpContext.Current.Session["MemberDetails"] as MemberDetails;
        try
        {
            lNICogRequestResponse.Append("CreateBooking Request");
            CreateBookingResponse lobjCreateBookingResponse = lobjModel.CreateBooking();
            lNICogRequestResponse.Append(string.Format(" CreateBooking Response: {0}", JsonConvert.SerializeObject(lobjCreateBookingResponse)));
            if (lobjCreateBookingResponse != null
                && lobjCreateBookingResponse.data != null
                 && !string.IsNullOrEmpty(lobjCreateBookingResponse.data.createBooking))
            {
                CreateBooking lobjCreateBooking = JsonConvert.DeserializeObject<CreateBooking>(lobjCreateBookingResponse.data.createBooking);
                if (lobjCreateBooking != null && lobjCreateBooking.status.ToLower() == "success" && lobjCreateBooking.data != null)
                {
                    lobjModel.LogActivity(string.Format("CreateBooking Request; pstrAvailabilityId - {0};  Status - {1};", pstrAvailabilityId, "Success"), ActivityType.PackageBooking);

                    if (!string.IsNullOrEmpty(lobjCreateBooking.data.id))
                    {
                        AddAvailabilityToBookingResponse lobjAddAvailabilityToBookingResponse = lobjModel.AddAvailabilityToBooking(pstrAvailabilityId, lobjCreateBooking.data.id);
                        if (lobjAddAvailabilityToBookingResponse != null
                           && lobjAddAvailabilityToBookingResponse.data != null
                            && !string.IsNullOrEmpty(lobjAddAvailabilityToBookingResponse.data.addAvailabilityToBooking))
                        {
                            AddAvailabilityToBooking lobjAddAvailabilityToBooking = JsonConvert.DeserializeObject<AddAvailabilityToBooking>(lobjAddAvailabilityToBookingResponse.data.addAvailabilityToBooking);
                            if (lobjAddAvailabilityToBooking != null && lobjAddAvailabilityToBooking.status.ToLower() == "success" && lobjAddAvailabilityToBooking.data != null)
                            {
                                if (!string.IsNullOrEmpty(lobjAddAvailabilityToBooking.data.id))
                                {
                                    lstrResponse = lobjAddAvailabilityToBooking.data.id;
                                }
                            }
                        }
                    }
                }
                else
                {
                    lobjModel.LogActivity(string.Format("CreateBooking Request;pstrAvailabilityId - {0};  Status - {1};", pstrAvailabilityId, "Failed"), ActivityType.PackageBooking);
                }
            }
        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("ExperienceProductDetails CreateBooking Exception: " + ex.Message + Environment.NewLine + ex.StackTrace);
        }
        finally
        {
            LoggingAdapter.WriteLog("ExperienceProductDetails CreateBooking RequestResponse: " + lNICogRequestResponse);
            //lobjModel.LogActivity(string.Format("ActivityType: {0} - RequestResponse: {1}", ActivityConstants.BookPackage, lNICogRequestResponse), ActivityType.PackageBooking);
        }
        return lstrResponse;
    }
    [WebMethod]
    public static bool CheckAvailability(decimal pntamount)
    {
        try
        {
            ABCModel lobjmodel = new ABCModel();
            ShopModel lmodel = new ShopModel();
            string lstrCurrency = lobjmodel.GetDefaultCurrency();
            MemberDetails lobjMemberDetails = HttpContext.Current.Session["MemberDetails"] as MemberDetails;
            MemberRelation lobjMemberRelation = lobjMemberDetails.MemberRelationsList.Find(lobj => lobj.RelationType.Equals(RelationType.LBMS));
            ProgramDefinition lobjProgramDefinition = lobjmodel.GetProgramMaster();
            int MemberMiles = lobjmodel.CheckAvailbility(lobjMemberRelation.RelationReference, Convert.ToInt32(lobjMemberRelation.RelationType), lstrCurrency, lobjProgramDefinition.ProgramId);
            if ((pntamount) > MemberMiles)
            {
                return false;
            }
        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("ExperienceProductDetails.aspx CheckAvailability Exception: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
            return false;
        }
        return true;
    }
}