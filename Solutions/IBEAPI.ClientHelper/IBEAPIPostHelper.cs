using Framework.EnterpriseLibrary.Adapters;
using Newtonsoft.Json;
using System;
using System.Collections.Specialized;
using System.Configuration;
using System.IO;
using System.Net;

namespace IBEAPI.ClientHelper
{
    public class IBEAPIPostHelper
    {
        public static string IBEAPIUsername = Convert.ToString(ConfigurationManager.AppSettings["HotelFlightsRefererName"]);
        public static string IBEAPIPassword = Convert.ToString(ConfigurationManager.AppSettings["HotelFlightsRefererPassword"]);
        public static string EnjoyTravelAPIUsername = Convert.ToString(ConfigurationManager.AppSettings["EnjoyTravelAPIUsername"]);
        public static string EnjoyTravelAPIPassword = Convert.ToString(ConfigurationManager.AppSettings["EnjoyTravelAPIPassword"]);

        internal static string PostData(string pstrURL, string pstrMethod, string pstrMethodName, string pstrParameters, string pstrtoken, string pstrLogCategory)
        {
            string lstrResponse = string.Empty;
            HttpWebResponse lobjWebResponse = null;
            NameValueCollection NVCHeader = new NameValueCollection();

            NVCHeader.Add("UserName", IBEAPIUsername);
            NVCHeader.Add("Token", IBEAPIPassword);

            try
            {
                LoggingAdapter.WriteLog(Environment.NewLine + "IBEAPIPostHelper PostData Request = " + " | " + pstrURL + " | " + pstrMethod + " | " + pstrMethodName, pstrLogCategory);

                ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
                //ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                HttpWebRequest lobjWebRequest = (HttpWebRequest)WebRequest.Create(pstrURL);
                lobjWebRequest.Timeout = 600000;
                lobjWebRequest.KeepAlive = false;
                lobjWebRequest.Method = pstrMethod;
                lobjWebRequest.ContentType = "application/json";
                lobjWebRequest.PreAuthenticate = true;
                // lobjWebRequest.Headers.Add("Authorization:" + "bearer " + pstrtoken);
                lobjWebRequest.Accept = "application/json";
                lobjWebRequest.MediaType = "application/json";

                if (NVCHeader != null && NVCHeader.Count > 0)
                {
                    lobjWebRequest.Headers.Add(NVCHeader);
                }

                if (pstrMethod.Equals("POST"))
                {
                    StreamWriter writer = new StreamWriter(lobjWebRequest.GetRequestStream());
                    writer.WriteLine(pstrParameters);
                    writer.Close();
                }
                ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(AcceptAllCertifications);
                lobjWebResponse = (HttpWebResponse)lobjWebRequest.GetResponse();
                LoggingAdapter.WriteLog(Environment.NewLine + "IBEAPIPostHelper PostData Response = | DateTime " + DateTime.Now + " | " + Environment.NewLine + "Response = " + JsonConvert.SerializeObject(lobjWebResponse) + Environment.NewLine + "Method Name = " + pstrMethodName, pstrLogCategory);
                if (lobjWebResponse.StatusCode == HttpStatusCode.OK || lobjWebResponse.StatusCode == HttpStatusCode.Created)
                {
                    lstrResponse = ReadStream(lobjWebResponse);
                    LoggingAdapter.WriteLog(Environment.NewLine + "IBEAPIPostHelper PostData Response HttpStatusCode.OK = | DateTime " + DateTime.Now, pstrLogCategory);
                }
            }
            catch (WebException wex)
            {
                lstrResponse = ReadStream(wex.Response as HttpWebResponse);
                LoggingAdapter.WriteLog(Environment.NewLine + "IBEAPIPostHelper PostData WebException-: | DateTime " + DateTime.Now + " | " + Environment.NewLine + "Response = " + wex.Message, pstrLogCategory);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (lobjWebResponse != null)
                {
                    lobjWebResponse.Close();
                }
            }
            return lstrResponse;
        }

        internal static string PostDataAPI(string pstrURL, string pstrMethod, string pstrMethodName, string pstrParameters, string pstrtoken)
        {
            string lstrResponse = string.Empty;
            HttpWebResponse lobjWebResponse = null;
            NameValueCollection NVCHeader = new NameValueCollection();

            NVCHeader.Add("username", EnjoyTravelAPIUsername);
            NVCHeader.Add("password", EnjoyTravelAPIPassword);

            try
            {
                LoggingAdapter.WriteLog(Environment.NewLine + "PostDataAPI Request = " + " | " + pstrURL + " | " + pstrMethod + " | " + pstrMethodName);

                ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                HttpWebRequest lobjWebRequest = (HttpWebRequest)WebRequest.Create(pstrURL);
                lobjWebRequest.Timeout = 600000;
                lobjWebRequest.KeepAlive = false;
                lobjWebRequest.Method = pstrMethod;
                lobjWebRequest.ContentType = "application/json";
                lobjWebRequest.PreAuthenticate = true;
                // lobjWebRequest.Headers.Add("Authorization:" + "bearer " + pstrtoken);
                lobjWebRequest.Accept = "application/json";
                lobjWebRequest.MediaType = "application/json";

                if (NVCHeader != null && NVCHeader.Count > 0)
                {
                    lobjWebRequest.Headers.Add(NVCHeader);
                }

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
                    LoggingAdapter.WriteLog(Environment.NewLine + "PostDataAPI Response = | DateTime " + DateTime.Now);
                }
            }
            catch (WebException wex)
            {
                lstrResponse = ReadStream(wex.Response as HttpWebResponse);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (lobjWebResponse != null)
                {
                    lobjWebResponse.Close();
                }
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

        public static bool AcceptAllCertifications(object sender, System.Security.Cryptography.X509Certificates.X509Certificate certification, System.Security.Cryptography.X509Certificates.X509Chain chain, System.Net.Security.SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }
    }
}
