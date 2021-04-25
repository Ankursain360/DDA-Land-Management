$(document).ready(function () {

    var id = parseInt($('#Id').val());
    GetOtherDetails(id);
    GetHistoryDetails(id);
       
});

function GetOtherDetails(id) {
    HttpGet(`/LeaseApplicationFormApproval/LeaseApplicationFormView/?Id=${id}`, 'html', function (response) {
        $('#LeaseApplicationFormDetailsDiv').html("");
        $('#LeaseApplicationFormDetailsDiv').html(response);
    });
};

function GetHistoryDetails(id) {
    HttpGet(`/LeaseApplicationFormApproval/HistoryDetails/?Id=${id}`, 'html', function (response) {
        $('#divHistoryDetails').html("");
        $('#divHistoryDetails').html(response);
    });
};

function callSelect2() {
    $("select").select2({
        placeholder: "Select",
        allowClear: true
    });
}

$("#collapse").click(function () {
    $('#collapseApprroval').collapse("toggle").promise().done(function () {
        $("select").select2({
            placeholder: "Select",
            allowClear: true
        });
    });
});

$("#collapse").click(function () {
    $("#collapseHistoryApprroval").collapse("toggle").promise().done(function () {
        $('#select').select2({
            placeholder: "Select",
            allowClear: true
        });
    });
});

