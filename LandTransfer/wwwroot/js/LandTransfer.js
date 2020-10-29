function onChangeDepartment(id) {
    HttpGet(`/LandTransfer/GetZoneList/?DepartmentId=${id}`, 'json', function (response) {
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
function GetZoneList(id) {
    HttpGet(`/LandTransfer/GetZoneList/?DepartmentId=${id}`, 'json', function (response) {
        var html = '<option value="">Select</option>';
        for (var i = 0; i < response.length; i++) {
            html = html + '<option value=' + response[i].id + '>' + response[i].name + '</option>';
        }
        $("#ZoneId").html(html);
        $("#DivisionId").html('<option value="">Select</option>');
        $("#LocalityId").html('<option value="">Select</option>');
        $("#ZoneId").select2('val', '');
        $("#DivisionId").select2('val', '');
        $("#LocalityId").select2('val', '');
    });
}
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
    debugger
    $('#collapseExample').collapse("toggle").promise().done(function () {
        $("select").select2({
            placeholder: "Select",
            allowClear: true
        });
    })
});
$('#Boundary').change(function () {
    debugger;
    var value = $('#Boundary option:selected').val();
    if (value == 0) {
        $('#BoundaryRemarks').val('');
        $('#divBoundarySelection').hide();
    }
    else {
        $('#BoundaryRemarks').val('');
        $('#divBoundarySelection').show();
    }
});



$('#BuiltUp').change(function () {
    var value = $('#BuiltUp option:selected').val();
    if (value == 0) {
        $('#BuiltUpRemarks').val('');
        $("#divBuilttype").hide();
    }
    else {
        $('#BuiltUpRemarks').val('');
        $("#divBuilttype").show();
    }
});

$('#LitigationStatus').change(function () {
    var value = $('#LitigationStatus option:selected').val();
    if (value == 0) {
        $('#LitigationStatusRemarks').val('');
        $("#divLitigationStatusSelection").hide();
    }
    else {
        $('#LitigationStatusRemarks').val('');
        $("#divLitigationStatusSelection").show();
    }
});



$('#GeoReferencing').change(function () {
    var value = $('#GeoReferencing option:selected').val();
    if (value == 0) {
        $('#GeoFileName').val('');
        $("#divGEOReferencingSelection").hide();
    }
    else {
        $('#GeoFileName').val('');
        $("#divGEOReferencingSelection").show();
    }
});

function callSelect2() {
    $("select").select2({
        placeholder: "Select",
        allowClear: true
    });
}

$('#EncroachmentStatusId').change(function () {
    var value = $('#EncroachmentStatusId option:selected').val();
    if (value == 0) {
        $('#divEncroachmentYesSelection').hide();
        $('#divEncroachmentDetails').hide();
    }
    else {
        $('#divEncroachmentYesSelection').show();
        $('#divEncroachmentDetails').show();
        callSelect2();
    }
});




$('#PlannedUnplannedLand').change(function () {
    var value = $('#PlannedUnplannedLand').val();
    if (value == 'Planned Land') {
        $('#DivLandUse').show();
        $('#divPlannedSelection').show();
        $("#divLayoutPlan").show();
        $('#divUnplannedSelection').hide();
        callSelect2();
    }
    else {
        $('#DivLandUse').hide();
        $('#divPlannedSelection').hide();
        $("#divLayoutPlan").hide();
        $('#divUnplannedSelection').show();
        callSelect2();
    }
});

$(document).ready(function () {
    $("input").each(function () {
        if ($(this).val() == '0') {
            $(this).val('');
        }
    });
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
    var khasraNo = $("#KhasraNo").val();
    if (khasraNo != '') {
        $.get(`/LandTransfer/GetHistoryDetails/?KhasraNo=${khasraNo}`, function (response) {
            $('#LoadView').html("");
            $('#LoadView').html(response);
        });
    }
    var value = $('#Propertyregistration_Boundary option:selected').val();
    if (value == 1) {
        $('#divBoundaryRemarks').show();
    }

    var value = $('#Propertyregistration_PlannedUnplannedLand').val();
    if (value == 'Planned Land') {
        $('#divPlannedLand').show();
    }
    else {
        $('#divPlannedLand').hide();
    }

    var value = $('#Propertyregistration_EncroachmentStatusId option:selected').val();
    if (value == 1) {
        $("#Propertyregistration_EncraochmentDetails").attr("disabled", "disabled");
        $("#Propertyregistration_EncraochmentDetails").removeAttr("disabled", "disabled");
        $("#Propertyregistration_Encroached").attr("disabled", "disabled");
        $("#Propertyregistration_Encroached").removeAttr("disabled", "disabled");
        $("#Propertyregistration_BuiltupEncroachmentArea").attr("disabled", "disabled");
        $("#Propertyregistration_BuiltupEncroachmentArea").removeAttr("disabled", "disabled");
    }
    else {
        $("#Propertyregistration_EncraochmentDetails").attr("disabled", "disabled");
        $("#Propertyregistration_Encroached").attr("disabled", "disabled");
        $("#Propertyregistration_BuiltupEncroachmentArea").attr("disabled", "disabled");
    }

    var value = $('#Propertyregistration_EncroachmentStatusId2 option:selected').val();
    if (value == 1) {
        $("#Propertyregistration_EncraochmentDetails2").attr("disabled", "disabled");
        $("#Propertyregistration_EncraochmentDetails2").removeAttr("disabled", "disabled");
        $("#Propertyregistration_Encroached2").attr("disabled", "disabled");
        $("#Propertyregistration_Encroached2").removeAttr("disabled", "disabled");
        $("#Propertyregistration_BuiltupEncroachmentArea2").attr("disabled", "disabled");
        $("#Propertyregistration_BuiltupEncroachmentArea2").removeAttr("disabled", "disabled");
    }
    else {
        $("#Propertyregistration_EncraochmentDetails2").attr("disabled", "disabled");
        $("#Propertyregistration_Encroached2").attr("disabled", "disabled");
        $("#Propertyregistration_BuiltupEncroachmentArea2").attr("disabled", "disabled");
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
        $("#divLitigationStatus").show();
    }

    var value = $('#Propertyregistration_LayoutPlan option:selected').val();
    if (value == 1) {
        $("#divLayoutPlan").show();
    }

    var value = $('#Propertyregistration_GeoReferencing option:selected').val();
    if (value == 1) {
        $("#divGEOReferencing").show();
    }

    var value = $('#Propertyregistration_Boundary option:selected').val();
    if (value == 1) {
        $('#divBoundarySelection').show();
    }
    else {
        $('#divBoundarySelection').hide();
    }


    var value = $('#Propertyregistration_PlannedUnplannedLand').val();
    if (value == 'Planned Land') {
        $('#DivLandUse').show();
        $('#divPlannedSelection').show();
        $('#divUnplannedSelection').hide();
        $("#divLayoutPlan").show();
        callSelect2();
    }
    else {
        $('#DivLandUse').hide();
        $('#divPlannedSelection').hide();
        $('#divUnplannedSelection').show();
        $("#divLayoutPlan").hide();
    }

    var value = $('#Propertyregistration_EncroachmentStatusId option:selected').val();
    if (value == 1) {
        $('#divEncroachmentYesSelection').show();
        $('#divEncroachmentDetails').show();
        $("select").select2({
            placeholder: "Select",
            allowClear: true
        });
    }
    else {
        $('#divEncroachmentYesSelection').hide();
        $('#divEncroachmentDetails').hide();
    }

    var value = $('#Propertyregistration_EncroachmentStatusId2 option:selected').val();
    if (value == 1) {
        $('#divEncroachmentYesSelection2').show();
        $('#divEncroachmentDetails2').show();
        $("select").select2({
            placeholder: "Select",
            allowClear: true
        });
    }
    else {
        $('#divEncroachmentYesSelection2').hide();
        $('#divEncroachmentDetails2').hide();
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
    else {
        $('#divLitigationStatusSelection').hide();
    }


    var value = $('#Propertyregistration_GeoReferencing option:selected').val();
    if (value == 1) {
        $("#divGEOReferencingSelection").show();
    }
    else {
        $('#divGEOReferencingSelection').hide();
    }
    var departmentid = $('#Propertyregistration_DepartmentId option:selected').val();
    if (departmentid > 0) {
        debugger;
        GetZoneList(departmentid);
    }
});

$('#Propertyregistration_Boundary').change(function () {
    var value = $('#Propertyregistration_Boundary option:selected').val();
    if (value == 0) {
        $('#Propertyregistration_BoundaryRemarks').val('');
        $('#divBoundaryRemarks').hide();
    }
    else {
        $('#Propertyregistration_BoundaryRemarks').val('');
        $('#divBoundaryRemarks').show();
    }
});



$('#Propertyregistration_BuiltUp').change(function () {
    var value = $('#Propertyregistration_BuiltUp option:selected').val();
    if (value == 0) {
        $('#Propertyregistration_BuiltUpRemarks').val('');
        $("#divBuilttype").hide();
    }
    else {
        $('#Propertyregistration_BuiltUpRemarks').val('');
        $("#divBuilttype").show();
    }
});



$('#Propertyregistration_LayoutPlan').change(function () {
    var value = $('#Propertyregistration_LayoutPlan option:selected').val();
    if (value == 0) {
        $('#Propertyregistration_LayoutFileName').val('');
        $("#divLayoutPlan").hide();
    }
    else {
        $('#Propertyregistration_LayoutFileName').val('');
        $("#divLayoutPlan").show();
    }
});




$('#Propertyregistration_LitigationStatus').change(function () {
    var value = $('#Propertyregistration_LitigationStatus option:selected').val();
    if (value == 0) {
        $('#Propertyregistration_LitigationStatusRemarks').val('');
        $("#divLitigationStatus").hide();
    }
    else {
        $('#Propertyregistration_LitigationStatusRemarks').val('');
        $("#divLitigationStatus").show();
    }
});



$('#Propertyregistration_GeoReferencing').change(function () {
    var value = $('#Propertyregistration_GeoReferencing option:selected').val();
    if (value == 0) {
        $('#Propertyregistration_GeoFileName').val('');
        $("#divGEOReferencing").hide();
    }
    else {
        $('#Propertyregistration_GeoFileName').val('');
        $("#divGEOReferencing").show();
    }
});



$('#Propertyregistration_EncroachmentStatusId').change(function () {
    var value = $('#Propertyregistration_EncroachmentStatusId option:selected').val();
    if (value == 0) {
        $('#Propertyregistration_EncraochmentDetails').val('');
        $("#Propertyregistration_EncraochmentDetails").attr("disabled", "disabled");
        $('#Propertyregistration_Encroached').val('');
        $("#Propertyregistration_Encroached").attr("disabled", "disabled");
        $('#Propertyregistration_BuiltupEncroachmentArea').val('');
        $("#Propertyregistration_BuiltupEncroachmentArea").attr("disabled", "disabled");
    }
    else {
        $('#Propertyregistration_EncraochmentDetails').val('');
        $("#Propertyregistration_EncraochmentDetails").removeAttr("disabled", "disabled");
        $('#Propertyregistration_Encroached').val('');
        $("#Propertyregistration_Encroached").attr("disabled", "disabled");
        $("#Propertyregistration_Encroached").removeAttr("disabled", "disabled");
        $('#Propertyregistration_BuiltupEncroachmentArea').val('');
        $("#Propertyregistration_BuiltupEncroachmentArea").attr("disabled", "disabled");
        $("#Propertyregistration_BuiltupEncroachmentArea").removeAttr("disabled", "disabled");
    }
});
function change() {

    var value = $('#Propertyregistration_EncroachmentStatusId2 option:selected').val();
    if (value == 0) {
        $('#divEncroachmentYesSelection2').hide();
        $('#Propertyregistration_EncraochmentDetails2').val('');
        $("#Propertyregistration_EncraochmentDetails2").attr("disabled", "disabled");
        $('#Propertyregistration_Encroached2').val('');
        $("#Propertyregistration_Encroached2").attr("disabled", "disabled");
        $('#Propertyregistration_BuiltupEncroachmentArea2').val('');
        $("#Propertyregistration_BuiltupEncroachmentArea2").attr("disabled", "disabled");
    }
    else {
        $('#divEncroachmentYesSelection2').show();
        $('#Propertyregistration_EncraochmentDetails2').val('');
        $("#Propertyregistration_EncraochmentDetails2").removeAttr("disabled", "disabled");
        $('#Propertyregistration_Encroached2').val('');
        $("#Propertyregistration_Encroached2").attr("disabled", "disabled");
        $("#Propertyregistration_Encroached2").removeAttr("disabled", "disabled");
        $('#Propertyregistration_BuiltupEncroachmentArea2').val('');
        $("#Propertyregistration_BuiltupEncroachmentArea2").attr("disabled", "disabled");
        $("#Propertyregistration_BuiltupEncroachmentArea2").removeAttr("disabled", "disabled");
    }
};




$('#Propertyregistration_PlannedUnplannedLand').change(function () {
    var value = $('#Propertyregistration_PlannedUnplannedLand').val();
    if (value == 'Planned Land') {
        $('#divPlannedLand').show();
    }
    else {
        $('#divPlannedLand').hide();
    }
});
    