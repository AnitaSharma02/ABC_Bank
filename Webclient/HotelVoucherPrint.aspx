<%@ Page Title="Hotel Voucher Print" Language="C#" AutoEventWireup="true" CodeFile="HotelVoucherPrint.aspx.cs"
    Inherits="HotelVoucherPrint" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no" />
    <title>Hotel Voucher</title>
    <link href="Css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="Css/root.css" rel="stylesheet" type="text/css" />
    <link href="Css/global.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">
        window.print();
    </script>
</head>
<body>
<form id="form1" runat="server">
    <div class="container-lg my-3">
        <div class="row">
            <div class="col-12">
                <div class="border p-3">
                    <div class="row">
                        <div class="col-12">
                            <div class="bg-colour3 p-3">
                                <img src="Images/logos/nic-logo.svg" alt="" />
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-12">
                            <h2 class="h5 heading-light text-colour7 my-3">Thank you <asp:Label CssClass="font-weight-bold" ID="lblPersonName" runat="server" Text="Nil"></asp:Label>, Your booking is now <span class="font-weight-bold">confirmed.</span></h2>
                            <p>                                
                                
                                <span class="font-weight-bold">NPoints:</span>
                                <span id="divTotalMiles" runat="server"></span><br />

                                <span class="font-weight-bold">Reference No:</span>
                                <asp:Label ID="lblTransactionReference" runat="server"></asp:Label><br />                                

                                <span class="font-weight-bold">Voucher No:</span>
                                <asp:Label ID="lblExternalRefId" runat="server" Text=""></asp:Label><br />

                                <span class="font-weight-bold">Booking ID:</span>
                                <asp:Label ID="lblBookingID" runat="server" Text="Nil"></asp:Label>
                            </p>
                        </div>
                    </div>
                    <div class="row mt-3">
                        <div class="col-sm-6">
                            <div class="border p-3 h-100">
                                <div class="row">
                                    <div class="col-12">
                                        <h2 class="h6 heading-semibold bg-colour3 text-colour7 text-capitalize p-3">Booking Details</h2>
                                    </div>
                                </div>
                                <div class="row mt-2">
                                    <div class="col-6 d-flex justify-content-between">
                                        <span class="h6 heading-semibold text-colour7">Your Reservation</span> <span>:</span>
                                    </div>
                                    <div class="col-6">
                                        <asp:Label ID="lblTotalRoom" runat="server" Text="Nil"></asp:Label>
                                        <asp:Label ID="lblTotalnight" runat="server" Text="Nil"></asp:Label>
                                        <%--<span>1 room(s),</span>--%>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-6 d-flex justify-content-between">
                                        <span class="h6 heading-semibold text-colour7">Check-in</span> <span>:</span>
                                    </div>
                                    <div class="col-6">
                                        <asp:Label ID="lblCheckinDate" runat="server" Text="Nil"></asp:Label>
                                        <%--<span>1 room(s),</span>--%>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-6 d-flex justify-content-between">
                                        <span class="h6 heading-semibold text-colour7">Check-out</span> <span>:</span>
                                    </div>
                                    <div class="col-6">
                                        <asp:Label ID="lblCheckoutDate" runat="server" Text="Nil"></asp:Label>
                                        <%--<span>1 room(s),</span>--%>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-6 d-flex justify-content-between">
                                        <span class="h6 heading-semibold text-colour7">Booked By</span> <span>:</span>
                                    </div>
                                    <div class="col-6">
                                        <asp:Label ID="lblBookPersonName" runat="server" Text="Nil"></asp:Label>
                                        <%--<span>1 room(s),</span>--%>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-6 d-flex justify-content-between">
                                        <span class="h6 heading-semibold text-colour7">Email</span> <span>:</span>
                                    </div>
                                    <div class="col-6">
                                        <asp:Label ID="lblPersonEmail" CssClass="text-break" runat="server" Text="Nil"></asp:Label>
                                        <%--<span>1 room(s),</span>--%>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-6 d-flex justify-content-between">
                                        <span class="h6 heading-semibold text-colour7">Address</span> <span>:</span>
                                    </div>
                                    <div class="col-6">
                                        <asp:Label ID="lblPersonAddress" runat="server" Text="Nil"></asp:Label>
                                        <%--<span>1 room(s),</span>--%>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-6">
                            <div class="border p-3 h-100">
                                <div class="row">
                                    <div class="col-12">
                                        <h2 class="h6 heading-semibold bg-colour3 text-colour7 text-capitalize p-3">Hotel Details</h2>
                                    </div>
                                </div>
                                <div class="row mt-2">
                                    <div class="col-6 d-flex justify-content-between">
                                        <span class="h6 heading-semibold text-colour7">Hotel Name</span> <span>:</span>
                                    </div>
                                    <div class="col-6">
                                        <asp:Label ID="lblHotelName" runat="server" Text="Nil"></asp:Label>
                                        <%--<span>1 room(s),</span>--%>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-6 d-flex justify-content-between">
                                        <span class="h6 heading-semibold text-colour7">Address</span> <span>:</span>
                                    </div>
                                    <div class="col-6">
                                        <asp:Label ID="lblAddress" runat="server" Text="Nil"></asp:Label>
                                        <%--<span>1 room(s),</span>--%>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-6 d-flex justify-content-between">
                                        <span class="h6 heading-semibold text-colour7">Contact</span> <span>:</span>
                                    </div>
                                    <div class="col-6">
                                        <asp:Label ID="lblHotelPhone" runat="server" Text="Nil"></asp:Label>
                                        <%--<span>1 room(s),</span>--%>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-6 d-flex justify-content-between">
                                        <span class="h6 heading-semibold text-colour7">Fax</span> <span>:</span>
                                    </div>
                                    <div class="col-6">
                                        <asp:Label ID="lblHotelFax" runat="server" Text="Nil"></asp:Label>
                                        <%--<span>1 room(s),</span>--%>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-6 d-flex justify-content-between">
                                        <span class="h6 heading-semibold text-colour7">Website</span> <span>:</span>
                                    </div>
                                    <div class="col-6">
                                        <asp:Label ID="lblWebsite" CssClass="text-break" runat="server" Text="Nil"></asp:Label>
                                        <%--<span>1 room(s),</span>--%>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-6 d-flex justify-content-between">
                                        <span class="h6 heading-semibold text-colour7">Rating</span> <span>:</span>
                                    </div>
                                    <div class="col-6">
                                        <asp:Label ID="lblRating" runat="server" Text="Nil"></asp:Label>
                                        <%--<span>1 room(s),</span>--%>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row mt-3">
                        <div class="col-sm-6">
                            <div class="row">
                                <div class="col-12">
                                    <div class="row">
                                        <div class="col-6 d-flex justify-content-between">
                                            <span class="h6 heading-semibold text-colour7">Payment Type</span> <span>:</span>
                                        </div>
                                        <div class="col-6">
                                            <span id="divPaymentDetails" runat="server"></span>
                                            <%--<span>1 room(s),</span>--%>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-12">
                                    <div class="row">
                                        <div class="col-6 d-flex justify-content-between">
                                            <span class="h6 heading-semibold text-colour7">Please Note</span> <span>:</span>
                                        </div>
                                        <div class="col-6">
                                            <span>Additional Supplements (e.g. extra bed) are not added to this total.</span>
                                            <%--<span>1 room(s),</span>--%>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-6">
                            <div class="bg-colour3 p-3">
                                <div class="row">
                                    <div class="col-12">
                                    <div class="row">
                                        <div class="col-6 d-flex justify-content-between">
                                            <span class="h6 heading-semibold text-colour7">Room Details</span> <span>:</span>
                                        </div>
                                        <div class="col-6">
                                            <asp:Label ID="lblRoomtype" runat="server" Text="Nil"></asp:Label>
                                            <%--<span>1 room(s),</span>--%>
                                        </div>
                                    </div>                            
                                </div>
                                    <div class="col-12">
                                    <div class="row">
                                        <div class="col-6 d-flex justify-content-between">
                                            <span class="h6 heading-semibold text-colour7">Special Request</span> <span>:</span>
                                        </div>
                                        <div class="col-6">
                                            <asp:Label ID="lblSpecialRequest" runat="server" Text="Nil"></asp:Label>
                                            <%--<span>1 room(s),</span>--%>
                                        </div>
                                    </div>                            
                                </div>
                                </div>
                            </div>
                        </div>
                        <div class="offset-10 col-2 my-2 d-none">
                            <div class="img-container">
                                <asp:Image ID="imgMap" runat="server" />
                            </div>
                        </div>                        
                    </div>
                </div>
            </div>
        </div>
    </div>
</form>

</body>
</html>
