using CB.IBE.Platform.Car.Entities;
using CB.IBE.Platform.Masters.Entities;
using ABC.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SourceControl_ChildPassangerDetails_Domestic : System.Web.UI.UserControl
{
    ABCModel lobjModel = new ABCModel();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            RefererDetails lobjRefererDetails = HttpContext.Current.Application["RefererSupplierDetails"] as RefererDetails;
            int supplierId = lobjRefererDetails.RefererSupplierProperties.SupplierId;

            //if (supplierId.Equals(6)) // provisio
            //{

            //}
            AdditionalInfo.Style.Add("display", "block");
            rfvGender.Enabled = true;

        }

    }
}