var currentPageNumber = 1;
var currentPageSize = 5;
var sortOrder = 1;//default Ascending 

$(document).ready(function () {

    GetDetails(currentPageNumber, currentPageSize, sortOrder);

});
$("#btnGenerate").click(function () {
    GetDetails(currentPageNumber, currentPageSize, sortOrder)
});
function GetDetails(currentPageNumber, currentPageSize, sortOrder) {
    var param = GetSearchParam(currentPageNumber, currentPageSize, sortOrder);
    HttpPost(`/DMSRetriveFile/GetDetails`, 'html', param, function (response) {
        $('#LoadReportView').html("");
        $('#LoadReportView').html(response);
    });
}

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
function GetSearchParam(pageNumber, pageSize, sortOrder) {
    var test = [];
    var model = {
        sortBy: $("#ddlSort").children("option:selected").val(),
        sortOrder: parseInt(sortOrder),
        pageSize: pageSize,
        pageNumber: pageNumber,
        Department: parseInt(($('#DepartmentId option:selected').val())),
        Locality: parseInt(($('#LocalityId option:selected').val())),
        Khasra: parseInt(($('#KhasraNoId option:selected').val())),
        FileNo: (($('#FileNo').val())),
        PropertyNo: (($('#PropertyNo').val())),
        AlmirahNo: (($('#AlmirahNo').val())),
        Title: (($('#Title').val()))
    }
    test.push(model);
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
function callSelect2() {
    $("select").select2({
        placeholder: "Select",
        allowClear: true
    });
}

$("#btnReset").click(function () {
    $('#DepartmentId').val('0').trigger('change');
    $('#LocalityId').val('0').trigger('change');
    $('#KhasraNoId').val('0').trigger('change');
    $('#FileNo').val('');
    $('#PropertyNo').val('');
    $('#AlmirahNo').val('');
    $('#Title').val('');
    GetDetails(currentPageNumber, currentPageSize, sortOrder);
});