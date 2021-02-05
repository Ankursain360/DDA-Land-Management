var currentPageNumber = 1;
var currentPageSize = 5;
var sortOrder = 1;//default Ascending 


$(function () {
    GetFileDetails(currentPageNumber, currentPageSize, sortOrder);
});

function GetFileDetails(pageNumber, pageSize, sortOrder) {
    var param = GetSearchParam(pageNumber, pageSize, sortOrder);
    HttpPost(`/DisplayLabel/List`, 'html', param, function (response) {
        $('#LoadView').html("");
        $('#LoadView').html(response);
    });
}

$("#btnSearch").click(function () {
    GetFileDetails(currentPageNumber, currentPageSize, sortOrder);
});
$("#btnAscending").click(function () {
    $("#btnDescending").removeClass("active");
    $("#btnAscending").addClass("active");
    sortOrder = 1;//for Ascending
    GetFileDetails(currentPageNumber, currentPageSize, sortOrder);
});

$("#btnDescending").click(function () {
    $("#btnAscending").removeClass("active");
    $("#btnDescending").addClass("active");
    sortOrder = 2;//for Descending
    GetFileDetails(currentPageNumber, currentPageSize, sortOrder);
});
$('#ddlSort').change(function () {
    GetFileDetails(currentPageNumber, currentPageSize, sortOrder);
});
$("#btnReset").click(function () {
    //$('#Id').val('0').trigger('change');
    $('#txtFileNo').val('');
    $('#txtName').val('');
   // $('#txtAlmirah').val('');
    //$('#txtRow').val('');
    //$('#txtColumn').val('');
    //$('#txtBundle').val('');
    GetFileDetails(currentPageNumber, currentPageSize, sortOrder);
});
function GetSearchParam(pageNumber, pageSize, sortOrder) {
    var model = {
        fileNo: $('#txtFileNo').val(),
        name: $('#txtName').val(),
        //almirah: $('#txtAlmirah').val(),
        //row: $('#txtRow').val(),
        //column: $('#txtColumn').val(),
        //bundle: $('#txtBundle').val(),
        sortBy: $("#ddlSort").children("option:selected").val(),
        sortOrder: parseInt(sortOrder),
        pageSize: pageSize,
        pageNumber: pageNumber,
        
    }
    return model;
}

function onPaging(pageNo) {
    pageNo = parseInt(pageNo);
    GetFileDetails(currentPageNumber, currentPageSize, sortOrder);
    currentPageNumber = pageNo;
}

function onChangePageSize(pageSize) {
    pageSize = parseInt(pageSize);
    GetFileDetails(currentPageNumber, currentPageSize, sortOrder);
    currentPageSize = pageSize;
}

