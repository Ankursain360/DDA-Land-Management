


var currentPageNumber = 1;
var currentPageSize = 5;
var sortOrder = 1;//default Ascending 


$(document).ready(function () {
    GetPossession(currentPageNumber, currentPageSize, sortOrder);
});

function GetExcel(pageNumber, pageSize, order) {
    var param = GetSearchParam(pageNumber, pageSize, order);
    HttpPost(`/PossessionDetail/PossessiondetailsList`, 'html', param, function (response) {
        var a = document.createElement("a");
        a.target = '_blank';
        a.href = '/PossessionDetail/download';
        a.click();
    });
}

$("#btndownload").click(function () {
    GetExcel(currentPageNumber, currentPageSize, sortOrder);
});

$("#btnSearch").click(function () {
    GetPossession(currentPageNumber, currentPageSize, sortOrder);
});


$("#btnReset").click(function () {
 
    $('#txtNotificationN').val('');

    GetPossession(currentPageNumber, currentPageSize, sortOrder);
});


$('#ddlSort').change(function () {
    GetPossession(currentPageNumber, currentPageSize, sortOrder);
});


$("#btnAscending").click(function () {
    $("#btnDescending").removeClass("active");
    $("#btnAscending").addClass("active");
    sortOrder = 1;//for Ascending
    GetPossession(currentPageNumber, currentPageSize, sortOrder);
});


$("#btnDescending").click(function () {
    $("#btnAscending").removeClass("active");
    $("#btnDescending").addClass("active");
    sortOrder = 2;//for Descending
    GetPossession(currentPageNumber, currentPageSize, sortOrder);
});

function GetPossession(pageNumber, pageSize, order) {
    var param = GetSearchParam(pageNumber, pageSize, order);
    HttpPost(`/PossessionDetail/List`, 'html', param, function (response) {
        console.log(response);

        $('#divPossessionDetail').html("");
        $('#divPossessionDetail').html(response);
    });


}

function GetSearchParam(pageNumber, pageSize, sortOrder) {

    var model = {
      
        villageid: $('#txtNotificationN').val(),

        sortBy: $("#ddlSort").children("option:selected").val(),
        sortOrder: parseInt(sortOrder),
        pageSize: parseInt(pageSize),
        pageNumber: parseInt(pageNumber)
    }
    console.log(model);
    return model;
}


function onPaging(pageNo) {
    GetPossession(parseInt(pageNo), parseInt(currentPageSize), sortOrder);
    currentPageNumber = pageNo;
}

function onChangePageSize(pageSize) {
    GetPossession(parseInt(currentPageNumber), parseInt(pageSize), sortOrder);
    currentPageSize = pageSize;
}


