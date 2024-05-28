using Framework.EnterpriseLibrary.Adapters;
using Newtonsoft.Json;
using System;
using System.Configuration;

namespace Core.Framework.PostHelper
{
    public class MemberLogin
    {
        public static string lstrOAuthAPIURL = Convert.ToString(ConfigurationManager.AppSettings["BaseCoreOAuthAPIURL"]);
        public static string lstrMemberAPIURL = Convert.ToString(ConfigurationManager.AppSettings["BaseCoreMemberAPIURL"]); 
        public static string lstrClientSecret = Convert.ToString(ConfigurationManager.AppSettings["AuthAPIClientSecret"]);
        public static string lstrClientId = Convert.ToString(ConfigurationManager.AppSettings["AuthAPIClientId"]);
        public static string lstrGrantType = Convert.ToString(ConfigurationManager.AppSettings["AuthAPIGrantType"]);
        public static string lstrExtLoginSSOTestCIF = Convert.ToString(ConfigurationManager.AppSettings["ExtLoginSSOTestCIF"]);

        public Token GenerateToken(string pstrScope)
        {
            Token lobjToken = null;
            try
            {
                string lstrPostData = "{\"grant_type\": \"" + lstrGrantType + "\",\"username\": \"\",\"password\": \"\", \"client_id\": \"" + lstrClientId + "\", \"client_secret\": \"" + lstrClientSecret + "\", \"scope\":\"" + pstrScope + "\"}";
                var lobjDynamic = JsonConvert.DeserializeObject<Root>(DataPostHelper.PostData(lstrOAuthAPIURL + "API/Auth/Token", "POST", "Token", lstrPostData, "").ToString());
                if (lobjDynamic != null)
                {
                    lobjToken = JsonConvert.DeserializeObject<Token>(lobjDynamic.results.ToString());
                }
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("MemberLogin Ex: " + ex.Message + Environment.NewLine + "Stack Trace: " + ex.StackTrace);
            }
            return lobjToken;
        }
        public object GetMemberProfile(string pstrProgramId, int pstrRelationType, string pstrOAuth)
        {
            try
            {
                return JsonConvert.DeserializeObject(DataPostHelper.PostData(lstrMemberAPIURL + "API/Member/GetMemberProfile?pintProgramId=" + pstrProgramId + "&pintRelationType=" + pstrRelationType.ToString(), "GET", "GetMemberProfile", "", pstrOAuth));
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("GetMemberProfile Ex: " + ex.Message + Environment.NewLine + "Stack Trace: " + ex.StackTrace);
                return null;
            }
        }

        public object GetMemberLocalAttrDetails(string pstrRelationReference, string pstrProgramId, int pstrRelationType, string pstrOAuth)
        {
            try
            {
                return JsonConvert.DeserializeObject(DataPostHelper.PostData(lstrMemberAPIURL + "API/Member/GetMemberAdditionalInfo?pstrRelationReference=" + pstrRelationReference + "&pintProgramId=" + pstrProgramId + "&pintRelationType=" + pstrRelationType.ToString(), "GET", "GetMemberAdditionalInfo", "", pstrOAuth));
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("GetMemberAdditionalInfo Ex: " + ex.Message + Environment.NewLine + "Stack Trace: " + ex.StackTrace);
                return null;
            }
        }
    }
}
