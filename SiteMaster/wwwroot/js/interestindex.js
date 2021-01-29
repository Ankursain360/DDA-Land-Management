var currentPageNumber = 1;
var currentPageSize = 5;
var sortOrder = 1;//default Ascending 


$(document).ready(function () {
    GetInterest(sortOrder);
});

$("#btnSearch").click(function () {
    GetInterest(sortOrder);
});

$("#btnReset").click(function () {
    $('#txtProperty').val('');
    GetInterest(sortOrder);
});

$("#btnAscending").click(function () {
    $("#btnDescending").removeClass("active");
    $("#btnAscending").addClass("active");
    sortOrder = 1;//for Ascending
    GetInterest(sortOrder);
});

$("#btnDescending").click(function () {
    $("#btnAscending").removeClass("active");
    $("#btnDescending").addClass("active");
    sortOrder = 2;//for Descending
    GetInterest(sortOrder);
});
$('#ddlSort').change(function () {
    GetInterest(sortOrder);
});
function GetInterest(sortOrder) {
    var param = GetSearchParam(sortOrder);
    HttpPost(`/Interest/List`, 'html', param, function (response) {
        $('#divTable').html("");
        $('#divTable').html(response);
    });
}

function GetSearchParam(sortOrder) {
    var model = {
        name: "interest",
        property: $('#txtProperty').val(),
        sortBy: $("#ddlSort").children("option:selected").val(),
        sortOrder: parseInt(sortOrder)
    };
    return model;
}




