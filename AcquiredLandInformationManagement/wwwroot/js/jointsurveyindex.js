var currentPageNumber = 1;
var currentPageSize = 5;
var sortOrder = 1;//default Ascending 



$(document).ready(function () {
    GetDetails(currentPageNumber, currentPageSize, sortOrder);
});

function GetExcel(pageNumber, pageSize, order) {
    var param = GetSearchParam(pageNumber, pageSize, order);
    HttpPost(`/JointSurvey/JointSurveyList`, 'html', param, function (response) {
        var a = document.createElement("a");
        a.target = '_blank';
        a.href = '/JointSurvey/download';
        a.click();
    });
}

$("#btndownload").click(function () {
    GetExcel(currentPageNumber, currentPageSize, sortOrder);
});

$("#btnGenerate").click(function () {
    GetDetails(currentPageNumber, currentPageSize, sortOrder);
});

$("#btnAscending").click(function () {
    $("#btnDescending").removeClass("active");
    $("#btnAscending").addClass("active");
    sortOrder = 1;//for Ascending
    GetDetails(currentPageNumber, currentPageSize, sortOrder);
});

$("#btnDescending").click(function () {
    $("#btnAscending").removeClass("active");
    $("#btnDescending").addClass("active");
    sortOrder = 2;//for Descending
    GetDetails(currentPageNumber, currentPageSize, sortOrder);
});
$('#ddlSort').change(function () {
    GetDetails(currentPageNumber, currentPageSize, sortOrder);
});


$("#btnReset").click(function () {
    $('#Name').val('');
    GetDetails(currentPageNumber, currentPageSize, sortOrder);
});


function GetDetails(pageNumber, pageSize) {
    var param = GetSearchParam(pageNumber, pageSize, sortOrder);
    HttpPost(`/JointSurvey/List`, 'html', param, function (response) {
        $('#divGetJointSurvey').html("");
        $('#divGetJointSurvey').html(response);
    });

}




function GetSearchParam(pageNumber, pageSize, sortOrder) {
    var model = {
        VillageName: ($('#VName').val()),
        KhasraName: ($('#KName').val()),
        sortBy: $("#ddlSort").children("option:selected").val(),
        sortOrder: parseInt(sortOrder),
        pageSize: pageSize,
        pageNumber: pageNumber
    }
    return model;
}

function onPaging(pageNo) {
    GetDetails(parseInt(pageNo), parseInt(currentPageSize), sortOrder);
    currentPageNumber = pageNo;
}

function onChangePageSize(pageSize) {
    GetDetails(parseInt(currentPageNumber), parseInt(pageSize), sortOrder);
    currentPageSize = pageSize;
}
$("#btnReset").click(function () {
    $('#VName').val('');
    $('#KName').val('');
    GetDetails(currentPageNumber, currentPageSize, sortOrder);
});
