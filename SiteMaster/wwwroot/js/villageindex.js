var currentPageNumber = 1;
var currentPageSize = 5;
var sortby = 1;//default Ascending 

$(document).ready(function () {
    GetVillage(currentPageNumber, currentPageSize, sortby);
});

function GetVillage(pageNumber, pageSize, order) {
    var param = GetSearchParam(pageNumber, pageSize, order);
  //  alert(JSON.stringify(param));
    HttpPost(`/village/List`, 'html', param, function (response) {
        $('#divVillageTable').html("");
        $('#divVillageTable').html(response);
    });
}

function GetSearchParam(pageNumber, pageSize, sortOrder) {
    var model = {
        name: $('#txtName').val(),
        zone: $('#txtZoneName').val(),
        sortBy: $("#ddlSort").children("option:selected").val(),
        sortOrder: parseInt(sortOrder),
        pageSize: parseInt(pageSize),
        pageNumber: parseInt(pageNumber)

    }
    return model;
}


$("#btnAscending").click(function () {
    $("#btnDescending").removeClass("active");
    $("#btnAscending").addClass("active");
    sortby = 1;//for Ascending
    GetVillage(currentPageNumber, currentPageSize, sortby);
});


$("#btnDescending").click(function () {
    $("#btnAscending").removeClass("active");
    $("#btnDescending").addClass("active");
    sortby = 2;//for Descending
    GetVillage(currentPageNumber, currentPageSize, sortby);
});


$("#btnSearch").click(function () {
    GetVillage(currentPageNumber, currentPageSize, sortby);
});

$("#btnReset").click(function () {
    $('#txtName').val('');
    $('#txtZoneName').val('');
   
    GetVillage(currentPageNumber, currentPageSize, sortby);

});


function onPaging(pageNo) {
    GetVillage(parseInt(pageNo), parseInt(currentPageSize), sortby);
    currentPageNumber = pageNo;
}

function onChangePageSize(pageSize) {
    GetVillage(parseInt(currentPageNumber), parseInt(pageSize), sortby);
    currentPageSize = pageSize;
}