var currentPageNumber = 1;
var currentPageSize = 5;
var sortOrder = 1;//default Ascending 

$(document).ready(function () {
    GetTehsil(currentPageNumber, currentPageSize, sortOrder);
});

$("#btnSearch").click(function () {
    GetTehsil(currentPageNumber, currentPageSize, sortOrder);
});

$("#btnReset").click(function () {

    $('#txtName').val('');
   // $('#txtBody').val(''),
   //$('#txtFile').val(''),
    //$('#txtDate').val(''),
        GetTehsil(currentPageNumber, currentPageSize, sortOrder);
});


$("#btnAscending").click(function () {
    $("#btnDescending").removeClass("active");
    $("#btnAscending").addClass("active");
    sortOrder = 1;//for Ascending
    GetTehsil(currentPageNumber, currentPageSize, sortOrder);
});


$("#btnDescending").click(function () {
    $("#btnAscending").removeClass("active");
    $("#btnDescending").addClass("active");
    sortOrder = 2;//for Descending
    GetTehsil(currentPageNumber, currentPageSize, sortOrder);
});
$('#ddlSort').change(function () {
    GetTehsil(currentPageNumber, currentPageSize, sortOrder);
});
function GetTehsil(pageNumber, pageSize, order) {
    var param = GetSearchParam(pageNumber, pageSize, order);
    HttpPost(`/Tehsil/List`, 'html', param, function (response) {
        $('#divTehsilTable').html("");
        $('#divTehsilTable').html(response);
    });
}

function GetSearchParam(pageNumber, pageSize, sortOrder) {
    var model = {
        name: $('#txtName').val(),
        //requiredAgency: $('#txtBody').val(),
        //proposalFileNo: $('#txtFile').val(),
        //proposalDate: $('#txtDate').val(),
        sortBy: $("#ddlSort").children("option:selected").val(),
        sortOrder: parseInt(sortOrder),
        pageSize: parseInt(pageSize),
        pageNumber: parseInt(pageNumber)
    }
    return model;
}


function onPaging(pageNo) {
    GetTehsil(parseInt(pageNo), parseInt(currentPageSize), sortOrder);
    currentPageNumber = pageNo;
}

function onChangePageSize(pageSize) {
    GetTehsil(parseInt(currentPageNumber), parseInt(pageSize), sortOrder);
    currentPageSize = pageSize;
}
