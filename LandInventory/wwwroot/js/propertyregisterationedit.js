
$(document).ready(function () {
    $("#TotalArea").attr("readonly", "readonly");

    var value = $('#Boundary option:selected').val();
    if (value == 2) {
        $('#divBoundarySelection').hide();
    }
    else {
        $('#divBoundarySelection').show();
    }


    var value = $('#PlannedUnplannedLand').val();
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

    var value = $('#EncroachmentStatusId option:selected').val();
    if (value == 1) {
        $('#divEncroachmentYesSelection').show();
        $('#divEncroachmentDetails').show();
        $("select").select2({
            placeholder: "Select",
            allowClear: true
        });
        //$("#EncraochmentDetails").attr("disabled", "disabled");
        //$("#EncraochmentDetails").removeAttr("disabled", "disabled");
        //$("#Encroached").attr("disabled", "disabled");
        //$("#Encroached").removeAttr("disabled", "disabled");
        //$("#BuiltupEncroachmentArea").attr("disabled", "disabled");
        //$("#BuiltupEncroachmentArea").removeAttr("disabled", "disabled");
    }
    else {
        $('#divEncroachmentYesSelection').hide();
        $('#divEncroachmentDetails').hide();
        //$("#EncraochmentDetails").attr("disabled", "disabled");
        //$("#Encroached").attr("disabled", "disabled");
        //$("#BuiltupEncroachmentArea").attr("disabled", "disabled");
    }

    var value = $('#BuiltUp option:selected').val();
    if (value == 1) {
        $("#divBuilttype").show();
    }
    else {
        $("#divBuilttype").hide();
    }

    var value = $('#LitigationStatus option:selected').val();
    if (value == 1) {
        $("#divLitigationStatusSelection").show();
    }
    else {
        $('#divLitigationStatusSelection').hide();
    }


    var value = $('#GeoReferencing option:selected').val();
    if (value == 1) {
        $("#divGEOReferencingSelection").show();
    }
    else {
        $('#divGEOReferencingSelection').hide();
    }

    ///*characters Calculation for Disposal comments */
    //var text_max = 4000;
    //$('#feedbackDisposalComments').html(text_max + ' characters remaining');
    //$('#DisposalComments').keyup(function () {
    //    var text_length = $('#DisposalComments').val().length;
    //    var text_remaining = text_max - text_length;
    //    $('#feedbackDisposalComments').html(text_remaining + ' characters remaining');
    //});

    ///*characters Calculation for Remarks */
    //$('#feedbackRemarks').html(text_max + ' characters remaining');
    //$('#Remarks').keyup(function () {
    //    var text_length = $('#Remarks').val().length;
    //    var text_remaining = text_max - text_length;
    //    $('#feedbackRemarks').html(text_remaining + ' characters remaining');
    //});

    ///*characters Calculation for Plot Not. */
    //$('#feedbackPalandmark').html(text_max + ' characters remaining');
    //$('#Palandmark').keyup(function () {
    //    var text_length = $('#Palandmark').val().length;
    //    var text_remaining = text_max - text_length;
    //    $('#feedbackPalandmark').html(text_remaining + ' characters remaining');
    //});

    ///*characters Calculation for TakenOverComments */
    //$('#feedbackTakenOverComments').html(text_max + ' characters remaining');
    //$('#TakenOverComments').keyup(function () {
    //    var text_length = $('#TakenOverComments').val().length;
    //    var text_remaining = text_max - text_length;
    //    $('#feedbackTakenOverComments').html(text_remaining + ' characters remaining');
    //});

    ///*characters Calculation for TakenOverComments */
    //$('#feedbackHandedOverComments').html(text_max + ' characters remaining');
    //$('#HandedOverComments').keyup(function () {
    //    var text_length = $('#HandedOverComments').val().length;
    //    var text_remaining = text_max - text_length;
    //    $('#feedbackHandedOverComments').html(text_remaining + ' characters remaining');
    //});

    var value = $('#AreaUnit option:selected').val();
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
        else if (value == 4)
            $('#LabelTotalAreaSqAcreHec').html('Total Area(' + "Sq. Mt." + ')');
    }

    $(".bbbcalculation").keyup(function () {
        var inbigha = $('#TotalAreaInBigha').val();
        var inbiswa = $('#TotalAreaInBiswa').val();
        var inbiswani = $('#TotalAreaInBiswani').val();

        var inbighavalue = parseFloat(inbigha == '' ? 0 : inbigha) * 1621.344;
        var inbiswavalue = parseFloat(inbiswa == '' ? 0 : inbiswa) * 32408.640;
        var inbiswanivalue = parseFloat(inbiswani == '' ? 0 : inbiswani) * 6.323;

        var totalarea = inbighavalue + inbiswavalue + inbiswanivalue;

        $("input[name='TotalArea']").val(totalarea.toFixed(3));
    });

    $(".TotalCalculation").keyup(function () {
        debugger;
        var value = $('#AreaUnit option:selected').val();
        var totalOther = $('#TotalAreaInSqAcreHt').val();
        if (value == 1) {
            $("input[name='TotalArea']").val((parseFloat(totalOther == '' ? 0 : totalOther) * 0.836).toFixed(3));
        }
        else if (value == 2) {
            $("input[name='TotalArea']").val((parseFloat(totalOther == '' ? 0 : totalOther) * 0.09).toFixed(3));
        }
        else if (value == 3) {
            $("input[name='TotalArea']").val((parseFloat(totalOther == '' ? 0 : totalOther) * 10098.156).toFixed(3));
        }
        else if (value == 4) {
            $("input[name='TotalArea']").val((parseFloat(totalOther == '' ? 0 : totalOther)).toFixed(3));
        }
    });

    // DropDrown check at initial 
    //var departmentid = $('#DepartmentId option:selected').val();
    //if (departmentid > 0) {
    //    debugger;
    //    GetZoneList(departmentid);
    //}


})


