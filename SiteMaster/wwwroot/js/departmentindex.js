var currentPageNumber = 1;
var currentPageSize = 10;

$(document).ready(function () {
    GetDepartment(currentPageNumber, currentPageSize);
});
$("#btnSearch").click(function () {
    GetDepartment(currentPageNumber, currentPageSize);
});

$("#btnReset").click(function () {
    $('#txtName').val('')
    GetDepartment(currentPageNumber, currentPageSize);
});

function GetDepartment(pageNumber, pageSize) {
    var param = GetSearchParam(pageNumber, pageSize);
    HttpPost(`/department/List`, 'html', param, function (response) {
        $('#divDepartmentTable').html("");
        $('#divDepartmentTable').html(response);
    });
}

//function GetSearchParam(pageNumber, pageSize) {
//    var model = {
//        name: "test",
//        pageSize: pageSize,
//        pageNumber: pageNumber
//    }
//    return model;
//}
function GetSearchParam(pageNumber, pageSize) {
    var model = {
        name: $('#txtName').val(),
        pageSize: parseInt(pageSize),
        pageNumber: parseInt(pageNumber)
    }
    return model;
}


function onPaging(pageNo) {
    GetDepartment(parseInt(pageNo), parseInt(currentPageSize));
    currentPageNumber = pageNo;
}

function onChangePageSize(pageSize) {
    GetDepartment(parseInt(currentPageNumber), parseInt(pageSize));
    currentPageSize = pageSize;
}