


var currentPageNumber = 1;
var currentPageSize = 5;
var sortOrder = 1;//default Ascending 


$(document).ready(function () {
    GetUS6(currentPageNumber, currentPageSize, sortOrder);
});

$("#btnSearch").click(function () {
    GetUS6(currentPageNumber, currentPageSize, sortOrder);
});


$("#btnReset").click(function () {
    $('#txtPName').val('');
    $('#txtNotificationN').val('');

    GetUS6(currentPageNumber, currentPageSize, sortOrder);
});


$('#ddlSort').change(function () {
    GetUS6(currentPageNumber, currentPageSize, sortOrder);
});


$("#btnAscending").click(function () {
    $("#btnDescending").removeClass("active");
    $("#btnAscending").addClass("active");
    sortOrder = 1;//for Ascending
    GetUS6(currentPageNumber, currentPageSize, sortOrder);
});


$("#btnDescending").click(function () {
    $("#btnAscending").removeClass("active");
    $("#btnDescending").addClass("active");
    sortOrder = 2;//for Descending
    GetUS6(currentPageNumber, currentPageSize, sortOrder);
});

function GetUS6(pageNumber, pageSize, order) {
    var param = GetSearchParam(pageNumber, pageSize, order);
    HttpPost(`/UnderSection6plotDetails/List`, 'html', param, function (response) {
        console.log(response);

        $('#divUnderSection6Plot').html("");
        $('#divUnderSection6Plot').html(response);
    });


}

function GetSearchParam(pageNumber, pageSize, sortOrder) {

    var model = {
        numbernotification6: $('#txtPName').val(),
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
    GetUS6(parseInt(pageNo), parseInt(currentPageSize), sortOrder);
    currentPageNumber = pageNo;
}

function onChangePageSize(pageSize) {
    GetUS6(parseInt(currentPageNumber), parseInt(pageSize), sortOrder);
    currentPageSize = pageSize;
}


