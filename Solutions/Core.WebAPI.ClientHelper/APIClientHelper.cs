using CB.IBE.DomesticFlight.Entities;
using CE.Entities;
using Core.Platform.CoreWebAPI.Response;
using Core.Platform.ExpirySchedule.Entities;
using Core.Platform.Member.Entites;
using Core.Platform.MemberActivity.Entities;
using Core.Platform.OTP.Entities;
using Core.Platform.ProgramMaster.Entities;
using Core.Platform.RedemptionAuditTrail.Entities;
using Core.Platform.Transactions.Entites;
using Core.Platform.TransactionSummary.Entites;
using Framework.EnterpriseLibrary.Adapters;
using Framework.EnterpriseLibrary.CommunicationEngine.Entity;
using Framework.EnterpriseLibrary.PasswordReset.Entities;
using GiiftPaymentGateway.Entities;
using LoyaltyManagement.Request;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using TransactionDetailsAdditionalInfo.Entities;

namespace Core.WebAPI.ClientHelper
{
    public class APIClientHelper
    {
        #region OAuth API
        public TokenModel GetAuthTokenforWebAPI(CoreAuthTokenRequest authTokenRequest)
        {
            TokenModel lobjResponse;
            try
            {
                APIResponseResults lobjAPIResponseResults = new APIResponseResults();
                lobjResponse = JsonConvert.DeserializeObject<TokenModel>(WebAPIHelper.PostData(APIConstant.Token, "POST", "GetAuthToken", JsonConvert.SerializeObject(authTokenRequest)));
                //if (lobjAPIResponseResults.results.IsSucessful)
                //{
                //    lobjResponse = JsonConvert.DeserializeObject<TokenModel>(JsonConvert.SerializeObject(lobjAPIResponseResults.results.ReturnObject));
                //}
                //else
                //{
                //    lobjResponse = null;
                //}
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("APIClientHelper GetAuthTokenforWebAPI Exception: " + ex.Message + Environment.NewLine + ex.StackTrace);
                lobjResponse = null;
            }
            return lobjResponse;
        }

        #endregion

        #region Member API

        public MemberDetails CheckMembershipCredentialsByRelationReference(LoginDetails pobjloginDetails, string pstrToken)
        {
            MemberDetails lobjResponse;
            try
            {
                APIResponseResults lobjAPIResponseResults = new APIResponseResults();
                lobjAPIResponseResults = JsonConvert.DeserializeObject<APIResponseResults>(WebAPIHelper.PostData(APIConstant.CheckMembershipCredentialsByRelationReference, "POST", "CheckMembershipCredentialsByRelationReference", JsonConvert.SerializeObject(pobjloginDetails), pstrToken));
                if (lobjAPIResponseResults.results.IsSucessful)
                {
                    lobjResponse = JsonConvert.DeserializeObject<MemberDetails>(JsonConvert.SerializeObject(lobjAPIResponseResults.results.ReturnObject));
                }
                else
                {
                    lobjResponse = null;
                }
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("APIClientHelper CheckMembershipCredentialsByRelationReference Exception: " + ex.Message + Environment.NewLine + ex.StackTrace);
                lobjResponse = null;
            }
            return lobjResponse;
        }

        public MemberDetails CheckMembershipCredentialsByNationalID(LoginDetails plobjLoginDetails, string pstrToken)
        {
            MemberDetails lobjResponse = null;
            try
            {
                APIResponseResults lobjAPIResponseResults = new APIResponseResults();
                lobjAPIResponseResults = JsonConvert.DeserializeObject<APIResponseResults>(WebAPIHelper.PostData(APIConstant.CheckMembershipCredentialsByNationalID, "POST", "CheckMembershipCredentialsByNationalID", JsonConvert.SerializeObject(plobjLoginDetails), pstrToken));
                if (lobjAPIResponseResults.results.IsSucessful)
                {
                    lobjResponse = JsonConvert.DeserializeObject<MemberDetails>(JsonConvert.SerializeObject(lobjAPIResponseResults.results.ReturnObject));
                }
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("CheckMembershipCredentialsByNationalID Exception: " + ex.Message + Environment.NewLine + ex.StackTrace);
            }
            return lobjResponse;
        }

        public MemberDetails GetMemberDetails(string pstrRelationReference, int pintProgramId, int pintRelationType, string pstrToken)
        {
            MemberDetails lobjResponse;
            try
            {
                APIResponseResults lobjAPIResponseResults = new APIResponseResults();
                lobjAPIResponseResults = JsonConvert.DeserializeObject<APIResponseResults>(WebAPIHelper.PostData(string.Format("{0}?pstrRelationReference={1}&pintProgramId={2}&pintRelationType={3}", APIConstant.GetMemberProfile, pstrRelationReference, pintProgramId, pintRelationType), "GET", "GetMemberProfile", string.Empty, pstrToken));
                if (lobjAPIResponseResults.results.IsSucessful)
                {
                    lobjResponse = JsonConvert.DeserializeObject<MemberDetails>(JsonConvert.SerializeObject(lobjAPIResponseResults.results.ReturnObject));
                }
                else
                {
                    lobjResponse = null;
                }
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("APIClientHelper GetMemberDetails Exception: " + ex.Message + Environment.NewLine + ex.StackTrace);
                lobjResponse = null;
            }
            return lobjResponse;
        }

        public MemberDetails GetMemberDetailsByNationalID(string pstrNationalID, int pintProgramId, int pintRelationType, string pstrToken)
        {
            MemberDetails lobjResponse;
            try
            {
                APIResponseResults lobjAPIResponseResults = new APIResponseResults();
                lobjAPIResponseResults = JsonConvert.DeserializeObject<APIResponseResults>(WebAPIHelper.PostData(string.Format("{0}?pstrNationalID={1}&pintProgramId={2}&pintRelationType={3}", APIConstant.GetMemberDetailsByNationalID, pstrNationalID, pintProgramId, pintRelationType), "GET", "GetMemberDetailsByNationalID", string.Empty, pstrToken));
                if (lobjAPIResponseResults.results.IsSucessful)
                {
                    lobjResponse = JsonConvert.DeserializeObject<MemberDetails>(JsonConvert.SerializeObject(lobjAPIResponseResults.results.ReturnObject));
                }
                else
                {
                    lobjResponse = null;
                }
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("APIClientHelper GetMemberDetailsByNationalID Exception: " + ex.Message + Environment.NewLine + ex.StackTrace);
                lobjResponse = null;
            }
            return lobjResponse;
        }

        public MemberDetails GetMemberDetailsByEmailID(int pintProgramId,string pstrEmailID,  int pintRelationType, string pstrToken)
        {
            MemberDetails lobjResponse;
            try
            {
                APIResponseResults lobjAPIResponseResults = new APIResponseResults();
                lobjAPIResponseResults = JsonConvert.DeserializeObject<APIResponseResults>(WebAPIHelper.PostData(string.Format("{0}?pstrEmailID={1}&pintProgramId={2}&pintRelationType={3}", APIConstant.GetMemberDetailsByEmailID, pstrEmailID, pintProgramId, pintRelationType), "GET", "GetMemberDetailsByNationalID", string.Empty, pstrToken));
                if (lobjAPIResponseResults.results.IsSucessful)
                {
                    lobjResponse = JsonConvert.DeserializeObject<MemberDetails>(JsonConvert.SerializeObject(lobjAPIResponseResults.results.ReturnObject));
                }
                else
                {
                    lobjResponse = null;
                }
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("APIClientHelper GetMemberDetailsByEmailID Exception: " + ex.Message + Environment.NewLine + ex.StackTrace);
                lobjResponse = null;
            }
            return lobjResponse;
        }

        public bool ChangePasswordForMember(MemberRelation lobjMemberRelation, string pstrToken)
        {
            bool lobjResponse = false;
            try
            {
                APIResponseResults lobjAPIResponseResults = new APIResponseResults();
                lobjAPIResponseResults = JsonConvert.DeserializeObject<APIResponseResults>(WebAPIHelper.PostData(APIConstant.ChangePasswordForMember, "POST", "ChangePasswordForMember", JsonConvert.SerializeObject(lobjMemberRelation), pstrToken));
                if (lobjAPIResponseResults.results.IsSucessful)
                {
                    lobjResponse = JsonConvert.DeserializeObject<bool>(JsonConvert.SerializeObject(lobjAPIResponseResults.results.ReturnObject));
                }
                else
                {
                    lobjResponse = false;
                }
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("APIClientHelper ChangePasswordForMember Exception: " + ex.Message + Environment.NewLine + ex.StackTrace);
                lobjResponse = false;
            }
            return lobjResponse;
        }

