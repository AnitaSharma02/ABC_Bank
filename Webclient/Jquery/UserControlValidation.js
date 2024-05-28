$(document).ready(function () {
    $('#ct_QNB_rptAdultControl_adultdetails_0_txtDOB_0').bind("paste", function (e) {
        e.preventDefault();
    });

    $('#ct_QNB_rptChildControl_childdetails_0_txtDOB_0').bind("paste", function (e) {
        e.preventDefault();
    });

    $('#ct_QNB_rptInfantControl_infantdetails_0_txtDOB_0').bind("paste", function (e) {
        e.preventDefault();
    });

    $('#ct_QNB_rptAdultControl_adultdetails_0_txtFirstName_0').bind("paste", function (e) {
        e.preventDefault();
    });

    $('#ct_QNB_rptChildControl_childdetails_0_txtFirstName_0').bind("paste", function (e) {
        e.preventDefault();
    });

    $('#ct_QNB_rptInfantControl_infantdetails_0_txtFirstName_0').bind("paste", function (e) {
        e.preventDefault();
    });

    $('#ct_QNB_rptAdultControl_adultdetails_0_txtLastName_0').bind("paste", function (e) {
        e.preventDefault();
    });

    $('#ct_QNB_rptChildControl_childdetails_0_txtLastName_0').bind("paste", function (e) {
        e.preventDefault();
    });

    $('#ct_QNB_rptInfantControl_infantdetails_0_txtLastName_0').bind("paste", function (e) {
        e.preventDefault();
    });
});

function LettersOnlyValidation(evt) {
    var regex = new RegExp("^[a-zA-Z\b]+$");
    var key = String.fromCharCode(!event.charCode ? event.which : event.charCode);
    if (!regex.test(key)) {
        event.preventDefault();
        return false;
    }
}

function DateOnlyValidation(evt) {
    var regex = new RegExp("^[]+$");
    var key = String.fromCharCode(!event.charCode ? event.which : event.charCode);
    if (!regex.test(key)) {
        event.preventDefault();
        return false;
    }
}