using Framework.EnterpriseLibrary.Adapters;
using Newtonsoft.Json;
using System;
using System.Dynamic;
using System.IO;
using System.Net;
using System.Text;

namespace SBL.ETL.WebAPI.Helper
{
    public class ClientHelper
    {
        public static string TokenPostData(string pstrURL, string granttype, string username, string password, string clientid, string clientsecret, string scope, string pstrReferenceId)
        {
            HttpWebResponse lobjWebResponse = null;
            string lstrResponseString = string.Empty;
            //string lstrReference = Get12DigitNumberDateTime();
            string lstrMethodName = "GenerateToken";
            try
            {
                LoggingAdapter.WriteLog(Environment.NewLine + "TokenPostData Start = " + pstrReferenceId + Environment.NewLine + pstrURL + Environment.NewLine, "TokenLogs");
                ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                string postData = string.Empty;
                HttpWebRequest lobjWebRequest = (HttpWebRequest)WebRequest.Create(pstrURL);
                if (granttype.Equals("password"))
                {
                    postData = "grant_type=" + granttype + "&username=" + username + "&password=" + password + "";
                    lobjWebRequest.ContentType = "application/x-www-form-urlencoded";
                }
                if (granttype.Equals("client_credentials"))
                {
                    //postData = "grant_type=" + granttype + "&client_id=" + clientid + "&client_secret=" + clientsecret + "&scope=" + scope + "";
                    postData = "{\"grant_type\": \"" + granttype + "\", \"client_id\": \"" + clientid + "\", \"client_secret\": \"" + clientsecret + "\", \"scope\":\"" + scope + "\"}";
                    lobjWebRequest.ContentType = "application/json";
                    lobjWebRequest.Accept = "application/json";
                    lobjWebRequest.MediaType = "application/json";
                }

                lobjWebRequest.Method = "POST";
                ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(AcceptAllCertifications);
                lobjWebRequest.Credentials = CredentialCache.DefaultCredentials;
                lobjWebRequest.UserAgent = "Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 7.1; Trident/5.0)";
                lobjWebRequest.Accept = "/";
                lobjWebRequest.UseDefaultCredentials = true;
                lobjWebRequest.Proxy.Credentials = System.Net.CredentialCache.DefaultCredentials;
                byte[] byteArray = Encoding.UTF8.GetBytes(postData);
                lobjWebRequest.ContentLength = byteArray.Length;

                Stream dataStream = lobjWebRequest.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();

                lobjWebResponse = (HttpWebResponse)lobjWebRequest.GetResponse();

                if (lobjWebResponse.StatusCode == HttpStatusCode.OK || lobjWebResponse.StatusCode == HttpStatusCode.Created)
                {
                    lstrResponseString = ReadStream(lobjWebResponse);

                    LoggingAdapter.WriteLog(Environment.NewLine + "PostData Response = " + pstrReferenceId + Environment.NewLine + "Response = " + lstrResponseString, "TokenLogs");
                }
                else
                {
                    LoggingAdapter.WriteLog(Environment.NewLine + "PostData Response (StatusCode Not Available) = " + pstrReferenceId + Environment.NewLine + "Response = " + lstrResponseString);
                }
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog(Environment.NewLine + "PostData Exception = " + " | DateTime " + DateTime.Now + " | " + pstrReferenceId + Environment.NewLine + "Method Name = " + lstrMethodName + Environment.NewLine + "Message = " + ex.Message + Environment.NewLine + "StackTrace = " + ex.StackTrace + Environment.NewLine + "Exception = " + ex.InnerException + Environment.NewLine + "Response = " + lstrResponseString, "TokenLogs");
            }
            LoggingAdapter.WriteLog(Environment.NewLine + "TokenPostData End Response = " + lstrResponseString + " | " + pstrReferenceId + Environment.NewLine + "Method Name = " + lstrMethodName, "TokenLogs");
            return lstrResponseString;
        }

