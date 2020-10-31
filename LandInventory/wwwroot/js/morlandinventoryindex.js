var currentPageNumber = 1;
var currentPageSize = 10;

$(document).ready(function () {
    GetDetails(currentPageNumber, currentPageSize);
});

$("#btnSearch").click(function () {
    GetDetails(currentPageNumber, currentPageSize);
});

$("#btnReset").click(function () {
    $('#DepartmentId').val('0').trigger('change');
    $('#ZoneId').val('0').trigger('change');
    $('#DivisionId').val('0').trigger('change');
    $('#InventoriedInId').val('0').trigger('change')
    $('#ClassificationOfLandId').val('0').trigger('change');
    $('#PlannedUnplannedLand').val('0').trigger('change');
    GetDetails(currentPageNumber, currentPageSize);
});

function GetDetails(pageNumber, pageSize) {
    var param = GetSearchParam(pageNumber, pageSize);
    HttpPost(`/MORLandInventory/List`, 'html', param, function (response) {
        $('#divTable').html("");
        $('#divTable').html(response);
    });
}

function GetSearchParam(pageNumber, pageSize) {
    var model = {
        name: "test",
        departmentId: parseInt($('#DepartmentId').val()),
        zoneId: parseInt($('#ZoneId').val()),
        divisionId: parseInt($('#DivisionId').val()),
        inventoriedId: parseInt($('#InventoriedInId').val()),
        classificationOfLandId: parseInt($('#ClassificationOfLandId').val()),
        plannedUnplannedLand: $('#PlannedUnplannedLand').val(),
        pageSize: pageSize,
        pageNumber: pageNumber
    }
    return model;
}

function onPaging(pageNo) {
    GetDetails(parseInt(pageNo), parseInt(currentPageSize));
    currentPageNumber = pageNo;
}

function onChangePageSize(pageSize) {
    GetDetails(parseInt(currentPageNumber), parseInt(pageSize));
    currentPageSize = pageSize;
}

function GetZoneList(id) {
    debugger;
    HttpGet(`/PropertyRegistration/GetZoneList/?departmentId=${id}`, 'json', function (response) {
        var html = '<option value="0">All</option>';
        for (var i = 0; i < response.length; i++) {
            html = html + '<option value=' + response[i].id + '>' + response[i].name + '</option>';
        }
        $("#ZoneId").val('0').trigger('change');
        $("#ZoneId").html(html);
        $("#DivisionId").val('0').trigger('change');
    });
};

//Bind Divison and Locality Dropdown from Department
function GetDivisionList(id) {

    HttpGet(`/PropertyRegistration/GetDivisionList/?zoneId=${id}`, 'json', function (response) {
        var html = '<option value="0">All</option>';
        for (var i = 0; i < response.length; i++) {
            html = html + '<option value=' + response[i].id + '>' + response[i].name + '</option>';
        }
        $("#DivisionId").val('0').trigger('change');
        $("#DivisionId").html(html);
    });
};