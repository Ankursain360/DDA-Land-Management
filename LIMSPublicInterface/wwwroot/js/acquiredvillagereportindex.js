var currentPageNumber = 1;
var currentPageSize = 5;
var sortby = 1;//default Ascending 
$(document).ready(function () {
    $("#btnGenerate").click(function () {
        debugger;
        var result = ValidateForm();
        var name = $('#Name option:selected').val();
      if (result) {
            GetDetails(currentPageNumber, currentPageSize, sortby);
        }
       
    });

    //$(".linkdisabled").click(function () {
    //    return false;
    //});
});

function GetDetails(pageNumber, pageSize, order) {
    var param = GetSearchParam(pageNumber, pageSize, order);
    debugger
    HttpPost(`/AcquiredVillageReport/GetDetails`, 'html', param, function (response) {
        $('#LoadReportView').html("");
        $('#LoadReportView').html(response);
    });
}

function GetSearchParam(pageNumber, pageSize, sortOrder) {
    debugger;
    var name = $('#Name option:selected').val();

    var model = {
        name: "report",
        pageSize: parseInt(pageSize),
        pageNumber: parseInt(pageNumber),
        name: parseInt(name),

        sortBy: $("#ddlSort").children("option:selected").val(),
        sortOrder: parseInt(sortOrder),
    }
    return model;
}
$("#btnAscending").click(function () {
    $("#btnDescending").removeClass("active");
    $("#btnAscending").addClass("active");
    sortby = 1;//for Ascending
    GetDetails(currentPageNumber, currentPageSize, sortby);
});


$("#btnDescending").click(function () {
    $("#btnAscending").removeClass("active");
    $("#btnDescending").addClass("active");
    sortby = 2;//for Descending
    GetDetails(currentPageNumber, currentPageSize, sortby);
});

$("#btnReset").click(function () {

    $('#Name').val('0').trigger('change');


    GetDetails(currentPageNumber, currentPageSize, sortby);

});

function onPaging(pageNo) {
    GetDetails(parseInt(pageNo), parseInt(currentPageSize), sortby);
    currentPageNumber = pageNo;
}

function onChangePageSize(pageSize) {
    GetDetails(parseInt(currentPageNumber), parseInt(pageSize), sortby);
    currentPageSize = pageSize;
}