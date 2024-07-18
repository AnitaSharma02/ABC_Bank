using CB.IBE.Platform.Transactions.Entites;
using Core.Platform.Member.Entites;
using Framework.EnterpriseLibrary.Adapters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Platform.Booking.Entities;
using CB.IBE.DepositAccountService.ClientHelper;
using Core.Platform.ProgramMaster.Entities;
using Core.Platform.PointGateway.Service.Helper;
using Core.Platform.Configurations;
using Core.Platform.Transactions.Entites;
using CB.IBE.Platform.ClientEntities;
using CB.IBE.Platform.Masters.Entities;
using CB.IBE.Platform.IBEClient;
using CB.IBE.Platform.Entities;
using CB.IBE.Platform.Hotels.ClientEntities;
using Framework.Integrations.Hotels.Entities;
using Framework.EnterpriseLibrary.CommunicationEngine.Entity;
using Framework.EnterpriseLibrary.CommunicationEngine.Helper;
using CB.IBE.Platform.Car.ClientEntities;
using CB.IBE.Platform.Car.Entities;
using CB.IBE.Platform.IBECarClient;
using Core.Platform.OTP.Entities;
using Core.Platform.OTP.Facade;
using Framework.EnterpriseLibrary.UniqueNumberGenerator;
using System.Configuration;
using InfiVoucher.Platform.Entities;
using Core.Platform.InfiVoucher.Entities;
using Core.Platform.InfiVoucher.ClientHelper;
using Framework.EnterpriseLibrary.Common.SerializationHelper;
using LoyaltyManagement.Request;
using Core.WebAPI.ClientHelper;
using System.Web;
using Newtonsoft.Json;
using CE.Entities;
using System.Globalization;

namespace Core.Framework.Booking.Facade
{
    public class BookingIntegrationFacade
    {
        enum FailureType { FAILUREEMAIL, SMS, CTRESPONSE, BLOCKMILES, REDEEMMILES, ROLLBACKMILES };
        string lstrMerchantId = Convert.ToString(ConfigurationManager.AppSettings["MerchantId"]);
        string lstrMerchantUserName = Convert.ToString(ConfigurationManager.AppSettings["MerchantUserName"]);
        string lstrMerchantPassword = Convert.ToString(ConfigurationManager.AppSettings["MerchantPassword"]);
        string lstrProgramName = Convert.ToString(ConfigurationManager.AppSettings["ProgramName"]);
        string lstrBookingFailureMsg = Convert.ToString(ConfigurationManager.AppSettings["BookingFailureMsg"]);

        #region Flight

