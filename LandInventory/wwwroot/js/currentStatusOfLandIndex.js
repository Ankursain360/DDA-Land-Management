var currentPageNumber = 1;
var currentPageSize = 10;

$(document).ready(function () {
    GetLandTransfer(currentPageNumber, currentPageSize);
});

function GetLandTransfer(pageNumber, pageSize) {
    var param = GetSearchParam(pageNumber, pageSize);
    HttpPost(`/CurrentStatusOfHandedOverTakenOverLand/List`, 'html', param, function (response) {
        $('#divCurrentStatusOfLandTable').html("");
        $('#divCurrentStatusOfLandTable').html(response);
    });
}

function GetSearchParam(pageNumber, pageSize) {
    var model = {
        name: "test",
        pageSize: pageSize,
        pageNumber: pageNumber
    }
    return model;
}

function onPaging(pageNo) {
    GetLandTransfer(parseInt(pageNo), currentPageSize);
    currentPageNumber = pageNo;
}

function onChangePageSize(pageSize) {
    GetLandTransfer(currentPageNumber, parseInt(pageSize));
    currentPageSize = pageSize;
}











//var currentPageNumber = 1;
//var currentPageSize = 10;
//$(document).ready(function () {
//    $("#btnGenerate").click(function () {
//        var departmentId = $('#DepartmentId option:selected').val();
//        var zoneId = $('#ZoneId option:selected').val();
//        var divisionId = $('#DivisionId option:selected').val();
//        var Id = $('#Id option:selected').val();

//        GetDetails(currentPageNumber, currentPageSize, departmentId, zoneId, divisionId, Id);
//    });
//    $(".linkdisabled").click(function () {
//        return false;
//    });
//});

//function GetDetails(pageNumber, pageSize, departmentId, zoneId, divisionId, localityId) {
//    var param = GetSearchParam(pageNumber, pageSize, departmentId, zoneId, divisionId, localityId == undefined ? 0 : localityId);
//    debugger
//    HttpPost(`/CurrentStatusOfHandedOverTakenOverLand/List`, 'html', param, function (response) {
//        $('#divCurrentStatusOfLandTable').html("");
//        $('#divCurrentStatusOfLandTable').html(response);
//    });
//}

//function GetSearchParam(pageNumber, pageSize, departmentId, zoneId, divisionId, Id) {
//    debugger;
//    var model = {
//        name: "test",
//        pageSize: parseInt(pageSize),
//        pageNumber: parseInt(pageNumber),
//        departmentId: parseInt(departmentId),
//        zoneId: parseInt(zoneId),
//        divisionId: parseInt(divisionId),
//        Id: parseInt(Id)
//    }
//    return model;
//}
//function onPaging(pageNo) {
//    var departmentId = $('#DepartmentId option:selected').val();
//    var zoneId = $('#ZoneId option:selected').val();
//    var divisionId = $('#DivisionId option:selected').val();
//    var Id = $('#Id option:selected').val();
//    GetDetails(parseInt(pageNo), parseInt(currentPageSize), departmentId, zoneId, divisionId, Id);
//    currentPageNumber = pageNo;
//}

//function onChangePageSize(pageSize) {
//    var departmentId = $('#DepartmentId option:selected').val();
//    var zoneId = $('#ZoneId option:selected').val();
//    var divisionId = $('#DivisionId option:selected').val();
//    var Id = $('#Id option:selected').val();
//    GetDetails(parseInt(currentPageNumber), parseInt(pageSize), departmentId, zoneId, divisionId, Id);
//    currentPageSize = pageSize;
//}
////Bind Zone Dropdown from Department
//function GetZoneList(id) {
//    HttpGet(`/CurrentStatusOfHandedOverTakenOverLand/GetZoneList/?departmentId=${id}`, 'json', function (response) {
//        var html = '<option value="0">All</option>';
//        for (var i = 0; i < response.length; i++) {
//            html = html + '<option value=' + response[i].id + '>' + response[i].name + '</option>';
//        }
//        $("#ZoneId").html(html);
//    });
//};
////Bind Divison Dropdown from zone
//function GetDivisionList(id) {

//    HttpGet(`/CurrentStatusOfHandedOverTakenOverLand/GetDivisionList/?zoneId=${id}`, 'json', function (response) {
//        var html = '<option value="0">All</option>';
//        for (var i = 0; i < response.length; i++) {
//            html = html + '<option value=' + response[i].id + '>' + response[i].name + '</option>';
//        }
//        $("#DivisionId").html(html);
//    });
//};
////Bind Locality Dropdown from division
//function GetLocalityList(id) {
//    debugger;

//    HttpGet(`/CurrentStatusOfHandedOverTakenOverLand/GetLocalityList/?divisionId=${id}`, 'json', function (response) {
//        var html = '<option value="0">All</option>';
//        for (var i = 0; i < response.length; i++) {
//            html = html + '<option value=' + response[i].id + '>' + response[i].name + '</option>';
//        }
//        $("#LocalityId").html(html);
//    });
//}; 