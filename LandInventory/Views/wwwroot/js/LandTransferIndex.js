var currentPageNumber = 1;
var currentPageSize = 10;

$(document).ready(function () {
    GetLandTransfer(currentPageNumber, currentPageSize);
});

function GetLandTransfer(pageNumber, pageSize) {
    var param = GetSearchParam(pageNumber, pageSize);
    HttpPost(`/LandTransfer/List`, 'html', param, function (response) {
        debugger;
        $('#divLandTransferTable').html("");
        $('#divLandTransferTable').html(response);
    });
}

$("#btnGenerate").click(function () {
    debugger;
    GetLandTransfer(currentPageNumber, currentPageSize);
});


function onChangePlanned(value) {

    if (value == 'Planned Land') {
        $('#DivLandUse').show();
        $('#divPlannedSelection').show();
        $("#divLayoutPlan").show();
        $('#divUnplannedSelection').hide();
        $('#Propertyregistration_KhasraNo').val('');
        $('#Propertyregistration_Colony').val('');
        $('#Propertyregistration_Sector').val('');
        $('#Propertyregistration_Block').val('');
        $('#Propertyregistration_Pocket').val('');
        $('#Propertyregistration_PlotNo').val('');
        $("#Propertyregistration_LocalityId").val('0').trigger('change');
        callSelect2();
    }
    else if (value == 'Unplanned Land') {
        $('#DivLandUse').hide();
        $('#divPlannedSelection').hide();
        $("#divLayoutPlan").hide();
        $('#divUnplannedSelection').show();
        $('#Propertyregistration_KhasraNo').val('');
        $('#Propertyregistration_Colony').val('');
        $('#Propertyregistration_Sector').val('');
        $('#Propertyregistration_Block').val('');
        $('#Propertyregistration_Pocket').val('');
        $('#Propertyregistration_PlotNo').val('');
        $("#Propertyregistration_LocalityId").val('0').trigger('change');
        callSelect2();
    }
    else {
        $('#DivLandUse').show();
        $('#divPlannedSelection').show();
        $("#divLayoutPlan").show();
        $('#divUnplannedSelection').show();
        $('#Propertyregistration_KhasraNo').val('');
        $('#Propertyregistration_Colony').val('');
        $('#Propertyregistration_Sector').val('');
        $('#Propertyregistration_Block').val('');
        $('#Propertyregistration_Pocket').val('');
        $('#Propertyregistration_PlotNo').val('');
        $("#Propertyregistration_LocalityId").val('0').trigger('change');
        callSelect2();
    }
};
function GetZoneList(id) {
    debugger;
    HttpGet(`/LandTransfer/GetZoneList/?departmentId=${id}`, 'json', function (response) {

        var html = '<option value="0">All</option>';
        for (var i = 0; i < response.length; i++) {
            html = html + '<option value=' + response[i].id + '>' + response[i].name + '</option>';
        }
        $("#Propertyregistration_ZoneId").val('0').trigger('change');
        $("#Propertyregistration_ZoneId").html(html);
        $("#Propertyregistration_DivisionId").val('0').trigger('change');
        $("#Propertyregistration_LocalityId").val('0').trigger('change');
    });
};

//Bind Divison and Locality Dropdown from Department
function GetDivisionList(id) {

    HttpGet(`/LandTransfer/GetDivisionList/?zoneId=${id}`, 'json', function (response) {
        var html = '<option value="0">All</option>';
        for (var i = 0; i < response.length; i++) {
            html = html + '<option value=' + response[i].id + '>' + response[i].name + '</option>';
        }
        $("#Propertyregistration_DivisionId").val('0').trigger('change');
        $("#Propertyregistration_DivisionId").html(html);
    });

    HttpGet(`/LandTransfer/GetLocalityList/?zoneId=${id}`, 'json', function (response) {
        var html = '<option value="0">All</option>';
        for (var i = 0; i < response.length; i++) {
            html = html + '<option value=' + response[i].id + '>' + response[i].name + '</option>';
        }
        $("#Propertyregistration_LocalityId").val('0').trigger('change');
        $("#Propertyregistration_LocalityId").html(html);
    });
};
function GetSearchParam(pageNumber, pageSize) {
    debugger;
    var classificationOfLandId = $('#Propertyregistration_ClassificationOfLandId option:selected').val();
    var departmentid = $('#Propertyregistration_DepartmentId option:selected').val();
    var zoneId = $('#Propertyregistration_ZoneId option:selected').val();
    var divisionId = $('#Propertyregistration_DivisionId option:selected').val();
    var plannedUnplannedLand = $('#Propertyregistration_PlannedUnplannedLand option:selected').val();
    var test = [];

    var model = {
        name: "test",
        pageSize: parseInt(pageSize),
        pageNumber: parseInt(pageNumber),
        classificationofland: parseInt(classificationOfLandId),
        department: parseInt(departmentid),
        zone: parseInt(zoneId),
        division: parseInt(divisionId),
        plannedUnplannedLand: plannedUnplannedLand,
        inventoriedIn: parseInt($('#Propertyregistration_InventoriedInId').val()),
    }
    test.push(model);
    return model;
}

function callSelect2() {
    $("select").select2({
        placeholder: "Select",
        allowClear: true
    });
}
function onPaging(pageNo) {
    GetLandTransfer(parseInt(pageNo), currentPageSize);
    currentPageNumber = pageNo;
}

function onChangePageSize(pageSize) {
    GetLandTransfer(currentPageNumber, parseInt(pageSize));
    currentPageSize = pageSize;
}
