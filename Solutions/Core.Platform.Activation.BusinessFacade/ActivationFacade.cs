using Core.Platform.Member.Entites;
using Core.Platform.MemberManagement.BusinessFacade;
using Core.Platform.MemberManagement.DataAccess.Wrapper;
using Core.Platform.OTP.Entities;
using Core.Platform.OTP.Facade;
using Framework.EnterpriseLibrary.Adapters;
using Framework.EnterpriseLibrary.CommunicationEngine.Entity;
using Framework.EnterpriseLibrary.CommunicationEngine.Helper;
using Framework.EnterpriseLibrary.PasswordGenerator;
using Framework.EnterpriseLibrary.Security;
using Framework.EnterpriseLibrary.Security.Constants;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Security.Cryptography;
using System.Text;

namespace Core.Platform.Activation.BusinessFacade
{
    public class ActivationFacade
    {
        public static string lstrProgramName = Convert.ToString(ConfigurationManager.AppSettings["ProgramName"]);
        public static string lstrCulture = Convert.ToString(ConfigurationManager.AppSettings["Culture"]);

        public MemberDetails GetMemberDetails(ActivationParameters pobjActivationParameters)
        {
            MemberDetails lobjMemberDetails = null;
            try
            {
                MemberBusinessFacade lobjMemberBusinessFacade = new MemberBusinessFacade();
                lobjMemberDetails = lobjMemberBusinessFacade.GetMemberDetailsForActivation(pobjActivationParameters);
                //if (lobjMemberDetails != null)
                //{
                //    if (lobjMemberDetails.MemberRelationsList[0].IsAccountActivated)
                //        throw new ApplicationException("Account Already Activated");
                //}
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("ActivationFacade GetMemberDetails-" + ex.InnerException + ex.Message + ex.StackTrace);
            }
            return lobjMemberDetails != null ? lobjMemberDetails : new MemberDetails();
        }

        public MemberDetails GetMemberDetailsByMemberIdNationalId(ActivationParameters pobjActivationParameters)
        {
            MemberBusinessFacade lobjMemberBusinessFacade = new MemberBusinessFacade();
            MemberDetails lobjMemberDetails = lobjMemberBusinessFacade.GetMemberDetailsForActivationByMemberIdNationalId(pobjActivationParameters);
            return lobjMemberDetails != null ? lobjMemberDetails : new MemberDetails();
        }

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

        public MemberDetails ActivationSuccessful(MemberDetails pobjMemberDetails)
        {
            //Framework.EnterpriseLibrary.Adapters.LoggingAdapter.WriteLog("RelationReference Number= " + pobjMemberDetails.MemberRelationsList[0].RelationReference + " Password= " + pobjMemberDetails.MemberRelationsList[0].WebPassword);
            PasswordDefinition lobjPasswordDefinition = new PasswordDefinition();
            string lstrPasswordDefinition = Convert.ToString(RandomPasswordGenerator.GeneratePassword(lobjPasswordDefinition));
            pobjMemberDetails.MemberRelationsList[0].WebPassword = RSAEncryptor.EncryptString((lstrPasswordDefinition), RSASecurityConstant.KeySize, RSASecurityConstant.RSAPublicKey);
            SPWrapper.ActivateMemberAccount(pobjMemberDetails);
            return pobjMemberDetails;
        }

        public bool ActivteUser(MemberDetails pobjMemberDetails)
        {
            bool lblnActivateUser = false;
            //PasswordDefinition lobjPasswordDefinition = new PasswordDefinition();
            ////pobjMemberDetails.MemberRelationsList[0].WebPassword = RSAEncryptor.EncryptString(Convert.ToString(RandomPasswordGenerator.GeneratePassword(lobjPasswordDefinition)), RSASecurityConstant.KeySize, RSASecurityConstant.RSAPublicKey);
            //pobjMemberDetails.MemberRelationsList[0].WebPassword = getMD5HashedString(pobjMemberDetails.NationalId + pobjMemberDetails.MemberRelationsList[0].WebPassword);

            lblnActivateUser = SPWrapper.ActivateMemberAccount(pobjMemberDetails);
            return lblnActivateUser;
        }

        public bool ActivteUserForLink(MemberDetails pobjMemberDetails)
        {
            bool lblnActivateUser = false;
            //PasswordDefinition lobjPasswordDefinition = new PasswordDefinition();
            ////pobjMemberDetails.MemberRelationsList[0].WebPassword = RSAEncryptor.EncryptString(Convert.ToString(RandomPasswordGenerator.GeneratePassword(lobjPasswordDefinition)), RSASecurityConstant.KeySize, RSASecurityConstant.RSAPublicKey);
            //pobjMemberDetails.MemberRelationsList[0].WebPassword = getMD5HashedString(pobjMemberDetails.NationalId + pobjMemberDetails.MemberRelationsList[0].WebPassword);

            lblnActivateUser = SPWrapper.ActivateMemberAccountForLink(pobjMemberDetails);
            return lblnActivateUser;
        }

        public bool ActivteUserAfterLink(MemberDetails pobjMemberDetails)
        {
            bool lblnActivateUser = false;
            try
            {
                lblnActivateUser = SPWrapper.ActivateMemberAccount(pobjMemberDetails);
                SendEmailSMSForActivationSuccess(pobjMemberDetails);
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("ActivteUserAfterLink ex-" + ex.Message + ex.InnerException + ex.StackTrace);
            }
            return lblnActivateUser;
        }

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

