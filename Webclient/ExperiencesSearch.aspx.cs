using BeMyGuest.Entities;
using ABC.Model;
using Framework.EnterpriseLibrary.Adapters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Core.Platform.MemberActivity.Entities;
using System.Text.RegularExpressions;

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
        try
        {
            //SearchExperiencesModel searchExperiencesModel = new SearchExperiencesModel()
            //{
            //    TypesAndCategory = lobjModel.GetTypesAndCategory(),
            //    searchTerm = pstrsearchTerm
            //};
            //List<string> TypesArray = new List<string>();
            //List<string> CategoriesArray = new List<string>();
            //if (types.Count > 0)
            //{
            //    TypesArray = types;
            //}
            //else
            //{
            //    TypesArray = searchExperiencesModel.TypesAndCategory.types;
            //}
            //if (categories.Count > 0)
            //{
            //    CategoriesArray = categories;
            //}
            //else
            //{
            //    CategoriesArray = searchExperiencesModel.TypesAndCategory.categories;
            //}
            
            ExperiencesRequest lobjProductListRequest = new ExperiencesRequest
            {
                page = pintPage,
                per_page = pintPageSize,
                category = categories,
                search_term = Regex.Replace(Regex.Replace(pstrsearchTerm, @"[^0-9a-zA-Z]+", " ").Replace("20"," "), @"\s\s+", " "),
                type_name = types,
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