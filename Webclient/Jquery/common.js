$(document).ready(function () {
    var pathname = window.location.href.substr(window.location.href.lastIndexOf("/") + 1);
    $('#navbarSupportedContent li a').removeClass("active");

    if (pathname == "Index.aspx" || pathname == " ") {
        $('#home').addClass("active");
        return false;
    }

    else if (pathname == "StatementSummary.aspx" || pathname == "TransactionSummary.aspx" || pathname == "PointsExpiry.aspx" || pathname == "ManageBooking.aspx" || pathname == "MyOrders.aspx" || pathname == "ViewMemberProfile.aspx" || pathname == "OrderHistory.aspx" || pathname.includes("OrderDetails.aspx")) {
        $('#navbarDropdown').addClass("active");
        return false;
    }

    else if (pathname == "AboutUs.aspx") {
        $('#about').addClass("active");
        return false;
    }
    else if (pathname == "Redeem.aspx") {
        $('#redeem').addClass("active");
        return false;
    }
    else if (pathname == "Earn.aspx") {
        $('#earn').addClass("active");
        return false;
    }
    else if (pathname == "Login.aspx") {
        $('#lnkLogin').addClass("active");
        return false;
    }
    else if (pathname == "Activation.aspx") {
        $('#activation').addClass("active");
        return false;
    }
});
function CommaSep(nStr) {
    nStr += '';
    x = nStr.split('.');
    x1 = x[0];
    x2 = x.length > 1 ? '.' + x[1] : '';
    var rgx = /(\d+)(\d{3})/;
    while (rgx.test(x1)) {
        x1 = x1.replace(rgx, '$1' + ',' + '$2');
    }
    return x1 + x2;
}
function redirectLocation(url) {
    window.location.href = url;
    return false;
};

function toggleDiv(actId, NactId1, NactId2, NactId3) {

    //var effect = 'slide';

    //// Set the options for the effect type chosen
    //var options = { direction: $('.mySelect').val() };

    // Set the duration (default: 400 milliseconds)
    //var duration = 500;
    //var windowWidth = $(window).width();
    //if (windowWidth > 768) {
    //    $("#div" + actId).toggle(effect, options, duration);
    //}
    //if (windowWidth < 768) {
    //    $("#div" + actId).toggle();
    //}
    //$("#div" + pannelid).next().animate({ width: 'toggle' }, 500);
    if ($(window).width() >= 768) {
        $("#div" + actId).slideToggle('slow');
    }
    else {
        $("#div" + actId).toggle();
    }
    $("#div" + NactId1).hide();
    $("#div" + NactId2).hide();
    $("#div" + NactId3).hide();



    $(".icon" + actId).toggleClass("menuSelect");
    //$("#ico" + actId).removeClass("ico" + actId);

    $(".icon" + NactId1).removeClass("menuSelect");
    $(".icon" + NactId2).removeClass("menuSelect");
    $(".icon" + NactId3).removeClass("menuSelect");
    return false;
}

/*footer usefull links*/
$(document).ready(function () {
    var acc = document.getElementsByClassName("moreLinks");
    var i;

    for (i = 0; i < acc.length; i++) {
        acc[i].addEventListener("click", function () {
            this.classList.toggle("activeLink");
            var panel = this.nextElementSibling;
            if (panel.style.maxHeight) {
                panel.style.maxHeight = null;
            } else {
                panel.style.maxHeight = panel.scrollHeight + "px";
            }
        });
    }
});
function fnActivateTimer(CountDownTimer, divCounter, ctrlResendLink) {
    var Timer = CountDownTimer;
    var interval = setInterval(function () {
        if (Timer != "0:00") {
            $('#' + ctrlResendLink).hide();
            var timer2 = Timer;
            var timer = timer2.split(':');
            //by parsing integer, I avoid all extra string processing
            var minutes = parseInt(timer[0], 10);
            var seconds = parseInt(timer[1], 10);
            --seconds;
            minutes = (seconds < 0) ? --minutes : minutes;
            if (minutes < 0) clearInterval(interval);
            seconds = (seconds < 0) ? 59 : seconds;
            seconds = (seconds < 10) ? '0' + seconds : seconds;
            $('#' + divCounter).html('Resend OTP in ' + minutes + ':' + seconds + ' minute(s)');
            Timer = minutes + ':' + seconds;
        }
        else {
            $('#' + divCounter).html('');
            $('#' + ctrlResendLink).show();
            clearInterval(interval);
        }

    }, 1000);
}