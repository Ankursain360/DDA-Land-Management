$("#btnlogin").click(function () {

    var txtpassword = document.getElementById("Password").value.trim();
    var txtUserName = document.getElementById("Username").value.trim();
    
    if (txtpassword == "") {
        alert('Please enter password.');
        return false;
    }
    if (txtUserName == "") {
        alert('Please enter UserName.');
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

$("#refreshcaptcha").click(function () {
    resetCaptchaImage();
});

function resetCaptchaImage() {
    d = new Date();
    $("#img-captcha").attr("src", "/get-captcha-image?" + d.getTime());
}
