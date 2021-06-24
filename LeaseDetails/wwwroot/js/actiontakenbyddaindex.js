
var currentPageNumber = 1;
var currentPageSize = 5;
var sortOrder = 1;//default Ascending 

$(document).ready(function () {
    debugger;
    GetAction(currentPageNumber, currentPageSize, sortOrder);
});

$("#btnSearch").click(function () {
    GetAction(currentPageNumber, currentPageSize, sortOrder);
});

$("#btnReset").click(function () {

    //$('#txtName').val('');
    //$('#txtCode').val('');
    //$('#txtFileNo').val('')
    $('#txtRefNo').val('')

    GetAction(currentPageNumber, currentPageSize, sortOrder);
});


$("#btnAscending").click(function () {
    $("#btnDescending").removeClass("active");
    $("#btnAscending").addClass("active");
    sortOrder = 1;//for Ascending
    GetAction(currentPageNumber, currentPageSize, sortOrder);
});


$("#btnDescending").click(function () {
    $("#btnAscending").removeClass("active");
    $("#btnDescending").addClass("active");
    sortOrder = 2;//for Descending
    GetAction(currentPageNumber, currentPageSize, sortOrder);
});
$('#ddlSort').change(function () {
    GetAction(currentPageNumber, currentPageSize, sortOrder);
});
function GetAction(pageNumber, pageSize, order) {
    var param = GetSearchParam(pageNumber, pageSize, order);
    HttpPost(`/ActionTakenByDDA/List`, 'html', param, function (response) {
        $('#divJudgementTable').html("");
        $('#divJudgementTable').html(response);
    });
}

function GetSearchParam(pageNumber, pageSize, sortOrder) {
    var model = {
        //letterReferenceNo: $('#txtFileNo').val(),
        //AllotmentNo: $('#txtReferenceNo').val(),
        //subject: $('#txtSubject').val(),
        letterReferenceNo: ($('#txtRefNo').val()),
        sortBy: $("#ddlSort").children("option:selected").val(),
        sortOrder: parseInt(sortOrder),
        pageSize: parseInt(pageSize),
        pageNumber: parseInt(pageNumber)


    }
    return model;
}

function onPaging(pageNo) {
    GetAction(parseInt(pageNo), parseInt(currentPageSize), sortOrder);
    currentPageNumber = pageNo;
}

function onChangePageSize(pageSize) {
    GetAction(parseInt(currentPageNumber), parseInt(pageSize), sortOrder);
    currentPageSize = pageSize;
}
