<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ExtSSO.aspx.cs" Inherits="ExtSSO" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="shortcut icon" type="image/x-icon" href="\Images/logos/favicon.ico" />
    <meta name="viewport" content="width=device-width" />
     <link rel="stylesheet" href="\Css/root.css" />
     <link rel="stylesheet" href="\Css/global.css" /> 
     <style>
         #updProgress {
           opacity:1;
           padding: 5px 0;
           background-color: rgba(255, 255, 255, 1);
         }
     </style>
</head>
<body>
    
    <form id="form1" runat="server" class="mainBody">
        <div id="divErrorMsg" runat="server" style="display: none;text-align:center;padding:50px 0">
            <strong>
                <asp:Label ID="lblMessage" CssClass="d-block p-5 text-center" runat="server" Text="Label"></asp:Label>
            </strong>
        </div>
    </form>
   <div id="updProgress" runat="server" visible="true" style="z-index: 0; height: 100%;text-align:center;">
     <img src="../Images/loading.gif" alt="loading..." />
    <div class="container-fluid">
        <div class="row">
             <div class="col-12">
                 <h2 class="h6 heading-semibold text-colour7">Please Wait...</h2>
             </div>
        </div>
    </div>
 </div>
</body>
</html>