        public bool GenerateOTPByRelationReference(OTPDetails plobjOTPDetails, string pstrToken)
        {
            bool lblnResponse = false;
            try
            {
                APIResponseResults lobjAPIResponseResults = new APIResponseResults();
                lobjAPIResponseResults = JsonConvert.DeserializeObject<APIResponseResults>(WebAPIHelper.PostData(APIConstant.GenerateOTPByRelationReference, "POST", "GenerateOTPByRelationReference", JsonConvert.SerializeObject(plobjOTPDetails), pstrToken));
                if (lobjAPIResponseResults.results.IsSucessful)
                {
                    lblnResponse = JsonConvert.DeserializeObject<bool>(JsonConvert.SerializeObject(lobjAPIResponseResults.results.ReturnObject));
                }
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("APIClientHelper GenerateOTPByRelationReference Exception: " + ex.Message + Environment.NewLine + ex.StackTrace);
            }
            return lblnResponse;
        }

        public bool CheckMemberExistsUsingEmailID(string pstrEmailId, string pstrToken)
        {
            bool lblnResponse = false;
            try
            {
                APIResponseResults lobjAPIResponseResults = new APIResponseResults();
                lobjAPIResponseResults = JsonConvert.DeserializeObject<APIResponseResults>(WebAPIHelper.PostData(APIConstant.CheckMemberExistsUsingEmailID, "POST", "CheckMemberExistsUsingEmailID", JsonConvert.SerializeObject(pstrEmailId), pstrToken));
                if (lobjAPIResponseResults.results.IsSucessful)
                {
                    lblnResponse = JsonConvert.DeserializeObject<bool>(JsonConvert.SerializeObject(lobjAPIResponseResults.results.ReturnObject));
                }
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("APIClientHelper CheckMemberExistsUsingEmailID Exception: " + ex.Message + Environment.NewLine + ex.StackTrace);
            }
            return lblnResponse;
        }

        public bool MemberActivation(SearchMember pobjSearchMember, string pstrToken)
        {
            bool lblnResponse = false;
            try
            {
                APIResponseResults lobjAPIResponseResults = new APIResponseResults();
                lobjAPIResponseResults = JsonConvert.DeserializeObject<APIResponseResults>(WebAPIHelper.PostData(APIConstant.MemberActivation, "POST", "MemberActivation", JsonConvert.SerializeObject(pobjSearchMember), pstrToken));
                if (lobjAPIResponseResults.results.IsSucessful)
                {
                    lblnResponse = JsonConvert.DeserializeObject<bool>(JsonConvert.SerializeObject(lobjAPIResponseResults.results.ReturnObject));
                }
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("APIClientHelper MemberActivation Exception: " + ex.Message + Environment.NewLine + ex.StackTrace);
            }
            return lblnResponse;
        }

        public bool MemberActivationByRelationReference(SearchMember pobjSearchMember, string pstrToken)
        {
            bool lblnResponse = false;
            try
            {
                APIResponseResults lobjAPIResponseResults = new APIResponseResults();
                lobjAPIResponseResults = JsonConvert.DeserializeObject<APIResponseResults>(WebAPIHelper.PostData(APIConstant.MemberActivationByRelationReference, "POST", "MemberActivationByRelationReference", JsonConvert.SerializeObject(pobjSearchMember), pstrToken));
                if (lobjAPIResponseResults.results.IsSucessful)
                {
                    lblnResponse = JsonConvert.DeserializeObject<bool>(JsonConvert.SerializeObject(lobjAPIResponseResults.results.ReturnObject));
                }
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("APIClientHelper MemberActivation Exception: " + ex.Message + Environment.NewLine + ex.StackTrace);
            }
            return lblnResponse;
        }

        public bool VerifyOTP(OTPDetails plobjOTPDetails, string pstrToken)
        {
            bool lblnResponse = false;
            try
            {
                APIResponseResults lobjAPIResponseResults = new APIResponseResults();
                lobjAPIResponseResults = JsonConvert.DeserializeObject<APIResponseResults>(WebAPIHelper.PostData(APIConstant.VerifyOTPByRelationReference, "POST", "VerifyOTPByRelationReference", JsonConvert.SerializeObject(plobjOTPDetails), pstrToken));
                if (lobjAPIResponseResults.results.IsSucessful)
                {
                    lblnResponse = JsonConvert.DeserializeObject<bool>(JsonConvert.SerializeObject(lobjAPIResponseResults.results.ReturnObject));
                }
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("APIClientHelper VerifyOTP Exception: " + ex.Message + Environment.NewLine + ex.StackTrace);
            }
            return lblnResponse;
        }

        public bool ResetLoginAttempt(MemberRelation pobjMemberRelation, string pstrToken)
        {
            bool lblnResponse = false;
            try
            {
                APIResponseResults lobjAPIResponseResults = new APIResponseResults();
                lobjAPIResponseResults = JsonConvert.DeserializeObject<APIResponseResults>(WebAPIHelper.PostData(APIConstant.ResetLoginAttempt, "POST", "ResetLoginAttempt", JsonConvert.SerializeObject(pobjMemberRelation), pstrToken));
                if (lobjAPIResponseResults.results.IsSucessful)
                {
                    lblnResponse = JsonConvert.DeserializeObject<bool>(JsonConvert.SerializeObject(lobjAPIResponseResults.results.ReturnObject));
                }
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("APIClientHelper ResetLoginAttempt Exception: " + ex.Message + Environment.NewLine + ex.StackTrace);
            }
            return lblnResponse;
        }

        public bool ResetPassword(ResetPassword pobjResetPassword, string pstrToken)
        {
            bool lobjResponse = false;
            try
            {
                APIResponseResults lobjAPIResponseResults = new APIResponseResults();
                lobjAPIResponseResults = JsonConvert.DeserializeObject<APIResponseResults>(WebAPIHelper.PostData(APIConstant.ResetPassword, "POST", "ResetPassword", JsonConvert.SerializeObject(pobjResetPassword), pstrToken));
                if (lobjAPIResponseResults.results.IsSucessful)
                {
                    lobjResponse = JsonConvert.DeserializeObject<bool>(JsonConvert.SerializeObject(lobjAPIResponseResults.results.ReturnObject));
                }
                else
                {
                    lobjResponse = false;
                }
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("APIClientHelper ResetPassword Exception: " + ex.Message + Environment.NewLine + ex.StackTrace);
                lobjResponse = false;
            }
            return lobjResponse;
        }

        public ResetPassword GetDetailsFromHashKey(ResetPassword pobjResetPassword, string pstrToken)
        {
            ResetPassword lobjResponse;
            try
            {
                APIResponseResults lobjAPIResponseResults = new APIResponseResults();
                lobjAPIResponseResults = JsonConvert.DeserializeObject<APIResponseResults>(WebAPIHelper.PostData(APIConstant.GetDetailsFromHashKey, "POST", "GetDetailsFromHashKey", JsonConvert.SerializeObject(pobjResetPassword), pstrToken));
                if (lobjAPIResponseResults.results.IsSucessful)
                {
                    lobjResponse = JsonConvert.DeserializeObject<ResetPassword>(JsonConvert.SerializeObject(lobjAPIResponseResults.results.ReturnObject));
                }
                else
                {
                    lobjResponse = null;
                }
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("APIClientHelper GetDetailsFromHashKey Exception: " + ex.Message + Environment.NewLine + ex.StackTrace);
                lobjResponse = null;
            }
            return lobjResponse;
        }
        public bool CreateProfile(string pstrInsertMemberRequest, string pstrToken)
        {
            bool isSaved = false;
            try
            {
                APIResponseResults lobjAPIResponseResults = new APIResponseResults();
                lobjAPIResponseResults = JsonConvert.DeserializeObject<APIResponseResults>(WebAPIHelper.PostData(string.Format("{0}", APIConstant.CreateProfile), "POST", "CreateProfile", pstrInsertMemberRequest, pstrToken));
                if (lobjAPIResponseResults.results.IsSucessful)
                {
                    isSaved = true;
                }
                else
                {
                    isSaved = false;
                }
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("APIClientHelper CreateProfile Exception: " + ex.Message + Environment.NewLine + ex.StackTrace);
                isSaved = false;
            }
            return isSaved;
        }

        public bool ValidateResetToken(ResetPassword pobjResetPassword, string pstrToken)
        {
            bool lobjResponse = false;
            try
            {
                APIResponseResults lobjAPIResponseResults = new APIResponseResults();
                lobjAPIResponseResults = JsonConvert.DeserializeObject<APIResponseResults>(WebAPIHelper.PostData(APIConstant.ValidateResetToken, "POST", "ValidateResetToken", JsonConvert.SerializeObject(pobjResetPassword), pstrToken));
                if (lobjAPIResponseResults.results.IsSucessful)
                {
                    lobjResponse = JsonConvert.DeserializeObject<bool>(JsonConvert.SerializeObject(lobjAPIResponseResults.results.ReturnObject));
                }
                else
                {
                    lobjResponse = false;
                }
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("APIClientHelper ValidateResetToken Exception: " + ex.Message + Environment.NewLine + ex.StackTrace);
                lobjResponse = false;
            }
            return lobjResponse;
        }

