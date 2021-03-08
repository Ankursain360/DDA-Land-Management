

$(document).ready(function () {


    FillOwnerAtEdit();
    FillPaymentAtEdit();
   
});

function onChange(id) {
    //debugger
    HttpGet(`/EncroachmentDetails/GetKhasraList/?VillageId=${id}`, 'json', function (response) {
        var html = '<option value=""> select</option>';
        for (var i = 0; i < response.length; i++) {
            html = html + '<option value=' + response[i].id + '>' + response[i].name + '</option>';
        }

        // $("#KhasraId").select2('val', '')
        $("#KhasraId").html(html);
    });
};
//****************** code for saving owner Rpt ************************



function FillOwnerAtEdit() {


    HttpGet(`/EncroachmentDetails/GetDetailsOwner/?Id=${$("#Id").val() == null ? "" : $("#Id").val()}`, 'json', function (data) {
       
        for (var i = 0; i < data.length; i++) {
           
            $("#tbl_posts #add #EName").val(data[i].name);
            $("#tbl_posts #add #EAddress").val(data[i].address);
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

    if ($("#tbl_posts #add #EName").val() != ''
       && $("#tbl_posts #add #Address").val() != '') {

        e.preventDefault();
        var content = jQuery('#tbl_posts #add tr'),
            size = jQuery('#tbl_posts >tbody >tr').length,
            element = null,
            element = content.clone();
        element.attr('id', 'rec-' + size);
        element.find('.delete-record').attr('data-id', size);
        element.appendTo('#tbl_posts_body');
        // $('#tbl_posts_body #rec-' + size + ' #Gender').val(Gender);

        element.find('.sn').html(size);
        $("#tbl_posts #add .sn").text($('#tbl_posts >tbody >tr').length);
        $("#tbl_posts #add .add").remove();
        $("#tbl_posts #tbl_posts_body .floating-label-field").attr("readonly", true);
        element.find(".add-record").hide();
        element.find(".delete-record").show();
        

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


//****************** code for payment details Rpt ************************


function FillPaymentAtEdit() {
    
    HttpGet(`/EncroachmentDetails/GetDetailsPayment/?Id=${$("#Id").val() == null ? "" : $("#Id").val()}`, 'json', function (data) {
       
     //   HttpGet(`/EncroachmentDetails/GetDetailsPayment/?Id=${$("#Id").val() == null ? "" : $("#Id").val()}`, 'json', function (data) {
       
        for (var i = 0; i < data.length; i++) {
            $("#tbl_posts1 #add1 #Amount").val(data[i].amount);
            $("#tbl_posts1 #add1 #ChequeNo").val(data[i].chequeNo);
            $("#tbl_posts1 #add1 #ChequeDate").val(data[i].chequeDate);
           
            if (i < data.length - 1) {
                var content = jQuery('#tbl_posts1 #add1 tr'),
                    size = jQuery('#tbl_posts1 >tbody >tr').length,
                    element = null,
                    element = content.clone();
                element.attr('id', 'rec1-' + size);
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
    

    if ($("#tbl_posts1 #add1 #Amount").val() != ''
        || $("#tbl_posts1 #add1 #ChequeNo").val() != ''
        || $("#tbl_posts1 #add1 #ChequeDate").val() != ''     ) {

        e.preventDefault();
        var content = jQuery('#tbl_posts1 #add1 tr'),
            size = jQuery('#tbl_posts1 >tbody >tr').length,
            element = null,
            element = content.clone();
        element.attr('id', 'rec1-' + size);
        element.find('.delete-record1').attr('data-id', size);
        element.appendTo('#tbl_posts1_body');
        // $('#tbl_posts_body #rec1-' + size + ' #Gender').val(Gender);

        element.find('.sn1').html(size);
        $("#tbl_posts1 #add1 .sn1").text($('#tbl_posts1 >tbody >tr').length);
        $("#tbl_posts1 #add1 .add").remove();
        $("#tbl_posts1 #tbl_posts1_body .floating-label-field").attr("readonly", true);
        element.find(".add-record1").hide();
        element.find(".delete-record1").show();
       

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
        jQuery('#rec1-' + id).remove();
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

$(function ()
{
   
    $("input[name='grpDamageArea']").click(function () {
        if ($("#Dyes").is(":checked")) {
            $('#DamageArea').val('yes');
        } else {
            $('#DamageArea').val('No');
        }
    });
});
$("input[name='grpDamageArea']").click(function () {
    var selected = $("input[type='radio'][name='grpDamageArea']:checked");
    $("#DamageArea").val(selected.val());

});



