var currentPageNumber = 1;
var currentPageSize = 5;
var sortby = 1;
$(document).ready(function () { 
    GetLms(currentPageNumber, currentPageSize, sortby);

});

function GetExcel(pageNumber, pageSize, order) {
    debugger;
    $.post(`/Legalmanagementsystem/LegalManagementSystemList`, function (data) {
        var param = GetSearchParam(pageNumber, pageSize, order);
        var w = window.open('about:blank');
        w.document.open();
        w.document.write(param);
        w.document.close();
    });
}

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
        lmfileno: $('#txtlmfileno').val(),
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
$("#btndownload").click(function () {
    GetExcel(currentPageNumber, currentPageSize, sortby);
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

$("#btnReset").click(function () {
    $("#txtfileno").val('');
    $("#txtlmfileno").val('');
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

