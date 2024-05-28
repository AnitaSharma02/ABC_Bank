using SBL.ETL.DataAccess.Constants;
using Framework.EnterpriseLibrary.Adapters;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Reflection;
using System.IO;

namespace SBL.ETL.DataAccess.Wrapper
{
    public class SPWrapper
    {
        private static IConfiguration _configuration;

        static SPWrapper()
        {
            var configBuilder = new ConfigurationBuilder()
   .SetBasePath(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location))
   .AddJsonFile("appsettings.json");
            _configuration = configBuilder.Build();
            
        }

        private static string GetProcessDate()
        {
            string ProcessDateColumnName = "Execution_Date";
            string processDate = $" WHERE {ProcessDateColumnName} IS NOT NULL";
            string ProcessStartDate = _configuration["StartProcessDate"].Trim();
            string ProcessEndDate = _configuration["EndProcessDate"].Trim();

            if (ProcessStartDate != "" && ProcessEndDate != "")
            {
                return processDate += $@" AND CAST({ProcessDateColumnName} AS DATE) BETWEEN '{ProcessStartDate}' AND '{ProcessEndDate}'";
            }

            if (ProcessStartDate == "" && ProcessEndDate == "")
            {
                return processDate += $@" AND CAST({ProcessDateColumnName} AS DATE) = DATEADD(DAY,-1,CAST(GETDATE() AS DATE))";
            }

            if (ProcessStartDate != "")
            {
                return processDate += $@" AND CAST({ProcessDateColumnName} AS DATE) = '{ProcessStartDate}'";
            }

            return processDate;
        }

