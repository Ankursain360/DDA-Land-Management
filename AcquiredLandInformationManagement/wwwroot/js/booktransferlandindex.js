var currentPageNumber = 1;
var currentPageSize = 5;
var sortOrder = 1;//default Ascending 

$(document).ready(function () {
    GetBooktransferland(currentPageNumber, currentPageSize, sortOrder);
});


function GetBooktransferland(pageNumber, pageSize, order) {
   
    var param = GetSearchParam(pageNumber, pageSize, order);
    HttpPost(`/bookTransferLand/List`, 'html', param, function (response) {
       // alert(response);
        $('#divBooktransferlandTable').html("");
        $('#divBooktransferlandTable').html(response);
    });
}

$("#btnSearch").click(function () {
    GetBooktransferland(currentPageNumber, currentPageSize, sortOrder);
});


$('#ddlSort').change(function () {
    GetBooktransferland(currentPageNumber, currentPageSize, sortOrder);
});

$("#btnAscending").click(function () {
    $("#btnDescending").removeClass("active");
    $("#btnAscending").addClass("active");
    sortOrder = 1;//for Ascending
    GetBooktransferland(currentPageNumber, currentPageSize, sortOrder);
});


$("#btnDescending").click(function () {
    $("#btnAscending").removeClass("active");
    $("#btnDescending").addClass("active");
    sortOrder = 2;//for Descending
    GetBooktransferland(currentPageNumber, currentPageSize, sortOrder);
});

$("#btnReset").click(function () {
    $('#txtName').val('');
    $('#txtCode').val('');
    GetBooktransferland(currentPageNumber, currentPageSize, sortOrder);
});

function GetSearchParam(pageNumber, pageSize, sortOrder) {
    var model = {
        name: $('#txtName').val(),
     //   localityCode: $('#txtCode').val(),
        sortBy: $("#ddlSort").children("option:selected").val(),
        sortOrder: parseInt(sortOrder),
        pageSize: parseInt(pageSize),
        pageNumber: parseInt(pageNumber)
    }
    return model;
}


function onPaging(pageNo) {
  
    GetBooktransferland(parseInt(pageNo), parseInt(currentPageSize), sortOrder);
    currentPageNumber = pageNo;
}

function onChangePageSize(pageSize) {
    GetBooktransferland(parseInt(currentPageNumber), parseInt(pageSize), sortOrder);
    currentPageSize = pageSize;
}


