<%@ Application Language="C#" %>
<script RunAt="server">
    void Application_Start(object sender, EventArgs e)
    {
        System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls
            | System.Net.SecurityProtocolType.Ssl3
            | System.Net.SecurityProtocolType.Tls12
            | System.Net.SecurityProtocolType.Tls11;
        System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
        System.Threading.Tasks.Task.Factory.StartNew(() =>
        {
            // Code that runs on application startup
            getProgramDetails();
            getAllHotelCities();
            getRefererData();
            getSystemConfigurations();
            GetAllAirfields();
            GetAllAirFieldsForDomestic();
            getAllCarriers();
            getAllAirCraftDetails();
            getRefererDetailsWithSupplier();
            GetSearchCategories();
            GetInsuranceServiceList();
            GetISPList();
        }).ConfigureAwait(false);
    }
    private void getAllAirCraftDetails()
    {
        try
        {
            if (Application["AllAirCraftsDetails"] == null)
            {
                System.Collections.Generic.List<CB.IBE.Platform.Entities.AirCraftDetails>
                AirCraftList = new System.Collections.Generic.List<CB.IBE.Platform.Entities.AirCraftDetails>();
                ABC.Model.ABCModel model = new ABC.Model.ABCModel();
                AirCraftList = model.GetAllAirCraftDetails();
                Application["AllAirCraftsDetails"] = AirCraftList;
            }
        }
        catch (Exception ex)
        {
            Framework.EnterpriseLibrary.Adapters.LoggingAdapter.WriteLog("getAllAirCraftDetails : " + ex.Message + Environment.NewLine + "Stack Trace-" + ex.StackTrace);
        }
    }
    private void getRefererDetailsWithSupplier()
    {
        try
        {
            if (Application["RefererSupplierDetails"] == null)
            {
                ABC.Model.ABCModel model = new ABC.Model.ABCModel();
                CB.IBE.Platform.Masters.Entities.RefererDetails lobjRefererDetails = new CB.IBE.Platform.Masters.Entities.RefererDetails();
                lobjRefererDetails = Application["RefererData"] as CB.IBE.Platform.Masters.Entities.RefererDetails;
                lobjRefererDetails = model.GetRefererDetailsWithSupplier(lobjRefererDetails);
                Application["RefererSupplierDetails"] = lobjRefererDetails;
            }
        }
        catch (Exception ex)
        {
            Framework.EnterpriseLibrary.Adapters.LoggingAdapter.WriteLog("getRefererDetailsWithSupplier : " + ex.Message + Environment.NewLine + "Stack Trace-" + ex.StackTrace);
        }
    }
    private void GetAllAirfields()
    {
        try
        {
            if (Application["AllAirfields"] == null || Application["Airfields"] == null)
            {
                System.Collections.Generic.Dictionary<string, CB.IBE.Platform.Entities.AirField> AirfieldsDictionary = new System.Collections.Generic.Dictionary<string, CB.IBE.Platform.Entities.AirField>();
                System.Collections.Generic.List<CB.IBE.Platform.Entities.AirField>
                AirfieldList = new System.Collections.Generic.List<CB.IBE.Platform.Entities.AirField>();
                ABC.Model.ABCModel model = new ABC.Model.ABCModel();
                AirfieldList = model.GetAllAirfields();
                Application["AllAirfields"] = AirfieldList;
                if (AirfieldList != null && AirfieldList.Count > 0)
                {
                    for (int carrierCount = 0; carrierCount < AirfieldList.Count; carrierCount++)
                    {
                        AirfieldsDictionary.Add(AirfieldList[carrierCount].IATACode, AirfieldList[carrierCount]);
                    }
                    Application["Airfields"] = AirfieldsDictionary;
                }
            }
        }
        catch (Exception ex)
        {
            Framework.EnterpriseLibrary.Adapters.LoggingAdapter.WriteLog("GetAllAirfields : " + ex.Message + Environment.NewLine + "Stack Trace-" + ex.StackTrace);
        }
    }
    private void getAllCarriers()
    {
        try
        {
            if (Application["CarrierList"] == null || Application["Carriers"] == null)
            {
                System.Collections.Generic.Dictionary<string, CB.IBE.Platform.Entities.Carrier> CarrierDictionary = new System.Collections.Generic.Dictionary<string, CB.IBE.Platform.Entities.Carrier>();
                System.Collections.Generic.List<CB.IBE.Platform.Entities.Carrier> carrierList = new System.Collections.Generic.List<CB.IBE.Platform.Entities.Carrier>();
                ABC.Model.ABCModel model = new ABC.Model.ABCModel();
                carrierList = model.GetAllCarriers();
                for (int carrierCount = 0; carrierCount < carrierList.Count; carrierCount++)
                {
                    if (!CarrierDictionary.ContainsKey(carrierList[carrierCount].CarrierCode))
                    {
                        CarrierDictionary.Add(carrierList[carrierCount].CarrierCode, carrierList[carrierCount]);
                    }
                }
                Application["CarrierList"] = carrierList;
                Application["Carriers"] = CarrierDictionary;
            }
        }
        catch (Exception ex)
        {
            Framework.EnterpriseLibrary.Adapters.LoggingAdapter.WriteLog("getAllCarriers Exception : " + ex.StackTrace + Environment.NewLine + "Stack Trace-" + ex.StackTrace);
        }
    }
    private void getSystemConfigurations()
    {
        try
        {
            if (Application["SystemParameters"] == null)
            {
                Core.Platform.ProgramMaster.Entities.ProgramDefinition lobjProgramDefinition = Application["ProgramMaster"] as Core.Platform.ProgramMaster.Entities.ProgramDefinition;
                ABC.Model.ABCModel model = new ABC.Model.ABCModel();
                Core.Platform.ProgramMaster.Entities.SystemParameter lobjSystemParameter = model.GetSystemParametres(lobjProgramDefinition.ProgramId);
                Application["SystemParameters"] = lobjSystemParameter;
            }
        }
        catch (Exception ex)
        {
            Framework.EnterpriseLibrary.Adapters.LoggingAdapter.WriteLog("getSystemConfigurations Exception : " + ex.StackTrace + Environment.NewLine + "Stack Trace-" + ex.StackTrace);
        }
    }
    private void getRefererData()
    {
        try
        {
            if (Application["RefererData"] == null)
            {
                ABC.Model.ABCModel model = new ABC.Model.ABCModel();
                CB.IBE.Platform.Masters.Entities.RefererDetails lobjRefererDetails = new CB.IBE.Platform.Masters.Entities.RefererDetails();
                lobjRefererDetails = model.GetRefererData();
                Application["RefererData"] = lobjRefererDetails;
            }
        }
        catch (Exception ex)
        {
            Framework.EnterpriseLibrary.Adapters.LoggingAdapter.WriteLog("getRefererData Exception : " + ex.StackTrace + Environment.NewLine + "Stack Trace-" + ex.StackTrace);
        }
    }
    private void getAllHotelCities()
    {
        try
        {
            if (Application["HotelCities"] == null)
            {
                System.Collections.Generic.List<string> lobjListOfCity = new System.Collections.Generic.List<string>();
                ABC.Model.ABCModel model = new ABC.Model.ABCModel();
                lobjListOfCity = model.GetAllHotelCities();
                Application["HotelCities"] = lobjListOfCity;
            }
        }
        catch (Exception ex)
        {
            Framework.EnterpriseLibrary.Adapters.LoggingAdapter.WriteLog("getAllHotelCities Exception : " + ex.StackTrace + Environment.NewLine + "Stack Trace-" + ex.StackTrace);
        }
    }
    private void getProgramDetails()
    {
        try
        {
            if (Application["ProgramMaster"] == null)
            {
                ABC.Model.ABCModel model = new ABC.Model.ABCModel();
                Core.Platform.ProgramMaster.Entities.ProgramDefinition lobjProgramDefinition = model.GetProgramDetails(ConfigurationManager.AppSettings["ProgramName"].ToString());
                Application["ProgramMaster"] = lobjProgramDefinition;
            }
        }
        catch (Exception ex)
        {
            Framework.EnterpriseLibrary.Adapters.LoggingAdapter.WriteLog("getProgramDetails Exception : " + ex.StackTrace + Environment.NewLine + "Stack Trace-" + ex.StackTrace);
        }
    }
    private void GetSearchCategories()
    {
        try
        {
            GiiftShopGateway.Model.ShopModel model = new GiiftShopGateway.Model.ShopModel();
            model.SearchCategories();
        }
        catch (Exception ex)
        {
            Framework.EnterpriseLibrary.Adapters.LoggingAdapter.WriteLog("GetSearchCategories : " + ex.Message + Environment.NewLine + "Stack Trace-" + ex.StackTrace);
        }
    }
    private void GetAllAirFieldsForDomestic()
    {
        try
        {
            if (Application["AllAirfieldsforDomestic"] == null || Application["AirfieldsforDomestic"] == null)
            {
                // System.Collections.Generic.Dictionary<string, CB.IBE.DomesticFlight.Entities.AirFieldsForDomestic> AirfieldsDictionaryForDomestic = new System.Collections.Generic.Dictionary<string, CB.IBE.DomesticFlight.Entities.AirFieldsForDomestic>();
                CB.IBE.DomesticFlight.Entities.AirFieldsForDomestic AirfieldList = new CB.IBE.DomesticFlight.Entities.AirFieldsForDomestic();
                ABC.Model.ABCModel model = new ABC.Model.ABCModel();
                AirfieldList = model.GetAllAirFieldsForDomestic();
                if (AirfieldList != null)
                {
                    Application["AllAirfieldsforDomestic"] = AirfieldList;
                }
            }
        }
        catch (Exception ex)
        {
            Framework.EnterpriseLibrary.Adapters.LoggingAdapter.WriteLog("GetAllAirFieldsForDomestic : " + ex.Message + Environment.NewLine + "Stack Trace-" + ex.StackTrace);
        }
    }
    private void GetInsuranceServiceList()
    {
        try
        {
            ABC.Model.ABCModel lobjModel = new ABC.Model.ABCModel();
            string UserName = string.Empty;
            string PageName = string.Empty;
            UserName = Convert.ToString(ConfigurationManager.AppSettings["KhaltiInsuranceUserName"]);
            PageName = "Insurance List";
            lobjModel.SearchInsuranceProducts(UserName, PageName);
        }
        catch (Exception ex)
        {
            Framework.EnterpriseLibrary.Adapters.LoggingAdapter.WriteLog("GetInsuranceServiceList Exception: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
        }
    }

    private void GetISPList()
    {
        try
        {
            ABC.Model.ABCModel lobjModel = new ABC.Model.ABCModel();
            string UserName = string.Empty;
            string PageName = string.Empty;
            UserName = Convert.ToString(ConfigurationManager.AppSettings["KhaltiISPUserName"]);
            PageName = "Internet Service Provider";
            lobjModel.SearchInsuranceProducts(UserName, PageName);
        }
        catch (Exception ex)
        {
            Framework.EnterpriseLibrary.Adapters.LoggingAdapter.WriteLog("GetStoreDetails Exception: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
        }
    }
    void Application_End(object sender, EventArgs e)
    {
        //  Code that runs on application shutdown

    }
    void Application_Error(object sender, EventArgs e)
    {
        // Code that runs when an unhandled error occurs
    }
    void Session_Start(object sender, EventArgs e)
    {
        try
        {
            // Code that runs when a new session is started
            HttpBrowserCapabilities lobjBrowser = HttpContext.Current.Request.Browser;
            string lstrScreenResolution = lobjBrowser.ScreenPixelsWidth.ToString() + "X" + lobjBrowser.ScreenPixelsHeight.ToString();
            // Code that runs when a new session is started
            Core.Platform.MemberActivity.Entities.MemberActivitySession lobjMemberActivitySession = new Core.Platform.MemberActivity.Entities.MemberActivitySession();
            lobjMemberActivitySession.Activity = "Session Start";
            lobjMemberActivitySession.Browser = lobjBrowser.Type;
            lobjMemberActivitySession.BrowserVersion = lobjBrowser.Version;
            lobjMemberActivitySession.DateTimeStamp = DateTime.Now.ToUniversalTime();
            lobjMemberActivitySession.IpAddress = HttpContext.Current.Request.UserHostAddress;
            lobjMemberActivitySession.PageURL = "Global.asax";
            lobjMemberActivitySession.Resolution = lstrScreenResolution;
            lobjMemberActivitySession.ReferenceNumber = "Guest";
            lobjMemberActivitySession.SessionID = Session.SessionID;
            ABC.Model.ABCModel lobjModel = new ABC.Model.ABCModel();
            Core.Platform.ProgramMaster.Entities.ProgramDefinition lobjProgramDefinition = lobjModel.GetProgramMaster();
            lobjMemberActivitySession.ProgramId = lobjProgramDefinition.ProgramId;
            int lintID = lobjModel.InsertMemberActivityWithSessionID(lobjMemberActivitySession);
            lobjMemberActivitySession.Id = lintID;
            Session["MemberActivitySession"] = lobjMemberActivitySession;
        }
        catch (Exception ex)
        {
            Framework.EnterpriseLibrary.Adapters.LoggingAdapter.WriteLog("Session_Start Exception : " + ex.StackTrace + Environment.NewLine + "Stack Trace-" + ex.StackTrace);
        }
    }
    void Session_End(object sender, EventArgs e)
    {
        // Code that runs when a session ends. 
        // Note: The Session_End event is raised only when the sessionstate mode
        // is set to InProc in the Web.config file. If session mode is set to StateServer 
        // or SQLServer, the event is not raised.
    }
</script>
