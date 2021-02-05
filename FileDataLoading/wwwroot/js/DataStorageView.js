$(document).delegate('a.add-record', 'click', function (e) {
    debugger
    if ($("#tbl_posts #add #Category").val() != '' && $("#tbl_posts #add #Header").val() != '' && $("#tbl_posts #add #SequenceNo").val() != ''
        && $("#tbl_posts #add #Year").val() != '' && $("#tbl_posts #add #Subject").val() != ''
    ) {
        var SchemeId = $("#tbl_posts #add #SchemeDptBranch").children("option:selected").val();
        var LocalityId = $("#tbl_posts #add #LocalityIdForPartFile").children("option:selected").val();
        e.preventDefault();
        var content = jQuery('#tbl_posts #add tr'),
            size = jQuery('#tbl_posts >tbody >tr').length,
            element = null,
            element = content.clone();
        element.attr('id', 'rec-' + size);
        element.find('.delete-record').attr('data-id', size);
        element.appendTo('#tbl_posts_body');
        $('#tbl_posts_body #rec-' + size + ' #SchemeDptBranch').val(SchemeId);
        $('#tbl_posts_body #rec-' + size + ' #LocalityIdForPartFile').val(LocalityId);
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

if ($("#rSubsequent").is(":checked")) {
    $("#DivForSubsequentPurchaser").hide();
    $("#IsPartOfMainFile").val("Subsequent");
} else {
    $("#DivForSubsequentPurchaser").show();
    $("#IsPartOfMainFile").val("Original");
}


$("input[name='grpDamageAssesseeType']").click(function () {
    if ($("#rSubsequent").is(":checked")) {
        $("#TypeOfDamageAssessee").val('0');
        $("#DivForSubsequentPurchaser").hide();
        callSelect2();
    } else {
        $("#TypeOfDamageAssessee").val('1');
        $("#DivForSubsequentPurchaser").show();
        callSelect2();
    }
});

$(document).ready(function () {
    HttpGet(`/DataStorageDetails/DetailsOfRepeater/?Id=${$("#Id").val() == null ? "" : $("#Id").val()}`, 'json', function (data) {
        debugger
        console.log(data);
        for (var i = 0; i < data.length; i++) {
            $("#tbl_posts #add #Category").val(data[i].category);
            $("#tbl_posts #add #Header").val(data[i].header);
            $("#tbl_posts #add #SequenceNoForPartFile").val(data[i].sequenceNo);
            $("#tbl_posts #add #YearForPartFile ").val(data[i].yearofPartFile);
            $("#tbl_posts #add #LocalityIdForPartFile").val(data[i].localityId);
            $("#tbl_posts #add #Subject").val(data[i].subject);
            $("#tbl_posts #add #SchemeDptBranch").val(data[i].schemeDptBranch);
            $('#tbl_posts #add #LocalityIdForPartFile').trigger('change');
            $('#tbl_posts #add #SchemeDptBranch').trigger('change');
            if (i < data.length - 1) {
                var LocalityIdForPartFile = $("#tbl_posts #add #LocalityIdForPartFile").children("option:selected").val();
                var SchemeDptBranch = $("#tbl_posts #add #SchemeDptBranch").children("option:selected").val();
                var content = jQuery('#tbl_posts #add tr'),
                    size = jQuery('#tbl_posts >tbody >tr').length,
                    element = null,
                    element = content.clone();
                element.attr('id', 'rec-' + size);
                element.find('.delete-record').attr('data-id', size);
                element.appendTo('#tbl_posts_body');
                $('#tbl_posts_body #rec-' + size + ' #LocalityIdForPartFile').val(LocalityIdForPartFile);
                $('#tbl_posts_body #rec-' + size + ' #SchemeDptBranch').val(schemeDptBranch);
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

function callSelect2() {
    $("select").select2({
        placeholder: "Select",
        allowClear: true
    });
}