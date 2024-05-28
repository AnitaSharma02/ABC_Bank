<%@ Page Title="Subscription Successful" Language="C#" AutoEventWireup="true" CodeFile="SubscriptionSuccessful.aspx.cs" Inherits="SubscriptionSuccessful" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Welcome to points!</title>
        <meta name="viewport" content="width=device-width" />

        <link href="Css/main.css" rel="stylesheet" type="text/css" />
    <link href="Css/responsive.css" rel="stylesheet" />

       <style>
        .insidepgs {
    margin: 30px auto;
    min-height: 200px;
}
           .statHead {background-color: #2a6ebb; height:132px; }
           .statFoot {background: #2a6ebb url("Images/stat-qib-footer-bg.jpg") repeat scroll 0 0; height:165px; }

    </style>
</head>


<body>
    <form id="form1" runat="server">
        <div class="statHead">
            <img src="Images/stat-qib-header.jpg" />
        </div>
        <div class="wrap insidepgs">
        <h1>Subscription</h1>
        <hr />
        <br />
        <div>
            <br />
            <p style="text-align:center;">Your account has been activated. Now you can access your NPoints by logging to <a href="https://absher.qib.com.qa"> https://absher.qib.com.qa</a></p>
            <br />
            
        </div>
    </div>
        <div class="statFoot">
            <img src="Images/stat-qib-footer.jpg" />
        </div>
    </form>
</body>
</html>