        public static string PostData(string pstrURL, string pstrMethod, string pstrMethodName, string pstrParameters, string pstrtoken, string pstrReferenceId, string pstrCategory)
        {
            string lstrResponse = string.Empty;
            HttpWebResponse lobjWebResponse = null;

            LoggingAdapter.WriteLog(Environment.NewLine + "PostData Request = | DateTime " + DateTime.Now + " | " + pstrReferenceId + Environment.NewLine + "Parameter = " + pstrParameters + Environment.NewLine + "Method Name = " + pstrMethodName + Environment.NewLine + "API URL = " + pstrURL);

            try
            {
                ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                HttpWebRequest lobjWebRequest = (HttpWebRequest)WebRequest.Create(pstrURL);
                lobjWebRequest.Timeout = 600000;
                lobjWebRequest.KeepAlive = false;
                lobjWebRequest.Method = pstrMethod;
                lobjWebRequest.ContentType = "application/json";
                lobjWebRequest.PreAuthenticate = true;
                lobjWebRequest.Headers.Add("Authorization", pstrtoken);
                lobjWebRequest.Accept = "application/json";
                lobjWebRequest.MediaType = "application/json";
                //lobjWebRequest.Proxy.Credentials = System.Net.CredentialCache.DefaultCredentials;
                
                if (pstrMethod.Equals("POST"))
                {
                    StreamWriter writer = new StreamWriter(lobjWebRequest.GetRequestStream());
                    writer.WriteLine(pstrParameters);
                    writer.Close();
                }

                ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(AcceptAllCertifications);

                lobjWebResponse = (HttpWebResponse)lobjWebRequest.GetResponse();

                if (lobjWebResponse.StatusCode == HttpStatusCode.OK || lobjWebResponse.StatusCode == HttpStatusCode.Created)
                {
                    lstrResponse = ReadStream(lobjWebResponse);

                    LoggingAdapter.WriteLog(Environment.NewLine + "PostData Response = | DateTime " + DateTime.Now + " | " + pstrReferenceId + Environment.NewLine + "Response = " + lstrResponse);
                }
                else
                {
                    LoggingAdapter.WriteLog(Environment.NewLine + "PostData Response (StatusCode Not Available) = | DateTime " + DateTime.Now + " | " + pstrReferenceId + Environment.NewLine + "Response = " + lstrResponse);
                }
            }
            catch (WebException wex)
            {
                LoggingAdapter.WriteLog(Environment.NewLine + "PostData WebException = | DateTime " + DateTime.Now + " | " + pstrReferenceId + Environment.NewLine + "Method Name = " + pstrMethodName + Environment.NewLine + "Message = " + wex.Message + Environment.NewLine + "StackTrace = " + wex.StackTrace + Environment.NewLine + "Exception = " + wex.InnerException + Environment.NewLine + "Response = " + lstrResponse);
                lstrResponse = ReadStream(wex.Response as HttpWebResponse);
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog(Environment.NewLine + "PostData Exception = | DateTime " + DateTime.Now + " | " + pstrReferenceId + Environment.NewLine + "Method Name = " + pstrMethodName + Environment.NewLine + "Message = " + ex.Message + Environment.NewLine + "StackTrace = " + ex.StackTrace + Environment.NewLine + "Exception = " + ex.InnerException + Environment.NewLine + "Response = " + lstrResponse);
                throw ex;
            }
            finally
            {
                if (lobjWebResponse != null)
                {   
                    lobjWebResponse.Close();
                }
            }
            /*LoggingAdapter.WriteLog(Environment.NewLine + "PostData End Response = " + lstrResponse + " | DateTime " + DateTime.Now + " | " + pstrReferenceId + Environment.NewLine + "Method Name = " + pstrMethodName + Environment.NewLine + "API URL = " + pstrURL);*/
            return lstrResponse;
        }

        public static ExpandoObject JsonFormatting(string pstrJsonData)
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

            return (ExpandoObject)JsonConvert.DeserializeObject(lstrResponse, typeof(ExpandoObject));
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

    }
}
