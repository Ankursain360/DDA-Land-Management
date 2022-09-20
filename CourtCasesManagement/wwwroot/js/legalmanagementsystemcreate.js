 
function GetLocalityList(id) {
    //debugger;
    HttpGet(`/Legalmanagementsystem/GetLocalityList/?ZoneId=${id}`, 'json', function (response) {
               var html = '<option value="">Select</option>';
        for (var i = 0; i < response.length; i++) {
            html = html + '<option value=' + response[i].id + '>' + response[i].name + '</option>';
        }
        $("#LocalityId").html(html);
    });

};
$('#ddlStayInterim').change(function () {
    var value = $('#ddlStayInterim option:selected').val();
    //$('#si').val() = value;
    if (value == 0) {
        $('#DivStayInterim').hide();
       

    }
    else {
        $('#DivStayInterim').show();
       
    }
   // debugger
});

$(document).ready(function () {

    if ($("#Case").is(":checked")) {
        $('#CaseType').val('24(2) Case');
    } else {
        $('#CaseType').val('Other');
    }

    if ($("#DDA").is(":checked")) {
        $('#InFavour').val('DDA');
    } else {
        $('#InFavour').val('Against DDA');
    }
   
});

$(function () {
    $("input[name='grpCaseType']").click(function () {
        if ($("#Case").is(":checked")) {
            $('#CaseType').val('24(2) Case');
        } else {
            $('#CaseType').val('Other');
        }
    });
});
$(function () {
    $("input[name='grpInfavour']").click(function () {
        if ($("#DDA").is(":checked")) {
            $('#InFavour').val('DDA');
        } else {
            $('#InFavour').val('Against DDA');
        }
    });

    //check
    var value = $('#ddlJudgement option:selected').val();
    if (value == 0) {
        $('#DivJudgement').hide();
    }
    else {
        $('#DivJudgement').show();
    }

    var value = $('#ddlStayInterim option:selected').val();
    //$('#si').val() = value;
    if (value == 0) {
        $('#DivStayInterim').hide();


    }
    else {
        $('#DivStayInterim').show();

    }
});

$("input[name='grpCaseType']").click(function () {
    var selected = $("input[type='radio'][name='grpCaseType']:checked");
    $("#CaseType").val(selected.val());

});
$("input[name='grpInfavour']").click(function () {
    var selected = $("input[type='radio'][name='grpInfavour']:checked");
    $("#InFavour").val(selected.val());

});


$('#ddlJudgement').change(function () {
    var value = $('#ddlJudgement option:selected').val();
    if (value == 0) {
        $('#DivJudgement').hide();
    }
    else {
        $('#DivJudgement').show();
    }
});

$('.checkExtension').on('change', function (e) {

    debugger;
    var flag = false;
    var result = $(this).val();
    var file = result;
    if (file != null) {

        var multi = file.split(".");
        if (multi.length > 2) {

            alert("Please upload proper file with single dot in filename");
            $(this).val('');
            return;
        }
        var extension = file.substr((file.lastIndexOf('.') + 1));

        switch (extension) {
            case 'pdf':
                flag = true;
                $('#error').empty();
                break;
            case 'PDF':
                flag = true;
                $('#error').empty();
                break;
            default:
                alert("You can upload only pdf extension file Only")
                $(this).val('');
                flag = false;
        }


        if (flag == true) {

            var FileID = $(this).attr('id');

            var size = ValidateFileSize(FileID, $(this));

            if (size > 5) {
                alert("You Can Upload file Size Up to 5 MB.");
                $(this).val('');
            }
            else {

                filecontrol = $(this);
                var myformData = new FormData();
                myformData.append('file', $(this)[0].files[0]);
                $.ajax({
                    async: false,
                    type: "POST",
                    url: "/Legalmanagementsystem/CheckFile",
                    contentType: false,
                    processData: false,
                    data: myformData,
                    success: function (response) {

                        showResult(response, filecontrol)

                    },
                    failure: function (response) {
                        //alert(response.d);
                        return false;
                    }
                });
                function showResult(response, filecontrol) {
                    debugger;
                    if (response == false) {
                        alert("Please select vaild pdf file.");
                        filecontrol.val('');
                    }
                    else {
                        return true;
                    }
                }

            }
        }
    }


});



function ValidateFileSize(fileid, file) {
    try {
        var fileSize = 0;
        if (navigator.userAgent.match(/msie/i)) {
            var obaxo = new ActiveXObject("Scripting.FileSystemObject");
            var filePath = file[0].value;
            var objFile = obaxo.getFile(filePath);
            var fileSize = objFile.size;
            fileSize = fileSize / 1048576;
        }
        else {
            fileSize = file[0].files[0].size
            fileSize = fileSize / 1048576;
        }

        return fileSize;
    }
    catch (e) {
        alert("Error is :" + e);
    }
}
function getKhasraList(id) {
    debugger;
    HttpGet(`/Legalmanagementsystem/getKhasraList/?acquiredVillageId=${id}`, 'json', function (response) {
        var html = '<option value="">Select</option>';
        for (var i = 0; i < response.length; i++) {
            html = html + '<option value=' + response[i].id + '>' + response[i].name + '</option>';
        }
        $("#khasraId").empty().html(html);
        $("#khasraId").val('').trigger('change');
       
    });

};
function empty() {
    debugger;
    var village = $('#acquiredVillageId option:selected').val();
    if (village != "") {
        alert("Khasra is mandatory");
        return false;
    }
    else {
        return true;
    }

};
