

function GetZoneListForHandOver(id) {
    debugger
    HttpGet(`/LandTransfer/GetZoneList/?DepartmentId=${id}`, 'json', function (response) {
        debugger
        var html = '<option value="">Select</option>';
        for (var i = 0; i < response.length; i++) {
            html = html + '<option value=' + response[i].id + '>' + response[i].name + '</option>';
        }
        $("#HandedOverZoneId").html(html);
        $("#HandedOverDivisionId").html('<option value="">Select</option>');
        $("#HandedOverZoneId").select2('val','');
        $("#HandedOverDivisionId").select2('val','');
    });
};
function GetDivisionListForHandOver(id) {
    HttpGet(`/LandTransfer/GetDivisionList/?ZoneId=${id}`, 'json', function (response) {
        var html = '<option value="">Select</option>';
        for (var i = 0; i < response.length; i++) {
            html = html + '<option value=' + response[i].id + '>' + response[i].name + '</option>';
        }
        $("#HandedOverDivisionId").html(html);
        $("#HandedOverDivisionId").select2('val', '');
    });
};
function GetZoneListForTakenOver(id) {
    debugger
    HttpGet(`/LandTransfer/GetZoneList/?DepartmentId=${id}`, 'json', function (response) {
        debugger
        var html = '<option value="">Select</option>';
        for (var i = 0; i < response.length; i++) {
            html = html + '<option value=' + response[i].id + '>' + response[i].name + '</option>';
        }
        $("#TakenOverZoneId").html(html);
        $("#TakenOverDivisionId").html('<option value="">Select</option>');
        $("#TakenOverZoneId").select2('val','');
        $("#TakenOverDivisionId").select2('val','');
    });
};
function GetDivisionListForTakenOver(id) {
    HttpGet(`/LandTransfer/GetDivisionList/?ZoneId=${id}`, 'json', function (response) {
        var html = '<option value="">Select</option>';
        for (var i = 0; i < response.length; i++) {
            html = html + '<option value=' + response[i].id + '>' + response[i].name + '</option>';
        }
        $("#TakenOverDivisionId").html(html);
        $("#TakenOverDivisionId").select2('val', '');
    });
};

function onChangeDepartment(id) {
    debugger
    HttpGet(`/LandTransfer/GetZoneList/?DepartmentId=${id}`, 'json', function (response) {
        debugger
        var html = '<option value="">Select</option>';
        for (var i = 0; i < response.length; i++) {
            html = html + '<option value=' + response[i].id + '>' + response[i].name + '</option>';
        }
        $("#ZoneId").html(html);
        $("#DivisionId").html('<option value="">Select</option>');
        $("#LocalityId").html('<option value="">Select</option>');
        $("#ZoneId").select2('val','');
        $("#DivisionId").select2('val','');
        $("#LocalityId").select2('val','');
    });
};  
function onChangeZone(id) {
    HttpGet(`/LandTransfer/GetDivisionList/?ZoneId=${id}`, 'json', function (response) {
        var html = '<option value="">Select</option>';
        for (var i = 0; i < response.length; i++) {
            html = html + '<option value=' + response[i].id + '>' + response[i].name + '</option>';
        }
        $("#DivisionId").html(html);
        $("#LocalityId").html('<option value="">Select</option>');
        $("#DivisionId").select2('val', '');
        $("#LocalityId").select2('val', '');

    });
};
function onChangeDivision(id) {
    HttpGet(`/LandTransfer/GetLocalityList/?DivisionId=${id}`, 'json', function (response) {
        var html = '<option value="">Select</option>';
        for (var i = 0; i < response.length; i++) {
            html = html + '<option value=' + response[i].id + '>' + response[i].name + '</option>';
        }
        $("#LocalityId").html(html);
        $("#LocalityId").select2('val', '');
    });
};
function fileValidation(filePath, fileInput, size) {
    var allowedExtensions = /(\.jpg|\.jpeg|\.png|\.gif|\.pdf|\.xls|\.xlsx|\.docx|\.doc)$/i;
    if (!allowedExtensions.exec(filePath)) {
        alert('Invalid file type');
        fileInput.value = '';
        return false;
    }
    if (size > 10535049) {
        alert("File must be of 10 MB or Lesser Than 10 MB");
        fileInput.value = '';
        return false;
    }
}
$("#KhasraNo").change(function () {
    var khasraNo = $("#KhasraNo").val();
    $.get(`/LandTransfer/GetHistoryDetails/?KhasraNo=${khasraNo}`, function (response) {
        $('#LoadView').html("");
        $('#LoadView').html(response);
    });
});

$("#collapse").click(function () {
    $('#collapseExample').collapse("toggle").promise().done(function () {
        $("select").select2({
            placeholder: "Select",
            allowClear: true
        });
    })
});
function callSelect2() {
    $("select").select2({
        placeholder: "Select",
        allowClear: true
    });
}