$('#Boundary').change(function () {
    debugger;
    var value = $('#Boundary option:selected').val();
    if (value == 2) {
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



$('#EncroachmentStatusId').change(function () {
    var value = $('#EncroachmentStatusId option:selected').val();
    if (value == 0) {
        $('#divEncroachmentYesSelection').hide();
        $('#divEncroachmentDetails').hide();
        //$('#EncraochmentDetails').val('');
        //$("#EncraochmentDetails").attr("disabled", "disabled");
        //$('#Encroached').val('');
        //$("#Encroached").attr("disabled", "disabled");
        //$('#BuiltupEncroachmentArea').val('');
        //$("#BuiltupEncroachmentArea").attr("disabled", "disabled");
    }
    else {
        $('#divEncroachmentYesSelection').show();
        $('#divEncroachmentDetails').show();
        callSelect2();
        //$('#EncraochmentDetails').val('');
        //$("#EncraochmentDetails").removeAttr("disabled", "disabled");
        //$('#Encroached').val('');
        //$("#Encroached").attr("disabled", "disabled");
        //$("#Encroached").removeAttr("disabled", "disabled");
        //$('#BuiltupEncroachmentArea').val('');
        //$("#BuiltupEncroachmentArea").attr("disabled", "disabled");
        //$("#BuiltupEncroachmentArea").removeAttr("disabled", "disabled");
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


//Validations For Page Added by Renu 19 Sep 2020


$('#myForm').validate({
    rules: {
        TakenOverEmailId: {
            minlength: 3,
            maxlength: 255,
            //required: true,
            email: true
        },

        HandedOverEmailId: {
            minlength: 3,
            maxlength: 255,
            email: true
        },

        ClassificationOfLandId: {
            required: true
        },
        DepartmentId: {
            required: true
        },
        ZoneId: {
            required: true
        },
        DivisionId: {
            required: true
        },
        LocalityId: {
            required: true
        },
        InventoriedInId: {
            required: true
        },
        TotalArea: {
            required: true
        }
    },

    messages: {
        ClassificationOfLandId: {
            required: ClassificationOfLandIdMessage //this is a function that returns custom messages
        },
        DepartmentId: {
            required: DepartmentIdMessage //this is a function that returns custom messages
        },
        ZoneId: {
            required: ZoneIdMessage //this is a function that returns custom messages
        },
        DivisionId: {
            required: DivisionIdMessage //this is a function that returns custom messages
        },
        LocalityId: {
            required: LocalityIdMessage //this is a function that returns custom messages
        },
        InventoriedInId: {
            required: InventoriedInIdMessage //this is a function that returns custom messages
        },
        TotalArea: {
            required: TotalAreaMessage //this is a function that returns custom messages
        }
    },
    highlight: function (element) {
        $(element).closest('.form-group').addClass('has-error');
    },
    unhighlight: function (element) {
        $(element).closest('.form-group').removeClass('has-error');
    },
    errorElement: 'span',
    errorClass: 'help-block',
    errorPlacement: function (error, element) {
        if (element.parent('.input-group').length) {
            error.insertAfter(element.parent());
        } else {
            error.insertAfter(element);
        }
    },
    submitHandler: function (form) {
        // alert('Form validated and submitted ok.');
        return true;
    }
});



//For Drop down
function ClassificationOfLandIdMessage() {
    var dropdown_val = $('#ClassificationOfLandId option:selected').val();
    if (dropdown_val < 1) {
        return "Classification Of Land is Mandatory";
    } else {
        return "";
    }
}

function DepartmentIdMessage() {
    var dropdown_val = $('#DepartmentId option:selected').val();
    if (dropdown_val < 1) {
        return "Department is Mandatory";
    } else {
        return "";
    }
}

function ZoneIdMessage() {
    var dropdown_val = $('#ZoneId option:selected').val();
    if (dropdown_val < 1) {
        return "Zone is Mandatory";
    } else {
        return "";
    }
}

function DivisionIdMessage() {
    var dropdown_val = $('#DivisionId option:selected').val();
    if (dropdown_val < 1) {
        return "Division is Mandatory";
    } else {
        return "";
    }
}

function LocalityIdMessage() {
    var dropdown_val = $('#LocalityId option:selected').val();
    if (dropdown_val < 1) {
        return "Locality is Mandatory";
    } else {
        return "";
    }
}

function InventoriedInIdMessage() {
    var dropdown_val = $('#InventoriedInId').val();
    if (dropdown_val == "") {
        return "Inventoried In is Mandatory";
    } else {
        return "";
    }
}


//For Textbox
function TotalAreaMessage() {
    var dropdown_val = $('#TotalArea').val();
    if (dropdown_val == "") {
        return "Total Area is Mandatory";
    } else {
        return "";
    }
}

//$(function () {
//    $("#HandedOverDate").datepicker();
//});

//Bind Zone Dropdown from Department
function GetZoneList(id) {
    debugger;
    HttpGet(`/PropertyRegistration/GetZoneList/?departmentId=${id}`, 'json', function (response) {
        $("#ZoneId").val('').trigger('change');
        var html = '<option value="">---Select---</option>';
        for (var i = 0; i < response.length; i++) {
            html = html + '<option value=' + response[i].id + '>' + response[i].name + '</option>';
        }
        $("#ZoneId").html(html);
        $("#DivisionId").val('').trigger('change');
        $("#LocalityId").val('').trigger('change');
    });
};

//Bind Divison and Locality Dropdown from Department
function GetDivisionList(id) {

    HttpGet(`/PropertyRegistration/GetDivisionList/?zoneId=${id}`, 'json', function (response) {
        var html = '<option value="">---Select---</option>';
        for (var i = 0; i < response.length; i++) {
            html = html + '<option value=' + response[i].id + '>' + response[i].name + '</option>';
        }
        $("#DivisionId").val('').trigger('change');
        $("#DivisionId").html(html);
    });

    HttpGet(`/PropertyRegistration/GetLocalityList/?zoneId=${id}`, 'json', function (response) {
        var html = '<option value="">---Select---</option>';
        for (var i = 0; i < response.length; i++) {
            html = html + '<option value=' + response[i].id + '>' + response[i].name + '</option>';
        }
        $("#LocalityId").val('').trigger('change');
        $("#LocalityId").html(html);
    });
};


$('#DisposalTypeAssignFile').change(function () {
    debugger;
    var fileInput = document.getElementById('DisposalTypeAssignFile');
    var filePath = fileInput.value;
    const size = (DisposalTypeAssignFile.files[0].size);
    fileValidation(filePath, fileInput, size);
});



$('#Assignfile').change(function () {
    var fileInput = document.getElementById('Assignfile');
    var filePath = fileInput.value;
    const size = (Assignfile.files[0].size);
    fileValidation(filePath, fileInput, size);
});


$('#GeoAssignFile').change(function () {
    var fileInput = document.getElementById('GeoAssignFile');
    var filePath = fileInput.value;
    const size = (GeoAssignFile.files[0].size);
    fileValidation(filePath, fileInput, size);
});


$('#TakenOverAssignFile').change(function () {
    var fileInput = document.getElementById('TakenOverAssignFile');
    var filePath = fileInput.value;
    const size = (TakenOverAssignFile.files[0].size);
    fileValidation(filePath, fileInput, size);
});


$('#HandedOverAssignFile').change(function () {
    var fileInput = document.getElementById('HandedOverAssignFile');
    var filePath = fileInput.value;
    const size = (HandedOverAssignFile.files[0].size);
    fileValidation(filePath, fileInput, size);
});

$('#HandedOverCopyofOrderDoc').change(function () {
    var fileInput = document.getElementById('HandedOverCopyofOrderDoc');
    var filePath = fileInput.value;
    const size = (HandedOverCopyofOrderDoc.files[0].size);
    fileValidation(filePath, fileInput, size);
});


$('#EncroachAtrDoc').change(function () {
    var fileInput = document.getElementById('EncroachAtrDoc');
    var filePath = fileInput.value;
    const size = (EncroachAtrDoc.files[0].size);
    fileValidation(filePath, fileInput, size);
});


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


//$(function () {
//    $('#Encroached').change(function () {
//        debugger;
//        var value = $('#Encroached').val();
//        var value1 = $('#Encroached').val();
//        if (parseFloat(value) > parseFloat(value1) || value != value1  ) {
//            $("#EncroachedValidation").val('');
//            $("#EncroachedValidation").hide();
//            return true;
//        }
//        else {
//            $("#EncroachedValidation").val('Total Area and Encroached Area must be ');
//            $("#EncroachedValidation").show();
//            return false;
//        }
//    });
//});


function ComapareTotalArea(element) {
    debugger;
    var isFormValid = true;
    var name = element.name;
    var FieldId = "span_" + name;
    var ValidationMsg = 'Value must be lesser than or equal to Total Area';
    var totalArea = $("input[name='TotalArea']").val();
    var value = $("input[name='" + name + "']").val();
    if ((parseFloat(value == '' ? '0' : value)) > (parseFloat(totalArea == '' ? '0' : totalArea))) {
        $("<span class='text-danger lb-sm' id='" + FieldId + "'>" + ValidationMsg + "</span>").insertAfter($(this).parent().closest('div').find("span[class='text-danger lb-sm field-validation-valid']"))
        $(this).parent().closest('div').addClass('has-error');
        isFormValid = false;
    }
    if ($("#" + FieldId).css('display') == 'none') {
        $("#" + FieldId).fadeIn(250);
    }

    return isFormValid;
}

function callSelect2() {
    $("select").select2({
        placeholder: "Select",
        allowClear: true
    });
}


$('#AreaUnit').change(function () {
    debugger;
    var value = $('#AreaUnit option:selected').val();
    if (value == 0) {
        $('#TotalAreaInSqAcreHt').val('');
        $('#TotalAreaInBigha').val('');
        $('#TotalAreaInBiswa').val('');
        $('#TotalAreaInBiswani').val('');
        $("#AreainSqAcreHec").hide();
        $("#bighabis").show();
    }
    else {
        $('#TotalAreaInSqAcreHt').val('');
        $('#TotalAreaInBigha').val('');
        $('#TotalAreaInBiswa').val('');
        $('#TotalAreaInBiswani').val('');
        $("#AreainSqAcreHec").show();
        $("#bighabis").hide();
        if (value == 1)
            $('#LabelTotalAreaSqAcreHec').html('Total Area(' + "Sq Yd." + ')');
        else if (value == 2)
            $('#LabelTotalAreaSqAcreHec').html('Total Area(' + "Acre" + ')');
        else if (value == 3)
            $('#LabelTotalAreaSqAcreHec').html('Total Area(' + "Hectare" + ')');
    }
});

//Bind Divison  Dropdown from Department for Taken Over
function GetTakenOverZoneList(id) {
    debugger;
    HttpGet(`/PropertyRegistration/GetZoneList/?departmentId=${id}`, 'json', function (response) {
        $("#TakenOverZone").val('').trigger('change');
        var html = '<option value="">---Select---</option>';
        for (var i = 0; i < response.length; i++) {
            html = html + '<option value=' + response[i].id + '>' + response[i].name + '</option>';
        }
        $("#TakenOverZone").html(html);
        $("#TakenOverDivision").val('').trigger('change'); GetTakenOverDivisionList
    });
};

//Bind Divison  Dropdown from Zone for Taken Over
function GetTakenOverDivisionList(id) {

    HttpGet(`/PropertyRegistration/GetDivisionList/?zoneId=${id}`, 'json', function (response) {
        var html = '<option value="">---Select---</option>';
        for (var i = 0; i < response.length; i++) {
            html = html + '<option value=' + response[i].id + '>' + response[i].name + '</option>';
        }
        $("#TakenOverDivision").val('').trigger('change');
        $("#TakenOverDivision").html(html);
    });
};

//Bind Divison  Dropdown from Department for Handed Over
function GetHandedOverZoneList(id) {
    debugger;
    HttpGet(`/PropertyRegistration/GetZoneList/?departmentId=${id}`, 'json', function (response) {
        $("#HandedOverZone").val('').trigger('change');
        var html = '<option value="">---Select---</option>';
        for (var i = 0; i < response.length; i++) {
            html = html + '<option value=' + response[i].id + '>' + response[i].name + '</option>';
        }
        $("#HandedOverZone").html(html);
        $("#HandedOverDivision").val('').trigger('change'); GetTakenOverDivisionList
    });
};

//Bind Divison  Dropdown from Zone for Handed Over
function GetHandedOverDivisionList(id) {

    HttpGet(`/PropertyRegistration/GetDivisionList/?zoneId=${id}`, 'json', function (response) {
        var html = '<option value="">---Select---</option>';
        for (var i = 0; i < response.length; i++) {
            html = html + '<option value=' + response[i].id + '>' + response[i].name + '</option>';
        }
        $("#HandedOverDivision").val('').trigger('change');
        $("#HandedOverDivision").html(html);
    });
};




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
                    url: "/PropertyRegistration/CheckFile",
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

function validateForm() {

    let localityid = document.forms["propert-form"]["LocalityId"].value;
    let PlannedUnplannedLand = document.forms["propert-form"]["PlannedUnplannedLand"].value;
    //  alert(PlannedUnplannedLand);
    //  let Letter = document.forms["kyc-form"]["Letter"].value;
    if (PlannedUnplannedLand == "Unplanned Land") {
        var status = 1;
        if (localityid == "") {

            $("#Locality-error").show();
            status = 2;
            // return false;
        }

        if (status == 2) {
            return false;
        }
    }


}



