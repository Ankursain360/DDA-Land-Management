$(document).ready(function () {
   
});

$("input[name='radioStatus']").click(function () {
    if ($("#Generate").is(":checked")) {

        $("#divGenerate").show();
        $("#divUpload").hide();

    }
    else if ($("#Upload").is(":checked")) {
        $("#divGenerate").hide();
        $("#divUpload").show();
    }

});