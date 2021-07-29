$(function () {
    $("input[name='IsPaymentAgreed']").click(function () {
        if ($("#rSubsequent").is(":checked")) {
            $("#DivForSubsequentPurchaser").show();
        } else {
            $("#DivForSubsequentPurchaser").hide();
        }
    });
});

$(document).ready(function () {
    GetPayment();
    GetPaymentFromBhoomi();
    $('#AAttendance').removeAttr('multiple');
});


function GetPayment() {    

    HttpGet(`/DemandDetails/PaymentDetails/?Id=${$("#KycId").val()}`, 'html', function (response) {
        debugger;
        $('#divPayment').html("");
        $('#divPayment').html(response);
    });
}


function GetPaymentFromBhoomi() {
    debugger;

    HttpGet(`/DemandDetails/PaymentFromBhoomi/?FileNo=${$("#FileNo").val()}`, 'html', function (response) {
        debugger;

        $('#divPaymentFromBhoomi').html("");
        $('#divPaymentFromBhoomi').html(response);
    });

}


//****************** code for Challan Payment ************************

$(document).delegate('a.add-record2', 'click', function (e) {
    debugger

    if ($("#tbl_posts2 #add2 #AName").val() != ''
        && $("#tbl_posts2 #add2 #ADesignation").val() != ''
        && $("#tbl_posts2 #add2 #AAttendance").children("option:selected").val() != ''
        && $("#tbl_posts2 #add2 #AAttendance").children("option:selected").val() != undefined
    ) {
        var Gender = $("#tbl_posts2 #add2 #AAttendance").children("option:selected").val();
        e.preventDefault();
        var content = jQuery('#tbl_posts2 #add2 tr'),
            size = jQuery('#tbl_posts2 >tbody >tr').length,
            element = null,
            element = content.clone();
        element.attr('id', 'rec-' + size);
        element.find('.delete-record2').attr('data-id', size);
        element.appendTo('#tbl_posts2_body');

        $('#tbl_posts2_body #rec-' + size + ' #AAttendance').val(Gender);
        element.find('.sn2').html(size);
        $("#tbl_posts2 #add2 .sn2").text($('#tbl_posts2 >tbody >tr').length);
        $("#tbl_posts2 #add2 .add").remove();
        $("#tbl_posts2 #add2 #AAttendance").select2('val', '');
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
