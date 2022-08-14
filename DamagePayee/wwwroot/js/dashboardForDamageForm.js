var currentPageNumber = 1;
var currentPageSize = 5;

var sortOrder = 1;

$(document).ready(function () {
    $("#btnGenerate").click(function () {
        debugger;
        var result = ValidateForm();
        if (result) {
            GetDamageDashboard(currentPageNumber, currentPageSize, sortOrder);

        }

    });
});


function GetDamageDashboard(pageNumber, pageSize, sortOrder) {
    var param = GetSearchParam(pageNumber, pageSize, sortOrder);
    HttpPost(`/DashboardForDamageForm/GetDetails`, 'html', param, function (response) {
        $('#divDamageDashboard').html("");
        $('#divDamageDashboard').html(response);
    });
}

$("#btnReset").click(function () {
    $('#todate').val(Date.now).add;
    $('#fromDate').val(Date.now(-30)).add;
   
});

function GetSearchParam(pageNumber, pageSize, sortOrder) {
    debugger;

    var model = {
        toDate: $('#todate').val(),
        fromDate: $('#fromDate').val(),
        sortBy: $("#ddlSort").children("option:selected").val(),
        sortOrder: parseInt(sortOrder),
        pageSize: parseInt(pageSize),
        pageNumber: parseInt(pageNumber),

    }
    return model;
}

function onPaging(pageNo) {
    GetDamageDashboard(currentPageNumber, currentPageSize, sortOrder);
}

function onChangePageSize(pageSize) {
    GetDamageDashboard(currentPageNumber, currentPageSize, sortOrder);
}
