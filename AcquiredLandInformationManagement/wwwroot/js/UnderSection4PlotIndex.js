


var currentPageNumber = 1;
var currentPageSize = 5;
var sortOrder = 1;//default Ascending 


$(document).ready(function () {
    GetUS4(currentPageNumber, currentPageSize, sortOrder);
});


function GetExcel(pageNumber, pageSize, order) {
    var param = GetSearchParam(pageNumber, pageSize, order);
    HttpPost(`/UnderSection4PlotForm/Undersection4plotList`, 'html', param, function (response) {
        var a = document.createElement("a");
        a.target = '_blank';
        a.href = '/UnderSection4PlotForm/download';
        a.click();
    });
}

$("#btndownload").click(function () {
    GetExcel(currentPageNumber, currentPageSize, sortOrder);
});

$("#btnSearch").click(function () {
    GetUS4(currentPageNumber, currentPageSize, sortOrder);
});


$("#btnReset").click(function () {
    $('#txtPName').val('');
    $('#txtNotificationN').val('');

    GetUS4(currentPageNumber, currentPageSize, sortOrder);
});

$('#ddlSort').change(function () {
    GetUS4(currentPageNumber, currentPageSize, sortOrder);
});

$("#btnAscending").click(function () {
    $("#btnDescending").removeClass("active");
    $("#btnAscending").addClass("active");
    sortOrder = 1;//for Ascending
    GetUS4(currentPageNumber, currentPageSize, sortOrder);
});


$("#btnDescending").click(function () {
    $("#btnAscending").removeClass("active");
    $("#btnDescending").addClass("active");
    sortOrder = 2;//for Descending
    GetUS4(currentPageNumber, currentPageSize, sortOrder);
});

function GetUS4(pageNumber, pageSize, order) {
    var param = GetSearchParam(pageNumber, pageSize, order);
    HttpPost(`/UnderSection4PlotForm/List`, 'html', param, function (response) {
        console.log(response);

        $('#divUnderSection4Plot').html("");
        $('#divUnderSection4Plot').html(response);
    });


}

function GetSearchParam(pageNumber, pageSize, sortOrder) {

    var model = {
        numbernotification4: $('#txtPName').val(),
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
    GetUS4(parseInt(pageNo), parseInt(currentPageSize), sortOrder);
    currentPageNumber = pageNo;
}

function onChangePageSize(pageSize) {
    GetUS4(parseInt(currentPageNumber), parseInt(pageSize), sortOrder);
    currentPageSize = pageSize;
}


