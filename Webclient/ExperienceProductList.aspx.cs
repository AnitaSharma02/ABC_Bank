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
using System.Configuration;
using System.Web;
using Core.Platform.Member.Entites;

public partial class ExperienceProductList : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            ABCModel lobjModel = new ABCModel();
            MemberDetails lobjMemberDetails = HttpContext.Current.Session["MemberDetails"] as MemberDetails;
            if (lobjMemberDetails != null)
            {
                lobjModel.LogActivity(string.Format(" Visit Experience Product List; MemberId:{0};", lobjMemberDetails.MemberRelationsList[0].RelationReference), ActivityType.PageLoad);
            }
        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("ExperienceProductList.aspx Pageload Exception: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
        }
    }

    [WebMethod]
    public static string GetExperienceProductList(int pintPage, int pintLimit, string pstrSort, string pstrPlaceName, string pstrGuidePrice)
    {
        string lstrResponse = string.Empty;
        ABCModel lobjModel = new ABCModel();
        StringBuilder lNICogRequestResponse = new StringBuilder();
        MemberDetails lobjMemberDetails = HttpContext.Current.Session["MemberDetails"] as MemberDetails;
        try
        {
            lNICogRequestResponse.Append(string.Format("GetExperienceProductList Request: pintPage - {0}, pintLimit - {1}, pstrSort - {2}, pstrPlaceName - {3}, pstrGuidePrice - {4}", pintPage, pintLimit, pstrSort, pstrPlaceName, pstrGuidePrice));
            ProductListResponse lstrProductListResponse = lobjModel.GetExperienceProductList(pintPage, pintLimit, pstrSort, pstrPlaceName, pstrGuidePrice);
            lNICogRequestResponse.Append(string.Format(" GetExperienceProductList Response: {0}", JsonConvert.SerializeObject(lstrProductListResponse)));
            if (lstrProductListResponse.data != null && !string.IsNullOrEmpty(lstrProductListResponse.data.getProductList))
            {
                ProductList lobjProductList = JsonConvert.DeserializeObject<ProductList>(lstrProductListResponse.data.getProductList);
                if (lobjProductList != null && lobjProductList.data != null && lobjProductList.status.ToLower() == "success")
                {
                    lobjModel.LogActivity(string.Format("GetExperienceProductList; pintPage - {0}; pintLimit - {1}; pstrSort - {2}; pstrPlaceName - {3}; pstrGuidePrice - {4}; Status:{5};",pintPage, pintLimit, pstrSort, pstrPlaceName, pstrGuidePrice, lobjProductList.status), ActivityType.PackageBooking);
                    string lstrCurrency = lobjModel.GetDefaultCurrency();
                    string lstrProgramName = ProgramHelper.ProgramName();
                    ProgramDefinition lobjProgramDefinition = lobjModel.GetProgramDetails(lstrProgramName);
                    foreach (var item in lobjProductList.data)
                    {
                        string pstrGrossFormattedText = string.Empty;
                        item.holibobGuidePrice.gross = lobjModel.ConvertToPointsOrCurrency(item.holibobGuidePrice.gross, lstrCurrency, lobjProgramDefinition.ProgramId, out pstrGrossFormattedText);
                        item.holibobGuidePrice.grossFormattedText = pstrGrossFormattedText;
                    }
                    lstrResponse = JsonConvert.SerializeObject(lobjProductList);
                }
                else
                {
                    lobjModel.LogActivity(string.Format("GetExperienceProductList; pintPage - {0}; pintLimit - {1}; pstrSort - {2}; pstrPlaceName - {3}; pstrGuidePrice - {4}; Status:{5};", pintPage, pintLimit, pstrSort, pstrPlaceName, pstrGuidePrice, "Failed"), ActivityType.PackageBooking);
                }
            }

        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("ExperienceProductList GetExperienceProductList Exception: " + ex.Message + Environment.NewLine + ex.StackTrace);
        }
        finally
        {
            LoggingAdapter.WriteLog("ExperienceProductList GetExperienceProductList RequestResponse: " + lNICogRequestResponse);
            // lobjModel.LogActivity(string.Format("ActivityType: {0} - RequestResponse: {1}", ActivityConstants.BookPackage, lNICogRequestResponse), ActivityType.PackageBooking);
        }
        return lstrResponse;
    }
    [WebMethod]
    public static List<ProductSearchData> GetSearchList(string pstrSearchText)
    {
        List<ProductSearchData> lobjResponse = new List<ProductSearchData>();
        StringBuilder lNICogRequestResponse = new StringBuilder();
        ABCModel lobjModel = new ABCModel();
        MemberDetails lobjMemberDetails = HttpContext.Current.Session["MemberDetails"] as MemberDetails;
        try
        {
            lNICogRequestResponse.Append(string.Format("GetSearchList Request: pstrSearchText - {0}", pstrSearchText));
            ProductSearchResponse lobjProductSearchResponse = lobjModel.GetSearchList(pstrSearchText);
            lNICogRequestResponse.Append(string.Format(" GetSearchList Response: {0}", JsonConvert.SerializeObject(lobjProductSearchResponse)));
            if (lobjProductSearchResponse != null && lobjProductSearchResponse.data != null && !string.IsNullOrEmpty(lobjProductSearchResponse.data.getSearchList))
            {
                ProductSearch lobjProductSearch = JsonConvert.DeserializeObject<ProductSearch>(lobjProductSearchResponse.data.getSearchList);
                if (lobjProductSearch != null && lobjProductSearch.data != null && lobjProductSearch.status.ToLower() == "success")
                {
                    lobjModel.LogActivity(string.Format("GetSearchList; pstrSearchText - {0}; Status:{1};", pstrSearchText, lobjProductSearch.status), ActivityType.PackageBooking);
                    lobjResponse = lobjProductSearch.data;
                }
                else
                {
                    lobjModel.LogActivity(string.Format("GetSearchList; pstrSearchText - {0}; Status:{1};", pstrSearchText, lobjProductSearch.status), ActivityType.PackageBooking);
                }
            }
        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("ExperienceProductList GetSearchList Exception: " + ex.Message + Environment.NewLine + ex.StackTrace);
        }
        finally
        {
            LoggingAdapter.WriteLog("ExperienceProductList GetSearchList RequestResponse: " + lNICogRequestResponse);
            //lobjModel.LogActivity(string.Format("ActivityType: {0} - RequestResponse: {1}", ActivityConstants.BookPackage, lNICogRequestResponse), ActivityType.PackageBooking);
        }
        return lobjResponse;
    }
}