        public static DataSet GetETLCPD(string pstrProcessDate)
        {
            DataSet ds = new DataSet();
            try
            {
                LoggingAdapter.WriteLog("GetETLCPD : Process Date " + pstrProcessDate);
                LoggingAdapter.WriteLog("Connection Started : " + DateTime.Now.ToString());                

                using (SqlConnection lobjOC = new SqlConnection(_configuration["DBConnectionString"]))
                {
                    LoggingAdapter.WriteLog("Connection Opened : " + DateTime.Now.ToString());
                    SqlCommand lobjOCMD = new SqlCommand
                    {
                        CommandText = Convert.ToString(_configuration["CPDQuery"]).Replace("$Action_Type$", "\"action_type\"").Replace("$DATE$", "\"DATE\"").Replace("$Date$", "\"Date\"").Replace("$pstrProcessDate$", pstrProcessDate),
                        CommandType = CommandType.Text,
                        Connection = lobjOC,
                        CommandTimeout = 0,
                    };

                    lobjOCMD.CommandText = lobjOCMD.CommandText + GetProcessDate();
                    LoggingAdapter.WriteLog("CommandText :" + lobjOCMD.CommandText);
                    SqlDataAdapter lobjODA = new SqlDataAdapter(lobjOCMD);
                    lobjODA.Fill(ds);

                    DataRow newRow = ds.Tables[0].NewRow();
                    newRow["StrData"] = "Action_Type,CIF,Customer_Segment,Customer_Type,Customer_Name,Customer_Address,Mobile_Number,E-mail_address, Date_of_Birth ,Gender,Nationality,National_Identity,Passport_Number,Preferred_Language,Citizenship_No.,Registration_No.,Pan_No.,Province,District,Branch,Additional_Details_1, Additional_Details_2 ,Execution_Date";
                    ds.Tables[0].Rows.InsertAt(newRow, 0);

                }
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("GetETLCPD : Error From Wrapper:" + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
            }
            return ds;
        }
        public static DataSet GetETLBNS(string pstrProcessDate)
        {
            LoggingAdapter.WriteLog("GetETLBNS Process Date : " + pstrProcessDate);
            DataSet ds = new DataSet();
            try
            {
                LoggingAdapter.WriteLog("Connection Started : " + DateTime.Now.ToString());
                using (SqlConnection lobjOC = new SqlConnection(_configuration["DBConnectionString"]))
                {
                    LoggingAdapter.WriteLog("Connection Opened : " + DateTime.Now.ToString());
                    SqlCommand lobjOCMD = new SqlCommand
                    {
                        CommandText = Convert.ToString(_configuration["BNSQuery"]).Replace("$DATE$", "\"DATE\"").Replace("$Date$", "\"Date\"").Replace("$pstrProcessDate$", pstrProcessDate),
                        CommandType = CommandType.Text,
                        Connection = lobjOC,
                        CommandTimeout = 0,
                    };
                    lobjOCMD.CommandText = lobjOCMD.CommandText + GetProcessDate();
                    LoggingAdapter.WriteLog("CommandText :" + lobjOCMD.CommandText);
                    SqlDataAdapter lobjODA = new SqlDataAdapter(lobjOCMD);
                    lobjODA.Fill(ds);                    

                    DataRow newRow = ds.Tables[0].NewRow();
                    newRow["StrData"] = "CIF,Accrual_Channel,Product_Code,Deal_Code,Narration,Bonus_Points,Promo_Code,Additional_Details_1, Additional_Details_2 ,Execution_Date";
                    ds.Tables[0].Rows.InsertAt(newRow, 0);

                }
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("GetETLBNS : Error From Wrapper:" + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
            }
            return ds;
        }
        public static DataSet GetETLCRD(string pstrProcessDate)
        {
            LoggingAdapter.WriteLog("GetETLCRD Process Date : " + pstrProcessDate);
            DataSet ds = new DataSet();
            try
            {
                LoggingAdapter.WriteLog("Connection Started : " + DateTime.Now.ToString());
                using (SqlConnection lobjOC = new SqlConnection(_configuration["DBConnectionString"]))
                {
                    LoggingAdapter.WriteLog("Connection Opened : " + DateTime.Now.ToString());
                    SqlCommand lobjOCMD = new SqlCommand
                    {
                        CommandText = Convert.ToString(_configuration["CRDQuery"]).Replace("$DATE$", "\"DATE\"").Replace("$Date$", "\"Date\"").Replace("$pstrProcessDate$", pstrProcessDate),
                        CommandType = CommandType.Text,
                        Connection = lobjOC,
                        CommandTimeout = 0,
                    };
                    lobjOCMD.CommandText = lobjOCMD.CommandText + GetProcessDate();
                    LoggingAdapter.WriteLog("CommandText :" + lobjOCMD.CommandText);
                    SqlDataAdapter lobjODA = new SqlDataAdapter(lobjOCMD);
                    lobjODA.Fill(ds);
                }
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("GetETLCRD : Error From Wrapper:" + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
            }
            return ds;
        }
        public static DataSet GetETLTXN(string pstrProcessDate)
        {
            LoggingAdapter.WriteLog("GetETLTXN Process Date : " + pstrProcessDate);
            DataSet ds = new DataSet();
            try
            {
                using (SqlConnection lobjOC = new SqlConnection(_configuration["DBConnectionString"]))
                {
                    LoggingAdapter.WriteLog("Connection Opened : " + DateTime.Now.ToString());
                    SqlCommand lobjOCMD = new SqlCommand
                    {
                        CommandText = Convert.ToString(_configuration["TXNQuery"]).Replace("$DATE$", "\"DATE\"").Replace("$Date$", "\"Date\"").Replace("$pstrProcessDate$", pstrProcessDate),
                        CommandType = CommandType.Text,
                        Connection = lobjOC,
                        CommandTimeout = 0,
                    };
                    lobjOCMD.CommandText = lobjOCMD.CommandText + GetProcessDate();
                    LoggingAdapter.WriteLog("CommandText :" + lobjOCMD.CommandText);
                    SqlDataAdapter lobjODA = new SqlDataAdapter(lobjOCMD);
                    lobjODA.Fill(ds);

                    DataRow newRow = ds.Tables[0].NewRow();
                    newRow["StrData"] = "CIF,Accrual_Channel,Bin_Number,Product_Code,sub_product_code,Transaction_Date,Transaction_Time,Transaction_Id, Merchant _Id ,Merchant_Category_Code,Merchant_Name,Merchant_City,Merchant_Country,Terminal-id,Transaction_Type.,Transaction_Code,Transaction_Amount,Source_Amount,Source_Currency,National_Id,Additional_Details_1, Additional_Details_2 ,Execution_Date";
                    ds.Tables[0].Rows.InsertAt(newRow, 0);
                }
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("GetETLTXN : Error From Wrapper:" + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
            }
            return ds;
        }
    }
}