        public bool UpdatePassword(ResetPassword pobjResetPassword, MemberRelation pobjMemberRelation, string pstrToken)
        {
            bool lobjResponse = false;
            try
            {
                APIResponseResults lobjAPIResponseResults = new APIResponseResults();

                lobjAPIResponseResults = JsonConvert.DeserializeObject<APIResponseResults>(WebAPIHelper.PostData(APIConstant.UpdatePassword, "POST", "UpdatePassword", JsonConvert.SerializeObject("{\"ResetPassword\":" + JsonConvert.SerializeObject(pobjResetPassword) + ",\"MemberRelation\":" + JsonConvert.SerializeObject(pobjMemberRelation) + "}"), pstrToken));
                if (lobjAPIResponseResults.results.IsSucessful)
                {
                    lobjResponse = JsonConvert.DeserializeObject<bool>(JsonConvert.SerializeObject(lobjAPIResponseResults.results.ReturnObject));
                }
                else
                {
                    lobjResponse = false;
                }
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("APIClientHelper ValidateResetToken Exception: " + ex.Message + Environment.NewLine + ex.StackTrace);
                lobjResponse = false;
            }
            return lobjResponse;
        }

        public bool InsertMemberSecurityDetails(List<SecurityDetails> lobjListSecurityDetails, string pstrToken)
        {
            bool lobjResponse = false;
            try
            {
                APIResponseResults lobjAPIResponseResults = new APIResponseResults();

                lobjAPIResponseResults = JsonConvert.DeserializeObject<APIResponseResults>(WebAPIHelper.PostData(APIConstant.InsertMemberSecurityDetails, "POST", "InsertMemberSecurityDetails", JsonConvert.SerializeObject(lobjListSecurityDetails), pstrToken));
                if (lobjAPIResponseResults.results.IsSucessful)
                {
                    lobjResponse = JsonConvert.DeserializeObject<bool>(JsonConvert.SerializeObject(lobjAPIResponseResults.results.ReturnObject));
                }
                else
                {
                    lobjResponse = false;
                }
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("APIClientHelper InsertMemberSecurityDetails Exception: " + ex.Message + Environment.NewLine + ex.StackTrace);
                lobjResponse = false;
            }
            return lobjResponse;
        }

        public List<SecurityDetails> GetMemberSecurityDetails(SecurityDetails pobjSecurityDetails, string pstrToken)
        {
            List<SecurityDetails> lobjResponse = null;
            try
            {
                APIResponseResults lobjAPIResponseResults = new APIResponseResults();
                lobjAPIResponseResults = JsonConvert.DeserializeObject<APIResponseResults>(WebAPIHelper.PostData(APIConstant.GetMemberSecurityDetails, "POST", "GetMemberSecurityDetails", JsonConvert.SerializeObject(pobjSecurityDetails), pstrToken));
                if (lobjAPIResponseResults.results.IsSucessful)
                {
                    lobjResponse = JsonConvert.DeserializeObject<List<SecurityDetails>>(JsonConvert.SerializeObject(lobjAPIResponseResults.results.ReturnObject));
                }
                else
                {
                    lobjResponse = null;
                }
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("APIClientHelper GetMemberSecurityDetails Exception: " + ex.Message + Environment.NewLine + ex.StackTrace);
                lobjResponse = null;
            }
            return lobjResponse;
        }

        public OTPDetails GenerateOTPDetails(OTPDetails pobjOTPDetails, string pstrToken)
        {
            OTPDetails lobjResponse;
            try
            {
                APIResponseResults lobjAPIResponseResults = new APIResponseResults();
                lobjAPIResponseResults = JsonConvert.DeserializeObject<APIResponseResults>(WebAPIHelper.PostData(APIConstant.GenerateOTPDetails, "POST", "GenerateOTPDetails", JsonConvert.SerializeObject(pobjOTPDetails), pstrToken));
                if (lobjAPIResponseResults.results.IsSucessful)
                {
                    lobjResponse = JsonConvert.DeserializeObject<OTPDetails>(JsonConvert.SerializeObject(lobjAPIResponseResults.results.ReturnObject));
                }
                else
                {
                    lobjResponse = null;
                }
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("APIClientHelper GenerateOTPDetails Exception: " + ex.Message + Environment.NewLine + ex.StackTrace);
                lobjResponse = null;
            }
            return lobjResponse;
        }

        public bool CheckWhetherOTPExists(OTPDetails pobjOTPDetails, string pstrToken)
        {
            bool lobjResponse = false;
            try
            {
                APIResponseResults lobjAPIResponseResults = new APIResponseResults();

                lobjAPIResponseResults = JsonConvert.DeserializeObject<APIResponseResults>(WebAPIHelper.PostData(APIConstant.CheckWhetherOTPExists, "POST", "CheckWhetherOTPExists", JsonConvert.SerializeObject(pobjOTPDetails), pstrToken));
                if (lobjAPIResponseResults.results.IsSucessful)
                {
                    lobjResponse = JsonConvert.DeserializeObject<bool>(JsonConvert.SerializeObject(lobjAPIResponseResults.results.ReturnObject));
                }
                else
                {
                    lobjResponse = false;
                }
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("APIClientHelper CheckWhetherOTPExists Exception: " + ex.Message + Environment.NewLine + ex.StackTrace);
                lobjResponse = false;
            }
            return lobjResponse;
        }

        public bool CheckRedemptionOTP(OTPDetails pobjOTPDetails, string pstrToken)
        {
            bool lobjResponse = false;
            try
            {
                APIResponseResults lobjAPIResponseResults = new APIResponseResults();

                lobjAPIResponseResults = JsonConvert.DeserializeObject<APIResponseResults>(WebAPIHelper.PostData(APIConstant.CheckRedemptionOTP, "POST", "CheckRedemptionOTP", JsonConvert.SerializeObject(pobjOTPDetails), pstrToken));
                if (lobjAPIResponseResults.results.IsSucessful)
                {
                    lobjResponse = JsonConvert.DeserializeObject<bool>(JsonConvert.SerializeObject(lobjAPIResponseResults.results.ReturnObject));
                }
                else
                {
                    lobjResponse = false;
                }
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("APIClientHelper CheckWhetherOTPExists Exception: " + ex.Message + Environment.NewLine + ex.StackTrace);
                lobjResponse = false;
            }
            return lobjResponse;
        }
        public bool InsertRedemptionAuditTrail(AuditTrailForRedemption pobjAuditTrailForRedemption, string pstrToken)
        {
            bool lobjResponse;
            try
            {
                APIResponseResults lobjAPIResponseResults = new APIResponseResults();
                lobjAPIResponseResults = JsonConvert.DeserializeObject<APIResponseResults>(WebAPIHelper.PostData(APIConstant.InsertRedemptionAuditTrail, "POST", "InsertRedemptionAuditTrail", JsonConvert.SerializeObject(pobjAuditTrailForRedemption), pstrToken));
                if (lobjAPIResponseResults.results.IsSucessful)
                {
                    lobjResponse = JsonConvert.DeserializeObject<bool>(JsonConvert.SerializeObject(lobjAPIResponseResults.results.ReturnObject));
                }
                else
                {
                    lobjResponse = false;
                }
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("APIClientHelper InsertRedemptionAuditTrail Exception: " + ex.Message + Environment.NewLine + ex.StackTrace);
                lobjResponse = false;
            }
            return lobjResponse;
        }
        #endregion

        #region Activity

        public int InsertMemberActivityWithSessionID(MemberActivitySession lobjMemberActivitySession, string pstrToken)
        {
            int lintResponse = 0;
            try
            {
                APIResponseResults lobjAPIResponseResults = new APIResponseResults();
                lobjAPIResponseResults = JsonConvert.DeserializeObject<APIResponseResults>(WebAPIHelper.PostData(APIConstant.InsertMemberActivityWithSessionID, "POST", "InsertMemberActivityWithSessionID", JsonConvert.SerializeObject(lobjMemberActivitySession), pstrToken));
                if (lobjAPIResponseResults.results.IsSucessful)
                {
                    lintResponse = JsonConvert.DeserializeObject<int>(JsonConvert.SerializeObject(lobjAPIResponseResults.results.ReturnObject));
                }
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("InsertMemberActivityWithSessionID Exception: " + ex.Message + Environment.NewLine + ex.StackTrace);
            }
            return lintResponse;
        }

        public int InsertMemberActivity(MemberActivityRequest pobjMemberActivityRequest, string pstrToken)
        {
            int lintResponse = 0;
            try
            {
                APIResponseResults lobjAPIResponseResults = new APIResponseResults();
                lobjAPIResponseResults = JsonConvert.DeserializeObject<APIResponseResults>(WebAPIHelper.PostData(APIConstant.InsertMemberActivity, "POST", "InsertMemberActivity", JsonConvert.SerializeObject(pobjMemberActivityRequest), pstrToken));
                if (lobjAPIResponseResults.results.IsSucessful)
                {
                    lintResponse = JsonConvert.DeserializeObject<int>(JsonConvert.SerializeObject(lobjAPIResponseResults.results.ReturnObject));
                }
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("InsertMemberActivity Exception: " + ex.Message + Environment.NewLine + ex.StackTrace);
            }
            return lintResponse;
        }

