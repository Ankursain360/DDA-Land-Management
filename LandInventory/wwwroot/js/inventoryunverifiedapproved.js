var currentPageNumber = 1;
var currentPageSize = 10;
$(document).ready(function () {
    GetDetails(pageNumber, pageSize);
    $("#btnGenerate").click(function () {
        debugger;
        var param = GetSearchParam(currentPageNumber, currentPageSize);
        HttpPost(`/InventoryUnverified/GetDetails`, 'html', param, function (response) {
            $('#LoadReportView').html("");
            $('#LoadReportView').html(response);
        });
    });
});
//Bind Zone Dropdown from Department
function GetZoneList(id) {
    debugger;
    HttpGet(`/InventoryUnverified/GetZoneList/?departmentId=${id}`, 'json', function (response) {

        var html = '<option value="0">All</option>';
        for (var i = 0; i < response.length; i++) {
            html = html + '<option value=' + response[i].id + '>' + response[i].name + '</option>';
        }
        $("#ZoneId").val('0').trigger('change');
        $("#ZoneId").html(html);
        $("#DivisionId").val('0').trigger('change');
        $("#LocalityId").val('0').trigger('change');
    });
};

//Bind Divison and Locality Dropdown from Department
function GetDivisionList(id) {

    HttpGet(`/InventoryUnverified/GetDivisionList/?zoneId=${id}`, 'json', function (response) {
        var html = '<option value="0">All</option>';
        for (var i = 0; i < response.length; i++) {
            html = html + '<option value=' + response[i].id + '>' + response[i].name + '</option>';
        }
        $("#DivisionId").val('0').trigger('change');
        $("#DivisionId").html(html);
    });

    HttpGet(`/InventoryUnverified/GetLocalityList/?zoneId=${id}`, 'json', function (response) {
        var html = '<option value="0">All</option>';
        for (var i = 0; i < response.length; i++) {
            html = html + '<option value=' + response[i].id + '>' + response[i].name + '</option>';
        }
        $("#LocalityId").val('0').trigger('change');
        $("#LocalityId").html(html);
    });
};


function GetDetails(pageNumber, pageSize) {
    var param = GetSearchParam(pageNumber, pageSize);
    HttpPost(`/InventoryUnverified/GetDetails`, 'html', param, function (response) {
        $('#LoadReportView').html("");
        $('#LoadReportView').html(response);
    });
}

function GetSearchParam(pageNumber, pageSize) {
    debugger;
    var departmentId = $('#DepartmentId option:selected').val();
    var zoneId = $('#ZoneId option:selected').val();
    var divisionId = $('#DivisionId option:selected').val();
    var Id = $('#LocalityId option:selected').val();
    var test = [];

    var model = {
        name: "test",
        pageSize: parseInt(pageSize),
        pageNumber: parseInt(pageNumber),
        departmentId: parseInt(departmentId),
        zoneId: parseInt(zoneId),
        divisionId: parseInt(divisionId),
        inventoriedId: parseInt($('#InventoriedInId').val()),
        classificationOfLandId: parseInt($('#ClassificationOfLandId').val()),
        plannedUnplannedLand: $('#PlannedUnplannedLand').val(),
      //  unverifiedverfied: 0,
        Id: parseInt(Id)
    }
    test.push(model);
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


function callSelect2() {
    $("select").select2({
        placeholder: "Select",
        allowClear: true
    });
}