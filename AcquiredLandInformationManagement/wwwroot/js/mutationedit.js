$(document).ready(function () {

    var id = parseInt($('#KhasraId option:selected').val());
    GetOtherDetails(id);
    FillRepeatorAtEdit();


});

function FillRepeatorAtEdit() {/* -----------Added by Renu  --------------- */

    /* -----------Mutation Particulars Repeator Added by Renu  --------------- */
    HttpGet(`/Mutation/GetDetailsMutationParticulars/?Id=${$("#Id").val() == null ? "" : $("#Id").val()}`, 'json', function (data) {
        debugger
        for (var i = 0; i < data.length; i++) {
            $("#tbl_posts #add #Name").val(data[i].name);
            $("#tbl_posts #add #FatherName").val(data[i].fatherName);
            $("#tbl_posts #add #Address").val(data[i].address);
            $("#tbl_posts #add #Share").val(data[i].share);
            if (i < data.length - 1) {
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
                $("#tbl_posts #tbl_posts_body .floating-label-field").attr("readonly", true);
                element.find(".add-record").hide();
                element.find(".delete-record").show();
            }
        }
    });

}

function GetKhasraList(id) {
    debugger;
    HttpGet(`/DemandListDetails/GetKhasraList/?Id=${id}`, 'json', function (response) {
        $("#KhasraNoId").val('').trigger('change');
        var html = '<option value="">---Select---</option>';
        for (var i = 0; i < response.length; i++) {
            html = html + '<option value=' + response[i].id + '>' + response[i].name + '</option>';
        }
        $("#KhasraNoId").html(html);
    });
};


//****************** Mutation Particulars Repeator ************************

$(document).delegate('a.add-record', 'click', function (e) {
    debugger

    if ($("#tbl_posts #add #Name").val() != ''
        // && $("#tbl_posts #add #FatherName").val() != ''
        && $("#tbl_posts #add #Address").val() != ''
        //  && $("#tbl_posts #add #Share").val() != ''

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
            $(this).find('span.sn').html(index + 1);
        });
        $("#tbl_posts #add .sn").text($('#tbl_posts >tbody >tr').length);
        return true;
    } else {
        return false;
    }
});

function GetOtherDetails(id) {
    HttpGet(`/Mutation/KhasraView/?Id=${id}`, 'html', function (response) {
        $('#KhasraDetailsDiv').html("");
        $('#KhasraDetailsDiv').html(response);
    });
};


$("#collapse").click(function () {
    $('#collapseExample').collapse("toggle").promise().done(function () {
        $("select").select2({
            placeholder: "Select",
            allowClear: true
        });
    })
});
