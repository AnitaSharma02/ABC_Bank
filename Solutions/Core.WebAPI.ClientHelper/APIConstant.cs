using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WebAPI.ClientHelper
{

    public class APIConstant
    {
        public static string BaseCoreProgramAPIURL = Convert.ToString(ConfigurationManager.AppSettings["BaseCoreProgramAPIURL"]);
        public static string BaseCorePGAPIURL = Convert.ToString(ConfigurationManager.AppSettings["BaseCorePGAPIURL"]);
        public static string BaseCoreTransactionAPIURL = Convert.ToString(ConfigurationManager.AppSettings["BaseCoreTransactionAPIURL"]);
        public static string BaseCoreReportAPIURL = Convert.ToString(ConfigurationManager.AppSettings["BaseCoreReportAPIURL"]);
        public static string BaseCoreMemberAPIURL = Convert.ToString(ConfigurationManager.AppSettings["BaseCoreMemberAPIURL"]);
        public static string BaseCoreOAuthAPIURL = Convert.ToString(ConfigurationManager.AppSettings["BaseCoreOAuthAPIURL"]);
        public static string BaseCoreCEAPIURL = Convert.ToString(ConfigurationManager.AppSettings["BaseCoreCEAPIURL"]);
        public static string KhaltiInsuranceAPIURL = Convert.ToString(ConfigurationManager.AppSettings["KhaltiInsuranceAPIURL"]);
        public static string KhaltiIBEDomesticFlightAPIURL = Convert.ToString(ConfigurationManager.AppSettings["KhaltiIBEDomesticFlightAPIURL"]);
        public static string KhaltiISPAPIURL = Convert.ToString(ConfigurationManager.AppSettings["KhaltiISPAPIURL"]);
        public static string BaseCorePGAPIURLNew = Convert.ToString(ConfigurationManager.AppSettings["BaseCorePGAPIURLNew"]);
        public static string GiiftPaymentAPIURL= Convert.ToString(ConfigurationManager.AppSettings["GiiftPaymentAPIURL"]); 
        #region OAuth

        //** OAuth **//
        public static string Token = string.Format("{0}API/Auth/Token", BaseCoreOAuthAPIURL);
        //** OAuth **//
        #endregion

        #region Program
        //** Program **//

        public static string InsertProgramDefinition = string.Format("{0}api/Program/InsertProgramDefinition", BaseCoreProgramAPIURL);
        public static string GetProgramDefinition = string.Format("{0}api/Program/GetProgramDefinition", BaseCoreProgramAPIURL);
        public static string UpdateProgramDefinition = string.Format("{0}api/Program/UpdateProgramDefinition", BaseCoreProgramAPIURL);
        public static string GetProgramAttributes = string.Format("{0}api/Program/GetProgramAttributes", BaseCoreProgramAPIURL);
        public static string UpdateProgramAttributes = string.Format("{0}api/Program/UpdateProgramAttributes", BaseCoreProgramAPIURL);
        public static string GetProgramDefinitionList = string.Format("{0}api/Program/GetProgramDefinitionList", BaseCoreProgramAPIURL);
        public static string InsertSystemParameter = string.Format("{0}api/Program/InsertSystemParameter", BaseCoreProgramAPIURL);
        public static string GetSystemParameter = string.Format("{0}api/Program/GetSystemParameter", BaseCoreProgramAPIURL);
        public static string UpdateSystemParameter = string.Format("{0}api/Program/UpdateSystemParameter", BaseCoreProgramAPIURL);
        public static string InsertProgramCurrencyDefinition = string.Format("{0}api/Program/InsertProgramCurrencyDefinition", BaseCoreProgramAPIURL);
        public static string GetProgramCurrencyDefinitionList = string.Format("{0}api/Program/GetProgramCurrencyDefinitionList", BaseCoreProgramAPIURL);
        public static string UpdateProgramCurrencyDefinition = string.Format("{0}api/Program/UpdateProgramCurrencyDefinition", BaseCoreProgramAPIURL);
        public static string InsertRedemptionKeys = string.Format("{0}api/Program/InsertRedemptionKeys", BaseCoreProgramAPIURL);
        public static string GetRedemptionKeyslst = string.Format("{0}api/Program/GetRedemptionKeyslst", BaseCoreProgramAPIURL);
        public static string UpdateProgramRedemptionKeys = string.Format("{0}api/Program/UpdateProgramRedemptionKeys", BaseCoreProgramAPIURL);
        public static string GetProgramRedemptionRate = string.Format("{0}api/Program/GetProgramRedemptionRate", BaseCoreProgramAPIURL);
        public static string InsertCustomerSegment = string.Format("{0}api/Program/InsertCustomerSegment", BaseCoreProgramAPIURL);
        public static string GetAllCustomerSegmentlst = string.Format("{0}api/Program/GetAllCustomerSegmentlst", BaseCoreProgramAPIURL);
        public static string UpdateCustomerSegment = string.Format("{0}api/Program/UpdateCustomerSegment", BaseCoreProgramAPIURL);
        public static string InsertProductCategory = string.Format("{0}api/Program/InsertProductCategory", BaseCoreProgramAPIURL);
        public static string GetProductCategoryList = string.Format("{0}api/Program/GetProductCategoryList", BaseCoreProgramAPIURL);
        public static string UpdateProductCategory = string.Format("{0}api/Program/UpdateProductCategory", BaseCoreProgramAPIURL);
        public static string InsertProductDefinition = string.Format("{0}api/Program/InsertProductDefinition", BaseCoreProgramAPIURL);
        public static string GetAllProductDefinition = string.Format("{0}api/Program/GetAllProductDefinition", BaseCoreProgramAPIURL);
        public static string GetProductDefinitionByCategoryId = string.Format("{0}api/Program/GetProductDefinitionByCategoryId", BaseCoreProgramAPIURL);
        public static string UpdateProductDefinition = string.Format("{0}api/Program/UpdateProductDefinition", BaseCoreProgramAPIURL);
        public static string GetProductDefinitionlst = string.Format("{0}api/Program/GetProductDefinitionlst", BaseCoreProgramAPIURL);
        public static string InsertProductCode = string.Format("{0}api/Program/InsertProductCode", BaseCoreProgramAPIURL);
        public static string InsertProductCodeRangeList = string.Format("{0}api/Program/InsertProductCodeRangeList", BaseCoreProgramAPIURL);
        public static string UpdateProductCodeRange = string.Format("{0}api/Program/UpdateProductCodeRange", BaseCoreProgramAPIURL);
        public static string InsertDealCode = string.Format("{0}api/Program/InsertDealCode", BaseCoreProgramAPIURL);
        public static string UpdateDealCode = string.Format("{0}api/Program/UpdateDealCode", BaseCoreProgramAPIURL);
        public static string InsertDealCodeRangeList = string.Format("{0}api/Program/InsertDealCodeRangeList", BaseCoreProgramAPIURL);
        public static string UpdateDealCodeRange = string.Format("{0}api/Program/UpdateDealCodeRange", BaseCoreProgramAPIURL);
        public static string InsertMccProductcodeRules = string.Format("{0}api/Program/InsertMccProductcodeRules", BaseCoreProgramAPIURL);
        public static string UpdateMccProductcodeRules = string.Format("{0}api/Program/UpdateMccProductcodeRules", BaseCoreProgramAPIURL);
        public static string InsertProgramProductCode = string.Format("{0}api/Program/InsertProgramProductCode", BaseCoreProgramAPIURL);
        public static string GetProgramProductCodeList = string.Format("{0}api/Program/GetProgramProductCodeList", BaseCoreProgramAPIURL);
        public static string UpdateProgramProductCode = string.Format("{0}api/Program/UpdateProgramProductCode", BaseCoreProgramAPIURL);
        public static string InsertProgramTransactionCode = string.Format("{0}api/Program/InsertProgramTransactionCode", BaseCoreProgramAPIURL);
        public static string GetProgramTransactionCodelst = string.Format("{0}api/Program/GetProgramTransactionCodelst", BaseCoreProgramAPIURL);
        public static string UpdateProgramTransactionCode = string.Format("{0}api/Program/UpdateProgramTransactionCode", BaseCoreProgramAPIURL);
        public static string InsertProductTransactionCode = string.Format("{0}api/Program/InsertProductTransactionCode", BaseCoreProgramAPIURL);
        public static string GetProductTransactionCodelst = string.Format("{0}api/Program/GetProductTransactionCodelst", BaseCoreProgramAPIURL);
        public static string UpdateProductTransactionCode = string.Format("{0}api/Program/UpdateProductTransactionCode", BaseCoreProgramAPIURL);

        //** Program **//
        #endregion

        #region Member
        //** Member **//

        public static string CreateProfile = string.Format("{0}API/Member/CreateProfile", BaseCoreMemberAPIURL);
        public static string GetMemberProfile = string.Format("{0}API/Member/GetMemberProfile", BaseCoreMemberAPIURL);
        public static string GetMemberDetailsByNationalID = string.Format("{0}API/Member/GetMemberDetailsByNationalID", BaseCoreMemberAPIURL);
        public static string GetMemberDetailsByEmailID = string.Format("{0}API/Member/GetMemberDetailsByEmailID", BaseCoreMemberAPIURL);
        public static string UpdateProfile = string.Format("{0}API/Member/UpdateProfile", BaseCoreMemberAPIURL);
        public static string CheckMemberExistsUsingEmailID = string.Format("{0}API/Member/CheckMemberExistsUsingEmailID", BaseCoreMemberAPIURL);
        public static string GenerateOTP = string.Format("{0}API/Member/GenerateOTP", BaseCoreMemberAPIURL);
        public static string GenerateOTPByNationalID = string.Format("{0}API/Member/GenerateOTPByNationalID", BaseCoreMemberAPIURL);
        public static string GenerateOTPByRelationReference = string.Format("{0}API/Member/GenerateOTPByRelationReference", BaseCoreMemberAPIURL);
        public static string VerifyOTP = string.Format("{0}API/Member/VerifyOTP", BaseCoreMemberAPIURL);
        public static string VerifyOTPByNationalID = string.Format("{0}API/Member/VerifyOTPByNationalID", BaseCoreMemberAPIURL);
        public static string VerifyOTPByRelationReference = string.Format("{0}API/Member/VerifyOTPByRelationReference", BaseCoreMemberAPIURL);
        public static string CheckMembershipCredentials = string.Format("{0}API/Member/CheckMembershipCredentials", BaseCoreMemberAPIURL);
        public static string CheckMembershipCredentialsByNationalID = string.Format("{0}API/Member/CheckMembershipCredentialsByNationalID", BaseCoreMemberAPIURL);
        public static string CheckMembershipCredentialsByRelationReference = string.Format("{0}API/Member/CheckMembershipCredentialsByRelationReference", BaseCoreMemberAPIURL);
        public static string GetTotalMember = string.Format("{0}API/Member/GetTotalMember", BaseCoreMemberAPIURL);
        public static string GetSearchMemberDetailAdvanceSearch = string.Format("{0}API/Member/GetSearchMemberDetailAdvanceSearch", BaseCoreMemberAPIURL);
        public static string CheckMemberExist = string.Format("{0}API/Member/CheckMemberExist", BaseCoreMemberAPIURL);
        public static string ChangeMemberAccountStatus = string.Format("{0}API/Member/ChangeMemberAccountStatus", BaseCoreMemberAPIURL);
        public static string ChangePasswordForMember = string.Format("{0}API/Member/ChangePasswordForMember", BaseCoreMemberAPIURL);
        public static string MemberActivation = string.Format("{0}API/Member/MemberActivation", BaseCoreMemberAPIURL);
        public static string MemberActivationByRelationReference = string.Format("{0}API/Member/MemberActivationByRelationReference", BaseCoreMemberAPIURL);
        public static string MemberActivationByNationalID = string.Format("{0}API/Member/MemberActivationByNationalID", BaseCoreMemberAPIURL);
        public static string ResetLoginAttempt = string.Format("{0}api/Member/ResetLoginAttempt", BaseCoreMemberAPIURL);
        public static string GenerateOTPDetails = string.Format("{0}api/Member/GenerateOTPDetails", BaseCoreMemberAPIURL);

        public static string InsertMemberActivityWithSessionID = string.Format("{0}api/Activity/InsertMemberActivityWithSessionID", BaseCoreMemberAPIURL);
        public static string InsertMemberActivity = string.Format("{0}api/Activity/InsertMemberActivity", BaseCoreMemberAPIURL);
        public static string GetMembershipActivitySessionDetails = string.Format("{0}api/Activity/GetMembershipActivitySessionDetails", BaseCoreMemberAPIURL);
        public static string GetMembershipActivityAllSessionDetails = string.Format("{0}api/Activity/GetMembershipActivityAllSessionDetails", BaseCoreMemberAPIURL);
        public static string GetMemberActivityForASessionID = string.Format("{0}api/Activity/GetMemberActivityForASessionID", BaseCoreMemberAPIURL);
        public static string UpdateMemberShipActivitySession = string.Format("{0}api/Activity/UpdateMemberShipActivitySession", BaseCoreMemberAPIURL);
        

        public static string GetMasterPreference = string.Format("{0}API/Preference/GetMasterPreference", BaseCoreMemberAPIURL);
        public static string RecordMyPreferences = string.Format("{0}API/Preference/RecordMyPreferences", BaseCoreMemberAPIURL);
        public static string GetMemberPreferences = string.Format("{0}API/Preference/GetMemberPreferences", BaseCoreMemberAPIURL);

        public static string GetProgramDetails = string.Format("{0}api/Program/GetProgramDetails", BaseCoreMemberAPIURL);
        public static string GetProductCodeDetails = string.Format("{0}api/Program/GetProductCodeDetails", BaseCoreMemberAPIURL);

        public static string UpdatePassword = string.Format("{0}api/Member/UpdatePassword", BaseCoreMemberAPIURL);
        public static string ResetPassword = string.Format("{0}api/Member/ResetPassword", BaseCoreMemberAPIURL);
        public static string GetDetailsFromHashKey = string.Format("{0}api/Member/GetDetailsFromHashKey", BaseCoreMemberAPIURL);
        public static string ValidateResetToken = string.Format("{0}api/Member/ValidateResetToken", BaseCoreMemberAPIURL);
        public static string InsertMemberSecurityDetails = string.Format("{0}api/Member/InsertMemberSecurityDetails", BaseCoreMemberAPIURL);
        public static string GetMemberSecurityDetails = string.Format("{0}api/Member/GetMemberSecurityDetails", BaseCoreMemberAPIURL);
        public static string CheckWhetherOTPExists = string.Format("{0}api/Member/CheckWhetherOTPExists", BaseCoreMemberAPIURL);
        public static string CheckRedemptionOTP = string.Format("{0}api/Member/CheckRedemptionOTP", BaseCoreMemberAPIURL);
        public static string InsertRedemptionAuditTrail = string.Format("{0}api/AuditTrail/InsertRedemptionAuditTrail", BaseCoreMemberAPIURL);

        public static string GetMemberDetailsByUniqueAttribute = string.Format("{0}API/Member/GetMemberDetailsByUniqueAttribute", BaseCoreMemberAPIURL);

        //** Member **//
        #endregion

        #region PG
        //** PG **//

        public static string CheckAvailability = string.Format("{0}API/PG/CheckAvailability", BaseCorePGAPIURL);
        public static string RedeemPoints = string.Format("{0}API/PG/RedeemPoints", BaseCorePGAPIURL);
        public static string ReversalPoints = string.Format("{0}API/PG/ReversalPoints", BaseCorePGAPIURL);

        //** PG **//
        #endregion

        #region Transaction
        //** Transaction **//

        public static string PointsAccrual = string.Format("{0}API/Transaction/PointsAccrual", BaseCoreTransactionAPIURL);
        public static string AwardFlatPoints = string.Format("{0}API/Transaction/AwardFlatPoints", BaseCoreTransactionAPIURL);
        public static string GetMemberStatementSummary = string.Format("{0}API/Transaction/GetMemberStatementSummary", BaseCoreTransactionAPIURL);
        public static string GetTotalMemberTransaction = string.Format("{0}API/Transaction/GetTotalMemberTransaction", BaseCoreTransactionAPIURL);
        public static string GetMemberTransactionSummary = string.Format("{0}API/Transaction/GetMemberTransactionSummary", BaseCoreTransactionAPIURL);
        public static string GetTotalMemberTransactionByDate = string.Format("{0}API/Transaction/GetTotalMemberTransactionByDate", BaseCoreTransactionAPIURL);
        public static string GetMemberTransactionSummaryByDate = string.Format("{0}API/Transaction/GetMemberTransactionSummaryByDate", BaseCoreTransactionAPIURL);
        public static string GetExpirySchedule = string.Format("{0}API/Transaction/GetExpirySchedule", BaseCoreTransactionAPIURL);
        public static string GetNextExpiredPointsOnDate = string.Format("{0}API/Transaction/GetNextExpiredPointsOnDate", BaseCoreTransactionAPIURL);
        public static string GetNextYearExpirySchedule = string.Format("{0}API/Transaction/GetNextYearExpirySchedule", BaseCoreTransactionAPIURL);
        public static string TransferPoints = string.Format("{0}API/Transaction/TransferPoints", BaseCoreTransactionAPIURL);
        public static string GetTransactionSummary = string.Format("{0}API/Transaction/GetTransactionSummary", BaseCoreTransactionAPIURL);
        public static string InsertManualTransactionDetails = string.Format("{0}API/Transaction/InsertManualTransactionDetails", BaseCoreTransactionAPIURL);
        public static string GetMemberStatementTransaction = string.Format("{0}API/Transaction/GetMemberStatementTransaction", BaseCoreTransactionAPIURL);
        public static string GetTotalMemberAccrualTransaction = string.Format("{0}API/Transaction/GetTotalMemberAccrualTransaction", BaseCoreTransactionAPIURL);
        public static string GetTotalMemberAccrualTransactionByDate = string.Format("{0}API/Transaction/GetTotalMemberAccrualTransactionByDate", BaseCoreTransactionAPIURL);
        public static string GetMemberAccrualTransaction = string.Format("{0}API/Transaction/GetMemberAccrualTransaction", BaseCoreTransactionAPIURL);
        public static string GetMemberAccrualTransactionByDate = string.Format("{0}API/Transaction/GetMemberAccrualTransactionByDate", BaseCoreTransactionAPIURL);
        public static string GetTotalMemberRedemptionTransaction = string.Format("{0}API/Transaction/GetTotalMemberRedemptionTransaction", BaseCoreTransactionAPIURL);
        public static string GetTotalMemberRedemptionTransactionByDate = string.Format("{0}API/Transaction/GetMemberRedemptionTransaction", BaseCoreTransactionAPIURL);
        public static string GetMemberRedemptionTransaction = string.Format("{0}API/Transaction/GetTotalMemberRedemptionTransactionByDate", BaseCoreTransactionAPIURL);
        public static string GetMemberRedemptionTransactionByDate = string.Format("{0}API/Transaction/GetMemberRedemptionTransactionByDate", BaseCoreTransactionAPIURL);
        public static string UpdateVoidRedemptionTxn = string.Format("{0}API/Transaction/UpdateVoidRedemptionTxn", BaseCoreTransactionAPIURL);
        public static string GetTransactionExpirySchedule = string.Format("{0}API/Transaction/GetTransactionExpirySchedule", BaseCoreTransactionAPIURL);


        //** Transaction **//
        #endregion

        #region Report
        //** Report **//

        public static string AccrualDetails = string.Format("{0}api/Report/AccrualDetails", BaseCoreReportAPIURL);
        public static string RedemptionDetails = string.Format("{0}api/Report/RedemptionDetails", BaseCoreReportAPIURL);
        public static string ReversalDetails = string.Format("{0}api/Report/ReversalDetails", BaseCoreReportAPIURL);
        public static string LiabilityDetails = string.Format("{0}api/Report/LiabilityDetails", BaseCoreReportAPIURL);
        public static string MemberFileProcessingDetails = string.Format("{0}api/Report/MemberFileProcessingDetails", BaseCoreReportAPIURL);
        public static string TransactionFileProcessingDetails = string.Format("{0}api/Report/TransactionFileProcessingDetails", BaseCoreReportAPIURL);
        public static string BonusFileProcessingDetails = string.Format("{0}api/Report/BonusFileProcessingDetails", BaseCoreReportAPIURL);
        public static string PurchasePointsDetails = string.Format("{0}api/Report/PurchasePointsDetails", BaseCoreReportAPIURL);
        public static string EmailDetails = string.Format("{0}api/Report/EmailDetails", BaseCoreReportAPIURL);
        public static string SMSDetails = string.Format("{0}api/Report/SMSDetails", BaseCoreReportAPIURL);
        public static string MemberActivityDetails = string.Format("{0}api/Report/MemberActivityDetails", BaseCoreReportAPIURL);
        public static string SpoilagePointsDetails = string.Format("{0}api/Report/SpoilagePointsDetails", BaseCoreReportAPIURL);
        public static string MonthlyStatementDetails = string.Format("{0}api/Report/MonthlyStatementDetails", BaseCoreReportAPIURL);
        public static string MemberDetails = string.Format("{0}api/Report/MemberDetails", BaseCoreReportAPIURL);
        public static string MissingMemberDetails = string.Format("{0}api/Report/MissingMemberDetails", BaseCoreReportAPIURL);
        public static string GetMemberStatusInfo = string.Format("{0}api/Report/GetMemberStatusInfo", BaseCoreReportAPIURL);

        //** Report **//
        #endregion

        #region CE
        //** CE **//
        public static string InsertEmailDetails = string.Format("{0}api/CE/InsertEmailDetails", BaseCoreCEAPIURL);
        public static string InsertSmsDetails = string.Format("{0}api/CE/InsertSmsDetails", BaseCoreCEAPIURL);
        //** CE **//
        #endregion

        #region Khalti Insurance
        public static string GetInsuranceServiceProviders = string.Format("{0}api/insurance/GetInsuranceServiceProviders", KhaltiInsuranceAPIURL);
        public static string GetRequiredDetails = string.Format("{0}api/insurance/GetRequiredDetails", KhaltiInsuranceAPIURL);
        public static string FetchUserDetails= string.Format("{0}api/insurance/FetchUserDetails", KhaltiInsuranceAPIURL);
        public static string InsurancePaymentRequest= string.Format("{0}api/insurance/InsurancePaymentRequest", KhaltiInsuranceAPIURL);
        public static string GetBookedInsuranceListForMember= string.Format("{0}api/insurance/GetBookedInsuranceListForMember", KhaltiInsuranceAPIURL);
        #endregion

        #region Khalti Domestic Flight
        public static string GetSectorRequest = string.Format("{0}/api/Flight/SectorRequest", KhaltiIBEDomesticFlightAPIURL);
        public static string SearchRequest = string.Format("{0}/api/Flight/SearchRequest", KhaltiIBEDomesticFlightAPIURL);
        public static string CreateItinerary = string.Format("{0}/api/Flight/CreateItinerary", KhaltiIBEDomesticFlightAPIURL);
        public static string CreateBooking = string.Format("{0}/api/Flight/CreateBooking", KhaltiIBEDomesticFlightAPIURL);
        public static string BookingStatus= string.Format("{0}/api/Flight/BookingStatus", KhaltiIBEDomesticFlightAPIURL);
        public static string TicketDownload= string.Format("{0}/api/Flight/TicketDownload", KhaltiIBEDomesticFlightAPIURL);
        public static string GetBookingByMemberId = string.Format("{0}/api/Flight/GetBookingByMemberId", KhaltiIBEDomesticFlightAPIURL);
        #endregion

        #region Khalti ISP(Internet Service Provider)
        public static string GetInternetServiceProvider = string.Format("{0}api/isp/GetInternetServiceProviders", KhaltiISPAPIURL);
        public static string GetISPRequiredDetails = string.Format("{0}api/isp/GetRequiredDetails", KhaltiISPAPIURL);
        public static string FetchISPUserDetails = string.Format("{0}api/isp/FetchUserDetails", KhaltiISPAPIURL);
        public static string ISPPaymentRequest = string.Format("{0}api/isp/ISPPaymentRequest", KhaltiISPAPIURL);
        public static string GetDiscount= string.Format("{0}api/isp/GetDiscount", KhaltiISPAPIURL);
        public static string GetBookedISPListForMember = string.Format("{0}api/isp/GetBookedISPListForMember", KhaltiISPAPIURL);
        #endregion

        public static string SendCommunication= string.Format("{0}API/Communication/SendCommunication", BaseCoreMemberAPIURL);
        public static string GetTransactionAdditionalInfo = string.Format("{0}API/Transaction/GetTransactionAdditionalInfo", BaseCoreTransactionAPIURL);

        #region PG
        public static string GetPGAuthToken = string.Format("{0}/api/v1/auth/login", GiiftPaymentAPIURL);
        public static string InitiatePayment = string.Format("{0}/api/v1/payment", GiiftPaymentAPIURL);
        public static string GetPaymentStatusByOrderId = string.Format("{0}/api/v1/payment", GiiftPaymentAPIURL);
        public static string InitiatePaymentRefund = string.Format("{0}/api/v1/payment/refund", GiiftPaymentAPIURL);
        #endregion
    }
}
