using Core.Platform.ProgramMaster.BusinessFacade;
using Core.Platform.ProgramMaster.Entities;
using Framework.EnterpriseLibrary.Adapters;
using Framework.EnterpriseLibrary.CommunicationEngine.BusinessFacade;
using Framework.EnterpriseLibrary.CommunicationEngine.Entity;
using Framework.EnterpriseLibrary.UniqueNumberGenerator;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Reflection;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using System.Dynamic;
using SBL.ETL.WebAPI.Helper.Response;

namespace SBL.ETL.WebAPI.Helper
{
    public class APIHelper
    {
        private static IConfiguration _configuration;

        private static string AuthToken = "";
        static APIHelper()
        {
            if (_configuration == null)
            {
                //var builder = new ConfigurationBuilder()
                //.SetBasePath(Directory.GetCurrentDirectory())
                //.AddJsonFile("appsettings.json");
                //Configuration = builder.Build();

                var configBuilder = new ConfigurationBuilder()
    .SetBasePath(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location))
    .AddJsonFile("appsettings.json");
                _configuration = configBuilder.Build();

            }
        }

        private static string CreateToken()
        {
            string lstrReferenceId = Convert.ToString(Guid.NewGuid());
            string grant_type, username, password, client_id, client_secret, scope;
            username = _configuration["username"];
            password = _configuration["password"];
            scope = _configuration["scope"];
            grant_type = _configuration["grant_type"];
            client_id = _configuration["client_id"];
            client_secret = _configuration["client_secret"];

            string TokenEndPoint = _configuration["AuthEndPoint"];

            LoggingAdapter.WriteLog("APIHelper CreateToken Request = " + DateTime.Now + " | RefId = " + lstrReferenceId);

            string lstrTokenResponse = ClientHelper.TokenPostData(TokenEndPoint + "Token", grant_type, username, password, client_id, client_secret, scope, lstrReferenceId);

            var tokenResponse = JsonConvert.DeserializeObject(lstrTokenResponse, typeof(TokenAPIResponse)) as TokenAPIResponse;

            LoggingAdapter.WriteLog("APIHelper CreateToken Response = " + DateTime.Now + " | RefId = " + lstrReferenceId);

            AuthToken = "bearer " + tokenResponse.results.Token;
            return AuthToken;
        }

        public static bool InsertEmailDetails(EmailDetails pstrRequestData)
        {
            string lstrReferenceId = Convert.ToString(Guid.NewGuid());
            string CEEndPoint = _configuration["CEEndPoint"];

            List<Attachments> attachments = new List<Attachments>();
            attachments.Add(new Attachments());

            InsertEmailRequest insertEmailRequest = new InsertEmailRequest()
            {
                EmailDetails = pstrRequestData,
                Attachments = attachments
            };

            try
            {
                LoggingAdapter.WriteLog("APIHelper InsertEmailDetails Request = " + DateTime.Now + " | RefId = " + lstrReferenceId);

                string lstrtoken = AuthToken;

                string lstrMethodName = "InsertEmailDetails";
                string lstrParameters = JsonConvert.SerializeObject(insertEmailRequest);
                lstrParameters = JsonConvert.SerializeObject(lstrParameters);

                string lstrResponse = ClientHelper.PostData(CEEndPoint + lstrMethodName, "POST", lstrMethodName, lstrParameters, lstrtoken, lstrReferenceId, "");

                if (lstrResponse == "You are unauthorized to access this resource")
                {
                    LoggingAdapter.WriteLog("APIHelper InsertEmailDetails Response = " + DateTime.Now + " | RefId = " + lstrReferenceId);
                }
                LoggingAdapter.WriteLog("APIHelper InsertEmailDetails Response = " + DateTime.Now + " | RefId = " + lstrReferenceId);

                return true;
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog(Environment.NewLine + "APIHelper InsertEmailDetails Ex: | DateTime " + DateTime.Now + Environment.NewLine + "Message = " + ex.Message + Environment.NewLine + "StackTrace = " + ex.StackTrace + Environment.NewLine + "Exception = " + ex.InnerException + " | RefId = " + lstrReferenceId);
                return false;
            }
        }

        public static ProgramDefinition GetProgramMaster(string pstrRequestData)
        {
            string lstrReferenceId = Convert.ToString(Guid.NewGuid());
            string ProgramEndPoint = _configuration["ProgramEndPoint"];

            try
            {
                LoggingAdapter.WriteLog("APIHelper GetProgramMaster Request = " + DateTime.Now + " | RefId = " + lstrReferenceId);

                string lstrtoken = CreateToken();

                string lstrMethodName = "GetProgramDefinition";
                string lstrParameters = "?pstrProgramName=" + pstrRequestData;
                string lstrResponse = ClientHelper.PostData(ProgramEndPoint + lstrMethodName + lstrParameters, "GET", lstrMethodName, lstrParameters, lstrtoken, lstrReferenceId, "");

                if (lstrResponse == "You are unauthorized to access this resource")
                {
                    LoggingAdapter.WriteLog("APIHelper GetProgramMaster Response = " + DateTime.Now + " | RefId = " + lstrReferenceId);
                }
                LoggingAdapter.WriteLog("APIHelper GetProgramMaster Response = " + DateTime.Now + " | RefId = " + lstrReferenceId);

                var programResponse = ((JsonConvert.DeserializeObject(lstrResponse, typeof(ProgramAPIResponse)) as ProgramAPIResponse)).results.ReturnObject;

                return programResponse;
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog(Environment.NewLine + "APIHelper GetProgramMaster Ex: | DateTime " + DateTime.Now + Environment.NewLine + "Message = " + ex.Message + Environment.NewLine + "StackTrace = " + ex.StackTrace + Environment.NewLine + "Exception = " + ex.InnerException + " | RefId = " + lstrReferenceId);
                return new ProgramDefinition();
            }
        }
    }
}
