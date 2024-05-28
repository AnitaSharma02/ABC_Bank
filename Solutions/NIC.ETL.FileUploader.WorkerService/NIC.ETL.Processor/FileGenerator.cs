using SBL.ETL.BusinessFacade;
using SBL.ETL.Entities;
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
using SBL.ETL.WebAPI.Helper;

namespace SBL.ETL.Processor
{
    public class FileGenerator
    {
        private static IConfiguration _configuration;
        private readonly string lstrSuccessfulFiles ;
        private readonly string lstrIntermediate ;
        private readonly string lstrFileTypes ;
        private string lstrProcessDate ;
        private string lstrMstrEmail = "<p> Dear Admin,</p><p style=margin-left: 40px> Please Find Following file list $$Template_Details$$ </p>";
        private string lstrFileList = @"<Table border=1><Tr><Th>File Name</Th><Th>Processed Date</Th><Th>Number Of Records</Th></Tr>";
        int lintTotalRecord = 0;

        public FileGenerator()
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

                lstrSuccessfulFiles = Convert.ToString(_configuration["SuccessfulFiles"]);
                lstrIntermediate = Convert.ToString(_configuration["Intermediate"]);
                lstrFileTypes = Convert.ToString(_configuration["FileTypes"]);
                lstrProcessDate = Convert.ToString(_configuration["ProcessDate"]);

            }
        }

        public FileGenerator(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public bool ProcessAllInProcess()
        {
            string lstrExternalReference = GenerateUniqueNumber.Get12DigitNumberDateTime();
            LoggingAdapter.WriteLog("FileGenerator GenerateFileProcessor Start = " + DateTime.Now + " | External Ref = " + lstrExternalReference);
            bool lblnResponse = false;
            if (string.IsNullOrEmpty(lstrProcessDate))
            {
                lstrProcessDate = DateTime.Now.AddDays(-1).ToString("dd/MM/yyyy");
            }
            try
            {
                foreach (var lstrFileType in lstrFileTypes.Split(','))
                {
                    StringBuilder lsbResponse = new StringBuilder();
                    string lstrFileName = "SBL" + lstrFileType + DateTime.Now.ToString("ddMMyyyy");
                    Facade lobjETLFacade = new Facade();
                    List<ETLDetails> llojETLDetailsLst = new List<ETLDetails>();
                    switch (lstrFileType)
                    {
                        case "CPD":
                            llojETLDetailsLst = lobjETLFacade.GetETLCPD(lstrProcessDate);
                            break;
                        case "TXN":
                            llojETLDetailsLst = lobjETLFacade.GetETLTXN(lstrProcessDate);
                            break;
                        case "BNS":
                            llojETLDetailsLst = lobjETLFacade.GetETLBNS(lstrProcessDate);
                            break;
                        case "CRD":
                            llojETLDetailsLst = lobjETLFacade.GetETLCRD(lstrProcessDate);
                            break;
                    }

                    if (llojETLDetailsLst != null)
                    {
                        lintTotalRecord = llojETLDetailsLst.Count-1;
                        LoggingAdapter.WriteLog("FileGenerator GenerateProcessor Response Count = " + lintTotalRecord + " | External Ref = " + lstrExternalReference);                        
                    }


                        if (llojETLDetailsLst != null && llojETLDetailsLst.Count > 1)
                    {
                        
                        for (int i = 0; i < llojETLDetailsLst.Count; i++)
                        {
                            lsbResponse.Append(llojETLDetailsLst[i].StrData + "\r\n");
                        }                        
                        string fileLoc;
                        fileLoc = lstrSuccessfulFiles + "/" + lstrFileName.ToUpper().Replace(".txt", "") + ".csv";
                        if (!File.Exists(fileLoc))
                            File.Create(fileLoc).Close();
                        File.WriteAllText(fileLoc, Convert.ToString(lsbResponse).TrimEnd());

                        fileLoc = lstrIntermediate + "/" + lstrFileName.ToUpper().Replace(".txt", "") + ".csv";
                        if (!File.Exists(fileLoc))
                            File.Create(fileLoc).Close();
                        File.WriteAllText(fileLoc, Convert.ToString(lsbResponse).TrimEnd());
                        lstrFileList += @"<Tr><Td>" + lstrFileName.ToUpper().Replace(".txt", "") + "</Td><Td><center>" + DateTime.Today.ToString("dd/MM/yyyy") + "</center></Td><Td><center>" + lintTotalRecord + "</center></Td></Tr>";
                    }
                    else
                    {
                        lstrFileList += @"<Tr><Td>" + lstrFileType + "</Td><Td><center>" + DateTime.Today.ToString("dd/MM/yyyy") + "</center></Td><Td><center>0</center></Td></Tr>";
                    }
                }
                //TODO Temporary
                //SendProcessedFileEmail();
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("FileGenerator ProcessAllInProcess Exception = " + ex.Message + " | " + ex.StackTrace + " | " + ex.InnerException + " | External Ref = " + lstrExternalReference);
            }
            LoggingAdapter.WriteLog("FileGenerator End = " + DateTime.Now + " | External Ref = " + lstrExternalReference);
            return lblnResponse;
        }        

        public void SendProcessedFileEmail()
        {
            try
            {
                string AdminEmail = _configuration["AdminEmail"];
                List<string> pstrEmailparameter = new List<string>();
                lstrFileList += "</Table>";
                lstrMstrEmail = lstrMstrEmail.Replace("$$Template_Details$$", lstrFileList);
                pstrEmailparameter.Add(lstrMstrEmail);
                
                ProgramDefinition lobjProgramDefinition = APIHelper.GetProgramMaster(_configuration["ProgramName"].ToString());
                EmailDetails lobjEmailDetail = new EmailDetails
                {
                    TemplateCode = "ETLFileGeneratorProcess",
                    ListParameter = pstrEmailparameter,
                    To= AdminEmail,
                    ProgramId = Convert.ToString(lobjProgramDefinition.ProgramId),
                    AttachmentList = new List<string>()
                };
                List<Attachments> lobjlstAttachments = new List<Attachments>();

                APIHelper.InsertEmailDetails(lobjEmailDetail);
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("FileGenerator File Parsing Send Email Error -" + ex.Message + " Stack trace -" + ex.StackTrace);
            }
        }
    }
}
