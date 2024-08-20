using System;
using System.Collections.Generic;
using System.Web;
using Core.Platform.ExpirySchedule.Entities;
using Core.Platform.Member.Entites;
using Core.Platform.ProgramMaster.Entities;
using Framework.EnterpriseLibrary.Adapters;
using ABC.Model;
public partial class PointsExpiry : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["MemberDetails"] == null)
        {
            Response.Redirect("Login.aspx", false);
        }
        if (!IsPostBack)
        {
            try
            {
                GetProductProgramCurrencyDefinition();
                if (Session["ExpiryProgramCurrency"].ToString().Equals(System.Configuration.ConfigurationManager.AppSettings["ProgramCurrency"]))
                {
                    List<int> lintYearlst = new List<int>();
                    int lintYear = DateTime.Now.Year;
                    lintYearlst.Add(lintYear);
                    for (int i = 0; i < 3; i++)
                    {
                        lintYear = lintYearlst[i] + 1;
                        lintYearlst.Add(lintYear);
                    }
                    dtYear.DataSource = lintYearlst;
                    dtYear.DataBind();
                    ABCModel lobjModel = new ABCModel();
                    ScheduleExpiry lobjScheduleExpiry = new ScheduleExpiry();
                    MemberDetails lobjMemberDetails = null;
                    lobjMemberDetails = Session["MemberDetails"] as MemberDetails;
                    lobjScheduleExpiry = lobjModel.GetExpirySchedule(Convert.ToInt32(dtYear.SelectedItem.Text), lobjMemberDetails);
                    if (lobjMemberDetails != null)
                    {
                        MemberRelation lobjMemberRelation = lobjMemberDetails.MemberRelationsList.Find(lobj => lobj.RelationType.Equals(RelationType.LBMS));
                        if (lobjMemberRelation != null)
                        {
                            lobjScheduleExpiry.RelationReference = lobjMemberRelation.RelationReference;
                            lobjScheduleExpiry.RelationType = Convert.ToInt32(RelationType.LBMS);
                            lobjScheduleExpiry.TransactionCurrency = Session["ExpiryProgramCurrency"].ToString();
                            lobjScheduleExpiry = lobjModel.GetTransactionExpirySchedule(lobjScheduleExpiry);
                            HttpContext.Current.Session["dtYearvalue"] = Convert.ToString(DateTime.Now.Year);
                            BindTable(lobjScheduleExpiry.ExpiryPeriod);
                            ScheduleExpiry lobjScheduleExpiryOnDate = new ScheduleExpiry();
                            lobjScheduleExpiryOnDate = lobjModel.GetNextExpiredPointsOnDate(lobjScheduleExpiry, lintYear + 3);
                            if (lobjScheduleExpiryOnDate.ExpiryPeriod[0] == null)
                            {
                                divExpiredon.Style.Add("display", "none");
                            }
                            else
                            {
                                divExpiredon.Style.Add("display", "block");
                                lblMiles.Text = Convert.ToString(lobjModel.FloatToThousandSeperated(lobjScheduleExpiryOnDate.ExpiryPeriod[0].TotalExpiredPoints));
                                lblDate.Text = lobjScheduleExpiryOnDate.ExpiryPeriod[0].ScheduleDate.ToString("d MMMM, yyyy");
                            }
                        }
                    }
                    Session["ExpirySchedule"] = lobjScheduleExpiry;
                }
                else
                {
                    divExpiredon.Style.Add("display", "none");
                }
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("Point Expiry Page load -" + ex.Message + Environment.NewLine + "Stack Trace-" + ex.StackTrace);
            }
        }
    }

    private void BindTable(List<ExpiryPeriod> ExpiryPeriod)
    {
        string strExpiryPeriod = "<div class='col-12 mb-3'><div class='row'><div class='col-6 pr-0 text-center bg-primary py-3 border-right'><p class='heading-bold text-white' data-i18n='pe-period'>Period</p></div><div class='col-6 pl-0 text-center bg-primary py-3'><p class='heading-bold text-white' data-i18n='pe-total-points'>Total Points</p></div></div><div class='row'>";
        string strExpiryPoints = string.Empty;
        ABCModel lobjModel = new ABCModel();
        for (int icount = 0; icount < ExpiryPeriod.Count; icount++)
        {
            string[] temp = (Convert.ToString(ExpiryPeriod[icount].ScheduleDate).Split('/'));

            if (Session["dtYearvalue"].ToString() == temp[2].Substring(0, 4))
            {

                strExpiryPeriod += "<div class='col-6 pr-0 text-center bg-white border-left border-right border-bottom py-3'><p>"
                    + setDate(ExpiryPeriod[icount].ScheduleDate) + "</p></div><div class='col-6 pl-0 text-center bg-white border-right border-bottom py-3'><p>"
                    + Convert.ToString(lobjModel.FloatToThousandSeperated(ExpiryPeriod[icount].TotalExpiredPoints)) + "</p></div>";
            }
        }
        rptExpirySchedule.InnerHtml = strExpiryPeriod + "</div>";
    }

    private string setDate(DateTime dateTime)
    {
        string strNewdate = String.Format("{0:d MMM yyyy}", dateTime);
        return strNewdate;
    }

    public void GetProductProgramCurrencyDefinition()
    {
        try
        {
            string lstrResponse = string.Empty;
            ABCModel lobjModel = new ABCModel();
            List<ProgramCurrencyDefinition> lobjListProgramCurrencyDefinition = new List<ProgramCurrencyDefinition>();
            ProgramCurrencyDefinition lobjProgramCurrencyDefinition = new ProgramCurrencyDefinition();
            MemberDetails lobjMemberDetails = new MemberDetails();
            lobjMemberDetails = HttpContext.Current.Session["MemberDetails"] as MemberDetails;
            lobjListProgramCurrencyDefinition = lobjModel.GetProductProgramCurrencyDefinition(lobjMemberDetails);
            if (lobjListProgramCurrencyDefinition.Count > 0)
            {
                lobjProgramCurrencyDefinition = lobjListProgramCurrencyDefinition.Find(lobj => lobj.Currency.Equals(System.Configuration.ConfigurationManager.AppSettings["ProgramCurrency"]));
                if (lobjProgramCurrencyDefinition != null)
                {
                    HttpContext.Current.Session["ExpiryProgramCurrency"] = System.Configuration.ConfigurationManager.AppSettings["ProgramCurrency"];
                }
            }
        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("PointsExpiry.aspx- GetProductProgramCurrencyDefinition Ex: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
        }
    }

    protected void dtYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        ScheduleExpiry lobjScheduleExpiry = Session["ExpirySchedule"] as ScheduleExpiry;
        HttpContext.Current.Session["dtYearvalue"] = dtYear.SelectedItem.Text;
        if (dtYear.SelectedItem.Text != Convert.ToString(DateTime.Now.Year))
        {
            MemberDetails lobjMemberDetails = null;
            lobjMemberDetails = Session["MemberDetails"] as MemberDetails;
            MemberRelation lobjMemberRelation = lobjMemberDetails.MemberRelationsList.Find(lobj => lobj.RelationType.Equals(RelationType.LBMS));
            ABCModel lobjModel = new ABCModel();
            lobjScheduleExpiry.RelationReference = lobjMemberRelation.RelationReference;
            lobjScheduleExpiry.RelationType = Convert.ToInt32(RelationType.LBMS);
            lobjScheduleExpiry.TransactionCurrency = Session["ExpiryProgramCurrency"].ToString();
            lobjScheduleExpiry = lobjModel.GetNextExpirySchedule(lobjScheduleExpiry, Convert.ToInt32(dtYear.SelectedItem.Text));
        }
        BindTable(lobjScheduleExpiry.ExpiryPeriod);
    }
}