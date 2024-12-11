using ABC.Model;
using BeMyGuest.Entities;
using CB.IBE.Platform.Masters.Entities;
using Core.Platform.Booking.Entities;
using Core.Platform.Member.Entites;
using Core.Platform.MemberActivity.Constants;
using Core.Platform.MemberActivity.Entities;
using Core.Platform.OTP.Entities;
using Core.Platform.ProgramMaster.Entities;
using Framework.EnterpriseLibrary.Adapters;
using Framework.EnterpriseLibrary.UniqueNumberGenerator;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web;
using System.Web.Services;
using System.Web.UI.WebControls;
using BookingRequest = BeMyGuest.Entities.BookingRequest;

public partial class ExperienceProductBookingDetails : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                string uuid = string.Empty;
                if (Request.QueryString.Count > 0)
                {
                    uuid = Convert.ToString(Request.QueryString["puuid"]);
                    hrefBookingDetailsId.HRef = "ExperienceProductDetails.aspx?uuid=" + uuid;
                    hrefEditbuttonId.HRef = "ExperienceProductDetails.aspx?uuid=" + uuid;
                    string PGTranPct = Convert.ToString(ConfigurationManager.AppSettings["PGTranPct"]) + "%";
                    if (Convert.ToInt32(ConfigurationManager.AppSettings["PGTranPct"]) == 0)
                    {
                        txtImpNote.Attributes.Add("style", "d-none");
                    }
                    else
                    {
                        txtImpNote.Attributes.Remove("style");
                        txtImpNote.InnerHtml = "<span class=\"heading-bold\">Please note: </span>All the transactions are subjected to " + PGTranPct + " payment gateway charge. You will now be redirected to Payment gateway for completing your transactions.";
                    }

                    BindCountryList();
                }

            }
        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("ExperienceProductBookingDetails.aspx PageLoad Exception: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
        }
    }

    private void BindCountryList()
    {
        drpNationality.Items.Add(new ListItem() { Text = "Afghanistan", Value = "AFG" });
        drpNationality.Items.Add(new ListItem() { Text = "Albania", Value = "ALB" });
        drpNationality.Items.Add(new ListItem() { Text = "Algeria", Value = "DZA" });
        drpNationality.Items.Add(new ListItem() { Text = "Åland Islands", Value = "ALA" });
        drpNationality.Items.Add(new ListItem() { Text = "American Samoa", Value = "ASM" });
        drpNationality.Items.Add(new ListItem() { Text = "Andorra", Value = "AND" });
        drpNationality.Items.Add(new ListItem() { Text = "Angola", Value = "AGO" });
        drpNationality.Items.Add(new ListItem() { Text = "Anguilla", Value = "AIA" });
        drpNationality.Items.Add(new ListItem() { Text = "Antarctica", Value = "ATA" });
        drpNationality.Items.Add(new ListItem() { Text = "Antigua and Barbuda", Value = "ATG" });
        drpNationality.Items.Add(new ListItem() { Text = "Argentina", Value = "ARG" });
        drpNationality.Items.Add(new ListItem() { Text = "Armenia", Value = "ARM" });
        drpNationality.Items.Add(new ListItem() { Text = "Aruba", Value = "ABW" });
        drpNationality.Items.Add(new ListItem() { Text = "Australia", Value = "AUS" });
        drpNationality.Items.Add(new ListItem() { Text = "Austria", Value = "AUT" });
        drpNationality.Items.Add(new ListItem() { Text = "Azerbaijan", Value = "AZE" });
        drpNationality.Items.Add(new ListItem() { Text = "Bahamas (the)", Value = "BHS" });
        drpNationality.Items.Add(new ListItem() { Text = "Bahrain", Value = "BHR" });
        drpNationality.Items.Add(new ListItem() { Text = "Bangladesh", Value = "BGD" });
        drpNationality.Items.Add(new ListItem() { Text = "Barbados", Value = "BRB" });
        drpNationality.Items.Add(new ListItem() { Text = "Belarus", Value = "BLR" });
        drpNationality.Items.Add(new ListItem() { Text = "Belgium", Value = "BEL" });
        drpNationality.Items.Add(new ListItem() { Text = "Belize", Value = "BLZ" });
        drpNationality.Items.Add(new ListItem() { Text = "Benin", Value = "BEN" });
        drpNationality.Items.Add(new ListItem() { Text = "Bermuda", Value = "BMU" });
        drpNationality.Items.Add(new ListItem() { Text = "Bhutan", Value = "BTN" });
        drpNationality.Items.Add(new ListItem() { Text = "Bolivia (Plurinational State of)", Value = "BOL" });
        drpNationality.Items.Add(new ListItem() { Text = "Bonaire, Sint Eustatius and Saba", Value = "BES" });
        drpNationality.Items.Add(new ListItem() { Text = "Bosnia and Herzegovina", Value = "BIH" });
        drpNationality.Items.Add(new ListItem() { Text = "Botswana", Value = "BWA" });
        drpNationality.Items.Add(new ListItem() { Text = "Bouvet Island", Value = "BVT" });
        drpNationality.Items.Add(new ListItem() { Text = "Brazil", Value = "BRA" });
        drpNationality.Items.Add(new ListItem() { Text = "British Indian Ocean Territory (the)", Value = "IOT" });
        drpNationality.Items.Add(new ListItem() { Text = "Brunei Darussalam", Value = "BRN" });
        drpNationality.Items.Add(new ListItem() { Text = "Bulgaria", Value = "BGR" });
        drpNationality.Items.Add(new ListItem() { Text = "Burkina Faso", Value = "BFA" });
        drpNationality.Items.Add(new ListItem() { Text = "Burundi", Value = "BDI" });
        drpNationality.Items.Add(new ListItem() { Text = "Cabo Verde", Value = "CPV" });
        drpNationality.Items.Add(new ListItem() { Text = "Cambodia", Value = "KHM" });
        drpNationality.Items.Add(new ListItem() { Text = "Cameroon", Value = "CMR" });
        drpNationality.Items.Add(new ListItem() { Text = "Canada", Value = "CAN" });
        drpNationality.Items.Add(new ListItem() { Text = "Cayman Islands (the)", Value = "CYM" });
        drpNationality.Items.Add(new ListItem() { Text = "Central African Republic (the)", Value = "CAF" });
        drpNationality.Items.Add(new ListItem() { Text = "Chad", Value = "TCD" });
        drpNationality.Items.Add(new ListItem() { Text = "Chile", Value = "CHL" });
        drpNationality.Items.Add(new ListItem() { Text = "China", Value = "CHN" });
        drpNationality.Items.Add(new ListItem() { Text = "Christmas Island", Value = "CXR" });
        drpNationality.Items.Add(new ListItem() { Text = "Cocos (Keeling) Islands (the)", Value = "CCK" });
        drpNationality.Items.Add(new ListItem() { Text = "Colombia", Value = "COL" });
        drpNationality.Items.Add(new ListItem() { Text = "Comoros (the)", Value = "COM" });
        drpNationality.Items.Add(new ListItem() { Text = "Congo (the Democratic Republic of the)", Value = "COD" });
        drpNationality.Items.Add(new ListItem() { Text = "Congo (the)", Value = "COG" });
        drpNationality.Items.Add(new ListItem() { Text = "Cook Islands (the)", Value = "COK" });
        drpNationality.Items.Add(new ListItem() { Text = "Costa Rica", Value = "CRI" });
        drpNationality.Items.Add(new ListItem() { Text = "Croatia", Value = "HRV" });
        drpNationality.Items.Add(new ListItem() { Text = "Cuba", Value = "CUB" });
        drpNationality.Items.Add(new ListItem() { Text = "Curaçao", Value = "CUW" });
        drpNationality.Items.Add(new ListItem() { Text = "Cyprus", Value = "CYP" });
        drpNationality.Items.Add(new ListItem() { Text = "Czechia", Value = "CZE" });
        drpNationality.Items.Add(new ListItem() { Text = "Côte d'Ivoire", Value = "CIV" });
        drpNationality.Items.Add(new ListItem() { Text = "Denmark", Value = "DNK" });
        drpNationality.Items.Add(new ListItem() { Text = "Djibouti", Value = "DJI" });
        drpNationality.Items.Add(new ListItem() { Text = "Dominica", Value = "DMA" });
        drpNationality.Items.Add(new ListItem() { Text = "Dominican Republic (the)", Value = "DOM" });
        drpNationality.Items.Add(new ListItem() { Text = "Ecuador", Value = "ECU" });
        drpNationality.Items.Add(new ListItem() { Text = "Egypt", Value = "EGY" });
        drpNationality.Items.Add(new ListItem() { Text = "El Salvador", Value = "SLV" });
        drpNationality.Items.Add(new ListItem() { Text = "Equatorial Guinea", Value = "GNQ" });
        drpNationality.Items.Add(new ListItem() { Text = "Eritrea", Value = "ERI" });
        drpNationality.Items.Add(new ListItem() { Text = "Estonia", Value = "EST" });
        drpNationality.Items.Add(new ListItem() { Text = "Eswatini", Value = "SWZ" });
        drpNationality.Items.Add(new ListItem() { Text = "Ethiopia", Value = "ETH" });
        drpNationality.Items.Add(new ListItem() { Text = "Falkland Islands (the) [Malvinas]", Value = "FLK" });
        drpNationality.Items.Add(new ListItem() { Text = "Faroe Islands (the)", Value = "FRO" });
        drpNationality.Items.Add(new ListItem() { Text = "Fiji", Value = "FJI" });
        drpNationality.Items.Add(new ListItem() { Text = "Finland", Value = "FIN" });
        drpNationality.Items.Add(new ListItem() { Text = "France", Value = "FRA" });
        drpNationality.Items.Add(new ListItem() { Text = "French Guiana", Value = "GUF" });
        drpNationality.Items.Add(new ListItem() { Text = "French Polynesia", Value = "PYF" });
        drpNationality.Items.Add(new ListItem() { Text = "French Southern Territories (the)", Value = "ATF" });
        drpNationality.Items.Add(new ListItem() { Text = "Gabon", Value = "GAB" });
        drpNationality.Items.Add(new ListItem() { Text = "Gambia (the)", Value = "GMB" });
        drpNationality.Items.Add(new ListItem() { Text = "Georgia", Value = "GEO" });
        drpNationality.Items.Add(new ListItem() { Text = "Germany", Value = "DEU" });
        drpNationality.Items.Add(new ListItem() { Text = "Ghana", Value = "GHA" });
        drpNationality.Items.Add(new ListItem() { Text = "Gibraltar", Value = "GIB" });
        drpNationality.Items.Add(new ListItem() { Text = "Greece", Value = "GRC" });
        drpNationality.Items.Add(new ListItem() { Text = "Greenland", Value = "GRL" });
        drpNationality.Items.Add(new ListItem() { Text = "Grenada", Value = "GRD" });
        drpNationality.Items.Add(new ListItem() { Text = "Guadeloupe", Value = "GLP" });
        drpNationality.Items.Add(new ListItem() { Text = "Guam", Value = "GUM" });
        drpNationality.Items.Add(new ListItem() { Text = "Guatemala", Value = "GTM" });
        drpNationality.Items.Add(new ListItem() { Text = "Guernsey", Value = "GGY" });
        drpNationality.Items.Add(new ListItem() { Text = "Guinea", Value = "GIN" });
        drpNationality.Items.Add(new ListItem() { Text = "Guinea-Bissau", Value = "GNB" });
        drpNationality.Items.Add(new ListItem() { Text = "Guyana", Value = "GUY" });
        drpNationality.Items.Add(new ListItem() { Text = "Haiti", Value = "HTI" });
        drpNationality.Items.Add(new ListItem() { Text = "Heard Island and McDonald Islands", Value = "HMD" });
        drpNationality.Items.Add(new ListItem() { Text = "Holy See (the)", Value = "VAT" });
        drpNationality.Items.Add(new ListItem() { Text = "Honduras", Value = "HND" });
        drpNationality.Items.Add(new ListItem() { Text = "Hong Kong", Value = "HKG" });
        drpNationality.Items.Add(new ListItem() { Text = "Hungary", Value = "HUN" });
        drpNationality.Items.Add(new ListItem() { Text = "Iceland", Value = "ISL" });
        drpNationality.Items.Add(new ListItem() { Text = "India", Value = "IND" });
        drpNationality.Items.Add(new ListItem() { Text = "Indonesia", Value = "IDN" });
        drpNationality.Items.Add(new ListItem() { Text = "Iran (Islamic Republic of)", Value = "IRN" });
        drpNationality.Items.Add(new ListItem() { Text = "Iraq", Value = "IRQ" });
        drpNationality.Items.Add(new ListItem() { Text = "Ireland", Value = "IRL" });
        drpNationality.Items.Add(new ListItem() { Text = "Isle of Man", Value = "IMN" });
        drpNationality.Items.Add(new ListItem() { Text = "Israel", Value = "ISR" });
        drpNationality.Items.Add(new ListItem() { Text = "Italy", Value = "ITA" });
        drpNationality.Items.Add(new ListItem() { Text = "Jamaica", Value = "JAM" });
        drpNationality.Items.Add(new ListItem() { Text = "Japan", Value = "JPN" });
        drpNationality.Items.Add(new ListItem() { Text = "Jersey", Value = "JEY" });
        drpNationality.Items.Add(new ListItem() { Text = "Jordan", Value = "JOR" });
        drpNationality.Items.Add(new ListItem() { Text = "Kazakhstan", Value = "KAZ" });
        drpNationality.Items.Add(new ListItem() { Text = "Kenya", Value = "KEN" });
        drpNationality.Items.Add(new ListItem() { Text = "Kiribati", Value = "KIR" });
        drpNationality.Items.Add(new ListItem() { Text = "Korea (the Democratic People's Republic of)", Value = "PRK" });
        drpNationality.Items.Add(new ListItem() { Text = "Korea (the Republic of)", Value = "KOR" });
        drpNationality.Items.Add(new ListItem() { Text = "Kuwait", Value = "KWT" });
        drpNationality.Items.Add(new ListItem() { Text = "Kyrgyzstan", Value = "KGZ" });
        drpNationality.Items.Add(new ListItem() { Text = "Lao People's Democratic Republic (the)", Value = "LAO" });
        drpNationality.Items.Add(new ListItem() { Text = "Latvia", Value = "LVA" });
        drpNationality.Items.Add(new ListItem() { Text = "Lebanon", Value = "LBN" });
        drpNationality.Items.Add(new ListItem() { Text = "Lesotho", Value = "LSO" });
        drpNationality.Items.Add(new ListItem() { Text = "Liberia", Value = "LBR" });
        drpNationality.Items.Add(new ListItem() { Text = "Libya", Value = "LBY" });
        drpNationality.Items.Add(new ListItem() { Text = "Liechtenstein", Value = "LIE" });
        drpNationality.Items.Add(new ListItem() { Text = "Lithuania", Value = "LTU" });
        drpNationality.Items.Add(new ListItem() { Text = "Luxembourg", Value = "LUX" });
        drpNationality.Items.Add(new ListItem() { Text = "Macao", Value = "MAC" });
        drpNationality.Items.Add(new ListItem() { Text = "Madagascar", Value = "MDG" });
        drpNationality.Items.Add(new ListItem() { Text = "Malawi", Value = "MWI" });
        drpNationality.Items.Add(new ListItem() { Text = "Malaysia", Value = "MYS" });
        drpNationality.Items.Add(new ListItem() { Text = "Maldives", Value = "MDV" });
        drpNationality.Items.Add(new ListItem() { Text = "Mali", Value = "MLI" });
        drpNationality.Items.Add(new ListItem() { Text = "Malta", Value = "MLT" });
        drpNationality.Items.Add(new ListItem() { Text = "Marshall Islands (the)", Value = "MHL" });
        drpNationality.Items.Add(new ListItem() { Text = "Martinique", Value = "MTQ" });
        drpNationality.Items.Add(new ListItem() { Text = "Mauritania", Value = "MRT" });
        drpNationality.Items.Add(new ListItem() { Text = "Mauritius", Value = "MUS" });
        drpNationality.Items.Add(new ListItem() { Text = "Mayotte", Value = "MYT" });
        drpNationality.Items.Add(new ListItem() { Text = "Mexico", Value = "MEX" });
        drpNationality.Items.Add(new ListItem() { Text = "Micronesia (Federated States of)", Value = "FSM" });
        drpNationality.Items.Add(new ListItem() { Text = "Moldova (the Republic of)", Value = "MDA" });
        drpNationality.Items.Add(new ListItem() { Text = "Monaco", Value = "MCO" });
        drpNationality.Items.Add(new ListItem() { Text = "Mongolia", Value = "MNG" });
        drpNationality.Items.Add(new ListItem() { Text = "Montenegro", Value = "MNE" });
        drpNationality.Items.Add(new ListItem() { Text = "Montserrat", Value = "MSR" });
        drpNationality.Items.Add(new ListItem() { Text = "Morocco", Value = "MAR" });
        drpNationality.Items.Add(new ListItem() { Text = "Mozambique", Value = "MOZ" });
        drpNationality.Items.Add(new ListItem() { Text = "Myanmar", Value = "MMR" });
        drpNationality.Items.Add(new ListItem() { Text = "Namibia", Value = "NAM" });
        drpNationality.Items.Add(new ListItem() { Text = "Nauru", Value = "NRU" });
        drpNationality.Items.Add(new ListItem() { Text = "Nepal", Value = "NPL" });
        drpNationality.Items.Add(new ListItem() { Text = "Netherlands (the)", Value = "NLD" });
        drpNationality.Items.Add(new ListItem() { Text = "New Caledonia", Value = "NCL" });
        drpNationality.Items.Add(new ListItem() { Text = "New Zealand", Value = "NZL" });
        drpNationality.Items.Add(new ListItem() { Text = "Nicaragua", Value = "NIC" });
        drpNationality.Items.Add(new ListItem() { Text = "Niger (the)", Value = "NER" });
        drpNationality.Items.Add(new ListItem() { Text = "Nigeria", Value = "NGA" });
        drpNationality.Items.Add(new ListItem() { Text = "Niue", Value = "NIU" });
        drpNationality.Items.Add(new ListItem() { Text = "Norfolk Island", Value = "NFK" });
        drpNationality.Items.Add(new ListItem() { Text = "Northern Mariana Islands (the)", Value = "MNP" });
        drpNationality.Items.Add(new ListItem() { Text = "Norway", Value = "NOR" });
        drpNationality.Items.Add(new ListItem() { Text = "Oman", Value = "OMN" });
        drpNationality.Items.Add(new ListItem() { Text = "Pakistan", Value = "PAK" });
        drpNationality.Items.Add(new ListItem() { Text = "Palau", Value = "PLW" });
        drpNationality.Items.Add(new ListItem() { Text = "Palestine, State of", Value = "PSE" });
        drpNationality.Items.Add(new ListItem() { Text = "Panama", Value = "PAN" });
        drpNationality.Items.Add(new ListItem() { Text = "Papua New Guinea", Value = "PNG" });
        drpNationality.Items.Add(new ListItem() { Text = "Paraguay", Value = "PRY" });
        drpNationality.Items.Add(new ListItem() { Text = "Peru", Value = "PER" });
        drpNationality.Items.Add(new ListItem() { Text = "Philippines (the)", Value = "PHL" });
        drpNationality.Items.Add(new ListItem() { Text = "Pitcairn", Value = "PCN" });
        drpNationality.Items.Add(new ListItem() { Text = "Poland", Value = "POL" });
        drpNationality.Items.Add(new ListItem() { Text = "Portugal", Value = "PRT" });
        drpNationality.Items.Add(new ListItem() { Text = "Puerto Rico", Value = "PRI" });
        drpNationality.Items.Add(new ListItem() { Text = "Qatar", Value = "QAT" });
        drpNationality.Items.Add(new ListItem() { Text = "Republic of North Macedonia", Value = "MKD" });
        drpNationality.Items.Add(new ListItem() { Text = "Romania", Value = "ROU" });
        drpNationality.Items.Add(new ListItem() { Text = "Russian Federation (the)", Value = "RUS" });
        drpNationality.Items.Add(new ListItem() { Text = "Rwanda", Value = "RWA" });
        drpNationality.Items.Add(new ListItem() { Text = "Réunion", Value = "REU" });
        drpNationality.Items.Add(new ListItem() { Text = "Saint Barthélemy", Value = "BLM" });
        drpNationality.Items.Add(new ListItem() { Text = "Saint Helena, Ascension and Tristan da Cunha", Value = "SHN" });
        drpNationality.Items.Add(new ListItem() { Text = "Saint Kitts and Nevis", Value = "KNA" });
        drpNationality.Items.Add(new ListItem() { Text = "Saint Lucia", Value = "LCA" });
        drpNationality.Items.Add(new ListItem() { Text = "Saint Martin (French part)", Value = "MAF" });
        drpNationality.Items.Add(new ListItem() { Text = "Saint Pierre and Miquelon", Value = "SPM" });
        drpNationality.Items.Add(new ListItem() { Text = "Saint Vincent and the Grenadines", Value = "VCT" });
        drpNationality.Items.Add(new ListItem() { Text = "Samoa", Value = "WSM" });
        drpNationality.Items.Add(new ListItem() { Text = "San Marino", Value = "SMR" });
        drpNationality.Items.Add(new ListItem() { Text = "Sao Tome and Principe", Value = "STP" });
        drpNationality.Items.Add(new ListItem() { Text = "Saudi Arabia", Value = "SAU" });
        drpNationality.Items.Add(new ListItem() { Text = "Senegal", Value = "SEN" });
        drpNationality.Items.Add(new ListItem() { Text = "Serbia", Value = "SRB" });
        drpNationality.Items.Add(new ListItem() { Text = "Seychelles", Value = "SYC" });
        drpNationality.Items.Add(new ListItem() { Text = "Sierra Leone", Value = "SLE" });
        drpNationality.Items.Add(new ListItem() { Text = "Singapore", Value = "SGP" });
        drpNationality.Items.Add(new ListItem() { Text = "Sint Maarten (Dutch part)", Value = "SXM" });
        drpNationality.Items.Add(new ListItem() { Text = "Slovakia", Value = "SVK" });
        drpNationality.Items.Add(new ListItem() { Text = "Slovenia", Value = "SVN" });
        drpNationality.Items.Add(new ListItem() { Text = "Solomon Islands", Value = "SLB" });
        drpNationality.Items.Add(new ListItem() { Text = "Somalia", Value = "SOM" });
        drpNationality.Items.Add(new ListItem() { Text = "South Africa", Value = "ZAF" });
        drpNationality.Items.Add(new ListItem() { Text = "South Georgia and the South Sandwich Islands", Value = "SGS" });
        drpNationality.Items.Add(new ListItem() { Text = "South Sudan", Value = "SSD" });
        drpNationality.Items.Add(new ListItem() { Text = "Spain", Value = "ESP" });
        drpNationality.Items.Add(new ListItem() { Text = "Sri Lanka", Value = "LKA" });
        drpNationality.Items.Add(new ListItem() { Text = "Sudan (the)", Value = "SDN" });
        drpNationality.Items.Add(new ListItem() { Text = "Suriname", Value = "SUR" });
        drpNationality.Items.Add(new ListItem() { Text = "Svalbard and Jan Mayen", Value = "SJM" });
        drpNationality.Items.Add(new ListItem() { Text = "Sweden", Value = "SWE" });
        drpNationality.Items.Add(new ListItem() { Text = "Switzerland", Value = "CHE" });
        drpNationality.Items.Add(new ListItem() { Text = "Syrian Arab Republic", Value = "SYR" });
        drpNationality.Items.Add(new ListItem() { Text = "Taiwan (Province of China)", Value = "TWN" });
        drpNationality.Items.Add(new ListItem() { Text = "Tajikistan", Value = "TJK" });
        drpNationality.Items.Add(new ListItem() { Text = "Tanzania, United Republic of", Value = "TZA" });
        drpNationality.Items.Add(new ListItem() { Text = "Thailand", Value = "THA" });
        drpNationality.Items.Add(new ListItem() { Text = "Timor-Leste", Value = "TLS" });
        drpNationality.Items.Add(new ListItem() { Text = "Togo", Value = "TGO" });
        drpNationality.Items.Add(new ListItem() { Text = "Tokelau", Value = "TKL" });
        drpNationality.Items.Add(new ListItem() { Text = "Tonga", Value = "TON" });
        drpNationality.Items.Add(new ListItem() { Text = "Trinidad and Tobago", Value = "TTO" });
        drpNationality.Items.Add(new ListItem() { Text = "Tunisia", Value = "TUN" });
        drpNationality.Items.Add(new ListItem() { Text = "Turkey", Value = "TUR" });
        drpNationality.Items.Add(new ListItem() { Text = "Turkmenistan", Value = "TKM" });
        drpNationality.Items.Add(new ListItem() { Text = "Turks and Caicos Islands (the)", Value = "TCA" });
        drpNationality.Items.Add(new ListItem() { Text = "Tuvalu", Value = "TUV" });
        drpNationality.Items.Add(new ListItem() { Text = "Uganda", Value = "UGA" });
        drpNationality.Items.Add(new ListItem() { Text = "Ukraine", Value = "UKR" });
        drpNationality.Items.Add(new ListItem() { Text = "United Arab Emirates (the)", Value = "ARE" });
        drpNationality.Items.Add(new ListItem() { Text = "United Kingdom of Great Britain and Northern Ireland (the)", Value = "GBR" });
        drpNationality.Items.Add(new ListItem() { Text = "United States Minor Outlying Islands (the)", Value = "UMI" });
        drpNationality.Items.Add(new ListItem() { Text = "United States of America (the)", Value = "USA" });
        drpNationality.Items.Add(new ListItem() { Text = "Uruguay", Value = "URY" });
        drpNationality.Items.Add(new ListItem() { Text = "Uzbekistan", Value = "UZB" });
        drpNationality.Items.Add(new ListItem() { Text = "Vanuatu", Value = "VUT" });
        drpNationality.Items.Add(new ListItem() { Text = "Venezuela (Bolivarian Republic of)", Value = "VEN" });
        drpNationality.Items.Add(new ListItem() { Text = "Viet Nam", Value = "VNM" });
        drpNationality.Items.Add(new ListItem() { Text = "Virgin Islands (British)", Value = "VGB" });
        drpNationality.Items.Add(new ListItem() { Text = "Virgin Islands (U.S.)", Value = "VIR" });
        drpNationality.Items.Add(new ListItem() { Text = "Wallis and Futuna", Value = "WLF" });
        drpNationality.Items.Add(new ListItem() { Text = "Western Sahara", Value = "ESH" });
        drpNationality.Items.Add(new ListItem() { Text = "Yemen", Value = "YEM" });
        drpNationality.Items.Add(new ListItem() { Text = "Zambia", Value = "ZMB" });
        drpNationality.Items.Add(new ListItem() { Text = "Zimbabwe", Value = "ZWE" });

        drpNationality.DataBind();


    }

    [WebMethod]
    public static string GetPaymentDetails( string adultCount, string childrenCount, string seniorsCount, string ptuuid, string puuid, string selectedDate, string timeslotuuid)
    {
        string lstrProductPaymentDetails = string.Empty;
        string lstrbalanceDetails = string.Empty;
        ABCModel lobjModel = new ABCModel();
        try
        {
            ProgramDefinition lobjProgramDefinition = lobjModel.GetProgramMaster();
            MemberDetails lobjMemberDetails = HttpContext.Current.Session["MemberDetails"] as MemberDetails;
            string lstrCurrency = lobjModel.GetDefaultCurrency();
            int lintABCBankPoints = lobjModel.CheckAvailbility(lobjMemberDetails.MemberRelationsList.Find(l => l.RelationType.Equals(RelationType.LBMS)).RelationReference, Convert.ToInt32(RelationType.LBMS), lstrCurrency, lobjProgramDefinition.ProgramId);
            if (!string.IsNullOrEmpty(puuid))
            {
                ProductInfoResponse productInfoResponse = new ProductInfoResponse();
                if (HttpContext.Current.Session["ProductInfo"] != null)
                {
                    productInfoResponse = HttpContext.Current.Session["ProductInfo"] as ProductInfoResponse;
                }
                else
                {
                    ProductInfoRequest productInfoRequest = new ProductInfoRequest();
                    productInfoRequest.uuid = puuid;
                    productInfoResponse = lobjModel.GetProductInfo(productInfoRequest);
                }
                if (productInfoResponse != null)
                {
                    foreach (var productTypeItem in productInfoResponse.producttypedetails.item_uuid)
                    {
                        ProductTypesPriceByDateRequest productTypesPriceByDateRequest = new ProductTypesPriceByDateRequest();
                        productTypesPriceByDateRequest.uuid = productTypeItem.uuid;
                        DateTime enteredDate = DateTime.ParseExact(selectedDate, "dd-MM-yyyy", null);
                        productTypesPriceByDateRequest.date = enteredDate.ToString("yyyy-MM-dd");
                        ProductTypesPriceByDateResponse productTypesPriceByDateResponse = new ProductTypesPriceByDateResponse();
                        productTypesPriceByDateResponse = lobjModel.GetProductTypesPriceByDate(productTypesPriceByDateRequest);
                        if (productTypesPriceByDateResponse != null && productTypesPriceByDateResponse.success == 1 && productTypesPriceByDateResponse.data != null)
                        {
                            productTypeItem.typePriceByDate = productTypesPriceByDateResponse.data;
                        }
                    }
                    ProductSelectedDetails productSelectedDetails = new ProductSelectedDetails();
                    productSelectedDetails.adultCount = Int32.Parse(adultCount);
                    productSelectedDetails.childrenCount = Int32.Parse(childrenCount);
                    productSelectedDetails.seniorsCount = Int32.Parse(seniorsCount);
                    productSelectedDetails.ptuuid = ptuuid;
                    productSelectedDetails.puuid = puuid;
                    productSelectedDetails.date = selectedDate;
                    productSelectedDetails.timeSlotUuid = timeslotuuid;
                    productInfoResponse.producttypedetails.ProductSelectedDetails = productSelectedDetails;
                    HttpContext.Current.Session["ProductInfo"] = productInfoResponse;
                    int totalPax = Convert.ToInt32(Int32.Parse(adultCount) + Int32.Parse(childrenCount) + Int32.Parse(seniorsCount));
                    List<List<ExperiencesPerPax>> experiencesPerPaxListOfList = new List<List<ExperiencesPerPax>>();
                    for (int i = 0; i < totalPax; i++)
                    {
                        List<ExperiencesPerPax> experiencesPerPaxList = new List<ExperiencesPerPax>();
                        experiencesPerPaxList.AddRange(productInfoResponse.producttypedetails.item_uuid.Find(x => x.uuid == ptuuid).typeinfo.options.perPax);
                        experiencesPerPaxListOfList.Add(experiencesPerPaxList);
                    };
                    PaymentDetailsModel paymentDetailsModel = new PaymentDetailsModel()
                    {
                        ProductInfoResponse = productInfoResponse,
                        BookingRequest = new BeMyGuest.Entities.BookingRequest()
                        {
                            options = new ExperiencesBookingRequestOptions()
                            {
                                perBooking = productInfoResponse.producttypedetails.item_uuid.Find(x => x.uuid == ptuuid).typeinfo.options.perBooking,
                                perPax = experiencesPerPaxListOfList
                            },
                            message=Convert.ToString(lintABCBankPoints)
                        }
                        
                    };
                    lstrProductPaymentDetails = JsonConvert.SerializeObject(paymentDetailsModel);
                }
                else
                {
                    lstrProductPaymentDetails = "ErrorPage.aspx";
                }
            }

        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("ExperienceProductBookingDetails.aspx GetPaymentDetails Exception: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
        }
        return lstrProductPaymentDetails ;
    }

    [WebMethod]
    public static string ProcessPayment(string currencycode, string totalAmount, string title, string firstName, string lastName, string emailId, string contactNo, BookingRequest pobjbookingRequest, string titleName, string Address, string DOB)
    {
        ABCModel lobjModel = new ABCModel();
        string redirectPGUrl = string.Empty;
        BookingRequest bookingRequest = new BeMyGuest.Entities.BookingRequest();

        try
        {

            ProductInfoResponse productInfoResponse = HttpContext.Current.Session["ProductInfo"] as ProductInfoResponse;
            if (productInfoResponse == null)
            {
                redirectPGUrl = "SESSION_TIME_OUT";
            }
            else
            {
                bookingRequest.productUuid = productInfoResponse.producttypedetails.ProductSelectedDetails.puuid;
                bookingRequest.productTypeUuid = productInfoResponse.producttypedetails.ProductSelectedDetails.ptuuid;
                bookingRequest.adults = productInfoResponse.producttypedetails.ProductSelectedDetails.adultCount;
                bookingRequest.children = productInfoResponse.producttypedetails.ProductSelectedDetails.childrenCount;
                bookingRequest.seniors = productInfoResponse.producttypedetails.ProductSelectedDetails.seniorsCount;
                // bookingRequest.timeSlotUuid = productInfoResponse.producttypedetails.ProductSelectedDetails.timeSlotUuid;
                bookingRequest.timeSlotUuid = string.IsNullOrEmpty(productInfoResponse.producttypedetails.ProductSelectedDetails.timeSlotUuid) ? null : productInfoResponse.producttypedetails.ProductSelectedDetails.timeSlotUuid;
                bookingRequest.arrivalDate = productInfoResponse.producttypedetails.ProductSelectedDetails.date;
                bookingRequest.totalAmount = Convert.ToInt32(totalAmount);
                bookingRequest.currencyCode = currencycode;
                bookingRequest.customer.salutation = title;
                bookingRequest.customer.firstName = firstName;
                bookingRequest.customer.lastName = lastName;
                bookingRequest.customer.email = emailId;
                bookingRequest.customer.phone = contactNo;
                //bookingRequest.memberId = cvMembershipNo;
                bookingRequest.options.perBooking = pobjbookingRequest.options.perBooking;
                bookingRequest.options.perPax = pobjbookingRequest.options.perPax;
                bookingRequest.titleName = titleName;

                ProgramDefinition lobjProgramDefinition = lobjModel.GetProgramMaster();
                MemberDetails lobjMemberDetails = HttpContext.Current.Session["MemberDetails"] as MemberDetails;

                if (lobjMemberDetails != null)
                {
                    if (bookingRequest != null)
                    {
                        bookingRequest.memberId = lobjMemberDetails.MemberRelationsList[0].RelationReference;
                        HttpContext.Current.Session["ExperienceBookingRequest"] = bookingRequest;
                        HttpContext.Current.Session["BookingFlag"] = "experience";
                        foreach (var perBookingItem in bookingRequest.options.perBooking)
                        {
                            if (perBookingItem.inputType.Equals(6))//date
                            {
                                perBookingItem.value = Convert.ToDateTime(perBookingItem.value).ToString("yyyy-MM-dd");
                            }
                            else if (perBookingItem.inputType.Equals(10))//time
                            {
                                perBookingItem.value = Convert.ToDateTime(perBookingItem.value).ToString("HH:mm");
                            }
                            else if (perBookingItem.inputType.Equals(11))//datetime
                            {
                                perBookingItem.value = Convert.ToDateTime(perBookingItem.value).ToString("yyyy-MM-dd HH:mm");
                            }
                        }

                        foreach (var perPaxItemList in bookingRequest.options.perPax)
                        {
                            foreach (var perPaxItem in perPaxItemList)
                            {
                                if (perPaxItem.inputType.Equals(6))//date
                                {
                                    perPaxItem.value = Convert.ToDateTime(perPaxItem.value).ToString("yyyy-MM-dd");
                                }
                                else if (perPaxItem.inputType.Equals(10))//time
                                {
                                    perPaxItem.value = Convert.ToDateTime(perPaxItem.value).ToString("HH:mm");
                                }
                                else if (perPaxItem.inputType.Equals(11))//datetime
                                {
                                    perPaxItem.value = Convert.ToDateTime(perPaxItem.value).ToString("yyyy-MM-dd HH:mm");
                                }
                            }
                        }

                        string lstrCurrency = lobjModel.GetDefaultCurrency();
                        List<ProgramCurrencyDefinition> lobjProgramCurrencyDefinition = lobjModel.GetProgramCurrencyDefinition(lobjProgramDefinition.ProgramId);
                        var PointRate = lobjProgramCurrencyDefinition[0].RedemptionRate;
                        List<RedemptionKeys> lobjRedemptionKeys = new List<RedemptionKeys>();
                        lobjRedemptionKeys = lobjModel.GetAllRedemptionKeys(lobjProgramDefinition.ProgramId);
                        int ThreshouldValue = 0;

                        ThreshouldValue = lobjRedemptionKeys.Find(lobj => lobj.RedemptionCode.Equals("EXPERIENCE") && lobj.Currency.Equals(lstrCurrency)).OTPThreshold;
                        int lintTotalPoints = Convert.ToInt32(totalAmount);
                        float lftAmount = Convert.ToSingle(totalAmount);

                        List<RedemptionDetails> lobjListOfRedemptionDetails = new List<RedemptionDetails>();
                        RedemptionDetails lobjRedemptionDetails = new RedemptionDetails();
                        lobjRedemptionDetails.Currency = lstrCurrency;
                        lobjRedemptionDetails.DisplayCurrency = lobjModel.CurrencyDisplayText(lstrCurrency);
                        lobjRedemptionDetails.Points = lintTotalPoints;
                        lobjRedemptionDetails.RelationReference = bookingRequest.memberId.ToString();
                        lobjRedemptionDetails.Amount = lftAmount;

                        lobjListOfRedemptionDetails.Add(lobjRedemptionDetails);
                        HttpContext.Current.Session["ExperienceRedemptionDetails"] = lobjListOfRedemptionDetails;
                        string lstrResponse = string.Empty;
                        double dblProductAmount = 0.0f;
                        HttpContext.Current.Session["BookingFlag"] = "experience";
                        dblProductAmount = Convert.ToInt32(totalAmount);

                        string FullName = bookingRequest.customer.firstName + " " + bookingRequest.customer.lastName;

                        if (ThreshouldValue <= lintTotalPoints && !ThreshouldValue.Equals(-1))
                        {
                            bool Status = false;
                            OTPDetails lobjOTPDetails = new OTPDetails();
                            lobjOTPDetails.UniquerefID = bookingRequest.memberId.ToString();
                            lobjOTPDetails.OtpEnumTypes = OTPEnumTypes.PACKAGEREVIEWNCONFIRM;
                            lobjOTPDetails.OtpType = Convert.ToString(OTPEnumTypes.PACKAGEREVIEWNCONFIRM);
                            HttpContext.Current.Session["OtpDetails"] = lobjOTPDetails as OTPDetails;

                            //  Status = lobjModel.SendOTPEmailAndSMS(bookingRequest.customer.email, bookingRequest.customer.phone, bookingRequest.memberId.ToString(), FullName, "redemption_otp", lobjOTPDetails, "Experiences");

                            Status = lobjModel.SendOTPEmailAndSMS(lobjMemberDetails, "redemption_otp", lobjOTPDetails, "Experience");

                            //Status = lobjModel.GenerateReviewnConfirmOTP(lobjOTPDetails, bookingRequest.customer.email, bookingRequest.memberId.ToString(), lobjProgramDefinition.ProgramId, FullName, bookingRequest.customer.phone);
                            if (Status)
                            {
                                lobjModel.LogActivity(string.Format(ActivityConstants.ReviewConfirmOTP, "Package", lobjRedemptionDetails.RelationReference, "Success"), ActivityType.ReviewConfirmOTPSuccess);
                                lstrResponse = "ValidateOTP.aspx?flag=Package";
                                redirectPGUrl = "ValidateOTP.aspx?flag=Package";
                            }
                            else
                            {
                                lobjModel.LogActivity(string.Format(ActivityConstants.ReviewConfirmOTP, "Package", lobjRedemptionDetails.RelationReference, "Failed"), ActivityType.ReviewConfirmOTPFailed);
                                lstrResponse = "BookingFailure.aspx";
                                redirectPGUrl = "BookingFailure.aspx";
                            }
                        }
                        else
                        {
                            lobjModel.LogActivity(string.Format(ActivityConstants.ReviewConfirmOTP, "Package", lobjRedemptionDetails.RelationReference, "Failed"), ActivityType.ReviewConfirmOTPFailed);
                            lstrResponse = "PointGateway.aspx";
                            redirectPGUrl = "PointGateway.aspx?flag=Package";
                        }
                        lobjModel.LogActivity(string.Format("ExperienceProductBookingDetails;  BookNow click; TotalAmount-:{0};Response-:{1};", totalAmount, lstrResponse), ActivityType.PackageBooking);

                    }
                }
                else
                {
                    lobjModel.LogActivity("MemberDetails Null : Failed", ActivityType.PackageBooking);
                    redirectPGUrl = "Login.aspx";
                }
            }

        }
        catch (Exception ex)
        {
            lobjModel.LogActivity(string.Format("Experience Booking {0}; Exception", bookingRequest.memberId.ToString()), ActivityType.PackageBooking);
            LoggingAdapter.WriteLog("ExperienceProductBookingDetails :" + ex.Message + ex.StackTrace);
            redirectPGUrl = "BookingFailure.aspx";
        }
        return redirectPGUrl;
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
        ABCModel lobjModel = new ABCModel();
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
}