        public bool UpdateMemberShipActivitySession(MemberActivitySession pobjMemberActivitySession, string pstrToken)
        {
            bool Response = false;
            try
            {
                APIResponseResults lobjAPIResponseResults = new APIResponseResults();
                lobjAPIResponseResults = JsonConvert.DeserializeObject<APIResponseResults>(WebAPIHelper.PostData(APIConstant.UpdateMemberShipActivitySession, "POST", "UpdateMemberShipActivitySession", JsonConvert.SerializeObject(pobjMemberActivitySession), pstrToken));
                if (lobjAPIResponseResults.results.IsSucessful)
                {
                    Response = JsonConvert.DeserializeObject<bool>(JsonConvert.SerializeObject(lobjAPIResponseResults.results.ReturnObject));
                }
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("UpdateMemberShipActivitySession Exception: " + ex.Message + Environment.NewLine + ex.StackTrace);
            }
            return Response;
        }

        #endregion

        #region Program API

        public List<ProgramDefinition> GetProgramDefinitionList(string lstrToken)
        {
            List<ProgramDefinition> lobjResponse = null;
            try
            {
                APIResponseResults lobjAPIResponseResults = new APIResponseResults();
                lobjAPIResponseResults = JsonConvert.DeserializeObject<APIResponseResults>(WebAPIHelper.PostData(APIConstant.GetProgramDefinitionList, "GET", "GetProgramDefinitionList", string.Empty, lstrToken));
                if (lobjAPIResponseResults.results.IsSucessful)
                {
                    lobjResponse = JsonConvert.DeserializeObject<List<ProgramDefinition>>(JsonConvert.SerializeObject(lobjAPIResponseResults.results.ReturnObject));
                }
                else
                {
                    lobjResponse = null;
                }
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("APIClientHelper GetProgramDefinition Exception: " + ex.Message + Environment.NewLine + ex.StackTrace);
                lobjResponse = null;
            }
            return lobjResponse;
        }

        public ProgramDefinition GetProgramDefinition(string pstrProgramName, string pstrToken)
        {
            ProgramDefinition lobjResponse = null;
            try
            {
                APIResponseResults lobjAPIResponseResults = new APIResponseResults();
                lobjAPIResponseResults = JsonConvert.DeserializeObject<APIResponseResults>(WebAPIHelper.PostData(APIConstant.GetProgramDefinition + "?pstrProgramName=" + pstrProgramName, "GET", "GetProgramDefinition", JsonConvert.SerializeObject(pstrProgramName), pstrToken));
                if (lobjAPIResponseResults.results.IsSucessful)
                {
                    lobjResponse = JsonConvert.DeserializeObject<ProgramDefinition>(JsonConvert.SerializeObject(lobjAPIResponseResults.results.ReturnObject));
                }
                else
                {
                    lobjResponse = null;
                }
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("APIClientHelper GetProgramDefinition Exception: " + ex.Message + Environment.NewLine + ex.StackTrace);
                lobjResponse = null;
            }
            return lobjResponse;
        }

        public ProgramAttribute GetProgramAttributes(int pintProgramId, string pstrToken)
        {
            ProgramAttribute lobjResponse = null;
            try
            {
                APIResponseResults lobjAPIResponseResults = new APIResponseResults();
                lobjAPIResponseResults = JsonConvert.DeserializeObject<APIResponseResults>(WebAPIHelper.PostData(APIConstant.GetProgramAttributes + "?pintProgramId=" + pintProgramId, "GET", "GetProgramAttributes", JsonConvert.SerializeObject(pintProgramId)));
                if (lobjAPIResponseResults.results.IsSucessful)
                {
                    lobjResponse = JsonConvert.DeserializeObject<ProgramAttribute>(JsonConvert.SerializeObject(lobjAPIResponseResults.results.ReturnObject));
                }
                else
                {
                    lobjResponse = null;
                }
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("APIClientHelper GetProgramAttributes Exception: " + ex.Message + Environment.NewLine + ex.StackTrace);
                lobjResponse = null;
            }
            return lobjResponse;
        }

        public List<ProgramProductCode> GetProgramProductCodeList(int pintProgramId, string pstrToken)
        {
            List<ProgramProductCode> lobjResponse = null;
            try
            {
                APIResponseResults lobjAPIResponseResults = new APIResponseResults();
                lobjAPIResponseResults = JsonConvert.DeserializeObject<APIResponseResults>(WebAPIHelper.PostData(APIConstant.GetProgramProductCodeList + "?pintProgramId=" + pintProgramId, "GET", "GetProgramProductCodelst", JsonConvert.SerializeObject(pintProgramId)));
                if (lobjAPIResponseResults.results.IsSucessful)
                {
                    lobjResponse = JsonConvert.DeserializeObject<List<ProgramProductCode>>(JsonConvert.SerializeObject(lobjAPIResponseResults.results.ReturnObject));
                }
                else
                {
                    lobjResponse = null;
                }
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("APIClientHelper GetProgramProductCodeList Exception: " + ex.Message + Environment.NewLine + ex.StackTrace);
                lobjResponse = null;
            }
            return lobjResponse;
        }

        public List<ProgramCurrencyDefinition> GetProgramCurrencyDefinitionList(int pintProductId, string pstrToken)
        {
            List<ProgramCurrencyDefinition> lobjResponse = null;
            try
            {
                APIResponseResults lobjAPIResponseResults = new APIResponseResults();
                lobjAPIResponseResults = JsonConvert.DeserializeObject<APIResponseResults>(WebAPIHelper.PostData(APIConstant.GetProgramCurrencyDefinitionList + "?pintProgramId=" + pintProductId, "GET", "GetProgramCurrencyDefinitionList", JsonConvert.SerializeObject(pintProductId), pstrToken));
                if (lobjAPIResponseResults.results.IsSucessful)
                {
                    lobjResponse = JsonConvert.DeserializeObject<List<ProgramCurrencyDefinition>>(JsonConvert.SerializeObject(lobjAPIResponseResults.results.ReturnObject));
                }
                else
                {
                    lobjResponse = null;
                }
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("APIClientHelper GetProgramProductCodeList Exception: " + ex.Message + Environment.NewLine + ex.StackTrace);
                lobjResponse = null;
            }
            return lobjResponse;
        }

        public List<CustomerSegment> GetAllCustomerSegmentlst(int pintProgramId, string pstrToken)
        {
            List<CustomerSegment> lobjResponse = null;
            try
            {
                APIResponseResults lobjAPIResponseResults = new APIResponseResults();
                lobjAPIResponseResults = JsonConvert.DeserializeObject<APIResponseResults>(WebAPIHelper.PostData(APIConstant.GetAllCustomerSegmentlst + "?pintProgramId=" + pintProgramId, "GET", "GetAllCustomerSegmentlst", JsonConvert.SerializeObject(pintProgramId)));
                if (lobjAPIResponseResults.results.IsSucessful)
                {
                    lobjResponse = JsonConvert.DeserializeObject<List<CustomerSegment>>(JsonConvert.SerializeObject(lobjAPIResponseResults.results.ReturnObject));
                }
                else
                {
                    lobjResponse = null;
                }
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("APIClientHelper GetAllCustomerSegmentlst Exception: " + ex.Message + Environment.NewLine + ex.StackTrace);
                lobjResponse = null;
            }
            return lobjResponse;
        }

        public List<RedemptionKeys> GetRedemptionKeyslst(int pintProgramId, string pstrToken)
        {
            List<RedemptionKeys> lobjResponse = null;
            try
            {
                APIResponseResults lobjAPIResponseResults = new APIResponseResults();
                lobjAPIResponseResults = JsonConvert.DeserializeObject<APIResponseResults>(WebAPIHelper.PostData(APIConstant.GetRedemptionKeyslst + "?pintProgramId=" + pintProgramId, "GET", "GetRedemptionKeyslst", JsonConvert.SerializeObject(pintProgramId), pstrToken));
                if (lobjAPIResponseResults.results.IsSucessful)
                {
                    lobjResponse = JsonConvert.DeserializeObject<List<RedemptionKeys>>(JsonConvert.SerializeObject(lobjAPIResponseResults.results.ReturnObject));
                }
                else
                {
                    lobjResponse = null;
                }
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("APIClientHelper GetRedemptionKeyslst Exception: " + ex.Message + Environment.NewLine + ex.StackTrace);
                lobjResponse = null;
            }
            return lobjResponse;
        }

        public SystemParameter GetSystemParameter(int pintProgramId, string pstrToken)
        {
            SystemParameter lobjResponse;
            try
            {
                APIResponseResults lobjAPIResponseResults = new APIResponseResults();
                lobjAPIResponseResults = JsonConvert.DeserializeObject<APIResponseResults>(WebAPIHelper.PostData(APIConstant.GetSystemParameter + "?pintProgramId=" + pintProgramId, "GET", "GetProgramDefinition", string.Empty, pstrToken));
                if (lobjAPIResponseResults.results.IsSucessful)
                {
                    lobjResponse = JsonConvert.DeserializeObject<SystemParameter>(JsonConvert.SerializeObject(lobjAPIResponseResults.results.ReturnObject));
                }
                else
                {
                    lobjResponse = null;
                }
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("APIClientHelper GetSystemParameter Exception: " + ex.Message + Environment.NewLine + ex.StackTrace);
                lobjResponse = null;
            }
            return lobjResponse;
        }

