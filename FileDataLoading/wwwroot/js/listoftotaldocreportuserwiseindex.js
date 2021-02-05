var currentPageNumber = 1;
var currentPageSize = 10;
var sortby = 1;
var freeholdstatus = 0;

$(document).ready(function () {
    GetDataStorage(currentPageNumber, currentPageSize, sortby);
});

function GetDataStorage(pageNumber, pageSize, order) {
    var param = GetSearchParam(pageNumber, pageSize, order);
    HttpPost(`/ListOfTotalDocReportUserWise/List`, 'html', param, function (response) {
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
$('#ddlColName').change(function () {
    $("#txtsearchtxt").val('');
});
$("#btnSearch").click(function () {
    GetDataStorage(currentPageNumber, currentPageSize, sortby);
});

$("#btnReset").click(function () {
    GetDataStorage(currentPageNumber, currentPageSize, sortby);
    var temp = "0";
    $("#txtsearchtxt").val('');
    $("#ddlColName").val('0').trigger('change');
    
   
});
function Descending() {
    $("#btnDescending").addClass("active");
    $("#btnAscending").removeClass("active");

    sortby = 2;     //for Descending
    GetDataStorage(currentPageNumber, currentPageSize, sortby);
};
function Ascending() {
    $("#btnAscending").addClass("active");
    $("#btnDescending").removeClass("active");

    sortby = 1;    //for Asc
    GetDataStorage(currentPageNumber, currentPageSize, sortby);

};

function GetDataStorageOrderby(pageNumber, pageSize, order) {
    var param = GetSearchParamOrderby(pageNumber, pageSize, order);
    HttpPost(`/ListOfTotalDocReportUserWise/List`, 'html', param, function (response) {
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