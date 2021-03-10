
function TestDate() {
    debugger
    var textbox1 = document.getElementById("<%=txtFromDate.ClientID%>");
    var textbox2 = document.getElementById("<%=txtToDate.ClientID%>");
   
    var flag = true;

    if (textbox2.value != "" < textbox3.value != "") {
        alert("To Date should be less than or Equal to From Date");
    }
};
function checkTextField(field) {
   // debugger
    var textbox1 = document.getElementById("<%=txtFromDate.ClientID%>");
    var textbox2 = document.getElementById("<%=txtToDate.ClientID%>");

    var flag = true;

    if (($('#txtToDate').val()) < ($('#txtFromDate').val()) ) {
        document.getElementById("error").innerText =
            "To Date should be less than or Equal to From Date";
        alert("To Date should be less than or Equal to From Date");
    }
   
}