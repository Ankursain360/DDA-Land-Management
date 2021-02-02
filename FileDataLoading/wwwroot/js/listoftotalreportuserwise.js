var currentPageNumber = 1;
var currentPageSize = 10;
var sortby = 1;
var freeholdstatus = 0;

$(document).ready(function () {
    GetDataStorage(currentPageNumber, currentPageSize, sortby);
});

function GetDataStorage(pageNumber, pageSize,order) {
    var param = GetSearchParam(pageNumber, pageSize,order);
    HttpPost(`/ListOfTotalFilesReportUserWise/List`, 'html', param, function (response) {
        $('#LoadView').html("");
        $('#LoadView').html(response);
    });

}

function GetSearchParam(pageNumber, pageSize, sortOrder) {
    var model = {
        name: freeholdstatus,
        searchCol: $('#ddlColName').children("option:selected").val(),
        searchText: $("#txtsearchtxt").val(),
        sortBy: $("#ddlSort").children("option:selected").val(),
        sortOrder: parseInt(sortOrder),
        pageSize: parseInt(pageSize),
        pageNumber: parseInt(pageNumber)
    }
  //  debugger
    return model;
}
$("#btnSearch").click(function () {
    GetDataStorage(currentPageNumber, currentPageSize, sortby);
});

$("#btnReset").click(function () {
    $("#txtsearchtxt").val('');
    GetDataStorage(currentPageNumber, currentPageSize, sortby);
});
function Descending() {
    $("#btnAscending").removeClass("active");
    $("#btnDescending").addClass("active");
    sortby = 2;     //for Descending
    GetDataStorage(currentPageNumber, currentPageSize, sortby);
};
function Ascending() {
    $("#btnDescending").removeClass("active");
    $("#btnAscending").addClass("active");
    sortby = 1;    //for Asc
    GetDataStorage(currentPageNumber, currentPageSize, sortby);

};
function FreeHoldYes() {
    $("#ryes").checked = true;
   
   
    freeholdstatus = 1;    //for Asc
    GetDataStorage(currentPageNumber, currentPageSize, sortby);

};
function FreeHoldNo() {
     
    freeholdstatus = 0;    //for Asc
    GetDataStorage(currentPageNumber, currentPageSize, sortby);

};

function GetDataStorageOrderby(pageNumber, pageSize, order) {
    var param = GetSearchParamOrderby(pageNumber, pageSize, order);
    HttpPost(`/ListOfTotalFilesReportUserWise/List`, 'html', param, function (response) {
        $('#LoadView').html("");
        $('#LoadView').html(response);
    });
function GetSearchParamOrderby(pageNumber, pageSize, sortOrder) {
    var model = {
        name: freeholdstatus,
        searchCol: $('#ddlColName').children("option:selected").val(),
        searchText: $("#txtsearchtxt").val(),
        sortBy: $("#ddlSort").children("option:selected").val(),
        sortOrder: parseInt(sortOrder),
        pageSize: pageSize,
        pageNumber: pageNumber
    }
    return model;
}



function onPaging(pageNo) {
    GetDataStorage(parseInt(pageNo), parseInt(currentPageSize), sortby);
    currentPageNumber = pageNo;
}

function onChangePageSize(pageSize) {
    GetDataStorage(parseInt(currentPageNumber), parseInt(pageSize), sortby);
    currentPageSize = pageSize;
}


}






