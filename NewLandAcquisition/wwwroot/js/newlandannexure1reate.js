

$(document).ready(function () {
    var id = parseInt($('#RequestId').val());
    GetRequestData(id);
});


// CODE FOR SAVING VALUE OF RADIO BUTTON
$("input[name='unit']").click(function () {
    var selected = $("input[type='radio'][name='unit']:checked");
    $("#AreaUnit").val(selected.val());

});


//****************** code for saving khasra details Rpt ************************

$(document).delegate('a.add-record', 'click', function (e) {
    debugger

    if ($("#tbl_posts #add #OwnershipStatus").children("option:selected").val() != ''
        && $("#tbl_posts #add #OwnershipStatus").children("option:selected").val() != undefined
        && $("#tbl_posts #add #KhasaNo").val() != ''
        && $("#tbl_posts #add #Bigha").val() != ''
        && $("#tbl_posts #add #Biswa").val() != ''
        && $("#tbl_posts #add #Biswanshi").val() != ''

    ) 
    {
        var status = $("#tbl_posts #add #OwnershipStatus").children("option:selected").val();
        e.preventDefault();
        var content = jQuery('#tbl_posts #add tr'),
            size = jQuery('#tbl_posts >tbody >tr').length,
            element = null,
            element = content.clone();
        element.attr('id', 'rec-' + size);
        element.find('.delete-record').attr('data-id', size);
        element.appendTo('#tbl_posts_body');
        $('#tbl_posts_body #rec-' + size + ' #OwnershipStatus').val(status);

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




function GetRequestData(id) {
   // var id = parseInt($('#Id').val());
    HttpGet(`/Newlandannexure1/RequestView/?Id=${id}`, 'html', function (response) {
        $('#RequestView').html("");
        $('#RequestView').html(response);
    });
   
};