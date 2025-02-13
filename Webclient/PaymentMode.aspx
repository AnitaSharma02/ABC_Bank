<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PaymentMode.aspx.cs" Inherits="PaymentMode" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
    <title>Payment Mode</title>
    <link href="Css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="Css/root.css" rel="stylesheet" type="text/css" />
    <link href="Css/global.css" rel="stylesheet" type="text/css" />
    <style>
         html, body {
            height: 100%;
        }
        body {
            margin: 0;
            display: flex;
            flex-direction: column;
        }
        .dvPaymentMode {
            flex: 1;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="dvLogo bg-colour6 shadow-sm">
            <div class="container-xl">
                <div class="row">
                    <div class="col-12">
                        <div class="text-center p-3">
                            <a href="Index.aspx"><img src="../images/logos/infinity-logo.svg" width="150" /></a>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="dvPaymentMode container-xl pt-3 pt-lg-5">
            <div class="row justify-content-center align-items-center vh100">
                <div class="col-md-8">
                    <div class="bg-colour2 p-3">
                        <h2 class="h6 heading-semibold">Select Payment Mode</h2>
                    </div>
                    <div class="bg-colour2 p-3">
                        <div class="row justify-content-center align-items-center">
                            <div class="col-lg-4 mb-3 mb-lg-0">
                                <div class="dvRadios">
                                    <div class="dvLabel d-flex justify-content-between my-sm-auto">
                                        <label class="radio-container d-flex">
                                            <span class="d-inline-block">
                                                <input type="radio" name="radio" value="customRadio1" checked />
                                                <span class="radiomark"></span>
                                            </span>
                                            <span class="d-inline-block heading-light ms-3">
                                                <img width="100" src="../images/logos/mobank-logo.png" alt="mo bank" /></span>
                                        </label>
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-4 mb-3 mb-lg-0">
                                <div class="dvRadios">
                                    <div class="dvLabel d-flex justify-content-between my-sm-auto">
                                        <label class="radio-container d-flex">
                                            <span class="d-inline-block">
                                                <input type="radio" name="radio" value="customRadio2" />
                                                <span class="radiomark"></span>
                                            </span>
                                            <span class="d-inline-block heading-light ms-3">
                                                <img width="100" src="../images/logos/quickpay-logo.png" alt="quick pay" />
                                            </span>
                                        </label>
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-4 d-flex justify-content-center justify-content-lg-end">
                                <button class="btn btn-one me-3">Proceed</button>
                                <button class="btn btn-two">Cancel</button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="dvFooter border-top py-3">
            <div class="container-xl">            
                <div class="row align-items-lg-center">
                    <%--<div class="col-sm-12 col-lg-2 text-center text-lg-end mb-3 mb-lg-0">
                        <a href="Index.aspx">
                            <img class="img-fluid" width="125" src="images/logos/infinity-logo.svg" alt="Infinity Rewards Logo" /></a>
                    </div>--%>
                    <div class="col-sm-12 text-center">
                        <p class="h8 heading-semibold">
                            ©
                  <script>
                      document.write(new Date().getFullYear());
                  </script> Infinity Rewards. All rights reserved.
                        </p>
                    </div>
                    <%--<div class="col-sm-12 col-lg-5 text-center text-lg-end">
                        <a href="https://www.giift.com/" target="_blank">
                            <img class="img-fluid" width="70" src="images/logos/giift-logo.svg" alt="Giift Logo" />
                        </a>
                    </div>--%>
                </div>
            </div>
            </div>
    </form>
</body>
</html>
