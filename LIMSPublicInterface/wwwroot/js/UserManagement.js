

$("#defaultpass").click(function () {
    var val = $("#defaultpassword_text").val();
   // alert(vall);
    //val 1=off,2=on
    if ($("#defaultpass").is(':checked')) {
        $("#defaultpassword_text").val(2);
        $("#Password").attr("disabled", "disabled");
        $("#ConfirmPassword").attr("disabled", "disabled");
    } else {
        $("#defaultpassword_text").val(1);
        $("#Password").removeAttr("disabled");
        $("#ConfirmPassword").removeAttr("disabled");
        $("#Password").focus();
        $("#ConfirmPassword").focus();

    }

});