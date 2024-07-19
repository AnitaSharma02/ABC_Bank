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
        #updProgress {
          opacity:1;
          padding: 5px 0;
          background-color: rgba(255, 255, 255, 1);
        }
    </style>
</head>
<body>
    <form id="form1" runat="server"></form>
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
    <script>
        //this script is for header onscroll animation
        const container = document.querySelector('.header .container');
        //const header = document.querySelector('.header');
        let isScrolled = false;
        function handleScroll() {
            if (window.innerWidth >= 992) {
                if (window.scrollY >= 100 && !isScrolled) {
                    container.classList.add('scrolled');
                    isScrolled = true;
                } else if (window.scrollY < 100 && isScrolled) {
                    container.classList.remove('scrolled');
                    isScrolled = false;
                }
            } else {
                // Remove the 'scrolled' class if the viewport width is less than 992px
                container.classList.remove('scrolled');
                isScrolled = false;
            }
        }
        handleScroll();
        document.addEventListener('scroll', handleScroll);
        window.addEventListener('resize', handleScroll);
    </script>
</body>
</html>
