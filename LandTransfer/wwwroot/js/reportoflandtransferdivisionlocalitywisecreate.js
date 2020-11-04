
var currentPageNumber = 1;
var currentPageSize = 10;
$(function () {
    $("#btnGenerate").click(function () {
        debugger;
        var result = ValidateForm();
        if (result) {

            var departmentid = $('#DepartmentId option:selected').val();
            var zoneId = $('#ZoneId option:selected').val();
            var divisionId = $('#DivisionId option:selected').val();

            var localityid = $('#LocalityId option:selected').val();


            GetReport(currentPageNumber, currentPageSize, departmentid, zoneId, divisionId, localityid)
        }

    });

    $(".linkdisabled").click(function () {
        return false;
    });
});

function onChangeDepartment(id) {
    HttpGet(`/ReportofLandTransferDivisionLocalityWise/GetZoneList/?DepartmentId=${id}`, 'json', function (response) {
        var html = '<option value="0">All</option>';
        for (var i = 0; i < response.length; i++) {
            html = html + '<option value=' + response[i].id + '>' + response[i].name + '</option>';
        }
        $("#ZoneId").html(html);
        $("#DivisionId").html('<option value="0">All</option>');
        $("#LocalityId").html('<option value="0">All</option>');
        $("#ZoneId").select2('val', '0');
        $("#DivisionId").select2('val', '0');
        $("#LocalityId").select2('val', '0');
    });
};
function onChangeZone(id) {
    HttpGet(`/ReportofLandTransferDivisionLocalityWise/GetDivisionList/?ZoneId=${id}`, 'json', function (response) {
        var html = '<option value="0">All</option>';
        for (var i = 0; i < response.length; i++) {
            html = html + '<option value=' + response[i].id + '>' + response[i].name + '</option>';
        }
        $("#DivisionId").html(html);
        $("#LocalityId").html('<option value="0">All</option>');
    });
};
function onChangeDivision(id) {
    HttpGet(`/ReportofLandTransferDivisionLocalityWise/GetLocalityList/?DivisionId=${id}`, 'json', function (response) {
        var html = '<option value="0">All</option>';
        for (var i = 0; i < response.length; i++) {
            html = html + '<option value=' + response[i].id + '>' + response[i].name + '</option>';
        }
        $("#LocalityId").html(html);
    });
};



function GetReport(pageNumber, pageSize, departmentid, zoneid, divisionid, localityid) {
    var param = GetSearchParam(pageNumber, pageSize, departmentid, zoneid, divisionid, localityid);
    HttpPost(`/ReportofLandTransferDivisionLocalityWise/List`, 'html', param, function (response) {
        $('#LoadReportView').html("");
        $('#LoadReportView').html(response);
    });
}

function GetSearchParam(pageNumber, pageSize, departmentid, zoneid, divisionid, localityid) {
    var model = {
        name: "test",
        pageSize: pageSize,
        pageNumber: pageNumber,
        departmentid: parseInt(departmentid),
        zoneid: parseInt(zoneid),
        divisionid: parseInt(divisionid),
        localityid: parseInt(localityid)
    }
    return model;
}

function onPaging(pageNo) {
    pageNo = parseInt(pageNo);
    var departmentid = $('#DepartmentId option:selected').val();
    var zoneid = $('#ZoneId option:selected').val();
    var divisionid = $('#DivisionId option:selected').val();
    var localityid = $('#LocalityId option:selected').val();
    GetReport(currentPageNumber, currentPageSize, departmentid, zoneid, divisionid, localityid);
    currentPageNumber = pageNo;
}

function onChangePageSize(pageSize) {
    pageSize = parseInt(pageSize);
    var departmentid = $('#DepartmentId option:selected').val();
    var zoneid = $('#ZoneId option:selected').val();
    var divisionid = $('#DivisionId option:selected').val();
    var localityid = $('#LocalityId option:selected').val();
    GetReport(currentPageNumber, currentPageSize, departmentid, zoneid, divisionid, localityid);
    currentPageSize = pageSize;
}

