using Framework.EnterpriseLibrary.Adapters;
using Framework.EnterpriseLibrary.UniqueNumberGenerator;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace BeMyGuest.Implementor
{
    public class DataPostHelper
    {
        public static string PostData(string pstrURL, string pstrMethod, string pstrMethodName, string pstrParameters, string UserName, string Password)
        {
            LoggingAdapter.WriteLog(Environment.NewLine + "PostData Request =  | " + pstrURL + " | " + pstrMethod + " | " + pstrMethodName + " | " + pstrParameters);
            string text = string.Empty;
            HttpWebResponse httpWebResponse = null;
            string text2 = Get12DigitNumberDateTime();
            try
            {
                LoggingAdapter.WriteLog(Environment.NewLine + "PostData Start = " + text2 + " | DateTime " + DateTime.Now.ToString() + Environment.NewLine + "Method Name = " + pstrMethodName + Environment.NewLine + "API URL = " + pstrURL + Environment.NewLine);
                ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(pstrURL);
                httpWebRequest.Timeout = 600000;
                httpWebRequest.KeepAlive = false;
                httpWebRequest.Method = pstrMethod;
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.PreAuthenticate = true;
                NameValueCollection c = new NameValueCollection
                {
                    { "UserName", UserName },
                    { "Password", Password }
                };
                httpWebRequest.Headers.Add(c);
                httpWebRequest.Accept = "application/json";
                httpWebRequest.MediaType = "application/json";
                if (pstrMethod.Equals("POST"))
                {
                    StreamWriter streamWriter = new StreamWriter(httpWebRequest.GetRequestStream());
                    streamWriter.WriteLine(pstrParameters);
                    streamWriter.Close();
                }

                ServicePointManager.ServerCertificateValidationCallback = AcceptAllCertifications;
                httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                if (httpWebResponse.StatusCode == HttpStatusCode.OK || httpWebResponse.StatusCode == HttpStatusCode.Created)
                {
                    text = ReadStream(httpWebResponse);
                    LoggingAdapter.WriteLog(Environment.NewLine + "PostData Response = | DateTime " + DateTime.Now.ToString() + " | " + Environment.NewLine + "Response = " + text);
                }
                else
                {
                    LoggingAdapter.WriteLog(Environment.NewLine + "PostData Response (StatusCode Not Available) = " + text2 + " | DateTime " + DateTime.Now.ToString() + Environment.NewLine + "Response = " + text + Environment.NewLine);
                }

                LoggingAdapter.WriteLog(Environment.NewLine + "PostData End = " + text2 + " | DateTime " + DateTime.Now.ToString() + Environment.NewLine + "Method Name = " + pstrMethodName + Environment.NewLine + "API URL = " + pstrURL + Environment.NewLine);
            }
            catch (WebException ex)
            {
                LoggingAdapter.WriteLog(Environment.NewLine + "PostData WebException = " + text2 + " | DateTime " + DateTime.Now.ToString() + Environment.NewLine + "Method Name = " + pstrMethodName + Environment.NewLine + "Message = " + ex.Message + Environment.NewLine + "StackTrace = " + ex.StackTrace + Environment.NewLine + "Exception = " + ex.InnerException?.ToString() + Environment.NewLine + "Response = " + text);
                text = ReadStream(ex.Response as HttpWebResponse);
            }
            catch (Exception ex2)
            {
                LoggingAdapter.WriteLog(Environment.NewLine + "PostData Exception = " + text2 + " | DateTime " + DateTime.Now.ToString() + Environment.NewLine + "Method Name = " + pstrMethodName + Environment.NewLine + "Message = " + ex2.Message + Environment.NewLine + "StackTrace = " + ex2.StackTrace + Environment.NewLine + "Exception = " + ex2.InnerException?.ToString() + Environment.NewLine + "Response = " + text);
                throw ex2;
            }
            finally
            {
                httpWebResponse?.Close();
            }

            return text;
        }

        public static string PostData(string pstrURL, string pstrMethod, string pstrMethodName, string UserName, string Password)
        {
            LoggingAdapter.WriteLog(Environment.NewLine + "PostData Request =  | " + pstrURL + " | " + pstrMethod + " | " + pstrMethodName );
            string text = string.Empty;
            HttpWebResponse httpWebResponse = null;
            string text2 = Get12DigitNumberDateTime();
            try
            {
                LoggingAdapter.WriteLog(Environment.NewLine + "PostData Start = " + text2 + " | DateTime " + DateTime.Now.ToString() + Environment.NewLine + "Method Name = " + pstrMethodName + Environment.NewLine + "API URL = " + pstrURL + Environment.NewLine);
                ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(pstrURL);
                httpWebRequest.Timeout = 600000;
                httpWebRequest.KeepAlive = false;
                httpWebRequest.Method = pstrMethod;
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.PreAuthenticate = true;
                NameValueCollection c = new NameValueCollection
                {
                    { "UserName", UserName },
                    { "Password", Password }
                };
                httpWebRequest.Headers.Add(c);
                httpWebRequest.Accept = "application/json";
                httpWebRequest.MediaType = "application/json";
                //if (pstrMethod.Equals("POST"))
                //{
                //    StreamWriter streamWriter = new StreamWriter(httpWebRequest.GetRequestStream());
                //    streamWriter.WriteLine(pstrParameters);
                //    streamWriter.Close();
                //}

                ServicePointManager.ServerCertificateValidationCallback = AcceptAllCertifications;
                httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                if (httpWebResponse.StatusCode == HttpStatusCode.OK || httpWebResponse.StatusCode == HttpStatusCode.Created)
                {
                    text = ReadStream(httpWebResponse);
                    LoggingAdapter.WriteLog(Environment.NewLine + "PostData Response = | DateTime " + DateTime.Now.ToString() + " | " + Environment.NewLine + "Response = " + text);
                }
                else
                {
                    LoggingAdapter.WriteLog(Environment.NewLine + "PostData Response (StatusCode Not Available) = " + text2 + " | DateTime " + DateTime.Now.ToString() + Environment.NewLine + "Response = " + text + Environment.NewLine);
                }

                LoggingAdapter.WriteLog(Environment.NewLine + "PostData End = " + text2 + " | DateTime " + DateTime.Now.ToString() + Environment.NewLine + "Method Name = " + pstrMethodName + Environment.NewLine + "API URL = " + pstrURL + Environment.NewLine);
            }
            catch (WebException ex)
            {
                LoggingAdapter.WriteLog(Environment.NewLine + "PostData WebException = " + text2 + " | DateTime " + DateTime.Now.ToString() + Environment.NewLine + "Method Name = " + pstrMethodName + Environment.NewLine + "Message = " + ex.Message + Environment.NewLine + "StackTrace = " + ex.StackTrace + Environment.NewLine + "Exception = " + ex.InnerException?.ToString() + Environment.NewLine + "Response = " + text);
                text = ReadStream(ex.Response as HttpWebResponse);
            }
            catch (Exception ex2)
            {
                LoggingAdapter.WriteLog(Environment.NewLine + "PostData Exception = " + text2 + " | DateTime " + DateTime.Now.ToString() + Environment.NewLine + "Method Name = " + pstrMethodName + Environment.NewLine + "Message = " + ex2.Message + Environment.NewLine + "StackTrace = " + ex2.StackTrace + Environment.NewLine + "Exception = " + ex2.InnerException?.ToString() + Environment.NewLine + "Response = " + text);
                throw ex2;
            }
            finally
            {
                httpWebResponse?.Close();
            }

            return text;
        }
        private static string ReadStream(HttpWebResponse response)
        {
             StreamReader streamReader = new StreamReader(response.GetResponseStream());
            return streamReader.ReadToEnd();
        }
        public static string Get12DigitNumberDateTime()
        {
            return GenerateUniqueNumber.Get12DigitNumberDateTime();
        }

        public static bool AcceptAllCertifications(object sender, X509Certificate certification, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }
    }
}
