

$(document).ready(function () {
    FillOwnerAtEdit();
    FillLesseeAtEdit();
    FillTenantAtEdit();
});

function onChange(id) {

    HttpGet(`/SakaniDetails/GetKhasraList/?villageId=${id}`, 'json', function (response) {
        var html = '<option value="0">Select</option>';
        for (var i = 0; i < response.length; i++) {
            html = html + '<option value=' + response[i].id + '>' + response[i].name + '</option>';
        }

        $("#KhasraId").select2('val', '')
        $("#KhasraId").html(html);
    });
};






//****************** code for saving owner Rpt ************************


function FillOwnerAtEdit() {


    HttpGet(`/SakaniDetails/GetDetailsOwner/?Id=${$("#Id").val() == null ? "" : $("#Id").val()}`, 'json', function (data) {
        debugger
        for (var i = 0; i < data.length; i++) {
            $("#tbl_posts #add #OwnerName").val(data[i].ownerName);
            $("#tbl_posts #add #FatherName").val(data[i].fatherName);

            $("#tbl_posts #add #Address").val(data[i].address);
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

$(document).delegate('a.add-record', 'click', function (e) {
    debugger

    if ($("#tbl_posts #add #OwnerName").val() != ''
        && $("#tbl_posts #add #FatherName").val() != ''
        && $("#tbl_posts #add #Address").val() != '') {

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






//****************** code for saving Lessee  details Rpt ************************



function FillLesseeAtEdit() {


    HttpGet(`/SakaniDetails/GetDetailslesssee/?Id=${$("#Id").val() == null ? "" : $("#Id").val()}`, 'json', function (data) {
        debugger
        for (var i = 0; i < data.length; i++) {
            $("#tbl_posts1 #add1 #LesseeName").val(data[i].lesseeName);
            $("#tbl_posts1 #add1 #LFather").val(data[i].fatherName);
            $("#tbl_posts1 #add1 #LAddress").val(data[i].address);
            $("#tbl_posts1 #add1 #LShare").val(data[i].lesseeShare);
            $("#tbl_posts1 #add1 #LMortgage").val(data[i].lesseeMortgage);
            if (i < data.length - 1) {
                var content = jQuery('#tbl_posts1 #add1 tr'),
                    size = jQuery('#tbl_posts1 >tbody >tr').length,
                    element = null,
                    element = content.clone();
                element.attr('id', 'rec-' + size);
                element.find('.delete-record1').attr('data-id', size);
                element.appendTo('#tbl_posts1_body');

                element.find('.sn1').html(size);
                $("#tbl_posts1 #add1 .sn1").text($('#tbl_posts1 >tbody >tr').length);
                $("#tbl_posts1 #add1 .add").remove();
                $("#tbl_posts1 #tbl_posts1_body .floating-label-field").attr("readonly", true);
                element.find(".add-record1").hide();
                element.find(".delete-record1").show();
            }
        }
    });
}

$(document).delegate('a.add-record1', 'click', function (e) {
    debugger

    if ($("#tbl_posts1 #add1 #LesseeName").val() != ''
        && $("#tbl_posts1 #add1 #LFather").val() != ''
        && $("#tbl_posts1 #add1 #LAddress").val() != ''
        && $("#tbl_posts1 #add1 #LShare").val() != ''
        && $("#tbl_posts1 #add1 #LMortgage").val() != '') {

        e.preventDefault();
        var content = jQuery('#tbl_posts1 #add1 tr'),
            size = jQuery('#tbl_posts1 >tbody >tr').length,
            element = null,
            element = content.clone();
        element.attr('id', 'rec-' + size);
        element.find('.delete-record1').attr('data-id', size);
        element.appendTo('#tbl_posts1_body');
        // $('#tbl_posts_body #rec-' + size + ' #Gender').val(Gender);

        element.find('.sn1').html(size);
        $("#tbl_posts1 #add1 .sn1").text($('#tbl_posts1 >tbody >tr').length);
        $("#tbl_posts1 #add1 .add").remove();
        $("#tbl_posts1 #tbl_posts1_body .floating-label-field").attr("readonly", true);
        element.find(".add-record1").hide();
        element.find(".delete-record1").show();
        debugger

        $("#tbl_posts1 #add1 .floating-label-field").val('');
    }
    else {
        alert('Please fill record before add new record ');
    }
});
$(document).delegate('a.delete-record1', 'click', function (e) {
    e.preventDefault();
    var didConfirm = confirm("Are you sure You want to delete");
    if (didConfirm == true) {
        var id = jQuery(this).attr('data-id');
        var targetDiv = jQuery(this).attr('targetDiv');
        jQuery('#rec-' + id).remove();
        //regnerate index number on table
        $('#tbl_posts1_body tr').each(function (index) {
            //alert(index);
            $(this).find('span.sn1').html(index + 1);
        });
        $("#tbl_posts1 #add1 .sn1").text($('#tbl_posts1 >tbody >tr').length);
        return true;
    } else {
        return false;
    }
});





//****************** code for saving tenant  details Rpt ************************

function FillTenantAtEdit() {


    HttpGet(`/SakaniDetails/GetDetailsTenant/?Id=${$("#Id").val() == null ? "" : $("#Id").val()}`, 'json', function (data) {
        debugger
        for (var i = 0; i < data.length; i++) {
            $("#tbl_posts2 #add2 #TName").val(data[i].tenantName);
            $("#tbl_posts2 #add2 #TFatherName").val(data[i].fatherName);
            $("#tbl_posts2 #add2 #TAddress").val(data[i].address);

            if (i < data.length - 1) {
                var content = jQuery('#tbl_posts2 #add2 tr'),
                    size = jQuery('#tbl_posts2 >tbody >tr').length,
                    element = null,
                    element = content.clone();
                element.attr('id', 'rec-' + size);
                element.find('.delete-record2').attr('data-id', size);
                element.appendTo('#tbl_posts2_body');

                element.find('.sn2').html(size);
                $("#tbl_posts2 #add2 .sn2").text($('#tbl_posts2 >tbody >tr').length);
                $("#tbl_posts2 #add2 .add").remove();
                $("#tbl_posts2 #tbl_posts2_body .floating-label-field").attr("readonly", true);
                element.find(".add-record2").hide();
                element.find(".delete-record2").show();
            }
        }
    });
}


$(document).delegate('a.add-record2', 'click', function (e) {
    debugger

    if ($("#tbl_posts2 #add2 #TName").val() != ''
        && $("#tbl_posts2 #add2 #TFatherName").val() != ''
        && $("#tbl_posts2 #add2 #TAddress").val() != '') {

        e.preventDefault();
        var content = jQuery('#tbl_posts2 #add2 tr'),
            size = jQuery('#tbl_posts2 >tbody >tr').length,
            element = null,
            element = content.clone();
        element.attr('id', 'rec-' + size);
        element.find('.delete-record2').attr('data-id', size);
        element.appendTo('#tbl_posts2_body');


        element.find('.sn2').html(size);
        $("#tbl_posts2 #add2 .sn2").text($('#tbl_posts2 >tbody >tr').length);
        $("#tbl_posts2 #add2 .add").remove();
        $("#tbl_posts2 #tbl_posts2_body .floating-label-field").attr("readonly", true);
        element.find(".add-record2").hide();
        element.find(".delete-record2").show();
        debugger

        $("#tbl_posts2 #add2 .floating-label-field").val('');
    }
    else {
        alert('Please fill record before add new record ');
    }
});
$(document).delegate('a.delete-record2', 'click', function (e) {
    e.preventDefault();
    var didConfirm = confirm("Are you sure You want to delete");
    if (didConfirm == true) {
        var id = jQuery(this).attr('data-id');
        var targetDiv = jQuery(this).attr('targetDiv');
        jQuery('#rec-' + id).remove();
        //regnerate index number on table
        $('#tbl_posts2_body tr').each(function (index) {
            //alert(index);
            $(this).find('span .sn2').html(index + 1);
        });
        $("#tbl_posts2 #add2 .sn2").text($('#tbl_posts2 >tbody >tr').length);
        return true;
    } else {
        return false;
    }
});
