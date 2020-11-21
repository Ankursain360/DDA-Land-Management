//$(function () {
//    $("#DivLandTransferDetails input").attr("readonly", true);
//    $("#DivLandTransferDetails textarea").attr("readonly", true);
//    $("#DivLandTransferDetails select").attr("disabled", true);
//});

$(document).ready(function () {
   
    var id = parseInt($('#Id').val());
    GetOtherDetails(id);

    HttpGet(`/WatchWardApproval/GetApprovalDropdownList`, 'html', function (response) {
        response = JSON.parse(response);
        $('#ApprovalStatus option').each(function () {
            if (response.length>0) {
                for (var i = 0; i < response.length; i++) {
                    if (response[i] == $(this).val()) {
                        $(this).show();
                    }
                    else {
                        $(this).remove();
                    }
                }
            }
        });
    });
   
});


function GetOtherDetails(id) {
    HttpGet(`/WatchWardApproval/WatchWardView/?Id=${id}`, 'html', function (response) {
        $('#WatchWardDetailsDiv').html("");
        $('#WatchWardDetailsDiv').html(response);
    });
};

function callSelect2() {
    $("select").select2({
        placeholder: "Select",
        allowClear: true
    });
}

$("#collapse").click(function () {
    debugger;
    $('#collapseApprroval').collapse("toggle").promise().done(function () {
        $("select").select2({
            placeholder: "Select",
            allowClear: true
        });
    })
});