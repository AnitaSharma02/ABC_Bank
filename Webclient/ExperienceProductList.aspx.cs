using ABC.Model;
using BeMyGuest.Entities;
using Core.Platform.MemberActivity.Entities;
using Core.Platform.ProgramMaster.Entities;
using Framework.EnterpriseLibrary.Adapters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;

public partial class ExperienceProductList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["CategoryName"] = "experiences";
    }
    [System.Web.Script.Services.ScriptMethod()]
    [WebMethod]
    public static string GetExperienceProductList(int pintPage, int pintPageSize)
    {
        string lstrResponse = string.Empty;
        ABCModel lobjModel = new ABCModel();
        ProgramDefinition lobjProgramDefinition = lobjModel.GetProgramMaster();
        float pfltPointrate = 0.0f;
        pfltPointrate = lobjModel.GetProgramRedemptionRate(lobjModel.GetDefaultCurrency(), "EXPERIENCE", lobjProgramDefinition.ProgramId);
        StringBuilder lCBCogRequestResponse = new StringBuilder();
        try
        {
            lCBCogRequestResponse.Append(string.Format("GetExperienceProductList Request: pintPage - {0}, pintPageSize - {1}", pintPage, pintPageSize));
            ExperiencesRequest lobjProductListRequest = new ExperiencesRequest
            {
                page = pintPage,
                per_page = pintPageSize,
                search_term="",
                type_name= new List<string>(),
                category= new List<string>(),
                pointConvrtRate= Convert.ToString(pfltPointrate)
            };

            ExperiencesResponse lstrProductListResponse = lobjModel.GetExperienceProductList(pintPage, pintPageSize, lobjProductListRequest);
            lCBCogRequestResponse.Append(string.Format(" GetExperienceProductList Response: {0}", JsonConvert.SerializeObject(lstrProductListResponse)));
            if (lstrProductListResponse.data != null /*&& !string.IsNullOrEmpty(lstrProductListResponse.data.getProductList)*/)
            {
                List<ExperiencesData> lobjProductList = lstrProductListResponse.data;
                if (lobjProductList != null && lobjProductList.Count > 0 && lstrProductListResponse.success == 1)
                {
                    HttpContext.Current.Session["SearchExperiencesResponse"] = lstrProductListResponse;
                    lobjModel.LogActivity(string.Format("GetExperienceProductList; pintPage - {0}; pintPageSize - {1}; Status:{2};", pintPage, pintPageSize, lstrProductListResponse.success), ActivityType.PackageBooking);
                    lstrResponse = JsonConvert.SerializeObject(lobjProductList);
                }
                else
                {
                    lobjModel.LogActivity(string.Format("GetExperienceProductList; pintPage - {0}; pintPageSize - {1}; Status:{3};", pintPage, pintPageSize, "Failed"), ActivityType.PackageBooking);
                }
            }



        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("ExperienceProductList GetExperienceProductList Exception: " + ex.Message + Environment.NewLine + ex.StackTrace);
        }
        finally
        {
            LoggingAdapter.WriteLog("ExperienceProductList GetExperienceProductList RequestResponse: " + lCBCogRequestResponse);
            // lobjModel.LogActivity(string.Format("ActivityType: {0} - RequestResponse: {1}", ActivityConstants.BookPackage, lCBCogRequestResponse), ActivityType.PackageBooking);
        }
        return lstrResponse;
    }

}