        public float GetProgramRedemptionRate(ProgramRedemptionRate lobjProgramRedemptionRate, string pstrToken)
        {
            float lobjResponse = 0.0F;
            try
            {
                APIResponseResults lobjAPIResponseResults = new APIResponseResults();
                lobjAPIResponseResults = JsonConvert.DeserializeObject<APIResponseResults>(WebAPIHelper.PostData(APIConstant.GetProgramRedemptionRate, "POST", "GetProgramRedemptionRate", JsonConvert.SerializeObject(lobjProgramRedemptionRate), pstrToken));
                if (lobjAPIResponseResults.results.IsSucessful)
                {
                    lobjResponse = JsonConvert.DeserializeObject<float>(JsonConvert.SerializeObject(lobjAPIResponseResults.results.ReturnObject));
                }
                else
                {
                    lobjResponse = 0.0F;
                }
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("APIClientHelper GetProgramRedemptionRate Exception: " + ex.Message + Environment.NewLine + ex.StackTrace);
                lobjResponse = 0.0F;
            }
            return lobjResponse;
        }

        public List<ProductDefinition> GetProductDefinitionlst(int pintProgramId, string pstrToken)
        {
            List<ProductDefinition> lobjResponse = null;
            try
            {
                APIResponseResults lobjAPIResponseResults = new APIResponseResults();
                lobjAPIResponseResults = JsonConvert.DeserializeObject<APIResponseResults>(WebAPIHelper.PostData(APIConstant.GetProductDefinitionlst + "?pintProgramId=" + pintProgramId, "GET", "GetProductDefinitionlst", JsonConvert.SerializeObject(pintProgramId)));
                if (lobjAPIResponseResults.results.IsSucessful)
                {
                    lobjResponse = JsonConvert.DeserializeObject<List<ProductDefinition>>(JsonConvert.SerializeObject(lobjAPIResponseResults.results.ReturnObject));
                }
                else
                {
                    lobjResponse = null;
                }
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("APIClientHelper GetProductDefinitionlst Exception: " + ex.Message + Environment.NewLine + ex.StackTrace);
                lobjResponse = null;
            }
            return lobjResponse;
        }

        public List<ProgramTransactionCode> GetProgramTransactionCodelst(int pintProgramId, string pstrToken)
        {
            List<ProgramTransactionCode> lobjResponse = null;
            try
            {
                APIResponseResults lobjAPIResponseResults = new APIResponseResults();
                lobjAPIResponseResults = JsonConvert.DeserializeObject<APIResponseResults>(WebAPIHelper.PostData(APIConstant.GetProgramTransactionCodelst + "?pintProgramId=" + pintProgramId, "GET", "GetProgramTransactionCodelst", JsonConvert.SerializeObject(pintProgramId)));
                if (lobjAPIResponseResults.results.IsSucessful)
                {
                    lobjResponse = JsonConvert.DeserializeObject<List<ProgramTransactionCode>>(JsonConvert.SerializeObject(lobjAPIResponseResults.results.ReturnObject));
                }
                else
                {
                    lobjResponse = null;
                }
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("APIClientHelper GetProgramTransactionCodelst Exception: " + ex.Message + Environment.NewLine + ex.StackTrace);
                lobjResponse = null;
            }
            return lobjResponse;
        }

        public List<ProductTransactionCode> GetProductTransactionCodelst(int pintProgramId, string pstrToken)
        {
            List<ProductTransactionCode> lobjResponse = null;
            try
            {
                APIResponseResults lobjAPIResponseResults = new APIResponseResults();
                lobjAPIResponseResults = JsonConvert.DeserializeObject<APIResponseResults>(WebAPIHelper.PostData(APIConstant.GetProductTransactionCodelst + "?pintProgramId=" + pintProgramId, "GET", "GetProductTransactionCodelst", JsonConvert.SerializeObject(pintProgramId)));
                if (lobjAPIResponseResults.results.IsSucessful)
                {
                    lobjResponse = JsonConvert.DeserializeObject<List<ProductTransactionCode>>(JsonConvert.SerializeObject(lobjAPIResponseResults.results.ReturnObject));
                }
                else
                {
                    lobjResponse = null;
                }
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("APIClientHelper GetProductTransactionCodelst Exception: " + ex.Message + Environment.NewLine + ex.StackTrace);
                lobjResponse = null;
            }
            return lobjResponse;
        }

        #endregion

        #region Transaction API

        public TxnSummaryDetails GetMemberStatementSummary(SearchTransactions pobjSearchTransactions, string pstrToken)
        {
            TxnSummaryDetails lobjResponse = null;
            try
            {
                APIResponseResults lobjAPIResponseResults = new APIResponseResults();
                lobjAPIResponseResults = JsonConvert.DeserializeObject<APIResponseResults>(WebAPIHelper.PostData(APIConstant.GetMemberStatementSummary, "POST", "GetMemberStatementSummary", JsonConvert.SerializeObject(pobjSearchTransactions), pstrToken));
                if (lobjAPIResponseResults.results.IsSucessful)
                {
                    lobjResponse = JsonConvert.DeserializeObject<TxnSummaryDetails>(JsonConvert.SerializeObject(lobjAPIResponseResults.results.ReturnObject));
                }
                else
                {
                    lobjResponse = null;
                }
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("APIClientHelper GetMemberStatementSummary Exception: " + ex.Message + Environment.NewLine + ex.StackTrace);
                lobjResponse = null;
            }
            return lobjResponse;
        }

        public ScheduleExpiry GetExpirySchedule(ScheduleExpiry pobjScheduleExpiry, string pstrToken)
        {
            ScheduleExpiry lobjResponse = null;
            try
            {
                APIResponseResults lobjAPIResponseResults = new APIResponseResults();
                lobjAPIResponseResults = JsonConvert.DeserializeObject<APIResponseResults>(WebAPIHelper.PostData(APIConstant.GetExpirySchedule, "POST", "GetExpirySchedule", JsonConvert.SerializeObject(pobjScheduleExpiry), pstrToken));
                if (lobjAPIResponseResults.results.IsSucessful)
                {
                    lobjResponse = JsonConvert.DeserializeObject<ScheduleExpiry>(JsonConvert.SerializeObject(lobjAPIResponseResults.results.ReturnObject));
                }
                else
                {
                    lobjResponse = null;
                }
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("APIClientHelper GetExpirySchedule Exception: " + ex.Message + Environment.NewLine + ex.StackTrace);
                lobjResponse = null;
            }
            return lobjResponse;
        }

        public ScheduleExpiry GetTransactionExpirySchedule(ScheduleExpiry lobjScheduleExpiry, string pstrToken)
        {
            ScheduleExpiry lobjResponse = null;
            try
            {
                APIResponseResults lobjAPIResponseResults = new APIResponseResults();
                lobjAPIResponseResults = JsonConvert.DeserializeObject<APIResponseResults>(WebAPIHelper.PostData(APIConstant.GetTransactionExpirySchedule, "POST", "GetTransactionExpirySchedule", JsonConvert.SerializeObject(lobjScheduleExpiry), pstrToken));
                if (lobjAPIResponseResults.results.IsSucessful)
                {
                    lobjResponse = JsonConvert.DeserializeObject<ScheduleExpiry>(JsonConvert.SerializeObject(lobjAPIResponseResults.results.ReturnObject));
                }
                else
                {
                    lobjResponse = null;
                }
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("APIClientHelper GetTransactionExpirySchedule Exception: " + ex.Message + Environment.NewLine + ex.StackTrace);
                lobjResponse = null;
            }
            return lobjResponse;
        }

        public ScheduleExpiry GetNextExpiredPointsOnDate(ScheduleExpiry lobjScheduleExpiry, string pstrToken)
        {
            ScheduleExpiry lobjResponse = null;
            try
            {
                APIResponseResults lobjAPIResponseResults = new APIResponseResults();
                lobjAPIResponseResults = JsonConvert.DeserializeObject<APIResponseResults>(WebAPIHelper.PostData(APIConstant.GetNextExpiredPointsOnDate, "POST", "GetNextExpiredPointsOnDate", JsonConvert.SerializeObject(lobjScheduleExpiry), pstrToken));
                if (lobjAPIResponseResults.results.IsSucessful)
                {
                    lobjResponse = JsonConvert.DeserializeObject<ScheduleExpiry>(JsonConvert.SerializeObject(lobjAPIResponseResults.results.ReturnObject));
                }
                else
                {
                    lobjResponse = null;
                }
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("APIClientHelper GetNextExpiredPointsOnDate Exception: " + ex.Message + Environment.NewLine + ex.StackTrace);
                lobjResponse = null;
            }
            return lobjResponse;
        }

