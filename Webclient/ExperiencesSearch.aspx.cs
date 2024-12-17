using ABC.Model;
using BeMyGuest.Entities;
using Core.Platform.ProgramMaster.Entities;
using Framework.EnterpriseLibrary.Adapters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Web.Services;

public partial class ExperiencesSearch : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }

    [WebMethod]
    public static string SearchExperiences(int pintPage, int pintPageSize, string pstrsearchTerm, List<string> types, List<string> categories)
    {
        string lstrResponse = string.Empty;
        ABCModel lobjModel = new ABCModel();
        ProgramDefinition lobjProgramDefinition = lobjModel.GetProgramMaster();
        float pfltPointrate = 0.0f;
        pfltPointrate = lobjModel.GetProgramRedemptionRate(lobjModel.GetDefaultCurrency(), "EXPERIENCE", lobjProgramDefinition.ProgramId);
        try
        {
            ExperiencesRequest lobjProductListRequest = new ExperiencesRequest
            {
                page = pintPage,
                per_page = pintPageSize,
                category = categories,
                search_term = Regex.Replace(Regex.Replace(pstrsearchTerm, @"[^0-9a-zA-Z]+", " ").Replace("20"," "), @"\s\s+", " "),
                type_name = types,
                pointConvrtRate = Convert.ToString(pfltPointrate)
            };
            ExperiencesResponse lstrProductListResponse = lobjModel.GetExperienceProductList(pintPage, pintPageSize, lobjProductListRequest);
            if (lstrProductListResponse.data != null)
            {
                lstrProductListResponse.ExperiencesCriteria = new ExperiencesCriteria()
                {
                    searchTerm = pstrsearchTerm.Replace("%20", " "),
                    categoryNames = categories,
                    typeNames = types
                };
                lstrResponse = JsonConvert.SerializeObject(lstrProductListResponse);
            }
        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("ExperienceProductList SearchExperiences Exception: " + ex.Message + Environment.NewLine + ex.StackTrace);
        }
        return lstrResponse;
    }

    [WebMethod]
    public static string GetTypesAndCategory()
    {
        string lstrResponse = string.Empty;
        ABCModel lobjModel = new ABCModel();
        try
        {
            ExperiencesTypesAndCategory lobjTypesAndCategory = lobjModel.GetTypesAndCategory();
            if (lobjTypesAndCategory != null)
            {
                lstrResponse = JsonConvert.SerializeObject(lobjTypesAndCategory);
            }
        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("ExperienceProductList GetTypesAndCategory Exception: " + ex.Message + Environment.NewLine + ex.StackTrace);
        }
        return lstrResponse;
    }
}