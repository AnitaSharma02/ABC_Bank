using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class NoResultFound : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["ERR"] != null && Request.QueryString["ERR"].ToString() == "BOOKINGFAILED")
        {
            divBOOKINGFAILED.Style.Add("display", "block");
            divHotelNoresult.Style.Add("display", "none");
            divRESULTNOTFOUND.Style.Add("display", "none");
        }
        else if (Request.QueryString["ERR"] != null && Request.QueryString["ERR"].ToString() == "RESULTNOTFOUND")
        {
            divBOOKINGFAILED.Style.Add("display", "none");
            divHotelNoresult.Style.Add("display", "none");
            divRESULTNOTFOUND.Style.Add("display", "block");
            if (Session["MemberDetails"] == null)
            {
            }
        }
        else
        {
            divBOOKINGFAILED.Style.Add("display", "none");
            divRESULTNOTFOUND.Style.Add("display", "none");
            divHotelNoresult.Style.Add("display", "block");
        }
    }
}