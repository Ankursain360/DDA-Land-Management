$(document).ready(function () {
    $("#Area").attr("readonly", "readonly");
    var id = parseInt($('#WatchWardId').val());
    GetOtherDetails(id);

    $(".inspectionbbbcalculation").keyup(function () {
        var inbigha = $('#TotalAreaInBighaInspection').val();
        var inbiswa = $('#TotalAreaInBiswaInspection').val();
        var inbiswani = $('#TotalAreaInBiswaniInspection').val();
        var inbighavalue = parseInt(inbigha == '' ? 0 : inbigha) * 1008 * 0.836;    //1008 * 0.836 = 842.688
        var inbiswavalue = parseInt(inbiswa == '' ? 0 : inbiswa) * 42.134;    //842.688/20  = 42.134
        var inbiswanivalue = parseInt(inbiswani == '' ? 0 : inbiswani) * 2.1067;     //42.134/20 = 2.1067

        var totalarea = inbighavalue + inbiswavalue + inbiswanivalue;

        $("input[id='inspectionArea']").val(totalarea.toFixed(3));
    });


    $(".inspectionTotalCalculation").keyup(function () {
        debugger;
        var value = $('#inspectionAreaUnit option:selected').val();
        var totalOther = $('#inspectionTotalAreaInSqAcreHt').val();
        if (value == 1) {
            $("input[id='inspectionArea']").val((parseFloat(totalOther == '' ? 0 : totalOther) * 0.836).toFixed(3));
        }
        else if (value == 2) {
            $("input[id='inspectionArea']").val((parseFloat(totalOther == '' ? 0 : totalOther) * 4840 * 0.836).toFixed(3));
        }
        else if (value == 3) {
            $("input[id='inspectionArea']").val((parseFloat(totalOther == '' ? 0 : totalOther) * 10000).toFixed(3));
        }
        else if (value == 4) {
            $("input[id='inspectionArea']").val((parseFloat(totalOther == '' ? 0 : totalOther)).toFixed(3));
        }
    });

    if ($('#StatusOfLand').val() == 'Other Govt. Land') {
        $("#divForLandStatus").show();
        callSelect2();
    }
    else {
        $("#divForLandStatus").hide();
    }

});

$('#inspectionAreaUnit').change(function () {
    debugger;
    var value = $('#inspectionAreaUnit option:selected').val();
    if (value == 0) {
        $('#inspectionTotalAreaInSqAcreHt').val('');
        $('#TotalAreaInBighaInspection').val('');
        $('#TotalAreaInBiswaInspection').val('');
        $('#TotalAreaInBiswaniInspection').val('');
        $('#inspectionArea').val('');
        $("#inspectionAreainSqAcreHec").hide();
        $("#inspectionbighabis").show();
    }
    else {
        $('#inspectionTotalAreaInSqAcreHt').val('');
        $('#TotalAreaInBighaInspection').val('');
        $('#TotalAreaInBiswaInspection').val('');
        $('#TotalAreaInBiswaniInspection').val('');
        $('#inspectionArea').val('');
        $("#inspectionAreainSqAcreHec").show();
        $("#inspectionbighabis").hide();
        if (value == 1)
            $('#LabelTotalAreaSqAcreHec').html('Total Area(' + "Sq Yd." + ')');
        else if (value == 2)
            $('#LabelTotalAreaSqAcreHec').html('Total Area(' + "Acre" + ')');
        else if (value == 3)
            $('#LabelTotalAreaSqAcreHec').html('Total Area(' + "Hectare" + ')');
        else if (value == 4)
            $('#LabelTotalAreaSqAcreHec').html('Total Area(' + "Sq. Mt." + ')');
    }
});

function GetOtherDetails(id) {
    HttpGet(`/EncroachmentRegister/WatchWardView/?Id=${id}`, 'html', function (response) {
        $('#WatchWardDetailsDiv').html("");
        $('#WatchWardDetailsDiv').html(response);
    });
};