        public ScheduleExpiry GetNextYearExpirySchedule(ScheduleExpiry lobjScheduleExpiry, string pstrToken)
        {
            ScheduleExpiry lobjResponse = null;
            try
            {
                APIResponseResults lobjAPIResponseResults = new APIResponseResults();
                lobjAPIResponseResults = JsonConvert.DeserializeObject<APIResponseResults>(WebAPIHelper.PostData(APIConstant.GetNextYearExpirySchedule, "POST", "GetNextYearExpirySchedule", JsonConvert.SerializeObject(lobjScheduleExpiry), pstrToken));
                if (lobjAPIResponseResults.results.IsSucessful)
                {
                    lobjResponse = JsonConvert.DeserializeObject<ScheduleExpiry>(JsonConvert.SerializeObject(lobjAPIResponseResults.results.ReturnObject));
                }
                else
                {
                    lobjResponse = null;
                }
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("APIClientHelper GetNextYearExpirySchedule Exception: " + ex.Message + Environment.NewLine + ex.StackTrace);
                lobjResponse = null;
            }
            return lobjResponse;
        }

        public int GetTotalMemberTransaction(SearchTransactions pobjSearchTransactions, string pstrToken)
        {
            int lobjResponse = 0;
            try
            {
                APIResponseResults lobjAPIResponseResults = new APIResponseResults();
                lobjAPIResponseResults = JsonConvert.DeserializeObject<APIResponseResults>(WebAPIHelper.PostData(APIConstant.GetTotalMemberTransaction, "POST", "GetTotalMemberTransaction", JsonConvert.SerializeObject(pobjSearchTransactions), pstrToken));
                if (lobjAPIResponseResults.results.IsSucessful)
                {
                    lobjResponse = JsonConvert.DeserializeObject<int>(JsonConvert.SerializeObject(lobjAPIResponseResults.results.ReturnObject));
                }
                else
                {
                    lobjResponse = 0;
                }
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("APIClientHelper GetTotalMemberTransaction Exception: " + ex.Message + Environment.NewLine + ex.StackTrace);
                lobjResponse = 0;
            }
            return lobjResponse;
        }

        public int GetTotalMemberTransactionByDate(SearchTransactions pobjSearchTransactions, string pstrToken)
        {
            int lobjResponse = 0;
            try
            {
                APIResponseResults lobjAPIResponseResults = new APIResponseResults();
                lobjAPIResponseResults = JsonConvert.DeserializeObject<APIResponseResults>(WebAPIHelper.PostData(APIConstant.GetTotalMemberTransactionByDate, "POST", "GetTotalMemberTransactionByDate", JsonConvert.SerializeObject(pobjSearchTransactions), pstrToken));
                if (lobjAPIResponseResults.results.IsSucessful)
                {
                    lobjResponse = JsonConvert.DeserializeObject<int>(JsonConvert.SerializeObject(lobjAPIResponseResults.results.ReturnObject));
                }
                else
                {
                    lobjResponse = 0;
                }
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("APIClientHelper GetTotalMemberTransactionByDate Exception: " + ex.Message + Environment.NewLine + ex.StackTrace);
                lobjResponse = 0;
            }
            return lobjResponse;
        }

        public List<TransactionDetails> GetMemberTransactionSummary(SearchTransactions pobjSearchTransactions, string pstrToken)
        {
            List<TransactionDetails> lobjResponse = null;
            try
            {
                APIResponseResults lobjAPIResponseResults = new APIResponseResults();
                lobjAPIResponseResults = JsonConvert.DeserializeObject<APIResponseResults>(WebAPIHelper.PostData(APIConstant.GetMemberTransactionSummary, "POST", "GetMemberTransactionSummary", JsonConvert.SerializeObject(pobjSearchTransactions), pstrToken));
                if (lobjAPIResponseResults.results.IsSucessful)
                {
                    lobjResponse = JsonConvert.DeserializeObject<List<TransactionDetails>>(JsonConvert.SerializeObject(lobjAPIResponseResults.results.ReturnObject));
                }
                else
                {
                    lobjResponse = null;
                }
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("APIClientHelper GetMemberTransactionSummary Exception: " + ex.Message + Environment.NewLine + ex.StackTrace);
                lobjResponse = null;
            }
            return lobjResponse;
        }

        public List<TransactionAdditionalInfo> GetTransactionAdditionalInfo(int pintProgramId, string pstrTransactionId, string pstrToken)
        {
            List<TransactionAdditionalInfo> lobjResponse = null;
            try
            {
                APIResponseResults lobjAPIResponseResults = new APIResponseResults();
                lobjAPIResponseResults = JsonConvert.DeserializeObject<APIResponseResults>(WebAPIHelper.PostData(APIConstant.GetTransactionAdditionalInfo + "?pstrTransactionId=" + pstrTransactionId + "&pintProgramId=" + pintProgramId, "GET", "GetTransactionAdditionalInfo", "", pstrToken));
                if (lobjAPIResponseResults.results.IsSucessful)
                {
                    lobjResponse = JsonConvert.DeserializeObject<List<TransactionAdditionalInfo>>(JsonConvert.SerializeObject(lobjAPIResponseResults.results.ReturnObject));
                }
                else
                {
                    lobjResponse = null;
                }
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("APIClientHelper GetMemberTransactionSummary Exception: " + ex.Message + Environment.NewLine + ex.StackTrace);
                lobjResponse = null;
            }
            return lobjResponse;
        }
        public List<TransactionDetails> GetMemberTransactionSummaryByDate(SearchTransactions pobjSearchTransactions, string pstrToken)
        {
            List<TransactionDetails> lobjResponse = null;
            try
            {
                APIResponseResults lobjAPIResponseResults = new APIResponseResults();
                lobjAPIResponseResults = JsonConvert.DeserializeObject<APIResponseResults>(WebAPIHelper.PostData(APIConstant.GetMemberTransactionSummaryByDate, "POST", "GetMemberTransactionSummaryByDate", JsonConvert.SerializeObject(pobjSearchTransactions), pstrToken));
                if (lobjAPIResponseResults.results.IsSucessful)
                {
                    lobjResponse = JsonConvert.DeserializeObject<List<TransactionDetails>>(JsonConvert.SerializeObject(lobjAPIResponseResults.results.ReturnObject));
                }
                else
                {
                    lobjResponse = null;
                }
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("APIClientHelper GetMemberTransactionSummaryByDate Exception: " + ex.Message + Environment.NewLine + ex.StackTrace);
                lobjResponse = null;
            }
            return lobjResponse;
        }

        #endregion

        #region PG API

        public int CheckAvailability(PGAvailabilityRequest lobjPGRequest, string pstrToken)
        {
            int lobjResponse = 0;
            try
            {
                APIResponseResults lobjAPIResponseResults = new APIResponseResults();
                lobjAPIResponseResults = JsonConvert.DeserializeObject<APIResponseResults>(WebAPIHelper.PostData(APIConstant.CheckAvailability, "POST", "CheckAvailability", JsonConvert.SerializeObject(lobjPGRequest), pstrToken));
                if (lobjAPIResponseResults.results.IsSucessful)
                {
                    lobjResponse = JsonConvert.DeserializeObject<int>(JsonConvert.SerializeObject(lobjAPIResponseResults.results.ReturnObject));
                }
                else
                {
                    lobjResponse = 0;
                }
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("APIClientHelper CheckAvailability Exception: " + ex.Message + Environment.NewLine + ex.StackTrace);
                lobjResponse = 0;
            }
            return lobjResponse;
        }

        public bool ReversalPoints(PGReversalRequest lobjPGReversalRequest, string pstrToken)
        {
            bool lobjResponse = false;
            try
            {
                APIResponseResults lobjAPIResponseResults = new APIResponseResults();
                lobjAPIResponseResults = JsonConvert.DeserializeObject<APIResponseResults>(WebAPIHelper.PostData(APIConstant.ReversalPoints, "POST", "ReversalPoints", JsonConvert.SerializeObject(lobjPGReversalRequest), pstrToken));
                if (lobjAPIResponseResults.results.IsSucessful)
                {
                    lobjResponse = JsonConvert.DeserializeObject<bool>(JsonConvert.SerializeObject(lobjAPIResponseResults.results.ReturnObject));
                }
                else
                {
                    lobjResponse = false;
                }
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("APIClientHelper ReversalPoints Exception: " + ex.Message + Environment.NewLine + ex.StackTrace);
                lobjResponse = false;
            }
            return lobjResponse;
        }

