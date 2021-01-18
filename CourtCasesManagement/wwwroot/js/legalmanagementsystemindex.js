var currentPageNumber = 1;
var currentPageSize = 10;
var sortby = 1;
$(document).ready(function () {
    GetLms(currentPageNumber, currentPageSize, sortby);
});

function GetLms(pageNumber, pageSize, order) {
    var param = GetSearchParam(pageNumber, pageSize, order);
       HttpPost(`/Legalmanagementsystem/List`, 'html', param, function (response) {
        $('#divLegalmanagementsystemTable').html("");
        $('#divLegalmanagementsystemTable').html(response);

    });

}
function GetSearchParam(pageNumber, pageSize, sortOrder) {
    var model = {
        name: $('#txtName').val(),
        sortBy: $("#ddlSort").children("option:selected").val(),
        sortOrder: parseInt(sortOrder),
        pageSize: parseInt(pageSize),
        pageNumber: parseInt(pageNumber)
    }
    //debugger
    return model;
}

$("#btnSearch").click(function () {
    GetLms(currentPageNumber, currentPageSize, sortby);
});


function Descending() {
    $("#btnAscending").removeClass("active");
    $("#btnDescending").addClass("active");
    sortby = 2;     //for Descending
    GetLms(currentPageNumber, currentPageSize, sortby);
};
function Ascending() {
    $("#btnDescending").removeClass("active");
    $("#btnAscending").addClass("active");
    sortby = 1;    //for Asc
    GetLms(currentPageNumber, currentPageSize, sortby);

};
function GetLmsOrderby(pageNumber, pageSize, order) {
    var param = GetSearchParamOrderby(pageNumber, pageSize, order);
    HttpPost(`/Legalmanagementsystem/List`, 'html', param, function (response) {
        $('#divLegalmanagementsystemTable').html("");
        $('#divLegalmanagementsystemTable').html(response);

    });
}
function GetSearchParamOrderby(pageNumber, pageSize, sortOrder) {
    var model = {
        name: $('#txtName').val(),
        sortBy: $("#ddlSort").children("option:selected").val(),
        sortOrder: parseInt(sortOrder),
        pageSize: pageSize,
        pageNumber: pageNumber
    }
    return model;
}


$("#btnReset").click(function () {
    $("#txtName").val('');
    GetLms(currentPageNumber, currentPageSize, sortby);
});

function onPaging(pageNo) {
    GetLms(parseInt(pageNo), parseInt(currentPageSize), sortby);
    currentPageNumber = pageNo;
}

function onChangePageSize(pageSize) {
    GetLms(parseInt(currentPageNumber), parseInt(pageSize), sortby);
    currentPageSize = pageSize;
}