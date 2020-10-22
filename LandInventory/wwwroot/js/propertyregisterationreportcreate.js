var currentPageNumber = 1;
var currentPageSize = 10;
$(document).ready(function () {

    // GetDetails(currentPageNumber, currentPageSize);


    $("#btnGenerate").click(function () {
        debugger;
        var param = GetSearchParam(currentPageNumber, currentPageSize);
        HttpPost(`/PropertyInventoryReport/GetDetails`, 'html', param, function (response) {
            $('#LoadReportView').html("");
            $('#LoadReportView').html(response);
        });
    });

});
//Bind Zone Dropdown from Department
function GetZoneList(id) {
    debugger;
    HttpGet(`/PropertyRegistration/GetZoneList/?departmentId=${id}`, 'json', function (response) {
        var html = '<option value="0">All</option>';
        for (var i = 0; i < response.length; i++) {
            html = html + '<option value=' + response[i].id + '>' + response[i].name + '</option>';
        }
        $("#ZoneId").html(html);
    });
};

//Bind Divison and Locality Dropdown from Department
function GetDivisionList(id) {

    HttpGet(`/PropertyRegistration/GetDivisionList/?zoneId=${id}`, 'json', function (response) {
        var html = '<option value="0">All</option>';
        for (var i = 0; i < response.length; i++) {
            html = html + '<option value=' + response[i].id + '>' + response[i].name + '</option>';
        }
        $("#DivisionId").html(html);
    });

    HttpGet(`/PropertyRegistration/GetLocalityList/?zoneId=${id}`, 'json', function (response) {
        var html = '<option value="0">All</option>';
        for (var i = 0; i < response.length; i++) {
            html = html + '<option value=' + response[i].id + '>' + response[i].name + '</option>';
        }
        $("#LocalityId").html(html);
    });
};


function GetDetails(pageNumber, pageSize) {
    var param = GetSearchParam(pageNumber, pageSize);
    HttpPost(`/PropertyInventoryReport/GetDetails`, 'html', param, function (response) {
        $('#LoadReportView').html("");
        $('#LoadReportView').html(response);
    });
}

function GetSearchParam(pageNumber, pageSize) {
    var classificationOfLandId = $('#ClassificationOfLandId option:selected').val();
    var departmentid = $('#DepartmentId option:selected').val();
    var zoneId = $('#ZoneId option:selected').val();
    var divisionId = $('#DivisionId option:selected').val();
    var localityId = $('#LocalityId option:selected').val();
    var plannedUnplannedLand = $('#PlannedUnplannedLand option:selected').val();
    var mainLandUseId = $('#MainLandUseId option:selected').val();
    var litigationid = $('#LitigationStatus option:selected').val();
    var encroachedid = $('#Encroached option:selected').val();
    var test = [];

    var model = {
        name: "test",
        pageSize: parseInt(pageSize),
        pageNumber: parseInt(pageNumber),
        classificationofland: parseInt(classificationOfLandId),
        department: parseInt(departmentid),
        zone: parseInt(zoneId),
        division: parseInt(divisionId),
        locality: parseInt(localityId),
        plannedUnplannedLand: plannedUnplannedLand,
        mainLandUse: parseInt(mainLandUseId),
        litigation: parseInt(litigationid),
        encroached: parseInt(encroachedid)
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