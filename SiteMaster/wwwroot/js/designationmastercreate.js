$(function () {
    $("#txtDesignation").keypress(function (e) {
        var keyCode = e.keyCode || e.which;

        $("#lblError").html("");

        //Regex for Valid Characters i.e. Alphabets and Numbers.
        var regex = /^[A-Za-z0-9]+$/;

        //Validate TextBox value against the Regex.
        var isValid = regex.test(String.fromCharCode(keyCode));
        if (!isValid) {
            $("#lblError").html("Only Alphabets and Numbers allowed.");
        }

        return isValid;
    });
});

$(function () {
    $("#txtDesignation").keypress(function (e) {
        var keyCode = e.keyCode || e.which;

        $("#lblError").html("");

        //Regex for Valid Characters i.e. Alphabets and Numbers.
        var regex = /^[A-Za-z0-9]+$/;

        //Validate TextBox value against the Regex.
        var isValid = regex.test(String.fromCharCode(keyCode));
        if (!isValid) {
            $("#lblError").html("Only Alphabets and Numbers allowed.");
        }

        return isValid;
    });
});
