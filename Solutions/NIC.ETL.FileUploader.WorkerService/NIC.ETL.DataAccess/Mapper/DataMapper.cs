using SBL.ETL.DataAccess.Constants;
using SBL.ETL.Entities;
using Core.Platform.ProgramMaster.Entities;
using Framework.EnterpriseLibrary.Adapters;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace SBL.ETL.DataAccess.Mapper
{
    public class DataMapper
    {
        public static List<ETLDetails> MapETLData(DataSet pobjDataset)
        {
            List<ETLDetails> lobjStrDataLst = new List<ETLDetails>();
            try
            {
                if (pobjDataset != null && pobjDataset.Tables.Count > 0)
                {
                    for (int i = 0; i < pobjDataset.Tables[0].Rows.Count; i++)
                    {
                        ETLDetails lobjETLDetails = new ETLDetails
                        {
                            StrData = Convert.ToString(pobjDataset.Tables[0].Rows[i][DBFields.StrData])
                        };
                        lobjStrDataLst.Add(lobjETLDetails);
                    }
                }
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("DataMapper MapETLData Exception: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
            }
            return lobjStrDataLst;
        }
    }
}