        public string RedeemPoints(PGRedeemRequest lobjPGRedeemRequest, string pstrToken)
        {
            string lobjResponse = string.Empty;
            try
            {
                APIResponseResults lobjAPIResponseResults = new APIResponseResults();
                lobjAPIResponseResults = JsonConvert.DeserializeObject<APIResponseResults>(WebAPIHelper.PostData(APIConstant.RedeemPoints, "POST", "RedeemPoints", JsonConvert.SerializeObject(lobjPGRedeemRequest), pstrToken));
                if (lobjAPIResponseResults.results.IsSucessful)
                {
                    lobjResponse = JsonConvert.DeserializeObject<string>(JsonConvert.SerializeObject(lobjAPIResponseResults.results.ReturnObject));
                }
                else
                {
                    lobjResponse = string.Empty;
                }
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("APIClientHelper RedeemPoints Exception: " + ex.Message + Environment.NewLine + ex.StackTrace);
                lobjResponse = string.Empty;
            }
            return lobjResponse;
        }

        #endregion

        #region CE API

        public bool InsertEmailDetails(EmailDetails pobjEmailDetails, List<Attachments> plstAttachments, string pstrToken)
        {
            bool lobjResponse = false;
            try
            {
                APIResponseResults lobjAPIResponseResults = new APIResponseResults();
                lobjAPIResponseResults = JsonConvert.DeserializeObject<APIResponseResults>(WebAPIHelper.PostData(APIConstant.InsertEmailDetails, "POST", "InsertEmailDetails", JsonConvert.SerializeObject("{\"EmailDetails\":" + JsonConvert.SerializeObject(pobjEmailDetails) + ",\"Attachments\":" + JsonConvert.SerializeObject(plstAttachments) + "}"), pstrToken));
                if (lobjAPIResponseResults.results.IsSucessful)
                {
                    lobjResponse = JsonConvert.DeserializeObject<bool>(JsonConvert.SerializeObject(lobjAPIResponseResults.results.ReturnObject));
                }
                else
                {
                    lobjResponse = false;
                }
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("APIClientHelper InsertEmailDetails Exception: " + ex.Message + Environment.NewLine + ex.StackTrace);
                lobjResponse = false;
            }
            return lobjResponse;
        }

        public bool InsertSmsDetails(SmsDetails pobjSmsDetail, string pstrToken)
        {
            bool lobjResponse = false;
            try
            {
                APIResponseResults lobjAPIResponseResults = new APIResponseResults();
                lobjAPIResponseResults = JsonConvert.DeserializeObject<APIResponseResults>(WebAPIHelper.PostData(APIConstant.InsertSmsDetails, "POST", "InsertSmsDetails", JsonConvert.SerializeObject(pobjSmsDetail), pstrToken));
                if (lobjAPIResponseResults.results.IsSucessful)
                {
                    lobjResponse = JsonConvert.DeserializeObject<bool>(JsonConvert.SerializeObject(lobjAPIResponseResults.results.ReturnObject));
                }
                else
                {
                    lobjResponse = false;
                }
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("APIClientHelper InsertSmsDetails Exception: " + ex.Message + Environment.NewLine + ex.StackTrace);
                lobjResponse = false;
            }
            return lobjResponse;
        }

        #endregion

        
        #region IBE Core Integration for Domestic Flights
        public AirFieldsForDomestic GetAllAirFieldsForDomestic()
        {
            AirFieldsForDomestic lobjDomesticFlightsResponse = null;
            try
            {
                string UserName = ConfigurationManager.AppSettings["KhaltiIBEDomesticFlightUserName"].ToString();
                string Password = ConfigurationManager.AppSettings["KhaltiIBEDomesticFlightPassword"].ToString();
               // string Token = Convert.ToString(ConfigurationManager.AppSettings["KhaltiIBEDomesticFlightToken"]);
                string APIResponse = WebAPIHelper.PostDataKhalti(APIConstant.GetSectorRequest, "POST", "SectorRequest", string.Empty,UserName, Password);
                if (APIResponse.Length > 0)
                {
                    lobjDomesticFlightsResponse = JsonConvert.DeserializeObject<AirFieldsForDomestic>(APIResponse);
                }
                else
                {
                    lobjDomesticFlightsResponse = null;
                }
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("APIClientHelper GetRequiredDetails Exception: " + ex.Message + Environment.NewLine + ex.StackTrace);
                lobjDomesticFlightsResponse = null;
            }
            return lobjDomesticFlightsResponse;
        }

        public SearchResponseForDomestic MapSearchResponseForDomesticFlights(SearchRequestForDomestic pobjSearchRequest)
        {
            SearchResponseForDomestic lobjSearchResponse = new SearchResponseForDomestic();
            try
            {
                string UserName = ConfigurationManager.AppSettings["KhaltiIBEDomesticFlightUserName"].ToString();
                string Password = ConfigurationManager.AppSettings["KhaltiIBEDomesticFlightPassword"].ToString();
                //string Token = ConfigurationManager.AppSettings["KhaltiIBEDomesticFlightToken"].ToString();
                string APIResponse = WebAPIHelper.PostDataKhalti(APIConstant.SearchRequest, "POST", "SearchRequest", JsonConvert.SerializeObject(pobjSearchRequest), UserName, Password);
                if (APIResponse.Length > 0)
                {
                    lobjSearchResponse = JsonConvert.DeserializeObject<SearchResponseForDomestic>(APIResponse);
                }
                else
                {
                    lobjSearchResponse = null;
                }
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("APIClientHelper MapSearchResponseForDomesticFlights Exception: " + ex.Message + Environment.NewLine + ex.StackTrace);
                lobjSearchResponse = null;
            }
            return lobjSearchResponse;
        }

        public CreateDomesticItineraryResponse CreateItineraryForDomesticFlights(CreateDomesticItineraryRequest pobjSearchRequest)
        {
            CreateDomesticItineraryResponse lobjSearchResponse = new CreateDomesticItineraryResponse();
            try
            {
                string UserName = ConfigurationManager.AppSettings["KhaltiIBEDomesticFlightUserName"].ToString();
                string Password = ConfigurationManager.AppSettings["KhaltiIBEDomesticFlightPassword"].ToString();
                //string Token = ConfigurationManager.AppSettings["KhaltiIBEDomesticFlightToken"].ToString();
                string APIResponse = WebAPIHelper.PostDataKhalti(APIConstant.CreateItinerary, "POST", "CreateItinerary", JsonConvert.SerializeObject(pobjSearchRequest), UserName, Password);
                if (APIResponse.Length > 0)
                {
                    lobjSearchResponse = JsonConvert.DeserializeObject<CreateDomesticItineraryResponse>(APIResponse);
                }
                else
                {
                    lobjSearchResponse = null;
                }
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("APIClientHelper CreateItineraryForDomesticFlights Exception: " + ex.Message + Environment.NewLine + ex.StackTrace);
                lobjSearchResponse = null;
            }
            return lobjSearchResponse;
        }

        public CreateDomesticBookingResponse CreateBookingForDomesticFlights(BookingDetailsRequest pobjSearchRequest)
        {
            CreateDomesticBookingResponse lobjSearchResponse = new CreateDomesticBookingResponse();
            try
            {
                string UserName = ConfigurationManager.AppSettings["KhaltiIBEDomesticFlightUserName"].ToString();
                string Password = ConfigurationManager.AppSettings["KhaltiIBEDomesticFlightPassword"].ToString();
                //string Token = ConfigurationManager.AppSettings["KhaltiIBEDomesticFlightToken"].ToString();
                string APIResponse = WebAPIHelper.PostDataKhalti(APIConstant.CreateBooking, "POST", "CreateBooking", JsonConvert.SerializeObject(pobjSearchRequest), UserName, Password);
                if (APIResponse.Length > 0)
                {
                    lobjSearchResponse = JsonConvert.DeserializeObject<CreateDomesticBookingResponse>(APIResponse);
                }
                else
                {
                    lobjSearchResponse = null;
                }
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("APIClientHelper CreateBookingForDomesticFlights Exception: " + ex.Message + Environment.NewLine + ex.StackTrace);
                lobjSearchResponse = null;
            }
            return lobjSearchResponse;
        }

        public BookingStatusResponseForDomestic BookingStatusForDomesticFlights(BookingStatusRequestForDomestic pobjSearchRequest)
        {
            BookingStatusResponseForDomestic lobjSearchResponse = new BookingStatusResponseForDomestic();
            try
            {
                string UserName = ConfigurationManager.AppSettings["KhaltiIBEDomesticFlightUserName"].ToString();
                string Password = ConfigurationManager.AppSettings["KhaltiIBEDomesticFlightPassword"].ToString();
                //string Token = ConfigurationManager.AppSettings["KhaltiIBEDomesticFlightToken"].ToString();
                string APIResponse = WebAPIHelper.PostDataKhalti(APIConstant.BookingStatus, "POST", "BookingStatus", JsonConvert.SerializeObject(pobjSearchRequest), UserName, Password);
                if (APIResponse.Length > 0)
                {
                    lobjSearchResponse = JsonConvert.DeserializeObject<BookingStatusResponseForDomestic>(APIResponse);
                }
                else
                {
                    lobjSearchResponse = null;
                }
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("APIClientHelper BookingStatusForDomesticFlights Exception: " + ex.Message + Environment.NewLine + ex.StackTrace);
                lobjSearchResponse = null;
            }
            return lobjSearchResponse;
        }

