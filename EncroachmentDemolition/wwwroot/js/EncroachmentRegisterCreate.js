function onChangeDepartment(id) {
    HttpGet(`/EncroachmentRegister/GetZoneList/?DepartmentId=${id}`, 'json', function (response) {
        var html = '<option value="">Select</option>';
        for (var i = 0; i < response.length; i++) {
            html = html + '<option value=' + response[i].id + '>' + response[i].name + '</option>';
        }
        $("#ZoneId").html(html);
        $("#DivisionId").html('<option value="">Select</option>');
        $("#LocalityId").html('<option value="">Select</option>');
    });
};
function onChangeZone(id) {
    HttpGet(`/EncroachmentRegister/GetDivisionList/?ZoneId=${id}`, 'json', function (response) {
        var html = '<option value="">Select</option>';
        for (var i = 0; i < response.length; i++) {
            html = html + '<option value=' + response[i].id + '>' + response[i].name + '</option>';
        }
        $("#DivisionId").html(html);
        $("#LocalityId").html('<option value="">Select</option>');
    });
};
function onChangeDivision(id) {
    HttpGet(`/EncroachmentRegister/GetLocalityList/?DivisionId=${id}`, 'json', function (response) {
        var html = '<option value="">Select</option>';
        for (var i = 0; i < response.length; i++) {
            html = html + '<option value=' + response[i].id + '>' + response[i].name + '</option>';
        }
        $("#LocalityId").html(html);
    });
};
$(function () {
    $("#tbl_posts #tbl_posts_body .odd").remove();
    $("#tbl_posts #add .form-control").attr("multiple", false);
    $("input[name='grpPossession']").click(function () {
        if ($("#rdbFormal").is(":checked")) {
            $("#divDepartment").show();
        } else {
            $("#divDepartment").hide();
        }
    });
});
$(document).delegate('a.add-record', 'click', function (e) {
    debugger
    if ($("#tbl_posts #add #NameOfStructure").val() != '' && $("#tbl_posts #add #AreaApprox").val() != '' && $("#tbl_posts #add #Type").val() != ''
        && $("#tbl_posts #add #DateOfEncroachment").val() != '' && $("#tbl_posts #add #ReferenceNoOnLocation").val() != '' && $("#tbl_posts #add #CountOfStructure").val() != ''
      ) {
        e.preventDefault();
        var content = jQuery('#tbl_posts #add tr'),
            size = jQuery('#tbl_posts >tbody >tr').length,
            element = null,
            element = content.clone();
        element.attr('id', 'rec-' + size);
        element.find('.delete-record').attr('data-id', size);
        element.appendTo('#tbl_posts_body');
        element.find('.sn').html(size);
        $("#tbl_posts #add .sn").text($('#tbl_posts >tbody >tr').length);
        $("#tbl_posts #add .add").remove();
        $("#tbl_posts #tbl_posts_body .form-control").attr("readonly", true);
        element.find(".add-record").hide();
        element.find(".delete-record").show();
        debugger
        $("#tbl_posts #add .form-control").val('');
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