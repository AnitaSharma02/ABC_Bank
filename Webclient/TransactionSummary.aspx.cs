using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ABC.Model;
using Core.Platform.Transactions.Entites;
using Core.Platform.ProgramMaster.Entities;
using Core.Platform.Member.Entites;
using System.Data;
using System.Text;
using System.Reflection;
using Framework.EnterpriseLibrary.Adapters;
using TransactionDetailsAdditionalInfo.Entities;
using System.Drawing.Printing;
using System.Web.Services;

public partial class TransactionSummary : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        // Stop Caching in IE
        Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);

        // Stop Caching in Firefox
        Response.Cache.SetNoStore();
        if (Session["MemberDetails"] == null)
        {
            Response.Redirect("Index.aspx", false);
        }
    }

    public static List<ProgramCurrencyDefinition> GetProgramCurrency()
    {
        ABCModel lobjModel = new ABCModel();
        ProgramDefinition lobjProgramDefinition = lobjModel.GetProgramMaster();
        MemberDetails lobjMemberDetails = null;
        lobjMemberDetails =HttpContext.Current.Session["MemberDetails"] as MemberDetails;
        List<ProgramCurrencyDefinition> lobjlistProgramCurrencyDefinition = new List<ProgramCurrencyDefinition>();
        try
        {
            lobjlistProgramCurrencyDefinition = lobjModel.GetProductProgramCurrencyDefinition(lobjMemberDetails);
        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("TransactionSummary.aspx - PageLoad Exception: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
        }
        return lobjlistProgramCurrencyDefinition;
    }
  
    protected void btnExport_Click(object sender, EventArgs e)
    {
        List<TransactionDetails> list = new List<TransactionDetails>();
        try
        {
            ABCModel lobjModel = new ABCModel();
            ProgramDefinition lobjProgramDefinition = lobjModel.GetProgramMaster();
            string lstrCurrencyName = string.Empty;

            List<ProgramCurrencyDefinition> lobjlistProgramCurrencyDefinition = GetProgramCurrency();
            if (lobjlistProgramCurrencyDefinition != null)
            {
                if (lobjlistProgramCurrencyDefinition.Count > 0)
                {
                    lstrCurrencyName = lobjlistProgramCurrencyDefinition[0].Currency;
                }

            }
            SearchTransactions objSearchTransactions = new SearchTransactions();
            objSearchTransactions.RelationReference = ((Session["MemberDetails"] as MemberDetails).MemberRelationsList.Find(lobj => lobj.RelationType.Equals(RelationType.LBMS)).RelationReference);
            objSearchTransactions.ProgramId = lobjProgramDefinition.ProgramId;
            objSearchTransactions.RelationType = Convert.ToInt32(RelationType.LBMS);
            objSearchTransactions.MinimumRange = Convert.ToInt32(1);
            objSearchTransactions.MaximumRange = Convert.ToInt32(100000000);
            objSearchTransactions.TransactionCurrency = lstrCurrencyName;

            if (FromDate.Text == "Enter Date" || Todate.Text == "Enter Date" || FromDate.Text == "" || Todate.Text == "")
            {
                objSearchTransactions.DateFrom = DateTime.Now.AddDays(-30);
                objSearchTransactions.DateTo = DateTime.Now;
                //list = lobjModel.GetMemberTransactionSummary(objSearchTransactions);
            }
            else
            {
                objSearchTransactions.DateFrom = lobjModel.StringToDateTime(FromDate.Text);
                objSearchTransactions.DateTo = lobjModel.StringToDateTime(Todate.Text);
            }
            Framework.EnterpriseLibrary.Adapters.LoggingAdapter.WriteLog("btnExport_Click TransactionSummary Datefrom -:" + objSearchTransactions.DateFrom + "Datefrom -:" + objSearchTransactions.DateTo);

            list = lobjModel.GetMemberTransactionSummaryByDate(objSearchTransactions);


            Framework.EnterpriseLibrary.Adapters.LoggingAdapter.WriteLog("btnExport_Click GetTotalMemberTransaction Count -:" + list.Count());

            // list = (this.Session["TransactionDetails"] as List<TransactionDetails>);
            if (list != null && list.Count > 0)
            {
                var source = from lobj in list
                             select new
                             {
                                 MerchantName = lobj.MerchantName,
                                 Amounts = lobj.Amounts,
                                 Points = lobj.Points,
                                 TransactionDate = lobj.TransactionDate,
                                 ProcessingDate = lobj.ProcessingDate,
                                 LoyaltyTxnType = lobj.LoyaltyTxnType
                             };
                DataTable dataTable = this.ToDataTable(source.ToList());
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    if (dataRow["TransactionDate"] != null)
                    {
                        // dataRow["TransactionDate"] = DateTime.Parse(dataRow["TransactionDate"].ToString()).ToString("dddd dd MMMM yyyy");

                        dataRow["TransactionDate"] = DateTime.Parse(dataRow["TransactionDate"].ToString()).ToString("dd MM yyyy");

                    }
                    if (dataRow["ProcessingDate"] != null)
                    {
                        //dataRow["ProcessingDate"] = DateTime.Parse(dataRow["ProcessingDate"].ToString()).ToString("dddd dd MMMM yyyy");
                        dataRow["ProcessingDate"] = DateTime.Parse(dataRow["ProcessingDate"].ToString()).ToString("dd MM yyyy");
                    }
                }
                dataTable.Columns["MerchantName"].ColumnName = "Description";
                dataTable.Columns["Amounts"].ColumnName = "Amount";
                dataTable.Columns["Points"].ColumnName = "Points";
                dataTable.Columns["LoyaltyTxnType"].ColumnName = "Transaction Type";
                base.Response.Clear();
                base.Response.Buffer = true;
                base.Response.AddHeader("content-disposition", "attachment;filename=TransactionSummary.csv");
                base.Response.Charset = "";
                base.Response.ContentType = "application/text";
                StringBuilder stringBuilder = new StringBuilder();
                for (int i = 0; i < dataTable.Columns.Count; i++)
                {
                    stringBuilder.Append(dataTable.Columns[i].ColumnName + ',');
                }
                stringBuilder.Append("\r\n");
                for (int j = 0; j < dataTable.Rows.Count; j++)
                {
                    for (int k = 0; k < dataTable.Columns.Count; k++)
                    {
                        stringBuilder.Append(dataTable.Rows[j][k].ToString().Replace(",", "") + ',');
                    }
                    stringBuilder.Append("\r\n");
                }
                base.Response.Output.Write(stringBuilder.ToString());
                base.Response.Flush();
                base.Response.End();
            }
        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("TransactionSummary.aspx - btnExport_Click Exception: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
        }
    }

    public DataTable ToDataTable<T>(List<T> items)
    {
        DataTable dataTable = new DataTable(typeof(T).Name);
        PropertyInfo[] properties = typeof(T).GetProperties(BindingFlags.Instance | BindingFlags.Public);
        PropertyInfo[] array = properties;
        for (int i = 0; i < array.Length; i++)
        {
            PropertyInfo propertyInfo = array[i];
            dataTable.Columns.Add(propertyInfo.Name);
        }
        foreach (T current in items)
        {
            object[] array2 = new object[properties.Length];
            for (int j = 0; j < properties.Length; j++)
            {
                array2[j] = properties[j].GetValue(current, null);
            }
            dataTable.Rows.Add(array2);
        }
        return dataTable;
    }

    [WebMethod]
    public static string BindTransactionDetails(int skip,string FromDate,string Todate)
    {
        string list = string.Empty;
        int TotalPagecount = 5;
        ABCModel lobjModel = new ABCModel();
        ProgramDefinition lobjProgramDefinition = lobjModel.GetProgramMaster();
        string lstrCurrencyName = string.Empty;
        try
        {
            List<ProgramCurrencyDefinition> lobjlistProgramCurrencyDefinition = GetProgramCurrency();
            if (lobjlistProgramCurrencyDefinition != null)
            {
                if (lobjlistProgramCurrencyDefinition.Count > 0)
                {
                    lstrCurrencyName = lobjlistProgramCurrencyDefinition[0].Currency;
                }
                          
            }
            int TotalRowCount = 0;
            SearchTransactions objSearchTransactions = new SearchTransactions();
            objSearchTransactions.RelationType = Convert.ToInt32(RelationType.LBMS);
            objSearchTransactions.TransactionCurrency = lobjlistProgramCurrencyDefinition[0].Currency;
            objSearchTransactions.RelationReference = ((HttpContext.Current.Session["MemberDetails"] as MemberDetails).MemberRelationsList.Find(lobj => lobj.RelationType.Equals(RelationType.LBMS)).RelationReference);
            objSearchTransactions.ProgramId = lobjProgramDefinition.ProgramId;
            List<TransactionDetails> LobjTransactionDetails = new List<TransactionDetails>();
            if (FromDate == "Enter Date" || Todate == "Enter Date" || FromDate == "" || Todate == "")
            {
                objSearchTransactions.DateFrom = DateTime.Now.AddDays(-30);
                objSearchTransactions.DateTo = DateTime.Now;
            }
            else
            {

                objSearchTransactions.DateFrom = lobjModel.StringToDateTime(FromDate);
                objSearchTransactions.DateTo = lobjModel.StringToDateTime(Todate);
            }
            LoggingAdapter.WriteLog("Serch Transaction Summary Datefrom -:" + objSearchTransactions.DateFrom + "Datefrom -:" + objSearchTransactions.DateTo);

            TotalRowCount = lobjModel.GetTotalMemberTransactionByDate(objSearchTransactions);

            LoggingAdapter.WriteLog("GetTotalMemberTransactionByDate  TotalRowCount -:" + TotalRowCount);

            HttpContext.Current.Session["TotalPages"] = TotalRowCount;
            if (TotalRowCount == 0)
            {
              return  list = "No Transactions Found";
            }
            
            List<string> lobjstringList = new List<string>();
            lobjstringList = lobjModel.PageDataRangeList(TotalRowCount, TotalPagecount);
            string ddlvalue = string.Empty;
            List<string> LobjTotalpage = new List<string>();
            LobjTotalpage = lobjstringList;
            if (lobjstringList.Count > 0)
            {
                if(skip==0) 
                {
                    ddlvalue = lobjstringList[0];
                }
                else
                {
                    ddlvalue = lobjstringList[skip-1];
                }
               
            }
            objSearchTransactions = new SearchTransactions();
            objSearchTransactions.RelationReference = ((HttpContext.Current.Session["MemberDetails"] as MemberDetails).MemberRelationsList.Find(lobj => lobj.RelationType.Equals(RelationType.LBMS)).RelationReference);
            objSearchTransactions.ProgramId = lobjProgramDefinition.ProgramId;
            objSearchTransactions.RelationType = Convert.ToInt32(RelationType.LBMS);
            objSearchTransactions.TransactionCurrency = lstrCurrencyName;
            objSearchTransactions.MinimumRange = Convert.ToInt32(ddlvalue.ToString().Split('-')[0]);
            objSearchTransactions.MaximumRange = Convert.ToInt32(ddlvalue.ToString().Split('-')[1]);
            objSearchTransactions.TransactionCurrency = lstrCurrencyName;

            LobjTransactionDetails = new List<TransactionDetails>();
            if (FromDate == "Enter Date" || Todate == "Enter Date" || FromDate == "" || Todate == "")
            {
                objSearchTransactions.DateFrom = DateTime.Now.AddDays(-30);
                objSearchTransactions.DateTo = DateTime.Now;
                //LobjTransactionDetails = lobjModel.GetMemberTransactionSummary(objSearchTransactions);
            }
            else
            {
                objSearchTransactions.DateFrom = lobjModel.StringToDateTime(FromDate);
                objSearchTransactions.DateTo = lobjModel.StringToDateTime(Todate);
            }
            LobjTransactionDetails = lobjModel.GetMemberTransactionSummaryByDate(objSearchTransactions);
            if (LobjTransactionDetails != null && LobjTransactionDetails.Count > 0)
            {
                //RepSummaryInfo.DataSource = LobjTransactionDetails;
                HttpContext.Current.Session["TransactionDetails"] = LobjTransactionDetails;
                List<TransactionDetails> AdditionalInfo = new List<TransactionDetails>();
                AdditionalInfo = LobjTransactionDetails.FindAll(x => x.LoyaltyTxnType == LoyaltyTxnType.Spend).ToList();
                List<TransactionAdditionalInfo> LobjTransactionAdditionalInfo = new List<TransactionAdditionalInfo>();
                foreach (TransactionDetails addinfo in AdditionalInfo)
                {
                    LobjTransactionAdditionalInfo = lobjModel.GetTransactionAdditionalInfo(lobjProgramDefinition.ProgramId, addinfo.ExternalReference);
                    if (LobjTransactionAdditionalInfo != null && LobjTransactionAdditionalInfo.Count > 0)
                    {
                        TransactionDetails transactionDetails = LobjTransactionDetails.Find(x => x.ExternalReference == addinfo.ExternalReference);
                        transactionDetails.MerchantName = LobjTransactionAdditionalInfo[0].merchant_name;
                    }
                }
                StringBuilder sb=new StringBuilder();
                sb.Append("<div class='row'>");

                foreach (TransactionDetails transactionDetails in LobjTransactionDetails)
                {
                    sb.Append("<div class='col-12 mb-3'>");
                    sb.Append("<div class='bg-colour6 p-3'>");
                    sb.Append("<div class='row'>");

                    sb.Append("<div class='col-12 col-sm-6'>");
                    if(transactionDetails.LoyaltyTxnType.ToString() == "Bonus")
                    {
                        sb.Append("<h2 class='h6 heading-bold text-capitalize text-colour7'>" +  "Bonus" + "</h2>");
                    }
                    else
                    {
                        sb.Append("<h2 class='h6 heading-bold text-capitalize text-colour7'>" + transactionDetails.MerchantName + "</h2>");
                    }
                    if (transactionDetails.TransactionType.ToString() == "Debit")
                    {
                        sb.Append("<h2 class='h6 heading-regular text-capitalize'>" + "Redeemed Points "+ "<span class='h6 heading-bold text-colour7'>" + transactionDetails.Points + "</span></h2>");
                    }
                    else
                    {
                        sb.Append("<h2 class='h6 heading-regular text-capitalize'>" + "Earned Points " + "<span class='h6 heading-bold text-colour7'>" + transactionDetails.Points + "</span></h2>");
                    }
                    //sb.Append("<h2 class='h6 heading-regular text-capitalize'>Amount Paid<span class='h6 heading-bold text-colour1'> MUR</span><span class='h6 heading-bold text-colour7'> "+ transactionDetails.TransactionDetailBreakage.SourceAmount + "</span></h2>");
                    if (transactionDetails.TransactionType.ToString() == "Debit")
                    {
                        sb.Append("<h2 class='h6 heading-bold text-success py-2 py-lg-0'>" +  "Redeemed"+ "</h2>");
                    }
                    else
                    {
                        sb.Append("<h2 class='h6 heading-bold text-success py-2 py-lg-0'>" + "Spend" + "</h2>");
                    }
                        
                    sb.Append("</div>");

                    sb.Append("<div class='col-12 col-sm-6 text-sm-right d-sm-flex align-items-sm-end justify-content-sm-center flex-sm-column'>");
                    //sb.Append("<h2 class='h6 heading-bold d-flex align-items-center'><span class='heading-bold text-colour1'>MUR</span><span class='heading-bold text-colour7 pl-1'>"+ transactionDetails.Amounts + "</span></h2>");
                    sb.Append("<h2 class='h7 heading-regular text-capitalize'>"+ string.Format("{0:dd MMM yyyy}",transactionDetails.ProcessingDate)+"</h2>");
                    sb.Append("<h2 class='h7 heading-regular text-capitalize'>"+ string.Format("{0:dd MMM yyyy}",transactionDetails.TransactionDate)+ "</h2>");
                    sb.Append("</div>");

                    sb.Append("</div>");
                    sb.Append("</div>");
                    sb.Append("</div>");
                }
                sb.Append("</div>");
                list = sb.ToString();
            }
            
           // RepSummaryInfo.DataBind();
           TotalRowCount = Convert.ToInt32(HttpContext.Current.Session["TotalPages"]);
            if (LobjTransactionDetails != null && TotalRowCount > 0)
            {
                if (skip <= 0)
                {
                    skip = 0;
                }
                else
                {
                    skip--;
                }
                int PageNo = skip + 1;
                string lstrPaginationHtml = string.Empty;
                StringBuilder sbPage = new StringBuilder();
                if (TotalRowCount >= TotalPagecount)
                {
                    sbPage.Append("<div class='row mx-0 dvPagination scroll-hoz mb-3 justify-content-sm-center'> <nav><ul class='pagination justify-content-center'>");

                    if (PageNo == 1)
                    {
                        sbPage.Append("<li class='page-item disabled'> <span class='page-link'>Previous</span> </li>");
                    }
                    else
                    {
                        sbPage.Append(string.Format("<li class='page-item'> <span class='page-link' onclick='BindTransactionDetails({0},\"\\\"{1}\\\"\",\"\\\"{2}\\\"\");return false;'>Previous</span> </li>", (PageNo - 1), FromDate,Todate));
                    }
                    int totalPages = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(TotalRowCount) / TotalPagecount));
                    int PagingSize = 15;
                    int Pager = 1;
                    if (PageNo >= PagingSize)
                    {
                        if (totalPages >= PageNo + PagingSize - 2)
                        {
                            Pager = PageNo - 1;
                        }
                        else
                        {
                            Pager = totalPages - 14;
                        }
                    }
                    int j = 1;
                    for (int i = Pager; i <= totalPages && j <= PagingSize; i++, j++)
                    {
                        if (i == PageNo)
                        {
                            sbPage.Append(string.Format("<li class='page-item active'> <span class='page-link'> {0} <span class='sr-only'>(current)</span> </span></li>", i));
                        }
                        else
                        {
                            sbPage.Append(string.Format("<li class='page-item'> <span class='page-link' onclick='BindTransactionDetails({0},\"\\\"{1}\\\"\",\"\\\"{2}\\\"\");return false;'> {0} </span> </li>", i , FromDate, Todate));
                        }
                    }
                    if (PageNo < totalPages)
                    {
                        sbPage.Append(string.Format("<li class='page-item'> <span class='page-link' onclick='BindTransactionDetails({0},\"\\\"{1}\\\"\",\"\\\"{2}\\\"\");return false;'>Next</span> </li> </ul> </nav></div>", (PageNo + 1), FromDate, Todate ));
                    }
                    else
                    {
                        sbPage.Append("<li class='page-item disabled'> <span class='page-link'>Next</span> </li> </ul> </nav>");
                    }
                    sbPage.ToString();
                }

                lstrPaginationHtml = sbPage.ToString();
                list += lstrPaginationHtml;
            }
            else
            {
                list += "<span>No Records Found.</span>";
            }
        }
        catch(Exception ex)
        {
            LoggingAdapter.WriteLog(ex.Message);
        }
        return list;
    }

}