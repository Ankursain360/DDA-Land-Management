var currentPageNumber = 1;
var currentPageSize = 5;
var sortOrder = 1;//default Ascending 

$(document).ready(function () {
    GetDetails(currentPageNumber, currentPageSize, sortOrder);
});

//$("#btnGenerate").click(function () {
   ////// GetDetails(currentPageNumber, currentPageSize, sortOrder);
//});

$("#btnReset").click(function () {
    document.getElementById("ddlRefNo").selectedIndex = "";

    location.reload();



});

$("#btnGenerate").click(function () {
   
    var id = $("#ddlRefNo").children("option:selected").val();
   
    if (id) {
        var param = {
            allotmentid: id,
        }
        HttpPost(`/PaymentDescription/GetDetails`, 'html', param, function (response) {
           
            $('#View').html("");
            $('#View').html(response);


        });

    }
})

















