var currentPageNumber = 1;
var currentPageSize = 5;
var sortOrder = 1;//default Ascending 
$(function () {

    GetReport(currentPageNumber, currentPageSize, sortOrder);
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

$("#btnReset").click(function () {
    $('#DepartmentId').val('0').trigger('change');
    $('#ZoneId').val('0').trigger('change');
    $('#DivisionId').val('0').trigger('change');
    $('#LocalityId').val('0').trigger('change')
    GetDetails(currentPageNumber, currentPageSize, sortOrder);
});

function GetReport(pageNumber, pageSize, sortOrder) {
    var param = GetSearchParam(pageNumber, pageSize, sortOrder);
    HttpPost(`/ReportofLandTransferDivisionLocalityWise/List`, 'html', param, function (response) {
        $('#LoadReportView').html("");
        $('#LoadReportView').html(response);
    });
}
$("#btnAscending").click(function () {
    $("#btnDescending").removeClass("active");
    $("#btnAscending").addClass("active");
    sortOrder = 1;//for Ascending
    GetReport(currentPageNumber, currentPageSize, sortOrder);
});

$("#btnDescending").click(function () {
    $("#btnAscending").removeClass("active");
    $("#btnDescending").addClass("active");
    sortOrder = 2;//for Descending
    GetReport(currentPageNumber, currentPageSize, sortOrder);
});
$('#ddlSort').change(function () {
    GetReport(currentPageNumber, currentPageSize, sortOrder);
});
function GetSearchParam(pageNumber, pageSize, sortOrder) {
    var model = {
        name: "test",
        sortBy: $("#ddlSort").children("option:selected").val(),
        sortOrder: parseInt(sortOrder),
        pageSize: pageSize,
        pageNumber: pageNumber,
        departmentid: parseInt($('#DepartmentId option:selected').val()),
        zoneid: parseInt($('#ZoneId option:selected').val()),
        divisionid: parseInt($('#DivisionId option:selected').val()),
        localityid: parseInt($('#LocalityId option:selected').val())
    }
    return model;
}

function onPaging(pageNo) {
    pageNo = parseInt(pageNo);
    GetReport(currentPageNumber, currentPageSize, sortOrder);
    currentPageNumber = pageNo;
}

function onChangePageSize(pageSize) {
    pageSize = parseInt(pageSize);
    GetReport(currentPageNumber, currentPageSize, sortOrder);
    currentPageSize = pageSize;
}

