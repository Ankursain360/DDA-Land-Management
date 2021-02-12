var currentPageNumber = 1;
var currentPageSize = 5;
var sortOrder = 1;//default Ascending 


$(document).ready(function () {
    GetDisposalLand(currentPageNumber, currentPageSize, sortOrder);
});


function GetDisposalLand(pageNumber, pageSize, order) {
    var param = GetSearchParam(pageNumber, pageSize, order);
    HttpPost(`/DisposalLand/List`, 'html', param, function (response) {
        $('#divDisposalLandTable').html("");
        $('#divDisposalLandTable').html(response);
    });
}

function GetSearchParam(pageNumber, pageSize, sortOrder) {
    var model = {
       
        name: $('#txtName').val(),
        Khasra: $('#txtKhasra').val(),
        //district: $('#txtDistrict').val(),
        sortBy: $("#ddlSort").children("option:selected").val(),
        sortOrder: parseInt(sortOrder),
        pageSize: parseInt(pageSize),
        pageNumber: parseInt(pageNumber)
    }
    return model;
}

$("#btnSearch").click(function () {
    GetDisposalLand(currentPageNumber, currentPageSize, sortOrder);
});

$("#btnReset").click(function () {

    $('#txtName').val('');
    $('#txtKhasra').val(''),
    //$('#txtDistrict').val(''),
     
        GetDisposalLand(currentPageNumber, currentPageSize, sortOrder);
});


$("#btnAscending").click(function () {
    $("#btnDescending").removeClass("active");
    $("#btnAscending").addClass("active");
    sortOrder = 1;//for Ascending
    GetDisposalLand(currentPageNumber, currentPageSize, sortOrder);
});


$("#btnDescending").click(function () {
    $("#btnAscending").removeClass("active");
    $("#btnDescending").addClass("active");
    sortOrder = 2;//for Descending
    GetDisposalLand(currentPageNumber, currentPageSize, sortOrder);
});
$('#ddlSort').change(function () {
    GetDisposalLand(currentPageNumber, currentPageSize, sortOrder);
});

function onPaging(pageNo) {
    GetDisposalLand(parseInt(pageNo), parseInt(currentPageSize), sortOrder);
    currentPageNumber = pageNo;
}

function onChangePageSize(pageSize) {
    GetDisposalLand(parseInt(currentPageNumber), parseInt(pageSize), sortOrder);
    currentPageSize = pageSize;
}
