using System;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using CB.IBE.Platform.Entities;
using System.Text.RegularExpressions;
using CB.IBE.Platform.Masters.Entities;
using ABC.Model;
using CB.IBE.Platform.ClientEntities;

public partial class SourceControl_ChildPassangerDetails : System.Web.UI.UserControl
{
    ABCModel lobjModel = new ABCModel();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            RefererDetails lobjRefererDetails = HttpContext.Current.Application["RefererSupplierDetails"] as RefererDetails;
            int supplierId = lobjRefererDetails.RefererSupplierProperties.SupplierId;
            if (supplierId.Equals(6)) // provisio
            {
                AdditionalInfo.Style.Add("display", "block");
                rfvAdultTelephone.Enabled = true;
                rfvAdultLocation.Enabled = true;
                rfvAdultEffectiveDate.Enabled = true;
                rfvAdultExpiryDate.Enabled = true;
                revAdulttelephone.Enabled = true;
                customIssueDateValidator.Enabled = true;
                CustomExpiryDate.Enabled = true;
            }
            else
            {
                AdditionalInfo.Style.Add("display", "none");
                divNationality.Style.Add("display", "none");
                divTelephone.Style.Add("display", "none");
                divPassportPlace.Style.Add("display", "none");
                divPassportissue.Style.Add("display", "none");
                divPassportexpiry.Style.Add("display", "none");

                string[] strPassportIssueDateAirlineCode = System.Configuration.ConfigurationManager.AppSettings["PassportIssueDateAirlineCode"].Split(',').Select(s => s.Trim()).ToArray();
                string[] strPassportExpiryDateAirlineCode = System.Configuration.ConfigurationManager.AppSettings["PassportExpiryDateAirlineCode"].Split(',').Select(s => s.Trim()).ToArray();
                string[] strPassportIssuingCountryAirlineCode = System.Configuration.ConfigurationManager.AppSettings["PassportIssuingCountryAirlineCode"].Split(',').Select(s => s.Trim()).ToArray();
                string[] strPassportNoAirlineCode = System.Configuration.ConfigurationManager.AppSettings["PassportNoAirlineCode"].Split(',').Select(s => s.Trim()).ToArray();
                string[] strDOBAirlineCode = System.Configuration.ConfigurationManager.AppSettings["DOBAirlineCode"].Split(',').Select(s => s.Trim()).ToArray();

                ItineraryDetails lobjItineraryDetails = (ItineraryDetails)Session["SelectedItinerary"];
                var FlightDetailsIataCode = lobjItineraryDetails.ListOfFlightDetails[0].ListOfFlightSegments[0].AirlinelIATACode;
                var FlightDetailsIataCode1 = string.Empty;
                if (lobjItineraryDetails.ListOfFlightDetails.Count > 1)
                {
                    FlightDetailsIataCode1 = lobjItineraryDetails.ListOfFlightDetails[1].ListOfFlightSegments[0].AirlinelIATACode;
                }


                if (strPassportIssueDateAirlineCode.Contains(FlightDetailsIataCode) || strPassportIssueDateAirlineCode.Contains(FlightDetailsIataCode1))
                {
                    AdditionalInfo.Style.Add("display", "block");
                    divPassportissue.Style.Add("display", "block");
                    rfvAdultEffectiveDate.Enabled = true;
                    customIssueDateValidator.Enabled = true;
                }
                if (strPassportExpiryDateAirlineCode.Contains(FlightDetailsIataCode) || strPassportExpiryDateAirlineCode.Contains(FlightDetailsIataCode1))
                {
                    AdditionalInfo.Style.Add("display", "block");
                    divPassportexpiry.Style.Add("display", "block");
                    rfvAdultExpiryDate.Enabled = true;
                    CustomExpiryDate.Enabled = true;
                }
                if (strPassportIssuingCountryAirlineCode.Contains(FlightDetailsIataCode) || strPassportIssuingCountryAirlineCode.Contains(FlightDetailsIataCode1))
                {
                    AdditionalInfo.Style.Add("display", "block");
                    divPassportPlace.Style.Add("display", "block");
                    divNationality.Style.Add("display", "block");
                    rfvAdultLocation.Enabled = true;
                }

            }
        }
    }
    protected void IssueDateValidator(object source, ServerValidateEventArgs args)
    {
        bool IsValid = false;
        string lstrMsg = string.Empty;
        string Exp = @"^(?:(?:(?:0?[1-9]|1\d|2[0-8])\/(?:0?[1-9]|1[0-2]))\/(?:(?:1[6-9]|[2-9]\d)\d{2}))$|^(?:(?:(?:31\/0?[13578]|1[02])|(?:(?:29|30)\/(?:0?[1,3-9]|1[0-2])))\/(?:(?:1[6-9]|[2-9]\d)\d{2}))$|^(?:29\/0?2\/(?:(?:(?:1[6-9]|[2-9]\d)(?:0[48]|[2468][048]|[13579][26]))))$";
        Regex regex = new Regex(Exp);
        if (regex.IsMatch(args.Value))
        {
            IsValid = true;
        }
        else
        {
            IsValid = false;
            customIssueDateValidator.ErrorMessage = "Please insert date in correct format";
        }
        if (IsValid)
        {
            IsValid = (Convert.ToDateTime(lobjModel.StringToDateTime(args.Value)) > Convert.ToDateTime(lobjModel.StringToDateTime(txtDOB.Text)));
            if (!IsValid)
            {
                customIssueDateValidator.ErrorMessage = "Issue date must be greater than DOB";
            }
        }
        args.IsValid = IsValid;

    }
    protected void IssueExpiryValidator(object source, ServerValidateEventArgs args)
    {
        bool IsValid = false;
        string Exp = @"^(?:(?:(?:0?[1-9]|1\d|2[0-8])\/(?:0?[1-9]|1[0-2]))\/(?:(?:1[6-9]|[2-9]\d)\d{2}))$|^(?:(?:(?:31\/0?[13578]|1[02])|(?:(?:29|30)\/(?:0?[1,3-9]|1[0-2])))\/(?:(?:1[6-9]|[2-9]\d)\d{2}))$|^(?:29\/0?2\/(?:(?:(?:1[6-9]|[2-9]\d)(?:0[48]|[2468][048]|[13579][26]))))$";
        Regex regex = new Regex(Exp);

        if (regex.IsMatch(args.Value))
        {
            IsValid = true;
        }
        else
        {
            IsValid = false;
            CustomExpiryDate.ErrorMessage = "Please insert date in correct format";
        }

        if (IsValid)
        {
            SearchRequest lobjSearchRequest = Session["SearchFlight"] as SearchRequest;
            IsValid = (Convert.ToDateTime(lobjModel.StringToDateTime(args.Value)) > Convert.ToDateTime(lobjModel.StringToDateTime(txtEffectiveDate.Text)));
            if (!IsValid)
            {
                CustomExpiryDate.ErrorMessage = "Expiry date must be greater than issue date";
                IsValid = false;
            }
            else if ((Convert.ToDateTime(lobjModel.StringToDateTime(args.Value)) <= lobjSearchRequest.SearchDetails.DepartureDate) && ((!lobjSearchRequest.SearchDetails.IsReturn) || (Convert.ToDateTime(lobjModel.StringToDateTime(args.Value)) < lobjSearchRequest.SearchDetails.ArrivalDate)))
            {
                CustomExpiryDate.ErrorMessage = "Expiry date must be greater than travel date";
                IsValid = false;
            }
        }

        args.IsValid = IsValid;

    }

    protected void IssueChildDOBValidator(object source, ServerValidateEventArgs args)
    {
        if (!string.IsNullOrEmpty(args.Value))
        {
            args.IsValid = validateChildAge(args);
        }
    }

    private bool validateChildAge(ServerValidateEventArgs args)
    {
        DateTime dtEnterDate = Convert.ToDateTime(lobjModel.StringToDateTime(args.Value));
        DateTime dtTodayDate = Convert.ToDateTime(lobjModel.StringToDateTime(DateTime.Now.ToString("dd/MM/yyyy")));
        TimeSpan ts = dtTodayDate.Subtract(dtEnterDate);
        int NoOfDays = ts.Days;
        double yr = NoOfDays / 365.25;
        double day = NoOfDays % 365.25;

        if ((yr >= 2) && (yr < 12))
        {

            return true;
        }
        else if (Math.Round(yr) == 12)
        {
            if (day > 0.5)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        else
        {
            return false;
        }
    }
}