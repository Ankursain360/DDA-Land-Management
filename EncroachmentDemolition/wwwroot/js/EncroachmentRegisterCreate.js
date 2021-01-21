$(document).ready(function () {

    var id = parseInt($('#WatchWardId').val());
    GetOtherDetails(id);
});

function GetOtherDetails(id) {
    HttpGet(`/EncroachmentRegister/WatchWardView/?Id=${id}`, 'html', function (response) {
        $('#WatchWardDetailsDiv').html("");
        $('#WatchWardDetailsDiv').html(response);
    });
};

function onChangeLandStatus(status) {
    if (status == 'DDA Land') {
        $("#divForLandStatus").show();
    }
    else {
        $("#divForLandStatus").hide();
    }
};
function onChangePossationStatus(status) {
    if (status == 'Yes') {
        $("#divPossessionGroup").show();
    }
    else {
        $("#divPossessionGroup").hide();
    }
};
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


    HttpGet(`/EncroachmentRegister/GetLocalityList/?DivisionId=${id}`, 'json', function (response) {
        var html = '<option value="">Select</option>';
        for (var i = 0; i < response.length; i++) {
            html = html + '<option value=' + response[i].id + '>' + response[i].name + '</option>';
        }
        $(".LocalitiyClass").html(html);
        $(".LocalitiyClass").select2('val', '')
    });
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
        var ConstructionStatus// = $("#tbl_posts #add #ConstructionStatus").children("option:selected").val();
        var ReligiousStructure //= $("#tbl_posts #add #ReligiousStructure").children("option:selected").val();
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