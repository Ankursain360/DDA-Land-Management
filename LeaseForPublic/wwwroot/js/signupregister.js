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
      
        if(!((key == 8) || (key == 32) || (key == 46) || (key >= 35 && key <= 40) || (key >= 65 && key <= 90))) {
            e.preventDefault();
        }
    }
});
$("#signup2").click(function () {
    $("#err-comm").hide();
    var name = $("#name").val();
    var mobile = $("#mobile").val();
    var email = $("#email").val();
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
    if (status == 2) {
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
    var model = {
        MobileNo: mobile,
        EmailId: email
      
    }
   
    if (name != "" && mobile != "" && email != "") {
        HttpPost(`/SignupForm/sendotp`, 'json', model, function (response) {
            //alert(response);
            if (response[0] == "true") {
                localStorage.setItem("otp", response[2]);

                $("#sinupshow").hide();
                $("#shomsgsuccess").show();
                $("#sotp").show();
                $("#sotp").val(response.otp);
            } else {
                $("#err-comm").show();
            }
        });

    }


});
$("#otp-button").click(function () {
    $("#suc-comm").hide();
    $("#err-otp").hide();
    var otp = $("#otp").val();
    var rotp = localStorage.getItem("otp");
    if (otp == rotp) {
        $("#suc-comm").show();
        $("#form").submit();
    } else {
        $("#otp").val('');
        $("#err-otp").show();
        alert("Please Enter Correct OTP");
    }
});