        public BookingResponse BookFlight(BookingRequest pobjBookingRequest, MemberDetails pobjMemberDetails, List<RedemptionDetails> pobjListOfRedemptionDetails)
        {
            bool IsBookingConfirm = false;
            string strRedeemMilesResponse = string.Empty;
            string strRollBackMilesResponse = string.Empty;
            bool lboolRollBackResponse = false;
            string strFailureType = "";

            BookingResponse lobjBookingResponse = new BookingResponse();
            PGHelper lobjPGHelper = new PGHelper();

            try
            {
                bool IsRedeem = true;
                List<BookingPaymentBreakage> lobjListOfBookingPaymentBreakage = new List<BookingPaymentBreakage>();
                for (int i = 0; i < pobjListOfRedemptionDetails.Count; i++)
                {
                    BookingPaymentBreakage lobjBookingPaymentBreakage = new BookingPaymentBreakage();
                    strRedeemMilesResponse = RedeemPoints((float)(pobjListOfRedemptionDetails[i].Amount), pobjListOfRedemptionDetails[i].Points, pobjMemberDetails.MemberRelationsList.Find(lobj => lobj.RelationType.Equals(RelationType.LBMS)).RelationReference, pobjMemberDetails.MemberRelationsList.Find(lobj => lobj.RelationType.Equals(RelationType.LBMS)).WebPassword, pobjBookingRequest.ItineraryDetails.OriginLocation + "-" + pobjBookingRequest.ItineraryDetails.DestinationLocation, Convert.ToInt32(LoyaltyTxnType.Air), pobjListOfRedemptionDetails[i].Currency, pobjMemberDetails);

                    pobjListOfRedemptionDetails[i].TransactionReference = strRedeemMilesResponse;
                    IsRedeem = IsRedeem && (pobjListOfRedemptionDetails[i].TransactionReference != "" && pobjListOfRedemptionDetails[i].TransactionReference != string.Empty);

                    lobjBookingPaymentBreakage.Currency = pobjListOfRedemptionDetails[i].Currency;
                    lobjBookingPaymentBreakage.Amount = pobjListOfRedemptionDetails[i].Points;
                    lobjBookingPaymentBreakage.TxnReference = strRedeemMilesResponse;

                    lobjListOfBookingPaymentBreakage.Add(lobjBookingPaymentBreakage);
                }

                BookingPaymentDetails lobjBookingPaymentDetails = new BookingPaymentDetails();
                lobjBookingPaymentDetails.BookingPaymentBreakageList = lobjListOfBookingPaymentBreakage;
                lobjBookingPaymentDetails.MemberId = pobjMemberDetails.MemberRelationsList[0].RelationReference;
                lobjBookingPaymentDetails.PaymentType = PaymentType.Points;
                lobjBookingPaymentDetails.ServiceType = ServiceType.FLIGHT;
                lobjBookingPaymentDetails.Points = pobjListOfRedemptionDetails[0].Points;
                lobjBookingPaymentDetails.PointsTxnRefererence = pobjListOfRedemptionDetails[0].TransactionReference;
                lobjBookingPaymentDetails.PaymentStatus = PaymentStatus.Success;

                pobjBookingRequest.ItineraryDetails.BookingPaymentDetails = lobjBookingPaymentDetails;

                if (IsRedeem)
                {
                    pobjBookingRequest.ItineraryDetails.TransactionReference = pobjListOfRedemptionDetails[0].TransactionReference;
                    IBECTClient lobjIBECTClient = new IBECTClient();

                    try
                    {
                        LoggingAdapter.WriteLog("RedeemMiles Success : " + strRedeemMilesResponse, "BookingLogCategory");
                        try
                        {
                            LoggingAdapter.WriteLog("BookFlight pobjBookingRequest: " + JsonConvert.SerializeObject(pobjBookingRequest), "BookingLogCategory");
                            lobjBookingResponse = lobjIBECTClient.CreateBooking(pobjBookingRequest);
                            LoggingAdapter.WriteLog("BookFlight lobjBookingResponse: " + JsonConvert.SerializeObject(lobjBookingResponse), "BookingLogCategory");
                        }
                        catch (Exception ex)
                        {
                            LoggingAdapter.WriteLog("CreateBooking Failure BookingFacade:" + ex.Message + ex.StackTrace);
                        }

                        if (lobjBookingResponse != null && lobjBookingResponse.PNRDetails.TripId != null && lobjBookingResponse.PNRDetails.TripId != string.Empty && lobjBookingResponse.PNRDetails.Status.Equals(1))
                        {
                            IsBookingConfirm = true;
                            LoggingAdapter.WriteLog("Booking called Trip Id  : " + lobjBookingResponse.PNRDetails.TripId + "IsBookingConfirm:" + IsBookingConfirm, "BookingLogCategory");
                        }
                        else
                        {
                            IsBookingConfirm = false;
                            LoggingAdapter.WriteLog("IsBookingConfirm:" + IsBookingConfirm, "BookingLogCategory");
                        }
                        if (IsBookingConfirm)
                        {
                            try
                            {
                                //communication Engine call for Email send
                                //List<string> lstEmailparameter = new List<string>();
                                //lstEmailparameter = GenerateFlightEmailParameters(lobjBookingResponse);
                                ItineraryDetails lobjItineraryDetails = new ItineraryDetails();
                                lobjItineraryDetails = GetFlightReceipt(Convert.ToInt32(lobjBookingResponse.BookingId.ToString().Trim()));
                                string strPaxInfo = "";
                                strPaxInfo += "<table cellpadding='0' cellspacing='0' width='100 %' border='0'><tr>";
                                strPaxInfo += "<td align='left' valign='top' bgcolor='#E3E3E3' width='15%' style='font-family: Arial; font-size: 13px; letter-spacing: normal; line-height: 18px; font-weight: normal; text-transform: capitalize; text-align: left; color: #000000; padding: 10px; border: 1px solid #dddddd; border-bottom: 1px solid #dddddd;'> Title </td>";
                                strPaxInfo += "<td align='left' valign='top' bgcolor='#E3E3E3' width='40%' style='font-family: Arial; font-size: 13px; letter-spacing: normal; line-height: 18px; font-weight: normal; text-transform: capitalize; text-align: left; color: #000000; padding: 10px; border: 1px solid #dddddd; border-bottom: 1px solid #dddddd;'>Passenger Name</td>";
                                strPaxInfo += "<td align='left' valign='top' bgcolor='#E3E3E3' width='22%' style='font-family: Arial; font-size: 13px; letter-spacing: normal; line-height: 18px; font-weight: normal; text-transform: capitalize; text-align: left; color: #000000; padding: 10px; border: 1px solid #dddddd; border-bottom: 1px solid #dddddd;'>Ticket No.</td>";
                                strPaxInfo += "<td align='left' valign='top' bgcolor='#E3E3E3' width='12%' style='font-family: Arial; font-size: 13px; letter-spacing: normal; line-height: 18px; font-weight: normal; text-transform: capitalize; text-align: left; color: #000000; padding: 10px; border: 1px solid #dddddd; border-bottom: 1px solid #dddddd;'>Gender</td>";
                                strPaxInfo += "<td align='left' valign='top' bgcolor='#E3E3E3' width='7%'  style='font-family: Arial; font-size: 13px; letter-spacing: normal; line-height: 18px; font-weight: normal; text-transform: capitalize; text-align: left; color: #000000; padding: 10px; border: 1px solid #dddddd; border-bottom: 1px solid #dddddd;'>Age</td>";
                                strPaxInfo += "</tr>";
                                List<PassengerDetails> lobjListOfPassengerDetails = new List<PassengerDetails>();
                                if (lobjItineraryDetails.TravelerInfo != null)
                                {
                                    string lstrPaxtype = "";
                                    lobjListOfPassengerDetails = lobjItineraryDetails.TravelerInfo;
                                    for (int k = 0; k < lobjListOfPassengerDetails.Count; k++)
                                    {

                                        if (Convert.ToString(lobjListOfPassengerDetails[k].PaxType) == "ADT")
                                        {
                                            lstrPaxtype = "Adult";
                                        }
                                        else if (Convert.ToString(lobjListOfPassengerDetails[k].PaxType) == "CHD")
                                        {
                                            lstrPaxtype = "Child";
                                        }
                                        else if (Convert.ToString(lobjListOfPassengerDetails[k].PaxType) == "INF")
                                        {
                                            lstrPaxtype = "Infant";
                                        }
                                        strPaxInfo += "<tr>";
                                        strPaxInfo += "<td align='left' valign='top' width='15%' style='font-family:Arial; font-size:12px; color:#231f20; padding: 10px; border: 1px solid #dddddd; border-bottom: 2px solid #dddddd;'> " + lstrPaxtype + " </td>";
                                        strPaxInfo += "<td align='left' valign='top' width='40%' style='font-family:Arial; font-size:12px; color:#231f20; padding: 10px; border: 1px solid #dddddd; border-bottom: 2px solid #dddddd;'>" + UppercaseFirst(lobjListOfPassengerDetails[k].LastName) + " " + UppercaseFirst(lobjListOfPassengerDetails[k].FirstName) + "</td>";
                                        strPaxInfo += "<td align='left' valign='top' width='22%' style='font-family:Arial; font-size:12px; color:#231f20; padding: 10px; border: 1px solid #dddddd; border-bottom: 2px solid #dddddd;'>" + lobjListOfPassengerDetails[k].TicketNo + " </td>";
                                        strPaxInfo += "<td align='left' valign='top' width='12%' style='font-family:Arial; font-size:12px; color:#231f20; padding: 10px; border: 1px solid #dddddd; border-bottom: 2px solid #dddddd;'>" + lobjListOfPassengerDetails[k].Gender + " </td>";
                                        strPaxInfo += "<td align='left' valign='top' width='7%' style='font-family:Arial; font-size:12px; color:#231f20; padding: 10px; border: 1px solid #dddddd; border-bottom: 2px solid #dddddd;'> " + Convert.ToString(lobjListOfPassengerDetails[k].Age) + " </td>";
                                        strPaxInfo += "</tr>";
                                    }

                                    strPaxInfo+= "</table>";
                                }

                                // Code for Departure table
                                List<FlightSegment> lobjFlightSegmentlst = new List<FlightSegment>();
                                lobjFlightSegmentlst = lobjItineraryDetails.ListOfFlightDetails[0].ListOfFlightSegments;
                                string strDepartute = "";
                                strDepartute += "<tr>";
                                strDepartute += "<td align='left' bgcolor='#E3E3E3' width='10%' style='font-family:Arial; font-size:12px; color:#000000; padding: 10px; border: 1px solid #dddddd; border-bottom: 1px solid #dddddd;'>Flight</td>";
                                strDepartute += "<td align='left' bgcolor='#E3E3E3' valign='top' width='22%' style='font-family:Arial; font-size:12px; color:#000000; padding: 10px; border: 1px solid #dddddd; border-bottom: 1px solid #dddddd;'>Departure</td>";
                                strDepartute += "<td align='left' bgcolor='#E3E3E3' valign='top' width='22%' style='font-family:Arial; font-size:12px; color:#000000; padding: 10px; border: 1px solid #dddddd; border-bottom: 1px solid #dddddd;'>Arrival</td>";
                                strDepartute += "<td align='left' bgcolor='#E3E3E3' valign='top' width='14%' style='font-family:Arial; font-size:12px; color:#000000; padding: 10px; border: 1px solid #dddddd; border-bottom: 1px solid #dddddd;'>Depart Time</td>";
                                strDepartute += "<td align='left' bgcolor='#E3E3E3' valign='top' width='14%' style='font-family:Arial; font-size:12px; color:#000000; padding: 10px; border: 1px solid #dddddd; border-bottom: 1px solid #dddddd;'>Arrial Time</td>";
                                strDepartute += "<td align='left' bgcolor='#E3E3E3' valign='top' width='14%' style='font-family:Arial; font-size:12px; color:#000000; padding: 10px; border: 1px solid #dddddd; border-bottom: 1px solid #dddddd;'>Aircraft Type</td>";
                                strDepartute += "</tr>";
                                for (int i = 0; i < lobjFlightSegmentlst.Count; i++)
                                {
                                    strDepartute += "<tr>";
                                    strDepartute += "<td align='left' width='10%' style='font-family:Arial; font-size:12px; color:#231f20; padding: 10px; border: 1px solid #dddddd; border-bottom: 1px solid #dddddd;'><img width='30' height='30' src='" + lobjFlightSegmentlst[i].Carrier.CarrierLogoPath + "'> <br/>" + lobjFlightSegmentlst[i].Carrier.CarrierName + "<br/>" + lobjFlightSegmentlst[i].Carrier.CarrierCode + lobjFlightSegmentlst[i].FlightNo + "</td>";
                                    strDepartute += "<td align='left' valign='top' width='22%' style='font-family:Arial; font-size:12px; color:#231f20; padding: 10px; border: 1px solid #dddddd; border-bottom: 2px solid #dddddd;'>" + lobjFlightSegmentlst[i].DepartureAirField.City + " (" + lobjFlightSegmentlst[i].DepartureAirField.IATACode + ")" + "<br/>" + lobjFlightSegmentlst[i].DepartureAirField.AirportName + ", " + lobjFlightSegmentlst[i].DepartureAirField.City + "</td>";
                                    strDepartute += "<td align='left' valign='top' width='22%' style='font-family:Arial; font-size:12px; color:#231f20; padding: 10px; border: 1px solid #dddddd; border-bottom: 2px solid #dddddd;'>" + lobjFlightSegmentlst[i].ArrivalAirField.City + " (" + lobjFlightSegmentlst[i].ArrivalAirField.IATACode + ")" + "<br/>" + lobjFlightSegmentlst[i].ArrivalAirField.AirportName + ", " + lobjFlightSegmentlst[i].ArrivalAirField.City + "</td>";
                                    strDepartute += "<td align='left' valign='top' width='14%' style='font-family:Arial; font-size:12px; color:#231f20; padding: 10px; border: 1px solid #dddddd; border-bottom: 2px solid #dddddd;'>" + lobjFlightSegmentlst[i].DepartureDate.ToString("dd/MM/yyyy") + "<br/>" + lobjFlightSegmentlst[i].DepartureDate.ToString("HH:mm") + "</td>";
                                    strDepartute += "<td align='left' valign='top' width='14%' style='font-family:Arial; font-size:12px; color:#231f20; padding: 10px; border: 1px solid #dddddd; border-bottom: 2px solid #dddddd;'>" + lobjFlightSegmentlst[i].ArrivalDate.ToString("dd/MM/yyyy") + "<br/>" + lobjFlightSegmentlst[i].ArrivalDate.ToString("HH:mm") + "</td>";
                                    strDepartute += "<td align='left' valign='top' width='14%' style='font-family:Arial; font-size:12px; color:#231f20; padding: 10px; border: 1px solid #dddddd; border-bottom: 2px solid #dddddd;'>" + lobjFlightSegmentlst[i].Carrier.EquipmentType + "</td>";
                                    strDepartute += "</tr>";
                                    //For AirLine PNR
                                    strDepartute += "<tr><td colspan='6'><table width='100%' border='0' cellpadding='0' cellspacing='0'><tr>";
                                    strDepartute += "<td width='50%' style='font-family:Arial; font-size:12px; color:#231f20; padding: 10px 0px;'> PNR:&nbsp;" + lobjFlightSegmentlst[i].AirlinePNR + "</td>";
                                    strDepartute += "<td width='50%' align='right' style='font-family:Arial; font-size:12px; color:#231f20; padding: 10px 0px; text-align:right'> Baggage Allowance:&nbsp;" + lobjFlightSegmentlst[i].BaggageAllowance + "</td>";
                                    strDepartute += "<tr></table></td></tr>";
                                }

                                string strReturn = "";
                                string strArrival = "";
                                string strClass = (lobjItineraryDetails.CabinType != null && Convert.ToString(lobjItineraryDetails.CabinType) != "") ? Convert.ToString(lobjItineraryDetails.CabinType) : "";
                                if (lobjItineraryDetails.ListOfFlightDetails.Count > 1)
                                {
                                    if (lobjItineraryDetails != null && lobjItineraryDetails.ListOfFlightDetails[1].ListOfFlightSegments != null)
                                    {
                                        // Code for Arrival Table
                                        strReturn += "<tr><td colspan='6'><table width='100%' border='0' cellpadding='0' cellspacing='0'><tr>";
                                        strReturn += "<td width='50%' height='25' valign='top' style='font-family: Arial; font-size: 13px; letter-spacing: normal; line-height: 18px; font-weight: bold; text-align: left; color: #000000; padding: 0px;'>Itinerary Details <span style='color: #231f20;'>(Return)</span></td>";
                                        strReturn += "<td width='50%' height='25' align='right' valign='top' style='font-family: Arial; font-size: 13px; letter-spacing: normal; line-height: 18px; font-weight: bold; text-align: right; color: #000000; padding: 0px;'>Class: <span style='color: #231f20;'>" + strClass + "</span></td>";
                                        strReturn += "<tr></table></td></tr>";
                                        lobjFlightSegmentlst = lobjItineraryDetails.ListOfFlightDetails[1].ListOfFlightSegments;

                                        if (lobjFlightSegmentlst != null && lobjFlightSegmentlst.Count > 0)
                                        {
                                            // Arrival Header Row
                                            strReturn += "<tr>";
                                            strReturn += "<td align='left' bgcolor='#E3E3E3' width='10%' style='font-family:Arial; font-size:12px; color: #000000;padding: 10px; border: 1px solid #dddddd; border-bottom: 1px solid #dddddd;'>Flight</td>";
                                            strReturn += "<td align='left' bgcolor='#E3E3E3' valign='top' width='22%' style='font-family:Arial; font-size:12px; color:#000000; padding: 10px; border: 1px solid #dddddd; border-bottom: 1px solid #dddddd;'>Departure</td>";
                                            strReturn += "<td align='left' bgcolor='#E3E3E3' valign='top' width='22%' style='font-family:Arial; font-size:12px; color:#000000; padding: 10px; border: 1px solid #dddddd; border-bottom: 1px solid #dddddd;'>Arrival</td>";
                                            strReturn += "<td align='left' bgcolor='#E3E3E3' valign='top' width='14%' style='font-family:Arial; font-size:12px; color:#000000; padding: 10px; border: 1px solid #dddddd; border-bottom: 1px solid #dddddd;'>Depart Time</td>";
                                            strReturn += "<td align='left' bgcolor='#E3E3E3' valign='top' width='14%' style='font-family:Arial; font-size:12px; color:#000000; padding: 10px; border: 1px solid #dddddd; border-bottom: 1px solid #dddddd;'>Arrial Time</td>";
                                            strReturn += "<td align='left' bgcolor='#E3E3E3' valign='top' width='14%' style='font-family:Arial; font-size:12px; color:#000000; padding: 10px; border: 1px solid #dddddd; border-bottom: 1px solid #dddddd;'>Aircraft Type</td>";
                                            strReturn += "</tr>";
                                            for (int j = 0; j < lobjFlightSegmentlst.Count; j++)
                                            {
                                                strArrival += "<tr>";
                                                strArrival += "<td align='left' width='10%' style='font-family:Arial; font-size:12px; color:#231f20; padding: 10px; border: 1px solid #dddddd; border-bottom: 1px solid #dddddd;'><img width='30' height='30' src='" + lobjFlightSegmentlst[j].Carrier.CarrierLogoPath + "'> <br/>" + lobjFlightSegmentlst[j].Carrier.CarrierName + "<br/>" + lobjFlightSegmentlst[j].Carrier.CarrierCode + lobjFlightSegmentlst[j].FlightNo + " </td>";
                                                strArrival += "<td align='left' valign='top' width='22%' style='font-family:Arial; font-size:12px; color:#231f20; padding: 10px; border: 1px solid #dddddd; border-bottom: 2px solid #dddddd;'>" + lobjFlightSegmentlst[j].DepartureAirField.City + " (" + lobjFlightSegmentlst[j].DepartureAirField.IATACode + ")" + "<br/>" + lobjFlightSegmentlst[j].DepartureAirField.AirportName + ", " + lobjFlightSegmentlst[j].DepartureAirField.City + "</td>";
                                                strArrival += "<td align='left' valign='top' width='22%' style='font-family:Arial; font-size:12px; color:#231f20; padding: 10px; border: 1px solid #dddddd; border-bottom: 2px solid #dddddd;'>" + lobjFlightSegmentlst[j].ArrivalAirField.City + " (" + lobjFlightSegmentlst[j].ArrivalAirField.IATACode + ")" + "<br/>" + lobjFlightSegmentlst[j].ArrivalAirField.AirportName + ", " + lobjFlightSegmentlst[j].ArrivalAirField.City + "</td>";
                                                strArrival += "<td align='left' valign='top' width='14%' style='font-family:Arial; font-size:12px; color:#231f20; padding: 10px; border: 1px solid #dddddd; border-bottom: 2px solid #dddddd;'>" + lobjFlightSegmentlst[j].DepartureDate.ToString("dd/MM/yyyy") + "<br/>" + lobjFlightSegmentlst[j].DepartureDate.ToString("HH:mm") + "</td>";
                                                strArrival += "<td align='left' valign='top' width='14%' style='font-family:Arial; font-size:12px; color:#231f20; padding: 10px; border: 1px solid #dddddd; border-bottom: 2px solid #dddddd;'>" + lobjFlightSegmentlst[j].ArrivalDate.ToString("dd/MM/yyyy") + "<br/>" + lobjFlightSegmentlst[j].ArrivalDate.ToString("HH:mm") + "</td>";
                                                strArrival += "<td align='left' valign='top' width='14%' style='font-family:Arial; font-size:12px; color:#231f20; padding: 10px; border: 1px solid #dddddd; border-bottom: 2px solid #dddddd;'>" + lobjFlightSegmentlst[j].Carrier.EquipmentType + "</td>";
                                                strArrival += "</tr>";

                                                //For Arrival AirLine PNR
                                                strArrival += "<tr><td colspan='6'><table width='100%' border='0' cellpadding='0' cellspacing='0'><tr>";
                                                strArrival += "<td width='50%' style='font-family:Arial; font-size:12px; color:#231f20; padding: 10px 0px;'> PNR:&nbsp;" + lobjFlightSegmentlst[j].AirlinePNR + "</td>";
                                                strArrival += "<td width='50%' align='right' style='font-family:Arial; font-size:12px; color:#231f20; padding: 10px 0px; text-align:right'> Baggage Allowance:&nbsp;" + lobjFlightSegmentlst[j].BaggageAllowance + "</td>";
                                                strArrival += "<tr></table></td></tr>";
                                            }
                                        }                                       
                                    }
                                }
                                string strGDSPNR = lobjItineraryDetails.PaxPricingInfoList.PaxPricingInfo[0].BookingInfoList.BookingInfo[0].gdspnr;
                                dynamic dynamicCls = new System.Dynamic.ExpandoObject();
                                dynamicCls.event_name = "Flight_Booking_Success";
                                dynamicCls.relation_reference = Convert.ToString(pobjMemberDetails.MemberRelationsList.Find(lobj => lobj.RelationType.Equals(RelationType.LBMS)).RelationReference);
                                dynamicCls.program_id = Convert.ToInt32(pobjMemberDetails.ProgramId); 
                                dynamicCls.to_email = pobjMemberDetails.Email;
                                dynamicCls.full_name = pobjMemberDetails.FullName;
                                dynamicCls.TransactionReferenceCode = lobjItineraryDetails.ItineraryReference;
                                dynamicCls.PaymentDetails = FloatToThousandSeperated(lobjItineraryDetails.FareDetails.TotalPoints)+ " Points";
                                dynamicCls.GDSPNR = strGDSPNR;
                                dynamicCls.TblPassengerInfo = strPaxInfo;
                                dynamicCls.Class = strClass;
                                dynamicCls.TblDeparture = strDepartute;
                                dynamicCls.ReturnFlight = strReturn;
                                dynamicCls.TblArrival = strArrival;
                                dynamicCls.MobileNo = pobjMemberDetails.MobileNumber;
                                dynamicCls.to_mobile = pobjMemberDetails.MobileNumber;
                                dynamicCls.CreditsConsumed = FloatToThousandSeperated(lobjItineraryDetails.FareDetails.TotalPoints);
                                Dictionary<string, dynamic> lobjDictionary = new Dictionary<string, dynamic>();
                                IDictionary<string, object> dict = (IDictionary<string, object>)dynamicCls;
                                foreach (var key in dict)
                                {
                                    lobjDictionary.Add(key.Key, key.Value);
                                }
                                string jsonParameters = JsonConvert.SerializeObject(lobjDictionary);
                                SendEmails(jsonParameters, pobjMemberDetails);

                                //SendEmail(lstEmailparameter, pobjMemberDetails, pobjMemberDetails.PreferredLanguage + "FlightBooked");
                                //Communication Engine Call for Sms Sending
                            }
                            catch (Exception ex)
                            {
                                LoggingAdapter.WriteLog("Communication engine Email exception in booking ", "BookingLogCategory");
                                strFailureType += string.Format("{0}/", FailureType.FAILUREEMAIL);
                            }
                            try
                            {
                                List<string> lstSMSparameter = new List<string>();
                                lstSMSparameter = GenerateFlightSMSParameters(lobjBookingResponse);
                                lstSMSparameter.Add(pobjMemberDetails.LastName);
                                SendSms(lstSMSparameter, pobjMemberDetails, "FlightBooked");
                                LoggingAdapter.WriteLog("Mail and SMS sent.", "BookingLogCategory");
                            }
                            catch (Exception ex)
                            {
                                LoggingAdapter.WriteLog("Communication engine SMS exception in booking", "BookingLogCategory");
                                strFailureType += string.Format("{0}/", FailureType.SMS);
                            }

                            if (strFailureType != "" && strFailureType != string.Empty)
                            {
                                List<string> lstEmailparameter = new List<string>();                               
                                lstEmailparameter = GenerateFlightBookingFailedEmailParameter(lobjBookingResponse, pobjMemberDetails);
                               SendFailureEmail(lstEmailparameter, pobjMemberDetails, pobjMemberDetails.PreferredLanguage + "FlightBookingFailed");
                            }
                        }
                        else
                        {
                            try
                            {
                                if (!IsBookingConfirm)
                                {
                                    for (int i = 0; i < pobjListOfRedemptionDetails.Count; i++)
                                    {
                                        strRedeemMilesResponse = pobjListOfRedemptionDetails[i].TransactionReference;
                                        if (strRedeemMilesResponse != "" && strRedeemMilesResponse != string.Empty)
                                            //lboolRollBackResponse = lobjPGHelper.RollBackTransaction(strRedeemMilesResponse, pobjMemberDetails.MemberRelationsList.Find(lob => lob.RelationType.Equals(RelationType.LBMS)).RelationReference, pobjBookingRequest.ItineraryDetails.OriginLocation + "-" + pobjBookingRequest.ItineraryDetails.DestinationLocation, lstrMerchantId, lstrMerchantUserName, lstrMerchantPassword);
                                            lboolRollBackResponse = RollBackTransaction(strRedeemMilesResponse, pobjMemberDetails.MemberRelationsList.Find(lob => lob.RelationType.Equals(RelationType.LBMS)).RelationReference, pobjBookingRequest.ItineraryDetails.OriginLocation + "-" + pobjBookingRequest.ItineraryDetails.DestinationLocation);
                                    }

                                    LoggingAdapter.WriteLog("RollBackMiles Success : " + lboolRollBackResponse, "BookingLogCategory");
                                }
                                strFailureType += string.Format("{0}/", FailureType.CTRESPONSE);
                                List<string> lstEmailparameter = new List<string>();
                                lstEmailparameter = GenerateFlightBookingFailedEmailParameter(lobjBookingResponse, pobjMemberDetails);
                                SendFailureEmail(lstEmailparameter, pobjMemberDetails, pobjMemberDetails.PreferredLanguage + "FlightBookingFailed");
                            }
                            catch (Exception ex)
                            {
                                LoggingAdapter.WriteLog("Communication engine exception in booking: " + lboolRollBackResponse + " \n Exception \n" + ex.StackTrace, "BookingLogCategory");
                                strFailureType += string.Format("{0}/", FailureType.ROLLBACKMILES);
                                List<string> lstEmailparameter = new List<string>();
                                lstEmailparameter = GenerateFlightBookingFailedEmailParameter(lobjBookingResponse, pobjMemberDetails);
                                SendFailureEmail(lstEmailparameter, pobjMemberDetails, pobjMemberDetails.PreferredLanguage + "FlightBookingFailed");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        try
                        {
                            if (!IsBookingConfirm)
                            {
                                for (int i = 0; i < pobjListOfRedemptionDetails.Count; i++)
                                {
                                    strRedeemMilesResponse = pobjListOfRedemptionDetails[i].TransactionReference;
                                    if (strRedeemMilesResponse != "" && strRedeemMilesResponse != string.Empty)
                                        //lboolRollBackResponse = lobjPGHelper.RollBackTransaction(strRedeemMilesResponse, pobjMemberDetails.MemberRelationsList.Find(lob => lob.RelationType.Equals(RelationType.LBMS)).RelationReference, pobjBookingRequest.ItineraryDetails.OriginLocation + "-" + pobjBookingRequest.ItineraryDetails.DestinationLocation + ", TRIP ID : " + lobjBookingResponse.PNRDetails.TripId + ", GDSPNR : " + lobjBookingResponse.PNRDetails.GDSPNR, lstrMerchantId, lstrMerchantUserName, lstrMerchantPassword);
                                        lboolRollBackResponse = RollBackTransaction(strRedeemMilesResponse, pobjMemberDetails.MemberRelationsList.Find(lob => lob.RelationType.Equals(RelationType.LBMS)).RelationReference, pobjBookingRequest.ItineraryDetails.OriginLocation + "-" + pobjBookingRequest.ItineraryDetails.DestinationLocation);
                                }
                                LoggingAdapter.WriteLog("RollBackMiles Success On Exception : " + lboolRollBackResponse + " \n Exception \n" + ex.StackTrace, "BookingLogCategory");
                            }
                            strFailureType += string.Format("{0}/", FailureType.CTRESPONSE);
                            List<string> lstEmailparameter = new List<string>();
                            lstEmailparameter = GenerateFlightBookingFailedEmailParameter(lobjBookingResponse, pobjMemberDetails);
                            SendFailureEmail(lstEmailparameter, pobjMemberDetails, pobjMemberDetails.PreferredLanguage + "FlightBookingFailed");
                        }
                        catch (Exception ex1)
                        {
                            LoggingAdapter.WriteLog("Communication engine exception in booking: " + lboolRollBackResponse, "BookingLogCategory");
                            strFailureType += string.Format("{0}/", FailureType.ROLLBACKMILES);
                            List<string> lstEmailparameter = new List<string>();
                            lstEmailparameter = GenerateFlightBookingFailedEmailParameter(lobjBookingResponse, pobjMemberDetails);
                            SendFailureEmail(lstEmailparameter, pobjMemberDetails, pobjMemberDetails.PreferredLanguage + "FlightBookingFailed");
                        }
                    }
                }
                else
                {
                    if (!IsBookingConfirm)
                    {
                        for (int i = 0; i < pobjListOfRedemptionDetails.Count; i++)
                        {
                            strRedeemMilesResponse = pobjListOfRedemptionDetails[i].TransactionReference;
                            if (strRedeemMilesResponse != "" && strRedeemMilesResponse != string.Empty)
                                //lboolRollBackResponse = lobjPGHelper.RollBackTransaction(strRedeemMilesResponse, pobjMemberDetails.MemberRelationsList.Find(lob => lob.RelationType.Equals(RelationType.LBMS)).RelationReference, pobjBookingRequest.ItineraryDetails.OriginLocation + "-" + pobjBookingRequest.ItineraryDetails.DestinationLocation, lstrMerchantId, lstrMerchantUserName, lstrMerchantPassword);
                                lboolRollBackResponse = RollBackTransaction(strRedeemMilesResponse, pobjMemberDetails.MemberRelationsList.Find(lob => lob.RelationType.Equals(RelationType.LBMS)).RelationReference, pobjBookingRequest.ItineraryDetails.OriginLocation + "-" + pobjBookingRequest.ItineraryDetails.DestinationLocation);
                        }
                    }

                    strFailureType += string.Format("{0}/", FailureType.REDEEMMILES);
                    List<string> lstEmailparameter = new List<string>();
                    lstEmailparameter = GenerateFlightBookingFailedEmailParameter(lobjBookingResponse, pobjMemberDetails);
                    SendFailureEmail(lstEmailparameter, pobjMemberDetails, pobjMemberDetails.PreferredLanguage + "FlightBookingFailed");
                }
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog(ex.StackTrace);
            }

            return lobjBookingResponse;
        }

        //private List<string> GenerateFlightEmailParameters(BookingResponse pobjBookingResponse)
        //{
        //    List<string> lstEmailparameter = new List<string>();
        //    lstEmailparameter.Add(Convert.ToString(pobjBookingResponse.BookingId));
        //    lstEmailparameter.Add("FlightBooked");
        //    lstEmailparameter.Add(Convert.ToString(pobjBookingResponse.PNRDetails.IsItineraryDateChange));
        //    lstEmailparameter.Add(GetAppSettingValue("CEReceiptPOSTURL"));
        //    return lstEmailparameter;
        //}

        private List<string> GenerateFlightBookingFailedEmailParameter(BookingResponse pobjBookingResponse, MemberDetails pobjMemberDetails)
        {
            List<string> lstEmailparameter = new List<string>();
            //0
            lstEmailparameter.Add((pobjMemberDetails.MemberRelationsList.Find(lobj => lobj.RelationType.Equals(RelationType.LBMS)).RelationReference));
            //1
            lstEmailparameter.Add(pobjMemberDetails.FirstName + " " + pobjMemberDetails.LastName);
            //2
            lstEmailparameter.Add(Convert.ToString(pobjBookingResponse.PNRDetails.ItineraryDetails.FareDetails.TotalPoints));
            //3
            lstEmailparameter.Add(pobjMemberDetails.MobileNumber);
            //4
            lstEmailparameter.Add(pobjBookingResponse.PNRDetails.ItineraryDetails.CabinType);

            // Code for Departure table
            List<FlightSegment> lobjFlightSegmentlst = new List<FlightSegment>();
            lobjFlightSegmentlst = pobjBookingResponse.PNRDetails.ItineraryDetails.ListOfFlightDetails[0].ListOfFlightSegments;
            string strDepartute = string.Empty;
            string strImagePath = GetAppSettingValue("ClientImageUrl");

            for (int i = 0; i < lobjFlightSegmentlst.Count; i++)
            {
                strDepartute += "<tr style='font-family:Arial; font-size:14px; color:#000000;' >";
                strDepartute += "<td width='20' bgcolor='#ffffff' >&nbsp;</td>";
                strDepartute += "<td width='5' bgcolor='#ffffff'>&nbsp;</td>";
                strDepartute += "<td bgcolor='#ffffff'  align='center'  ><img width='30' height='30'  src='" + strImagePath + lobjFlightSegmentlst[i].Carrier.CarrierLogoPath + "'> <br/>" + lobjFlightSegmentlst[i].Carrier.CarrierName + " <br/>" + lobjFlightSegmentlst[i].Carrier.CarrierCode + lobjFlightSegmentlst[i].FlightNo + "</td>";
                strDepartute += "<td bgcolor='#ffffff' style='font-family:Arial; font-size:14px; color:#000000;'>" + lobjFlightSegmentlst[i].DepartureAirField.City + " (" + lobjFlightSegmentlst[i].DepartureAirField.IATACode + ")" + "<br/>" + lobjFlightSegmentlst[i].DepartureAirField.AirportName + ", " + lobjFlightSegmentlst[i].DepartureAirField.City + "</td>";
                strDepartute += "<td bgcolor='#ffffff' style='font-family:Arial; font-size:14px; color:#000000;'>" + lobjFlightSegmentlst[i].ArrivalAirField.City + " (" + lobjFlightSegmentlst[i].ArrivalAirField.IATACode + ")" + "<br/>" + lobjFlightSegmentlst[i].ArrivalAirField.AirportName + ", " + lobjFlightSegmentlst[i].ArrivalAirField.City + "</td>";
                strDepartute += "<td bgcolor='#ffffff' style='font-family:Arial; font-size:14px; color:#000000;'>" + lobjFlightSegmentlst[i].DepartureDate.ToString("dd/MM/yyyy") + "<br/>" + lobjFlightSegmentlst[i].DepartureDate.ToString("HH:mm") + "</td>";
                strDepartute += "<td bgcolor='#ffffff' style='font-family:Arial; font-size:14px; color:#000000;'>" + lobjFlightSegmentlst[i].ArrivalDate.ToString("dd/MM/yyyy") + "<br/>" + lobjFlightSegmentlst[i].ArrivalDate.ToString("HH:mm") + "</td>";
                strDepartute += "<td bgcolor='#ffffff' style='font-family:Arial; font-size:14px; color:#000000;'>" + lobjFlightSegmentlst[i].Carrier.EquipmentType + "</td>";
                strDepartute += "</tr>";


            }
            //5
            lstEmailparameter.Add(strDepartute);

            if (pobjBookingResponse.PNRDetails.ItineraryDetails.ListOfFlightDetails.Count > 1)
            {
                if (pobjBookingResponse.PNRDetails.ItineraryDetails != null && pobjBookingResponse.PNRDetails.ItineraryDetails.ListOfFlightDetails[1].ListOfFlightSegments != null)
                {
                    // Code for Arrival Table
                    string strReturn = string.Empty;
                    strReturn = "<tr> <td colspan='8' bgcolor='#CCCCCC' style='font-family: Arial; font-size: 14px;color: #000000;'>";
                    strReturn += "<table width='720' border='0' cellpadding='0' cellspacing='0' style='font-family: Arial;font-size: 14px; color: #000000;'>";
                    strReturn += "<tr> <td width='20' bgcolor='#CCCCC' >&nbsp;</td>";
                    strReturn += "<td width='5' bgcolor='#CCCCCC'> &nbsp;</td>";
                    strReturn += "<td width='400' colspan='8'  height='25' bgcolor='#CCCCCC' style='font-family:Arial; font-size:14px; color:#000000;font-weight: bold;'> Itinerary Details (Return) </td>";
                    strReturn += "<td align='left' width='80' bgcolor='#CCCCCC' style='font-family:Arial; font-size:14px; color:#000000;'>Class :</td>";
                    strReturn += "<td align='left' width='150'  bgcolor='#CCCCCC' style='font-family:Arial; font-size:14px; color:#000000;'>" + pobjBookingResponse.PNRDetails.ItineraryDetails.CabinType + "</td>";
                    strReturn += "</tr></table>";
                    strReturn += "</tr>";
                    //6
                    lstEmailparameter.Add(strReturn);
                    lobjFlightSegmentlst = pobjBookingResponse.PNRDetails.ItineraryDetails.ListOfFlightDetails[1].ListOfFlightSegments;

                    if (lobjFlightSegmentlst != null && lobjFlightSegmentlst.Count > 0)
                    {
                        // Arrival Header Row
                        string strArrival = string.Empty;
                        strArrival += "<tr height='25'>";
                        strArrival += "<td width='20' bgcolor='#CCCCCC' >&nbsp;</td>";
                        strArrival += "<td width='11' bgcolor='#CCCCCC' >&nbsp;</td>";
                        strArrival += "<td width='25' bgcolor='#CCCCCC' style='font-family:Arial; font-size:14px; color:#000000;'>Flight</td>";
                        strArrival += "<td width='172' bgcolor='#CCCCCC' style='font-family:Arial; font-size:14px; color:#000000;'>Departure</td>";
                        strArrival += "<td width='192' bgcolor='#CCCCCC' style='font-family:Arial; font-size:14px; color:#000000;'> Arrival </td>";
                        strArrival += "<td width='122' bgcolor='#CCCCCC' style='font-family:Arial; font-size:14px; color:#000000;'> Departure Time </td>";
                        strArrival += "<td width='90' bgcolor='#CCCCCC' style='font-family:Arial; font-size:14px; color:#000000;'> Arrival Time </td>";
                        strArrival += "<td width='90' bgcolor='#CCCCCC' style='font-family:Arial; font-size:14px; color:#000000;'>Aircraft Type</td>";
                        strArrival += "	</tr>";

                        for (int j = 0; j < lobjFlightSegmentlst.Count; j++)
                        {
                            strArrival += "<tr style='font-family:Arial; font-size:14px; color:#000000;' >";
                            strArrival += "<td width='20' bgcolor='#ffffff' >&nbsp;</td>";
                            strArrival += "<td width='11' bgcolor='#ffffff'>&nbsp;</td>";
                            strArrival += "<td width='25' bgcolor='#ffffff'  align='center'><img width='30' height='30' src='" + strImagePath + lobjFlightSegmentlst[j].Carrier.CarrierLogoPath + "'> <br/>" + lobjFlightSegmentlst[j].Carrier.CarrierName + "<br/>" + lobjFlightSegmentlst[j].Carrier.CarrierCode + lobjFlightSegmentlst[j].FlightNo + "</td>";
                            strArrival += "<td width='172' bgcolor='#ffffff' style='font-family:Arial; font-size:14px; color:#000000;'>" + lobjFlightSegmentlst[j].DepartureAirField.City + " (" + lobjFlightSegmentlst[j].DepartureAirField.IATACode + ")" + "<br/>" + lobjFlightSegmentlst[j].DepartureAirField.AirportName + ", " + lobjFlightSegmentlst[j].DepartureAirField.City + "</td>";
                            strArrival += "<td width='192' bgcolor='#ffffff' style='font-family:Arial; font-size:14px; color:#000000;'>" + lobjFlightSegmentlst[j].ArrivalAirField.City + " (" + lobjFlightSegmentlst[j].ArrivalAirField.IATACode + ")" + "<br/>" + lobjFlightSegmentlst[j].ArrivalAirField.AirportName + ", " + lobjFlightSegmentlst[j].ArrivalAirField.City + "</td>";
                            strArrival += "<td width='122' bgcolor='#ffffff' style='font-family:Arial; font-size:14px; color:#000000;'>" + lobjFlightSegmentlst[j].DepartureDate.ToString("dd/MM/yyyy") + "<br/>" + lobjFlightSegmentlst[j].DepartureDate.ToString("HH:mm") + "</td>";
                            strArrival += "<td width='90' bgcolor='#ffffff' style='font-family:Arial; font-size:14px; color:#000000;'>" + lobjFlightSegmentlst[j].ArrivalDate.ToString("dd/MM/yyyy") + "<br/>" + lobjFlightSegmentlst[j].ArrivalDate.ToString("HH:mm") + "</td>";
                            strArrival += "<td  width='90' bgcolor='#ffffff' style='font-family:Arial; font-size:14px; color:#000000;'>" + lobjFlightSegmentlst[j].Carrier.EquipmentType + "</td>";
                            strArrival += "</tr>";

                        }
                        //7
                        lstEmailparameter.Add(strArrival);
                        //8
                        lstEmailparameter.Add("");
                    }
                }
            }
            else
            {
                //6
                lstEmailparameter.Add("");
                //7
                lstEmailparameter.Add("");
                //8
                lstEmailparameter.Add("");

            }
            // Code For Travellor Info

            List<PassengerDetails> lobjListOfPassengerDetails = new List<PassengerDetails>();
            if (pobjBookingResponse.PNRDetails.ItineraryDetails.TravelerInfo != null)
            {
                string lstrPaxtype = "";
                string strPaxInfo = string.Empty;
                lobjListOfPassengerDetails = pobjBookingResponse.PNRDetails.ItineraryDetails.TravelerInfo;
                for (int k = 0; k < lobjListOfPassengerDetails.Count; k++)
                {

                    if (Convert.ToString(lobjListOfPassengerDetails[k].PaxType) == "ADT")
                    {
                        lstrPaxtype = "Adult";
                    }
                    else if (Convert.ToString(lobjListOfPassengerDetails[k].PaxType) == "CHD")
                    {
                        lstrPaxtype = "Child";
                    }
                    else if (Convert.ToString(lobjListOfPassengerDetails[k].PaxType) == "INF")
                    {
                        lstrPaxtype = "Infant";
                    }

                    strPaxInfo += "<tr>";
                    strPaxInfo += " <td width='20' bgcolor='#ffffff' >&nbsp;</td>";
                    strPaxInfo += " <td width='11' bgcolor='#ffffff'>&nbsp;</td>";
                    strPaxInfo += "<td align='left'  width='100' bgcolor='#ffffff'> " + lstrPaxtype + " </td>";
                    strPaxInfo += "<td align='left' width='280' bgcolor='#ffffff'>" + lobjListOfPassengerDetails[k].LastName + " " + lobjListOfPassengerDetails[k].FirstName + "</td>";
                    strPaxInfo += "<td align='left'  width='145'  bgcolor='#ffffff'>" + lobjListOfPassengerDetails[k].TicketNo + " </td>";
                    strPaxInfo += "<td align='left' width='73'  bgcolor='#ffffff'>" + lobjListOfPassengerDetails[k].Gender + " </td>";
                    strPaxInfo += "<td width='73'  bgcolor='#ffffff'> " + Convert.ToString(lobjListOfPassengerDetails[k].Age) + " </td>";
                    strPaxInfo += "</tr>";
                }
                //9
                lstEmailparameter.Add(strPaxInfo);

            }
            //10
            lstEmailparameter.Add(pobjBookingResponse.PNRDetails.BookingReference);

            return lstEmailparameter;
        }

        private List<string> GenerateFlightSMSParameters(BookingResponse pobjBookingResponse)
        {
            List<string> lstSmsparameter = new List<string>();

            lstSmsparameter.Add(Convert.ToString(pobjBookingResponse.PNRDetails.BookingReference));

            return lstSmsparameter;
        }

        private List<string> GenerateFlightEmailParameters(BookingResponse pobjBookingResponse)
        {
            List<string> lstEmailparameter = new List<string>();
            lstEmailparameter.Add(Convert.ToString(pobjBookingResponse.BookingId));
            lstEmailparameter.Add("FlightBooked");
            lstEmailparameter.Add(Convert.ToString(pobjBookingResponse.PNRDetails.IsItineraryDateChange));
            return lstEmailparameter;
        }
        //Added For Infipay
        public BookingResponse BookForFlight(BookingRequest pobjBookingRequest, MemberDetails pobjMemberDetails)
        {
            bool IsBookingConfirm = true;
            string strFailureType = "";

            BookingResponse lobjBookingResponse = new BookingResponse();

            try
            {
                IBECTClient lobjIBECTClient = new IBECTClient();

                try
                {
                    lobjBookingResponse = lobjIBECTClient.CreateBooking(pobjBookingRequest);
                }
                catch (Exception ex)
                {
                    LoggingAdapter.WriteLog("CreateBooking Failure BookingFacade:" + ex.Message + ex.StackTrace);
                }

                if (lobjBookingResponse != null && lobjBookingResponse.PNRDetails.TripId != null && lobjBookingResponse.PNRDetails.TripId != string.Empty && lobjBookingResponse.PNRDetails.Status.Equals(1))
                {
                    IsBookingConfirm = true;
                    LoggingAdapter.WriteLog("Booking called Trip Id  : " + lobjBookingResponse.PNRDetails.TripId + "IsBookingConfirm:" + IsBookingConfirm, "BookingLogCategory");
                }
                else
                {
                    IsBookingConfirm = false;
                    LoggingAdapter.WriteLog("IsBookingConfirm:" + IsBookingConfirm, "BookingLogCategory");
                }
                if (IsBookingConfirm)
                {

                    try
                    {
                        //communication Engine call for Email send
                        List<string> lstEmailparameter = new List<string>();
                        lstEmailparameter = GenerateFlightEmailParameters(lobjBookingResponse);
                        SendEmail(lstEmailparameter, pobjMemberDetails, pobjMemberDetails.PreferredLanguage + "FlightBooked");
                        //Communication Engine Call for Sms Sending
                    }
                    catch (Exception ex)
                    {
                        strFailureType += string.Format("{0}/", FailureType.FAILUREEMAIL);
                    }
                    try
                    {
                        List<string> lstSMSparameter = new List<string>();
                        lstSMSparameter = GenerateFlightSMSParameters(lobjBookingResponse);
                        SendSms(lstSMSparameter, pobjMemberDetails, "FlightBooked");

                    }
                    catch (Exception ex)
                    {
                        strFailureType += string.Format("{0}/", FailureType.SMS);
                    }

                    if (strFailureType != "" && strFailureType != string.Empty)
                    {
                        List<string> lstEmailparameter = new List<string>();
                        lstEmailparameter = GenerateFlightBookingFailedEmailParameter(lobjBookingResponse, pobjMemberDetails);
                        SendFailureEmail(lstEmailparameter, pobjMemberDetails, pobjMemberDetails.PreferredLanguage + "FlightBookingFailed");
                    }
                }
                else
                {
                    try
                    {

                        strFailureType += string.Format("{0}/", FailureType.CTRESPONSE);
                        List<string> lstEmailparameter = new List<string>();
                        lstEmailparameter = GenerateFlightBookingFailedEmailParameter(lobjBookingResponse, pobjMemberDetails);
                        SendFailureEmail(lstEmailparameter, pobjMemberDetails, pobjMemberDetails.PreferredLanguage + "FlightBookingFailed");
                    }
                    catch (Exception ex)
                    {


                        List<string> lstEmailparameter = new List<string>();
                        lstEmailparameter = GenerateFlightBookingFailedEmailParameter(lobjBookingResponse, pobjMemberDetails);
                        SendFailureEmail(lstEmailparameter, pobjMemberDetails, pobjMemberDetails.PreferredLanguage + "FlightBookingFailed");
                    }
                }
            }
            catch (Exception ex)
            {
                try
                {


                    strFailureType += string.Format("{0}/", FailureType.CTRESPONSE);
                    List<string> lstEmailparameter = new List<string>();
                    lstEmailparameter = GenerateFlightBookingFailedEmailParameter(lobjBookingResponse, pobjMemberDetails);
                    SendFailureEmail(lstEmailparameter, pobjMemberDetails, pobjMemberDetails.PreferredLanguage + "FlightBookingFailed");
                }
                catch (Exception ex1)
                {


                    List<string> lstEmailparameter = new List<string>();
                    lstEmailparameter = GenerateFlightBookingFailedEmailParameter(lobjBookingResponse, pobjMemberDetails);
                    SendFailureEmail(lstEmailparameter, pobjMemberDetails, pobjMemberDetails.PreferredLanguage + "FlightBookingFailed");
                }
            }



            return lobjBookingResponse;
        }

        #endregion

        #region Hotel

        public HotelBookingResponse BookHotel(HotelBookingRequest pobjBookingRequest, Hotel pobjHotel, HotelSearchRequest pobjHotelSearchRequest, MemberDetails pobjMemberDetails, Customer pobjCustomer, List<RedemptionDetails> pobjListOfRedemptionDetails)
        {
            bool IsBookingConfirm = false;
            string strRedeemMilesResponse = string.Empty;
            bool lboolRollBackResponse = false;
            string strRollBackMilesResponse = string.Empty;

            string strFailureType = "";

            HotelBookingResponse lobjBookingResponse = new HotelBookingResponse();
            PGHelper lobjPGHelper = new PGHelper();

            try
            {
                bool IsRedeem = true;
                List<BookingPaymentBreakage> lobjListOfBookingPaymentBreakage = new List<BookingPaymentBreakage>();
                for (int i = 0; i < pobjListOfRedemptionDetails.Count; i++)
                {
                    //strRedeemMilesResponse = lobjPGHelper.DoOtherRedemption((float)(pobjListOfRedemptionDetails[i].Amount), (float)(pobjListOfRedemptionDetails[i].Amount), pobjListOfRedemptionDetails[i].Points, string.Empty, string.Empty, pobjMemberDetails.MemberRelationsList.Find(lobj => lobj.RelationType.Equals(RelationType.LBMS)).RelationReference, pobjMemberDetails.MemberRelationsList.Find(lobj => lobj.RelationType.Equals(RelationType.LBMS)).WebPassword, pobjHotel.basicinfo.hotelname, Convert.ToInt32(RelationType.LBMS), Convert.ToInt32(LoyaltyTxnType.Hotel), pobjListOfRedemptionDetails[i].Currency, lstrMerchantId, lstrMerchantUserName, lstrMerchantPassword);

                    strRedeemMilesResponse = RedeemPoints((float)(pobjListOfRedemptionDetails[i].Amount), pobjListOfRedemptionDetails[i].Points, pobjMemberDetails.MemberRelationsList.Find(lobj => lobj.RelationType.Equals(RelationType.LBMS)).RelationReference, pobjMemberDetails.MemberRelationsList.Find(lobj => lobj.RelationType.Equals(RelationType.LBMS)).WebPassword, pobjHotel.basicinfo.hotelname, Convert.ToInt32(LoyaltyTxnType.Hotel), pobjListOfRedemptionDetails[i].Currency, pobjMemberDetails);

                    pobjListOfRedemptionDetails[i].TransactionReference = strRedeemMilesResponse;
                    IsRedeem = IsRedeem && (pobjListOfRedemptionDetails[i].TransactionReference != "" && pobjListOfRedemptionDetails[i].TransactionReference != string.Empty);
                    BookingPaymentBreakage lobjBookingPaymentBreakage = new BookingPaymentBreakage();
                    lobjBookingPaymentBreakage.Currency = pobjListOfRedemptionDetails[i].Currency;
                    lobjBookingPaymentBreakage.Amount = pobjListOfRedemptionDetails[i].Points;
                    lobjBookingPaymentBreakage.TxnReference = strRedeemMilesResponse;
                    lobjListOfBookingPaymentBreakage.Add(lobjBookingPaymentBreakage);
                }

                IBECTHotelClient lobjIBECTClient = new IBECTHotelClient();

                BookingPaymentDetails lobjBookingPaymentDetails = new BookingPaymentDetails();
                lobjBookingPaymentDetails.BookingPaymentBreakageList = lobjListOfBookingPaymentBreakage;
                lobjBookingPaymentDetails.MemberId = pobjMemberDetails.MemberRelationsList[0].RelationReference;
                lobjBookingPaymentDetails.PaymentType = PaymentType.Points;
                lobjBookingPaymentDetails.ServiceType = ServiceType.HOTEL;
                lobjBookingPaymentDetails.Points = pobjListOfRedemptionDetails[0].Points;
                lobjBookingPaymentDetails.PointsTxnRefererence = pobjListOfRedemptionDetails[0].TransactionReference;
                lobjBookingPaymentDetails.PaymentStatus = PaymentStatus.Success;
                pobjBookingRequest.BookingPaymentDetails = lobjBookingPaymentDetails;

                if (IsRedeem)
                {
                    try
                    {
                        LoggingAdapter.WriteLog("Hotel RedeemMiles Success : " + strRedeemMilesResponse, "HotelBookingLogCategory");

                        try
                        {
                            lobjBookingResponse = lobjIBECTClient.GetHotelBookingResponse(pobjBookingRequest, pobjHotel, pobjHotelSearchRequest, pobjCustomer, pobjListOfRedemptionDetails[0].TransactionReference);
                        }
                        catch (Exception ex)
                        {
                            LoggingAdapter.WriteLog("Hotel GetHotelBookingResponse in BookingFacade Failure" + ex.Message + ex.StackTrace);
                        }

                        if (lobjBookingResponse != null && lobjBookingResponse.BookingResponse.bookingid != null && lobjBookingResponse.BookingResponse.bookingid != string.Empty)
                        {
                            LoggingAdapter.WriteLog("Booking called Booking Id  : " + lobjBookingResponse.BookingResponse.bookingid, "HotelBookingLogCategory");
                            IsBookingConfirm = lobjBookingResponse.BookingResponse.confirmationnumber != "" && lobjBookingResponse.BookingResponse.confirmationnumber != null && lobjBookingResponse.BookingResponse.bookingid != "" && lobjBookingResponse.BookingResponse.confirmationnumber != null;
                        }
                        else
                            IsBookingConfirm = false;

                        if (IsBookingConfirm)
                        {
                            try
                            {
                                //communication Engine call for Email send
                                //List<string> lstEmailparameter = new List<string>();
                                lobjBookingResponse.BookingPaymentDetails = pobjBookingRequest.BookingPaymentDetails;
                                //lstEmailparameter = GenerateHotelEmailParameters(lobjBookingResponse, pobjHotel, pobjHotelSearchRequest, pobjCustomer, pobjBookingRequest.BookingPaymentDetails.BookingPaymentBreakageList);

                                dynamic dynamicCls = new System.Dynamic.ExpandoObject();
                                dynamicCls.event_name = "Hotel_Booking_Success";
                                dynamicCls.relation_reference = Convert.ToString(pobjMemberDetails.MemberRelationsList.Find(lobj => lobj.RelationType.Equals(RelationType.LBMS)).RelationReference);
                                dynamicCls.program_id = Convert.ToInt32(pobjMemberDetails.ProgramId); 
                                dynamicCls.to_email = pobjMemberDetails.Email;
                                dynamicCls.full_name = pobjMemberDetails.FullName;
                                dynamicCls.vouchernumber = lobjBookingResponse.BookingResponse.confirmationnumber;
                                dynamicCls.BookingRef= lobjBookingResponse.BookingResponse.TransactionRefCode;
                                dynamicCls.BookedBy = pobjCustomer.title + " " + pobjCustomer.firstname + " " + pobjCustomer.lastname;
                                dynamicCls.email = pobjCustomer.email;
                                dynamicCls.address=pobjCustomer.city;
                                dynamicCls.NoOfRooms=Convert.ToString(pobjHotelSearchRequest.SearchRequest.NoOfRooms);
                                dynamicCls.NoOfDays=Convert.ToString((pobjHotelSearchRequest.SearchRequest.CheckOutDate - pobjHotelSearchRequest.SearchRequest.CheckInDate).Days);
                                dynamicCls.CheckInDate=pobjHotelSearchRequest.SearchRequest.CheckInDate.ToString("dd/MM/yyyy");
                                dynamicCls.CheckOutDate=pobjHotelSearchRequest.SearchRequest.CheckOutDate.ToString("dd/MM/yyyy");
                                dynamicCls.hotelname = pobjHotel.basicinfo.hotelname;
                                dynamicCls.hoteladdress = pobjHotel.basicinfo.address;
                                dynamicCls.phone = pobjHotel.basicinfo.communicationinfo.phone;
                                dynamicCls.fax = pobjHotel.basicinfo.communicationinfo.fax;
                                dynamicCls.starrating = pobjHotel.basicinfo.starrating;
                                dynamicCls.TotalPoints= FloatToThousandSeperated(pobjHotel.roomrates.RoomRate[0].TotalPoints) +" Points";
                                dynamicCls.roomdescription=pobjHotel.roomrates.RoomRate[0].roomtype.roomdescription;
                                dynamicCls.SpecialRequest=pobjHotel.SpecialRequest;
                                dynamicCls.to_mobile = pobjMemberDetails.MobileNumber;
                                dynamicCls.CreditsConsumed = FloatToThousandSeperated(pobjHotel.roomrates.RoomRate[0].TotalPoints);
                                Dictionary<string, dynamic> lobjDictionary = new Dictionary<string, dynamic>();
                                IDictionary<string, object> dict = (IDictionary<string, object>)dynamicCls;
                                foreach (var key in dict)
                                {
                                    lobjDictionary.Add(key.Key, key.Value);
                                }
                                string jsonParameters = JsonConvert.SerializeObject(lobjDictionary);
                                SendEmails(jsonParameters, pobjMemberDetails);
                               // SendEmail(lstEmailparameter, pobjMemberDetails, pobjMemberDetails.PreferredLanguage + "HotelBooked");
                                LoggingAdapter.WriteLog("Mail sent. ", "HotelBookingLogCategory");
                            }
                            catch (Exception ex)
                            {
                                LoggingAdapter.WriteLog("Communication engine exception in booking ", "HotelBookingLogCategory");
                                strFailureType += string.Format("{0}/", FailureType.FAILUREEMAIL);
                            }
                            try
                            {
                                //Communication Engine Call for Sms Sending
                                List<string> lstSMSparameter = new List<string>();
                                lstSMSparameter = GenerateHotelSMSParameters(lobjBookingResponse);
                                lstSMSparameter.Add(pobjMemberDetails.LastName);
                                SendSms(lstSMSparameter, pobjMemberDetails, "HotelBooked");
                                LoggingAdapter.WriteLog("SMS sent.", "HotelBookingLogCategory");
                            }
                            catch (Exception ex)
                            {
                                LoggingAdapter.WriteLog("Communication engine exception in booking ", "HotelBookingLogCategory");
                                strFailureType += string.Format("{0}/", FailureType.SMS);
                            }

                            if (strFailureType != "" && strFailureType != string.Empty)
                            {
                                //List<string> lstEmailparameter = new List<string>();
                               GenerateHotelBookingFailedEmailParameters(lobjBookingResponse, pobjHotel, pobjMemberDetails, pobjHotelSearchRequest, pobjCustomer, strFailureType);
                                //SendFailureEmail(lstEmailparameter, pobjMemberDetails, pobjMemberDetails.PreferredLanguage + "HotelBookingFailed");                              
                            }
                        }
                        else
                        {
                            try
                            {

                                for (int i = 0; i < pobjListOfRedemptionDetails.Count; i++)
                                {
                                    strRedeemMilesResponse = pobjListOfRedemptionDetails[i].TransactionReference;
                                    if (strRedeemMilesResponse != "" && strRedeemMilesResponse != string.Empty)
                                        //lboolRollBackResponse = lobjPGHelper.RollBackTransaction(strRedeemMilesResponse, pobjMemberDetails.MemberRelationsList.Find(lob => lob.RelationType.Equals(RelationType.LBMS)).RelationReference, pobjHotel.basicinfo.hotelname, lstrMerchantId, lstrMerchantUserName, lstrMerchantPassword);
                                        lboolRollBackResponse = RollBackTransaction(strRedeemMilesResponse, pobjMemberDetails.MemberRelationsList.Find(lob => lob.RelationType.Equals(RelationType.LBMS)).RelationReference, pobjHotel.basicinfo.hotelname);
                                }

                                LoggingAdapter.WriteLog("RollBackMiles Success : " + lboolRollBackResponse, "HotelBookingLogCategory");
                                strFailureType += string.Format("{0}/", FailureType.CTRESPONSE);
                                //List<string> lstEmailparameter = new List<string>();
                                GenerateHotelBookingFailedEmailParameters(lobjBookingResponse, pobjHotel, pobjMemberDetails, pobjHotelSearchRequest, pobjCustomer, strFailureType);
                                //SendEmail(lstEmailparameter, pobjMemberDetails, "HotelBookingFailed");
                                //SendFailureEmail(lstEmailparameter, pobjMemberDetails, pobjMemberDetails.PreferredLanguage + "HotelBookingFailed");
                            }
                            catch (Exception ex)
                            {
                                LoggingAdapter.WriteLog("RollBackMiles Exception : " + lboolRollBackResponse + " \n Exception \n" + ex.StackTrace, "HotelBookingLogCategory");
                                strFailureType += string.Format("{0}/", FailureType.ROLLBACKMILES);
                                //List<string> lstEmailparameter = new List<string>();
                                GenerateHotelBookingFailedEmailParameters(lobjBookingResponse, pobjHotel, pobjMemberDetails, pobjHotelSearchRequest, pobjCustomer, strFailureType);
                                //SendEmail(lstEmailparameter, pobjMemberDetails, "HotelBookingFailed");
                               // SendFailureEmail(lstEmailparameter, pobjMemberDetails, pobjMemberDetails.PreferredLanguage + "HotelBookingFailed");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        try
                        {
                            if (!IsBookingConfirm)
                            {
                                for (int i = 0; i < pobjListOfRedemptionDetails.Count; i++)
                                {
                                    strRedeemMilesResponse = pobjListOfRedemptionDetails[i].TransactionReference;
                                    if (strRedeemMilesResponse != "" && strRedeemMilesResponse != string.Empty)
                                        //lboolRollBackResponse = lobjPGHelper.RollBackTransaction(strRedeemMilesResponse, pobjMemberDetails.MemberRelationsList.Find(lob => lob.RelationType.Equals(RelationType.LBMS)).RelationReference, pobjHotel.basicinfo.hotelname, lstrMerchantId, lstrMerchantUserName, lstrMerchantPassword);
                                        lboolRollBackResponse = RollBackTransaction(strRedeemMilesResponse, pobjMemberDetails.MemberRelationsList.Find(lob => lob.RelationType.Equals(RelationType.LBMS)).RelationReference, pobjHotel.basicinfo.hotelname);
                                }
                            }

                            LoggingAdapter.WriteLog("RollBackMiles Success On Exception : " + strRollBackMilesResponse + " \n Exception \n" + ex.StackTrace, "HotelBookingLogCategory");
                            strFailureType += string.Format("{0}/", FailureType.CTRESPONSE);
                           // List<string> lstEmailparameter = new List<string>();
                            GenerateHotelBookingFailedEmailParameters(lobjBookingResponse, pobjHotel, pobjMemberDetails, pobjHotelSearchRequest, pobjCustomer, strFailureType);
                            //SendEmail(lstEmailparameter, pobjMemberDetails, "HotelBookingFailed");
                            //SendFailureEmail(lstEmailparameter, pobjMemberDetails, pobjMemberDetails.PreferredLanguage + "HotelBookingFailed");
                        }
                        catch (Exception exp)
                        {
                            LoggingAdapter.WriteLog("RollBackMiles Exception : " + strRollBackMilesResponse + " \n Exception \n" + exp.StackTrace, "HotelBookingLogCategory");
                            strFailureType += string.Format("{0}/", FailureType.ROLLBACKMILES);
                            //List<string> lstEmailparameter = new List<string>();
                            GenerateHotelBookingFailedEmailParameters(lobjBookingResponse, pobjHotel, pobjMemberDetails, pobjHotelSearchRequest, pobjCustomer, strFailureType);
                            //SendEmail(lstEmailparameter, pobjMemberDetails, "HotelBookingFailed");
                            //SendFailureEmail(lstEmailparameter, pobjMemberDetails, pobjMemberDetails.PreferredLanguage + "HotelBookingFailed");
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < pobjListOfRedemptionDetails.Count; i++)
                    {
                        strRedeemMilesResponse = pobjListOfRedemptionDetails[i].TransactionReference;
                        if (strRedeemMilesResponse != "" && strRedeemMilesResponse != string.Empty)
                            //lboolRollBackResponse = lobjPGHelper.RollBackTransaction(strRedeemMilesResponse, pobjMemberDetails.MemberRelationsList.Find(lob => lob.RelationType.Equals(RelationType.LBMS)).RelationReference, pobjHotel.basicinfo.hotelname, lstrMerchantId, lstrMerchantUserName, lstrMerchantPassword);
                            lboolRollBackResponse = RollBackTransaction(strRedeemMilesResponse, pobjMemberDetails.MemberRelationsList.Find(lob => lob.RelationType.Equals(RelationType.LBMS)).RelationReference, pobjHotel.basicinfo.hotelname);
                    }

                    strFailureType += string.Format("{0}/", FailureType.REDEEMMILES);
                    //List<string> lstEmailparameter = new List<string>();
                    GenerateHotelBookingFailedEmailParameters(lobjBookingResponse, pobjHotel, pobjMemberDetails, pobjHotelSearchRequest, pobjCustomer, strFailureType);
                    //SendEmail(lstEmailparameter, pobjMemberDetails, "HotelBookingFailed");
                    //SendFailureEmail(lstEmailparameter, pobjMemberDetails, pobjMemberDetails.PreferredLanguage + "HotelBookingFailed");
                }
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog(ex.StackTrace);
            }

            return lobjBookingResponse;
        }

        private List<string> GenerateHotelEmailParameters(HotelBookingResponse pobjBookingResponse, Hotel pobjHotel, HotelSearchRequest pobjSearchRequest, Customer pobjCustomer, List<BookingPaymentBreakage> pobjListOfBookingPaymentBreakage)
        {
            List<string> lstEmailparameter = new List<string>();

            //0
            lstEmailparameter.Add(pobjBookingResponse.BookingResponse.TransactionRefCode);
            //1
            lstEmailparameter.Add(pobjCustomer.city);
            //2
            lstEmailparameter.Add(pobjCustomer.email);
            //3
            lstEmailparameter.Add(pobjCustomer.title + " " + pobjCustomer.firstname + " " + pobjCustomer.lastname);
            //4
            lstEmailparameter.Add(Convert.ToString(pobjSearchRequest.SearchRequest.NoOfRooms));
            //5
            lstEmailparameter.Add(Convert.ToString((pobjSearchRequest.SearchRequest.CheckOutDate - pobjSearchRequest.SearchRequest.CheckInDate).Days));
            //6
            lstEmailparameter.Add(pobjSearchRequest.SearchRequest.CheckInDate.ToString("dd/MM/yyyy"));
            //7
            lstEmailparameter.Add(pobjSearchRequest.SearchRequest.CheckOutDate.ToString("dd/MM/yyyy")); //
            //8
            lstEmailparameter.Add(Convert.ToString(pobjHotel.roomrates.RoomRate[0].TotalPoints));
            //9
            lstEmailparameter.Add(pobjHotel.basicinfo.hotelname);
            //10
            lstEmailparameter.Add(pobjHotel.basicinfo.address);
            //11
            lstEmailparameter.Add(pobjHotel.basicinfo.communicationinfo.phone);
            //12
            lstEmailparameter.Add(pobjHotel.basicinfo.communicationinfo.fax);
            //13
            lstEmailparameter.Add(pobjHotel.basicinfo.starrating);
            //14
            lstEmailparameter.Add(pobjHotel.otherinfo.locationinfo.latitude);
            //15
            lstEmailparameter.Add(pobjHotel.otherinfo.locationinfo.longitude);
            //16
            lstEmailparameter.Add(pobjHotel.roomrates.RoomRate[0].roomtype.roomdescription);
            //17
            lstEmailparameter.Add(pobjHotel.SpecialRequest);
            //18
            lstEmailparameter.Add(pobjBookingResponse.BookingResponse.confirmationnumber);
            //19
            lstEmailparameter.Add(pobjBookingResponse.BookingResponse.bookingid);
            //20
            string strPaymentDetailsHtml = "<tr><td width='2%'>&nbsp;</td><td width='56%' align='left' valign='top' style='font-family: arial; padding-top: 5px;font-size:12px;'>{0}</td><td width='40%' align='left' valign='middle' style='font-family: arial; font-size: 12px;text-align: left;'>{1}</td><td width='2%'>&nbsp;</td></tr>";
            string strPaymentDetails = string.Empty;

            for (int i = 0; i < pobjListOfBookingPaymentBreakage.Count; i++)
            {
                if (!pobjListOfBookingPaymentBreakage[i].Currency.Equals("LKR"))
                {
                    strPaymentDetails += string.Format(strPaymentDetailsHtml, pobjListOfBookingPaymentBreakage[i].Currency, Convert.ToInt32(pobjListOfBookingPaymentBreakage[i].Amount));
                }
                else
                {
                    strPaymentDetails += string.Format(strPaymentDetailsHtml, pobjListOfBookingPaymentBreakage[i].Currency, pobjListOfBookingPaymentBreakage[i].Amount);
                }
            }

            lstEmailparameter.Add(strPaymentDetails);

            return lstEmailparameter;
        }

        private List<string> GenerateHotelSMSParameters(HotelBookingResponse plobBookingResponse)
        {
            List<string> lstSMSparameter = new List<string>();
            //lstSMSparameter.Add(Convert.ToString(plobBookingResponse.BookingResponse.confirmationnumber));
            lstSMSparameter.Add(Convert.ToString(plobBookingResponse.BookingResponse.TransactionRefCode));
            return lstSMSparameter;
        }

        private void GenerateHotelBookingFailedEmailParameters(HotelBookingResponse pobjBookingResponse, Hotel pobjHotel, MemberDetails pobjMemberDetails, HotelSearchRequest pobjSearchRequest, Customer pobjCustomer, string pstrFailureType)
        {
            dynamic dynamicCls = new System.Dynamic.ExpandoObject();
            dynamicCls.event_name = "Hotel_Booking_Failed";
            dynamicCls.relation_reference = Convert.ToString(pobjMemberDetails.MemberRelationsList.Find(lobj => lobj.RelationType.Equals(RelationType.LBMS)).RelationReference);
            dynamicCls.program_id = Convert.ToInt32(pobjMemberDetails.ProgramId); ;
            dynamicCls.to_email = pobjMemberDetails.Email;
            dynamicCls.full_name = pobjMemberDetails.FullName;
            dynamicCls.Miles = Convert.ToString(pobjHotel.roomrates.RoomRate[0].TotalPoints);
            dynamicCls.MobileNumber = pobjMemberDetails.MobileNumber;
            dynamicCls.TransactionRefCode = "NA";
            dynamicCls.hotelname = pobjHotel.basicinfo.hotelname;
            dynamicCls.city = pobjHotel.basicinfo.city;
            dynamicCls.NoOfRooms = Convert.ToString(pobjSearchRequest.SearchRequest.NoOfRooms);
            dynamicCls.to_mobile = pobjMemberDetails.MobileNumber;
            int TotalAdult = 0;
            string[] arrayAdultPerRoom = pobjSearchRequest.SearchRequest.AdultPerRoom.Split(',');
            for (int i = 0; i < arrayAdultPerRoom.Count(); i++)
            {
                TotalAdult = TotalAdult + Convert.ToInt32(arrayAdultPerRoom[i]);
            }
            dynamicCls.TotalAdult = (Convert.ToString(TotalAdult));
            int TotalChild = 0;
            string[] arrayChildPerRoom = pobjSearchRequest.SearchRequest.ChildrenPerRoom.Split(',');
            for (int i = 0; i < arrayChildPerRoom.Count(); i++)
            {
                TotalChild = TotalChild + Convert.ToInt32(arrayChildPerRoom[i]);
            }
            dynamicCls.TotalChild = Convert.ToString(TotalChild);
            dynamicCls.CheckInDate = pobjSearchRequest.SearchRequest.CheckInDate.ToString("dd/MM/yyyy");
            dynamicCls.CheckOutDate = pobjSearchRequest.SearchRequest.CheckOutDate.ToString("dd/MM/yyyy");
            dynamicCls.CheckOutDate = pobjCustomer.title + pobjCustomer.firstname + " " + pobjCustomer.lastname;
            dynamicCls.address = pobjCustomer.city;
            dynamicCls.country = pobjCustomer.country;
            dynamicCls.postalcode = pobjCustomer.postalcode;
            dynamicCls.Customermobile = pobjCustomer.mobile;
            dynamicCls.Customeremail = pobjCustomer.email;

            dynamicCls.FailureType = pstrFailureType;
            Dictionary<string, dynamic> lobjDictionary = new Dictionary<string, dynamic>();
            IDictionary<string, object> dict = (IDictionary<string, object>)dynamicCls;
            foreach (var key in dict)
            {
                lobjDictionary.Add(key.Key, key.Value);
            }
            string jsonParameters = JsonConvert.SerializeObject(lobjDictionary);
            SendEmails(jsonParameters, pobjMemberDetails);
        }

        private List<string> GenerateHotelBookingFailedEmailParametersInfiPay(HotelBookingResponse pobjBookingResponse, Hotel pobjHotel, MemberDetails pobjMemberDetails, HotelSearchRequest pobjSearchRequest, Customer pobjCustomer, string pstrFailureType)
        {
            List<string> lstEmailparameter = new List<string>();

            //0
            lstEmailparameter.Add((pobjMemberDetails.MemberRelationsList.Find(lobj => lobj.RelationType.Equals(RelationType.LBMS)).RelationReference));
            //1
            lstEmailparameter.Add(pobjMemberDetails.LastName);
            //2
            lstEmailparameter.Add(Convert.ToString(pobjHotel.roomrates.RoomRate[0].TotalPoints));
            //3
            lstEmailparameter.Add(pobjMemberDetails.MobileNumber);
            //4
            lstEmailparameter.Add(pobjHotel.basicinfo.hotelname);
            //5
            lstEmailparameter.Add(pobjHotel.basicinfo.city);
            //6
            lstEmailparameter.Add(Convert.ToString(pobjSearchRequest.SearchRequest.NoOfRooms));
            //7
            int TotalAdult = 0;
            string[] arrayAdultPerRoom = pobjSearchRequest.SearchRequest.AdultPerRoom.Split(',');
            for (int i = 0; i < arrayAdultPerRoom.Count(); i++)
            {
                TotalAdult = TotalAdult + Convert.ToInt32(arrayAdultPerRoom[i]);
            }
            lstEmailparameter.Add(Convert.ToString(TotalAdult));
            //8
            int TotalChild = 0;
            string[] arrayChildPerRoom = pobjSearchRequest.SearchRequest.ChildrenPerRoom.Split(',');
            for (int i = 0; i < arrayChildPerRoom.Count(); i++)
            {
                TotalChild = TotalChild + Convert.ToInt32(arrayChildPerRoom[i]);
            }
            lstEmailparameter.Add(Convert.ToString(TotalChild));
            //9
            lstEmailparameter.Add(pobjSearchRequest.SearchRequest.CheckInDate.ToString("dd/MM/yyyy"));
            //10
            lstEmailparameter.Add(pobjSearchRequest.SearchRequest.CheckOutDate.ToString("dd/MM/yyyy"));
            //11
            lstEmailparameter.Add(pobjCustomer.title + pobjCustomer.firstname + " " + pobjCustomer.lastname);
            //12
            lstEmailparameter.Add(pobjCustomer.city);
            //13
            lstEmailparameter.Add(pobjCustomer.country);
            //14
            lstEmailparameter.Add(pobjCustomer.postalcode);
            //15
            lstEmailparameter.Add(pobjCustomer.mobile);
            //16
            lstEmailparameter.Add(pobjCustomer.email);
            //17
            lstEmailparameter.Add(pobjBookingResponse.BookingResponse.TransactionRefCode);
            //18
            lstEmailparameter.Add(pstrFailureType);

            return lstEmailparameter;
        }


        //Added For InfiPay
        public HotelBookingResponse BookForHotel(HotelBookingRequest pobjBookingRequest, Hotel pobjHotel, HotelSearchRequest pobjHotelSearchRequest, MemberDetails pobjMemberDetails, Customer pobjCustomer, string pstrReferenceId)
        {
            bool IsBookingConfirm = true;
            string strFailureType = "";

            HotelBookingResponse lobjBookingResponse = new HotelBookingResponse();
            try
            {
                IBECTHotelClient lobjIBECTClient = new IBECTHotelClient();

                try
                {
                    lobjBookingResponse = lobjIBECTClient.GetHotelBookingResponse(pobjBookingRequest, pobjHotel, pobjHotelSearchRequest, pobjCustomer, pstrReferenceId);
                }
                catch (Exception ex)
                {
                    LoggingAdapter.WriteLog("Hotel GetHotelBookingResponse in BookingFacade Failure" + ex.Message + ex.StackTrace);
                }

                if (lobjBookingResponse != null && lobjBookingResponse.BookingResponse.bookingid != null && lobjBookingResponse.BookingResponse.bookingid != string.Empty)
                {
                    LoggingAdapter.WriteLog("Booking called Booking Id  : " + lobjBookingResponse.BookingResponse.bookingid, "HotelBookingLogCategory");
                    IsBookingConfirm = lobjBookingResponse.BookingResponse.confirmationnumber != "" && lobjBookingResponse.BookingResponse.confirmationnumber != null && lobjBookingResponse.BookingResponse.bookingid != "" && lobjBookingResponse.BookingResponse.bookingid != null;
                }
                else
                    IsBookingConfirm = false;

                if (IsBookingConfirm)
                {
                    try
                    {
                        //communication Engine call for Email send
                        List<string> lstEmailparameter = new List<string>();
                        lobjBookingResponse.BookingPaymentDetails = pobjBookingRequest.BookingPaymentDetails;
                        lstEmailparameter = GenerateHotelEmailParameters(lobjBookingResponse, pobjHotel, pobjHotelSearchRequest, pobjCustomer, pobjBookingRequest.BookingPaymentDetails.BookingPaymentBreakageList);
                        SendEmail(lstEmailparameter, pobjMemberDetails, "HotelBooked");

                    }
                    catch (Exception ex)
                    {

                        strFailureType += string.Format("{0}/", FailureType.FAILUREEMAIL);
                    }
                    try
                    {
                        //Communication Engine Call for Sms Sending
                        List<string> lstSMSparameter = new List<string>();
                        lstSMSparameter = GenerateHotelSMSParameters(lobjBookingResponse);
                        SendSms(lstSMSparameter, pobjMemberDetails, "HotelBooked");

                    }
                    catch (Exception ex)
                    {

                        strFailureType += string.Format("{0}/", FailureType.SMS);
                    }

                    if (strFailureType != "" && strFailureType != string.Empty)
                    {
                        List<string> lstEmailparameter = new List<string>();
                        lstEmailparameter = GenerateHotelBookingFailedEmailParametersInfiPay(lobjBookingResponse, pobjHotel, pobjMemberDetails, pobjHotelSearchRequest, pobjCustomer, strFailureType);

                        SendFailureEmail(lstEmailparameter, pobjMemberDetails, "HotelBookingFailed");
                    }
                }
                else
                {
                    try
                    {

                        strFailureType += string.Format("{0}/", FailureType.CTRESPONSE);
                        List<string> lstEmailparameter = new List<string>();
                        lstEmailparameter = GenerateHotelBookingFailedEmailParametersInfiPay(lobjBookingResponse, pobjHotel, pobjMemberDetails, pobjHotelSearchRequest, pobjCustomer, strFailureType);

                        SendFailureEmail(lstEmailparameter, pobjMemberDetails, "HotelBookingFailed");
                    }
                    catch (Exception ex)
                    {
                        strFailureType += string.Format("{0}/", FailureType.FAILUREEMAIL);
                        List<string> lstEmailparameter = new List<string>();
                        lstEmailparameter = GenerateHotelBookingFailedEmailParametersInfiPay(lobjBookingResponse, pobjHotel, pobjMemberDetails, pobjHotelSearchRequest, pobjCustomer, strFailureType);

                        SendFailureEmail(lstEmailparameter, pobjMemberDetails, "HotelBookingFailed");
                    }
                }
            }
            catch (Exception ex)
            {
                try
                {

                    strFailureType += string.Format("{0}/", FailureType.CTRESPONSE);
                    List<string> lstEmailparameter = new List<string>();
                    lstEmailparameter = GenerateHotelBookingFailedEmailParametersInfiPay(lobjBookingResponse, pobjHotel, pobjMemberDetails, pobjHotelSearchRequest, pobjCustomer, strFailureType);
                    SendFailureEmail(lstEmailparameter, pobjMemberDetails, "HotelBookingFailed");
                }
                catch (Exception exp)
                {
                    List<string> lstEmailparameter = new List<string>();
                    lstEmailparameter = GenerateHotelBookingFailedEmailParametersInfiPay(lobjBookingResponse, pobjHotel, pobjMemberDetails, pobjHotelSearchRequest, pobjCustomer, strFailureType);
                    SendFailureEmail(lstEmailparameter, pobjMemberDetails, "HotelBookingFailed");
                }
            }


            return lobjBookingResponse;
        }

        #endregion

        #region SendEmail
        public string FloatToThousandSeperated(float pfltValue)
        {
            NumberFormatInfo nfo = new CultureInfo("en-US", false).NumberFormat;
            string pstrString = pfltValue.ToString("N", nfo);
            string[] lstrArray = pstrString.Split('.');
            return lstrArray[0];
        }
        private void SendEmail(List<string> pstrEmailparameter, MemberDetails pobjMemberDetails, string pstrTemplateCode)
        {
            EmailDetails lobjEmailDetail = new EmailDetails();
            List<string> lstAttachment = new List<string>();
            string lstrToken = GetAuthTokenforWebAPI();
            List<Attachments> lstAttachments = new List<Attachments>();
            APIClientHelper lobjcehelper = new APIClientHelper();

            lobjEmailDetail.TemplateCode = pstrTemplateCode;
            lobjEmailDetail.ListParameter = pstrEmailparameter;
            lobjEmailDetail.ProgramId = Convert.ToString(pobjMemberDetails.ProgramId);
            lobjEmailDetail.MemberId = Convert.ToString(pobjMemberDetails.MemberRelationsList.Find(lobj => lobj.RelationType.Equals(RelationType.LBMS)).RelationReference);
            lobjEmailDetail.AttachmentList = lstAttachment;
            lobjEmailDetail.To = pobjMemberDetails.Email;
            lobjcehelper.InsertEmailDetails(lobjEmailDetail, lstAttachments, lstrToken);
        }

        private bool SendEmails(string Parameters, MemberDetails pobjMemberDetails)
        {
            bool lblnEmailSend = false;
            try
            {
                NICEmailDetailsResponse lobjEmailResponse = new NICEmailDetailsResponse();
                APIClientHelper lobjcehelper = new APIClientHelper();
                if (pobjMemberDetails.Email != string.Empty)
                {
                    string lstrToken = GetAuthTokenforWebAPI();
                    lobjEmailResponse = lobjcehelper.NICEmailDetails(Parameters, lstrToken);
                    if (lobjEmailResponse != null)
                    {
                        if (lobjEmailResponse.results.IsSucessful)
                        {
                            lblnEmailSend = true;
                        }
                        else
                        {
                            LoggingAdapter.WriteLog("Send Communication UnSuccessful :" + lobjEmailResponse.results.ExceptionMessage);
                            lblnEmailSend = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("Send Communication error :" + ex.Message);
            }
            return lblnEmailSend;
        }
        private ItineraryDetails GetFlightReceipt(int pintBookingId)
        {
            IBECTClient lobjIBECTClient = new IBECTClient();
            try
            {
                return lobjIBECTClient.GetFlightReceipt(pintBookingId);
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("GetFlightReceipt - Model - Exception :" + ex.InnerException.Message);
                return null;
            }
        }

        private void SendFailureEmail(List<string> pstrEmailparameter, MemberDetails pobjMemberDetails, string pstrTemplateCode)
        {
            EmailDetails lobjEmailDetail = new EmailDetails();
            List<string> lstAttachment = new List<string>();
            string lstrToken = GetAuthTokenforWebAPI();
            List<Attachments> lstAttachments = new List<Attachments>();
            APIClientHelper lobjcehelper = new APIClientHelper();
            lobjEmailDetail.TemplateCode = pstrTemplateCode;
            lobjEmailDetail.ListParameter = pstrEmailparameter;
            lobjEmailDetail.ProgramId = Convert.ToString(pobjMemberDetails.ProgramId);
            lobjEmailDetail.MemberId = Convert.ToString(pobjMemberDetails.MemberRelationsList.Find(lobj => lobj.RelationType.Equals(RelationType.LBMS)).RelationReference);
            lobjEmailDetail.AttachmentList = lstAttachment;
            lobjEmailDetail.To = String.Empty;
            lobjcehelper.InsertEmailDetails(lobjEmailDetail, lstAttachments, lstrToken);
        }
        #endregion

        #region SendSMS

        private void SendSms(List<string> pstrSmsparameter, MemberDetails pobjMemberDetails, string pstrTemplateCode)
        {
            string lsrtTemplateLangCode = "";
            if (pobjMemberDetails.PreferredLanguage.ToUpper() != "EN")
            {
                lsrtTemplateLangCode = pobjMemberDetails.PreferredLanguage.ToUpper();
            }
            string lstrToken = GetAuthTokenforWebAPI();
            APIClientHelper lobjcehelper = new APIClientHelper();
            SmsDetails lobjSmsDetail = new SmsDetails();
            lobjSmsDetail.TemplateCode = lsrtTemplateLangCode + pstrTemplateCode;
            lobjSmsDetail.ListParameter = pstrSmsparameter;
            lobjSmsDetail.ReceiverMobile = Convert.ToString(pobjMemberDetails.MobileNumber);
            lobjSmsDetail.ProgramId = Convert.ToString(pobjMemberDetails.ProgramId);
            lobjSmsDetail.MemberId = Convert.ToString(pobjMemberDetails.MemberRelationsList.Find(lobj => lobj.RelationType.Equals(RelationType.LBMS)).RelationReference);
            lobjcehelper.InsertSmsDetails(lobjSmsDetail, lstrToken);

        }

        #endregion

        private void SendEmailOnFailure(BookingResponse pobjBookingResponse, MemberDetails pobjMemberDetails)
        {
            EmailDetails lobjEmailDetail = new EmailDetails();
            List<string> lstEmailparameter = new List<string>();
            List<string> lstAttachment = new List<string>();

            string lstrToken = GetAuthTokenforWebAPI();
            List<Attachments> lstAttachments = new List<Attachments>();
            APIClientHelper lobjcehelper = new APIClientHelper();

            //0
            lstEmailparameter.Add((pobjMemberDetails.NationalId));
            //1
            lstEmailparameter.Add(pobjMemberDetails.LastName);
            //2
            lstEmailparameter.Add(Convert.ToString(pobjBookingResponse.PNRDetails.ItineraryDetails.FareDetails.TotalPoints));
            //3
            lstEmailparameter.Add(pobjMemberDetails.MobileNumber);
            //4

            lstEmailparameter.Add(pobjBookingResponse.PNRDetails.ItineraryDetails.ListOfFlightDetails[0].ListOfFlightSegments[0].DepartureAirField.City);
            //5
            lstEmailparameter.Add(pobjBookingResponse.PNRDetails.ItineraryDetails.ListOfFlightDetails[0].ListOfFlightSegments[pobjBookingResponse.PNRDetails.ItineraryDetails.ListOfFlightDetails[0].ListOfFlightSegments.Count - 1].ArrivalAirField.City);
            //6
            if (pobjBookingResponse.PNRDetails.ItineraryDetails.ListOfFlightDetails[0].ListOfFlightSegments.Count > 1)
            {
                lstEmailparameter.Add("( Via " + pobjBookingResponse.PNRDetails.ItineraryDetails.ListOfFlightDetails[0].ListOfFlightSegments[1].DepartureAirField.City + " )");
            }
            else
            {
                lstEmailparameter.Add(string.Empty);
            }
            //7
            if (pobjBookingResponse.PNRDetails.ItineraryDetails.ListOfFlightDetails.Count > 1)
            {
                lstEmailparameter.Add("Return");
            }
            else
            {
                lstEmailparameter.Add("One Way");
            }
            //8
            lstEmailparameter.Add(pobjBookingResponse.PNRDetails.ItineraryDetails.ListOfFlightDetails[0].ListOfFlightSegments[0].DisplayDepartureDate);
            //9
            lstEmailparameter.Add(pobjBookingResponse.PNRDetails.ItineraryDetails.ListOfFlightDetails[0].ListOfFlightSegments[0].DisplayDepartureTime);
            //10
            lstEmailparameter.Add(pobjBookingResponse.PNRDetails.ItineraryDetails.ListOfFlightDetails[0].ListOfFlightSegments[pobjBookingResponse.PNRDetails.ItineraryDetails.ListOfFlightDetails[0].ListOfFlightSegments.Count - 1].DisplayArrivalDate);
            //11
            lstEmailparameter.Add(pobjBookingResponse.PNRDetails.ItineraryDetails.ListOfFlightDetails[0].ListOfFlightSegments[pobjBookingResponse.PNRDetails.ItineraryDetails.ListOfFlightDetails[0].ListOfFlightSegments.Count - 1].DisplayArrivalTime);

            if (pobjBookingResponse.PNRDetails.ItineraryDetails.ListOfFlightDetails.Count > 1)
            {
                //12
                lstEmailparameter.Add(pobjBookingResponse.PNRDetails.ItineraryDetails.ListOfFlightDetails[1].ListOfFlightSegments[0].DisplayDepartureDate);
                //13
                lstEmailparameter.Add(pobjBookingResponse.PNRDetails.ItineraryDetails.ListOfFlightDetails[0].ListOfFlightSegments[0].DisplayDepartureTime);
                //14
                lstEmailparameter.Add(pobjBookingResponse.PNRDetails.ItineraryDetails.ListOfFlightDetails[0].ListOfFlightSegments[pobjBookingResponse.PNRDetails.ItineraryDetails.ListOfFlightDetails[0].ListOfFlightSegments.Count - 1].DisplayArrivalDate);
                //15
                lstEmailparameter.Add(pobjBookingResponse.PNRDetails.ItineraryDetails.ListOfFlightDetails[0].ListOfFlightSegments[pobjBookingResponse.PNRDetails.ItineraryDetails.ListOfFlightDetails[0].ListOfFlightSegments.Count - 1].DisplayArrivalTime);
            }
            else
            {
                //12
                lstEmailparameter.Add("");
                //13
                lstEmailparameter.Add("");
                //14
                lstEmailparameter.Add("");
                //15
                lstEmailparameter.Add("");
            }
            string strPaxDetails = "";
            //16
            for (int i = 0; i < pobjBookingResponse.PNRDetails.ItineraryDetails.TravelerInfo.Count; i++)
            {
                strPaxDetails += "Pax No:" + (i + 1) + " ";
                strPaxDetails += (pobjBookingResponse.PNRDetails.ItineraryDetails.TravelerInfo[i].FirstName + " " + pobjBookingResponse.PNRDetails.ItineraryDetails.TravelerInfo[i].LastName) + "<br/>";
                strPaxDetails += (pobjBookingResponse.PNRDetails.ItineraryDetails.TravelerInfo[i].PassportNumber) + "<br/>";
            }
            lstEmailparameter.Add(strPaxDetails);
            //17
            lstEmailparameter.Add(pobjBookingResponse.PNRDetails.BookingReference);
            lobjEmailDetail.ListParameter = lstEmailparameter;
            lobjEmailDetail.ProgramId = Convert.ToString(pobjMemberDetails.ProgramId);
            lobjEmailDetail.MemberId = Convert.ToString(pobjMemberDetails.MemberRelationsList.Find(lobj => lobj.RelationType.Equals(RelationType.LBMS)).RelationReference);
            lobjEmailDetail.TemplateCode = "FlightBookingFailed";
            lobjEmailDetail.AttachmentList = lstAttachment;
            lobjEmailDetail.To = string.Empty;
            lobjcehelper.InsertEmailDetails(lobjEmailDetail, lstAttachments, lstrToken);
        }

        public string GetAppSettingValue(string key)
        {

            string arr = ConfigurationManager.AppSettings[key].ToString();
            LoggingAdapter.WriteLog("GetAppSettingValue BookingDetails key " + key + " Value " + arr, "Communication Tracing");
            return arr;


        }

        #region Car
        //public CarMakeBookingResponse BookCar(CarMakeBookingRequest pobjCarMakeBookingRequest, string pstrReceipt, CarSearchRequest pobjCarSearchRequest, Match pobjMatch, CarExtrasListResponse pobjCarExtrasListResponse, MemberDetails pobjMemberDetails)
        public CarMakeBookingResponse BookCar(CarMakeBookingRequest pobjCarMakeBookingRequest, CarSearchRequest pobjCarSearchRequest, Match pobjMatch, CarExtrasListResponse pobjCarExtrasListResponse, MemberDetails pobjMemberDetails, List<RedemptionDetails> pobjListOfRedemptionDetails)
        {

            bool lboolRollBackResponse = false;
            bool IsBookingConfirm = false;
            string strRedeemMilesResponse = string.Empty;
            string strRollBackMilesResponse = string.Empty;
            string strFailureType = "";

            CarMakeBookingResponse lobjBookingResponse = new CarMakeBookingResponse();
            PGHelper lobjPGHelper = new PGHelper();
            try
            {

                bool IsRedeem = true;
                List<BookingPaymentBreakage> lobjListOfBookingPaymentBreakage = new List<BookingPaymentBreakage>();

                for (int i = 0; i < pobjListOfRedemptionDetails.Count; i++)
                {
                    //strRedeemMilesResponse = lobjPGHelper.DoOtherRedemption((float)(pobjListOfRedemptionDetails[i].Amount), (float)(pobjListOfRedemptionDetails[i].Amount), pobjListOfRedemptionDetails[i].Points, string.Empty, string.Empty, pobjMemberDetails.MemberRelationsList.Find(lobj => lobj.RelationType.Equals(RelationType.LBMS)).RelationReference, pobjMemberDetails.MemberRelationsList.Find(lobj => lobj.RelationType.Equals(RelationType.LBMS)).WebPassword, pobjMatch.Route[0].PickUp[0].locName + "-" + pobjMatch.Route[0].DropOff[0].locName, Convert.ToInt32(RelationType.LBMS), Convert.ToInt32(LoyaltyTxnType.Car), pobjListOfRedemptionDetails[i].Currency, lstrMerchantId, lstrMerchantUserName, lstrMerchantPassword);

                    strRedeemMilesResponse = RedeemPoints((float)(pobjListOfRedemptionDetails[i].Amount), pobjListOfRedemptionDetails[i].Points, pobjMemberDetails.MemberRelationsList.Find(lobj => lobj.RelationType.Equals(RelationType.LBMS)).RelationReference, pobjMemberDetails.MemberRelationsList.Find(lobj => lobj.RelationType.Equals(RelationType.LBMS)).WebPassword, pobjMatch.Route[0].PickUp[0].locName + "-" + pobjMatch.Route[0].DropOff[0].locName, Convert.ToInt32(LoyaltyTxnType.Car), pobjListOfRedemptionDetails[i].Currency, pobjMemberDetails);
                    pobjListOfRedemptionDetails[i].TransactionReference = strRedeemMilesResponse;
                    IsRedeem = IsRedeem && (pobjListOfRedemptionDetails[i].TransactionReference != "" && pobjListOfRedemptionDetails[i].TransactionReference != string.Empty);

                    BookingPaymentBreakage lobjBookingPaymentBreakage = new BookingPaymentBreakage();
                    lobjBookingPaymentBreakage.Currency = pobjListOfRedemptionDetails[i].Currency;
                    lobjBookingPaymentBreakage.Amount = pobjListOfRedemptionDetails[i].Points;
                    lobjBookingPaymentBreakage.TxnReference = strRedeemMilesResponse;
                    lobjListOfBookingPaymentBreakage.Add(lobjBookingPaymentBreakage);
                }

                BookingPaymentDetails lobjBookingPaymentDetails = new BookingPaymentDetails();
                lobjBookingPaymentDetails.BookingPaymentBreakageList = lobjListOfBookingPaymentBreakage;
                lobjBookingPaymentDetails.MemberId = pobjMemberDetails.MemberRelationsList[0].RelationReference;
                lobjBookingPaymentDetails.PaymentType = PaymentType.Points;
                lobjBookingPaymentDetails.ServiceType = ServiceType.CAR;
                lobjBookingPaymentDetails.Points = pobjListOfRedemptionDetails[0].Points;
                lobjBookingPaymentDetails.PointsTxnRefererence = pobjListOfRedemptionDetails[0].TransactionReference;
                lobjBookingPaymentDetails.PaymentStatus = PaymentStatus.Success;


                pobjCarMakeBookingRequest.BookingPaymentDetails = lobjBookingPaymentDetails;

                IBERCCarClient lobjIBERCCarClient = new IBERCCarClient();

                if (IsRedeem)
                {
                    try
                    {
                        LoggingAdapter.WriteLog("Car RedeemMiles Success ", "CarBookingLogCategory");

                        List<PaymentInfo> lobjPaymentInfoList = new List<PaymentInfo>();
                        PaymentInfo lobjPaymentInfo = new PaymentInfo();
                        lobjPaymentInfo.depositPayment = "False";
                        lobjPaymentInfoList.Add(lobjPaymentInfo);
                        pobjCarMakeBookingRequest.MakeBookingRQ.Booking.PaymentInfo = lobjPaymentInfoList.ToArray();

                        CreditCard lobjCreditCard = new CreditCard();
                        List<ExpirationDate> lobjListOfExpirationDate = new List<ExpirationDate>();
                        ExpirationDate lobjExpirationDate = new ExpirationDate();
                        lobjListOfExpirationDate.Add(lobjExpirationDate);
                        lobjCreditCard.ExpirationDate = lobjListOfExpirationDate.ToArray();
                        pobjCarMakeBookingRequest.MakeBookingRQ.Booking.PaymentInfo[0].CreditCard = lobjCreditCard;

                        lobjBookingResponse = lobjIBERCCarClient.CarMakeBookingResponse(pobjCarMakeBookingRequest, pobjCarSearchRequest, pobjMatch, pobjCarExtrasListResponse);

                        if (lobjBookingResponse != null && lobjBookingResponse.MakeBookingRS.Booking.id != null && lobjBookingResponse.MakeBookingRS.Booking.id != string.Empty)
                        {
                            LoggingAdapter.WriteLog("Booking called Booking Id  : " + lobjBookingResponse.MakeBookingRS.Booking.id, "CarBookingLogCategory");
                            IsBookingConfirm = lobjBookingResponse != null && lobjBookingResponse.MakeBookingRS != null && lobjBookingResponse.MakeBookingRS.Booking != null && !string.IsNullOrEmpty(lobjBookingResponse.MakeBookingRS.Booking.id);
                        }
                        else
                            IsBookingConfirm = false;

                        if (IsBookingConfirm)
                        {
                            try
                            {
                                //communication Engine call for Email send
                                List<string> lstEmailparameter = new List<string>();
                                lstEmailparameter = GenerateCarEmailParameters(lobjBookingResponse, pobjCarMakeBookingRequest, pobjMemberDetails, pobjCarSearchRequest, pobjMatch, pobjCarExtrasListResponse, pobjCarMakeBookingRequest.BookingPaymentDetails.BookingPaymentBreakageList);
                                CarSendEmail(lstEmailparameter, pobjMemberDetails, pobjMemberDetails.PreferredLanguage + "CarBooked");
                                LoggingAdapter.WriteLog("Mail sent. : " + strRedeemMilesResponse, "CarBookingLogCategory");
                            }
                            catch (Exception ex)
                            {
                                LoggingAdapter.WriteLog("Communication engine exception in booking: " + strRedeemMilesResponse, "CarBookingLogCategory");
                                strFailureType += string.Format("{0}/", FailureType.FAILUREEMAIL);
                            }
                            try
                            {
                                //Communication Engine Call for Sms Sending
                                List<string> lstSMSparameter = new List<string>();
                                lstSMSparameter = GenerateCarSMSParameters(lobjBookingResponse);
                                lstSMSparameter.Add(pobjMemberDetails.LastName);
                                SendSms(lstSMSparameter, pobjMemberDetails, "CarBooked");
                                LoggingAdapter.WriteLog("SMS sent. : " + strRedeemMilesResponse, "CarBookingLogCategory");
                            }
                            catch (Exception ex)
                            {
                                LoggingAdapter.WriteLog("Communication engine exception in booking: " + strRedeemMilesResponse, "CarBookingLogCategory");
                                strFailureType += string.Format("{0}/", FailureType.SMS);
                            }

                            if (strFailureType != "" && strFailureType != string.Empty)
                            {
                                List<string> lstEmailparameter = new List<string>();
                                lstEmailparameter = GenerateCarBookingFailedEmailParameters(lobjBookingResponse, pobjCarMakeBookingRequest, pobjMemberDetails, pobjCarSearchRequest, pobjMatch, pobjCarExtrasListResponse);
                                CarSendEmail(lstEmailparameter, pobjMemberDetails, pobjMemberDetails.PreferredLanguage + "CarBookingFailed");
                            }
                        }
                        else
                        {
                            try
                            {
                                for (int i = 0; i < pobjListOfRedemptionDetails.Count; i++)
                                {
                                    strRedeemMilesResponse = pobjListOfRedemptionDetails[i].TransactionReference;
                                    if (strRedeemMilesResponse != "" && strRedeemMilesResponse != string.Empty)
                                        //lboolRollBackResponse = lobjPGHelper.RollBackTransaction(strRedeemMilesResponse, pobjMemberDetails.MemberRelationsList.Find(lob => lob.RelationType.Equals(RelationType.LBMS)).RelationReference, pobjCarMakeBookingRequest.MakeBookingRQ.Booking.PickUp[0].Location[0].locName + "-" + pobjCarMakeBookingRequest.MakeBookingRQ.Booking.DropOff[0].Location[0].locName, lstrMerchantId, lstrMerchantUserName, lstrMerchantPassword);
                                        lboolRollBackResponse = RollBackTransaction(strRedeemMilesResponse, pobjMemberDetails.MemberRelationsList.Find(lob => lob.RelationType.Equals(RelationType.LBMS)).RelationReference, pobjCarMakeBookingRequest.MakeBookingRQ.Booking.PickUp[0].Location[0].locName + "-" + pobjCarMakeBookingRequest.MakeBookingRQ.Booking.DropOff[0].Location[0].locName);
                                }

                                LoggingAdapter.WriteLog("RollBackMiles Success : " + lboolRollBackResponse, "CarBookingLogCategory");
                                strFailureType += string.Format("{0}/", FailureType.CTRESPONSE);
                                List<string> lstEmailparameter = new List<string>();
                                lstEmailparameter = GenerateCarBookingFailedEmailParameters(lobjBookingResponse, pobjCarMakeBookingRequest, pobjMemberDetails, pobjCarSearchRequest, pobjMatch, pobjCarExtrasListResponse);
                                CarSendEmail(lstEmailparameter, pobjMemberDetails, pobjMemberDetails.PreferredLanguage + "CarBookingFailed");
                            }
                            catch (Exception ex)
                            {
                                LoggingAdapter.WriteLog("RollBackMiles Exception : " + lboolRollBackResponse + " \n Exception \n" + ex.StackTrace, "CarBookingLogCategory");
                                strFailureType += string.Format("{0}/", FailureType.ROLLBACKMILES);
                                List<string> lstEmailparameter = new List<string>();
                                lstEmailparameter = GenerateCarBookingFailedEmailParameters(lobjBookingResponse, pobjCarMakeBookingRequest, pobjMemberDetails, pobjCarSearchRequest, pobjMatch, pobjCarExtrasListResponse);
                                CarSendEmail(lstEmailparameter, pobjMemberDetails, pobjMemberDetails.PreferredLanguage + "CarBookingFailed");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        try
                        {
                            if (!IsBookingConfirm)
                            {
                                for (int i = 0; i < pobjListOfRedemptionDetails.Count; i++)
                                {
                                    strRedeemMilesResponse = pobjListOfRedemptionDetails[i].TransactionReference;
                                    if (strRedeemMilesResponse != "" && strRedeemMilesResponse != string.Empty)
                                        //lboolRollBackResponse = lobjPGHelper.RollBackTransaction(strRedeemMilesResponse, pobjMemberDetails.MemberRelationsList.Find(lob => lob.RelationType.Equals(RelationType.LBMS)).RelationReference, pobjCarMakeBookingRequest.MakeBookingRQ.Booking.PickUp[0].Location[0].locName + "-" + pobjCarMakeBookingRequest.MakeBookingRQ.Booking.DropOff[0].Location[0].locName, lstrMerchantId, lstrMerchantUserName, lstrMerchantPassword);
                                        lboolRollBackResponse = RollBackTransaction(strRedeemMilesResponse, pobjMemberDetails.MemberRelationsList.Find(lob => lob.RelationType.Equals(RelationType.LBMS)).RelationReference, pobjCarMakeBookingRequest.MakeBookingRQ.Booking.PickUp[0].Location[0].locName + "-" + pobjCarMakeBookingRequest.MakeBookingRQ.Booking.DropOff[0].Location[0].locName);
                                }
                            }

                            LoggingAdapter.WriteLog("RollBackMiles Success On Exception : " + lboolRollBackResponse + " \n Exception \n" + ex.StackTrace, "CarBookingLogCategory");
                            strFailureType += string.Format("{0}/", FailureType.CTRESPONSE);
                            List<string> lstEmailparameter = new List<string>();
                            lstEmailparameter = GenerateCarBookingFailedEmailParameters(lobjBookingResponse, pobjCarMakeBookingRequest, pobjMemberDetails, pobjCarSearchRequest, pobjMatch, pobjCarExtrasListResponse);
                            CarSendEmail(lstEmailparameter, pobjMemberDetails, pobjMemberDetails.PreferredLanguage + "CarBookingFailed");
                        }
                        catch (Exception exp)
                        {
                            LoggingAdapter.WriteLog("RollBackMiles Exception : " + lboolRollBackResponse + " \n Exception \n" + exp.StackTrace, "CarBookingLogCategory");
                            strFailureType += string.Format("{0}/", FailureType.ROLLBACKMILES);
                            List<string> lstEmailparameter = new List<string>();
                            lstEmailparameter = GenerateCarBookingFailedEmailParameters(lobjBookingResponse, pobjCarMakeBookingRequest, pobjMemberDetails, pobjCarSearchRequest, pobjMatch, pobjCarExtrasListResponse);
                            CarSendEmail(lstEmailparameter, pobjMemberDetails, pobjMemberDetails.PreferredLanguage + "CarBookingFailed");
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < pobjListOfRedemptionDetails.Count; i++)
                    {
                        strRedeemMilesResponse = pobjListOfRedemptionDetails[i].TransactionReference;
                        if (strRedeemMilesResponse != "" && strRedeemMilesResponse != string.Empty)
                            //lboolRollBackResponse = lobjPGHelper.RollBackTransaction(strRedeemMilesResponse, pobjMemberDetails.MemberRelationsList.Find(lob => lob.RelationType.Equals(RelationType.LBMS)).RelationReference, pobjCarMakeBookingRequest.MakeBookingRQ.Booking.PickUp[0].Location[0].locName + "-" + pobjCarMakeBookingRequest.MakeBookingRQ.Booking.DropOff[0].Location[0].locName, lstrMerchantId, lstrMerchantUserName, lstrMerchantPassword);
                            lboolRollBackResponse = RollBackTransaction(strRedeemMilesResponse, pobjMemberDetails.MemberRelationsList.Find(lob => lob.RelationType.Equals(RelationType.LBMS)).RelationReference, pobjCarMakeBookingRequest.MakeBookingRQ.Booking.PickUp[0].Location[0].locName + "-" + pobjCarMakeBookingRequest.MakeBookingRQ.Booking.DropOff[0].Location[0].locName);
                    }
                    strFailureType += string.Format("{0}/", FailureType.REDEEMMILES);
                    List<string> lstEmailparameter = new List<string>();
                    lstEmailparameter = GenerateCarBookingFailedEmailParameters(lobjBookingResponse, pobjCarMakeBookingRequest, pobjMemberDetails, pobjCarSearchRequest, pobjMatch, pobjCarExtrasListResponse);
                    CarSendEmail(lstEmailparameter, pobjMemberDetails, pobjMemberDetails.PreferredLanguage + "CarBookingFailed");
                }
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog(ex.StackTrace);
            }

            return lobjBookingResponse;
        }

        public CarMakeBookingResponse BookCarByInfiVoucher(CarMakeBookingRequest pobjCarMakeBookingRequest, CarSearchRequest pobjCarSearchRequest, Match pobjMatch, CarExtrasListResponse pobjCarExtrasListResponse, MemberDetails pobjMemberDetails)
        {
            bool IsBookingConfirm = true;
            string strFailureType = "";

            CarMakeBookingResponse lobjBookingResponse = new CarMakeBookingResponse();

            try
            {
                IBERCCarClient lobjIBERCCarClient = new IBERCCarClient();

                try
                {
                    List<PaymentInfo> lobjPaymentInfoList = new List<PaymentInfo>();
                    PaymentInfo lobjPaymentInfo = new PaymentInfo();
                    lobjPaymentInfo.depositPayment = "False";
                    lobjPaymentInfoList.Add(lobjPaymentInfo);
                    pobjCarMakeBookingRequest.MakeBookingRQ.Booking.PaymentInfo = lobjPaymentInfoList.ToArray();

                    CreditCard lobjCreditCard = new CreditCard();
                    List<ExpirationDate> lobjListOfExpirationDate = new List<ExpirationDate>();
                    ExpirationDate lobjExpirationDate = new ExpirationDate();
                    lobjListOfExpirationDate.Add(lobjExpirationDate);
                    lobjCreditCard.ExpirationDate = lobjListOfExpirationDate.ToArray();
                    pobjCarMakeBookingRequest.MakeBookingRQ.Booking.PaymentInfo[0].CreditCard = lobjCreditCard;


                    lobjBookingResponse = lobjIBERCCarClient.CarMakeBookingResponse(pobjCarMakeBookingRequest, pobjCarSearchRequest, pobjMatch, pobjCarExtrasListResponse);

                }
                catch (Exception ex)
                {
                    LoggingAdapter.WriteLog("CreateBooking Failure BookingFacade:" + ex.Message + ex.StackTrace);
                }

                if (lobjBookingResponse != null && lobjBookingResponse.MakeBookingRS.Booking.id != null && lobjBookingResponse.MakeBookingRS.Booking.id != string.Empty)
                {
                    LoggingAdapter.WriteLog("Booking called Booking Id  : " + lobjBookingResponse.MakeBookingRS.Booking.id, "CarBookingLogCategory");
                    IsBookingConfirm = lobjBookingResponse != null && lobjBookingResponse.MakeBookingRS != null && lobjBookingResponse.MakeBookingRS.Booking != null && !string.IsNullOrEmpty(lobjBookingResponse.MakeBookingRS.Booking.id);
                }
                else
                {
                    IsBookingConfirm = false;
                }

                if (IsBookingConfirm)
                {
                    try
                    {
                        //communication Engine call for Email send
                        List<string> lstEmailparameter = new List<string>();
                        lstEmailparameter = GenerateCarEmailParameters(lobjBookingResponse, pobjCarMakeBookingRequest, pobjMemberDetails, pobjCarSearchRequest, pobjMatch, pobjCarExtrasListResponse, pobjCarMakeBookingRequest.BookingPaymentDetails.BookingPaymentBreakageList);
                        LoggingAdapter.WriteLog("Mail sent. : " + lstEmailparameter, "CarBookingLogCategory");
                        CarSendEmail(lstEmailparameter, pobjMemberDetails, pobjMemberDetails.PreferredLanguage + "CarBooked");
                    }
                    catch (Exception ex)
                    {
                        strFailureType += string.Format("{0}/", FailureType.FAILUREEMAIL);
                    }
                    try
                    {
                        //Communication Engine Call for Sms Sending
                        List<string> lstSMSparameter = new List<string>();
                        lstSMSparameter = GenerateCarSMSParameters(lobjBookingResponse);
                        LoggingAdapter.WriteLog("SMS sent. : " + lstSMSparameter, "CarBookingLogCategory");
                        SendSms(lstSMSparameter, pobjMemberDetails, "CarBookedInfiVoucher");

                    }
                    catch (Exception ex)
                    {
                        strFailureType += string.Format("{0}/", FailureType.SMS);
                    }
                }
                else
                {
                    try
                    {
                        strFailureType += string.Format("{0}/", FailureType.CTRESPONSE);
                        List<string> lstEmailparameter = new List<string>();
                        lstEmailparameter = GenerateCarBookingFailedEmailParameters(lobjBookingResponse, pobjCarMakeBookingRequest, pobjMemberDetails, pobjCarSearchRequest, pobjMatch, pobjCarExtrasListResponse);
                        LoggingAdapter.WriteLog("Failed Email parameter: " + lstEmailparameter, "CarBookingLogCategory");
                        CarSendEmail(lstEmailparameter, pobjMemberDetails, pobjMemberDetails.PreferredLanguage + "CarBookingFailed");
                    }
                    catch (Exception ex)
                    {
                        List<string> lstEmailparameter = new List<string>();
                        lstEmailparameter = GenerateCarBookingFailedEmailParameters(lobjBookingResponse, pobjCarMakeBookingRequest, pobjMemberDetails, pobjCarSearchRequest, pobjMatch, pobjCarExtrasListResponse);
                        LoggingAdapter.WriteLog("Failed Email parameter: " + lstEmailparameter, "CarBookingLogCategory");
                        CarSendEmail(lstEmailparameter, pobjMemberDetails, pobjMemberDetails.PreferredLanguage + "CarBookingFailed");
                    }
                }
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog(ex.StackTrace);
            }
            return lobjBookingResponse;
        }

        public List<string> GenerateCarEmailParameters(CarMakeBookingResponse pobjCarBookingResponse, CarMakeBookingRequest pobjCarMakeBookingRequest, MemberDetails pobjMemberDetails, CarSearchRequest pobjCarSearchRequest, Match pobjMatch, CarExtrasListResponse pobjCarExtrasListResponse, List<BookingPaymentBreakage> pobjListOfBookingPaymentBreakage)
        {
            List<string> lstEmailparameter = new List<string>();
            //[0]
            lstEmailparameter.Add(pobjMemberDetails.LastName);
            //[1]
            lstEmailparameter.Add(pobjMemberDetails.NationalId);
            //[2]
            lstEmailparameter.Add(pobjCarBookingResponse.MakeBookingRS.Booking.id);
            //lstEmailparameter.Add("BookId");
            //[3]
            lstEmailparameter.Add(pobjCarBookingResponse.BookingReferenceId);
            //lstEmailparameter.Add("BookRedId");
            //[4]
            lstEmailparameter.Add(pobjCarBookingResponse.MakeBookingRS.Booking.status);
            //lstEmailparameter.Add("BStatus");
            //[5]
            lstEmailparameter.Add(pobjMatch.Route[0].PickUp[0].locName);
            //lstEmailparameter.Add("PLocation");
            //[6]
            lstEmailparameter.Add(pobjMatch.Route[0].DropOff[0].locName);
            // lstEmailparameter.Add("D Location");

            //[7]
            lstEmailparameter.Add(pobjCarSearchRequest.SearchRequest.PickUp[0].Date[0].day + "/" + pobjCarSearchRequest.SearchRequest.PickUp[0].Date[0].month + "/" + pobjCarSearchRequest.SearchRequest.PickUp[0].Date[0].year);
            // lstEmailparameter.Add("02/02/2010");
            //[8]
            lstEmailparameter.Add(pobjCarSearchRequest.SearchRequest.PickUp[0].Date[0].hour + ":" + pobjCarSearchRequest.SearchRequest.PickUp[0].Date[0].minute);
            //lstEmailparameter.Add("10:10");
            //[9]
            lstEmailparameter.Add(pobjCarSearchRequest.SearchRequest.DropOff[0].Date[0].day + "/" + pobjCarSearchRequest.SearchRequest.DropOff[0].Date[0].month + "/" + pobjCarSearchRequest.SearchRequest.DropOff[0].Date[0].year);
            //lstEmailparameter.Add("03/03/2010");
            //[10]
            lstEmailparameter.Add(pobjCarSearchRequest.SearchRequest.DropOff[0].Date[0].hour + ":" + pobjCarSearchRequest.SearchRequest.DropOff[0].Date[0].minute);
            //lstEmailparameter.Add("20:20");
            //[11]
            lstEmailparameter.Add(pobjMatch.Vehicle[0].Name);
            //lstEmailparameter.Add("vehicle name");
            //[12]
            lstEmailparameter.Add(pobjMatch.Vehicle[0].automatic);
            //lstEmailparameter.Add("Automatic");
            //[13]
            lstEmailparameter.Add(pobjMatch.Vehicle[0].aircon);
            // lstEmailparameter.Add("aircon");
            //[14]
            lstEmailparameter.Add(pobjMatch.Route[0].PickUp[0].locName);
            //lstEmailparameter.Add("PickupLocation");
            //[15]
            lstEmailparameter.Add(pobjCarSearchRequest.SearchRequest.PickUp[0].Location[0].city);
            // lstEmailparameter.Add("City");
            //[16]
            lstEmailparameter.Add(pobjCarSearchRequest.SearchRequest.PickUp[0].Location[0].country);
            // lstEmailparameter.Add("Country");
            //[17]
            lstEmailparameter.Add(pobjCarMakeBookingRequest.MakeBookingRQ.Booking.DriverInfo[0].DriverName[0].title + ". " + pobjCarMakeBookingRequest.MakeBookingRQ.Booking.DriverInfo[0].DriverName[0].firstname + " " + pobjCarMakeBookingRequest.MakeBookingRQ.Booking.DriverInfo[0].DriverName[0].lastname);
            //lstEmailparameter.Add("Driver name");
            //[18]
            lstEmailparameter.Add(pobjMemberDetails.Address);
            //lstEmailparameter.Add("Address ");
            //[19]
            lstEmailparameter.Add(pobjMemberDetails.MobileNumber);
            //lstEmailparameter.Add("123456780");
            //[20]
            string strPaymentDetails = string.Empty;

            string strPaymentDetailsHTML = "<tr><td width='2%'>&nbsp;</td><td width='56%' align='left' valign='top' style='font-family: arial; padding-top: 5px;font-size:12px;'>{0}</td><td width='40%' align='left' valign='middle' style='font-family: arial; font-size: 12px;text-align: left;'>{1}</td><td width='2%'>&nbsp;</td></tr>";

            for (int i = 0; i < pobjListOfBookingPaymentBreakage.Count; i++)
            {
                strPaymentDetails += string.Format(strPaymentDetailsHTML, pobjListOfBookingPaymentBreakage[i].Currency, pobjListOfBookingPaymentBreakage[i].Amount);
            }

            lstEmailparameter.Add(strPaymentDetails);
            //lstEmailparameter.Add(Convert.ToString(pobjMatch.Price[0].TotalPoints + pobjCarExtrasListResponse.ExtrasListRS.Items[0].ExtraInfo[0].Price[0].ExtraTotalPoints));
            //lstEmailparameter.Add("123point");
            return lstEmailparameter;

        }

        private List<string> GenerateCarSMSParameters(CarMakeBookingResponse plobBookingResponse)
        {
            List<string> lstSMSparameter = new List<string>();

            // lstSMSparameter.Add("Your Car has booked");
            lstSMSparameter.Add(Convert.ToString(plobBookingResponse.BookingReferenceId));
            return lstSMSparameter;
        }

        private List<string> GenerateCarBookingFailedEmailParameters(CarMakeBookingResponse pobjCarMakeBookingResponse, CarMakeBookingRequest pobjCarMakeBookingRequest, MemberDetails pobjMemberDetails, CarSearchRequest pobjCarSearchRequest, Match pobjMatch, CarExtrasListResponse pobjCarExtrasListResponse)
        {
            List<string> lstEmailparameter = new List<string>();

            //[0]
            lstEmailparameter.Add(pobjMemberDetails.NationalId);
            //[1]
            lstEmailparameter.Add(pobjMemberDetails.LastName);
            //[2]
            lstEmailparameter.Add(pobjMemberDetails.MobileNumber);

            //[3]
            lstEmailparameter.Add(pobjCarMakeBookingResponse.BookingReferenceId);

            //[4]
            lstEmailparameter.Add(Convert.ToString(pobjMatch.Price[0].TotalPoints + pobjCarExtrasListResponse.ExtrasListRS.Items[0].ExtraInfo[0].Price[0].ExtraTotalPoints));

            //[5]
            lstEmailparameter.Add(pobjMatch.Vehicle[0].Name);
            //[6]
            lstEmailparameter.Add(pobjMatch.Route[0].PickUp[0].locName);
            //[7]
            lstEmailparameter.Add(pobjCarSearchRequest.SearchRequest.PickUp[0].Location[0].city);
            //[8]
            lstEmailparameter.Add(pobjCarSearchRequest.SearchRequest.PickUp[0].Location[0].country);
            //[9]
            lstEmailparameter.Add(pobjCarSearchRequest.SearchRequest.PickUp[0].Date[0].day + "/" + pobjCarSearchRequest.SearchRequest.PickUp[0].Date[0].month + "/" + pobjCarSearchRequest.SearchRequest.PickUp[0].Date[0].year);
            //[10]
            lstEmailparameter.Add(pobjMatch.Route[0].DropOff[0].locName);

            //[11]
            lstEmailparameter.Add(pobjCarSearchRequest.SearchRequest.DropOff[0].Location[0].city);
            //[12]
            lstEmailparameter.Add(pobjCarSearchRequest.SearchRequest.DropOff[0].Location[0].country);


            //[13]
            lstEmailparameter.Add(pobjCarSearchRequest.SearchRequest.DropOff[0].Date[0].day + "/" + pobjCarSearchRequest.SearchRequest.DropOff[0].Date[0].month + "/" + pobjCarSearchRequest.SearchRequest.DropOff[0].Date[0].year);



            //[14]
            lstEmailparameter.Add(pobjCarMakeBookingRequest.MakeBookingRQ.Booking.DriverInfo[0].DriverName[0].title + ". " + pobjCarMakeBookingRequest.MakeBookingRQ.Booking.DriverInfo[0].DriverName[0].firstname + " " + pobjCarMakeBookingRequest.MakeBookingRQ.Booking.DriverInfo[0].DriverName[0].lastname);



            string strPaymentDetails = string.Empty;
            if (pobjCarMakeBookingRequest.BookingPaymentDetails.PaymentType.Equals(PaymentType.CashPoints))
            {
                strPaymentDetails = "<table width='100%' border='0' cellspacing='0' cellpadding='0'><tr><td align='left' valign='top' width='50%'><table align='left'" +
                    " cellpadding='0' cellspacing='0' width='100%'><tr><td align='left' valign='top' width='46%' style='font-family: Arial; font-size: 12px;letter-spacing:" +
                    " normal; line-height: 18px; font-weight: bold; text-transform: capitalize;text-align: left; color: #231f20; padding: 4px 0 3px 0;'>Points</td>" +
                    "<td width='5px' >:</td><td align='left' valign='top' style='font-family: Arial; font-size: 12px; letter-spacing: normal;line-height: 18px; font-weight:" +
                    " normal; text-transform: inherit; text-align: left;color: #231f20; padding: 4px 0 3px 0;'>"
                    + Convert.ToString(pobjCarMakeBookingRequest.BookingPaymentDetails.Points) + "</td></tr></table></td><td align='left' valign='top'" +
                    " width='50%'><table align='left' cellpadding='0' cellspacing='0' width='100%'><tr><td align='left' valign='top' width='46%' style='font-family: Arial;" +
                    " font-size: 12px;letter-spacing: normal; line-height: 18px; font-weight: bold; text-transform: capitalize;text-align: left; color: #231f20;" +
                    " padding: 4px 0 3px 0;'>Cash Payment</td><td width='5px'>:</td><td align='left' valign='top' style='font-family: Arial; font-size: 12px; letter-spacing:" +
                    " normal;line-height: 18px; font-weight: normal; text-transform: inherit; text-align: left;color: #231f20; padding: 4px 0 3px 0;'>"
                    + Convert.ToString(pobjCarMakeBookingRequest.BookingPaymentDetails.CashAmount) + "</td></tr></table></td></tr></table>";
            }
            else if (pobjCarMakeBookingRequest.BookingPaymentDetails.PaymentType.Equals(PaymentType.Points))
            {


                strPaymentDetails = "<table width='100%' border='0' cellspacing='0' cellpadding='0'><tr><td align='left' valign='top' width='50%'><table align='left'" +
                    " cellpadding='0' cellspacing='0' width='100%'><tr><td align='left' valign='top' width='46%' style='font-family: Arial; font-size: 12px;letter-spacing:" +
                    " normal; line-height: 18px; font-weight: bold; text-transform: capitalize;text-align: left; color: #231f20; padding: 4px 0 3px 0;'>Points" +
                    "</td><td width='5px'>:</td><td align='left' valign='top' style='font-family: Arial; font-size: 12px; letter-spacing: normal;line-height: 18px;" +
                    " font-weight: normal; text-transform: inherit; text-align: left;color: #231f20; padding: 4px 0 3px 0;'>"
                    + Convert.ToString(pobjMatch.Price[0].TotalPoints + pobjCarExtrasListResponse.ExtrasListRS.Items[0].ExtraInfo[0].Price[0].ExtraTotalPoints) +
                    "</td></tr></table></td></tr></table>";


            }
            else if (pobjCarMakeBookingRequest.BookingPaymentDetails.PaymentType.Equals(PaymentType.Cash))
            {

                strPaymentDetails = "<table width='100%' border='0' cellspacing='0' cellpadding='0'><tr> <td align='left' valign='top' width='50%'><table align='left' cellpadding='0' cellspacing='0' width='100%'><tr><td align='left'" +
                    " valign='top' width='46%' style='font-family: Arial; font-size: 12px;letter-spacing: normal; line-height: 18px; font-weight: bold; text-transform:" +
                    " capitalize;text-align: left; color: #231f20; padding: 4px 0 3px 0;'>Cash Payment</td><td width='5px'>:</td><td align='left' valign='top'" +
                    " style='font-family: Arial; font-size: 12px; letter-spacing: normal;line-height: 18px; font-weight: normal; text-transform: inherit; text-align:" +
                    " left;color: #231f20; padding: 4px 0 3px 0;'>"
                    + Convert.ToString(pobjMatch.Price[0].TotalBaseFare + pobjCarExtrasListResponse.ExtrasListRS.Items[0].ExtraInfo[0].Price[0].ExtraTotalBaseFare) +
                    "</td></tr></table></td></tr></table>";
            }
            //[15]
            lstEmailparameter.Add(strPaymentDetails);



            return lstEmailparameter;
        }


        //Added For Infipay
        public CarMakeBookingResponse BookCarForCashPlusPoints(CarMakeBookingRequest pobjCarMakeBookingRequest, CarSearchRequest pobjCarSearchRequest, Match pobjMatch, CarExtrasListResponse pobjCarExtrasListResponse, MemberDetails pobjMemberDetails)
        {
            bool IsBookingConfirm = true;
            string strFailureType = "";

            CarMakeBookingResponse lobjBookingResponse = new CarMakeBookingResponse();

            try
            {
                IBERCCarClient lobjIBERCCarClient = new IBERCCarClient();

                try
                {
                    List<PaymentInfo> lobjPaymentInfoList = new List<PaymentInfo>();
                    PaymentInfo lobjPaymentInfo = new PaymentInfo();
                    lobjPaymentInfo.depositPayment = "False";
                    lobjPaymentInfoList.Add(lobjPaymentInfo);
                    pobjCarMakeBookingRequest.MakeBookingRQ.Booking.PaymentInfo = lobjPaymentInfoList.ToArray();

                    CreditCard lobjCreditCard = new CreditCard();
                    List<ExpirationDate> lobjListOfExpirationDate = new List<ExpirationDate>();
                    ExpirationDate lobjExpirationDate = new ExpirationDate();
                    lobjListOfExpirationDate.Add(lobjExpirationDate);
                    lobjCreditCard.ExpirationDate = lobjListOfExpirationDate.ToArray();
                    pobjCarMakeBookingRequest.MakeBookingRQ.Booking.PaymentInfo[0].CreditCard = lobjCreditCard;


                    lobjBookingResponse = lobjIBERCCarClient.CarMakeBookingResponse(pobjCarMakeBookingRequest, pobjCarSearchRequest, pobjMatch, pobjCarExtrasListResponse);

                }
                catch (Exception ex)
                {
                    LoggingAdapter.WriteLog("CreateBooking Failure BookingFacade:" + ex.Message + ex.StackTrace);
                }

                if (lobjBookingResponse != null && lobjBookingResponse.MakeBookingRS.Booking.id != null && lobjBookingResponse.MakeBookingRS.Booking.id != string.Empty)
                {
                    LoggingAdapter.WriteLog("Booking called Booking Id  : " + lobjBookingResponse.MakeBookingRS.Booking.id, "CarBookingLogCategory");
                    IsBookingConfirm = lobjBookingResponse != null && lobjBookingResponse.MakeBookingRS != null && lobjBookingResponse.MakeBookingRS.Booking != null && !string.IsNullOrEmpty(lobjBookingResponse.MakeBookingRS.Booking.id);
                }
                else
                {
                    IsBookingConfirm = false;
                }

                if (IsBookingConfirm)
                {
                    try
                    {
                        //communication Engine call for Email send
                        List<string> lstEmailparameter = new List<string>();
                        lstEmailparameter = GenerateCarEmailParameters(lobjBookingResponse, pobjCarMakeBookingRequest, pobjMemberDetails, pobjCarSearchRequest, pobjMatch, pobjCarExtrasListResponse, pobjCarMakeBookingRequest.BookingPaymentDetails.BookingPaymentBreakageList);
                        LoggingAdapter.WriteLog("Mail sent. : " + lstEmailparameter, "CarBookingLogCategory");
                        CarSendEmail(lstEmailparameter, pobjMemberDetails, pobjMemberDetails.PreferredLanguage + "CarBooked");
                    }
                    catch (Exception ex)
                    {
                        strFailureType += string.Format("{0}/", FailureType.FAILUREEMAIL);
                    }
                    try
                    {
                        //Communication Engine Call for Sms Sending
                        List<string> lstSMSparameter = new List<string>();
                        lstSMSparameter = GenerateCarSMSParameters(lobjBookingResponse);
                        LoggingAdapter.WriteLog("SMS sent. : " + lstSMSparameter, "CarBookingLogCategory");
                        SendSms(lstSMSparameter, pobjMemberDetails, "CarBooked");

                    }
                    catch (Exception ex)
                    {
                        strFailureType += string.Format("{0}/", FailureType.SMS);
                    }
                }
                else
                {
                    try
                    {
                        strFailureType += string.Format("{0}/", FailureType.CTRESPONSE);
                        List<string> lstEmailparameter = new List<string>();
                        lstEmailparameter = GenerateCarBookingFailedEmailParameters(lobjBookingResponse, pobjCarMakeBookingRequest, pobjMemberDetails, pobjCarSearchRequest, pobjMatch, pobjCarExtrasListResponse);
                        LoggingAdapter.WriteLog("Failed Email parameter: " + lstEmailparameter, "CarBookingLogCategory");
                        CarSendEmail(lstEmailparameter, pobjMemberDetails, pobjMemberDetails.PreferredLanguage + "CarBookingFailed");
                    }
                    catch (Exception ex)
                    {
                        List<string> lstEmailparameter = new List<string>();
                        lstEmailparameter = GenerateCarBookingFailedEmailParameters(lobjBookingResponse, pobjCarMakeBookingRequest, pobjMemberDetails, pobjCarSearchRequest, pobjMatch, pobjCarExtrasListResponse);
                        LoggingAdapter.WriteLog("Failed Email parameter: " + lstEmailparameter, "CarBookingLogCategory");
                        CarSendEmail(lstEmailparameter, pobjMemberDetails, pobjMemberDetails.PreferredLanguage + "CarBookingFailed");
                    }
                }
            }

            catch (Exception ex)
            {
                LoggingAdapter.WriteLog(ex.StackTrace);
            }


            return lobjBookingResponse;

        }
        public void CarSendEmail(List<string> pstrEmailparameter, MemberDetails pobjMemberDetails, string pstrTemplateCode)
        {
            EmailDetails lobjEmailDetail = new EmailDetails();
            List<string> lstAttachment = new List<string>();
            string lstrToken = GetAuthTokenforWebAPI();
            List<Attachments> lstAttachments = new List<Attachments>();
            APIClientHelper lobjcehelper = new APIClientHelper();

            lobjEmailDetail.TemplateCode = pstrTemplateCode;
            lobjEmailDetail.ListParameter = pstrEmailparameter;
            lobjEmailDetail.ProgramId = Convert.ToString(pobjMemberDetails.ProgramId);
            lobjEmailDetail.MemberId = Convert.ToString(pobjMemberDetails.MemberRelationsList.Find(lobj => lobj.RelationType.Equals(RelationType.LBMS)).RelationReference);
            lobjEmailDetail.AttachmentList = lstAttachment;
            if (pstrTemplateCode == "CarBookingFailed")
            {
                lobjEmailDetail.To = string.Empty;
            }
            else
            {
                lobjEmailDetail.To = pobjMemberDetails.Email;
            }
            lobjcehelper.InsertEmailDetails(lobjEmailDetail, lstAttachments, lstrToken);
        }

        #endregion

        public bool CheckWhetherOTPExists(OTPDetails pobjOTPDetails)
        {
            OTPFacade lobjfacade = new OTPFacade();
            return lobjfacade.UpdateOTP(pobjOTPDetails);
        }

        public bool GenerateReviewnConfirmOTP(OTPDetails pobjOTPDetails, MemberDetails pobjMemberDetails, string redemptionType)
        {

            OTPDetails lobjOTPDetails = GenerateOTPDetails(pobjOTPDetails);
            string lstrTemplateCode = string.Empty;
            if (lobjOTPDetails != null)
            {
                //if (lobjOTPDetails.OtpEnumTypes.Equals(OTPEnumTypes.AIRREVIEWNCONFIRM))
                //{
                //    redemption_type = "International Flight";
                //}
                //else if (lobjOTPDetails.OtpEnumTypes.Equals(OTPEnumTypes.HOTELREVIEWNCONFIRM))
                //{
                //    redemption_type = "Hotel";
                //}
                //else if (lobjOTPDetails.OtpEnumTypes.Equals(OTPEnumTypes.CARREVIEWNCONFIRM))
                //{
                //    redemption_type = "Car";
                //}
                //else if (lobjOTPDetails.OtpEnumTypes.Equals(OTPEnumTypes.SHOPDIGITALREVIEWNCONFIRM))
                //{
                //    redemption_type = "Gift Cards";
                //}
                //else if (lobjOTPDetails.OtpEnumTypes.Equals(OTPEnumTypes.SHOPREVIEWNCONFIRM))
                //{
                //    redemption_type = "Shop";
                //}
                //else if (lobjOTPDetails.OtpEnumTypes.Equals(OTPEnumTypes.PACKAGEREVIEWNCONFIRM))
                //{
                //    redemption_type = "Experience";
                //}
                //else if (lobjOTPDetails.OtpEnumTypes.Equals(OTPEnumTypes.INSURANCEREVIEWNCONFIRM))
                //{
                //    redemption_type = "Insurance";
                //}
                //else if (lobjOTPDetails.OtpEnumTypes.Equals(OTPEnumTypes.ISPREVIEWNCONFIRM))
                //{
                //    redemption_type = "Internet Service Provider";
                //}
                //else if (lobjOTPDetails.OtpEnumTypes.Equals(OTPEnumTypes.DOMESTICFLIGHTREVIEWNCONFIRM))
                //{
                //    redemption_type = "Domestic Flight";
                //}
                lstrTemplateCode = "Redemption_OTP";
                SendOTPEmail(pobjMemberDetails, lobjOTPDetails.OTP.ToString(), lstrTemplateCode, redemptionType);
                SendOTPSMS(pobjMemberDetails, lobjOTPDetails.OTP.ToString(), lobjOTPDetails.ExpiryDateTime, lstrTemplateCode);
                return true;
            }
            return false;
        }

        //private bool SendOTPEmail(MemberDetails pobjMemberDetails, string pstrOTPstirng, DateTime pdtExpirydatetime, string pstrTemplateCode)
        //{
        //    bool lblnEmailSend = false;
        //    try
        //    {
        //        if (pobjMemberDetails.Email != string.Empty)
        //        {
        //            string lsrtTemplateLangCode = "";
        //            if (pobjMemberDetails.PreferredLanguage.ToUpper() != "EN")
        //            {
        //                lsrtTemplateLangCode = pobjMemberDetails.PreferredLanguage.ToUpper();
        //            }

        //            string lstrToken = GetAuthTokenforWebAPI();
        //            string lstrexpirydt = pdtExpirydatetime.ToString("dd/MM/yyyy");

        //            EmailDetails lobjEmailDetail = new EmailDetails();
        //            List<string> lstEmailparameter = new List<string>();
        //            List<string> lstAttachment = new List<string>();
        //            List<Attachments> lstAttachments = new List<Attachments>();
        //            APIClientHelper lobjcehelper = new APIClientHelper();
        //            lstEmailparameter.Add(pstrOTPstirng);
        //            lstEmailparameter.Add(pobjMemberDetails.LastName);
        //            //lstEmailparameter.Add(lstrexpirydt); //new line of code
        //            lobjEmailDetail.TemplateCode = lsrtTemplateLangCode + pstrTemplateCode;
        //            lobjEmailDetail.ListParameter = lstEmailparameter;
        //            lobjEmailDetail.AttachmentList = lstAttachment;
        //            lobjEmailDetail.To = pobjMemberDetails.Email;
        //            lobjEmailDetail.ProgramId = Convert.ToString(pobjMemberDetails.ProgramId);
        //            lobjEmailDetail.MemberId = Convert.ToString(pobjMemberDetails.MemberRelationsList.Find(lobj => lobj.RelationType.Equals(RelationType.LBMS)).RelationReference);
        //            lblnEmailSend = lobjcehelper.InsertEmailDetails(lobjEmailDetail, lstAttachments, lstrToken);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        LoggingAdapter.WriteLog("OTP password error :" + ex.Message);
        //        throw ex;
        //    }
        //    return lblnEmailSend;
        //}

        private bool SendOTPEmail(MemberDetails pobjMemberDetails, string pstrOTPstirng, string pstrTemplateCode, string redemption_type)
        {
            bool lblnEmailSend = false;
            try
            {
                NICEmailDetailsResponse lobjEmailResponse = new NICEmailDetailsResponse();
                APIClientHelper lobjcehelper = new APIClientHelper();
                if (pobjMemberDetails.Email != string.Empty)
                {
                    NICEmailDetails lobjEmailDetail = new NICEmailDetails();
                    lobjEmailDetail.CE_Event = pstrTemplateCode;
                    lobjEmailDetail.relation_reference = Convert.ToString(pobjMemberDetails.MemberRelationsList.Find(lobj => lobj.RelationType.Equals(RelationType.LBMS)).RelationReference);
                    lobjEmailDetail.program_id = Convert.ToInt32(pobjMemberDetails.ProgramId);
                    lobjEmailDetail.to_email = pobjMemberDetails.Email;
                    lobjEmailDetail.full_name = pobjMemberDetails.FullName;
                    lobjEmailDetail.otp = pstrOTPstirng;
                    lobjEmailDetail.to_mobile = pobjMemberDetails.MobileNumber;
                    lobjEmailDetail.redemption_type = redemption_type;
                    string parameters = JsonConvert.SerializeObject(lobjEmailDetail);
                    string lstrToken = GetAuthTokenforWebAPI();
                    lobjEmailResponse = lobjcehelper.NICEmailDetails(parameters, lstrToken);
                    if (lobjEmailResponse != null)
                    {
                        if (lobjEmailResponse.results.IsSucessful)
                        {
                            lblnEmailSend = true;
                        }
                        else
                        {
                            LoggingAdapter.WriteLog("Send Communication UnSuccessful :" + lobjEmailResponse.results.ExceptionMessage);
                            lblnEmailSend = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("Send Communication error :" + ex.Message);
            }
            return lblnEmailSend;
        }

        private bool SendOTPSMS(MemberDetails lobjMemberDetails, string pstrOTPstring, DateTime pdtExpirydatetime, string pstrTemplateCode)
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

                    string lstrToken = GetAuthTokenforWebAPI();
                    string lstrexpirydt = pdtExpirydatetime.ToString("dd/MM/yyyy");
                    //Communication Engine Call For SMS. 
                    APIClientHelper lobjcehelper = new APIClientHelper();
                    SmsDetails lobjSmsDetail = new SmsDetails();
                    List<string> lstSmsparameter = new List<string>();
                    lstSmsparameter.Add(pstrOTPstring);
                    lstSmsparameter.Add(lobjMemberDetails.FullName);
                    //lstSmsparameter.Add(lstrexpirydt);
                    lobjSmsDetail.TemplateCode = lsrtTemplateLangCode + pstrTemplateCode;
                    lobjSmsDetail.ListParameter = lstSmsparameter;
                    lobjSmsDetail.ReceiverMobile = Convert.ToString(lobjMemberDetails.MobileNumber);
                    lobjSmsDetail.ProgramId = Convert.ToString(lobjMemberDetails.ProgramId);
                    lobjSmsDetail.MemberId = Convert.ToString(lobjMemberDetails.MemberRelationsList.Find(lobj => lobj.RelationType.Equals(RelationType.LBMS)).RelationReference);
                    lblnSMSSend = lobjcehelper.InsertSmsDetails(lobjSmsDetail, lstrToken);
                }
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("OTP SMS ERROR :" + ex.Message);
            }
            return lblnSMSSend;
        }

        #region Voucher
        public List<VoucherSummary> CreateVoucher(MemberDetails pobjMemberDetails, IBEAccountTransactionDetails pobjIBEAccountTransactionDetails, List<VoucherDetails> pobjListVoucherDetails, string pstrCurrency)
        {
            string strRedeemMilesResponse = string.Empty;
            string strRollBackMilesResponse = string.Empty;
            bool lboolRollBackResponse = false;
            bool lboolRedeemResponseDA = false;
            bool lboolRollBackResponseDA = false;
            bool statusFlag = false;
            string lstrBlockDeposiAccount = string.Empty;
            DepositAccountClientHelper lobjDepositAccountClientHelper = new DepositAccountClientHelper();
            PGHelper lobjPGHelper = new PGHelper();
            List<ProgramCurrencyDefinition> lobjListProgramCurrencyDefinition = new List<ProgramCurrencyDefinition>();
            ProgramConfigs lobjProgramConfigs = new ProgramConfigs();
            ProgramDefinition lobjProgramDefinition = new ProgramDefinition();
            ProgramCurrencyDefinition lobjProgramCurrencyDefinition = new ProgramCurrencyDefinition();

            lobjProgramDefinition = lobjProgramConfigs.GetProgramDetails(lstrProgramName);
            lobjListProgramCurrencyDefinition = lobjProgramConfigs.GetAllCurrencyDefinition(lobjProgramDefinition.ProgramId);
            lobjProgramCurrencyDefinition = lobjListProgramCurrencyDefinition.Find(lobj => lobj.Currency.Equals(pstrCurrency));

            string lstrVoucherNo = string.Empty;
            List<string> lstrVoucherNoList = new List<string>();
            List<VoucherSummary> lobjListVoucherSummary = new List<VoucherSummary>();


            RedemptionDetails lobjRedemptionDetails = new RedemptionDetails();
            lobjRedemptionDetails.Currency = pstrCurrency;
            lobjRedemptionDetails.RelationReference = pobjMemberDetails.MemberRelationsList.Find(lobj => lobj.RelationType.Equals(RelationType.LBMS)).RelationReference;

            try
            {
                LoggingAdapter.WriteLog("Create Voucher Start Member " + lobjRedemptionDetails.RelationReference + " Time - " + DateTime.Now);
                for (int i = 0; i < pobjListVoucherDetails.Count; i++)
                {
                    VoucherSummary lobjVoucherSummary = new VoucherSummary();
                    lobjVoucherSummary.Amount = Convert.ToInt32(pobjListVoucherDetails[i].Value);

                    int pintTotalPoints = Convert.ToInt32(pobjListVoucherDetails[i].Value / lobjProgramCurrencyDefinition.VoucherRate);
                    lobjRedemptionDetails.Points = pintTotalPoints;
                    lobjRedemptionDetails.Amount = pobjListVoucherDetails[i].Value;
                    LoggingAdapter.WriteLog("DoOtherRedemption Request Amount " + lobjRedemptionDetails.Amount + " Points - " + lobjRedemptionDetails.Points + " RelationReference -  " + pobjMemberDetails.MemberRelationsList.Find(lobj => lobj.RelationType.Equals(RelationType.LBMS)).RelationReference + " Currency - " + lobjRedemptionDetails.Currency);
                    strRedeemMilesResponse = lobjPGHelper.DoOtherRedemption(lobjRedemptionDetails.Amount, lobjRedemptionDetails.Amount, lobjRedemptionDetails.Points, string.Empty, string.Empty, pobjMemberDetails.MemberRelationsList.Find(lobj => lobj.RelationType.Equals(RelationType.LBMS)).RelationReference, pobjMemberDetails.MemberRelationsList.Find(lobj => lobj.RelationType.Equals(RelationType.LBMS)).WebPassword, "Redemption InfiVoucher", Convert.ToInt32(RelationType.LBMS), Convert.ToInt32(LoyaltyTxnType.InfiVoucher), lobjRedemptionDetails.Currency, lstrMerchantId, lstrMerchantUserName, lstrMerchantPassword);
                    LoggingAdapter.WriteLog(" DoOtherRedemption Response - " + strRedeemMilesResponse);
                    if (strRedeemMilesResponse != string.Empty)
                    {
                        if (pobjIBEAccountTransactionDetails != null)
                        {
                            pobjIBEAccountTransactionDetails.Amount = pobjListVoucherDetails[i].Value;
                            lstrBlockDeposiAccount = lobjDepositAccountClientHelper.BlockAccount(pobjIBEAccountTransactionDetails);
                            LoggingAdapter.WriteLog("BookingIntegrationFacade CreateVoucher DABlockAccount : " + lstrBlockDeposiAccount);


                            if (lstrBlockDeposiAccount != string.Empty)
                            {
                                pobjListVoucherDetails[i].MemberId = pobjMemberDetails.MemberRelationsList[0].RelationReference;
                                pobjListVoucherDetails[i].EmailId = pobjMemberDetails.Email;
                                pobjListVoucherDetails[i].CustomerName = pobjMemberDetails.LastName;
                                pobjListVoucherDetails[i].MobileNumber = pobjMemberDetails.MobileNumber;
                                pobjListVoucherDetails[i].CreatedBy = pobjMemberDetails.MemberRelationsList[0].RelationReference;

                                VoucherHelper lobjVoucherHelper = new VoucherHelper();
                                Core.Platform.Common.Entities.Response lobjCreateResponse = null;

                                lobjCreateResponse = new Core.Platform.Common.Entities.Response();

                                try
                                {
                                    lobjCreateResponse = lobjVoucherHelper.CreateVoucher(pobjListVoucherDetails[i]);
                                }
                                catch (Exception ex)
                                {
                                    LoggingAdapter.WriteLog("BookingIntegrationFacade CreateVoucher Client : " + ex.Message + Environment.NewLine + "Stack Trace: " + ex.StackTrace);
                                    statusFlag = false;
                                }

                                if (lobjCreateResponse != null && lobjCreateResponse.IsSucessful)
                                {
                                    lstrVoucherNo = JSONSerialization.Deserialize<string>(lobjCreateResponse.ReturnObject);
                                    if (lstrVoucherNo != string.Empty)
                                    {
                                        lobjVoucherSummary.VoucherNo = lstrVoucherNo;
                                        pobjIBEAccountTransactionDetails.Description = pobjListVoucherDetails[i].VoucherDescription;
                                        pobjIBEAccountTransactionDetails.BookingReference = lstrVoucherNo;
                                        pobjIBEAccountTransactionDetails.TransactionReference = lstrBlockDeposiAccount;
                                        pobjIBEAccountTransactionDetails.TransactionType = IBETransactionType.Blocked;
                                        lboolRedeemResponseDA = lobjDepositAccountClientHelper.RedeemAccount(pobjIBEAccountTransactionDetails);

                                        LoggingAdapter.WriteLog("BookingIntegrationFacade CreateVoucher DARedeemAccount : " + lboolRedeemResponseDA);

                                        if (lboolRedeemResponseDA)
                                        {
                                            statusFlag = true;
                                            lobjVoucherSummary.Status = true;
                                            lstrVoucherNoList.Add(lstrVoucherNo);
                                        }
                                        else
                                        {
                                            statusFlag = false;
                                        }
                                    }

                                }
                            }
                        }
                        else
                        {
                            pobjListVoucherDetails[i].MemberId = pobjMemberDetails.MemberRelationsList.Find(lobj => lobj.RelationType.Equals(RelationType.LBMS)).RelationReference;
                            pobjListVoucherDetails[i].EmailId = pobjMemberDetails.Email;
                            pobjListVoucherDetails[i].CustomerName = pobjMemberDetails.LastName;
                            pobjListVoucherDetails[i].MobileNumber = pobjMemberDetails.MobileNumber;
                            pobjListVoucherDetails[i].CreatedBy = pobjMemberDetails.MemberRelationsList.Find(lobj => lobj.RelationType.Equals(RelationType.LBMS)).RelationReference;

                            VoucherHelper lobjVoucherHelper = new VoucherHelper();
                            Core.Platform.Common.Entities.Response lobjCreateResponse = null;

                            lobjCreateResponse = new Core.Platform.Common.Entities.Response();

                            try
                            {
                                lobjCreateResponse = lobjVoucherHelper.CreateVoucher(pobjListVoucherDetails[i]);
                            }
                            catch (Exception ex)
                            {
                                LoggingAdapter.WriteLog("BookingIntegrationFacade CreateVoucher Client : " + ex.Message + Environment.NewLine + "Stack Trace: " + ex.StackTrace);
                                statusFlag = false;
                            }

                            if (lobjCreateResponse != null && lobjCreateResponse.IsSucessful)
                            {
                                LoggingAdapter.WriteLog("CreateVoucher service response IsSucessful-True - " + strRedeemMilesResponse);
                                lstrVoucherNo = JSONSerialization.Deserialize<string>(lobjCreateResponse.ReturnObject);
                                if (lstrVoucherNo != string.Empty)
                                {
                                    lobjVoucherSummary.VoucherNo = lstrVoucherNo;
                                    statusFlag = true;
                                    lobjVoucherSummary.Status = true;
                                    lstrVoucherNoList.Add(lstrVoucherNo);
                                }
                                else
                                {
                                    statusFlag = false;
                                }

                            }
                            else
                            {
                                LoggingAdapter.WriteLog("CreateVoucher service response IsSucessful-False - " + strRedeemMilesResponse);
                            }
                        }

                        if (!statusFlag)
                        {
                            lboolRollBackResponse = lobjPGHelper.RollBackTransaction(strRedeemMilesResponse, pobjMemberDetails.MemberRelationsList.Find(lob => lob.RelationType.Equals(RelationType.LBMS)).RelationReference, pobjListVoucherDetails[i].VoucherDescription, lstrMerchantId, lstrMerchantUserName, lstrMerchantPassword);
                            if (pobjIBEAccountTransactionDetails != null && lboolRollBackResponse)
                            {
                                pobjIBEAccountTransactionDetails.TransactionReference = lstrBlockDeposiAccount;
                                pobjIBEAccountTransactionDetails.TransactionType = IBETransactionType.Blocked;
                                pobjIBEAccountTransactionDetails.Description = pobjListVoucherDetails[i].VoucherDescription;
                                lboolRollBackResponseDA = lobjDepositAccountClientHelper.RollBackAccount(pobjIBEAccountTransactionDetails);
                            }
                        }

                    }
                    else
                    {
                        LoggingAdapter.WriteLog("Do Other Redemption Response Null");
                    }
                    lobjListVoucherSummary.Add(lobjVoucherSummary);
                }
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("BookingintegrationFacade CreateVoucher : " + Environment.NewLine + ex.StackTrace);
            }
            LoggingAdapter.WriteLog("Create Voucher End Member " + lobjRedemptionDetails.RelationReference + " Time - " + DateTime.Now);
            return lobjListVoucherSummary;
            //return lstrVoucherNoList;
        }
        #endregion

        #region WEBAPI Call

        public string GetAuthTokenforWebAPI()
        {
            APIClientHelper lobjAPIClientHelper = new APIClientHelper();
            string AccessToken = string.Empty;
            TokenModel authTokenResponse = null;
            try
            {
                if (HttpContext.Current.Session["AccessTokenWebAPI"] != null && HttpContext.Current.Session["AccessTokenTimeWebAPI"] != null)
                {
                    authTokenResponse = HttpContext.Current.Session["AccessTokenWebAPI"] as TokenModel;
                    DateTime dtAccessTokenTime = Convert.ToDateTime(HttpContext.Current.Session["AccessTokenTimeWebAPI"]);
                    TimeSpan dt = DateTime.Now - dtAccessTokenTime;
                    if (dt.TotalMinutes > 25)
                        authTokenResponse = null;
                }
            }
            catch { }
            if (authTokenResponse == null)
            {
                try
                {
                    CoreAuthTokenRequest authTokenRequest = new CoreAuthTokenRequest();
                    authTokenRequest.client_id = ConfigurationManager.AppSettings["AuthAPIClientId"];
                    authTokenRequest.client_secret = ConfigurationManager.AppSettings["AuthAPIClientSecret"];
                    authTokenRequest.grant_type = ConfigurationManager.AppSettings["AuthAPIGrantType"];
                    authTokenResponse = lobjAPIClientHelper.GetAuthTokenforWebAPI(authTokenRequest);
                    try
                    {
                        HttpContext.Current.Session["AccessTokenWebAPI"] = authTokenResponse;
                        HttpContext.Current.Session["AccessTokenTimeWebAPI"] = DateTime.Now;
                    }
                    catch { }
                }
                catch (Exception ex)
                {
                    LoggingAdapter.WriteLog("BookingIntegrationFacade GetAuthTokenforWebAPI AccessToken null");
                    LoggingAdapter.WriteLog("BookingIntegrationFacade GetAuthTokenforWebAPI Ex-" + ex.InnerException + ex.StackTrace + ex.Message);
                }
            }
            AccessToken = authTokenResponse.results.token;
            return AccessToken;
        }
        public string RedeemPoints(float pfltAmount, int pintPoints, string pstrRelationReference, string pstrPassword, string pstrNarration, int pintLoyaltyTxnType, string pstrProgramCurrency, MemberDetails pobjMemberDetails)
        {
            string lstrToken = GetAuthTokenforWebAPI();
            string strRedeemMilesResponse = string.Empty;


            PGRedeemRequest lobjPGRedeemRequest = new PGRedeemRequest();
            lobjPGRedeemRequest.RelationReference = pstrRelationReference;
            lobjPGRedeemRequest.Amount = Convert.ToDecimal(pfltAmount);
            lobjPGRedeemRequest.Points = pintPoints;
            lobjPGRedeemRequest.TransactionCurrency = pstrProgramCurrency;
            lobjPGRedeemRequest.LoyaltyTxnType = pintLoyaltyTxnType;
            lobjPGRedeemRequest.MerchantName = pstrNarration;
            lobjPGRedeemRequest.ProgramId = pobjMemberDetails.ProgramId;
            lobjPGRedeemRequest.RelationType = Convert.ToInt32(RelationType.LBMS);

            APIClientHelper lobjAPIClientHelper = new APIClientHelper();
            strRedeemMilesResponse = lobjAPIClientHelper.RedeemPoints(lobjPGRedeemRequest, lstrToken);

            return strRedeemMilesResponse;
        }

        public bool RollBackTransaction(string pstrExternalReference, string pstrRelationReference, string pstrMerchantName)
        {
            bool lboolRollBackResponse = false;
            string lstrToken = GetAuthTokenforWebAPI();
            APIClientHelper lobjAPIClientHelper = new APIClientHelper();
            PGReversalRequest lobjPGReversalRequest = new PGReversalRequest();
            lobjPGReversalRequest.ExternalReference = pstrExternalReference;
            lobjPGReversalRequest.RelationReference = pstrRelationReference;
            lobjPGReversalRequest.MerchantName = pstrMerchantName;

            lboolRollBackResponse = lobjAPIClientHelper.ReversalPoints(lobjPGReversalRequest, lstrToken);
            return lboolRollBackResponse;
        }

        public OTPDetails GenerateOTPDetails(OTPDetails pobjOTPDetails)
        {
            try
            {
                string lstrToken = GetAuthTokenforWebAPI();
                OTPDetails lobjOTPDetails = null;
                APIClientHelper lobjAPIClientHelper = new APIClientHelper();
                lobjOTPDetails = lobjAPIClientHelper.GenerateOTPDetails(pobjOTPDetails, lstrToken);

                return lobjOTPDetails;
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("BookingFacade GenerateOTPDetails:" + ex.Message + ex.StackTrace);
                return null;
            }
        }

        #endregion
        static string UppercaseFirst(string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }
            char[] a = s.ToCharArray();
            a[0] = char.ToUpper(a[0]);
            return new string(a);
        }
    }
}
