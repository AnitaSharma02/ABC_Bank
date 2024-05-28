using System;
using System.Web.UI;

public partial class HotelSearch : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["CategoryName"] = "hotel";
    }
}