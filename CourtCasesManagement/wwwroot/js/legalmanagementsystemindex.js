var currentPageNumber = 1;
var currentPageSize = 5;
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
    debugger;
    var model = {
        fileNo: $('#txtfileno').val(),
        courtCaseNo: $('#txtCaseNo').val(),
       /* CourtType: parseInt($('#ddlCaseStatus option:selected').val()),*/
        caseStatus: $('#ddlCaseStatus').val(),
        courtType: $('#ddlCourtName').val(),
        courtCaseTitle: $('#txtCaseTitle').val(),
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
    $("#btndescending").addClass("active");
    sortby = 2;     //for Descending
    GetLms(currentPageNumber, currentPageSize, sortby);
};
function Ascending() {
    $("#btndescending").removeClass("active");
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
//function GetSearchParamOrderby(pageNumber, pageSize, sortOrder) {
//    var model = {
//      /*  name: $('#txtName').val(),*/
//        fileNo: $('#txtfileno').val(),
//        courtCaseNo: $('#txtCaseNo').val(),
//        /* CourtType: parseInt($('#ddlCaseStatus option:selected').val()),*/
//        courtType: $('#ddlCaseStatus').val(),
//        courtCaseTitle: $('#ddlCourtName').val(),
//        caseStatus: $('#txtCaseTitle').val(),
//        sortBy: $("#ddlSort").children("option:selected").val(),
//        sortOrder: parseInt(sortOrder),
//        pageSize: pageSize,
//        pageNumber: pageNumber
//    }
//    return model;
//}


$("#btnReset").click(function () {
    $("#txtfileno").val('');
    $("#txtCaseNo").val('');
   /* $('#ddlCaseStatus').val('0').trigger('change');*/
    $("#ddlCaseStatus").val('');
    $("#ddlCourtName").val('');
    $("#txtCaseTitle").val('');
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

