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
    FillChallanAtEdit();
    $('#AAttendance').removeAttr('multiple');
});



//****************** code for Challan Payment ************************



function FillChallanAtEdit() {


    HttpGet(`/KycPaymentApproval/GetChallanDetails/?Id=${$("#Id").val() == null ? "" : $("#Id").val()}`, 'json', function (data) {
        debugger
        for (var i = 0; i < data.length; i++) {
            $("#tbl_posts2 #add2 #PaymentType").val(data[i].paymentType);
            $("#tbl_posts2 #add2 #Period").val(data[i].Period);
            $("#tbl_posts2 #add2 #ChallanNoR").val(data[i].ChallanNo);
            $("#tbl_posts2 #add2 #Amount").val(data[i].Amount);
            $("#tbl_posts2 #add2 #DateofPaymentByAllottee").val(data[i].DateofPaymentByAllottee);
            $("#tbl_posts2 #add2 #Proofinpdf").val(data[i].Proofinpdf); 
            $("#tbl_posts2 #add2 #Ddabankcredit").val(data[i].Ddabankcredit); 

            if (i < data.length - 1) {
                var content = jQuery('#tbl_posts2 #add2 tr'),
                    size = jQuery('#tbl_posts2 >tbody >tr').length,
                    element = null,
                    element = content.clone();
                element.attr('id', 'rec2-' + size);
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
