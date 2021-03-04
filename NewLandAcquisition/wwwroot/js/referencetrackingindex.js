var currentPageNumber = 1;
var currentPageSize = 10;

$(document).ready(function () {




})

$("#btnGenerate").click(function () {


    debugger;
    var UnderSection4Id = $("#ReferenceNo").val();
   // alert(UnderSection4Id);
    
    if (UnderSection4Id) {
        var param = GetSearchParam(currentPageNumber, currentPageSize);

        HttpPost(`/ReferenceTracking/GetDetails`, 'html', param, function (response) {
            $('#LoadReportView').html("");
            $('#LoadReportView').html(response);
        });
    } else {
        alert("Please Enter Reference No");
    }
});
function GetDetails(pageNumber, pageSize) {
    var param = GetSearchParam(pageNumber, pageSize);
    HttpPost(`/ReferenceTracking/GetDetails`, 'html', param, function (response) {
        $('#LoadReportView').html("");
        $('#LoadReportView').html(response);
    });

}


$("#btnReset").click(function () {
    $('#village_tag').html('Village Name - All');

    $('#Name').val('0').trigger('change');

    GetDetails(currentPageNumber, currentPageSize);


});


function GetSearchParam(pageNumber, pageSize) {

    var VillageId = $('#ReferenceNo').val();
    //alert(VillageId);
 //   name: $('#txtName').val(),

    var test = [];

    var model = {


        pageSize: parseInt(pageSize),
        pageNumber: parseInt(pageNumber),

        referenceNo: VillageId,

    }
    test.push(model);
    return model;
}
function onPaging(pageNo) {
    GetDetails(parseInt(pageNo), parseInt(currentPageSize));
    currentPageNumber = pageNo;
}

function onChangePageSize(pageSize) {
    GetDetails(parseInt(currentPageNumber), parseInt(pageSize));
    currentPageSize = pageSize;
}







