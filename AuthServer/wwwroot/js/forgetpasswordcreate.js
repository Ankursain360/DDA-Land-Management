$("#refreshcaptcha").click(function () {
    resetCaptchaImage();
});

function resetCaptchaImage() {
    d = new Date();
    $("#img-captcha").attr("src", "/get-captcha-image?" + d.getTime());
}

function CheckPasswordValidation() {
    debugger;
    var checkresult = false;
    let regex = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@.#$!%*?&])[A-Za-z\d@.#$!%*?&]{8,15}$/;
    var password = document.getElementById("Password").value
    if (regex.test(password)) {
        checkresult = true;
        return checkresult;
    }
    else {
        alert('Passwords must contain at least eight characters, including uppercase, lowercase letters,numbers and special character (@#$!%*?&)')
    }
}