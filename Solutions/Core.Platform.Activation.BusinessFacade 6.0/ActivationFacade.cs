using Core.Platform.Member.Entites;
using Core.Platform.MemberManagement.BusinessFacade;
using Core.Platform.MemberManagement.DataAccess.Wrapper;
using Core.Platform.OTP.Entities;
using Core.Platform.OTP.Facade;
using Framework.EnterpriseLibrary.Adapters;
using Framework.EnterpriseLibrary.CommunicationEngine.BusinessFacade;
using Framework.EnterpriseLibrary.CommunicationEngine.Entity;
using Framework.EnterpriseLibrary.PasswordGenerator;
using Framework.EnterpriseLibrary.Security;
using Framework.EnterpriseLibrary.Security.Constants;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace Core.Platform.Activation.BusinessFacade
{
    /// <summary>
    /// Business Facade For Activation
    /// </summary>
    public class ActivationFacade
    {
        public static string lstrProgramName = string.Empty;
        public static string lstrCulture = string.Empty;
        private IConfiguration _configuration;
        public ActivationFacade(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        /// <summary>
        /// Get Member Details
        /// </summary>
        /// <param name="pobjActivationParameters">contains RelationReference,ProgramId,RelationType etc</param>
        /// <returns>lobjMemberDetails</returns>
        public MemberDetails GetMemberDetails(ActivationParameters pobjActivationParameters)
        {
            MemberBusinessFacade lobjMemberBusinessFacade = new MemberBusinessFacade();
            MemberDetails lobjMemberDetails = lobjMemberBusinessFacade.GetMemberDetailsForActivation(pobjActivationParameters);
            //if (lobjMemberDetails != null)
            //{
            //    if (lobjMemberDetails.MemberRelationsList[0].IsAccountActivated)
            //        throw new ApplicationException("Account Already Activated");
            //}
            return lobjMemberDetails != null ? lobjMemberDetails : new MemberDetails();
        }
        /// <summary>
        /// Get Member Details By Verve ID and National Id
        /// </summary>
        /// <param name="pobjActivationParameters">contains RelationReference,NationalId,ProgramId,RelationType etc</param>
        /// <returns>lobjMemberDetails</returns>
        public MemberDetails GetMemberDetailsByMemberIdNationalId(ActivationParameters pobjActivationParameters)
        {
            MemberBusinessFacade lobjMemberBusinessFacade = new MemberBusinessFacade();
            MemberDetails lobjMemberDetails = lobjMemberBusinessFacade.GetMemberDetailsForActivationByMemberIdNationalId(pobjActivationParameters);
            return lobjMemberDetails != null ? lobjMemberDetails : new MemberDetails();
        }
        /// <summary>
        /// Get Member Details Using National ID
        /// </summary>
        /// <param name="pobjActivationParameters">contains NationalId,ProgramId,RelationType etc</param>
        /// <returns>lobjMemberDetails</returns>
        public MemberDetails GetMemberDetailsUsingNationalID(ActivationParameters pobjActivationParameters)
        {
            MemberBusinessFacade lobjMemberBusinessFacade = new MemberBusinessFacade();
            SearchMember lobjSearchMember = new SearchMember();
            lobjSearchMember.NationalId = pobjActivationParameters.NationalId;
            lobjSearchMember.ProgramId = pobjActivationParameters.ProgramId;
            lobjSearchMember.RelationType = pobjActivationParameters.RelationType;
            MemberDetails lobjMemberDetails = lobjMemberBusinessFacade.GetMemberDetailsUsingNationalID(lobjSearchMember);
            //if (lobjMemberDetails != null)
            //{
            //    if (lobjMemberDetails.MemberRelationsList[0].IsAccountActivated)
            //        throw new ApplicationException("Account Already Activated");
            //}
            return lobjMemberDetails != null ? lobjMemberDetails : new MemberDetails();
        }
        /// <summary>
        /// Activation Successful for Member Account
        /// </summary>
        /// <param name="pobjMemberDetails">contains MemberId,WebPassword,RelationReference,RelationType etc</param>
        /// <returns>pobjMemberDetails</returns>
        public MemberDetails ActivationSuccessful(MemberDetails pobjMemberDetails)
        {
            LoggingAdapter.WriteLog("RelationReference Number= " + pobjMemberDetails.MemberRelationsList[0].RelationReference + " Password= " + pobjMemberDetails.MemberRelationsList[0].WebPassword);
            PasswordDefinition lobjPasswordDefinition = new PasswordDefinition();
            string lstrPasswordDefinition = Convert.ToString(RandomPasswordGenerator.GeneratePassword(lobjPasswordDefinition));
            pobjMemberDetails.MemberRelationsList[0].WebPassword = RSAEncryptor.EncryptString((lstrPasswordDefinition), RSASecurityConstant.KeySize, RSASecurityConstant.RSAPublicKey);
            SPWrapper.ActivateMemberAccount(pobjMemberDetails);
            return pobjMemberDetails;
        }
        /// <summary>
        /// Activte User Account
        /// </summary>
        /// <param name="pobjMemberDetails">contains MemberId,WebPassword,RelationReference,RelationType etc</param>
        /// <returns>bool</returns>
        public bool ActivteUser(MemberDetails pobjMemberDetails)
        {
            bool lblnActivateUser = false;
            LoggingAdapter.WriteLog("RelationReference Number= " + pobjMemberDetails.MemberRelationsList[0].RelationReference + " Password= " + pobjMemberDetails.MemberRelationsList[0].WebPassword);
            PasswordDefinition lobjPasswordDefinition = new PasswordDefinition();
            //pobjMemberDetails.MemberRelationsList[0].WebPassword = RSAEncryptor.EncryptString(Convert.ToString(RandomPasswordGenerator.GeneratePassword(lobjPasswordDefinition)), RSASecurityConstant.KeySize, RSASecurityConstant.RSAPublicKey);
            pobjMemberDetails.MemberRelationsList[0].WebPassword = RSAEncryptor.EncryptString(Convert.ToString(pobjMemberDetails.MemberRelationsList[0].WebPassword), RSASecurityConstant.KeySize, RSASecurityConstant.RSAPublicKey);
            lblnActivateUser = SPWrapper.ActivateMemberAccount(pobjMemberDetails);
            return lblnActivateUser;
        }
        /// <summary>
        /// Activte User For Link
        /// </summary>
        /// <param name="pobjMemberDetails">contains Id,Email,WebPassword,RelationReference,RelationType etc</param>
        /// <returns>bool</returns>
        public bool ActivteUserForLink(MemberDetails pobjMemberDetails)
        {
            bool lblnActivateUser = false;
            LoggingAdapter.WriteLog("RelationReference Number= " + pobjMemberDetails.MemberRelationsList[0].RelationReference + " Password= " + pobjMemberDetails.MemberRelationsList[0].WebPassword);
            PasswordDefinition lobjPasswordDefinition = new PasswordDefinition();
            //pobjMemberDetails.MemberRelationsList[0].WebPassword = RSAEncryptor.EncryptString(Convert.ToString(RandomPasswordGenerator.GeneratePassword(lobjPasswordDefinition)), RSASecurityConstant.KeySize, RSASecurityConstant.RSAPublicKey);
            pobjMemberDetails.MemberRelationsList.Find(x => x.RelationType.Equals(RelationType.LBMS)).WebPassword = RSAEncryptor.EncryptString(Convert.ToString(pobjMemberDetails.MemberRelationsList.Find(x => x.RelationType.Equals(RelationType.LBMS)).WebPassword), RSASecurityConstant.KeySize, RSASecurityConstant.RSAPublicKey);
            lblnActivateUser = SPWrapper.ActivateMemberAccountForLink(pobjMemberDetails);
            return lblnActivateUser;
        }
        /// <summary>
        /// Activte User After Link
        /// </summary>
        /// <param name="pobjMemberDetails">contains MemberId,WebPassword,RelationReference,RelationType etc</param>
        /// <returns>bool</returns>
        public bool ActivteUserAfterLink(MemberDetails pobjMemberDetails)
        {
            bool lblnActivateUser = false;
            try
            {
                lblnActivateUser = SPWrapper.ActivateMemberAccount(pobjMemberDetails);
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("ActivteUserAfterLink ex-" + ex.Message + ex.InnerException + ex.StackTrace);
            }
            return lblnActivateUser;
        }
        /// <summary>
        ///Activte User By CIF National Id
        /// </summary>
        /// <param name="pobjMemberDetails">contains for UpdateMemberEmailId  Email,LastName,ProgramId,NationalId,Id etc
        /// contains for ActivateMemberAccount WebPassword,RelationReference,RelationType etc</param>
        /// <returns>bool</returns>
        public bool ActivteUserByCIFNationalId(MemberDetails pobjMemberDetails)
        {
            if (pobjMemberDetails != null)
            {
                PasswordDefinition lobjPasswordDefinition = new PasswordDefinition();
                pobjMemberDetails.MemberRelationsList[0].WebPassword = RSAEncryptor.EncryptString(Convert.ToString(pobjMemberDetails.MemberRelationsList[0].WebPassword), RSASecurityConstant.KeySize, RSASecurityConstant.RSAPublicKey);
                SPWrapper.UpdateMemberEmailId(pobjMemberDetails);
                SPWrapper.ActivateMemberAccount(pobjMemberDetails);

                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// Deactive Member Account
        /// </summary>
        /// <param name="pobjMemberDetails">contains RelationReference,Status,RelationType etc</param>
        /// <returns>bool</returns>
        public bool DeactiveMemberAccount(MemberDetails pobjMemberDetails)
        {
            return SPWrapper.DeactiveMemberAccount(pobjMemberDetails);
        }
        /// <summary>
        /// Send Email For Activation
        /// </summary>
        /// <param name="pobjMemberDetails">contains NationalId,RelationReference,ProgramId etc</param>
        /// <returns>pobjMemberDetails</returns>
        public MemberDetails SendEmailForActivation(MemberDetails pobjMemberDetails)
        {

            //MasterFacade objMasterFacade = new MasterFacade();
            //EmailTemplate lobjEmailTemplate = objMasterFacade.GetEmailTemplate("ActivationSuccessful");
            //MailComponent lobjComponent = new MailComponent();
            //EmailParameters lobjParams = new EmailParameters();
            //lobjParams.MailTo = pobjMemberDetails.Email;
            //lobjParams.MailFrom = ConfigurationManager.AppSettings["MailFrom"].ToString();
            //lobjParams.MailSubject = lobjEmailTemplate.Subject;//"Thank you for activating FlyWards Account.";
            //lobjParams.MailBody = lobjEmailTemplate.TemplateDetails;
            //lobjParams.MailBody = lobjParams.MailBody.Replace("$$Password$$", RSAEncryptor.DecryptString(pobjMemberDetails.MemberRelationsList[0].WebPassword, RSASecurityConstant.KeySize, RSASecurityConstant.PrivateKey));
            //lobjParams.MailBody = lobjParams.MailBody.Replace("$$RelationReference$$", pobjMemberDetails.MemberRelationsList[0].RelationReference);
            //lobjParams.MailBody = lobjParams.MailBody.Replace("$$LastName$$", pobjMemberDetails.LastName);
            ////lobjParams.MailBody = lobjParams.MailBody.Replace("$$Password$$", pobjMemberDetails.MemberRelationsList[0].WebPassword);
            //lobjParams.SMTPUsername = ConfigurationManager.AppSettings["SMTPUsername"].ToString();
            //lobjParams.SMTPPassword = ConfigurationManager.AppSettings["SMTPPassword"].ToString();
            //lobjParams.SMTPServer = ConfigurationManager.AppSettings["SMTPServer"].ToString();
            //lobjParams.SMTPPort = Convert.ToInt32(ConfigurationManager.AppSettings["SMTPPort"].ToString());
            //lobjParams.CC = Convert.ToString(ConfigurationManager.AppSettings["CC"].ToString());
            //lobjParams.BCC = Convert.ToString(ConfigurationManager.AppSettings["BCC"].ToString());
            //bool lblnEmailSend = lobjComponent.Send(lobjParams, new List<Attachment>());

            //SPWrapper.ActivateMemberAccount(pobjMemberDetails);
            try
            {
                if (pobjMemberDetails.Email != string.Empty)
                {
                    string lsrtTemplateLangCode = "";
                    if (pobjMemberDetails.PreferredLanguage.ToUpper() != "EN")
                    {
                        lsrtTemplateLangCode = pobjMemberDetails.PreferredLanguage.ToUpper();
                    }

                    // Communication Engine Call FOr Email.
                    EmailDetails lobjEmailDetail = new EmailDetails();
                    List<string> lstEmailparameter = new List<string>();
                    List<string> lstAttachment = new List<string>();
                    CeFacade lobjcehelper = new CeFacade();
                    List<Attachments> lobjAttachments = null;
                    lstEmailparameter.Add(pobjMemberDetails.LastName);
                    lstEmailparameter.Add(pobjMemberDetails.NationalId);
                    lstEmailparameter.Add(pobjMemberDetails.MemberRelationsList.Find(x => x.RelationType.Equals(RelationType.LBMS)).RelationReference);
                    //lstEmailparameter.Add(pobjMemberDetails.MemberRelationsList[0].RelationReference);
                    //lstEmailparameter.Add(RSAEncryptor.DecryptString(pobjMemberDetails.MemberRelationsList[0].WebPassword, RSASecurityConstant.KeySize, RSASecurityConstant.PrivateKey));
                    lobjEmailDetail.TemplateCode = lsrtTemplateLangCode + "MemberActivation";
                    lobjEmailDetail.ListParameter = lstEmailparameter;
                    lobjEmailDetail.MemberId = Convert.ToString(pobjMemberDetails.MemberRelationsList.Find(lobj => lobj.RelationType.Equals(RelationType.LBMS)).RelationReference);
                    lobjEmailDetail.ProgramId = Convert.ToString(pobjMemberDetails.ProgramId);
                    lobjEmailDetail.AttachmentList = lstAttachment;
                    lobjEmailDetail.To = pobjMemberDetails.Email;
                    lobjcehelper.InsertEmailDetails(lobjEmailDetail, lobjAttachments);
                }
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("Activation Email :" + ex.Message);
            }

            try
            {
                if (pobjMemberDetails.MobileNumber != string.Empty)
                {
                    string lsrtTemplateLangCode = "";
                    if (pobjMemberDetails.PreferredLanguage.ToUpper() != "EN")
                    {
                        lsrtTemplateLangCode = pobjMemberDetails.PreferredLanguage.ToUpper();
                    }

                    //Communication Engine Call For SMS. 
                    CeFacade lobjcehelper = new CeFacade();
                    SmsDetails lobjSmsDetail = new SmsDetails();
                    List<string> lstSmsparameter = new List<string>();
                    lobjSmsDetail.TemplateCode = lsrtTemplateLangCode + "MemberActivation";
                    lstSmsparameter.Add(pobjMemberDetails.LastName);
                    lstSmsparameter.Add(pobjMemberDetails.NationalId);
                    lstSmsparameter.Add(pobjMemberDetails.MemberRelationsList.Find(x => x.RelationType.Equals(RelationType.LBMS)).RelationReference);
                    lobjSmsDetail.ListParameter = lstSmsparameter;
                    lobjSmsDetail.ProgramId = Convert.ToString(pobjMemberDetails.ProgramId);
                    lobjSmsDetail.MemberId = Convert.ToString(pobjMemberDetails.MemberRelationsList.Find(lobj => lobj.RelationType.Equals(RelationType.LBMS)).RelationReference);
                    lobjSmsDetail.ReceiverMobile = Convert.ToString(pobjMemberDetails.MobileNumber);
                    lobjcehelper.InsertSmsDetails(lobjSmsDetail);
                }
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("Activation SMS :" + ex.Message);
            }

            LoggingAdapter.WriteLog("RelationReference Number= " + pobjMemberDetails.MemberRelationsList[0].RelationReference + " Password= " + RSAEncryptor.DecryptString(pobjMemberDetails.MemberRelationsList[0].WebPassword, RSASecurityConstant.KeySize, RSASecurityConstant.PrivateKey));
            return pobjMemberDetails;
        }
        /// <summary>
        /// Send Email For Activation By National Id
        /// </summary>
        /// <param name="pobjMemberDetails">contains NationalId,RelationReference,ProgramId etc</param>
        /// <returns>pobjMemberDetails</returns>
        public MemberDetails SendEmailForActivationByNationalId(MemberDetails pobjMemberDetails)
        {
            try
            {
                if (pobjMemberDetails.Email != string.Empty)
                {
                    string lsrtTemplateLangCode = "";
                    if (pobjMemberDetails.PreferredLanguage.ToUpper() != "EN")
                    {
                        lsrtTemplateLangCode = pobjMemberDetails.PreferredLanguage.ToUpper();
                    }

                    // Communication Engine Call FOr Email.
                    EmailDetails lobjEmailDetail = new EmailDetails();
                    List<string> lstEmailparameter = new List<string>();
                    List<string> lstAttachment = new List<string>();
                    CeFacade lobjcehelper = new CeFacade();
                    List<Attachments> lobjAttachments = null;
                    lstEmailparameter.Add(pobjMemberDetails.LastName);
                    lstEmailparameter.Add(pobjMemberDetails.NationalId);
                    lstEmailparameter.Add(RSAEncryptor.DecryptString(pobjMemberDetails.MemberRelationsList[0].WebPassword, RSASecurityConstant.KeySize, RSASecurityConstant.PrivateKey));
                    lobjEmailDetail.TemplateCode = lsrtTemplateLangCode + "MemberActivation";
                    lobjEmailDetail.ListParameter = lstEmailparameter;
                    lobjEmailDetail.MemberId = Convert.ToString(pobjMemberDetails.MemberRelationsList.Find(lobj => lobj.RelationType.Equals(RelationType.LBMS)).RelationReference);
                    lobjEmailDetail.ProgramId = Convert.ToString(pobjMemberDetails.ProgramId);
                    lobjEmailDetail.AttachmentList = lstAttachment;
                    lobjEmailDetail.To = pobjMemberDetails.Email;
                    lobjcehelper.InsertEmailDetails(lobjEmailDetail, lobjAttachments);
                }
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("Activation Email :" + ex.Message);
            }

            try
            {
                if (pobjMemberDetails.MobileNumber != string.Empty)
                {
                    string lsrtTemplateLangCode = "";
                    if (pobjMemberDetails.PreferredLanguage.ToUpper() != "EN")
                    {
                        lsrtTemplateLangCode = pobjMemberDetails.PreferredLanguage.ToUpper();
                    }

                    //Communication Engine Call For SMS. 
                    CeFacade lobjcehelper = new CeFacade();
                    SmsDetails lobjSmsDetail = new SmsDetails();
                    List<string> lstSmsparameter = new List<string>();
                    lobjSmsDetail.TemplateCode = lsrtTemplateLangCode + "MemberActivation";
                    lstSmsparameter.Add(RSAEncryptor.DecryptString(pobjMemberDetails.MemberRelationsList[0].WebPassword, RSASecurityConstant.KeySize, RSASecurityConstant.PrivateKey));
                    lobjSmsDetail.ListParameter = lstSmsparameter;
                    lobjSmsDetail.ProgramId = Convert.ToString(pobjMemberDetails.ProgramId);
                    lobjSmsDetail.MemberId = Convert.ToString(pobjMemberDetails.NationalId);
                    lobjSmsDetail.ReceiverMobile = Convert.ToString(pobjMemberDetails.MobileNumber);
                    lobjcehelper.InsertSmsDetails(lobjSmsDetail);
                }
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("Activation SMS :" + ex.Message);
            }
            return pobjMemberDetails;
        }
        //private MemberDetails SendSMSForActivation(MemberDetails pobjMemberDetails)
        //{
        //    //Framework.EnterpriseLibrary.Adapters.LoggingAdapter.WriteLog("SendEmailForActivation Member  mobile number" + pobjMemberDetails.MemberRelationsList[0].RelationReference);
        //    return pobjMemberDetails;
        //}
        //public void ActivationUnSuccessful()
        //{
        //    MasterFacade lobjMasterFacade = new MasterFacade();
        //    ProgramMaster lobjProgramMaster = null;
        //    lobjProgramMaster = lobjMasterFacade.GetProgramDetails(lstrProgramName);
        //    //throw new ApplicationException("Invalid info");
        //    throw new ApplicationException(ExceptionMessagesFacade.GetErrorMessage(102, lstrCulture, lobjProgramMaster.Id));
        //    // //Framework.EnterpriseLibrary.Adapters.LoggingAdapter.WriteLog(System.DateTime.Now + " " + "ActivationUnSuccessful Process Started");
        //    //Throw ApplicationException with the Message
        //}
        /// <summary>
        /// Generate OTP Details
        /// </summary>
        /// <param name="pobjOTPDetails">contains UniquerefID,ProgramId,RelationType etc</param>
        /// <returns>bool</returns>
        public bool GenerateOTPDetails(OTPDetails pobjOTPDetails)
        {
            if (!GenerateOTP(pobjOTPDetails))
                return false;
            return true;
        }
        /// <summary>
        /// Generate OTP Details QIB
        /// </summary>
        /// <param name="pobjOTPDetails">contains UniquerefID,ProgramId,RelationType etc</param>
        /// <returns>bool</returns>
        public bool GenerateOTPDetailsQIB(OTPDetails pobjOTPDetails)
        {
            if (!GenerateOTPQIB(pobjOTPDetails))
                return false;
            return true;
        }
        /// <summary>
        /// Generate OTP QIB
        /// </summary>
        /// <param name="pobjOTPDetails">contains UniquerefID,ProgramId,RelationType etc</param>
        /// <returns>bool</returns>
        public bool GenerateOTPQIB(OTPDetails pobjOTPDetails)
        {
            bool lboolStatus = false;
            try
            {
                OTPFacade lobjOTPFacade = new OTPFacade();
                OTPDetails lobjOTPDetails = lobjOTPFacade.GenerateOTP(pobjOTPDetails);
                if (lobjOTPDetails != null)
                {
                    ActivationParameters lobjActivationParameters = new ActivationParameters();
                    lobjActivationParameters.RelationReference = pobjOTPDetails.UniquerefID;
                    lobjActivationParameters.ProgramId = pobjOTPDetails.ProgramId;
                    lobjActivationParameters.RelationType = pobjOTPDetails.RelationType;
                    MemberBusinessFacade lobjMemberBusinessFacade = new MemberBusinessFacade();
                    MemberDetails lobjMemberDetails = lobjMemberBusinessFacade.GetMemberDetailsForActivation(lobjActivationParameters);
                    if (pobjOTPDetails.OtpEnumTypes.Equals(OTPEnumTypes.POINTTRANSFERCONFIRM))
                    {
                        SendEmailForPointTransferOTP(lobjMemberDetails, lobjOTPDetails.OTP.ToString(), lobjOTPDetails.ExpiryDateTime, pobjOTPDetails);
                        SendSMSForPointTransferOTP(lobjMemberDetails, lobjOTPDetails.OTP.ToString(), lobjOTPDetails.ExpiryDateTime, pobjOTPDetails);
                    }
                    else
                    {
                        //SendEmail(lobjMemberDetails, lobjOTPDetails.OTP.ToString(), lobjOTPDetails.ExpiryDateTime);
                        SendSMS(lobjMemberDetails, lobjOTPDetails.OTP.ToString(), lobjOTPDetails.ExpiryDateTime);
                    }
                    lboolStatus = true;
                }
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("GenerateOTPQIB Ex-" + ex.InnerException + ex.Message + ex.StackTrace);
            }
            return lboolStatus;
        }
        /// <summary>
        /// Generate OTP
        /// </summary>
        /// <param name="pobjOTPDetails">contains UniquerefID,ProgramId,RelationType etc</param>
        /// <returns>bool</returns>
        public bool GenerateOTP(OTPDetails pobjOTPDetails)
        {
            bool lboolStatus = false;
            try
            {
                OTPFacade lobjOTPFacade = new OTPFacade();
                OTPDetails lobjOTPDetails = lobjOTPFacade.GenerateOTP(pobjOTPDetails);
                if (lobjOTPDetails != null)
                {
                    LoggingAdapter.WriteLog("OTP Not Null");
                    ActivationParameters lobjActivationParameters = new ActivationParameters();
                    lobjActivationParameters.RelationReference = pobjOTPDetails.UniquerefID;
                    lobjActivationParameters.ProgramId = pobjOTPDetails.ProgramId;
                    lobjActivationParameters.RelationType = pobjOTPDetails.RelationType;
                    MemberBusinessFacade lobjMemberBusinessFacade = new MemberBusinessFacade();
                    MemberDetails lobjMemberDetails = lobjMemberBusinessFacade.GetMemberDetailsForActivation(lobjActivationParameters);
                    if (pobjOTPDetails.OtpEnumTypes.Equals(OTPEnumTypes.POINTTRANSFERCONFIRM))
                    {
                        SendEmailForPointTransferOTP(lobjMemberDetails, lobjOTPDetails.OTP.ToString(), lobjOTPDetails.ExpiryDateTime, pobjOTPDetails);
                        SendSMSForPointTransferOTP(lobjMemberDetails, lobjOTPDetails.OTP.ToString(), lobjOTPDetails.ExpiryDateTime, pobjOTPDetails);
                    }
                    else
                    {

                        SendEmail(lobjMemberDetails, lobjOTPDetails.OTP.ToString(), lobjOTPDetails.ExpiryDateTime);
                        SendSMS(lobjMemberDetails, lobjOTPDetails.OTP.ToString(), lobjOTPDetails.ExpiryDateTime);
                    }
                    lboolStatus = true;
                }
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("GenerateOTP Ex-" + ex.InnerException + ex.Message + ex.StackTrace);
            }
            return lboolStatus;
        }

        /// <summary>
        /// Forgot Generate OTP
        /// </summary>
        /// <param name="pobjOTPDetails">contains UniquerefID,OtpType,SourceCode etc</param>
        /// <returns>bool</returns>
        public bool ForgotGenerateOTP(OTPDetails pobjOTPDetails)
        {
            OTPFacade lobjOTPFacade = new OTPFacade();
            OTPDetails lobjOTPDetails = lobjOTPFacade.GenerateOTP(pobjOTPDetails);
            if (lobjOTPDetails != null)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Send Email
        /// </summary>
        /// <param name="lobjMemberDetails">contains TemplateCode,MemberId,ProgramId etc</param>
        /// <param name="pstrOTPstirng">contains pstrOTPstirng</param>
        /// <param name="pdtExpirydatetime">contains pdtExpirydatetime</param>
        /// <returns>bool</returns>
        private bool SendEmail(MemberDetails lobjMemberDetails, string pstrOTPstirng, DateTime pdtExpirydatetime)
        {
            bool lblnEmailSend = false;
            try
            {
                if (lobjMemberDetails.Email != string.Empty)
                {
                    string lsrtTemplateLangCode = "";
                    if (lobjMemberDetails.PreferredLanguage.ToUpper() != "EN")
                    {
                        lsrtTemplateLangCode = lobjMemberDetails.PreferredLanguage.ToUpper();
                    }
                    string lstrexpirydt = pdtExpirydatetime.ToString("dd/MM/yyyy");
                    EmailDetails lobjEmailDetail = new EmailDetails();
                    List<string> lstEmailparameter = new List<string>();
                    List<string> lstAttachment = new List<string>();
                    CeFacade lobjcehelper = new CeFacade();
                    List<Attachments> lobjAttachments = null;
                    lstEmailparameter.Add(pstrOTPstirng);
                    lstEmailparameter.Add(lobjMemberDetails.FullName);
                    lobjEmailDetail.TemplateCode = lsrtTemplateLangCode + "SendOTP";
                    lobjEmailDetail.ListParameter = lstEmailparameter;
                    lobjEmailDetail.AttachmentList = lstAttachment;
                    lobjEmailDetail.To = lobjMemberDetails.Email;
                    lobjEmailDetail.MemberId = Convert.ToString(lobjMemberDetails.MemberRelationsList[0].RelationReference);
                    lobjEmailDetail.ProgramId = Convert.ToString(lobjMemberDetails.ProgramId);
                    lblnEmailSend = lobjcehelper.InsertEmailDetails(lobjEmailDetail, lobjAttachments);
                    LoggingAdapter.WriteLog("ActivationFacade SendEmail -" + lblnEmailSend);
                }
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("OTP password error :" + ex.Message);
            }
            return lblnEmailSend;
        }
        /// <summary>
        /// Send SMS
        /// </summary>
        /// <param name="lobjMemberDetails">contains TemplateCode,ReceiverMobile,MemberId,ProgramId etc</param>
        /// <param name="pstrOTPstring">contains pstrOTPstirng</param>
        /// <param name="pdtExpirydatetime">contains pdtExpirydatetime</param>
        /// <returns></returns>
        private bool SendSMS(MemberDetails lobjMemberDetails, string pstrOTPstring, DateTime pdtExpirydatetime)
        {
            bool lblnSMSSend = false;
            try
            {
                if (lobjMemberDetails.MobileNumber != string.Empty)
                {
                    string lsrtTemplateLangCode = "";
                    if (lobjMemberDetails.PreferredLanguage.ToUpper() != "EN")
                    {
                        lsrtTemplateLangCode = lobjMemberDetails.PreferredLanguage.ToUpper();
                    }
                    string lstrexpirydt = pdtExpirydatetime.ToString("dd/MM/yyyy");
                    //Communication Engine Call For SMS. 
                    CeFacade lobjcehelper = new CeFacade();
                    SmsDetails lobjSmsDetail = new SmsDetails();
                    List<string> lstSmsparameter = new List<string>();
                    lstSmsparameter.Add(pstrOTPstring);
                    lstSmsparameter.Add(lobjMemberDetails.FullName);//Customer Name

                    lobjSmsDetail.TemplateCode = lsrtTemplateLangCode + "OTPSMS";
                    lobjSmsDetail.ListParameter = lstSmsparameter;
                    lobjSmsDetail.ReceiverMobile = Convert.ToString(lobjMemberDetails.MobileNumber);
                    lobjSmsDetail.MemberId = Convert.ToString(lobjMemberDetails.MemberRelationsList[0].RelationReference);
                    lobjSmsDetail.ProgramId = Convert.ToString(lobjMemberDetails.ProgramId);
                    lblnSMSSend = lobjcehelper.InsertSmsDetails(lobjSmsDetail);
                }
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("OTP SMS ERROR :" + ex.Message);
            }
            return lblnSMSSend;
        }

        /// <summary>
        /// Unlcok Member By OTP
        /// </summary>
        /// <param name="pobjOTPDetails">contains UniquerefID,RelationType etc</param>
        /// <returns>bool</returns>
        public bool UnlcokMemberByOTP(OTPDetails pobjOTPDetails)
        {
            OTPFacade lobjOTPFacade = new OTPFacade();

            //OTPDetails lobjOTPDetails = new OTPDetails();
            //lobjOTPDetails.UniquerefID = pstrRelationReferece;
            //lobjOTPDetails.OTP = Convert.ToInt32(pstrOTPDetails);

            bool lblnIsOTPupdated = false;
            lblnIsOTPupdated = lobjOTPFacade.UpdateOTP(pobjOTPDetails);
            if (lblnIsOTPupdated)
            {
                return SPWrapper.UnlockMemberByOTP(pobjOTPDetails);
            }
            return lblnIsOTPupdated;
        }
        /// <summary>
        /// Update Valid OTP
        /// </summary>
        /// <param name="pobjOTPDetails">contains UniquerefID,OTP,OtpType etc</param>
        /// <param name="pobjMemberDetails">contains WebPassword,RelationReference,RelationType etc</param>
        /// <returns>bool</returns>
        public bool UpdateValidOTP(OTPDetails pobjOTPDetails, MemberDetails pobjMemberDetails)
        {
            OTPFacade lobjOTPFacade = new OTPFacade();
            bool lblnIsOTPupdated = false;
            lblnIsOTPupdated = lobjOTPFacade.UpdateOTP(pobjOTPDetails);
            if (lblnIsOTPupdated)
            {
                lblnIsOTPupdated = ActivteUser(pobjMemberDetails);
                if (lblnIsOTPupdated)
                {
                    SendEmailForActivation(pobjMemberDetails);
                }
            }
            return lblnIsOTPupdated;
        }
        /// <summary>
        /// Update Valid OTP For Activation
        /// </summary>
        /// <param name="pobjMemberDetails">contains WebPassword,RelationReference,RelationType etc</param>
        /// <returns>bool</returns>
        public bool UpdateValidOTPForActivation(MemberDetails pobjMemberDetails)
        {
            bool lblnIsOTPupdated = false;
            lblnIsOTPupdated = ActivteUser(pobjMemberDetails);
            if (lblnIsOTPupdated)
            {
                SendEmailForActivation(pobjMemberDetails);
            }
            return lblnIsOTPupdated;
        }
        /// <summary>
        /// Update Valid OTP For Activation Link
        /// </summary>
        /// <param name="pobjMemberDetails">contains Id,Email,WebPassword,RelationReference,RelationType etc</param>
        /// <returns></returns>
        public bool UpdateValidOTPForActivationLink(MemberDetails pobjMemberDetails)
        {
            bool lblnIsResponse = false;
            try
            {
                lblnIsResponse = ActivteUserForLink(pobjMemberDetails);
                if (lblnIsResponse)
                {
                    SendEmailForActivation(pobjMemberDetails);
                }
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("UpdateValidOTPForActivationLink Ex-" + ex.Message + ex.InnerException + ex.StackTrace);
            }
            return lblnIsResponse;
        }

        /// <summary>
        /// Update Valid OTP By CIF National Id NBO
        /// </summary>
        /// <param name="pobjOTPDetails">contains UniquerefID,OTP,OtpType etc</param>
        /// <param name="pobjMemberDetails">contains contains for UpdateMemberEmailId  Email,LastName,ProgramId,NationalId,Id etc
        /// contains for ActivateMemberAccount WebPassword,RelationReference,RelationType etc</param>
        /// <returns>bool</returns>
        public bool UpdateValidOTPByCIFNationalId(OTPDetails pobjOTPDetails, MemberDetails pobjMemberDetails)
        {
            OTPFacade lobjOTPFacade = new OTPFacade();
            bool lblnIsOTPupdated = false;
            lblnIsOTPupdated = lobjOTPFacade.UpdateOTP(pobjOTPDetails);
            if (lblnIsOTPupdated)
            {
                lblnIsOTPupdated = ActivteUserByCIFNationalId(pobjMemberDetails);
                if (lblnIsOTPupdated)
                {
                    SendEmailForActivation(pobjMemberDetails);
                }
            }
            return lblnIsOTPupdated;
        }
        /// <summary>
        /// Update Valid OTP By National Id
        /// </summary>
        /// <param name="pobjOTPDetails">contains UniquerefID,OTP,OtpType etc</param>
        /// <param name="pobjMemberDetails">contains WebPassword,RelationReference,RelationType etc</param>
        /// <returnsbool></returns>
        public bool UpdateValidOTPByNationalId(OTPDetails pobjOTPDetails, MemberDetails pobjMemberDetails)
        {
            OTPFacade lobjOTPFacade = new OTPFacade();
            bool lblnIsOTPupdated = false;
            lblnIsOTPupdated = lobjOTPFacade.UpdateOTP(pobjOTPDetails);
            if (lblnIsOTPupdated)
            {
                lblnIsOTPupdated = ActivteUser(pobjMemberDetails);
                if (lblnIsOTPupdated)
                {
                    SendEmailForActivationByNationalId(pobjMemberDetails);
                }
            }
            return lblnIsOTPupdated;
        }

        /// <summary>
        /// // UAB Rewards // This Method is used to Activate A Member On Activation
        /// </summary>
        /// <param name="pobjMemberDetails">contains RelationReference,ProgramId,RelationType etc</param>
        /// <returns></returns>
        public bool MemberActivation(ActivationParameters pobjActivationParameters)
        {
            bool lblnUpdateSuccess = false;

            MemberRelation lobjMemberRelation = null;
            MemberBusinessFacade lobjMemberBusinessFacade = new MemberBusinessFacade();
            MemberDetails lobjMemberDetails = lobjMemberBusinessFacade.GetMemberDetailsForActivation(pobjActivationParameters);

            if (lobjMemberDetails != null)
            {
                if ((lobjMemberDetails.Email.Equals(string.Empty) || lobjMemberDetails.MobileNumber.Equals(string.Empty) || lobjMemberDetails.MothersMaidenName.Equals(string.Empty)) || (!CheckMembershipAuthenticationDetails(pobjActivationParameters, lobjMemberDetails)))
                    throw new ApplicationException("Please call 800 474 to update your records to activate your UAB Rewards account");

                lobjMemberRelation = lobjMemberDetails.MemberRelationsList.Find(lobj => lobj.RelationType.Equals((RelationType)pobjActivationParameters.RelationType));

                if (lobjMemberRelation != null)
                {
                    if ((!lobjMemberRelation.IsAccountActivated) && (lobjMemberRelation.Status.Equals(Status.InActive)))
                    {
                        string lstrPasswordDefinition = new Random().Next(100000, 200000).ToString();

                        lobjMemberRelation.WebPassword = RSAEncryptor.EncryptString(lstrPasswordDefinition, RSASecurityConstant.KeySize, RSASecurityConstant.RSAPublicKey);
                        lobjMemberRelation.IsAccountActivated = true;
                        lobjMemberRelation.Status = Status.Active;
                        lobjMemberRelation.ForceChangePassword = true;

                        lblnUpdateSuccess = SPWrapper.UpdateMemberDetailsForActivation(lobjMemberRelation);

                        if (lblnUpdateSuccess)
                        {
                            //lobjMemberDetails.MemberRelationsList.Find(lobj => lobj.RelationType.Equals(RelationType.LBMS)) ;
                            int lintIndex = lobjMemberDetails.MemberRelationsList.FindIndex(lobj => lobj.RelationType.Equals((RelationType)pobjActivationParameters.RelationType));
                            lobjMemberDetails.MemberRelationsList[lintIndex] = lobjMemberRelation;
                            SendEmailSMSForActivation(lobjMemberDetails);
                        }
                    }
                    else
                    {
                        throw new ApplicationException("Please call 800 474 to update your records to activate your UAB Rewards account");
                    }
                }
            }
            else
            {
                throw new ApplicationException("Invalid credentials. Please call the 24/7 contact centre  on 800 474 to activate your UAB REWARDS Account");
            }


            return lblnUpdateSuccess;

            //LoggingAdapter.WriteLog("Login-" + lobjMemberDetails.);
            //return lobjMemberDetails != null ? lobjMemberDetails : new MemberDetails();

        }

        /// <summary>
        ///  UAB Activation Email and SMS
        /// </summary>
        /// <param name="pobjMemberDetails">contains TemplateCode,MemberId,ProgramId etc</param>
        public void SendEmailSMSForActivation(MemberDetails pobjMemberDetails)
        {
            try
            {
                if (pobjMemberDetails.Email != string.Empty)
                {
                    string lsrtTemplateLangCode = "";
                    if (pobjMemberDetails.PreferredLanguage.ToUpper() != "EN")
                    {
                        lsrtTemplateLangCode = pobjMemberDetails.PreferredLanguage.ToUpper();
                    }

                    // Communication Engine Call FOr Email.
                    EmailDetails lobjEmailDetail = new EmailDetails();
                    List<string> lstEmailparameter = new List<string>();
                    List<string> lstAttachment = new List<string>();
                    CeFacade lobjcehelper = new CeFacade();
                    List<Attachments> lobjAttachments = null;
                    lstEmailparameter.Add(pobjMemberDetails.MemberRelationsList.Find(lobj => lobj.RelationType.Equals(RelationType.LBMS)).RelationReference);
                    lobjEmailDetail.TemplateCode = lsrtTemplateLangCode + "MemberActivation";
                    lobjEmailDetail.ListParameter = lstEmailparameter;
                    lobjEmailDetail.MemberId = Convert.ToString(pobjMemberDetails.MemberRelationsList.Find(lobj => lobj.RelationType.Equals(RelationType.LBMS)).RelationReference);
                    lobjEmailDetail.ProgramId = Convert.ToString(pobjMemberDetails.ProgramId);
                    lobjEmailDetail.AttachmentList = lstAttachment;
                    lobjEmailDetail.To = pobjMemberDetails.Email;
                    lobjcehelper.InsertEmailDetails(lobjEmailDetail, lobjAttachments);
                }
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("Activation Email :" + ex.Message);
            }

            try
            {
                if (pobjMemberDetails.MobileNumber != string.Empty)
                {
                    string lsrtTemplateLangCode = "";
                    if (pobjMemberDetails.PreferredLanguage.ToUpper() != "EN")
                    {
                        lsrtTemplateLangCode = pobjMemberDetails.PreferredLanguage.ToUpper();
                    }

                    //Communication Engine Call For SMS. 
                    CeFacade lobjcehelper = new CeFacade();
                    SmsDetails lobjSmsDetail = new SmsDetails();
                    List<string> lstSmsparameter = new List<string>();
                    lobjSmsDetail.TemplateCode = lsrtTemplateLangCode + "MemberActivation";
                    lstSmsparameter.Add(RSAEncryptor.DecryptString(pobjMemberDetails.MemberRelationsList.Find(lobj => lobj.RelationType.Equals(RelationType.LBMS)).WebPassword, RSASecurityConstant.KeySize, RSASecurityConstant.PrivateKey));
                    lobjSmsDetail.ListParameter = lstSmsparameter;
                    lobjSmsDetail.ProgramId = Convert.ToString(pobjMemberDetails.ProgramId);
                    lobjSmsDetail.MemberId = Convert.ToString(pobjMemberDetails.MemberRelationsList.Find(lobj => lobj.RelationType.Equals(RelationType.LBMS)).RelationReference);
                    lobjSmsDetail.ReceiverMobile = Convert.ToString(pobjMemberDetails.MobileNumber);
                    lobjcehelper.InsertSmsDetails(lobjSmsDetail);
                }
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("Activation SMS :" + ex.Message);
            }

            LoggingAdapter.WriteLog("RelationReference Number= " + pobjMemberDetails.MemberRelationsList.Find(lobj => lobj.RelationType.Equals("LBMS")).RelationReference + " Password= " + RSAEncryptor.DecryptString(pobjMemberDetails.MemberRelationsList.Find(lobj => lobj.RelationType.Equals("LBMS")).WebPassword, RSASecurityConstant.KeySize, RSASecurityConstant.PrivateKey));
        }

        /// <summary>
        /// UAB Rewards : This Method is used in MemberActivation Method to authenticate user credenials.. 
        /// </summary>
        /// <param name="pobjActivationParameters">contains MobileNumber,Email,MothersMaidenName etc</param>
        /// <param name="lobjMemberDetails">contains MobileNumber,Email,MothersMaidenName etc</param>
        /// <returns></returns>
        private bool CheckMembershipAuthenticationDetails(ActivationParameters pobjActivationParameters, MemberDetails lobjMemberDetails)
        {
            if (lobjMemberDetails == null)
                return false;
            //bool lblnMembershipReference = pobjActivationParameters.RelationReference.ToUpper().Equals(lobjMemberDetails.MemberRelationsList.Find(lobj => lobj.RelationType.Equals(RelationType.LBMS)).RelationReference.ToUpper());
            bool lblnMobileNumber = pobjActivationParameters.MobileNumber.Trim().ToUpper().Equals(lobjMemberDetails.MobileNumber.Trim().ToUpper());
            bool lblnEmail = pobjActivationParameters.Email.Trim().ToUpper().Equals(lobjMemberDetails.Email.Trim().ToUpper());
            bool lblnMothersMaidenName = pobjActivationParameters.MothersMaidenName.ToUpper().Equals(lobjMemberDetails.MothersMaidenName.ToUpper());

            //return lblnEmail ? (lblnDOB || lblnMothersMaidenName) : (lblnDOB && lblnMothersMaidenName);

            return lblnMobileNumber && lblnEmail && lblnMothersMaidenName;
        }


        /// <summary>
        /// Membership Activation For SkyMiles Based on DOB, Mothers maiden name
        /// </summary>
        /// <param name="pobjMemberDetails">contains RelationReference,ProgramId,RelationType</param>
        /// <returns>bool</returns>
        public bool CheckMembershipActivation(ActivationParameters pobjActivationParameters)
        {
            bool lblnUpdateSuccess = false;

            MemberRelation lobjMemberRelation = null;
            MemberBusinessFacade lobjMemberBusinessFacade = new MemberBusinessFacade();
            MemberDetails lobjMemberDetails = lobjMemberBusinessFacade.GetMemberDetailsForActivation(pobjActivationParameters);
            //if (lobjMemberDetails.MemberRelationsList.Count > 0)
            //{
            //    LoggingAdapter.WriteLog("Member Details" + "True");
            //}
            //else
            //{
            //    LoggingAdapter.WriteLog("Member Details" + "False");
            //}
            if (lobjMemberDetails != null)
            {
                if ((lobjMemberDetails.Email.Equals(string.Empty) || lobjMemberDetails.MobileNumber.Equals(string.Empty) || lobjMemberDetails.MothersMaidenName.Equals(string.Empty)) || lobjMemberDetails.DOB.Equals(string.Empty) || (!CheckMembershipActivationDetails(pobjActivationParameters, lobjMemberDetails)))
                    throw new ApplicationException("Invalid Credentials.");

                lobjMemberRelation = lobjMemberDetails.MemberRelationsList.Find(lobj => lobj.RelationType.Equals((RelationType)pobjActivationParameters.RelationType));

                if (lobjMemberRelation != null)
                {
                    if ((!lobjMemberRelation.IsAccountActivated) && (lobjMemberRelation.Status.Equals(Status.InActive)))
                    {
                        string lstrPasswordDefinition = new Random().Next(100000, 200000).ToString();

                        lobjMemberRelation.WebPassword = RSAEncryptor.EncryptString(lstrPasswordDefinition, RSASecurityConstant.KeySize, RSASecurityConstant.RSAPublicKey);
                        lobjMemberRelation.IsAccountActivated = true;
                        lobjMemberRelation.Status = Status.Active;
                        lobjMemberRelation.ForceChangePassword = true;

                        lblnUpdateSuccess = SPWrapper.UpdateMemberDetailsForActivation(lobjMemberRelation);

                        if (lblnUpdateSuccess)
                        {
                            //lobjMemberDetails.MemberRelationsList.Find(lobj => lobj.RelationType.Equals(RelationType.LBMS)) ;
                            int lintIndex = lobjMemberDetails.MemberRelationsList.FindIndex(lobj => lobj.RelationType.Equals((RelationType)pobjActivationParameters.RelationType));
                            lobjMemberDetails.MemberRelationsList[lintIndex] = lobjMemberRelation;
                            SendActivationEmailSMS(lobjMemberDetails);
                        }
                    }
                    else
                    {
                        throw new ApplicationException("Account already activated.");
                    }
                }
            }
            else
            {
                throw new ApplicationException("Invalid credentials.");
            }


            return lblnUpdateSuccess;

            //LoggingAdapter.WriteLog("Login-" + lobjMemberDetails.);
            //return lobjMemberDetails != null ? lobjMemberDetails : new MemberDetails();

        }

        /// <summary>
        /// Skymiles : This Method is used in CheckMembershipActivation Method to authenticate user credenials.. 
        /// </summary>
        /// <param name="pobjActivationParameters">contains RelationReference,Email,MothersMaidenName etc</param>
        /// <param name="lobjMemberDetails">contains RelationReference,Email,MothersMaidenName etc</param>
        /// <returns>bool</returns>
        private bool CheckMembershipActivationDetails(ActivationParameters pobjActivationParameters, MemberDetails lobjMemberDetails)
        {
            if (lobjMemberDetails == null)
                return false;
            bool lblnMembershipReference = pobjActivationParameters.RelationReference.ToUpper().Equals(lobjMemberDetails.MemberRelationsList.Find(lobj => lobj.RelationType.Equals((RelationType)pobjActivationParameters.RelationType)).RelationReference.ToUpper());
            //bool lblnMobileNumber = pobjActivationParameters.MobileNumber.Trim().ToUpper().Equals(lobjMemberDetails.MobileNumber.Trim().ToUpper());
            bool lblnEmail = pobjActivationParameters.Email.Trim().ToUpper().Equals(lobjMemberDetails.Email.Trim().ToUpper());
            bool lblnMothersMaidenName = pobjActivationParameters.MothersMaidenName.ToUpper().Equals(lobjMemberDetails.MothersMaidenName.ToUpper());
            LoggingAdapter.WriteLog("MemberId" + lblnMembershipReference + "EmailId" + lblnEmail + "lblnMothersMaidenName" + lblnMothersMaidenName);
            LoggingAdapter.WriteLog("Client DOB" + pobjActivationParameters.DOB);
            LoggingAdapter.WriteLog("DB DOB" + lobjMemberDetails.DOB);
            bool lblnDob = false;
            if (pobjActivationParameters.DOB.Year.Equals(lobjMemberDetails.DOB.Year))
            {
                if (pobjActivationParameters.DOB.Month.Equals(lobjMemberDetails.DOB.Month))
                {
                    if (pobjActivationParameters.DOB.Day.Equals(lobjMemberDetails.DOB.Day))
                    {
                        lblnDob = true;
                    }
                }
            }


            LoggingAdapter.WriteLog("MemberId" + lblnMembershipReference + "EmailId" + lblnEmail + "lblnMothersMaidenName" + lblnMothersMaidenName + "lblnDob" + lblnDob);
            return lblnMembershipReference && lblnEmail && lblnMothersMaidenName && lblnDob;
        }

        /// <summary>
        ///  skyymiles Activation Email and SMS
        /// </summary>
        /// <param name="pobjMemberDetails">contains TemplateCode,MemberId,ProgramId etc</param>
        public void SendActivationEmailSMS(MemberDetails pobjMemberDetails)
        {
            try
            {
                if (pobjMemberDetails.Email != string.Empty)
                {
                    string lsrtTemplateLangCode = "";
                    if (pobjMemberDetails.PreferredLanguage.ToUpper() != "EN")
                    {
                        lsrtTemplateLangCode = pobjMemberDetails.PreferredLanguage.ToUpper();
                    }

                    // Communication Engine Call FOr Email.
                    EmailDetails lobjEmailDetail = new EmailDetails();
                    List<string> lstEmailparameter = new List<string>();
                    List<string> lstAttachment = new List<string>();
                    CeFacade lobjcehelper = new CeFacade();
                    List<Attachments> lobjAttachments = null;
                    lstEmailparameter.Add(pobjMemberDetails.LastName);
                    lobjEmailDetail.TemplateCode = lsrtTemplateLangCode + "MemberActivation";
                    lobjEmailDetail.ListParameter = lstEmailparameter;
                    lobjEmailDetail.MemberId = Convert.ToString(pobjMemberDetails.MemberRelationsList.Find(lobj => lobj.RelationType.Equals(RelationType.LBMS)).RelationReference);
                    lobjEmailDetail.ProgramId = Convert.ToString(pobjMemberDetails.ProgramId);
                    lobjEmailDetail.AttachmentList = lstAttachment;
                    lobjEmailDetail.To = pobjMemberDetails.Email;
                    lobjcehelper.InsertEmailDetails(lobjEmailDetail, lobjAttachments);
                }
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("Activation Email :" + ex.Message);
            }

            try
            {

                if (pobjMemberDetails.MobileNumber != string.Empty)
                {
                    //Communication Engine Call For SMS. 
                    try
                    {
                        string lsrtTemplateLangCode = "";
                        if (pobjMemberDetails.PreferredLanguage.ToUpper() != "EN")
                        {
                            lsrtTemplateLangCode = pobjMemberDetails.PreferredLanguage.ToUpper();
                        }

                        CeFacade lobjcehelper = new CeFacade();
                        SmsDetails lobjSmsDetail = new SmsDetails();
                        List<string> lstSmsparameter = new List<string>();
                        lobjSmsDetail.TemplateCode = lsrtTemplateLangCode + "MemberActivation";
                        lobjSmsDetail.ListParameter = lstSmsparameter;
                        lobjSmsDetail.ProgramId = Convert.ToString(pobjMemberDetails.ProgramId);
                        lobjSmsDetail.MemberId = Convert.ToString(pobjMemberDetails.MemberRelationsList.Find(lobj => lobj.RelationType.Equals(RelationType.LBMS)).RelationReference);
                        lobjSmsDetail.ReceiverMobile = Convert.ToString(pobjMemberDetails.MobileNumber);
                        lobjcehelper.InsertSmsDetails(lobjSmsDetail);
                    }
                    catch (Exception ex)
                    {
                        LoggingAdapter.WriteLog("Activation 1st msg:" + ex.Message + ex.StackTrace);
                    }
                }

                if (pobjMemberDetails.MobileNumber != string.Empty)
                {
                    //Communication Engine Call For SMS. 
                    try
                    {
                        CeFacade lobjcehelper = new CeFacade();
                        SmsDetails lobjSmsDetail = new SmsDetails();
                        List<string> lstSmsparameter = new List<string>();
                        lstSmsparameter.Add(RSAEncryptor.DecryptString(pobjMemberDetails.MemberRelationsList[0].WebPassword, RSASecurityConstant.KeySize, RSASecurityConstant.PrivateKey));
                        lobjSmsDetail.TemplateCode = "MemberActivationPwd";
                        lobjSmsDetail.ListParameter = lstSmsparameter;
                        lobjSmsDetail.ProgramId = Convert.ToString(pobjMemberDetails.ProgramId);
                        lobjSmsDetail.MemberId = Convert.ToString(pobjMemberDetails.MemberRelationsList.Find(lobj => lobj.RelationType.Equals(RelationType.LBMS)).RelationReference);
                        lobjSmsDetail.ReceiverMobile = Convert.ToString(pobjMemberDetails.MobileNumber);
                        lobjcehelper.InsertSmsDetails(lobjSmsDetail);
                    }
                    catch (Exception ex)
                    {
                        LoggingAdapter.WriteLog("Activation 2nd msg:" + ex.Message + ex.StackTrace);
                    }
                }
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("Activation SMS :" + ex.Message);
            }
        }

        /// <summary>
        /// created for MAF
        /// </summary>
        /// <param name="pobjActivationParameters">contains RelationReference,ProgramId,RelationType etc</param>
        /// <returns>bool</returns>
        public bool CheckMembershipActivationData(ActivationParameters pobjActivationParameters)
        {
            bool lblnUpdateSuccess = false;
            //MasterFacade lobjMasterFacade = new MasterFacade();
            MemberRelation lobjMemberRelation = null;
            MemberBusinessFacade lobjMemberBusinessFacade = new MemberBusinessFacade();
            MemberDetails lobjMemberDetails = lobjMemberBusinessFacade.GetMemberDetailsForActivation(pobjActivationParameters);

            if (lobjMemberDetails != null)
            {
                if ((lobjMemberDetails.Email.Equals(string.Empty) || lobjMemberDetails.MobileNumber.Equals(string.Empty) || lobjMemberDetails.MothersMaidenName.Equals(string.Empty)) || (!CheckMembershipAuthentication(pobjActivationParameters, lobjMemberDetails)))
                    throw new ApplicationException("Invalid Credentials.");

                lobjMemberRelation = lobjMemberDetails.MemberRelationsList.Find(lobj => lobj.RelationType.Equals((RelationType)pobjActivationParameters.RelationType));

                if (lobjMemberRelation != null)
                {
                    if ((!lobjMemberRelation.IsAccountActivated) && (lobjMemberRelation.Status.Equals(Status.InActive)))
                    {
                        string lstrPasswordDefinition = new Random().Next(100000, 200000).ToString();

                        lobjMemberRelation.WebPassword = RSAEncryptor.EncryptString(lstrPasswordDefinition, RSASecurityConstant.KeySize, RSASecurityConstant.RSAPublicKey);
                        lobjMemberRelation.IsAccountActivated = true;
                        lobjMemberRelation.Status = Status.Active;
                        lobjMemberRelation.ForceChangePassword = true;

                        lblnUpdateSuccess = SPWrapper.UpdateMemberDetailsForActivation(lobjMemberRelation);

                        if (lblnUpdateSuccess)
                        {
                            //lobjMemberDetails.MemberRelationsList.Find(lobj => lobj.RelationType.Equals(RelationType.LBMS)) ;
                            int lintIndex = lobjMemberDetails.MemberRelationsList.FindIndex(lobj => lobj.RelationType.Equals((RelationType)pobjActivationParameters.RelationType));
                            lobjMemberDetails.MemberRelationsList[lintIndex] = lobjMemberRelation;
                            SendEmailSMSActivation(lobjMemberDetails);
                        }
                    }
                    else
                    {
                        throw new ApplicationException("Account already activated.");
                    }
                }
            }
            else
            {
                throw new ApplicationException("Invalid credentials.");
            }

            return lblnUpdateSuccess;
        }

        /// <summary>
        /// Check Membership Authentication MAF
        /// </summary>
        /// <param name="pobjActivationParameters">contains MobileNumber,DOB,MothersMaidenName etc</param>
        /// <param name="lobjMemberDetails">contains MobileNumber,DOB,MothersMaidenName etc</param>
        /// <returns></returns>
        private bool CheckMembershipAuthentication(ActivationParameters pobjActivationParameters, MemberDetails lobjMemberDetails)
        {
            if (lobjMemberDetails == null)
                return false;
            //bool lblnMembershipReference = pobjActivationParameters.RelationReference.ToUpper().Equals(lobjMemberDetails.MemberRelationsList.Find(lobj => lobj.RelationType.Equals(RelationType.LBMS)).RelationReference.ToUpper());
            bool lblnMobileNumber = pobjActivationParameters.MobileNumber.Trim().ToUpper().Equals(lobjMemberDetails.MobileNumber.Trim().ToUpper());
            bool lblnDOB = pobjActivationParameters.DOB.ToString("dd/MM/yyyy").Equals(lobjMemberDetails.DOB.ToString("dd/MM/yyyy"));
            bool lblnMothersMaidenName = pobjActivationParameters.MothersMaidenName.ToUpper().Equals(lobjMemberDetails.MothersMaidenName.ToUpper());

            //return lblnEmail ? (lblnDOB || lblnMothersMaidenName) : (lblnDOB && lblnMothersMaidenName);

            return lblnMobileNumber && lblnDOB && lblnMothersMaidenName;
        }

        /// <summary>
        ///  MAF Activation Email and SMS
        /// </summary>
        /// <param name="pobjMemberDetails">contains TemplateCode,MemberId,ProgramId etc</param>
        public void SendEmailSMSActivation(MemberDetails pobjMemberDetails)
        {
            try
            {
                if (pobjMemberDetails.Email != string.Empty)
                {
                    string lsrtTemplateLangCode = "";
                    if (pobjMemberDetails.PreferredLanguage.ToUpper() != "EN")
                    {
                        lsrtTemplateLangCode = pobjMemberDetails.PreferredLanguage.ToUpper();
                    }

                    // Communication Engine Call FOr Email.
                    EmailDetails lobjEmailDetail = new EmailDetails();
                    List<string> lstEmailparameter = new List<string>();
                    List<string> lstAttachment = new List<string>();
                    CeFacade lobjcehelper = new CeFacade();
                    List<Attachments> lobjAttachments = null;
                    lstEmailparameter.Add(pobjMemberDetails.LastName);
                    lstEmailparameter.Add(pobjMemberDetails.MemberRelationsList.Find(lobj => lobj.RelationType.Equals(RelationType.LBMS)).RelationReference);
                    lstEmailparameter.Add(RSAEncryptor.DecryptString(pobjMemberDetails.MemberRelationsList.Find(lobj => lobj.RelationType.Equals(RelationType.LBMS)).WebPassword, RSASecurityConstant.KeySize, RSASecurityConstant.PrivateKey));
                    lobjEmailDetail.TemplateCode = lsrtTemplateLangCode + "MemberActivation";
                    lobjEmailDetail.ListParameter = lstEmailparameter;
                    lobjEmailDetail.MemberId = Convert.ToString(pobjMemberDetails.MemberRelationsList.Find(lobj => lobj.RelationType.Equals(RelationType.LBMS)).RelationReference);
                    lobjEmailDetail.ProgramId = Convert.ToString(pobjMemberDetails.ProgramId);
                    lobjEmailDetail.AttachmentList = lstAttachment;
                    lobjEmailDetail.To = pobjMemberDetails.Email;
                    lobjcehelper.InsertEmailDetails(lobjEmailDetail, lobjAttachments);
                }
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("Activation Email :" + ex.Message);
            }

            try
            {
                if (pobjMemberDetails.MobileNumber != string.Empty)
                {
                    string lsrtTemplateLangCode = "";
                    if (pobjMemberDetails.PreferredLanguage.ToUpper() != "EN")
                    {
                        lsrtTemplateLangCode = pobjMemberDetails.PreferredLanguage.ToUpper();
                    }

                    //Communication Engine Call For SMS. 
                    CeFacade lobjcehelper = new CeFacade();
                    SmsDetails lobjSmsDetail = new SmsDetails();
                    List<string> lstSmsparameter = new List<string>();
                    lobjSmsDetail.TemplateCode = lsrtTemplateLangCode + "MemberActivation";
                    lstSmsparameter.Add(RSAEncryptor.DecryptString(pobjMemberDetails.MemberRelationsList.Find(lobj => lobj.RelationType.Equals(RelationType.LBMS)).WebPassword, RSASecurityConstant.KeySize, RSASecurityConstant.PrivateKey));
                    lobjSmsDetail.ListParameter = lstSmsparameter;
                    lobjSmsDetail.ProgramId = Convert.ToString(pobjMemberDetails.ProgramId);
                    lobjSmsDetail.MemberId = Convert.ToString(pobjMemberDetails.MemberRelationsList.Find(lobj => lobj.RelationType.Equals(RelationType.LBMS)).RelationReference);
                    lobjSmsDetail.ReceiverMobile = Convert.ToString(pobjMemberDetails.MobileNumber);
                    lobjcehelper.InsertSmsDetails(lobjSmsDetail);
                }
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("Activation SMS :" + ex.Message);
            }

            LoggingAdapter.WriteLog("RelationReference Number= " + pobjMemberDetails.MemberRelationsList.Find(lobj => lobj.RelationType.Equals("LBMS")).RelationReference + " Password= " + RSAEncryptor.DecryptString(pobjMemberDetails.MemberRelationsList.Find(lobj => lobj.RelationType.Equals("LBMS")).WebPassword, RSASecurityConstant.KeySize, RSASecurityConstant.PrivateKey));
        }

        /// <summary>
        /// created for CBI Get Member Details Using EmailId and MobNo
        /// </summary>
        /// <param name="pobjActivationParameters">contains RelationReference,ProgramId,RelationType etc</param>
        /// <returns></returns>
        public MemberDetails GetMemberDetailsUsingEmailIdMobNo(ActivationParameters pobjActivationParameters)
        {
            MemberBusinessFacade lobjMemberBusinessFacade = new MemberBusinessFacade();
            MemberDetails lobjMemberDetails = null;
            lobjMemberDetails = lobjMemberBusinessFacade.GetMemberDetailsForActivation(pobjActivationParameters);

            if (lobjMemberDetails != null)
            {
                if (!lobjMemberDetails.Email.Equals(string.Empty) && !lobjMemberDetails.MobileNumber.Equals(string.Empty) && (CheckMemberEmailMobAuthentication(pobjActivationParameters, lobjMemberDetails)))
                {
                    return lobjMemberDetails;
                }
            }
            return null;
        }

        /// <summary>
        /// Check Member Email Mob Authentication
        /// </summary>
        /// <param name="pobjActivationParameters">contains MobileNumber,EmailId etc</param>
        /// <param name="lobjMemberDetails">contains MobileNumber,EmailId etc</param>
        /// <returns></returns>
        private bool CheckMemberEmailMobAuthentication(ActivationParameters pobjActivationParameters, MemberDetails lobjMemberDetails)
        {
            //bool lblnMembershipReference = pobjActivationParameters.RelationReference.ToUpper().Equals(lobjMemberDetails.MemberRelationsList.Find(lobj => lobj.RelationType.Equals(RelationType.LBMS)).RelationReference.ToUpper());
            bool lblnMobileNumber = pobjActivationParameters.MobileNumber.Trim().ToUpper().Equals(lobjMemberDetails.MobileNumber.Trim().ToUpper());
            bool lblnEmailId = pobjActivationParameters.Email.ToUpper().Equals(lobjMemberDetails.Email.ToUpper());

            //return lblnEmail ? (lblnDOB || lblnMothersMaidenName) : (lblnDOB && lblnMothersMaidenName);

            return lblnMobileNumber && lblnEmailId;
        }

        /// <summary>
        /// Update Valid OTP By MemberId CBI
        /// </summary>
        /// <param name="pobjOTPDetails">contains UniquerefID,OTP,OtpType etc</param>
        /// <param name="pobjMemberDetails">contains WebPassword,RelationReference,RelationType etc</param>
        /// <returns></returns>
        public bool UpdateValidOTPByMemberId(OTPDetails pobjOTPDetails, MemberDetails pobjMemberDetails)
        {
            OTPFacade lobjOTPFacade = new OTPFacade();
            bool lblnIsOTPupdated = false;
            lblnIsOTPupdated = lobjOTPFacade.UpdateOTP(pobjOTPDetails);
            if (lblnIsOTPupdated)
            {
                lblnIsOTPupdated = ActivteUserByMemberId(pobjMemberDetails);
                if (lblnIsOTPupdated)
                {
                    SendEmailForActivation(pobjMemberDetails);
                }
            }
            return lblnIsOTPupdated;
        }

        /// <summary>
        /// Update EmailId, Gender And Activate Account
        /// </summary>
        /// <param name="pobjMemberDetails">contains Email,Gender,ProgramId etc</param>
        /// <returns></returns>
        public bool UpdateEmailIdGenderAndActivteUserByMemberId(MemberDetails pobjMemberDetails)
        {
            bool IsActivateAccount = false;
            if (pobjMemberDetails != null)
            {
                IsActivateAccount = ActivteUserByMemberId(pobjMemberDetails);
                if (IsActivateAccount)
                {
                    SPWrapper.UpdateEmailIdGenderAndActivteUserByMemberId(pobjMemberDetails);
                    if (IsActivateAccount)
                    {
                        try
                        {
                            IsActivateAccount = false;
                            string lstrexpirydt = DateTime.Now.ToString("dd/MM/yyyy");
                            EmailDetails lobjEmailDetail = new EmailDetails();
                            List<string> lstEmailparameter = new List<string>();
                            List<string> lstAttachment = new List<string>();
                            CeFacade lobjcehelper = new CeFacade();
                            List<Attachments> lobjAttachments = null;
                            lstEmailparameter.Add(pobjMemberDetails.MemberRelationsList[0].RelationReference.ToString());
                            lstEmailparameter.Add(pobjMemberDetails.Email);
                            lstEmailparameter.Add(pobjMemberDetails.Gender.ToString());
                            lstEmailparameter.Add(DateTime.Now.ToString("dd/MM/yyyy"));
                            //lstEmailparameter.Add(lstrexpirydt); //new line of code
                            lobjEmailDetail.TemplateCode = "SendActivationDetailsToBank";

                            LoggingAdapter.WriteLog(" Bank Activation Details - Email Parameter" + lstEmailparameter.Count);
                            lobjEmailDetail.ListParameter = lstEmailparameter;
                            lobjEmailDetail.AttachmentList = lstAttachment;

                            lobjEmailDetail.MemberId = Convert.ToString(pobjMemberDetails.MemberRelationsList[0].RelationReference);
                            lobjEmailDetail.ProgramId = Convert.ToString(pobjMemberDetails.ProgramId);
                            lobjcehelper.InsertEmailDetails(lobjEmailDetail, lobjAttachments);
                            IsActivateAccount = true;
                        }
                        catch (Exception ex)
                        {
                            LoggingAdapter.WriteLog("SendActivationDetailsToBank - " + pobjMemberDetails.MemberRelationsList[0].RelationReference.ToString() + ".Exception -" + ex.ToString());
                            return false;
                        }
                    }
                }
            }
            return IsActivateAccount;
        }
        /// <summary>
        /// Activte User By MemberId
        /// </summary>
        /// <param name="pobjMemberDetails">contains WebPassword,RelationReference,RelationType etc</param>
        /// <returns>bool</returns>
        public bool ActivteUserByMemberId(MemberDetails pobjMemberDetails)
        {
            bool IsActivateAccount = false;
            if (pobjMemberDetails != null)
            {
                PasswordDefinition lobjPasswordDefinition = new PasswordDefinition();
                pobjMemberDetails.MemberRelationsList[0].WebPassword = RSAEncryptor.EncryptString(Convert.ToString(pobjMemberDetails.MemberRelationsList[0].WebPassword), RSASecurityConstant.KeySize, RSASecurityConstant.RSAPublicKey);
                IsActivateAccount = SPWrapper.ActivateMemberAccount(pobjMemberDetails);
            }
            return IsActivateAccount;
        }

        /// <summary>
        /// BMI // This Method is used to Activate A Member On Activation
        /// </summary>
        /// <param name="pobjActivationParameters">contains RelationReference,ProgramId,RelationType etc</param>
        /// <returns></returns>
        public MemberDetails GetMemberActivationDetails(ActivationParameters pobjActivationParameters)
        {
            MemberRelation lobjMemberRelation = null;
            MemberBusinessFacade lobjMemberBusinessFacade = new MemberBusinessFacade();
            MemberDetails lobjMemberDetails = lobjMemberBusinessFacade.GetMemberDetailsForActivation(pobjActivationParameters);

            if (lobjMemberDetails != null)
            {
                if ((lobjMemberDetails.AdditionalDetails.Equals(string.Empty) || lobjMemberDetails.MobileNumber.Equals(string.Empty) || (!CheckMemberActivationDetails(pobjActivationParameters, lobjMemberDetails))))
                    return null;

                lobjMemberRelation = lobjMemberDetails.MemberRelationsList.Find(lobj => lobj.RelationType.Equals((RelationType)pobjActivationParameters.RelationType));

                if (lobjMemberRelation != null)
                {
                    if ((!lobjMemberRelation.IsAccountActivated) && (lobjMemberRelation.Status.Equals(Status.InActive)))
                    {
                        return lobjMemberDetails;
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// BMI : This Method is used in MemberActivation Method to authenticate user credenials.. 
        /// </summary>
        /// <param name="pobjActivationParameters">contains MobileNumber,AdditionalDetails etc</param>
        /// <param name="lobjMemberDetails">contains MobileNumber,AdditionalDetails etc</param>
        /// <returns></returns>
        private bool CheckMemberActivationDetails(ActivationParameters pobjActivationParameters, MemberDetails lobjMemberDetails)
        {
            if (lobjMemberDetails == null)
                return false;

            bool lblnMobileNumber = pobjActivationParameters.MobileNumber.Trim().ToUpper().Equals(lobjMemberDetails.MobileNumber.Trim().ToUpper());
            bool lblnAdditionalDetails = pobjActivationParameters.AdditionalDetails.Trim().ToUpper().Equals(lobjMemberDetails.AdditionalDetails.Trim().ToUpper());

            return lblnMobileNumber && lblnAdditionalDetails;
        }

        /// <summary>
        /// Login Relation Activation
        /// </summary>
        /// <param name="pobjMemberDetails">contains RelationReference,RelationType etc</param>
        /// <returns></returns>
        public bool LoginRelationActivation(MemberDetails pobjMemberDetails)
        {
            bool IsMemberActivated = false;
            MemberBusinessFacade lobjMemberBusinessFacade = new MemberBusinessFacade();
            SearchMember lobjSearchMember = new SearchMember();

            MemberRelation lobjMemberRelations = pobjMemberDetails.MemberRelationsList.Find(lobj => lobj.RelationType.Equals(RelationType.Login));

            if (lobjMemberRelations != null)
            {
                lobjSearchMember.RelationReference = lobjMemberRelations.RelationReference;
                lobjSearchMember.RelationType = Convert.ToInt32(lobjMemberRelations.RelationType);
                bool IsLoginRelationExist = lobjMemberBusinessFacade.IsLoginRelationAlreadyExist(lobjSearchMember);

                if (!IsLoginRelationExist)
                {
                    pobjMemberDetails.MemberRelationsList.Find(lobj => lobj.RelationType.Equals(RelationType.Login)).MemberSubRelationList = new List<MemberSubRelation>();
                    bool IsRelationInserted = lobjMemberBusinessFacade.CreateMemberRelation(pobjMemberDetails);
                    if (IsRelationInserted)
                    {
                        IsMemberActivated = ActivteUserByMemberId(pobjMemberDetails);
                        if (IsMemberActivated)
                        {
                            SendLoginRelationEmailForActivation(pobjMemberDetails);
                        }
                    }
                }
            }
            return IsMemberActivated;
        }
        /// <summary>
        /// Send Login Relation Email Fo rActivation
        /// </summary>
        /// <param name="pobjMemberDetails">contains TemplateCode,MemberId,ProgramId etc</param>
        /// <returns></returns>
        public MemberDetails SendLoginRelationEmailForActivation(MemberDetails pobjMemberDetails)
        {
            try
            {
                if (pobjMemberDetails.Email != string.Empty)
                {
                    string lsrtTemplateLangCode = "";
                    if (pobjMemberDetails.PreferredLanguage.ToUpper() != "EN")
                    {
                        lsrtTemplateLangCode = pobjMemberDetails.PreferredLanguage.ToUpper();
                    }
                    // Communication Engine Call FOr Email.
                    EmailDetails lobjEmailDetail = new EmailDetails();
                    List<string> lstEmailparameter = new List<string>();
                    List<string> lstAttachment = new List<string>();
                    CeFacade lobjcehelper = new CeFacade();
                    List<Attachments> lobjAttachments = null;
                    lstEmailparameter.Add(pobjMemberDetails.LastName);
                    lobjEmailDetail.TemplateCode = lsrtTemplateLangCode + "MemberActivation";
                    lobjEmailDetail.ListParameter = lstEmailparameter;
                    lobjEmailDetail.MemberId = Convert.ToString(pobjMemberDetails.MemberRelationsList.Find(lobj => lobj.RelationType.Equals(RelationType.Login)).RelationReference);
                    lobjEmailDetail.ProgramId = Convert.ToString(pobjMemberDetails.ProgramId);
                    lobjEmailDetail.AttachmentList = lstAttachment;
                    lobjEmailDetail.To = pobjMemberDetails.Email;
                    lobjcehelper.InsertEmailDetails(lobjEmailDetail, lobjAttachments);
                }
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("Activation Email :" + ex.Message);
            }

            try
            {
                if (pobjMemberDetails.MobileNumber != string.Empty)
                {
                    string lsrtTemplateLangCode = "";
                    if (pobjMemberDetails.PreferredLanguage.ToUpper() != "EN")
                    {
                        lsrtTemplateLangCode = pobjMemberDetails.PreferredLanguage.ToUpper();
                    }
                    //Communication Engine Call For SMS. 
                    CeFacade lobjcehelper = new CeFacade();
                    SmsDetails lobjSmsDetail = new SmsDetails();
                    List<string> lstSmsparameter = new List<string>();
                    lobjSmsDetail.TemplateCode = lsrtTemplateLangCode + "MemberActivation";
                    lstSmsparameter.Add(pobjMemberDetails.LastName);
                    lobjSmsDetail.ListParameter = lstSmsparameter;
                    lobjSmsDetail.ProgramId = Convert.ToString(pobjMemberDetails.ProgramId);
                    lobjSmsDetail.MemberId = Convert.ToString(pobjMemberDetails.MemberRelationsList.Find(lobj => lobj.RelationType.Equals(RelationType.Login)).RelationReference);
                    lobjSmsDetail.ReceiverMobile = Convert.ToString(pobjMemberDetails.MobileNumber);
                    lobjcehelper.InsertSmsDetails(lobjSmsDetail);
                }
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("Activation SMS :" + ex.Message);
            }

            LoggingAdapter.WriteLog("RelationReference Number= " + pobjMemberDetails.MemberRelationsList[0].RelationReference + " Password= " + RSAEncryptor.DecryptString(pobjMemberDetails.MemberRelationsList[0].WebPassword, RSASecurityConstant.KeySize, RSASecurityConstant.PrivateKey));
            return pobjMemberDetails;
        }
        /// <summary>
        /// Send Email For Point Transfer OTP
        /// </summary>
        /// <param name="lobjMemberDetails">contains MemberId,ProgramId etc</param>
        /// <param name="pstrOTPstirng">pstrOTPstirng</param>
        /// <param name="pdtExpirydatetime">pdtExpirydatetime</param>
        /// <param name="pobjOTPDetails"></param>
        /// <returns>bool</returns>
        private bool SendEmailForPointTransferOTP(MemberDetails lobjMemberDetails, string pstrOTPstirng, DateTime pdtExpirydatetime, OTPDetails pobjOTPDetails)
        {
            bool lblnEmailSend = false;
            try
            {
                if (lobjMemberDetails.Email != string.Empty)
                {

                    string lstrexpirydt = pdtExpirydatetime.ToString("dd/MM/yyyy");

                    EmailDetails lobjEmailDetail = new EmailDetails();
                    List<string> lstEmailparameter = new List<string>();
                    List<string> lstAttachment = new List<string>();
                    CeFacade lobjcehelper = new CeFacade();
                    List<Attachments> lobjAttachments = null;
                    lstEmailparameter.Add(pstrOTPstirng);
                    lstEmailparameter.Add(lobjMemberDetails.FullName);
                    lobjEmailDetail.TemplateCode = "PointTransferOTP";
                    LoggingAdapter.WriteLog("OTP Email Parameter" + lstEmailparameter.Count);
                    lobjEmailDetail.ListParameter = lstEmailparameter;
                    lobjEmailDetail.AttachmentList = lstAttachment;
                    lobjEmailDetail.To = lobjMemberDetails.Email;
                    lobjEmailDetail.MemberId = Convert.ToString(lobjMemberDetails.MemberRelationsList[0].RelationReference);
                    lobjEmailDetail.ProgramId = Convert.ToString(lobjMemberDetails.ProgramId);
                    lblnEmailSend = lobjcehelper.InsertEmailDetails(lobjEmailDetail, lobjAttachments);
                }
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("OTP password error :" + ex.Message);
                throw ex;
            }
            return lblnEmailSend;
        }
        /// <summary>
        /// Send SMS For Point Transfer OTP
        /// </summary>
        /// <param name="lobjMemberDetails">contains MobileNumber,RelationReference,ProgramId etc</param>
        /// <param name="pstrOTPstring">pstrOTPstring</param>
        /// <param name="pdtExpirydatetime">pdtExpirydatetime</param>
        /// <param name="pobjOTPDetails"></param>
        /// <returns>bool</returns>
        private bool SendSMSForPointTransferOTP(MemberDetails lobjMemberDetails, string pstrOTPstring, DateTime pdtExpirydatetime, OTPDetails pobjOTPDetails)
        {
            bool lblnSMSSend = false;
            try
            {
                if (lobjMemberDetails.MobileNumber != string.Empty)
                {
                    string lstrexpirydt = pdtExpirydatetime.ToString("dd/MM/yyyy");
                    //Communication Engine Call For SMS. 
                    CeFacade lobjcehelper = new CeFacade();
                    SmsDetails lobjSmsDetail = new SmsDetails();
                    List<string> lstSmsparameter = new List<string>();
                    lstSmsparameter.Add(pstrOTPstring);
                    lobjSmsDetail.TemplateCode = "PointTransferOTP";
                    lobjSmsDetail.ListParameter = lstSmsparameter;
                    lobjSmsDetail.ReceiverMobile = Convert.ToString(lobjMemberDetails.MobileNumber);
                    lobjSmsDetail.MemberId = Convert.ToString(lobjMemberDetails.MemberRelationsList[0].RelationReference);
                    lobjSmsDetail.ProgramId = Convert.ToString(lobjMemberDetails.ProgramId);
                    lblnSMSSend = lobjcehelper.InsertSmsDetails(lobjSmsDetail);
                }
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("OTP SMS ERROR :" + ex.Message);
            }
            return lblnSMSSend;
        }

        public bool MemberActivationBNI(MemberDetails pobjMemberDetails)
        {
            bool lblnActivateUser = false;
            PasswordDefinition lobjPasswordDefinition = new PasswordDefinition();
            pobjMemberDetails.MemberRelationsList[0].WebPassword = RSAEncryptor.EncryptString(Convert.ToString(pobjMemberDetails.MemberRelationsList[0].WebPassword), RSASecurityConstant.KeySize, RSASecurityConstant.RSAPublicKey);
            lblnActivateUser = SPWrapper.ActivateMemberAccountBNI(pobjMemberDetails);
            if (lblnActivateUser)
            {
                SendCommunicationForActivationBNI(pobjMemberDetails);
            }
            return lblnActivateUser;
        }

        public MemberDetails SendCommunicationForActivationBNI(MemberDetails pobjMemberDetails)
        {
            try
            {
                if (pobjMemberDetails.Email != string.Empty)
                {
                    string lsrtTemplateLangCode = "";
                    if (pobjMemberDetails.PreferredLanguage.ToUpper() != "EN")
                    {
                        lsrtTemplateLangCode = pobjMemberDetails.PreferredLanguage.ToUpper();
                    }
                    // Communication Engine Call FOr Email.
                    EmailDetails lobjEmailDetail = new EmailDetails();
                    List<string> lstEmailparameter = new List<string>();
                    List<string> lstAttachment = new List<string>();
                    CeFacade lobjcehelper = new CeFacade();
                    List<Attachments> lobjAttachments = null;
                    lstEmailparameter.Add(pobjMemberDetails.LastName);
                    lstEmailparameter.Add(pobjMemberDetails.MemberRelationsList.Find(x => x.RelationType.Equals(RelationType.LBMS)).RelationReference);
                    lstEmailparameter.Add(pobjMemberDetails.AdditionalDetails1);
                    lobjEmailDetail.TemplateCode = lsrtTemplateLangCode + "MemberActivation";
                    lobjEmailDetail.ListParameter = lstEmailparameter;
                    lobjEmailDetail.MemberId = Convert.ToString(pobjMemberDetails.MemberRelationsList.Find(lobj => lobj.RelationType.Equals(RelationType.LBMS)).RelationReference);
                    lobjEmailDetail.ProgramId = Convert.ToString(pobjMemberDetails.ProgramId);
                    lobjEmailDetail.AttachmentList = lstAttachment;
                    lobjEmailDetail.To = pobjMemberDetails.Email;
                    lobjcehelper.InsertEmailDetails(lobjEmailDetail, lobjAttachments);
                }
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("Activation Email :" + ex.Message);
            }

            try
            {
                if (pobjMemberDetails.MobileNumber != string.Empty)
                {
                    string lsrtTemplateLangCode = "";
                    if (pobjMemberDetails.PreferredLanguage.ToUpper() != "EN")
                    {
                        lsrtTemplateLangCode = pobjMemberDetails.PreferredLanguage.ToUpper();
                    }
                    //Communication Engine Call For SMS. 
                    CeFacade lobjcehelper = new CeFacade();
                    SmsDetails lobjSmsDetail = new SmsDetails();
                    List<string> lstSmsparameter = new List<string>();
                    lobjSmsDetail.TemplateCode = lsrtTemplateLangCode + "MemberActivation";
                    lstSmsparameter.Add(pobjMemberDetails.LastName);
                    lstSmsparameter.Add(pobjMemberDetails.MemberRelationsList.Find(x => x.RelationType.Equals(RelationType.LBMS)).RelationReference);
                    lobjSmsDetail.ListParameter = lstSmsparameter;
                    lobjSmsDetail.ProgramId = Convert.ToString(pobjMemberDetails.ProgramId);
                    lobjSmsDetail.MemberId = Convert.ToString(pobjMemberDetails.MemberRelationsList.Find(lobj => lobj.RelationType.Equals(RelationType.LBMS)).RelationReference);
                    lobjSmsDetail.ReceiverMobile = Convert.ToString(pobjMemberDetails.MobileNumber);
                    lobjcehelper.InsertSmsDetails(lobjSmsDetail);
                }
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("Activation SMS :" + ex.Message);
            }

            LoggingAdapter.WriteLog("RelationReference Number= " + pobjMemberDetails.MemberRelationsList[0].RelationReference + " Password= " + RSAEncryptor.DecryptString(pobjMemberDetails.MemberRelationsList[0].WebPassword, RSASecurityConstant.KeySize, RSASecurityConstant.PrivateKey));
            return pobjMemberDetails;
        }

        public bool MemberActivation(MemberDetails pobjMemberDetails)
        {
            bool lblnActivateUser = false;
            //pobjMemberDetails.MemberRelationsList[0].WebPassword = RSAEncryptor.EncryptString(Convert.ToString(pobjMemberDetails.MemberRelationsList[0].WebPassword), RSASecurityConstant.KeySize, RSASecurityConstant.RSAPublicKey);
            if (pobjMemberDetails != null && pobjMemberDetails.MemberRelationsList != null && pobjMemberDetails.MemberRelationsList.Find(lobj => lobj.RelationType.Equals(RelationType.LBMS)).RelationReference != string.Empty)
            {
                lblnActivateUser = SPWrapper.ActivateMemberAccount(pobjMemberDetails);
                if (lblnActivateUser)
                {
                    SendCommunicationForActivation(pobjMemberDetails);
                }
            }
            return lblnActivateUser;
        }

        public MemberDetails SendCommunicationForActivation(MemberDetails pobjMemberDetails)
        {
            try
            {
                if (pobjMemberDetails.Email != string.Empty)
                {
                    string lsrtTemplateLangCode = "";
                    if (pobjMemberDetails.PreferredLanguage.ToUpper() != "EN")
                    {
                        lsrtTemplateLangCode = pobjMemberDetails.PreferredLanguage.ToUpper();
                    }

                    // Communication Engine Call FOr Email.
                    EmailDetails lobjEmailDetail = new EmailDetails();
                    List<string> lstEmailparameter = new List<string>();
                    List<string> lstAttachment = new List<string>();
                    CeFacade lobjcehelper = new CeFacade();
                    List<Attachments> lobjAttachments = null;
                    lstEmailparameter.Add(pobjMemberDetails.LastName);
                    lstEmailparameter.Add(pobjMemberDetails.NationalId);
                    lstEmailparameter.Add(pobjMemberDetails.PassportNumber);
                    lstEmailparameter.Add(pobjMemberDetails.MemberRelationsList.Find(x => x.RelationType.Equals(RelationType.LBMS)).RelationReference);
                    lstEmailparameter.Add(pobjMemberDetails.MemberRelationsList.Find(x => x.RelationType.Equals(RelationType.LBMS)).CreatedBy);
                    lobjEmailDetail.TemplateCode = lsrtTemplateLangCode + "MemberActivation";
                    lobjEmailDetail.ListParameter = lstEmailparameter;
                    lobjEmailDetail.MemberId = Convert.ToString(pobjMemberDetails.MemberRelationsList.Find(lobj => lobj.RelationType.Equals(RelationType.LBMS)).RelationReference);
                    lobjEmailDetail.ProgramId = Convert.ToString(pobjMemberDetails.ProgramId);
                    lobjEmailDetail.AttachmentList = lstAttachment;
                    lobjEmailDetail.To = pobjMemberDetails.Email;
                    lobjcehelper.InsertEmailDetails(lobjEmailDetail, lobjAttachments);
                }
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("Activation Email :" + ex.Message);
            }

            try
            {
                if (pobjMemberDetails.MobileNumber != string.Empty)
                {
                    string lsrtTemplateLangCode = "";
                    if (pobjMemberDetails.PreferredLanguage.ToUpper() != "EN")
                    {
                        lsrtTemplateLangCode = pobjMemberDetails.PreferredLanguage.ToUpper();
                    }

                    //Communication Engine Call For SMS. 
                    CeFacade lobjcehelper = new CeFacade();
                    SmsDetails lobjSmsDetail = new SmsDetails();
                    List<string> lstSmsparameter = new List<string>();
                    lobjSmsDetail.TemplateCode = lsrtTemplateLangCode + "MemberActivation";
                    lstSmsparameter.Add(pobjMemberDetails.LastName);
                    lstSmsparameter.Add(pobjMemberDetails.NationalId);
                    lstSmsparameter.Add(pobjMemberDetails.MemberRelationsList.Find(x => x.RelationType.Equals(RelationType.LBMS)).RelationReference);
                    lstSmsparameter.Add(pobjMemberDetails.PassportNumber);
                    lstSmsparameter.Add(pobjMemberDetails.MemberRelationsList.Find(x => x.RelationType.Equals(RelationType.LBMS)).CreatedBy);
                    lobjSmsDetail.ListParameter = lstSmsparameter;
                    lobjSmsDetail.ProgramId = Convert.ToString(pobjMemberDetails.ProgramId);
                    lobjSmsDetail.MemberId = Convert.ToString(pobjMemberDetails.MemberRelationsList.Find(lobj => lobj.RelationType.Equals(RelationType.LBMS)).RelationReference);
                    lobjSmsDetail.ReceiverMobile = Convert.ToString(pobjMemberDetails.MobileNumber);
                    lobjcehelper.InsertSmsDetails(lobjSmsDetail);
                }
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("Activation SMS :" + ex.Message);
            }

            LoggingAdapter.WriteLog("RelationReference Number = " + pobjMemberDetails.MemberRelationsList[0].RelationReference + " Password= " + RSAEncryptor.DecryptString(pobjMemberDetails.MemberRelationsList[0].WebPassword, RSASecurityConstant.KeySize, RSASecurityConstant.PrivateKey));
            return pobjMemberDetails;
        }
    }
}