$(document).ready(function () {
    $("input").each(function () {
        if ($(this).val() == '0') {
            $(this).val('');
        }
    });


    //var TakenOverLandLineNo = $("#TakenOverLandLineNo").val();
    //$("#TakenOverLandLineNo").val(Math.trunc(TakenOverLandLineNo));
    //var TakenOverMobileNo = $("#TakenOverMobileNo").val();
    //$("#TakenOverMobileNo").val(Math.trunc(TakenOverMobileNo));
    //var HandedOverMobileNo = $("#HandedOverMobileNo").val();
    //$("#HandedOverMobileNo").val(Math.trunc(HandedOverMobileNo));
    //var HandedOverLandLineNo = $("#HandedOverLandLineNo").val();
    //$("#HandedOverLandLineNo").val(Math.trunc(HandedOverLandLineNo));

    $(".numbers").keyup(function () {
        var buildupArea = $("input[name='BuildupArea']").val();
        var VacantArea = $("input[name='VacantArea']").val();
        $("input[name='TotalArea']").val(parseFloat(buildupArea == '' ? '0' : buildupArea) + parseFloat(VacantArea == '' ? '0' : VacantArea));
    });
    $('#CopyofOrder').change(function () {
        var fileInput = document.getElementById('CopyofOrder');
        var filePath = fileInput.value;
        const size = (CopyofOrder.files[0].size);
        fileValidation(filePath, fileInput, size);
    });
    var value = $('#Propertyregistration_Boundary option:selected').val();
    if (value == 1 || value == 0) {
        $('#divBoundarySelection').show();
    }
    $("a").each(function () {
        if ($(this).attr("href") == '/LandTransfer/ViewDocument?path=') {
            $(this).removeAttr("href");
        }
    });
    var value = $('#Propertyregistration_AreaUnit option:selected').val();
    if (value == 0) {
        $("#AreainSqAcreHec").hide();
        $("#bighabis").show();
    }
    else {
        $("#AreainSqAcreHec").show();
        $("#bighabis").hide();
        if (value == 1)
            $('#LabelTotalAreaSqAcreHec').html('Total Area(' + "Sq Yd." + ')');
        else if (value == 2)
            $('#LabelTotalAreaSqAcreHec').html('Total Area(' + "Acre" + ')');
        else if (value == 3)
            $('#LabelTotalAreaSqAcreHec').html('Total Area(' + "Hectare" + ')');
    }

    var value = $('#Propertyregistration_PlannedUnplannedLand').val();
    if (value == 'Planned Land') {
        $('#divUnplannedSelection').hide();
        $('#divPlannedSelection').show();
    }
    else {
        $('#divUnplannedSelection').show();
        $('#divPlannedSelection').hide();
    }

    var value = $('#Propertyregistration_EncroachmentStatusId option:selected').val();
    if (value == 1) {
        $('#divEncroachmentYesSelection').show();
        $("#Propertyregistration_EncraochmentDetails").removeAttr("disabled");
        $("#Propertyregistration_Encroached").removeAttr("disabled");
        $("#Propertyregistration_BuiltupEncroachmentArea").removeAttr("disabled");
    }
    else {
        $("#Propertyregistration_EncraochmentDetails").attr("disabled", "disabled");
        $("#Propertyregistration_Encroached").attr("disabled", "disabled");
        $("#Propertyregistration_BuiltupEncroachmentArea").attr("disabled", "disabled");
    }

    var value = $('#Propertyregistration_BuiltUp option:selected').val();
    if (value == 1) {
        $("#divBuilttype").show();
    }
    else {
        $("#divBuilttype").hide();
    }

    var value = $('#Propertyregistration_LitigationStatus option:selected').val();
    if (value == 1) {
        $("#divLitigationStatusSelection").show();
    }

    var value = $('#Propertyregistration_LayoutPlan option:selected').val();
    if (value == 1) {
        $("#divLayoutPlan").show();
    }

    var value = $('#Propertyregistration_GeoReferencing option:selected').val();
    if (value == 1) {
        $("#divGEOReferencingSelection").show();
    }
    var status = $("#Encroachment option:selected").val();
    debugger
    if (status == '1') {
        $("#divEncroachmentYesSelectionForLandTransfer").show();
        $("#divEncroachmentYesSelectionForLandTransfer select").removeAttr("disabled");
        $("#divEncroachmentYesSelectionForLandTransfer input").removeAttr("disabled");
        $("#divEncroachmentYesSelectionForLandTransfer textarea").removeAttr("disabled");
    } else {
        $("#divEncroachmentYesSelectionForLandTransfer").hide();
        $("#divEncroachmentYesSelectionForLandTransfer select").attr("disabled", "disabled");
        $("#divEncroachmentYesSelectionForLandTransfer input").attr("disabled", "disabled");
        $("#divEncroachmentYesSelectionForLandTransfer textarea").attr("disabled", "disabled");
    }
    callSelect2();
});
function ChangeEncroachmentStatus(status) {
    if (status == '1') {
        $("#divEncroachmentYesSelectionForLandTransfer").show();
        $("#divEncroachmentYesSelectionForLandTransfer select").removeAttr("disabled");
        $("#divEncroachmentYesSelectionForLandTransfer input").removeAttr("disabled");
        $("#divEncroachmentYesSelectionForLandTransfer textarea").removeAttr("disabled");
        $("#divEncroachmentYesSelectionForLandTransfer select").val('');
        $("#divEncroachmentYesSelectionForLandTransfer select").select2('val','');
        $("#divEncroachmentYesSelectionForLandTransfer input").val('');
        $("#divEncroachmentYesSelectionForLandTransfer textarea").val('');
    } else {
        $("#divEncroachmentYesSelectionForLandTransfer").hide();
        $("#divEncroachmentYesSelectionForLandTransfer select").attr("disabled","disabled");
        $("#divEncroachmentYesSelectionForLandTransfer input").attr("disabled", "disabled");
        $("#divEncroachmentYesSelectionForLandTransfer textarea").attr("disabled", "disabled");
    }
    callSelect2();
}