        public bool DeactiveMemberAccount(MemberDetails pobjMemberDetails)
        {
            return SPWrapper.DeactiveMemberAccount(pobjMemberDetails);
        }
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
                    // Communication Engine Call FOr Email.
                    EmailDetails lobjEmailDetail = new EmailDetails();
                    List<string> lstEmailparameter = new List<string>();
                    List<string> lstAttachment = new List<string>();
                    CEHelper lobjcehelper = new CEHelper();
                    lstEmailparameter.Add(pobjMemberDetails.LastName);
                    lstEmailparameter.Add(pobjMemberDetails.NationalId);
                    lstEmailparameter.Add(pobjMemberDetails.MemberRelationsList.Find(x => x.RelationType.Equals(RelationType.LBMS)).RelationReference);
                    lstEmailparameter.Add(Base64Encode(pobjMemberDetails.MemberRelationsList.Find(x => x.RelationType.Equals(RelationType.LBMS)).RelationReference));
                    //lstEmailparameter.Add(pobjMemberDetails.MemberRelationsList[0].RelationReference);
                    //lstEmailparameter.Add(RSAEncryptor.DecryptString(pobjMemberDetails.MemberRelationsList[0].WebPassword, RSASecurityConstant.KeySize, RSASecurityConstant.PrivateKey));
                    lobjEmailDetail.TemplateCode = "MemberActivation";
                    lobjEmailDetail.ListParameter = lstEmailparameter;
                    lobjEmailDetail.MemberId = Convert.ToString(pobjMemberDetails.MemberRelationsList.Find(lobj => lobj.RelationType.Equals(RelationType.LBMS)).RelationReference);
                    lobjEmailDetail.ProgramId = Convert.ToString(pobjMemberDetails.ProgramId);
                    lobjEmailDetail.AttachmentList = lstAttachment;
                    lobjEmailDetail.To = pobjMemberDetails.Email;
                    lobjcehelper.InsertEmailDetails(lobjEmailDetail);
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
                    CEHelper lobjcehelper = new CEHelper();
                    SmsDetails lobjSmsDetail = new SmsDetails();
                    List<string> lstSmsparameter = new List<string>();
                    lobjSmsDetail.TemplateCode = "MemberActivation";
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

            Framework.EnterpriseLibrary.Adapters.LoggingAdapter.WriteLog("RelationReference Number= " + pobjMemberDetails.MemberRelationsList[0].RelationReference + " Password= " + RSAEncryptor.DecryptString(pobjMemberDetails.MemberRelationsList[0].WebPassword, RSASecurityConstant.KeySize, RSASecurityConstant.PrivateKey));
            return pobjMemberDetails;
        }

