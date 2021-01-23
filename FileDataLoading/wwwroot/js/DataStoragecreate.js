$(document).ready(function () {
    $('#SchemeDptBranch').removeAttr('multiple');
    $('#LocalityIdForPartFile').removeAttr('multiple');
    $("input[name='grpSearchby']").click(function () {
        if ($("#rdbDocumentname").is(":checked")) {
            $("#IsFileDocument").val("1");
        } else {
            $("#IsFileDocument").val("0");
        }
    });
});
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