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


function GetOtherDetails(id) {
    HttpGet(`/Mutation/KhasraView/?Id=${id}`, 'html', function (response) {
        $('#KhasraDetailsDiv').html("");
        $('#KhasraDetailsDiv').html(response);
    });
    //});
};


$("#collapse").click(function () {
    debugger;
    $('#collapseExample').collapse("toggle").promise().done(function () {
        $("select").select2({
            placeholder: "Select",
            allowClear: true
        });
    })
});
