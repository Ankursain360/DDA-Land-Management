var currentPageNumber = 1;
var currentPageSize = 5;
var sortOrder = 1;//default Ascending 


$(document).ready(function () {
    GetDivision(currentPageNumber, currentPageSize, sortOrder);
});

$("#btnSearch").click(function () {
 
    GetDivision(currentPageNumber, currentPageSize, sortOrder);
});


$("#btnReset").click(function () {
    $('#txtReferenceNo').val('');
    $('#txtSubject').val('');
    $('#txtFileNo').val('')
    GetDivision(currentPageNumber, currentPageSize, sortOrder);
});

$("#btnAscending").click(function () {
    $("#btnDescending").removeClass("active");
    $("#btnAscending").addClass("active");
    sortOrder = 1;//for Ascending
    GetDivision(currentPageNumber, currentPageSize, sortOrder);
});


$("#btnDescending").click(function () {
    $("#btnAscending").removeClass("active");
    $("#btnDescending").addClass("active");
    sortOrder = 2;//for Descending
    GetDivision(currentPageNumber, currentPageSize, sortOrder);
});

function GetDivision(pageNumber, pageSize, order) {
  
    var param = GetSearchParam(pageNumber, pageSize, order);
    HttpPost(`/RequestForProceedingEviction/List`, 'html', param, function (response) {
        console.log(response);

        $('#divScheme').html("");
        $('#divScheme').html(response);
    });


}

function GetSearchParam(pageNumber, pageSize, sortOrder) {

    var model = {
        letterReferenceNo: $('#txtFileNo').val(),
        AllotmentNo: $('#txtReferenceNo').val(),
        subject: $('#txtSubject').val(),
        sortBy: $("#ddlSort").children("option:selected").val(),
        sortOrder: parseInt(sortOrder),
        pageSize: parseInt(pageSize),
        pageNumber: parseInt(pageNumber)
    }
    console.log(model);
    return model;
}


function onPaging(pageNo) {
    GetDivision(parseInt(pageNo), parseInt(currentPageSize), sortOrder);
    currentPageNumber = pageNo;
}

function onChangePageSize(pageSize) {
    GetDivision(parseInt(currentPageNumber), parseInt(pageSize), sortOrder);
    currentPageSize = pageSize;
}


