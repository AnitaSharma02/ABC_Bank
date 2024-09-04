using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CB.IBE.Platform.Entities;
using System.Text.RegularExpressions;
using CB.IBE.Platform.Masters.Entities;
using ABC.Model;
using CB.IBE.Platform.ClientEntities;
using Core.Platform.MemberActivity.Entities;
using Framework.EnterpriseLibrary.Adapters;

public partial class SourceControl_AdultPassangerDetails : System.Web.UI.UserControl
{
    ABCModel lobjModel = new ABCModel();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //RefererDetails lobjRefererDetails = HttpContext.Current.Application["RefererSupplierDetails"] as RefererDetails;
            int supplierId = 0;//lobjRefererDetails.RefererSupplierProperties.SupplierId;
            LoggingAdapter.WriteLog(string.Format("Flight Adult Details"+ supplierId));
            try
            {
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
                    //divTelephone.Style.Add("display", "none");
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
                BindCountryList();
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog(string.Format("Flight Adult Details" + ex.InnerException));

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

    protected void IssueAdultDateValidator(object source, ServerValidateEventArgs args)
    {
        if (!string.IsNullOrEmpty(args.Value))
        {
            args.IsValid = validateAdultAge(args);
        }
    }
    private bool validateAdultAge(ServerValidateEventArgs args)
    {
        DateTime dtEnterDate = Convert.ToDateTime(lobjModel.StringToDateTime(args.Value));
        DateTime dtTodayDate = Convert.ToDateTime(lobjModel.StringToDateTime(DateTime.Now.ToString("dd/MM/yyyy")));
        TimeSpan ts = dtTodayDate.Subtract(dtEnterDate);
        int NoOfDays = ts.Days;
        double yr = NoOfDays / 365.25;
        double day = NoOfDays % 365.25;

        if ((yr >= 12) && (yr < 90))
        {

            return true;
        }
        else if (Math.Round(yr) == 90)
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
    private void BindCountryList()
    {
        drpNationality.Items.Add(new ListItem() { Text = "Afghanistan", Value = "AF" });
        drpNationality.Items.Add(new ListItem() { Text = "Albania", Value = "AL" });
        drpNationality.Items.Add(new ListItem() { Text = "Algeria", Value = "DZ" });
        drpNationality.Items.Add(new ListItem() { Text = "Åland Islands", Value = "AX" });
        drpNationality.Items.Add(new ListItem() { Text = "American Samoa", Value = "AS" });
        drpNationality.Items.Add(new ListItem() { Text = "Andorra", Value = "AD" });
        drpNationality.Items.Add(new ListItem() { Text = "Angola", Value = "AO" });
        drpNationality.Items.Add(new ListItem() { Text = "Anguilla", Value = "AI" });
        drpNationality.Items.Add(new ListItem() { Text = "Antarctica", Value = "AQ" });
        drpNationality.Items.Add(new ListItem() { Text = "Antigua and Barbuda", Value = "AG" });
        drpNationality.Items.Add(new ListItem() { Text = "Argentina", Value = "AR" });
        drpNationality.Items.Add(new ListItem() { Text = "Armenia", Value = "AM" });
        drpNationality.Items.Add(new ListItem() { Text = "Aruba", Value = "AW" });
        drpNationality.Items.Add(new ListItem() { Text = "Australia", Value = "AU" });
        drpNationality.Items.Add(new ListItem() { Text = "Austria", Value = "AT" });
        drpNationality.Items.Add(new ListItem() { Text = "Azerbaijan", Value = "AZ" });
        drpNationality.Items.Add(new ListItem() { Text = "Bahamas (the)", Value = "BS" });
        drpNationality.Items.Add(new ListItem() { Text = "Bahrain", Value = "BH" });
        drpNationality.Items.Add(new ListItem() { Text = "Bangladesh", Value = "BD" });
        drpNationality.Items.Add(new ListItem() { Text = "Barbados", Value = "BB" });
        drpNationality.Items.Add(new ListItem() { Text = "Belarus", Value = "BY" });
        drpNationality.Items.Add(new ListItem() { Text = "Belgium", Value = "BE" });
        drpNationality.Items.Add(new ListItem() { Text = "Belize", Value = "BZ" });
        drpNationality.Items.Add(new ListItem() { Text = "Benin", Value = "BJ" });
        drpNationality.Items.Add(new ListItem() { Text = "Bermuda", Value = "BM" });
        drpNationality.Items.Add(new ListItem() { Text = "Bhutan", Value = "BT" });
        drpNationality.Items.Add(new ListItem() { Text = "Bolivia (Plurinational State of)", Value = "BO" });
        drpNationality.Items.Add(new ListItem() { Text = "Bonaire, Sint Eustatius and Saba", Value = "BQ" });
        drpNationality.Items.Add(new ListItem() { Text = "Bosnia and Herzegovina", Value = "BA" });
        drpNationality.Items.Add(new ListItem() { Text = "Botswana", Value = "BW" });
        drpNationality.Items.Add(new ListItem() { Text = "Bouvet Island", Value = "BV" });
        drpNationality.Items.Add(new ListItem() { Text = "Brazil", Value = "BR" });
        drpNationality.Items.Add(new ListItem() { Text = "British Indian Ocean Territory (the)", Value = "IO" });
        drpNationality.Items.Add(new ListItem() { Text = "Brunei Darussalam", Value = "BN" });
        drpNationality.Items.Add(new ListItem() { Text = "Bulgaria", Value = "BG" });
        drpNationality.Items.Add(new ListItem() { Text = "Burkina Faso", Value = "BF" });
        drpNationality.Items.Add(new ListItem() { Text = "Burundi", Value = "BI" });
        drpNationality.Items.Add(new ListItem() { Text = "Cabo Verde", Value = "CV" });
        drpNationality.Items.Add(new ListItem() { Text = "Cambodia", Value = "KH" });
        drpNationality.Items.Add(new ListItem() { Text = "Cameroon", Value = "CM" });
        drpNationality.Items.Add(new ListItem() { Text = "Canada", Value = "CA" });
        drpNationality.Items.Add(new ListItem() { Text = "Cayman Islands (the)", Value = "KY" });
        drpNationality.Items.Add(new ListItem() { Text = "Central African Republic (the)", Value = "CF" });
        drpNationality.Items.Add(new ListItem() { Text = "Chad", Value = "TD" });
        drpNationality.Items.Add(new ListItem() { Text = "Chile", Value = "CL" });
        drpNationality.Items.Add(new ListItem() { Text = "China", Value = "CN" });
        drpNationality.Items.Add(new ListItem() { Text = "Christmas Island", Value = "CX" });
        drpNationality.Items.Add(new ListItem() { Text = "Cocos (Keeling) Islands (the)", Value = "CC" });
        drpNationality.Items.Add(new ListItem() { Text = "Colombia", Value = "CO" });
        drpNationality.Items.Add(new ListItem() { Text = "Comoros (the)", Value = "KM" });
        drpNationality.Items.Add(new ListItem() { Text = "Congo (the Democratic Republic of the)", Value = "CD" });
        drpNationality.Items.Add(new ListItem() { Text = "Congo (the)", Value = "CG" });
        drpNationality.Items.Add(new ListItem() { Text = "Cook Islands (the)", Value = "CK" });
        drpNationality.Items.Add(new ListItem() { Text = "Costa Rica", Value = "CR" });
        drpNationality.Items.Add(new ListItem() { Text = "Croatia", Value = "HR" });
        drpNationality.Items.Add(new ListItem() { Text = "Cuba", Value = "CU" });
        drpNationality.Items.Add(new ListItem() { Text = "Curaçao", Value = "CW" });
        drpNationality.Items.Add(new ListItem() { Text = "Cyprus", Value = "CY" });
        drpNationality.Items.Add(new ListItem() { Text = "Czechia", Value = "CZ" });
        drpNationality.Items.Add(new ListItem() { Text = "Côte d'Ivoire", Value = "CI" });
        drpNationality.Items.Add(new ListItem() { Text = "Denmark", Value = "DK" });
        drpNationality.Items.Add(new ListItem() { Text = "Djibouti", Value = "DJ" });
        drpNationality.Items.Add(new ListItem() { Text = "Dominica", Value = "DM" });
        drpNationality.Items.Add(new ListItem() { Text = "Dominican Republic (the)", Value = "DO" });
        drpNationality.Items.Add(new ListItem() { Text = "Ecuador", Value = "EC" });
        drpNationality.Items.Add(new ListItem() { Text = "Egypt", Value = "EG" });
        drpNationality.Items.Add(new ListItem() { Text = "El Salvador", Value = "SV" });
        drpNationality.Items.Add(new ListItem() { Text = "Equatorial Guinea", Value = "GQ" });
        drpNationality.Items.Add(new ListItem() { Text = "Eritrea", Value = "ER" });
        drpNationality.Items.Add(new ListItem() { Text = "Estonia", Value = "EE" });
        drpNationality.Items.Add(new ListItem() { Text = "Eswatini", Value = "SZ" });
        drpNationality.Items.Add(new ListItem() { Text = "Ethiopia", Value = "ET" });
        drpNationality.Items.Add(new ListItem() { Text = "Falkland Islands (the) [Malvinas]", Value = "FK" });
        drpNationality.Items.Add(new ListItem() { Text = "Faroe Islands (the)", Value = "FO" });
        drpNationality.Items.Add(new ListItem() { Text = "Fiji", Value = "FJ" });
        drpNationality.Items.Add(new ListItem() { Text = "Finland", Value = "FI" });
        drpNationality.Items.Add(new ListItem() { Text = "France", Value = "FR" });
        drpNationality.Items.Add(new ListItem() { Text = "French Guiana", Value = "GF" });
        drpNationality.Items.Add(new ListItem() { Text = "French Polynesia", Value = "PF" });
        drpNationality.Items.Add(new ListItem() { Text = "French Southern Territories (the)", Value = "TF" });
        drpNationality.Items.Add(new ListItem() { Text = "Gabon", Value = "GA" });
        drpNationality.Items.Add(new ListItem() { Text = "Gambia (the)", Value = "GM" });
        drpNationality.Items.Add(new ListItem() { Text = "Georgia", Value = "GE" });
        drpNationality.Items.Add(new ListItem() { Text = "Germany", Value = "DE" });
        drpNationality.Items.Add(new ListItem() { Text = "Ghana", Value = "GH" });
        drpNationality.Items.Add(new ListItem() { Text = "Gibraltar", Value = "GI" });
        drpNationality.Items.Add(new ListItem() { Text = "Greece", Value = "GR" });
        drpNationality.Items.Add(new ListItem() { Text = "Greenland", Value = "GL" });
        drpNationality.Items.Add(new ListItem() { Text = "Grenada", Value = "GD" });
        drpNationality.Items.Add(new ListItem() { Text = "Guadeloupe", Value = "GP" });
        drpNationality.Items.Add(new ListItem() { Text = "Guam", Value = "GU" });
        drpNationality.Items.Add(new ListItem() { Text = "Guatemala", Value = "GT" });
        drpNationality.Items.Add(new ListItem() { Text = "Guernsey", Value = "GG" });
        drpNationality.Items.Add(new ListItem() { Text = "Guinea", Value = "GN" });
        drpNationality.Items.Add(new ListItem() { Text = "Guinea-Bissau", Value = "GW" });
        drpNationality.Items.Add(new ListItem() { Text = "Guyana", Value = "GY" });
        drpNationality.Items.Add(new ListItem() { Text = "Haiti", Value = "HT" });
        drpNationality.Items.Add(new ListItem() { Text = "Heard Island and McDonald Islands", Value = "HM" });
        drpNationality.Items.Add(new ListItem() { Text = "Holy See (the)", Value = "VA" });
        drpNationality.Items.Add(new ListItem() { Text = "Honduras", Value = "HN" });
        drpNationality.Items.Add(new ListItem() { Text = "Hong Kong", Value = "HK" });
        drpNationality.Items.Add(new ListItem() { Text = "Hungary", Value = "HU" });
        drpNationality.Items.Add(new ListItem() { Text = "Iceland", Value = "IS" });
        drpNationality.Items.Add(new ListItem() { Text = "India", Value = "IN" });
        drpNationality.Items.Add(new ListItem() { Text = "Indonesia", Value = "ID" });
        drpNationality.Items.Add(new ListItem() { Text = "Iran (Islamic Republic of)", Value = "IR" });
        drpNationality.Items.Add(new ListItem() { Text = "Iraq", Value = "IQ" });
        drpNationality.Items.Add(new ListItem() { Text = "Ireland", Value = "IE" });
        drpNationality.Items.Add(new ListItem() { Text = "Isle of Man", Value = "IM" });
        drpNationality.Items.Add(new ListItem() { Text = "Israel", Value = "IL" });
        drpNationality.Items.Add(new ListItem() { Text = "Italy", Value = "IT" });
        drpNationality.Items.Add(new ListItem() { Text = "Jamaica", Value = "JM" });
        drpNationality.Items.Add(new ListItem() { Text = "Japan", Value = "JP" });
        drpNationality.Items.Add(new ListItem() { Text = "Jersey", Value = "JE" });
        drpNationality.Items.Add(new ListItem() { Text = "Jordan", Value = "JO" });
        drpNationality.Items.Add(new ListItem() { Text = "Kazakhstan", Value = "KZ" });
        drpNationality.Items.Add(new ListItem() { Text = "Kenya", Value = "KE" });
        drpNationality.Items.Add(new ListItem() { Text = "Kiribati", Value = "KI" });
        drpNationality.Items.Add(new ListItem() { Text = "Korea (the Democratic People's Republic of)", Value = "KP" });
        drpNationality.Items.Add(new ListItem() { Text = "Korea (the Republic of)", Value = "KR" });
        drpNationality.Items.Add(new ListItem() { Text = "Kuwait", Value = "KW" });
        drpNationality.Items.Add(new ListItem() { Text = "Kyrgyzstan", Value = "KG" });
        drpNationality.Items.Add(new ListItem() { Text = "Lao People's Democratic Republic (the)", Value = "LA" });
        drpNationality.Items.Add(new ListItem() { Text = "Latvia", Value = "LV" });
        drpNationality.Items.Add(new ListItem() { Text = "Lebanon", Value = "LB" });
        drpNationality.Items.Add(new ListItem() { Text = "Lesotho", Value = "LS" });
        drpNationality.Items.Add(new ListItem() { Text = "Liberia", Value = "LR" });
        drpNationality.Items.Add(new ListItem() { Text = "Libya", Value = "LY" });
        drpNationality.Items.Add(new ListItem() { Text = "Liechtenstein", Value = "LI" });
        drpNationality.Items.Add(new ListItem() { Text = "Lithuania", Value = "LT" });
        drpNationality.Items.Add(new ListItem() { Text = "Luxembourg", Value = "LU" });
        drpNationality.Items.Add(new ListItem() { Text = "Macao", Value = "MO" });
        drpNationality.Items.Add(new ListItem() { Text = "Madagascar", Value = "MG" });
        drpNationality.Items.Add(new ListItem() { Text = "Malawi", Value = "MW" });
        drpNationality.Items.Add(new ListItem() { Text = "Malaysia", Value = "MY" });
        drpNationality.Items.Add(new ListItem() { Text = "Maldives", Value = "MV" });
        drpNationality.Items.Add(new ListItem() { Text = "Mali", Value = "ML" });
        drpNationality.Items.Add(new ListItem() { Text = "Malta", Value = "MT" });
        drpNationality.Items.Add(new ListItem() { Text = "Marshall Islands (the)", Value = "MH" });
        drpNationality.Items.Add(new ListItem() { Text = "Martinique", Value = "MQ" });
        drpNationality.Items.Add(new ListItem() { Text = "Mauritania", Value = "MR" });
        drpNationality.Items.Add(new ListItem() { Text = "Mauritius", Value = "MU" });
        drpNationality.Items.Add(new ListItem() { Text = "Mayotte", Value = "YT" });
        drpNationality.Items.Add(new ListItem() { Text = "Mexico", Value = "MX" });
        drpNationality.Items.Add(new ListItem() { Text = "Micronesia (Federated States of)", Value = "FM" });
        drpNationality.Items.Add(new ListItem() { Text = "Moldova (the Republic of)", Value = "MD" });
        drpNationality.Items.Add(new ListItem() { Text = "Monaco", Value = "MC" });
        drpNationality.Items.Add(new ListItem() { Text = "Mongolia", Value = "MN" });
        drpNationality.Items.Add(new ListItem() { Text = "Montenegro", Value = "ME" });
        drpNationality.Items.Add(new ListItem() { Text = "Montserrat", Value = "MS" });
        drpNationality.Items.Add(new ListItem() { Text = "Morocco", Value = "MA" });
        drpNationality.Items.Add(new ListItem() { Text = "Mozambique", Value = "MZ" });
        drpNationality.Items.Add(new ListItem() { Text = "Myanmar", Value = "MM" });
        drpNationality.Items.Add(new ListItem() { Text = "Namibia", Value = "NA" });
        drpNationality.Items.Add(new ListItem() { Text = "Nauru", Value = "NR" });
        drpNationality.Items.Add(new ListItem() { Text = "Nepal", Value = "NP" });
        drpNationality.Items.Add(new ListItem() { Text = "Netherlands (the)", Value = "NL" });
        drpNationality.Items.Add(new ListItem() { Text = "New Caledonia", Value = "NC" });
        drpNationality.Items.Add(new ListItem() { Text = "New Zealand", Value = "NZ" });
        drpNationality.Items.Add(new ListItem() { Text = "Nicaragua", Value = "NI" });
        drpNationality.Items.Add(new ListItem() { Text = "Niger (the)", Value = "NE" });
        drpNationality.Items.Add(new ListItem() { Text = "Nigeria", Value = "NG" });
        drpNationality.Items.Add(new ListItem() { Text = "Niue", Value = "NU" });
        drpNationality.Items.Add(new ListItem() { Text = "Norfolk Island", Value = "NF" });
        drpNationality.Items.Add(new ListItem() { Text = "Northern Mariana Islands (the)", Value = "MP" });
        drpNationality.Items.Add(new ListItem() { Text = "Norway", Value = "NO" });
        drpNationality.Items.Add(new ListItem() { Text = "Oman", Value = "OM" });
        drpNationality.Items.Add(new ListItem() { Text = "Pakistan", Value = "PK" });
        drpNationality.Items.Add(new ListItem() { Text = "Palau", Value = "PW" });
        drpNationality.Items.Add(new ListItem() { Text = "Palestine, State of", Value = "PS" });
        drpNationality.Items.Add(new ListItem() { Text = "Panama", Value = "PA" });
        drpNationality.Items.Add(new ListItem() { Text = "Papua New Guinea", Value = "PG" });
        drpNationality.Items.Add(new ListItem() { Text = "Paraguay", Value = "PY" });
        drpNationality.Items.Add(new ListItem() { Text = "Peru", Value = "PE" });
        drpNationality.Items.Add(new ListItem() { Text = "Philippines (the)", Value = "PH" });
        drpNationality.Items.Add(new ListItem() { Text = "Pitcairn", Value = "PN" });
        drpNationality.Items.Add(new ListItem() { Text = "Poland", Value = "PL" });
        drpNationality.Items.Add(new ListItem() { Text = "Portugal", Value = "PT" });
        drpNationality.Items.Add(new ListItem() { Text = "Puerto Rico", Value = "PR" });
        drpNationality.Items.Add(new ListItem() { Text = "Qatar", Value = "QA" });
        drpNationality.Items.Add(new ListItem() { Text = "Republic of North Macedonia", Value = "MK" });
        drpNationality.Items.Add(new ListItem() { Text = "Romania", Value = "RO" });
        drpNationality.Items.Add(new ListItem() { Text = "Russian Federation (the)", Value = "RU" });
        drpNationality.Items.Add(new ListItem() { Text = "Rwanda", Value = "RW" });
        drpNationality.Items.Add(new ListItem() { Text = "Réunion", Value = "RE" });
        drpNationality.Items.Add(new ListItem() { Text = "Saint Barthélemy", Value = "BL" });
        drpNationality.Items.Add(new ListItem() { Text = "Saint Helena, Ascension and Tristan da Cunha", Value = "SH" });
        drpNationality.Items.Add(new ListItem() { Text = "Saint Kitts and Nevis", Value = "KN" });
        drpNationality.Items.Add(new ListItem() { Text = "Saint Lucia", Value = "LC" });
        drpNationality.Items.Add(new ListItem() { Text = "Saint Martin (French part)", Value = "MF" });
        drpNationality.Items.Add(new ListItem() { Text = "Saint Pierre and Miquelon", Value = "PM" });
        drpNationality.Items.Add(new ListItem() { Text = "Saint Vincent and the Grenadines", Value = "VC" });
        drpNationality.Items.Add(new ListItem() { Text = "Samoa", Value = "WS" });
        drpNationality.Items.Add(new ListItem() { Text = "San Marino", Value = "SM" });
        drpNationality.Items.Add(new ListItem() { Text = "Sao Tome and Principe", Value = "ST" });
        drpNationality.Items.Add(new ListItem() { Text = "Saudi Arabia", Value = "SA" });
        drpNationality.Items.Add(new ListItem() { Text = "Senegal", Value = "SN" });
        drpNationality.Items.Add(new ListItem() { Text = "Serbia", Value = "RS" });
        drpNationality.Items.Add(new ListItem() { Text = "Seychelles", Value = "SC" });
        drpNationality.Items.Add(new ListItem() { Text = "Sierra Leone", Value = "SL" });
        drpNationality.Items.Add(new ListItem() { Text = "Singapore", Value = "SG" });
        drpNationality.Items.Add(new ListItem() { Text = "Sint Maarten (Dutch part)", Value = "SX" });
        drpNationality.Items.Add(new ListItem() { Text = "Slovakia", Value = "SK" });
        drpNationality.Items.Add(new ListItem() { Text = "Slovenia", Value = "SI" });
        drpNationality.Items.Add(new ListItem() { Text = "Solomon Islands", Value = "SB" });
        drpNationality.Items.Add(new ListItem() { Text = "Somalia", Value = "SO" });
        drpNationality.Items.Add(new ListItem() { Text = "South Africa", Value = "ZA" });
        drpNationality.Items.Add(new ListItem() { Text = "South Georgia and the South Sandwich Islands", Value = "GS" });
        drpNationality.Items.Add(new ListItem() { Text = "South Sudan", Value = "SS" });
        drpNationality.Items.Add(new ListItem() { Text = "Spain", Value = "ES" });
        drpNationality.Items.Add(new ListItem() { Text = "Sri Lanka", Value = "LK" });
        drpNationality.Items.Add(new ListItem() { Text = "Sudan (the)", Value = "SD" });
        drpNationality.Items.Add(new ListItem() { Text = "Suriname", Value = "SR" });
        drpNationality.Items.Add(new ListItem() { Text = "Svalbard and Jan Mayen", Value = "SJ" });
        drpNationality.Items.Add(new ListItem() { Text = "Sweden", Value = "SE" });
        drpNationality.Items.Add(new ListItem() { Text = "Switzerland", Value = "CH" });
        drpNationality.Items.Add(new ListItem() { Text = "Syrian Arab Republic", Value = "SY" });
        drpNationality.Items.Add(new ListItem() { Text = "Taiwan (Province of China)", Value = "TW" });
        drpNationality.Items.Add(new ListItem() { Text = "Tajikistan", Value = "TJ" });
        drpNationality.Items.Add(new ListItem() { Text = "Tanzania, United Republic of", Value = "TZ" });
        drpNationality.Items.Add(new ListItem() { Text = "Thailand", Value = "TH" });
        drpNationality.Items.Add(new ListItem() { Text = "Timor-Leste", Value = "TL" });
        drpNationality.Items.Add(new ListItem() { Text = "Togo", Value = "TG" });
        drpNationality.Items.Add(new ListItem() { Text = "Tokelau", Value = "TK" });
        drpNationality.Items.Add(new ListItem() { Text = "Tonga", Value = "TO" });
        drpNationality.Items.Add(new ListItem() { Text = "Trinidad and Tobago", Value = "TT" });
        drpNationality.Items.Add(new ListItem() { Text = "Tunisia", Value = "TN" });
        drpNationality.Items.Add(new ListItem() { Text = "Turkey", Value = "TR" });
        drpNationality.Items.Add(new ListItem() { Text = "Turkmenistan", Value = "TM" });
        drpNationality.Items.Add(new ListItem() { Text = "Turks and Caicos Islands (the)", Value = "TC" });
        drpNationality.Items.Add(new ListItem() { Text = "Tuvalu", Value = "TV" });
        drpNationality.Items.Add(new ListItem() { Text = "Uganda", Value = "UG" });
        drpNationality.Items.Add(new ListItem() { Text = "Ukraine", Value = "UA" });
        drpNationality.Items.Add(new ListItem() { Text = "United Arab Emirates (the)", Value = "AE" });
        drpNationality.Items.Add(new ListItem() { Text = "United Kingdom of Great Britain and Northern Ireland (the)", Value = "GB" });
        drpNationality.Items.Add(new ListItem() { Text = "United States Minor Outlying Islands (the)", Value = "UM" });
        drpNationality.Items.Add(new ListItem() { Text = "United States of America (the)", Value = "US" });
        drpNationality.Items.Add(new ListItem() { Text = "Uruguay", Value = "UY" });
        drpNationality.Items.Add(new ListItem() { Text = "Uzbekistan", Value = "UZ" });
        drpNationality.Items.Add(new ListItem() { Text = "Vanuatu", Value = "VU" });
        drpNationality.Items.Add(new ListItem() { Text = "Venezuela (Bolivarian Republic of)", Value = "VE" });
        drpNationality.Items.Add(new ListItem() { Text = "Viet Nam", Value = "VN" });
        drpNationality.Items.Add(new ListItem() { Text = "Virgin Islands (British)", Value = "VG" });
        drpNationality.Items.Add(new ListItem() { Text = "Virgin Islands (U.S.)", Value = "VI" });
        drpNationality.Items.Add(new ListItem() { Text = "Wallis and Futuna", Value = "WF" });
        drpNationality.Items.Add(new ListItem() { Text = "Western Sahara", Value = "EH" });
        drpNationality.Items.Add(new ListItem() { Text = "Yemen", Value = "YE" });
        drpNationality.Items.Add(new ListItem() { Text = "Zambia", Value = "ZM" });
        drpNationality.Items.Add(new ListItem() { Text = "Zimbabwe", Value = "ZW" });
        drpNationality.DataBind();
    }
}