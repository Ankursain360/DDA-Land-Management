

$('#mobile').keyup(function () {

    var mob = $('#mobile').val();
    if (!mob.match('[0-9]{10}') || mob.length > 10) {
        $("#err-mob").show();
        return;
    } else {
        $("#err-mob").hide();
    }

});


$("#signup2").click(function () {
 
    $("#err-comm").hide();
  
    var mobile = $("#mobile").val();

 
    if (mobile == "") {
        $("#err-mob").show();
    }
   
    var model = {
        MobileNo: mobile,
        EmailId:""

       
    }
   
    if (mobile != "") {
        HttpPost(`/SignupForm/sendotp1`, 'json', model, function (response) {
            //alert(response[2]);
            if (response[0] == "true") {
                localStorage.setItem("otp", response[2]);
                $("#login-div").hide();
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
    var rotp = '123456';//localStorage.getItem("otp");
    if (otp == rotp) {
     
        $("#suc-comm").show();
       window.location = "/KYCform/Index";
    } else {
         $("#otp").val('');
       // $("#err-otp").show();
        alert("Please Enter Correct OTP");
    }
});