var currentPageNumber = 1;
var currentPageSize = 10;

$(document).ready(function () {
    GetDamagePayeeRegistration(currentPageNumber, currentPageSize);
});

function GetDamagePayeeRegistration(pageNumber, pageSize) {
    var param = GetSearchParam(pageNumber, pageSize);
    HttpPost(`/DamagePayeeRegistration/List`, 'html', param, function (response) {
        $('#divdamagepayeeregistrationTable').html("");
        $('#divdamagepayeeregistrationTable').html(response);
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
    GetDamagePayeeRegistration(pageNo, currentPageSize);
    currentPageNumber = pageNo;
}

function onChangePageSize(pageSize) {
    GetDamagePayeeRegistration(currentPageNumber, pageSize);
    currentPageSize = pageSize;
}

