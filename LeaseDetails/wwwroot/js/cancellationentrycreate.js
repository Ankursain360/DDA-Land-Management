$(document).ready(function () {


});

function get_socity() {

    var sname = $("#AllotmentId").find(':selected').attr('data-id');
    var res = sname.split(",");
    // alert(res[0]);
    $("#Society_name").val(res[0]);
    $("#Purpose_name").val(res[1]);
}

function GetOtherData(id) {
    debugger;
    HttpGet(`/CancellationEntry/GetOtherData/?AllottmentId=${id}`, 'json', function (response) {
        if (response != null) {
            $("#Society_name").val(response.application.name);
            $("#Purpose_name").val(response.leasePurposesType.purposeUse); 
        }
    });
};