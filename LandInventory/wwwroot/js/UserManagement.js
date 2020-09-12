function PasswordMatch() {
    var Password = $("#Password").val();
    var ConfirmPassword = $("#ConfirmPassword").val();
    if (Password != '') {
        alert("Please Enter Password");
        break;
    }
    if (Password != ConfirmPassword) {
        alert("Password Not Matched Please Enter Same Password");
        break;
    }
}