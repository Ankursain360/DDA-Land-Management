//$(function () {
//    $("#DivLandTransferDetails input").attr("readonly", true);
//    $("#DivLandTransferDetails textarea").attr("readonly", true);
//    $("#DivLandTransferDetails select").attr("disabled", true);
//});

$(document).ready(function () {
   
    var id = parseInt($('#Id').val());
    GetOtherDetails(id);

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