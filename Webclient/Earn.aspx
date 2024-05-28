<%@ Page Title="Earn" Language="C#" MasterPageFile="~/SiteMaster.master" AutoEventWireup="true" CodeFile="earn.aspx.cs" Inherits="earn" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CP" runat="Server">
    <style>
        .innerHeader {
            height: auto;
            padding: 0 70px;
            background: transparent !important;
            box-shadow: unset;
            height: auto !important;
        }

        .innerText {
            margin: 0 40px;
        }

        .website-text {
            text-align: center;
            font: normal normal normal 16px/28px Averta;
            letter-spacing: 0px;
            color: #fff;
        }

        .services {
            list-style: none;
            padding: 0px;
            margin: 0px 0;
        }

        @media screen and (max-width:800px) {
            .earnBox {
                background: #fff;
                margin: 100px 0 10px 0;
            }

            .services {
                list-style: none;
                padding: 0px;
                margin: 0px 0;
            }
        }
    </style>
    <section>
        <div class="bg-banner-earn">
        </div>
        <div class="breadcrumbBox d-none">
            <div class="flightPage">
                <div class="breadcrumbFlight">
                    <ul class="d-flex">
                        <li><a href="\">
                            <img class="pr-3" src="../images/arrow-left.svg"></a></</li>
                        <li><a href="\" class="custom-text">Home</a></li>
                        <li class="px-2">/</li>
                        <li class="brd-bold">Earn</li>
                    </ul>
                </div>
            </div>
        </div>
        <div class="flightList">
            <div class="container">
                <div class="earnBox">
                    <div class="innerQue" id="innerPageTerms">
                        <div class="innerBx">
                            <div class="privacyContent">
                                <div class="priText" id="innerPageearn" dir="ltr">
                                    <h4 class="blue">Earning NIC Express Reward NPoints</h4>
                                    <p class="pt-4 px-md-5 d-none">Experience the good life while effortlessly earning Verve Rewards! It's as easy as using your Verve Card for online and offline payments, at POS terminals, on the web, and even at ATMs. The more transactions you do, the more rewards you'll earn. Embrace a lifestyle of convenience and reap the benefits of Verve Rewards along the way!</p>
                                    <div class="py-5">
                                        <h4><span>
                                            <img class="mr-3" style="width: 50px; aspect-ratio: 1/1;"
                                                src="../images/Name.png"></span>Physical Token</h4>
                                        <div class="ovr-scr">
                                            <table class="GenTable my-4">
                                                <thead>
                                                    <tr>
                                                        <th class="bg-blu">Product Name</th>
                                                        <th class="bg-blu">ATM </th>
                                                        <th class="bg-blu">POS</th>
                                                        <th class="bg-blu">Web </th>
                                                    </tr>
                                                </thead>
                                                <tbody>

                                                    <tr>
                                                        <td>Verve Debit Card</td>
                                                        <td>Every 2 Transactions on ATM = 1 point </td>
                                                        <td>Every 2 Transactions on POS = 1 point </td>
                                                        <td>Every 2 Transactions on Web = 1 point </td>
                                                    </tr>

                                                </tbody>
                                            </table>
                                        </div>
                                        <p class="text-left p-0">Your minimum monthly spend on verve cards must be a minimum of NGN 500 to start earning NPoints.</p>

                                        <h4 class="d-none"><span>
                                            <img class="mr-3" style="width: 50px; aspect-ratio: 1/1;"
                                                src="../images/Name.png"></span>Online Options</h4>
                                        <div class="ovr-scr d-none">
                                            <table class="GenTable my-4">
                                                <thead>
                                                    <tr>
                                                        <th class="bg-blu">Product Name</th>
                                                        <th class="bg-blu">ATM </th>
                                                        <th class="bg-blu">POS</th>
                                                        <th class="bg-blu">Web </th>
                                                    </tr>
                                                </thead>
                                                <tbody>

                                                    <tr>
                                                        <td>e-Cash </td>
                                                        <td>500 NGN Trnx Value = 1 Point</td>
                                                        <td>500 NGN Trnx Value = 1 Point</td>
                                                        <td>500 NGN Trnx Value = 1 Point</td>
                                                    </tr>

                                                    <tr>
                                                        <td>Paycode </td>
                                                        <td>500 NGN Trnx Value = 1 Point</td>
                                                        <td>500 NGN Trnx Value = 1 Point</td>
                                                        <td>500 NGN Trnx Value = 1 Point</td>
                                                    </tr>


                                                </tbody>
                                            </table>
                                        </div>


                                        <div class="bg-green p-5 mt-5">
                                            <h4 class="blue text-white p-0">Track Your NIC Express Reward NPoints Effortlessly!</h4>
                                            <div class="py-3">
                                                <p class="text-white p-0">
                                                    Your monthly e-statement will be useful, and<br />
                                                    you can also stay up-to-date on your earned NPoints by visiting the NIC Express Reward website.
                                                </p>
                                                <%--  <p class="text-white p-0">  Use the <a class="website-text" href="https://www.myverveworld.com/">NIC Express Reward website</a>, Online/Mobile banking, Contact Centre, or <br/> monthly e-statement to stay updated
                                on your earned points.</p>--%>
                                            </div>
                                            <div class="d-flex justify-content-center travelBtn">
                                                <a href="StatementSummary.aspx" class="hvr-sweep-to-right">Learn More</a>
                                            </div>
                                        </div>
                                    </div>

                                    <div style="display: none">
                                        <h4>Activation and Viewing NIC Express Reward NPoints through XXXX Mobile Banking App</h4>
                                        <p>Customers can Activate their NIC Express Reward account by logging in XXXX Mobile App as per below:</p>
                                        <ul class="innerText">
                                            <li>Customer logs into XXXX Mobile App.</li>
                                            <li>The NIC Express Reward point balance will be seen on the first page.</li>
                                            <li>If the customer did not yet activate the NIC Express Reward account, an option to “Activate” will be available</li>
                                            <li>On clicking “Activate” a new screen opens which shows
                                        <ul class="innerText">
                                            <li>The customer’s registered mobile number and email ID with XXXX (non-editable field)</li>
                                            <li>A new password has to be created and then click “Submit” button.</li>
                                        </ul>
                                            </li>
                                            <li>An OTP is generated</li>
                                            <li>Customer has to key in the OTP and the NIC Express Reward account immediately gets activated.</li>
                                        </ul>
                                    </div>
                                </div>

                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="bg-greenTexture py-5">
                <div class="container">
                    <h4 class="text-white text-center texture-head">Activate your Verve Rewards Account:</h4>
                    <div class="parent-box d-flex align-items-center justify-content-center flex-wrap">
                        <div class="align-items-center child-box d-flex flex-column p-4">
                            <div class="align-items-center d-flex justify-content-center red-circle">1</div>
                            <p class="align-items-center box-txt d-flex justify-content-center pt-4">Launch the Verve Rewards Portal on your browser</p>
                        </div>
                        <div class="align-items-center child-box d-flex flex-column p-4">
                            <div class="align-items-center d-flex justify-content-center red-circle">2</div>
                            <p class="align-items-center box-txt d-flex justify-content-center pt-4">Log in to the Verve Rewards Portal</p>
                        </div>
                        <div class="align-items-center child-box d-flex flex-column p-4">
                            <div class="align-items-center d-flex justify-content-center red-circle">3</div>
                            <p class="align-items-center box-txt d-flex justify-content-center pt-4">Click on "Activate Membership"</p>
                        </div>
                        <div class="align-items-center child-box d-flex flex-column p-4">
                            <div class="align-items-center d-flex justify-content-center red-circle">4</div>
                            <p class="align-items-center box-txt d-flex justify-content-center pt-4">Enter your Verve ID</p>
                        </div>
                        <div class="align-items-center child-box d-flex flex-column p-4">
                            <div class="align-items-center d-flex justify-content-center red-circle">5</div>
                            <p class="align-items-center box-txt d-flex justify-content-center pt-4">Agree to all the Terms & Conditions</p>
                        </div>
                        <div class="align-items-center child-box d-flex flex-column p-4">
                            <div class="align-items-center d-flex justify-content-center red-circle">6</div>
                            <p class="align-items-center box-txt d-flex justify-content-center pt-4">Enter the OTP</p>
                        </div>
                        <div class="align-items-center child-box d-flex flex-column p-4">
                            <div class="align-items-center d-flex justify-content-center red-circle">7</div>
                            <p class="align-items-center box-txt d-flex justify-content-center pt-4">Create a new password</p>
                        </div>
                        <div class="align-items-center child-box d-flex flex-column p-4">
                            <div class="align-items-center d-flex justify-content-center red-circle">8</div>
                            <p class="align-items-center box-txt d-flex justify-content-center pt-4">Enter the Captcha/security code</p>
                        </div>
                        <div class="align-items-center child-box d-flex flex-column p-4">
                            <div class="align-items-center d-flex justify-content-center red-circle">9</div>
                            <p class="align-items-center box-txt d-flex justify-content-center pt-4">Click "Continue" </p>
                        </div>

                    </div>
                    <div class="py-5">
                        <h3 class="align-items-center cong-text d-flex justify-content-center text-center pt-5">
                            <img class="mr-3" style="width: 40px;" src="Images/Tick-Mark.png">Congratulations!</h3>
                        <h5 class="cong-para text-center pb-2">Your Verve Rewards Account is Active now!</h5>
                    </div>



                    <p class="text-white text-center last-text pb-5">
                        <strong class="pr-2" style="font-weight: 800;">Note:</strong>In case your email address is not registered with Verve Rewards, Verve Rewards activation will not be possible.<br />

                        You should update your email ID by visiting nearest partner bank before proceeding to Verve Rewards activation.
                    </p>
                </div>


            </div>
        </div>
    </section>

</asp:Content>