        public TicketDownloadResponse TicketDownloadForDomesticFlights(TicketDownloadRequest pobjticketDownload)
        {
            TicketDownloadResponse lobjResponse = new TicketDownloadResponse();
            try
            {
                string UserName = ConfigurationManager.AppSettings["KhaltiIBEDomesticFlightUserName"].ToString();
                string Password = ConfigurationManager.AppSettings["KhaltiIBEDomesticFlightPassword"].ToString();
                //string Token = ConfigurationManager.AppSettings["KhaltiIBEDomesticFlightToken"].ToString();
                string APIResponse = WebAPIHelper.PostDataKhalti(APIConstant.TicketDownload, "POST", "TicketDownload", JsonConvert.SerializeObject(pobjticketDownload), UserName, Password);
                if (APIResponse.Contains("Response status code does not indicate success: 400 (Bad Request)."))
                {
                    lobjResponse = null;
                }
                else
                {
                    if (APIResponse.Length > 0)
                    {
                        lobjResponse = JsonConvert.DeserializeObject<TicketDownloadResponse>(APIResponse);
                    }
                    else
                    {
                        lobjResponse = null;
                    }
                }
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("APIClientHelper TicketDownloadForDomesticFlights Exception: " + ex.Message + Environment.NewLine + ex.StackTrace);
                lobjResponse = null;
            }
            return lobjResponse;
        }

        public List<DomesticItineraryDetails> GetDomesticFlightBookingDetails(string pstrMemberId)
        {
            List<DomesticItineraryDetails> lobjResponse = new List<DomesticItineraryDetails>();
            try
            {
                string UserName = ConfigurationManager.AppSettings["KhaltiIBEDomesticFlightUserName"].ToString();
                string Password = ConfigurationManager.AppSettings["KhaltiIBEDomesticFlightPassword"].ToString();
               // string Token = ConfigurationManager.AppSettings["KhaltiIBEDomesticFlightToken"].ToString();
                string APIResponse = WebAPIHelper.PostDataKhalti(APIConstant.GetBookingByMemberId + "?memberId=" + pstrMemberId, "POST", "GetBookingByMemberId", string.Empty, UserName, Password);
                if (APIResponse.Length > 0)
                {
                    lobjResponse = JsonConvert.DeserializeObject<List<DomesticItineraryDetails>>(APIResponse);
                }
                else
                {
                    lobjResponse = null;
                }
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("APIClientHelper GetDomesticFlightBookingDetails Exception: " + ex.Message + Environment.NewLine + ex.StackTrace);
                lobjResponse = null;
            }
            return lobjResponse;
        }
        #endregion

        

        #region ABC Email Configuration
        public ABCEmailDetailsResponse ABCEmailDetails(string parameters, string lstrToken)
        {
            ABCEmailDetailsResponse lobjEmailResponse = new ABCEmailDetailsResponse();
            try
            {
                // string json = JsonConvert.SerializeObject(pobjEmailDetails);
                string APIResponse = WebAPIHelper.PostData(APIConstant.SendCommunication, "POST", "SendCommunication", parameters, lstrToken);
                if (APIResponse.Length > 0)
                {
                    lobjEmailResponse = JsonConvert.DeserializeObject<ABCEmailDetailsResponse>(APIResponse);
                }
                else
                {
                    lobjEmailResponse = null;
                }
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("APIClientHelper ABCEmailDetails Exception: " + ex.Message + Environment.NewLine + ex.StackTrace);
                lobjEmailResponse = null;
            }
            return lobjEmailResponse;
        }
        #endregion
        #region Payment gateway
        public bool InsertManualTransactionDetails(TransactionDetails lobjtransactionRequest, string pstrToken)
        {
            bool lobjResponse = false;
            try
            {
                APIResponseResults lobjAPIResponseResults = new APIResponseResults();
                lobjAPIResponseResults = JsonConvert.DeserializeObject<APIResponseResults>(WebAPIHelper.PostData(APIConstant.InsertManualTransactionDetails, "POST", "InsertManualTransactionDetails", JsonConvert.SerializeObject(lobjtransactionRequest), pstrToken));
                if (lobjAPIResponseResults.results.IsSucessful)
                {
                    lobjResponse = JsonConvert.DeserializeObject<bool>(JsonConvert.SerializeObject(lobjAPIResponseResults.results.ReturnObject));
                }
                else
                {
                    lobjResponse = false;
                }
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("APIClientHelper InsertManualTransactionDetails Exception: " + ex.Message + Environment.NewLine + ex.StackTrace);
                lobjResponse = false;
            }
            return lobjResponse;
        }
        #endregion

        #region PG
        public PGTokenResponse GetAuthToken(PGTokenRequest authTokenRequest)
        {
            PGTokenResponse lobjResponse;
            try
            {
                APIResponseResults lobjAPIResponseResults = new APIResponseResults();
                lobjResponse = JsonConvert.DeserializeObject<PGTokenResponse>(WebAPIHelper.PostData(APIConstant.GetPGAuthToken, "POST", "login", JsonConvert.SerializeObject(authTokenRequest)));
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("APIClientHelper GetPGAuthToken Exception: " + ex.Message + Environment.NewLine + ex.StackTrace);
                lobjResponse = null;
            }
            return lobjResponse;
        }

        public PGResponse InitiatePayment(PGRequest pgRequest, string lstrToken)
        {
            PGResponse lobjPGResponse = null;
            try
            {
                string lstrResponse = WebAPIHelper.PostData(APIConstant.InitiatePayment, "POST", "payment", JsonConvert.SerializeObject(pgRequest), lstrToken);
                if (!string.IsNullOrEmpty(lstrResponse))
                {
                    lobjPGResponse = JsonConvert.DeserializeObject<PGResponse>(lstrResponse);
                }
                else
                {
                    lobjPGResponse = null;
                }
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("APIClientHelper InitiatePayment Exception: " + ex.Message + Environment.NewLine + ex.StackTrace);
                lobjPGResponse = null;
            }
            return lobjPGResponse;
        }

        public PGDetails GetPaymentStatusByOrderId(string orderId, string lstrToken)
        {
            PGDetails lobjPGDetails = null;
            try
            {
                string lstrResponse = WebAPIHelper.PostData(APIConstant.GetPaymentStatusByOrderId + "?orderId=" + orderId, "GET", "payment", string.Empty, lstrToken);
                if (!string.IsNullOrEmpty(lstrResponse))
                {
                    lobjPGDetails = JsonConvert.DeserializeObject<PGDetails>(lstrResponse);
                }
                else
                {
                    lobjPGDetails = null;
                }
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("APIClientHelper GetPaymentStatusByOrderId Exception: " + ex.Message + Environment.NewLine + ex.StackTrace);
                lobjPGDetails = null;
            }
            return lobjPGDetails;
        }

        public PGRefundResponse InitiatePaymentRefund(PGRefundRequest pgRefundRequest, string lstrToken)
        {
            PGRefundResponse lobjPGResponse = null;
            try
            {
                string lstrResponse = WebAPIHelper.PostData(APIConstant.InitiatePaymentRefund, "POST", "refund", JsonConvert.SerializeObject(pgRefundRequest), lstrToken);
                if (!string.IsNullOrEmpty(lstrResponse))
                {
                    lobjPGResponse = JsonConvert.DeserializeObject<PGRefundResponse>(lstrResponse);
                }
                else
                {
                    lobjPGResponse = null;
                }
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("APIClientHelper InitiatePaymentRefund Exception: " + ex.Message + Environment.NewLine + ex.StackTrace);
                lobjPGResponse = null;
            }
            return lobjPGResponse;
        }


        public MemberDetails GetMemberDetailsByUniqueAttribute(int pintProgramId, string pstrUniqueAttributeValue, string pstrToken)
        {
            MemberDetails lobjResponse;
            try
            {
                APIResponseResults lobjAPIResponseResults = new APIResponseResults();
                string pstrUniqueAttributeKey = Convert.ToString(ConfigurationManager.AppSettings["UniqueMemberAttributeKey"]);
                lobjAPIResponseResults = JsonConvert.DeserializeObject<APIResponseResults>(WebAPIHelper.PostData(string.Format("{0}?pintProgramId={1}&pstrUniqueAttributeKey={2}&pstrUniqueAttributeValue={3}", APIConstant.GetMemberDetailsByUniqueAttribute, pintProgramId, pstrUniqueAttributeKey, pstrUniqueAttributeValue), "GET", "GetMemberDetailsByUniqueAttribute", string.Empty, pstrToken));
                if (lobjAPIResponseResults.results.IsSucessful)
                {
                    lobjResponse = JsonConvert.DeserializeObject<MemberDetails>(JsonConvert.SerializeObject(lobjAPIResponseResults.results.ReturnObject));
                }
                else
                {
                    lobjResponse = null;
                }
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("APIClientHelper GetMemberDetails Exception: " + ex.Message + Environment.NewLine + ex.StackTrace);
                lobjResponse = null;
            }
            return lobjResponse;
        }
        #endregion
    }
}
