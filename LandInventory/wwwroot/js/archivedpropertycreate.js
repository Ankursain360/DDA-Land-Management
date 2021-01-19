var currentPageNumber = 1;
var currentPageSize = 5;
var sortOrder = 1;//default Ascending 

$(document).ready(function () {
    $(".linkdisabled").click(function () {
        return false;
    });
    GetDetails(currentPageNumber, currentPageSize, sortOrder);
})
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
    $('#DepartmentId').val('0').trigger('change');
    $('#ZoneId').val('0').trigger('change');
    $('#DivisionId').val('0').trigger('change');
    $('#LocalityId').val('0').trigger('change');
    $('#InventoriedInId').val('0').trigger('change')
    $('#ClassificationOfLandId').val('0').trigger('change');
    $('#PlannedUnplannedLand').val('0').trigger('change');
    GetDetails(currentPageNumber, currentPageSize, sortOrder);
});
function GetDetails(pageNumber, pageSize, sortOrder) {
    var param = GetSearchParam(pageNumber, pageSize, sortOrder);
    debugger
    HttpPost(`/ArchivedProperty/GetDetails`, 'html', param, function (response) {
        $('#LoadReportView').html("");
        $('#LoadReportView').html(response);
    });
}
function GetSearchParam(pageNumber, pageSize, sortOrder) {
    debugger;
    var model = {
        name: "test",
        sortBy: $("#ddlSort").children("option:selected").val(),
        sortOrder: parseInt(sortOrder),
        pageSize: pageSize,
        pageNumber: pageNumber,
        departmentId: parseInt($('#DepartmentId option:selected').val()),
        zoneId: parseInt($('#ZoneId option:selected').val()),
        divisionId: parseInt($('#DivisionId option:selected').val()),
        inventoriedId: parseInt($('#InventoriedInId').val()),
        classificationOfLandId: parseInt($('#ClassificationOfLandId').val()),
        plannedUnplannedLand: $('#PlannedUnplannedLand').val(),
        Id: parseInt($('#LocalityId option:selected').val())
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
//Bind Zone Dropdown from Department
function GetZoneList(id) {
    HttpGet(`/ArchivedProperty/GetZoneList/?departmentId=${id}`, 'json', function (response) {
        var html = '<option value="0">All</option>';
        for (var i = 0; i < response.length; i++) {
            html = html + '<option value=' + response[i].id + '>' + response[i].name + '</option>';
        }
        $("#ZoneId").html(html);
    });
};
//Bind Divison Dropdown from zone
function GetDivisionList(id) {
    HttpGet(`/ArchivedProperty/GetDivisionList/?zoneId=${id}`, 'json', function (response) {
        var html = '<option value="0">All</option>';
        for (var i = 0; i < response.length; i++) {
            html = html + '<option value=' + response[i].id + '>' + response[i].name + '</option>';
        }
        $("#DivisionId").html(html);
    });
};
function GetLocalityList(id) {
    debugger;
    HttpGet(`/ArchivedProperty/GetLocalityList/?divisionId=${id}`, 'json', function (response) {
        var html = '<option value="0">All</option>';
        for (var i = 0; i < response.length; i++) {
            html = html + '<option value=' + response[i].id + '>' + response[i].name + '</option>';
        }
        $("#LocalityId").html(html);
    });
};