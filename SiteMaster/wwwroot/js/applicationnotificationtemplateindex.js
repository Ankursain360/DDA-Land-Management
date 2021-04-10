var currentPageNumber = 1;
var currentPageSize = 5;
var sortOrder = 1;//default Ascending 

$(document).ready(function () {
    GetApplicationNotification(currentPageNumber, currentPageSize, sortOrder);
});


function GetApplicationNotification(pageNumber, pageSize, order) {
    var param = GetSearchParam(pageNumber, pageSize, order);
    HttpPost(`/ApplicationNotificationTemplate/List`, 'html', param, function (response) {
        $('#divApplicationNotificationTemplateTable').html("");
        $('#divApplicationNotificationTemplateTable').html(response);
    });
}
$("#btnSearch").click(function () {
    GetApplicationNotification(currentPageNumber, currentPageSize, sortOrder);
});

$("#btnDescending").click(function () {
    $("#btnAscending").removeClass("active");
    $("#btnDescending").addClass("active");
    sortOrder = 2;//for Descending
    GetApplicationNotification(currentPageNumber, currentPageSize, sortOrder);
});

$("#btnAscending").click(function () {
    $("#btnDescending").removeClass("active");
    $("#btnAscending").addClass("active");
    sortOrder = 1;//for Ascending 
    GetApplicationNotification(currentPageNumber, currentPageSize, sortOrder);

});

$("#btnReset").click(function () {
    $('#txtName').val('');  
    GetApplicationNotification(currentPageNumber, currentPageSize, sortOrder);
});
$('#ddlSort').change(function () {
    GetApplicationNotification(currentPageNumber, currentPageSize, sortOrder);
});


function GetSearchParam(pageNumber, pageSize, sortOrder) {
    var model = {
        name: $('#txtName').val(),
        sortBy: $("#ddlSort").children("option:selected").val(),
        sortOrder: parseInt(sortOrder),
        pageSize: parseInt(pageSize),
        pageNumber: parseInt(pageNumber)
    }
    return model;
}

function onPaging(pageNo) {
    GetApplicationNotification(parseInt(pageNo), parseInt(currentPageSize), sortOrder);
    currentPageNumber = pageNo;
}

function onChangePageSize(pageSize) {
    GetApplicationNotification(parseInt(currentPageNumber), parseInt(pageSize), sortOrder);
    currentPageSize = pageSize;
}