        public MemberDetails SendEmailForActivationByNationalId(MemberDetails pobjMemberDetails)
        {
            try
            {
                if (pobjMemberDetails.Email != string.Empty)
                {
                    // Communication Engine Call FOr Email.
                    EmailDetails lobjEmailDetail = new EmailDetails();
                    List<string> lstEmailparameter = new List<string>();
                    List<string> lstAttachment = new List<string>();
                    CEHelper lobjcehelper = new CEHelper();
                    lstEmailparameter.Add(pobjMemberDetails.LastName);
                    lstEmailparameter.Add(pobjMemberDetails.NationalId);
                    lstEmailparameter.Add(RSAEncryptor.DecryptString(pobjMemberDetails.MemberRelationsList[0].WebPassword, RSASecurityConstant.KeySize, RSASecurityConstant.PrivateKey));
                    lobjEmailDetail.TemplateCode = "MemberActivation";
                    lobjEmailDetail.ListParameter = lstEmailparameter;
                    lobjEmailDetail.MemberId = Convert.ToString(pobjMemberDetails.MemberRelationsList.Find(lobj => lobj.RelationType.Equals(RelationType.LBMS)).RelationReference);
                    lobjEmailDetail.ProgramId = Convert.ToString(pobjMemberDetails.ProgramId);
                    lobjEmailDetail.AttachmentList = lstAttachment;
                    lobjEmailDetail.To = pobjMemberDetails.Email;
                    lobjcehelper.InsertEmailDetails(lobjEmailDetail);
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
                    CEHelper lobjcehelper = new CEHelper();
                    SmsDetails lobjSmsDetail = new SmsDetails();
                    List<string> lstSmsparameter = new List<string>();
                    lobjSmsDetail.TemplateCode = "MemberActivation";
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
        //    Framework.EnterpriseLibrary.Adapters.LoggingAdapter.WriteLog("SendEmailForActivation Member  mobile number" + pobjMemberDetails.MemberRelationsList[0].RelationReference);
        //    return pobjMemberDetails;
        //}
        //public void ActivationUnSuccessful()
        //{
        //    MasterFacade lobjMasterFacade = new MasterFacade();
        //    ProgramMaster lobjProgramMaster = null;
        //    lobjProgramMaster = lobjMasterFacade.GetProgramDetails(lstrProgramName);
        //    //throw new ApplicationException("Invalid info");
        //    throw new ApplicationException(ExceptionMessagesFacade.GetErrorMessage(102, lstrCulture, lobjProgramMaster.Id));
        //    // Framework.EnterpriseLibrary.Adapters.LoggingAdapter.WriteLog(System.DateTime.Now + " " + "ActivationUnSuccessful Process Started");
        //    //Throw ApplicationException with the Message
        //}

        public bool GenerateOTPDetails(OTPDetails pobjOTPDetails)
        {
            if (!GenerateOTP(pobjOTPDetails))
                return false;
            return true;
        }

        public bool GenerateOTPDetailsQIB(OTPDetails pobjOTPDetails)
        {
            if (!GenerateOTPQIB(pobjOTPDetails))
                return false;
            return true;
        }

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
                    else if (pobjOTPDetails.OtpEnumTypes.Equals(OTPEnumTypes.LOGIN))
                    {
                        if (Convert.ToString((ConfigurationManager.AppSettings["LOGINEMAIL"])).Equals("TRUE"))
                        {
                            SendEmail(lobjMemberDetails, lobjOTPDetails.OTP.ToString(), lobjOTPDetails.ExpiryDateTime);
                        }
                        SendSMSTemplate(lobjMemberDetails, lobjOTPDetails.OTP.ToString(), lobjOTPDetails.ExpiryDateTime, pobjOTPDetails);
                    }
                    else
                    {
                        if (Convert.ToString((ConfigurationManager.AppSettings["ACTIVATEEMAIL"])).Equals("TRUE"))
                        {
                            SendEmail(lobjMemberDetails, lobjOTPDetails.OTP.ToString(), lobjOTPDetails.ExpiryDateTime);
                        }
                        SendSMSTemplate(lobjMemberDetails, lobjOTPDetails.OTP.ToString(), lobjOTPDetails.ExpiryDateTime, pobjOTPDetails);
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


        private bool SendEmail(MemberDetails lobjMemberDetails, string pstrOTPstirng, DateTime pdtExpirydatetime)
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
                    CEHelper lobjcehelper = new CEHelper();
                    lstEmailparameter.Add(pstrOTPstirng);
                    lstEmailparameter.Add(lobjMemberDetails.LastName);
                    lstEmailparameter.Add(lobjMemberDetails.NationalId);
                    lstEmailparameter.Add(lobjMemberDetails.MemberRelationsList.Find(x => x.RelationType.Equals(RelationType.LBMS)).RelationReference);
                    lstEmailparameter.Add(Base64Encode(lobjMemberDetails.MemberRelationsList.Find(x => x.RelationType.Equals(RelationType.LBMS)).RelationReference));

                    if (lobjMemberDetails.MemberRelationsList[0].Status.Equals(Status.Blocked))
                    {
                        lobjEmailDetail.TemplateCode = "SendOTPForBlockUser";
                    }
                    else if (lobjMemberDetails.MemberRelationsList[0].Status.Equals(Status.InActive))
                    {
                        lobjEmailDetail.TemplateCode = "SendOTP";
                    }
                    else if (lobjMemberDetails.MemberRelationsList[0].Status.Equals(Status.Active))
                    {
                        lobjEmailDetail.TemplateCode = "LoginOTP";
                    }
                    else
                    {
                        lobjEmailDetail.TemplateCode = "SendOTPPooling";
                    }
                    LoggingAdapter.WriteLog("OTP Email Parameter" + lstEmailparameter.Count);
                    lobjEmailDetail.ListParameter = lstEmailparameter;
                    lobjEmailDetail.AttachmentList = lstAttachment;
                    lobjEmailDetail.To = lobjMemberDetails.Email;
                    lobjEmailDetail.MemberId = Convert.ToString(lobjMemberDetails.MemberRelationsList[0].RelationReference);
                    lobjEmailDetail.ProgramId = Convert.ToString(lobjMemberDetails.ProgramId);
                    lblnEmailSend = lobjcehelper.InsertEmailDetails(lobjEmailDetail);

                    LoggingAdapter.WriteLog("ActivationFacade SendEmail -" + lblnEmailSend);
                }
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("OTP password error :" + ex.Message);
                throw ex;
            }
            return lblnEmailSend;
        }

        private bool SendSMS(MemberDetails lobjMemberDetails, string pstrOTPstring, DateTime pdtExpirydatetime)
        {
            FinalSms objFinalSms = new FinalSms();
            bool lblnSMSSend = false;
            try
            {
                if (lobjMemberDetails.MobileNumber != string.Empty)
                {
                    string lstrexpirydt = pdtExpirydatetime.ToString("dd/MM/yyyy");
                    //Communication Engine Call For SMS. 
                    CEHelper lobjcehelper = new CEHelper();
                    SmsDetails lobjSmsDetail = new SmsDetails();

                    List<string> lstSMSparameter = new List<string>();                   
                    lstSMSparameter.Add(pstrOTPstring);

                    lobjSmsDetail.TemplateCode = "OTPSMS";
                    lobjSmsDetail.ListParameter = lstSMSparameter;
                    lobjSmsDetail.ProgramId = Convert.ToString(lobjMemberDetails.ProgramId);
                    lobjSmsDetail.MemberId = Convert.ToString(lobjMemberDetails.MemberRelationsList[0].RelationReference);
                    lobjSmsDetail.ReceiverMobile = lobjMemberDetails.MobileNumber;
                    lblnSMSSend = lobjcehelper.InsertSmsDetails(lobjSmsDetail);


                    //LoggingAdapter.WriteLog("Activation OTP SMS Start :");
                    //if (lobjMemberDetails.PreferredLanguage.ToUpper() == "AR")
                    //{
                    //    objFinalSms.TemplateCode = "AR" + lobjSmsDetail.TemplateCode;
                    //    objFinalSms.Message = Convert.ToString(ConfigurationManager.AppSettings["AROTPSMS"]);
                    //    objFinalSms.Message = string.Format(objFinalSms.Message, pstrOTPstring);
                    //}
                    //else
                    //{
                    //    objFinalSms.TemplateCode = lobjSmsDetail.TemplateCode;
                    //    objFinalSms.Message = Convert.ToString(ConfigurationManager.AppSettings["OTPSMS"]);
                    //    objFinalSms.Message = string.Format(objFinalSms.Message, pstrOTPstring);
                    //}
                    //SendSmsImplementor obj = new SendSmsImplementor();                    
                    //objFinalSms.MemberId = Convert.ToString(lobjMemberDetails.MemberRelationsList[0].RelationReference);
                    //objFinalSms.UserName = lobjMemberDetails.FirstName;
                    //objFinalSms.DetailReceiver = lobjMemberDetails.MobileNumber;

                    //HttpContext ctx = HttpContext.Current;
                    //System.Threading.Thread.Sleep(500);
                    //System.Threading.Tasks.Task.Factory.StartNew(() =>
                    //{
                    //    HttpContext.Current = ctx;
                    //    if (obj.SendSms(objFinalSms))
                    //    {
                    //        lblnSMSSend = true;

                    //        LoggingAdapter.WriteLog(lobjSmsDetail.TemplateCode + " ACTIVATION SMS OBJECT true : " + JSONSerialization.Serialize(objFinalSms));
                    //    }
                    //    else
                    //    {
                    //        lblnSMSSend = false;
                    //        LoggingAdapter.WriteLog(lobjSmsDetail.TemplateCode + "ACTIVATION SMS OBJECT False : " + JSONSerialization.Serialize(objFinalSms) + "TimeStamp ");
                    //    }
                    //});
                }
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("OTP SMS ERROR :" + ex.Message);
            }
            return lblnSMSSend;
        }

        private bool SendSMSTemplate(MemberDetails lobjMemberDetails, string pstrOTPstring, DateTime pdtExpirydatetime, OTPDetails pobjOTPDetails)
        {
            FinalSms objFinalSms = new FinalSms();
            bool lblnSMSSend = false;
            try
            {
                if (lobjMemberDetails.MobileNumber != string.Empty)
                {
                    string lstrexpirydt = pdtExpirydatetime.ToString("dd/MM/yyyy");
                    //Communication Engine Call For SMS. 
                    CEHelper lobjcehelper = new CEHelper();
                    SmsDetails lobjSmsDetail = new SmsDetails();
                    List<string> lstSmsparameter = new List<string>();
                    lstSmsparameter.Add(pstrOTPstring);
                    //lstSmsparameter.Add(lstrexpirydt);
                    LoggingAdapter.WriteLog("SendSMSTemplate Step1 " + DateTime.Now.ToString());
                    if (pobjOTPDetails.OtpEnumTypes.Equals(OTPEnumTypes.LOGIN))
                    {
                        //lobjSmsDetail.TemplateCode = "OTPLOGINSMS";
                        lobjSmsDetail.TemplateCode = "LOGIN";
                        if (lobjMemberDetails.PreferredLanguage.ToUpper() == "AR")
                        {
                            LoggingAdapter.WriteLog("SendSMSTemplate Step2 " + DateTime.Now.ToString());

                            objFinalSms.TemplateCode = "AR" + lobjSmsDetail.TemplateCode;
                            objFinalSms.Message = Convert.ToString(ConfigurationManager.AppSettings["AROTPLOGINSMS"]);
                            objFinalSms.Message = string.Format(objFinalSms.Message, pstrOTPstring);
                        }
                        else
                        {
                            LoggingAdapter.WriteLog("SendSMSTemplate Step2.1 " + DateTime.Now.ToString());
                            objFinalSms.TemplateCode = lobjSmsDetail.TemplateCode;
                            objFinalSms.Message = Convert.ToString(ConfigurationManager.AppSettings["OTPLOGINSMS"]);
                            objFinalSms.Message = string.Format(objFinalSms.Message, pstrOTPstring);
                        }
                    }
                    else if (pobjOTPDetails.OtpEnumTypes.Equals(OTPEnumTypes.ACTIVATION))
                    {
                        lobjSmsDetail.TemplateCode = "OTPSMS";
                        if (lobjMemberDetails.PreferredLanguage.ToUpper() == "AR")
                        {
                            LoggingAdapter.WriteLog("SendSMSTemplate Step3 " + DateTime.Now.ToString());

                            objFinalSms.TemplateCode = "AR" + lobjSmsDetail.TemplateCode;
                            objFinalSms.Message = Convert.ToString(ConfigurationManager.AppSettings["AROTPSMS"]);
                            objFinalSms.Message = string.Format(objFinalSms.Message, pstrOTPstring);
                        }
                        else
                        {
                            LoggingAdapter.WriteLog("SendSMSTemplate Step3.1 " + DateTime.Now.ToString());
                            objFinalSms.TemplateCode = lobjSmsDetail.TemplateCode;
                            objFinalSms.Message = Convert.ToString(ConfigurationManager.AppSettings["OTPSMS"]);
                            objFinalSms.Message = string.Format(objFinalSms.Message, pstrOTPstring);
                        }

                    }
                    else
                    {
                        lobjSmsDetail.TemplateCode = "OTPSMS";
                        if (lobjMemberDetails.PreferredLanguage.ToUpper() == "AR")
                        {
                            objFinalSms.TemplateCode = "AR" + lobjSmsDetail.TemplateCode;
                            objFinalSms.Message = Convert.ToString(ConfigurationManager.AppSettings["AROTPSMS"]);
                            objFinalSms.Message = string.Format(objFinalSms.Message, pstrOTPstring);
                        }
                        else
                        {
                            objFinalSms.TemplateCode = lobjSmsDetail.TemplateCode;
                            objFinalSms.Message = Convert.ToString(ConfigurationManager.AppSettings["OTPSMS"]);
                            objFinalSms.Message = string.Format(objFinalSms.Message, pstrOTPstring);
                        }
                    }
                    lobjSmsDetail.ListParameter = lstSmsparameter;
                    lobjSmsDetail.ReceiverMobile = Convert.ToString(lobjMemberDetails.MobileNumber);
                    lobjSmsDetail.MemberId = Convert.ToString(lobjMemberDetails.MemberRelationsList[0].RelationReference);
                    lobjSmsDetail.ProgramId = Convert.ToString(lobjMemberDetails.ProgramId);
                    lblnSMSSend = lobjcehelper.InsertSmsDetails(lobjSmsDetail);

                    LoggingAdapter.WriteLog("ActivationFacade SendSMS -" + lblnSMSSend);

                    //LoggingAdapter.WriteLog("SendSMSTemplate Step 4 " + DateTime.Now.ToString());                    
                    //HttpContext ctx = HttpContext.Current;
                    //System.Threading.Thread.Sleep(500);
                    //System.Threading.Tasks.Task.Factory.StartNew(() =>
                    //{
                    //    HttpContext.Current = ctx;
                    //    SendSmsImplementor obj = new SendSmsImplementor();


                    //    objFinalSms.MemberId = Convert.ToString(lobjMemberDetails.MemberRelationsList[0].RelationReference);
                    //    objFinalSms.UserName = lobjMemberDetails.FirstName;
                    //    objFinalSms.DetailReceiver = lobjMemberDetails.MobileNumber;
                    //    LoggingAdapter.WriteLog("SendSMSTemplate Step 4 " + DateTime.Now.ToString());
                    //    if (obj.SendSms(objFinalSms))
                    //    {
                    //        lblnSMSSend = true;

                    //        LoggingAdapter.WriteLog(lobjSmsDetail.TemplateCode + " SMS OBJECT true : " + JSONSerialization.Serialize(objFinalSms));
                    //    }
                    //    else
                    //    {
                    //        lblnSMSSend = false;
                    //        LoggingAdapter.WriteLog(lobjSmsDetail.TemplateCode + " SMS OBJECT False : " + JSONSerialization.Serialize(objFinalSms) + "TimeStamp " + DateTime.Now.ToString());
                    //    }
                    //});

                }
                LoggingAdapter.WriteLog("SendSMSTemplate END " + DateTime.Now.ToString());

            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("OTP SMS ERROR :" + DateTime.Now.ToString());
                LoggingAdapter.WriteLog("OTP SMS ERROR-" + ex.Message + ex.InnerException + ex.StackTrace);

            }
            return lblnSMSSend;
        }



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

        public bool UpdateValidOTPForActivationLink(MemberDetails pobjMemberDetails)
        {
            bool lblnIsResponse = false;
            try
            {
                //if (pobjMemberDetails.AdditionalDetails1 == "MobAPI")
                //{
                //    pobjMemberDetails.Email = pobjMemberDetails.Email + "|MobAPI";
                //    lblnIsResponse = ActivteUserForLink(pobjMemberDetails);
                //}
                //else
                //{
                //    lblnIsResponse = ActivteUserForLink(pobjMemberDetails);
                //}

                lblnIsResponse = ActivteUserForLink(pobjMemberDetails);

                if (lblnIsResponse)
                {
                    SendEmailForActivation(pobjMemberDetails);
                }

                //if (lblnIsResponse && pobjMemberDetails.AdditionalDetails1 != "MobAPI")
                //{
                //    SendEmailForActivation(pobjMemberDetails);
                //}
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("UpdateValidOTPForActivationLink Ex-" + ex.Message + ex.InnerException + ex.StackTrace);
            }
            return lblnIsResponse;
        }

        /// <summary>
        /// UpdateValidOTPByCIFNationalId NBO
        /// </summary>
        /// <param name="pobjOTPDetails"></param>
        /// <param name="pobjMemberDetails"></param>
        /// <returns></returns>
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
        /// <param name="pobjMemberDetails"></param>
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

                        lblnUpdateSuccess = SPWrapper.UpdateMemberDetailsForActivation(lobjMemberRelation, lobjMemberDetails);

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


        // UAB Activation Email and SMS
        public void SendEmailSMSForActivation(MemberDetails pobjMemberDetails)
        {
            try
            {
                if (pobjMemberDetails.Email != string.Empty)
                {
                    // Communication Engine Call FOr Email.
                    EmailDetails lobjEmailDetail = new EmailDetails();
                    List<string> lstEmailparameter = new List<string>();
                    List<string> lstAttachment = new List<string>();
                    CEHelper lobjcehelper = new CEHelper();
                    lstEmailparameter.Add(pobjMemberDetails.MemberRelationsList.Find(lobj => lobj.RelationType.Equals(RelationType.LBMS)).RelationReference);
                    lobjEmailDetail.TemplateCode = "MemberActivation";
                    lobjEmailDetail.ListParameter = lstEmailparameter;
                    lobjEmailDetail.MemberId = Convert.ToString(pobjMemberDetails.MemberRelationsList.Find(lobj => lobj.RelationType.Equals(RelationType.LBMS)).RelationReference);
                    lobjEmailDetail.ProgramId = Convert.ToString(pobjMemberDetails.ProgramId);
                    lobjEmailDetail.AttachmentList = lstAttachment;
                    lobjEmailDetail.To = pobjMemberDetails.Email;
                    lobjcehelper.InsertEmailDetails(lobjEmailDetail);
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
                    CEHelper lobjcehelper = new CEHelper();
                    SmsDetails lobjSmsDetail = new SmsDetails();
                    List<string> lstSmsparameter = new List<string>();
                    lobjSmsDetail.TemplateCode = "MemberActivation";
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

            //    Framework.EnterpriseLibrary.Adapters.LoggingAdapter.WriteLog("RelationReference Number= " + pobjMemberDetails.MemberRelationsList.Find(lobj => lobj.RelationType.Equals("LBMS")).RelationReference + " Password= " + RSAEncryptor.DecryptString(pobjMemberDetails.MemberRelationsList.Find(lobj => lobj.RelationType.Equals("LBMS")).WebPassword, RSASecurityConstant.KeySize, RSASecurityConstant.PrivateKey));            
        }

        /// <summary>
        /// UAB Rewards : This Method is used in MemberActivation Method to authenticate user credenials.. 
        /// </summary>
        /// <param name="pobjActivationParameters"></param>
        /// <param name="lobjMemberDetails"></param>
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
        /// <param name="pobjMemberDetails"></param>
        /// <returns></returns>
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

                        lblnUpdateSuccess = SPWrapper.UpdateMemberDetailsForActivation(lobjMemberRelation, lobjMemberDetails);

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
        /// <param name="pobjActivationParameters"></param>
        /// <param name="lobjMemberDetails"></param>
        /// <returns></returns>
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

        // skyymiles Activation Email and SMS
        public void SendActivationEmailSMS(MemberDetails pobjMemberDetails)
        {
            try
            {
                if (pobjMemberDetails.Email != string.Empty)
                {
                    // Communication Engine Call FOr Email.
                    EmailDetails lobjEmailDetail = new EmailDetails();
                    List<string> lstEmailparameter = new List<string>();
                    List<string> lstAttachment = new List<string>();
                    CEHelper lobjcehelper = new CEHelper();
                    lstEmailparameter.Add(pobjMemberDetails.LastName);
                    lobjEmailDetail.TemplateCode = "MemberActivation";
                    lobjEmailDetail.ListParameter = lstEmailparameter;
                    lobjEmailDetail.MemberId = Convert.ToString(pobjMemberDetails.MemberRelationsList.Find(lobj => lobj.RelationType.Equals(RelationType.LBMS)).RelationReference);
                    lobjEmailDetail.ProgramId = Convert.ToString(pobjMemberDetails.ProgramId);
                    lobjEmailDetail.AttachmentList = lstAttachment;
                    lobjEmailDetail.To = pobjMemberDetails.Email;
                    lobjcehelper.InsertEmailDetails(lobjEmailDetail);
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
                        CEHelper lobjcehelper = new CEHelper();
                        SmsDetails lobjSmsDetail = new SmsDetails();
                        List<string> lstSmsparameter = new List<string>();
                        lobjSmsDetail.TemplateCode = "MemberActivation";
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
                        CEHelper lobjcehelper = new CEHelper();
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
        /// <param name="pobjActivationParameters"></param>
        /// <returns></returns>
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

                        lblnUpdateSuccess = SPWrapper.UpdateMemberDetailsForActivation(lobjMemberRelation, lobjMemberDetails);

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

        //MAF
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

        // MAF Activation Email and SMS
        public void SendEmailSMSActivation(MemberDetails pobjMemberDetails)
        {
            try
            {
                if (pobjMemberDetails.Email != string.Empty)
                {
                    // Communication Engine Call FOr Email.
                    EmailDetails lobjEmailDetail = new EmailDetails();
                    List<string> lstEmailparameter = new List<string>();
                    List<string> lstAttachment = new List<string>();
                    CEHelper lobjcehelper = new CEHelper();
                    lstEmailparameter.Add(pobjMemberDetails.LastName);
                    lstEmailparameter.Add(pobjMemberDetails.MemberRelationsList.Find(lobj => lobj.RelationType.Equals(RelationType.LBMS)).RelationReference);
                    lstEmailparameter.Add(RSAEncryptor.DecryptString(pobjMemberDetails.MemberRelationsList.Find(lobj => lobj.RelationType.Equals(RelationType.LBMS)).WebPassword, RSASecurityConstant.KeySize, RSASecurityConstant.PrivateKey));
                    lobjEmailDetail.TemplateCode = "MemberActivation";
                    lobjEmailDetail.ListParameter = lstEmailparameter;
                    lobjEmailDetail.MemberId = Convert.ToString(pobjMemberDetails.MemberRelationsList.Find(lobj => lobj.RelationType.Equals(RelationType.LBMS)).RelationReference);
                    lobjEmailDetail.ProgramId = Convert.ToString(pobjMemberDetails.ProgramId);
                    lobjEmailDetail.AttachmentList = lstAttachment;
                    lobjEmailDetail.To = pobjMemberDetails.Email;
                    lobjcehelper.InsertEmailDetails(lobjEmailDetail);
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
                    CEHelper lobjcehelper = new CEHelper();
                    SmsDetails lobjSmsDetail = new SmsDetails();
                    List<string> lstSmsparameter = new List<string>();
                    lobjSmsDetail.TemplateCode = "MemberActivation";
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

            //    Framework.EnterpriseLibrary.Adapters.LoggingAdapter.WriteLog("RelationReference Number= " + pobjMemberDetails.MemberRelationsList.Find(lobj => lobj.RelationType.Equals("LBMS")).RelationReference + " Password= " + RSAEncryptor.DecryptString(pobjMemberDetails.MemberRelationsList.Find(lobj => lobj.RelationType.Equals("LBMS")).WebPassword, RSASecurityConstant.KeySize, RSASecurityConstant.PrivateKey));            
        }


        /// <summary>
        /// created for CBI
        /// </summary>
        /// <param name="pobjActivationParameters"></param>
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


        private bool CheckMemberEmailMobAuthentication(ActivationParameters pobjActivationParameters, MemberDetails lobjMemberDetails)
        {
            //bool lblnMembershipReference = pobjActivationParameters.RelationReference.ToUpper().Equals(lobjMemberDetails.MemberRelationsList.Find(lobj => lobj.RelationType.Equals(RelationType.LBMS)).RelationReference.ToUpper());
            bool lblnMobileNumber = pobjActivationParameters.MobileNumber.Trim().ToUpper().Equals(lobjMemberDetails.MobileNumber.Trim().ToUpper());
            bool lblnEmailId = pobjActivationParameters.Email.ToUpper().Equals(lobjMemberDetails.Email.ToUpper());

            //return lblnEmail ? (lblnDOB || lblnMothersMaidenName) : (lblnDOB && lblnMothersMaidenName);

            return lblnMobileNumber && lblnEmailId;
        }

        /// <summary>
        /// UpdateValidOTPByMemberId CBI
        /// </summary>
        /// <param name="pobjOTPDetails"></param>
        /// <param name="pobjMemberDetails"></param>
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
        /// <param name="pobjMemberDetails"></param>
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
                            CEHelper lobjcehelper = new CEHelper();
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
                            lobjcehelper.InsertEmailDetails(lobjEmailDetail);
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
        /// <param name="pobjActivationParameters"></param>
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
        /// <param name="pobjActivationParameters"></param>
        /// <param name="lobjMemberDetails"></param>
        /// <returns></returns>
        private bool CheckMemberActivationDetails(ActivationParameters pobjActivationParameters, MemberDetails lobjMemberDetails)
        {
            if (lobjMemberDetails == null)
                return false;

            bool lblnMobileNumber = pobjActivationParameters.MobileNumber.Trim().ToUpper().Equals(lobjMemberDetails.MobileNumber.Trim().ToUpper());
            bool lblnAdditionalDetails = pobjActivationParameters.AdditionalDetails.Trim().ToUpper().Equals(lobjMemberDetails.AdditionalDetails.Trim().ToUpper());

            return lblnMobileNumber && lblnAdditionalDetails;
        }


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

        public MemberDetails SendLoginRelationEmailForActivation(MemberDetails pobjMemberDetails)
        {
            try
            {
                if (pobjMemberDetails.Email != string.Empty)
                {
                    // Communication Engine Call FOr Email.
                    EmailDetails lobjEmailDetail = new EmailDetails();
                    List<string> lstEmailparameter = new List<string>();
                    List<string> lstAttachment = new List<string>();
                    CEHelper lobjcehelper = new CEHelper();
                    lstEmailparameter.Add(pobjMemberDetails.LastName);
                    lobjEmailDetail.TemplateCode = "MemberActivation";
                    lobjEmailDetail.ListParameter = lstEmailparameter;
                    lobjEmailDetail.MemberId = Convert.ToString(pobjMemberDetails.MemberRelationsList.Find(lobj => lobj.RelationType.Equals(RelationType.Login)).RelationReference);
                    lobjEmailDetail.ProgramId = Convert.ToString(pobjMemberDetails.ProgramId);
                    lobjEmailDetail.AttachmentList = lstAttachment;
                    lobjEmailDetail.To = pobjMemberDetails.Email;
                    lobjcehelper.InsertEmailDetails(lobjEmailDetail);
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
                    CEHelper lobjcehelper = new CEHelper();
                    SmsDetails lobjSmsDetail = new SmsDetails();
                    List<string> lstSmsparameter = new List<string>();
                    lobjSmsDetail.TemplateCode = "MemberActivation";
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

            Framework.EnterpriseLibrary.Adapters.LoggingAdapter.WriteLog("RelationReference Number= " + pobjMemberDetails.MemberRelationsList[0].RelationReference + " Password= " + RSAEncryptor.DecryptString(pobjMemberDetails.MemberRelationsList[0].WebPassword, RSASecurityConstant.KeySize, RSASecurityConstant.PrivateKey));
            return pobjMemberDetails;
        }


        public string KCBServiceGenerateOTPDetails(OTPDetails pobjOTPDetails)
        {
            string lstrOTP = string.Empty;
            lstrOTP = KCBServiceGenerateOTP(pobjOTPDetails);
            return lstrOTP;
        }

        public string KCBServiceGenerateOTP(OTPDetails pobjOTPDetails)
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
                SendEmail(lobjMemberDetails, lobjOTPDetails.OTP.ToString(), lobjOTPDetails.ExpiryDateTime);
                SendSMS(lobjMemberDetails, lobjOTPDetails.OTP.ToString(), lobjOTPDetails.ExpiryDateTime);
                return lobjOTPDetails.OTP.ToString();
            }
            return string.Empty;
        }

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
                    CEHelper lobjcehelper = new CEHelper();
                    lstEmailparameter.Add(pstrOTPstirng);
                    lstEmailparameter.Add(lobjMemberDetails.LastName);
                    lobjEmailDetail.TemplateCode = "PointTransferOTP";
                    LoggingAdapter.WriteLog("OTP Email Parameter" + lstEmailparameter.Count);
                    lobjEmailDetail.ListParameter = lstEmailparameter;
                    lobjEmailDetail.AttachmentList = lstAttachment;
                    lobjEmailDetail.To = lobjMemberDetails.Email;
                    lobjEmailDetail.MemberId = Convert.ToString(lobjMemberDetails.MemberRelationsList[0].RelationReference);
                    lobjEmailDetail.ProgramId = Convert.ToString(lobjMemberDetails.ProgramId);
                    lblnEmailSend = lobjcehelper.InsertEmailDetails(lobjEmailDetail);
                }
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("OTP password error :" + ex.Message);
                throw ex;
            }
            return lblnEmailSend;
        }

        private bool SendSMSForPointTransferOTP(MemberDetails lobjMemberDetails, string pstrOTPstring, DateTime pdtExpirydatetime, OTPDetails pobjOTPDetails)
        {
            bool lblnSMSSend = false;
            try
            {
                if (lobjMemberDetails.MobileNumber != string.Empty)
                {
                    string lstrexpirydt = pdtExpirydatetime.ToString("dd/MM/yyyy");
                    //Communication Engine Call For SMS. 
                    CEHelper lobjcehelper = new CEHelper();
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

        private string getMD5HashedString(string pstSecureData)
        {
            string lstSecureHash = string.Empty;
            MD5 md5HashAlgo = MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(pstSecureData);
            byte[] hashBytes = md5HashAlgo.ComputeHash(inputBytes);
            StringBuilder secureData = new StringBuilder();
            for (int i = 0; i < hashBytes.Length; i++)
            {
                secureData.Append(hashBytes[i].ToString("X2"));
            }
            lstSecureHash = Convert.ToString(secureData);
            return lstSecureHash;
        }

        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        public void SendEmailSMSForActivationSuccess(MemberDetails pobjMemberDetails)
        {
            try
            {
                if (pobjMemberDetails.Email != string.Empty)
                {
                    // Communication Engine Call FOr Email.
                    EmailDetails lobjEmailDetail = new EmailDetails();
                    List<string> lstEmailparameter = new List<string>();
                    List<string> lstAttachment = new List<string>();
                    CEHelper lobjcehelper = new CEHelper();
                    lstEmailparameter.Add(pobjMemberDetails.MemberRelationsList.Find(lobj => lobj.RelationType.Equals(RelationType.LBMS)).RelationReference);
                    lstEmailparameter.Add(pobjMemberDetails.LastName);
                    lstEmailparameter.Add(pobjMemberDetails.NationalId);
                    lstEmailparameter.Add(pobjMemberDetails.Email);
                    lstEmailparameter.Add(pobjMemberDetails.MobileNumber);
                    lobjEmailDetail.TemplateCode = "MemberActivationSuccess";
                    lobjEmailDetail.ListParameter = lstEmailparameter;
                    lobjEmailDetail.MemberId = Convert.ToString(pobjMemberDetails.MemberRelationsList.Find(lobj => lobj.RelationType.Equals(RelationType.LBMS)).RelationReference);
                    lobjEmailDetail.ProgramId = Convert.ToString(pobjMemberDetails.ProgramId);
                    lobjEmailDetail.AttachmentList = lstAttachment;
                    lobjEmailDetail.To = pobjMemberDetails.Email;
                    lobjcehelper.InsertEmailDetails(lobjEmailDetail);
                }
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("SendEmailSMSForActivationSuccess Email :" + ex.Message + ex.InnerException + ex.StackTrace);
            }

            try
            {
                if (pobjMemberDetails.MobileNumber != string.Empty)
                {
                    //Communication Engine Call For SMS. 
                    CEHelper lobjcehelper = new CEHelper();
                    SmsDetails lobjSmsDetail = new SmsDetails();
                    List<string> lstSmsparameter = new List<string>();
                    lobjSmsDetail.TemplateCode = "MemberActivationSuccess";
                    //lstSmsparameter.Add(RSAEncryptor.DecryptString(pobjMemberDetails.MemberRelationsList.Find(lobj => lobj.RelationType.Equals(RelationType.LBMS)).WebPassword, RSASecurityConstant.KeySize, RSASecurityConstant.PrivateKey));
                    lstSmsparameter.Add(pobjMemberDetails.LastName);
                    lstSmsparameter.Add(pobjMemberDetails.NationalId);
                    lstSmsparameter.Add(pobjMemberDetails.Email);
                    lstSmsparameter.Add(pobjMemberDetails.MobileNumber);
                    lobjSmsDetail.ListParameter = lstSmsparameter;
                    lobjSmsDetail.ProgramId = Convert.ToString(pobjMemberDetails.ProgramId);
                    lobjSmsDetail.MemberId = Convert.ToString(pobjMemberDetails.MemberRelationsList.Find(lobj => lobj.RelationType.Equals(RelationType.LBMS)).RelationReference);
                    lobjSmsDetail.ReceiverMobile = Convert.ToString(pobjMemberDetails.MobileNumber);
                    lobjcehelper.InsertSmsDetails(lobjSmsDetail);
                }
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("SendEmailSMSForActivationSuccess SMS :" + ex.Message + ex.InnerException + ex.StackTrace);
            }
        }
    }
}

