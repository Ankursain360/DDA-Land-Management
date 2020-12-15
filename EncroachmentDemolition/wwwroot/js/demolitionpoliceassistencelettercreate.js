$(document).ready(function () {
    if ($("#Generate").is(":checked")) {

        $("#divGenerate").show();
        $("#divUpload").hide();

    }
    else if ($("#Upload").is(":checked")) {
        $("#divGenerate").hide();
        $("#divUpload").show();
    }
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

//$(function () {
//    $('#MeetingTime').datetimepicker();
//});

//$("#btnCreate").click(function () {
//    var checkresult = false;
//    var dropdown_val = $('#ModuleId option:selected').val();
//    if (parseInt(dropdown_val) < 1) {
//        checkresult = false;
//        $("#ModuleIdMessage").show();
//    } else {
//        checkresult = true;
//    }

//    var Name_val = $('#Name').val();
//    if (Name_val == "") {
//        checkresult = false;
//        $("#NameMessage").show();
//    } else {
//        checkresult = true;
//    }

//    var Description_val = $('#Description').val();
//    if (Description_val == "") {
//        checkresult = false;
//        $("#DescriptionMessage").show();
//    } else {
//        checkresult = true;
//    }

//    if (parseInt(dropdown_val) < 1 || Name_val == "" || Description_val == "") {
//        checkresult = false;
//    }

//    if (checkresult) {
//        var param = GetListData();
//        HttpPost(`/WorkFlowTemplate/Create`, 'json', param, function (response) {
//            window.location.href = response;  //'/WorkFlowTemplate/Index';
//            SuccessMessage('Data updated successfully.');
//        });
//    }

//});