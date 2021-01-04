var currentPageNumber = 1;
var currentPageSize = 10;

$(document).ready(function () {
    GetStructure(currentPageNumber, currentPageSize);
});

$("#btnSearch").click(function () {
    GetStructure(currentPageNumber, currentPageSize);
});

$("#btnReset").click(function () {
   
    $('#txtName').val('');
  
    GetStructure(currentPageNumber, currentPageSize);
});
function GetStructure(pageNumber, pageSize) {
    var param = GetSearchParam(pageNumber, pageSize);
    HttpPost(`/Structure/List`, 'html', param, function (response) {
        $('#divStructureTable').html("");
        $('#divStructureTable').html(response);
    });
}

function GetSearchParam(pageNumber, pageSize) {
    var model = {
       // name: "test",
        name: $('#txtName').val(),
       
        pageSize: parseInt(pageSize),
        pageNumber: parseInt(pageNumber)
    }
    return model;
}

function onPaging(pageNo) {
    GetStructure(pageNo, currentPageSize);
    currentPageNumber = pageNo;
}

function onChangePageSize(pageSize) {
    GetStructure(currentPageNumber, pageSize);
    currentPageSize = pageSize;
}

