using SBL.ETL.Entities;
using SBL.ETL.DataAccess.Mapper;
using SBL.ETL.DataAccess.Wrapper;
using Core.Platform.ProgramMaster.Entities;
using Framework.EnterpriseLibrary.Adapters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace SBL.ETL.BusinessFacade
{
    public class Facade
    {
        public List<ETLDetails> GetETLCPD(string pstrProcessDate)
        {
            List<ETLDetails> lobjListOfETLDetails = new List<ETLDetails>();
            try
            {
                lobjListOfETLDetails = DataMapper.MapETLData(SPWrapper.GetETLCPD(pstrProcessDate));
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("Facade GetETLCPD Exception: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
            }
            return lobjListOfETLDetails;
        }
        public List<ETLDetails> GetETLBNS(string pstrProcessDate)
        {
            List<ETLDetails> lobjListOfETLDetails = new List<ETLDetails>();
            try
            {
                lobjListOfETLDetails = DataMapper.MapETLData(SPWrapper.GetETLBNS(pstrProcessDate));
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("Facade GetETLBNS Exception: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
            }
            return lobjListOfETLDetails;
        }
        public List<ETLDetails> GetETLCRD(string pstrProcessDate)
        {
            List<ETLDetails> lobjListOfETLDetails = new List<ETLDetails>();
            try
            {
                lobjListOfETLDetails = DataMapper.MapETLData(SPWrapper.GetETLCRD(pstrProcessDate));
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("Facade GetETLCRD Exception: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
            }
            return lobjListOfETLDetails;
        }
        public List<ETLDetails> GetETLTXN(string pstrProcessDate)
        {
            List<ETLDetails> lobjListOfETLDetails = new List<ETLDetails>();
            try
            {
                lobjListOfETLDetails = DataMapper.MapETLData(SPWrapper.GetETLTXN(pstrProcessDate));
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("Facade GetETLTXN Exception: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
            }
            return lobjListOfETLDetails;
        }
    }
}
