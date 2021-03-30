
function checkTextField(field) {
   // debugger
    var textbox1 = document.getElementById("<%=txtFromDate.ClientID%>");
    var textbox2 = document.getElementById("<%=txtToDate.ClientID%>");

    var flag = true;

    if (($('#txtToDate').val()) < ($('#txtFromDate').val()) ) {
        document.getElementById("error").innerText =
            "To Date should be greater than or Equal to From Date";
        alert("To Date should be greater than or Equal to From Date");
    }
   
}
function onChange(id) {

    HttpGet(`/GroundRent/GetAllLeaseSubpurpose/?purposeUseId=${id}`, 'json', function (response) {
        var html = '<option value="">Select</option>';
        for (var i = 0; i < response.length; i++) {
            html = html + '<option value=' + response[i].id + '>' + response[i].subPurposeUse + '</option>';
        }

        $("#LeaseSubPurposeId").select2('val', '')
        $("#LeaseSubPurposeId").html(html);
    });
};