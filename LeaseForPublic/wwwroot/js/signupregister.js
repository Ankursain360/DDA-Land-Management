﻿$("#refreshcaptcha").click(function () {
    resetCaptchaImage();
});

function resetCaptchaImage() {
    d = new Date();
    $("#img-captcha").attr("src", "/get-captcha-image?" + d.getTime());
}





$('#email').keyup(function () {
    $("#err-email").hide();
    var email = $('#email').val();

    var testEmail = /^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/;
    if (!testEmail.test(email)) {
        $("#err-email").show();
        return;
    } else {
        $("#err-email").hide();
    }

});


$('#mobile').keyup(function () {

    var mob = $('#mobile').val();
    if (!mob.match('[0-9]{10}') || mob.length > 10) {
        $("#err-mob").show();
        return;
    } else {
        $("#err-mob").hide();
    }

});

$('#name').keydown(function (e) {

    $("#err-name").hide();
    if (e.ctrlKey || e.altKey) {
        e.preventDefault();
    } else {
        var key = e.keyCode;

        if (!((key == 8) || (key == 32) || (key == 46) || (key >= 35 && key <= 40) || (key >= 65 && key <= 90))) {
            e.preventDefault();
        }
    }
});
$("#signup2").click(function () {

    $("#err-comm").hide();

    var name = $("#name").val();
    var mobile = $("#mobile").val();
    var email = $("#email").val();
    var code = $("#CaptchaCode").val();
    var status = 1;
    if (name == "") {
        $("#err-name").show();
        status = 2;
    }
    if (mobile == "") {
        $("#err-mob").show();
        status = 2;
    }
    if (email == "") {
        $("#err-email").show();
        status = 2;
    }
    if (code == "") {
        $("#err-captcha").show();
        status = 2;
    }
    else {
        $("#err-captcha").hide();
    }

    if (status == 2) {
        $("#loader-wrapper").css("display", "none");
        return false;
    }
    if (!mobile.match('[0-9]{10}') || mobile.length > 10) {
        $("#err-mob").show();
        return;
    } else {
        $("#err-mob").hide();
    }
    //alert("r");
    var testEmail = /^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/;
    if (!testEmail.test(email)) {
        $("#err-email").show();
        return;
    } else {
        $("#err-email").hide();
    }


    //$("#loader-wrapper").css("display", "block");
    var model = {
        MobileNo: mobile,
        EmailId: email,
        Name: name,
        CaptchaCode: code
    }

    if (name != "" && mobile != "" && email != "") {

        // beforeSend: function() {


        //    }

        HttpPost(`/SignupForm/sendotp`, 'json', model, function (response) {
            // setTimeout(function () {
            $("#loader-wrapper").css("display", "block");
            if (response[0] == "true") {
                setTimeout(function () {
                    $("#loader-wrapper").css("display", "none");
                }, 3000);
                $("#sinupshow").hide();
                $("#shomsgsuccess").show();
                $("#sotp").show();
                $("#sotp").val(response.otp);
                resetCaptchaImage();
                //  $("#loader-wrapper").css("display", "none");
            } else {
                resetCaptchaImage();
                $("#loader-wrapper").css("display", "none");
                $("#err-comm").text(response[1]).show();
            }
        });

    }


});
$("#otp-button").click(function () {
    $("#suc-comm").hide();
    $("#err-otp").hide();
    var model = { 
        CaptchaCode: $("#otp").val()
    }
     
    HttpPost(`/SignupForm/verifyOTP`, 'json', model, function (response) {
        debugger;
        var otp = $("#otp").val();
        var rotp = localStorage.getItem("otp");
        if (response[0] == "true")  {
            $("#suc-comm").show();
            $("#form").submit();

        } else {
            $("#otp").val('');
            $("#err-otp").text(response[1]).show();            
        }
    });
  
});