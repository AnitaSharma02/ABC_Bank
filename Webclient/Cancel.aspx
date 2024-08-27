<%@ Page Title="" Language="C#" MasterPageFile="SiteMaster.master" AutoEventWireup="true" CodeFile="Cancel.aspx.cs" Inherits="Cancel" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CP" runat="Server">
    <style>
    div#sitemap{display:none}
    .myAccount{padding-top:0}
    .innerHeader{height:auto;padding:0 70px;background:#00425f!important;box-shadow:rgb(0 0 0 / 25%) 0 5px 15px;height:auto!important}
    div#paymentpage{background:#eeeff1;margin:100px auto 50px auto;width:550px;padding:20px;border:1px solid #e2e2e2}
    .amt{padding:20px}
    .Paymentloading p{margin-top:15px;font-size:15px;font-weight:500;color:#320d6d;text-align:center;width:100%!important}
</style>
 <div id="paymentpage">
    <div class="pay-options">
        <div class="total-amt text-center">
            <div class="amt bg-colour6 border">
                <form id="form1">
                    <div>
                        <p class="text mb-2 heading-bold"> Your Transaction is Cancelled</p>
                    </div>
                </form>
            </div>
        </div>
        <div class="">
            <a href="Index.aspx" class="btn blue_button w-100 mt-4"> Back to Home</a>
        </div>
    </div>
</div>
    
 
</asp:Content>