function onChangeLandStatus(status) {
    if (status == 'Other Govt. Land') {
        $("#divForLandStatus").show();
        callSelect2();
    }
    else {
        $("#divForLandStatus").hide();
    }
};
//function onChangePossationStatus(status) {
//    if (status == 'Yes') {
//        $("#divPossessionGroup").show();
//    }
//    else {
//        $("#divPossessionGroup").hide();
//    }
//};
function onChangeDepartment(id) {
    HttpGet(`/EncroachmentRegister/GetZoneList/?DepartmentId=${id}`, 'json', function (response) {
        var html = '<option value="">Select</option>';
        for (var i = 0; i < response.length; i++) {
            html = html + '<option value=' + response[i].id + '>' + response[i].name + '</option>';
        }
        $(".ZoneClass").html(html);
        $(".DivisionClass").html('<option value="">Select</option>');
        $(".LocalitiyClass").html('<option value="">Select</option>');
        $(".LocalitiyClass").select2('val', '')
        $(".DivisionClass").select2('val', '');
        $(".ZoneClass").select2('val', '');
    });
};
function onChangeZone(id) {
    HttpGet(`/EncroachmentRegister/GetDivisionList/?ZoneId=${id}`, 'json', function (response) {
        var html = '<option value="">Select</option>';
        for (var i = 0; i < response.length; i++) {
            html = html + '<option value=' + response[i].id + '>' + response[i].name + '</option>';
        }
        $(".DivisionClass").html(html);
        $(".LocalitiyClass").html('<option value="">Select</option>');
        $(".LocalitiyClass").select2('val', '')
        $(".DivisionClass").select2('val', '');
    });


    //HttpGet(`/EncroachmentRegister/GetLocalityList/?DivisionId=${id}`, 'json', function (response) {
    //    var html = '<option value="">Select</option>';
    //    for (var i = 0; i < response.length; i++) {
    //        html = html + '<option value=' + response[i].id + '>' + response[i].name + '</option>';
    //    }
    //    $(".LocalitiyClass").html(html);
    //    $(".LocalitiyClass").select2('val', '')
    // });
};
function onChangeDivision(id) {
    HttpGet(`/EncroachmentRegister/GetLocalityList/?DivisionId=${id}`, 'json', function (response) {
        var html = '<option value="">Select</option>';
        for (var i = 0; i < response.length; i++) {
            html = html + '<option value=' + response[i].id + '>' + response[i].name + '</option>';
        }
        $(".LocalitiyClass").html(html);
        $(".LocalitiyClass").select2('val', '')
    });
};
$(function () {
    $("#tbl_posts #tbl_posts_body .odd").remove();
    $("#tbl_posts #add .form-control").attr("multiple", false);
    $("input[name='grpPossession']").click(function () {
        if ($("#rdbFormal").is(":checked")) {
            $("#divDepartment").show();
            $("#PossessionType").val("Formal Possession");
        } else {
            $("#divDepartment").hide();
            $("#PossessionType").val("Physical Possession");
        }
    });
});
$(document).delegate('a.add-record', 'click', function (e) {
    debugger

    if ($("#tbl_posts #add #ConstructionStatus").children("option:selected").val() != '' && $("#tbl_posts #add #ConstructionStatus").children("option:selected").val() != undefined && $("#tbl_posts #add #NameOfStructure").val() != '' && $("#tbl_posts #add #AreaApprox").val() != '' && $("#tbl_posts #add #Type").val() != ''
        && $("#tbl_posts #add #DateOfEncroachment").val() != '' && $("#tbl_posts #add #ReligiousStructure").val() != '' && $("#tbl_posts #add #ReferenceNoOnLocation").val() != '' && $("#tbl_posts #add #CountOfStructure").val() != ''
    ) {
        var ConstructionStatus = $("#tbl_posts #add #ConstructionStatus").children("option:selected").val();
        var ReligiousStructure = $("#tbl_posts #add #ReligiousStructure").children("option:selected").val();
        e.preventDefault();
        var content = jQuery('#tbl_posts #add tr'),
            size = jQuery('#tbl_posts >tbody >tr').length,
            element = null,
            element = content.clone();
        element.attr('id', 'rec-' + size);
        element.find('.delete-record').attr('data-id', size);
        element.appendTo('#tbl_posts_body');
        $('#tbl_posts_body #rec-' + size + ' #ConstructionStatus').val(ConstructionStatus);
        $('#tbl_posts_body #rec-' + size + ' #ReligiousStructure').val(ReligiousStructure);
        element.find('.sn').html(size);
        $("#tbl_posts #add .sn").text($('#tbl_posts >tbody >tr').length);
        $("#tbl_posts #add .add").remove();
        $("#tbl_posts #tbl_posts_body .floating-label-field").attr("readonly", true);
        element.find(".add-record").hide();
        element.find(".delete-record").show();
        debugger
        /*$("#tbl_posts #add .form-control").val('');*/
        $("#tbl_posts #add .floating-label-field").val('');
        $("#tbl_posts #add #ConstructionStatus").val("").trigger('change');
        $("#tbl_posts #add #ReligiousStructure").val("").trigger('change');
    }
    else {
        alert('Please fill record before add new record ');
    }
});
$(document).delegate('a.delete-record', 'click', function (e) {
    e.preventDefault();
    var didConfirm = confirm("Are you sure You want to delete");
    if (didConfirm == true) {
        var id = jQuery(this).attr('data-id');
        var targetDiv = jQuery(this).attr('targetDiv');
        jQuery('#rec-' + id).remove();
        //regnerate index number on table
        $('#tbl_posts_body tr').each(function (index) {
            //alert(index);
            $(this).find('span.sn').html(index + 1);
        });
        $("#tbl_posts #add .sn").text($('#tbl_posts >tbody >tr').length);
        return true;
    } else {
        return false;
    }
});

function callSelect2() {
    $("select").select2({
        placeholder: "Select",
        allowClear: true
    });
}


$("input[name='grpIsEncroachment']").click(function () {
    var selected = $("input[type='radio'][name='grpIsEncroachment']:checked");
    $("#IsEncroachment").val(selected.val());

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
                    url: "/EncroachmentRegister/CheckFile",
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

