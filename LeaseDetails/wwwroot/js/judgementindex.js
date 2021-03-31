
var currentPageNumber = 1;
var currentPageSize = 5;
var sortOrder = 1;//default Ascending 

$(document).ready(function () {
    debugger;
    GetJudgement(currentPageNumber, currentPageSize, sortOrder);
});

$("#btnSearch").click(function () {
    GetJudgement(currentPageNumber, currentPageSize, sortOrder);
});

$("#btnReset").click(function () {

    $('#txtName').val('');
    $('#txtCode').val('');
    $('#txtFileNo').val('')

    GetJudgement(currentPageNumber, currentPageSize, sortOrder);
});


$("#btnAscending").click(function () {
    $("#btnDescending").removeClass("active");
    $("#btnAscending").addClass("active");
    sortOrder = 1;//for Ascending
    GetJudgement(currentPageNumber, currentPageSize, sortOrder);
});


$("#btnDescending").click(function () {
    $("#btnAscending").removeClass("active");
    $("#btnDescending").addClass("active");
    sortOrder = 2;//for Descending
    GetJudgement(currentPageNumber, currentPageSize, sortOrder);
});
$('#ddlSort').change(function () {
    GetJudgement(currentPageNumber, currentPageSize, sortOrder);
});
function GetJudgement(pageNumber, pageSize, order) {
    var param = GetSearchParam(pageNumber, pageSize, order);
    HttpPost(`/Judgement/List`, 'html', param, function (response) {
        $('#divJudgementTable').html("");
        $('#divJudgementTable').html(response);
    });
}

function GetSearchParam(pageNumber, pageSize, sortOrder) {
    var model = {
        letterReferenceNo: $('#txtFileNo').val(),
        AllotmentNo: $('#txtReferenceNo').val(),
        subject: $('#txtSubject').val(),
        sortBy: $("#ddlSort").children("option:selected").val(),
        sortOrder: parseInt(sortOrder),
        pageSize: parseInt(pageSize),
        pageNumber: parseInt(pageNumber)


    }
    return model;
}

function onPaging(pageNo) {
    GetJudgement(parseInt(pageNo), parseInt(currentPageSize), sortOrder);
    currentPageNumber = pageNo;
}

function onChangePageSize(pageSize) {
    GetJudgement(parseInt(currentPageNumber), parseInt(pageSize), sortOrder);
    currentPageSize = pageSize;
}
