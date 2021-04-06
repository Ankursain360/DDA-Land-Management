$(document).ready(function () {

    GetOtherData();
    GetTimeLineExtensionFees();

    $(".TotalCalculation").keyup(function () {
        debugger;
        var period = $('#ExtensionPeriod').val();
        var fees = $('#ExtentionFees').val();

        $("input[name='TotalAmount']").val((parseFloat(period == '' ? 0 : period) * parseFloat(fees == '' ? 0 : fees)).toFixed(3));

    });

});

function GetOtherData() {
    HttpGet(`/ExtensionService/GetOtherData/`, 'json', function (response) {
        if (response != null) {
            $("#AllotmentId").val(response.allotmentId);
            $("#LeaseApplicationId").val(response.allotment.applicationId);
            $("#RefNo").val(response.allotment.application.refNo);
            $("#RegisterationNo").val(response.allotment.leasePurposesType.purposeUse);
            $("#ContactNo").val(response.allotment.application.contactNo);
            $("#EmailId").val(response.allotment.application.emailId);
            $("#AllotmentDate").val(response.allotment.allotmentDate.split('T')[0]);
            $("#LeasePurpose").val(response.allotment.leasePurposesType.purposeUse);
            $("#LeaseDate").val(response.allotment.allotmentDate.split('T')[0]);
            $("#AllottedArea").val(response.allotment.totalArea);
            $("#PossessionArea").val(response.allotment.PossessionArea);
        }
    });
};

function GetTimeLineExtensionFees() {
    HttpGet(`/ExtensionService/GetTimeLineExtensionFees/`, 'json', function (response) {
        if (response != null) {
            $("#ExtentionFees").val(response.extensionFees);
        }
    });
};

function onDocumentChange(element) {
    debugger;
    var filePath = element.value;
    const size = (element.files[0].size);
    fileValidation(filePath, element, size);
}

function fileValidation(filePath, fileInput, size) {
    var allowedExtensions = /(.pdf)$/i;
    if (!allowedExtensions.exec(filePath)) {
        alert('Invalid file type');
        fileInput.value = '';
        return false;
    }
    if (size > 10535049) {
        alert("File must be of 10 MB or Lesser Than 10 MB");
        fileInput.value = '';
        return false;
    }

}

function EditDocument(id) {
    HttpGet(`/ExtensionService/EditDocument/?DocumentId=${id}`, 'json', function (response) {
        if (response != null) {

            $("#EditPosition").val("NotComplete");
            $("#EditDocumentId").val(response.id);
            $("#EditDocumentFileName").val(response.documentFileName);
            $("#DocumentEdit").show();
        }
        else {
            $("#EditPosition").val("Complete");
            $("#EditDocumentId").val("0");
            $("#EditDocumentFileName").val("");
            $("#DocumentEdit").hide();
        }
    });
}
function EditUpdate() {

}
function Close() {
    $("#EditPosition").val("Complete");
    $("#EditDocumentId").val("0");
    $("#EditDocumentFileName").val("");
    $("#DocumentEdit").hide();
}