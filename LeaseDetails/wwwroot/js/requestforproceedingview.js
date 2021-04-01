$(document).ready(function () {

    var id = $("#CancellationId").find(':selected').attr('value');

    GetRelativeData(id);

});

function GetRelativeData(id) {
    HttpGet(`/RequestForProceedingEviction/GetRelativeData/?CancellationId=${id}`, 'json', function (response) {
        debugger;
        if (response != null) {
            $("#AllotmentId").val(response.allotmentId);
            $("#CancellationId").val(response.id);
            $("#Society_name").val(response.allotment.application.name);
            $("#Purpose_name").val(response.allotment.leasePurposesType.purposeUse);
        }
    });
};