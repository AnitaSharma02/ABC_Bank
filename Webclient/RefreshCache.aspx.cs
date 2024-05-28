using System.Web.UI;
using ABC.Model;
using Framework.EnterpriseLibrary.Adapters;
using CB.IBE.Platform.Masters.Entities;
using System.Collections.Generic;
using Core.Platform.ProgramMaster.Entities;
using System.Configuration;
using System;
using GiiftShopGateway.Model;
using KhaltiInsurance.Entities;
using System.Web;
using CB.IBE.Platform.AirClientModel;

public partial class RefreshCache : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            string lstrUN = Convert.ToString(ConfigurationManager.AppSettings["RefreshCacheUN"]);
            string lstrPWD = Convert.ToString(ConfigurationManager.AppSettings["RefreshCachePWD"]);
            if (lstrUN.Equals(txtUN.Text.Trim()) && lstrPWD.Equals(txtPwd.Text.Trim()))
            {
                ABCModel model = new ABCModel();
                if (ddlCache.SelectedItem.Value.Equals("RefererSupplier"))
                {
                    GetRefererDetailsWithSupplier();
                }
                else if (ddlCache.SelectedItem.Value.Equals("ProgramMaster"))
                {
                    GetProgramDetails();
                }
                else if (ddlCache.SelectedItem.Value.Equals("HotelCities"))
                {
                    GetAllHotelCities();
                }
                else if (ddlCache.SelectedItem.Value.Equals("Store"))
                {
                    Application["HomeRedemptionOptions"] = null;
                    Application["GetTravelOptions"] = null;
                    Application["SearchCategories"] = null;
                    GetStoreDetails();
                    GetSearchCategories();
                }
                else if (ddlCache.SelectedItem.Value.Equals("Insurance"))
                {
                    GetInsuranceServiceList();
                }
                else if (ddlCache.SelectedItem.Value.Equals("ISP"))
                {
                    GetISPList();
                }
                else if (ddlCache.SelectedItem.Value.Equals("All"))
                {
                    Application["HomeRedemptionOptions"] = null;
                    Application["GetTravelOptions"] = null;
                    Application["SearchCategories"] = null;
                    GetProgramDetails();
                    GetAllHotelCities();
                    GetRefererData();
                    GetSystemConfigurations();
                    GetAllAirfields();
                    GetAllCarriers();
                    GetAllAirCraftDetails();
                    GetRefererDetailsWithSupplier();
                    GetStoreDetails();
                    GetSearchCategories();
                    GetInsuranceServiceList();
                    GetISPList();
                }
            }
            else
            {
                lblLoginError.Text = "Please enter valid username & password.";
            }
        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("RefreshCache.aspx - btnSubmit_Click Exception: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
        }
    }
    private void GetStoreDetails()
    {
        try
        {
            ShopModel model = new ShopModel();
            Application["Store"] = null;
            Application["Store"] = model.GetStoreDetails();
            lblLoginError.Text += "Store Details Cache refreshed successfully<br/>";
        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("GetStoreDetails Exception: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
            lblLoginError.Text += "Store Details Cache refreshed failed<br/>";
        }
    }
    private void GetAllAirCraftDetails()
    {
        try
        {
            List<CB.IBE.Platform.Entities.AirCraftDetails>
            AirCraftList = new List<CB.IBE.Platform.Entities.AirCraftDetails>();
            ABCModel model = new ABCModel();
            AirCraftList = model.GetAllAirCraftDetails();
            Application["AllAirCraftsDetails"] = null;
            Application["AllAirCraftsDetails"] = AirCraftList;
            lblLoginError.Text += "Air Craft Cache refreshed successfully<br/>";
        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("getAllAirCraftDetails : " + ex.Message + Environment.NewLine + "Stack Trace-" + ex.StackTrace);
            lblLoginError.Text += "Air Craft Cache refreshed failed<br/>";
        }
    }
    private void GetRefererDetailsWithSupplier()
    {
        try
        {
            ABCModel model = new ABCModel();
            RefererDetails lobjRefererDetails = new RefererDetails();
            lobjRefererDetails = Application["RefererData"] as RefererDetails;
            lobjRefererDetails = model.GetRefererDetailsWithSupplier(lobjRefererDetails);
            Application["RefererSupplierDetails"] = null;
            Application["RefererSupplierDetails"] = lobjRefererDetails;
            lblLoginError.Text += "Referer Details With Supplier Cache refreshed successfully<br/>";
        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("getRefererDetailsWithSupplier : " + ex.Message + Environment.NewLine + "Stack Trace-" + ex.StackTrace);
            lblLoginError.Text += "Referer Details With Supplier Cache refreshed failed<br/>";
        }
    }
    private void GetAllAirfields()
    {
        try
        {
            Dictionary<string, CB.IBE.Platform.Entities.AirField> AirfieldsDictionary = new Dictionary<string, CB.IBE.Platform.Entities.AirField>();
            List<CB.IBE.Platform.Entities.AirField>
            AirfieldList = new List<CB.IBE.Platform.Entities.AirField>();
            ABCModel model = new ABCModel();
            AirfieldList = model.GetAllAirfields();
            Application["AllAirfields"] = null;
            Application["AllAirfields"] = AirfieldList;
            if (AirfieldList != null && AirfieldList.Count > 0)
            {
                for (int carrierCount = 0; carrierCount < AirfieldList.Count; carrierCount++)
                {
                    AirfieldsDictionary.Add(AirfieldList[carrierCount].IATACode, AirfieldList[carrierCount]);
                }
                Application["Airfields"] = null;
                Application["Airfields"] = AirfieldsDictionary;
            }
            lblLoginError.Text += "All Air fields Cache refreshed successfully<br/>";
        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("GetAllAirfields Exception: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
            lblLoginError.Text += "All Air fields Cache refreshed failed<br/>";
        }
    }
    private void GetAllCarriers()
    {
        try
        {
            Dictionary<string, CB.IBE.Platform.Entities.Carrier> CarrierDictionary = new Dictionary<string, CB.IBE.Platform.Entities.Carrier>();
            List<CB.IBE.Platform.Entities.Carrier> carrierList = new List<CB.IBE.Platform.Entities.Carrier>();
            ABCModel model = new ABCModel();
            carrierList = model.GetAllCarriers();

            for (int carrierCount = 0; carrierCount < carrierList.Count; carrierCount++)
            {
                if (!CarrierDictionary.ContainsKey(carrierList[carrierCount].CarrierCode))
                {
                    CarrierDictionary.Add(carrierList[carrierCount].CarrierCode, carrierList[carrierCount]);
                }
            }
            Application["CarrierList"] = null;
            Application["Carriers"] = null;
            Application["CarrierList"] = carrierList;
            Application["Carriers"] = CarrierDictionary;
            lblLoginError.Text += "All Carriers Cache refreshed successfully<br/>";
        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("GetAllCarriers Exception: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
            lblLoginError.Text += "All Carriers Cache refreshed failed<br/>";
        }
    }
    private void GetSystemConfigurations()
    {
        try
        {
            ProgramDefinition lobjProgramDefinition = Application["ProgramMaster"] as ProgramDefinition;
            ABCModel model = new ABCModel();
            SystemParameter lobjSystemParameter = model.GetSystemParametres(lobjProgramDefinition.ProgramId);
            Application["SystemParameters"] = null;
            Application["SystemParameters"] = lobjSystemParameter;
            lblLoginError.Text += "System Configurations Cache refreshed successfully<br/>";
        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("GetSystemConfigurations Exception: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
            lblLoginError.Text += "System Configurations Cache refreshed failed<br/>";
        }
    }
    private void GetRefererData()
    {
        try
        {
            ABCModel model = new ABCModel();
            RefererDetails lobjRefererDetails = new RefererDetails();
            lobjRefererDetails = model.GetRefererData();
            Application["RefererData"] = null;
            Application["RefererData"] = lobjRefererDetails;
            lblLoginError.Text += "Referer Cache refreshed successfully<br/>";
        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("GetRefererData Exception: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
            lblLoginError.Text += "Referer Cache refreshed failed<br/>";
        }
    }
    private void GetAllHotelCities()
    {
        try
        {
            List<string> lobjListOfCity = new List<string>();
            ABCModel model = new ABCModel();
            lobjListOfCity = model.GetAllHotelCities();
            Application["HotelCities"] = null;
            Application["HotelCities"] = lobjListOfCity;
            lblLoginError.Text += "Hotel Cities Cache refreshed successfully<br/>";
        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("GetAllHotelCities Exception: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
            lblLoginError.Text += "Hotel Cities Cache refreshed failed<br/>";
        }
    }
    private void GetProgramDetails()
    {
        try
        {
            ABCModel model = new ABCModel();
            ProgramDefinition lobjProgramDefinition = model.GetProgramDetails(ConfigurationManager.AppSettings["ProgramName"].ToString());
            Application["ProgramMaster"] = null;
            Application["ProgramMaster"] = lobjProgramDefinition;
            lblLoginError.Text += "Program Cache refreshed successfully<br/>";
        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("GetProgramDetails Exception: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
            lblLoginError.Text += "Program Cache refreshed failed<br/>";
        }
    }
    private void GetSearchCategories()
    {
        try
        {
            ShopModel lobjmodel = new ShopModel();
            lobjmodel.SearchCategories();
        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("GetSearchCategories : " + ex.Message + Environment.NewLine + "Stack Trace-" + ex.StackTrace);
        }
    }
    private void GetInsuranceServiceList()
    {
        try
        {
            ABCModel lobjModel = new ABCModel();
            string UserName = string.Empty;
            string PageName = string.Empty;
            UserName = Convert.ToString(ConfigurationManager.AppSettings["KhaltiInsuranceUserName"]);
            PageName = "Insurance List";
            Application["SearchInsuranceProducts"] = null;
            Application["SearchInsuranceProducts"] = lobjModel.SearchInsuranceProducts(UserName, PageName);
            lblLoginError.Text += "Insurance Details Cache refreshed successfully<br/>";
        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("GetInsuranceServiceList Exception: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
            lblLoginError.Text += "Insurance Details Cache refreshed failed<br/>";
        }
    }

    private void GetISPList()
    {
        try
        {
            ABCModel lobjModel = new ABCModel();
            string UserName = string.Empty;
            string PageName = string.Empty;
            UserName = Convert.ToString(ConfigurationManager.AppSettings["KhaltiISPUserName"]);
            PageName = "Internet Service Provider";
            Application["SearchISPProducts"] = null;
            Application["SearchISPProducts"] = lobjModel.SearchInsuranceProducts(UserName, PageName);
            lblLoginError.Text += "ISP Details Cache refreshed successfully<br/>";
        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("GetStoreDetails Exception: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
            lblLoginError.Text += "ISP Details Cache refreshed failed<br/>";
        }
    }
}