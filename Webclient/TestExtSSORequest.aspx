<%@ Page Title="Test Ext SSO Request" Language="C#" AutoEventWireup="true" CodeFile="TestExtSSORequest.aspx.cs" Inherits="TestExtSSORequest" %>

<!DOCTYPE html>
<html lang="en" dir="ltr">
<head id="Head1" runat="server">
    <meta charset="UTF-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">
    <title>Welcome to Infinity Rewards</title>
    <link rel="shortcut icon" type="image/x-icon" href="\Images/logos/favicon.ico" />
    <meta name="viewport" content="width=device-width" />
    <link rel="stylesheet" href="\Css/root.css" />
    <link rel="stylesheet" href="\Css/global.css" /> 
    <style>
        #updProgress .image{
            display:flex;
            flex-direction:column;
            align-items:center;
            width:100px;
            position:fixed;
            top:50%;
            left:50%;
            transform:translate(-50%,-50%);
        }
        #updProgress .image img{
            position:static !important;
            top:auto !important;
            left:auto !important;
            transform:none !important;
            width:48px;
            margin-bottom:.75rem;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server"></form>

   <div id="updProgress" runat="server" visible="true">
       <div class="image">
           <img src="../Images/loading.gif" alt="loading..." />
            <h2 class="heading6">Please Wait</h2>
       </div>
    </div>
</body>
</html>
