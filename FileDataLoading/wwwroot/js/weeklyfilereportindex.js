var currentPageNumber = 1;
var currentPageSize = 10;
var sortby = 1;
var freeholdstatus = 0;

$(document).ready(function () {
         getDate();
       GetDetails(currentPageNumber, currentPageSize, sortby);
});

function GetDetails(pageNumber, pageSize, order) {
    var param = GetSearchParam(pageNumber, pageSize, order);
    HttpPost(`/WeeklyFileReport/List`, 'html', param, function (response) {
        $('#LoadView').html("");
        $('#LoadView').html(response);
    });

}
function getDate() {
    var today = new Date();
    document.getElementById("txtToDate").value = today.getFullYear() + '-' + ('0' + (today.getMonth() + 1)).slice(-2) + '-' + ('0' + today.getDate()).slice(-2);
    var dt = new Date(today);
    dt.setDate(dt.getDate() - 7);
    document.getElementById("txtFromDate").value = dt.getFullYear() + '-' + ('0' + (dt.getMonth() + 1)).slice(-2) + '-' + ('0' + dt.getDate()).slice(-2);
   
}


function GetSearchParam(pageNumber, pageSize, sortOrder) {
    var model = {
        DeptId: parseInt($('#DepartmentId option:selected').val()),
        FromDate:  $('#txtFromDate').val(),
        ToDate: $("#txtToDate").val(),
        sortBy: $("#ddlSort").children("option:selected").val(),
        sortOrder: parseInt(sortOrder),
        pageSize: parseInt(pageSize),
        pageNumber: parseInt(pageNumber)
    }
      debugger
    return model;
}
$('#ddlColName').change(function () {
    $("#txtsearchtxt").val('');
});
$("#btnSearch").click(function () {
    var fromdate = new Date($('#txtFromDate').val());
    var toDate = new Date($("#txtToDate").val());
    if (fromdate > toDate) {
        getDate();
        alert("From Date can not be greated than To Date. !!");
           }
    GetDetails(currentPageNumber, currentPageSize, sortby);
});

$("#btnReset").click(function () {
    $("#DepartmentId").val('0').trigger('change');
    getDate();
    GetDetails(currentPageNumber, currentPageSize, sortby);

});
function Descending() {
    $("#btnDescending").addClass("active");
    $("#btnAscending").removeClass("active");

    sortby = 2;     //for Descending
    GetDetails(currentPageNumber, currentPageSize, sortby);
};
function Ascending() {
    $("#btnAscending").addClass("active");
    $("#btnDescending").removeClass("active");

    sortby = 1;    //for Asc
    GetDetails(currentPageNumber, currentPageSize, sortby);

};

function GetDetailsOrderby(pageNumber, pageSize, order) {
    var param = GetSearchParamOrderby(pageNumber, pageSize, order);
    HttpPost(`/WeeklyFileReport/List`, 'html', param, function (response) {
        $('#LoadView').html("");
        $('#LoadView').html(response);
    });
    function GetSearchParamOrderby(pageNumber, pageSize, sortOrder) {
        var model = {
            DeptId: parseInt($('#DepartmentId option:selected').val()),
            FromDate: $('#txtFromDate').val(),
            ToDate: $("#txtToDate").val(),
            sortBy: $("#ddlSort").children("option:selected").val(),
            sortOrder: parseInt(sortOrder),
            pageSize: pageSize,
            pageNumber: pageNumber
        }
        debugger
        return model;
    }

     function onPaging(pageNo) {
        GetDetails(parseInt(pageNo), parseInt(currentPageSize), sortby);
        currentPageNumber = pageNo;
    }

    function onChangePageSize(pageSize) {
        GetDetails(parseInt(currentPageNumber), parseInt(pageSize), sortby);
        currentPageSize = pageSize;
    }


}