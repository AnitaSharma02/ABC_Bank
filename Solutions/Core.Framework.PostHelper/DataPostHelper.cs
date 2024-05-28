using Framework.EnterpriseLibrary.Adapters;
using Framework.EnterpriseLibrary.UniqueNumberGenerator;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Core.Framework.PostHelper
{
    public class DataPostHelper
    {
        public static string PostData(string pstrURL, string pstrMethod, string pstrMethodName, string pstrParameters, string pstrtoken, string pstrContentType ="application/json", string pstrAcceptMedia = "application/json")
        {
            LoggingAdapter.WriteLog(Environment.NewLine + "PostData Request = " + " | " + pstrURL + " | " + pstrMethod + " | " + pstrMethodName + " | " + pstrParameters + " | " + pstrtoken);
            string lstrResponse = string.Empty;
            HttpWebResponse lobjWebResponse = null;
            string lstrReference = Get12DigitNumberDateTime();
            LoggingAdapter.WriteLog(Environment.NewLine + "PostData Start = " + lstrReference + " | DateTime " + DateTime.Now + Environment.NewLine + "Method Name = " + pstrMethodName + Environment.NewLine + "API URL = " + pstrURL + Environment.NewLine);
            try
            {
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
                ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                HttpWebRequest lobjWebRequest = (HttpWebRequest)WebRequest.Create(pstrURL);
                lobjWebRequest.Timeout = 600000;
                lobjWebRequest.KeepAlive = false;
                lobjWebRequest.Method = pstrMethod;
                lobjWebRequest.ContentType = pstrContentType;
                lobjWebRequest.PreAuthenticate = true;
                if (!string.IsNullOrEmpty(pstrtoken))
                {
                    
                    lobjWebRequest.Headers.Add("Authorization", "Bearer " + pstrtoken);
                }
                lobjWebRequest.Accept = pstrAcceptMedia;
                lobjWebRequest.MediaType = pstrAcceptMedia;
                if (pstrMethod.Equals("POST"))
                {
                    StreamWriter writer = new StreamWriter(lobjWebRequest.GetRequestStream());
                    writer.WriteLine(pstrParameters);
                    writer.Close();
                }
                lobjWebResponse = (HttpWebResponse)lobjWebRequest.GetResponse();

                if (lobjWebResponse.StatusCode == HttpStatusCode.OK || lobjWebResponse.StatusCode == HttpStatusCode.Created)
                {
                    lstrResponse = ReadStream(lobjWebResponse);
                    LoggingAdapter.WriteLog(Environment.NewLine + "PostData Response = " + lstrReference + " | DateTime " + DateTime.Now + Environment.NewLine + "Response = " + lstrResponse + Environment.NewLine);
                }
                else
                {
                    LoggingAdapter.WriteLog(Environment.NewLine + "PostData Response (StatusCode Not Available) = " + lstrReference + " | DateTime " + DateTime.Now + Environment.NewLine + "Response = " + lstrResponse + Environment.NewLine);
                }
                LoggingAdapter.WriteLog(Environment.NewLine + "PostData End = " + lstrReference + " | DateTime " + DateTime.Now + Environment.NewLine + "Method Name = " + pstrMethodName + Environment.NewLine + "API URL = " + pstrURL + Environment.NewLine);
            }
            catch (WebException wex)
            {
                LoggingAdapter.WriteLog(Environment.NewLine + "PostData WebException = " + lstrReference + " | DateTime " + DateTime.Now + Environment.NewLine + "Method Name = " + pstrMethodName + Environment.NewLine + "Message = " + wex.Message + Environment.NewLine + "StackTrace = " + wex.StackTrace + Environment.NewLine + "Exception = " + wex.InnerException + Environment.NewLine + "Response = " + lstrResponse);
                lstrResponse = ReadStream(wex.Response as HttpWebResponse);
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog(Environment.NewLine + "PostData Exception = " + lstrReference + " | DateTime " + DateTime.Now + Environment.NewLine + "Method Name = " + pstrMethodName + Environment.NewLine + "Message = " + ex.Message + Environment.NewLine + "StackTrace = " + ex.StackTrace + Environment.NewLine + "Exception = " + ex.InnerException + Environment.NewLine + "Response = " + lstrResponse);
                throw ex;
            }
            finally
            {
                lobjWebResponse?.Close();
            }
            return lstrResponse;
        }
        public static JObject JsonFormatting(string pstrJsonData)
        {
            string lstrResponse = string.Empty;
            try
            {
                if (pstrJsonData.Length > 12)
                {
                    lstrResponse = pstrJsonData.Substring(0, pstrJsonData.Length - 1).Remove(0, 11);
                }
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("CommonMethods JsonFormatting Ex: " + Environment.NewLine + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
            }
            return JObject.Parse(lstrResponse);
        }
        public static bool AcceptAllCertifications(object sender, System.Security.Cryptography.X509Certificates.X509Certificate certification, System.Security.Cryptography.X509Certificates.X509Chain chain, System.Net.Security.SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }
        public static string ReadStream(HttpWebResponse response)
        {
            using (StreamReader sr = new StreamReader(response.GetResponseStream()))
            {
                return sr.ReadToEnd();
            }
        }
        public static string Get12DigitNumberDateTime()
        {
            return GenerateUniqueNumber.Get12DigitNumberDateTime();
        }
    }
}
