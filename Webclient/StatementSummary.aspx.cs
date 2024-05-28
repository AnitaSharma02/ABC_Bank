using System;
using System.Web;
using System.Web.UI;
using Core.Platform.Transactions.Entites;
using ABC.Model;
using Core.Platform.Member.Entites;
using System.Text;
using Core.Platform.ExpirySchedule.Entities;
using Core.Platform.TransactionSummary.Entites;
using Framework.EnterpriseLibrary.Adapters;
using System.Web.Services;

public partial class StatementSummary : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            // Stop Caching in IE
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            // Stop Caching in Firefox
            Response.Cache.SetNoStore();
            if (Session["MemberDetails"] == null)
            {
                HttpContext.Current.Session["MyAccount"] = "MyAccount";
                Response.Redirect("Login.aspx", false);
            }
        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("StatementSummary.aspx - Page_Load Exception: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
        }
    }
    [WebMethod]
    public static string[] GetStatementSummary()
    {
        try
        {
            MemberDetails objMemberDetails = new MemberDetails();
            objMemberDetails = HttpContext.Current.Session["MemberDetails"] as MemberDetails;
            ABCModel lobjModel = new ABCModel();
            string lstrCurrency = lobjModel.GetDefaultCurrency();
            SearchTransactions objSearchTransactions = new SearchTransactions();
            objSearchTransactions.RelationReference = objMemberDetails.MemberRelationsList.Find(lobj => lobj.RelationType.Equals(RelationType.LBMS)).RelationReference;
            objSearchTransactions.ProgramId = objMemberDetails.ProgramId;
            objSearchTransactions.RelationType = Convert.ToInt32(RelationType.LBMS);
            objSearchTransactions.TransactionCurrency = lstrCurrency;
            TxnSummaryDetails lobjTransactionSummaryDetails = new TxnSummaryDetails();
            lobjTransactionSummaryDetails = lobjModel.GetMemberStatementSummary(objSearchTransactions);
            string lstrlExpiredPoints = string.Empty;
            string lstrlRedeemedmile = string.Empty;
            string lstrlBonusmile = string.Empty;
            string lstrlSpendmile = string.Empty;
            string lstrlPurchasemile = string.Empty;
            if (lobjTransactionSummaryDetails != null)
            {
                lstrlExpiredPoints = Convert.ToString(lobjModel.FloatToThousandSeperated(lobjTransactionSummaryDetails.ExpiredPoints));
                lstrlRedeemedmile = Convert.ToString(lobjModel.FloatToThousandSeperated(lobjTransactionSummaryDetails.Redeem));
                lstrlBonusmile = Convert.ToString(lobjModel.FloatToThousandSeperated(lobjTransactionSummaryDetails.Bonus));
                lstrlSpendmile = Convert.ToString(lobjModel.FloatToThousandSeperated(lobjTransactionSummaryDetails.Spend));
                lstrlPurchasemile = Convert.ToString(lobjModel.FloatToThousandSeperated(lobjTransactionSummaryDetails.Purchased));
            }
            string lstrAvailablePoints = Convert.ToString(lobjModel.FloatToThousandSeperated(
                lobjModel.CheckAvailbility(objMemberDetails.MemberRelationsList.Find(lobj => lobj.RelationType.Equals(RelationType.LBMS)).RelationReference
                , Convert.ToInt16(RelationType.LBMS)
                , lstrCurrency
                , objMemberDetails.ProgramId)));
            return new[] { lstrlExpiredPoints, lstrlRedeemedmile, lstrlBonusmile, lstrlSpendmile, lstrlPurchasemile, lstrAvailablePoints };
        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("StatementSummary.aspx - GetStatementSummary Exception: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
            return null;    
        }
    }
    [WebMethod]
    public static string[] BindPointsExpiryDetails()
    {
        string lstrQ1Points = string.Empty;
        string lstrQ2Points = string.Empty;
        string lstrQ3Points = string.Empty;
        string lstrQ4Points = string.Empty;

        string lstrQ1Year = string.Empty;
        string lstrQ2Year = string.Empty;
        string lstrQ3Year = string.Empty;
        string lstrQ4Year = string.Empty;
        ScheduleExpiry lobjScheduleExpiry = new ScheduleExpiry();
        StringBuilder sbPointExpMonthlyHTML = new StringBuilder();
        try
        {
            ABCModel lobjModel = new ABCModel();
            string lstrCurrency = lobjModel.GetDefaultCurrency();
            MemberDetails lobjMemberDetails = HttpContext.Current.Session["MemberDetails"] as MemberDetails;

            lobjScheduleExpiry = lobjModel.GetExpirySchedule(DateTime.Now.Year, lobjMemberDetails);

            if (lobjMemberDetails != null)
            {
                MemberRelation lobjMemberRelation = lobjMemberDetails.MemberRelationsList.Find(lobj => lobj.RelationType.Equals(RelationType.LBMS));
                if (lobjMemberRelation != null)
                {
                    lobjScheduleExpiry.RelationReference = lobjMemberRelation.RelationReference;
                    lobjScheduleExpiry.RelationType = Convert.ToInt32(RelationType.LBMS);
                    lobjScheduleExpiry.TransactionCurrency = lstrCurrency;
                    lobjScheduleExpiry = lobjModel.GetTransactionExpirySchedule(lobjScheduleExpiry);

                    if (lobjScheduleExpiry.ExpiryPeriod != null)
                    {
                        if (lobjScheduleExpiry.Period.ToLower().Equals("quarterly"))
                        {
                            if (lobjScheduleExpiry.ExpiryPeriod[0].ScheduleDate.Date.Equals(new DateTime(DateTime.Now.Year, 6, 30)))
                            {
                                lstrQ1Points = Convert.ToString(lobjModel.FloatToThousandSeperated(lobjScheduleExpiry.ExpiryPeriod[0].TotalExpiredPoints));
                                lstrQ2Points = Convert.ToString(lobjModel.FloatToThousandSeperated(lobjScheduleExpiry.ExpiryPeriod[1].TotalExpiredPoints));
                                lstrQ3Points = Convert.ToString(lobjModel.FloatToThousandSeperated(lobjScheduleExpiry.ExpiryPeriod[2].TotalExpiredPoints));
                                lstrQ4Points = Convert.ToString(lobjModel.FloatToThousandSeperated(lobjScheduleExpiry.ExpiryPeriod[3].TotalExpiredPoints));

                                lstrQ1Year = "Apr - Jun " + lobjScheduleExpiry.ExpiryPeriod[0].ScheduleDate.Date.Year;
                                lstrQ2Year = "Jul - Sep " + lobjScheduleExpiry.ExpiryPeriod[1].ScheduleDate.Date.Year;
                                lstrQ3Year = "Oct - Dec " + lobjScheduleExpiry.ExpiryPeriod[2].ScheduleDate.Date.Year;
                                lstrQ4Year = "Jan - Mar " + lobjScheduleExpiry.ExpiryPeriod[3].ScheduleDate.Date.Year;
                            }

                            if (lobjScheduleExpiry.ExpiryPeriod[0].ScheduleDate.Date.Equals(new DateTime(DateTime.Now.Year, 9, 30)))
                            {
                                lstrQ2Points = Convert.ToString(lobjModel.FloatToThousandSeperated(lobjScheduleExpiry.ExpiryPeriod[0].TotalExpiredPoints));
                                lstrQ3Points = Convert.ToString(lobjModel.FloatToThousandSeperated(lobjScheduleExpiry.ExpiryPeriod[1].TotalExpiredPoints));
                                lstrQ4Points = Convert.ToString(lobjModel.FloatToThousandSeperated(lobjScheduleExpiry.ExpiryPeriod[2].TotalExpiredPoints));
                                lstrQ1Points = Convert.ToString(lobjModel.FloatToThousandSeperated(lobjScheduleExpiry.ExpiryPeriod[3].TotalExpiredPoints));

                                lstrQ2Year = "Jul - Sep " + lobjScheduleExpiry.ExpiryPeriod[0].ScheduleDate.Date.Year;
                                lstrQ3Year = "Oct - Dec " + lobjScheduleExpiry.ExpiryPeriod[1].ScheduleDate.Date.Year;
                                lstrQ4Year = "Jan - Mar " + lobjScheduleExpiry.ExpiryPeriod[2].ScheduleDate.Date.Year;
                                lstrQ1Year = "Apr - Jun " + lobjScheduleExpiry.ExpiryPeriod[3].ScheduleDate.Date.Year;
                            }

                            if (lobjScheduleExpiry.ExpiryPeriod[0].ScheduleDate.Date.Equals(new DateTime(DateTime.Now.Year, 12, 31)))
                            {
                                lstrQ3Points = Convert.ToString(lobjModel.FloatToThousandSeperated(lobjScheduleExpiry.ExpiryPeriod[0].TotalExpiredPoints));
                                lstrQ4Points = Convert.ToString(lobjModel.FloatToThousandSeperated(lobjScheduleExpiry.ExpiryPeriod[1].TotalExpiredPoints));
                                lstrQ1Points = Convert.ToString(lobjModel.FloatToThousandSeperated(lobjScheduleExpiry.ExpiryPeriod[2].TotalExpiredPoints));
                                lstrQ2Points = Convert.ToString(lobjModel.FloatToThousandSeperated(lobjScheduleExpiry.ExpiryPeriod[3].TotalExpiredPoints));

                                lstrQ3Year = "Oct - Dec " + lobjScheduleExpiry.ExpiryPeriod[0].ScheduleDate.Date.Year;
                                lstrQ4Year = "Jan - Mar " + lobjScheduleExpiry.ExpiryPeriod[1].ScheduleDate.Date.Year;
                                lstrQ1Year = "Apr - Jun " + lobjScheduleExpiry.ExpiryPeriod[2].ScheduleDate.Date.Year;
                                lstrQ2Year = "Jul - Sep " + lobjScheduleExpiry.ExpiryPeriod[3].ScheduleDate.Date.Year;
                            }

                            if (lobjScheduleExpiry.ExpiryPeriod[0].ScheduleDate.Date.Equals(new DateTime(DateTime.Now.Year, 3, 31)))
                            {
                                lstrQ4Points = Convert.ToString(lobjModel.FloatToThousandSeperated(lobjScheduleExpiry.ExpiryPeriod[0].TotalExpiredPoints));
                                lstrQ1Points = Convert.ToString(lobjModel.FloatToThousandSeperated(lobjScheduleExpiry.ExpiryPeriod[1].TotalExpiredPoints));
                                lstrQ2Points = Convert.ToString(lobjModel.FloatToThousandSeperated(lobjScheduleExpiry.ExpiryPeriod[2].TotalExpiredPoints));
                                lstrQ3Points = Convert.ToString(lobjModel.FloatToThousandSeperated(lobjScheduleExpiry.ExpiryPeriod[3].TotalExpiredPoints));

                                lstrQ4Year = "Jan - Mar " + lobjScheduleExpiry.ExpiryPeriod[0].ScheduleDate.Date.Year;
                                lstrQ1Year = "Apr - Jun " + lobjScheduleExpiry.ExpiryPeriod[1].ScheduleDate.Date.Year;
                                lstrQ2Year = "Jul - Sep " + lobjScheduleExpiry.ExpiryPeriod[2].ScheduleDate.Date.Year;
                                lstrQ3Year = "Oct - Dec " + lobjScheduleExpiry.ExpiryPeriod[3].ScheduleDate.Date.Year;
                            }
                        }
                        else if (lobjScheduleExpiry.Period.ToLower().Equals("monthly"))
                        {
                            foreach (var item in lobjScheduleExpiry.ExpiryPeriod)
                            {
                                sbPointExpMonthlyHTML.Append("<li class=\"imbr0 boxShadows\">");
                                sbPointExpMonthlyHTML.Append("<p class=\"bTxt\">" + setDate(item.ScheduleDate) + "</p>");
                                sbPointExpMonthlyHTML.Append("<p class=\"nTxt\"></p>");
                                sbPointExpMonthlyHTML.Append("<p class=\"bHead\">" + item.TotalExpiredPoints + "</p>");
                                sbPointExpMonthlyHTML.Append("</li>");
                            }
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("BindPointsExpiryDetails Ex -- " + ex.InnerException + Environment.NewLine + "Msg --" + ex.Message + Environment.NewLine + "StackTrace -- " + ex.StackTrace);
        }
        return new[] { lobjScheduleExpiry.Period.ToLower(), lstrQ1Points, lstrQ2Points, lstrQ3Points, lstrQ4Points, lstrQ1Year, lstrQ2Year, lstrQ3Year, lstrQ4Year, Convert.ToString(sbPointExpMonthlyHTML) };
    }
    private static string setDate(DateTime dateTime)
    {
        string strNewdate = String.Format("{0:d MMM, yyyy}", dateTime);
        return strNewdate;
    }
}
