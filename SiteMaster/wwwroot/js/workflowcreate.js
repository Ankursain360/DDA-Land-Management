// Repeator code 
$(document).ready(function () {
    $("#tbl_posts #tbl_posts_body .odd").remove();
    $("#tbl_posts #add .form-control").attr("multiple", false);
})
$(document).delegate('a.add-record', 'click', function (e) {
    var name = $("#tbl_posts #add #parameterName").val();
    var value = $("#tbl_posts #add #parameterValue").val();
    var level = $("#tbl_posts #add #parameterLevel").val();
    debugger
    if ($("#tbl_posts #add #parameterName").val() != '' && $("#tbl_posts #add #parameterValue").val() != '' && $("#tbl_posts #add #parameterLevel").val() != ''
        //&& $("#tbl_posts #add #drpPersonalGender").val() != '' && $("#tbl_posts #add #txtPersonalAddress").val() != ''
        //&& $("#tbl_posts #add #txtPersonalMobileNo").val() != '' && $("#tbl_posts #add #txtPersonalEmailid").val() != ''
    ) {
        //var GenderValue = $("#tbl_posts #add #drpPersonalGender").children("option:selected").val();
        //var father = $("#tbl_posts #add #txtPersonalFatherName").children("option:selected").val();
        //var gender = $("#tbl_posts #add #drpPersonalGender").children("option:selected").val();
        debugger
        e.preventDefault();
        var content = jQuery('#tbl_posts #add tr'),
            size = jQuery('#tbl_posts >tbody>tr').length,
            element = null,
            element = content.clone();
        element.attr('id', 'rec-' + size);
        element.find('.delete-record').attr('data-id', size);
        element.appendTo('#tbl_posts_body');
        //$('#tbl_posts_body #rec-' + size + ' #drpPersonalGender').val(GenderValue);
        //$('#tbl_posts_body #rec-' + size + ' #txtPersonalFatherName').val(father);
        //$('#tbl_posts_body #rec-' + size + ' #drpPersonalGender').val(gender);
        element.find('.sn').html('Task '+ size);
        $("#tbl_posts #add .sn").text('Task ' +$('#tbl_posts >tbody >tr').length);
        $("#tbl_posts #add .add").remove();
        $("#tbl_posts #tbl_posts_body .form-control").attr("readonly", true);
        //element.find(".add-record").hide();
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
            var index = index + 1;
            $(this).find('span.sn').html('Task '+index);
        });
        $("#tbl_posts #add .sn").text('Task ' +$('#tbl_posts >tbody >tr').length);
        return true;
    } else {
        return false;
    }
});