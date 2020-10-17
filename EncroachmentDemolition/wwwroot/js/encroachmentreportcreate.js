var currentPageNumber = 1;
var currentPageSize = 10;
$(document).ready(function () {
    $("#btnGenerate").click(function () {
        debugger
        GetDetails(currentPageNumber, currentPageSize);
    });
    $(".linkdisabled").click(function () {
        return false;
    });
});
function GetDetails(pageNumber, pageSize) {
    var param = GetSearchParam(pageNumber, pageSize);
    debugger
    HttpPost(`/EncroachmentReport/GetDetails`, 'html', param, function (response) {
        $('#LoadReportView').html("");
        $('#LoadReportView').html(response);
    });
}

function GetSearchParam(pageNumber, pageSize) {
    debugger;
    var departmentid = $('#DepartmentId option:selected').val();
    var zoneid = $('#ZoneId option:selected').val();
    var divisionid = $('#DivisionId option:selected').val();
    var localityid = $('#LocalityId option:selected').val();
    var model = {
        name: "test",
        pageSize: parseInt(pageSize),
        pageNumber: parseInt(pageNumber),
        departmentId: parseInt(departmentid),
        zoneId: parseInt(zoneid),
        divisionId: parseInt(divisionid),
        localityId: parseInt(localityid),
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
function onChangeDepartment(id) {
    HttpGet(`/EncroachmentReport/GetZoneList/?DepartmentId=${id}`, 'json', function (response) {
        var html = '<option value="0">All</option>';
        for (var i = 0; i < response.length; i++) {
            html = html + '<option value=' + response[i].id + '>' + response[i].name + '</option>';
        }
        $("#ZoneId").html(html);
        $("#DivisionId").html('<option value="0">All</option>');
        $("#LocalityId").html('<option value="0">All</option>');
    });
};
function onChangeZone(id) {
    HttpGet(`/EncroachmentReport/GetDivisionList/?ZoneId=${id}`, 'json', function (response) {
        var html = '<option value="0">All</option>';
        for (var i = 0; i < response.length; i++) {
            html = html + '<option value=' + response[i].id + '>' + response[i].name + '</option>';
        }
        $("#DivisionId").html(html);
        $("#LocalityId").html('<option value="0">All</option>');
    });
};
function onChangeDivision(id) {
    HttpGet(`/EncroachmentReport/GetLocalityList/?DivisionId=${id}`, 'json', function (response) {
        var html = '<option value="0">All</option>';
        for (var i = 0; i < response.length; i++) {
            html = html + '<option value=' + response[i].id + '>' + response[i].name + '</option>';
        }
        $("#LocalityId").html(html);
    });
};