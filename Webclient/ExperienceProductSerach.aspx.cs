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

public partial class ExperienceProductSerach : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            ABCModel lobjModel = new ABCModel();
            MemberDetails lobjMemberDetails = HttpContext.Current.Session["MemberDetails"] as MemberDetails;
            if (lobjMemberDetails != null)
            {
                lobjModel.LogActivity(string.Format(" Visit Experience Product Serach; MemberId:{0};", lobjMemberDetails.MemberRelationsList[0].RelationReference), ActivityType.PageLoad);
            }
        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("ExperienceProductSerach.aspx Pageload Exception: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
        }
    }
    [WebMethod]
    public static string GetExperienceProductListByPlaceId(string pstrPlaceId, bool pblnIsPrivate, bool pblnIsNew, string pstrIsRecommended, string pstrGuidePrice, string pstrSearch, List<string> plstCategoryIds, List<string> plstAttributeIds)
    {
        string lstrResponse = string.Empty;
        StringBuilder lNICogRequestResponse = new StringBuilder();
        ABCModel lobjModel = new ABCModel();
        MemberDetails lobjMemberDetails = HttpContext.Current.Session["MemberDetails"] as MemberDetails;
        try
        {
            lNICogRequestResponse.Append(string.Format("GetExperienceProductListByPlaceId Request: pstrPlaceId - {0}, pblnIsPrivate - {1}, pblnIsNew - {2}, pstrIsRecommended - {3}, pstrGuidePrice - {4}, pstrSearch - {5}, plstCategoryIds - {6}, plstAttributeIds - {7}", pstrPlaceId, pblnIsPrivate, pblnIsNew, pstrIsRecommended, pstrGuidePrice, pstrSearch, JsonConvert.SerializeObject(plstCategoryIds), JsonConvert.SerializeObject(plstAttributeIds)));
            SearchProductListResponse lstrProductListResponse = lobjModel.GetExperienceProductListByPlaceId(pstrPlaceId, pblnIsPrivate, pblnIsNew, pstrIsRecommended, pstrGuidePrice, pstrSearch, plstCategoryIds, plstAttributeIds);
            lNICogRequestResponse.Append(string.Format(" GetExperienceProductListByPlaceId Response: {0}", JsonConvert.SerializeObject(lstrProductListResponse)));
            if (lstrProductListResponse.data != null && !string.IsNullOrEmpty(lstrProductListResponse.data.getProductListByPlace))
            {
                ProductListByPlace lobjProductListByPlace = JsonConvert.DeserializeObject<ProductListByPlace>(lstrProductListResponse.data.getProductListByPlace);
                if (lobjProductListByPlace != null && lobjProductListByPlace.status.ToLower() == "success" && lobjProductListByPlace.data != null)
                {
                    lobjModel.LogActivity(string.Format("GetExperienceProductListByPlaceId; pstrPlaceId - {0}; pblnIsPrivate - {1}; pblnIsNew - {2}; pstrIsRecommended - {3}; pstrGuidePrice - {4}; pstrSearch - {5}; plstCategoryIds - {6}; plstAttributeIds - {7}; Status:{8};", pstrPlaceId, pblnIsPrivate, pblnIsNew, pstrIsRecommended, pstrGuidePrice, pstrSearch, JsonConvert.SerializeObject(plstCategoryIds), JsonConvert.SerializeObject(plstAttributeIds), "Success"), ActivityType.PackageBooking);
                    string lstrCurrency = lobjModel.GetDefaultCurrency();
                    string lstrProgramName = ProgramHelper.ProgramName();
                    ProgramDefinition lobjProgramDefinition = lobjModel.GetProgramDetails(lstrProgramName);
                    foreach (var item in lobjProductListByPlace.data)
                    {
                        string pstrGrossFormattedText = string.Empty;
                        item.guidePriceAmount = lobjModel.ConvertToPointsOrCurrency(item.guidePriceAmount, lstrCurrency, lobjProgramDefinition.ProgramId,out pstrGrossFormattedText);
                        item.guidePriceFormattedText = pstrGrossFormattedText;
                    }
                    lstrResponse = JsonConvert.SerializeObject(lobjProductListByPlace);
                }
            }
            else
            {
                lobjModel.LogActivity(string.Format("GetExperienceProductListByPlaceId;pstrPlaceId - {0}; pblnIsPrivate - {1}; pblnIsNew - {2}; pstrIsRecommended - {3}; pstrGuidePrice - {4}; pstrSearch - {5}; plstCategoryIds - {6}; plstAttributeIds - {7}; Status:{8};", pstrPlaceId, pblnIsPrivate, pblnIsNew, pstrIsRecommended, pstrGuidePrice, pstrSearch, JsonConvert.SerializeObject(plstCategoryIds), JsonConvert.SerializeObject(plstAttributeIds), "Failed"), ActivityType.PackageBooking);
            }
        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("ExperienceProductSerach GetExperienceProductListByPlaceId Exception: " + ex.Message + Environment.NewLine + ex.StackTrace);
        }
        finally
        {
            LoggingAdapter.WriteLog("ExperienceProductSerach GetExperienceProductListByPlaceId RequestResponse: " + lNICogRequestResponse);
           //lobjModel.LogActivity(string.Format("ActivityType: {0} - RequestResponse: {1}", ActivityConstants.BookPackage, lNICogRequestResponse), ActivityType.PackageBooking);
        }
        return lstrResponse;
    }
}