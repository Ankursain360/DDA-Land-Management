var currentPageNumber = 1;
var currentPageSize = 5;
var sortOrder = 1;//default Ascending 

$(document).ready(function () {
    GetRate(sortOrder);
});
$("#btnSearch").click(function () {
    GetRate(sortOrder);
});

$("#btnReset").click(function () {
    $('#txtProperty').val('');
    GetRate(sortOrder);
}); 

$("#btnAscending").click(function () {
    $("#btnDescending").removeClass("active");
    $("#btnAscending").addClass("active");
    sortOrder = 1;//for Ascending
    GetRate(sortOrder);
});

$("#btnDescending").click(function () {
    $("#btnAscending").removeClass("active");
    $("#btnDescending").addClass("active");
    sortOrder = 2;//for Descending
    GetRate(sortOrder);
});
$('#ddlSort').change(function () {
    GetRate(sortOrder);
});
function GetRate(sortOrder) {
    var param = GetSearchParam(sortOrder);
    HttpPost(`/Rate/List`, 'html', param, function (response) {
        $('#divTable').html("");
        $('#divTable').html(response);
    });

   
}

function GetSearchParam(sortOrder) {
    var model = {
        name: "rate",
        property: $('#txtProperty').val(),
        sortBy: $("#ddlSort").children("option:selected").val(),
        sortOrder: parseInt(sortOrder)
    };
    return model;
}

