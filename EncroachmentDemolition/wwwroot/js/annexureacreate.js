$(document).ready(function () {

  
    var watchWardId = parseInt($('#Encroachment_WatchWardId').val());
    GetWatchWardDetails(watchWardId);


});

function GetWatchWardDetails(id) {
    HttpGet(`/EncroachmentRegisterApproval/WatchWardView/?Id=${id}`, 'html', function (response) {
        $('#WatchWardDetailsDiv').html("");
        $('#WatchWardDetailsDiv').html(response);
    });
};

function callSelect2() {
    $("select").select2({
        placeholder: "Select",
        allowClear: true
    });
}

$("#collapse").click(function () {
    $('#collapseApprroval').collapse("toggle").promise().done(function () {
        $("select").select2({
            placeholder: "Select",
            allowClear: true
        });
    });
});


$("#collapse").click(function () {
    $("#collapseWatchWardApprroval").collapse("toggle").promise().done(function () {
        $('#select').select2({
            placeholder: "Select",
            allowClear: true
        });
    });
});

$(function () {
    $("#tbl_posts #tbl_posts_body .odd").remove();
    $("#tbl_posts #add .floating-label-field").attr("multiple", false);
    if ($('#StatusOfLand option:selected').val() == 'DDA Land') {
        $("#divForLandStatus").show();
    }
    else {
        $("#divForLandStatus").hide();
    }
    if ($('#IsPossession option:selected').val() == 'Yes') {
        $("#divPossessionGroup").show();
    }
    else {
        $("#divPossessionGroup").hide();
    }
    $("input[name='grpPossession']").click(function () {
        if ($("#rdbFormal").is(":checked")) {
            $("#divDepartment").show();
            $("#PossessionType").val("Formal Possession");
        } else {
            $("#divDepartment").hide();
            $("#PossessionType").val("Physical Possession");
        }
    });
    HttpGet(`/EncroachmentRegister/DetailsOfRepeater/?Id=${$("#hdnId").val() == null ? "" : $("#hdnId").val()}`, 'json', function (data) {
        debugger
        for (var i = 0; i < data.length; i++) {
            $("#tbl_posts #add #NameOfStructure").val(data[i].nameOfStructure);
            $("#tbl_posts #add #AreaApprox").val(data[i].area);
            $("#tbl_posts #add #Type").val(data[i].type);
            $("#tbl_posts #add #ReligiousStructure").val(data[i].religiousStructure);
            $("#tbl_posts #add #DateOfEncroachment").val(data[i].dateOfEncroachment);
            $("#tbl_posts #add #ReferenceNoOnLocation").val(data[i].referenceNoOnLocation);
            $("#tbl_posts #add #CountOfStructure").val(data[i].countOfStructure);
            $("#tbl_posts #add #ConstructionStatus").val(data[i].constructionStatus);
            $('#tbl_posts #add #ConstructionStatus').trigger('change');
            $('#tbl_posts #add #ReligiousStructure').trigger('change');
            if (i < data.length - 1) {
                var ConstructionStatus = $("#tbl_posts #add #ConstructionStatus").children("option:selected").val();
                var ReligiousStructure = $("#tbl_posts #add #ReligiousStructure").children("option:selected").val();
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
            }
        }
    });
});