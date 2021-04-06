$(document).ready(function () {

    var userid = $("#UserId").val();
    GetOtherData(userid);
});

function GetOtherData() {
    debugger;
    HttpGet(`/Mortgage/GetOtherData/`, 'json', function (response) {
        if (response != null) {
            $("#AllottmentId").val(response.allotmentId);
            $("#LeaseApplicationId").val(response.allotment.applicationId);
            $("#RefNo").val(response.allotment.application.refNo);
            $("#RegisterationNo").val(response.allotment.leasePurposesType.purposeUse);
            $("#ContactNo").val(response.allotment.application.contactNo);
            $("#EmailId").val(response.allotment.application.emailId);
            $("#AllotmentDate").val(response.allotment.allotmentDate.split('T')[0]);
            $("#LeasePurpose").val(response.allotment.leasePurposesType.purposeUse);
            $("#LeaseDate").val(response.allotment.allotmentDate.split('T')[0]);
            $("#AllottedArea").val(response.allotment.AllotedArea);
            $("#PossessionArea").val(response.allotment.PossessionArea);
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