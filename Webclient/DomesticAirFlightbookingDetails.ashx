<%@ WebHandler Language="C#" Class="DomesticAirFlightbookingDetails" %>

using System;
using System.Web;
using CB.IBE.DomesticFlight.Entities;
using ABC.Model;
using System.Configuration;
public class DomesticAirFlightbookingDetails : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        ABCModel lobjModel = new ABCModel();
        TicketDownloadRequest lobjTicketDownloadRequest = new TicketDownloadRequest();
        lobjTicketDownloadRequest.LogId =Convert.ToInt32(context.Request.QueryString["LogId"]);// Convert.ToInt32(LogId);
        lobjTicketDownloadRequest.Token = ConfigurationManager.AppSettings["KhaltiIBEDomesticFlightToken"].ToString();
        lobjTicketDownloadRequest.isBase64 = true;
        TicketDownloadResponse lobjTicketDownloadResponse = lobjModel.TicketDownload(lobjTicketDownloadRequest);
        if (lobjTicketDownloadResponse != null)
        {
            if (lobjTicketDownloadResponse.Status && !string.IsNullOrEmpty(lobjTicketDownloadResponse.Data))
            {
                string base64String = lobjTicketDownloadResponse.Data;
                context.Response.Clear();
                context.Response.AddHeader("Content-Type", "application/pdf");
                context.Response.AddHeader("Content-Disposition", "inline;");
                context.Response.AddHeader("Cache-Control", "private, max-age=0, must-revalidate");
                context.Response.AddHeader("Pragma", "public");
                context.Response.BinaryWrite(Convert.FromBase64String(base64String));
                context.Response.Flush();
                context.Response.Close();

            }
        }
        else
        {
                context.Response.Clear();
                context.Response.AddHeader("Content-Type", "application/pdf");
                context.Response.AddHeader("Content-Disposition", "inline;");
                context.Response.AddHeader("Cache-Control", "private, max-age=0, must-revalidate");
                context.Response.AddHeader("Pragma", "public");
                context.Response.Write("Error while gererating PDF.");
                context.Response.Flush();
                context.Response.Close();
        }
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}