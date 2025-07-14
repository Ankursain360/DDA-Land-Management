$("#btnlogin").click(function () {

    var txtpassword = document.getElementById("Password").value.trim();
    var txtUserName = document.getElementById("Username").value.trim();
    
    if (txtpassword == "") {
        toastr.error('Please Enter Password', 'Error', { timeOut: 3000, "progressBar": true });
        alert('Please Enter Password');
        return false;
    }
    if (txtUserName == "") {
        toastr.error('Please Enter Username', 'Error', { timeOut: 3000, "progressBar": true });
        alert('Please Enter Username');
        return false;
    }
    else {
        var keyss = document.getElementById("hdnvals").value.trim();
        //var keyss = '@ViewBag.hdnValue' ;
        var key = CryptoJS.enc.Utf8.parse(keyss);
        var iv = CryptoJS.enc.Utf8.parse(keyss);
        //var key = CryptoJS.enc.Utf8.parse('8080808080808080');
        //var iv = CryptoJS.enc.Utf8.parse('8080808080808080');
        var encryptedpassword = CryptoJS.AES.encrypt(CryptoJS.enc.Utf8.parse(txtpassword), key,
            {
                keySize: 128 / 8,
                iv: iv,
                mode: CryptoJS.mode.CBC,
                padding: CryptoJS.pad.Pkcs7
            });
        var encryptedUsername = CryptoJS.AES.encrypt(CryptoJS.enc.Utf8.parse(txtUserName), key,
            {
                keySize: 128 / 8,
                iv: iv,
                mode: CryptoJS.mode.CBC,
                padding: CryptoJS.pad.Pkcs7
            });
        document.getElementById("Password").value = encryptedpassword;
        document.getElementById("Username").value = encryptedUsername;
    }
});

//$("#refreshcaptcha").click(function () {
//    resetCaptchaImage();
//});

//function resetCaptchaImage() {
//    d = new Date();
//    $("#img-captcha").attr("src", "/get-captcha-image?" + d.getTime());
//}
$("#refreshcaptcha").click(function () {
    $.ajax({
        url: "/refresh-captcha",
        type: "GET",
        success: function (data) {
            $("#img-captcha").attr("src", data.image); // Update image
            $("#captchaValue").val(data.code); // Update hidden field
        },
        error: function () {
            alert("Failed to refresh CAPTCHA.");
        }
    });
});

$("#showpassword").click(function (){
    var value = $('#Password');
    if (value[0].type== 'password') {
        value[0].type = "text";
    }
    else {

        value[0].type = "password";
    }
});
document.getElementById("speakCaptcha").addEventListener("click", function () {
    debugger;
    var captcha = document.getElementById("captchaValue").value;

    if (!captcha) {
        alert("Captcha not available.");
        return;
    }

    var msg = new SpeechSynthesisUtterance();
    msg.text = captcha.split("").join(" "); 
    msg.lang = "en-US";
    msg.rate = 0.8;
    msg.pitch = 1;

    window.speechSynthesis.speak(msg);
});

$(".icon-clickable").on("keydown", function (e) {
    if (e.key === "Enter" || e.key === " ") {
        e.preventDefault();
        $(this).click(); // Trigger the click event
    }
});
