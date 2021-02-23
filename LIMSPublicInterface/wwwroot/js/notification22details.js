var currentPageNumber = 1;
var currentPageSize = 10;



$("#btnGenerate").click(function () {
    var UnderSection4Id = $("#UnderSection22Id").val();
    if (UnderSection4Id) {
        debugger;
        var param = GetSearchParam(currentPageNumber, currentPageSize);

        HttpPost(`/Notification22Details/GetDetails`, 'html', param, function (response) {
            $('#LoadReportView').html("");
            $('#LoadReportView').html(response);
        });
    } else {
        alert("Please Select Notification Number/Date");
    }

});
function GetDetails(pageNumber, pageSize) {
    var param = GetSearchParam(pageNumber, pageSize);
    HttpPost(`/Notification22Details/GetDetails`, 'html', param, function (response) {
        $('#LoadReportView').html("");
        $('#LoadReportView').html(response);
    });

}


$("#btnReset").click(function () {

    $('#UnderSection22Id').trigger('change');
    location.reload();




});


function GetSearchParam(pageNumber, pageSize) {



    var VillageId = $('#UnderSection22Id option:selected').val();

    var test = [];

    var model = {


        pageSize: parseInt(pageSize),
        pageNumber: parseInt(pageNumber),

        notification22: parseInt(VillageId),

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

