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
            $("#Subject").val(response.subject);
            $("#GroundOfViolations").val(response.groundOfViolations);
            $("#DateOfCancellationofLease").val(response.dateOfCancellationofLease.split('T')[0]);
            $("#HonebleLgOrCommon").val(response.honebleLgOrCommon).trigger('change');
            $("#ProceedingEvictionPossession").val(response.proceedingEvictionPossession);
            $("#CourtCaseifAny").val(response.courtCaseifAny);
            if (response.demandLetter != null) {
                $("#viewDemandLetterId").attr('href', '/RequestForProceedingEviction/ViewLetter/' + response.id)
                $("#viewDemandLetterId").show();
                $("#NoFileDemandLetterId").hide();
                $("#DemandLetter").val(response.demandLetter);
            }
            else {
                $('#viewDemandLetterId').hide();
                $("#NoFileDemandLetterId").show();
                $("#DemandLetter").val('');
            }
            if (response.noc != null) {
                $("#viewNOCId").attr('href', '/RequestForProceedingEviction/ViewLetter1/' + response.id)
                $("#viewNOCId").show();
                $("#NoFileNOCId").hide();
                $("#Noc").val(response.noc);
            }
            else {
                $('#viewNOCId').hide();
                $("#NoFileNOCId").show();
                $("#Noc").val('');
            }
            if (response.cancellationOrder != null) {
                $("#viewCancelOrderId").attr('href', '/RequestForProceedingEviction/ViewLetter2/' + response.id);
                $("#viewCancelOrderId").show();
                $("#NoFileCancelOrderId").hide();
                $("#CancellationOrder").val(response.cancellationOrder);
            }
            else {
                $('#viewCancelOrderId').hide();
                $("#NoFileCancelOrderId").show();
                $("#CancellationOrder").val('');
            }
        }
    });
};