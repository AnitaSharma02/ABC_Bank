using Framework.EnterpriseLibrary.Adapters;
using Framework.EnterpriseLibrary.UniqueNumberGenerator;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Core.WebAPI.ClientHelper
{
    public class WebAPIHelper
    {
        public static string PostData(string pstrURL, string pstrMethod, string pstrMethodName, string pstrParameters)
        {
            LoggingAdapter.WriteLog(Environment.NewLine + "PostData Request = " + " | " + pstrURL + " | " + pstrMethod + " | " + pstrMethodName + " | " + pstrParameters);
            string lstrResponse = string.Empty;
            HttpWebResponse lobjWebResponse = null;
            string lstrReference = Get12DigitNumberDateTime();
            try
            {
                ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                LoggingAdapter.WriteLog(Environment.NewLine + "PostData Start = " + lstrReference + " | DateTime " + DateTime.Now + Environment.NewLine + "Method Name = " + pstrMethodName + Environment.NewLine + "API URL = " + pstrURL + Environment.NewLine);
                HttpWebRequest lobjWebRequest = (HttpWebRequest)WebRequest.Create(pstrURL);
                lobjWebRequest.Timeout = 600000;
                lobjWebRequest.KeepAlive = false;
                lobjWebRequest.Method = pstrMethod;
                lobjWebRequest.ContentType = "application/json";
                lobjWebRequest.PreAuthenticate = true;
                lobjWebRequest.Accept = "application/json";
                lobjWebRequest.MediaType = "application/json";               
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
                    LoggingAdapter.WriteLog(Environment.NewLine + "PostData Response = | DateTime " + DateTime.Now + " | " + Environment.NewLine + "Response = " + lstrResponse);
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

        internal static string PostData(string pstrURL, string pstrMethod, string pstrMethodName, string pstrParameters, string pstrtoken)
        {
            LoggingAdapter.WriteLog(Environment.NewLine + "PostData Request = " + " | " + pstrURL + " | " + pstrMethod + " | " + pstrMethodName + " | " + pstrParameters + " | " + pstrtoken);
            string lstrResponse = string.Empty;
            HttpWebResponse lobjWebResponse = null;
            string lstrReference = Get12DigitNumberDateTime();
            try
            {
                LoggingAdapter.WriteLog(Environment.NewLine + "PostData Start = " + lstrReference + " | DateTime " + DateTime.Now + Environment.NewLine + "Method Name = " + pstrMethodName + Environment.NewLine + "API URL = " + pstrURL + Environment.NewLine);
                ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                HttpWebRequest lobjWebRequest = (HttpWebRequest)WebRequest.Create(pstrURL);
                lobjWebRequest.Timeout = 600000;
                lobjWebRequest.KeepAlive = false;
                lobjWebRequest.Method = pstrMethod;
                lobjWebRequest.ContentType = "application/json";
                lobjWebRequest.PreAuthenticate = true;
                lobjWebRequest.Headers.Add("Authorization:" + "Bearer " + pstrtoken);
                lobjWebRequest.Accept = "application/json";
                lobjWebRequest.MediaType = "application/json";
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
                    LoggingAdapter.WriteLog(Environment.NewLine + "PostData Response = | DateTime " + DateTime.Now + " | " + Environment.NewLine + "Response = " + lstrResponse);
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


        //newly added
        internal static string PostData(string pstrURL, string pstrMethod, string pstrMethodName, string pstrParameters, string UserName, string pstrtoken)
        {
            LoggingAdapter.WriteLog(Environment.NewLine + "PostData Request = " + " | " + pstrURL + " | " + pstrMethod + " | " + pstrMethodName + " | " + pstrParameters + " | " + pstrtoken);
            string lstrResponse = string.Empty;
            HttpWebResponse lobjWebResponse = null;
            string lstrReference = Get12DigitNumberDateTime();
            try
            {
                LoggingAdapter.WriteLog(Environment.NewLine + "PostData Start = " + lstrReference + " | DateTime " + DateTime.Now + Environment.NewLine + "Method Name = " + pstrMethodName + Environment.NewLine + "API URL = " + pstrURL + Environment.NewLine);
                ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                HttpWebRequest lobjWebRequest = (HttpWebRequest)WebRequest.Create(pstrURL);
                lobjWebRequest.Timeout = 600000;
                lobjWebRequest.KeepAlive = true;
                lobjWebRequest.Method = pstrMethod;
                lobjWebRequest.ContentType = "application/json";
                lobjWebRequest.PreAuthenticate = true;
                NameValueCollection nameValueCollection = new NameValueCollection
                {
                 { "UserName", UserName },
                 { "Token", pstrtoken },
                };
                lobjWebRequest.Headers.Add(nameValueCollection);
                //lobjWebRequest.Headers.Add("Authorization:" + "bearer " + pstrtoken);
                lobjWebRequest.Accept = "application/json";
                lobjWebRequest.MediaType = "application/json";
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
                    LoggingAdapter.WriteLog(Environment.NewLine + "PostData Response = | DateTime " + DateTime.Now + " | " + Environment.NewLine + "Response = " + lstrResponse);
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

        internal static string PostDataKhalti(string pstrURL, string pstrMethod, string pstrMethodName, string pstrParameters, string UserName, string Password)
        {
            LoggingAdapter.WriteLog(Environment.NewLine + "PostData Request = " + " | " + pstrURL + " | " + pstrMethod + " | " + pstrMethodName + " | " + pstrParameters );
            string lstrResponse = string.Empty;
            HttpWebResponse lobjWebResponse = null;
            string lstrReference = Get12DigitNumberDateTime();
            try
            {
                LoggingAdapter.WriteLog(Environment.NewLine + "PostData Start = " + lstrReference + " | DateTime " + DateTime.Now + Environment.NewLine + "Method Name = " + pstrMethodName + Environment.NewLine + "API URL = " + pstrURL + Environment.NewLine);
                ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                HttpWebRequest lobjWebRequest = (HttpWebRequest)WebRequest.Create(pstrURL);
                lobjWebRequest.Timeout = 600000;
                lobjWebRequest.KeepAlive = false;
                lobjWebRequest.Method = pstrMethod;
                lobjWebRequest.ContentType = "application/json";
                lobjWebRequest.PreAuthenticate = true;
                NameValueCollection nameValueCollection = new NameValueCollection
                {
                 { "UserName", UserName },
                 {"Password" ,Password},
                };
                lobjWebRequest.Headers.Add(nameValueCollection);
                //lobjWebRequest.Headers.Add("Authorization:" + "bearer " + pstrtoken);
                lobjWebRequest.Accept = "application/json";
                lobjWebRequest.MediaType = "application/json";
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
                    LoggingAdapter.WriteLog(Environment.NewLine + "PostData Response = | DateTime " + DateTime.Now + " | " + Environment.NewLine + "Response = " + lstrResponse);
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

        private static string ReadStream(HttpWebResponse response)
        {
            using (StreamReader sr = new StreamReader(response.GetResponseStream()))
            {
                return sr.ReadToEnd();
            }
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
                LoggingAdapter.WriteLog("WebAPIHelper CommonMethods JsonFormatting Ex: " + Environment.NewLine + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
            }
            return JObject.Parse(lstrResponse);
        }
        public static string Get12DigitNumberDateTime()
        {
            return GenerateUniqueNumber.Get12DigitNumberDateTime();
        }
        public static bool AcceptAllCertifications(object sender, System.Security.Cryptography.X509Certificates.X509Certificate certification, System.Security.Cryptography.X509Certificates.X509Chain chain, System.Net.Security.SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }
    }
}
