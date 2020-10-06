var currentPageNumber = 1;
var currentPageSize = 10;

$(document).ready(function () {
    GetJointSurvey(currentPageNumber, currentPageSize);
});

function GetJointSurvey(pageNumber, pageSize) {
    var param = GetSearchParam(pageNumber, pageSize);
    HttpPost(`/JointSurvey/List`, 'html', param, function (response) {
        $('#divGetJointSurvey').html("");
        $('#divGetJointSurvey').html(response);
    });
}

function GetSearchParam(pageNumber, pageSize) {
    var model = {
        name: "test",
        pageSize: pageSize,
        pageNumber: pageNumber
    }
    return model;
}

function onPaging(pageNo) {
    GetJointSurvey(pageNo, currentPageSize);
    currentPageNumber = pageNo;
}

function onChangePageSize(pageSize) {
    GetJointSurvey(currentPageNumber, pageSize);
    currentPageSize = pageSize;
}
