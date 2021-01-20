//$(document).ready(function () {
//    $(function () {
//        $("#btnGenerate").click(function () {
//            debugger;
//            var url = '/InspectionRegisterPeriodReport/GetDetails';

//            var departmentid = $('#DepartmentId option:selected').val();
//            var zoneId = $('#ZoneId option:selected').val();
//            var divisionId = $('#DivisionId option:selected').val();
//            var localityid = $('#LocalityId option:selected').val();
//            var fromDate = $('#txtFromDate').val();
//            var toDate = $('#txtToDate').val();
//            if (departmentid != '' && departmentid != undefined && fromDate != '' && toDate != '' && departmentid != null && fromDate != null && toDate != null) {

//                $('#LoadReportView').empty();
//                $('#LoadReportView').load(url, {
//                    department: departmentid, zone: zoneId, division: divisionId,

//                    locality: localityid,
//                    fromdate: fromDate,
//                    todate: toDate

//                }).hide().fadeIn(1000);;
//            }
//            else {
//                alert('Please Fill All Fields');
//            }
//            });
//    });
//})

















//$(function () {
//    $(".linkdisabled").click(function () {
//        return false;
//    });
//});
//function onChangeDepartment(id) {
//    HttpGet(`/InspectionRegisterPeriodReport/GetZoneList/?DepartmentId=${id}`, 'json', function (response) {
//        var html = '<option value="">Select</option>';
//        for (var i = 0; i < response.length; i++) {
//            html = html + '<option value=' + response[i].id + '>' + response[i].name + '</option>';
//        }
//        $("#ZoneId").html(html);
//        $("#DivisionId").html('<option value="">Select</option>');
//        $("#LocalityId").html('<option value="">Select</option>');
//    });
//};
//function onChangeZone(id) {
//    HttpGet(`/InspectionRegisterPeriodReport/GetDivisionList/?ZoneId=${id}`, 'json', function (response) {
//        var html = '<option value="">Select</option>';
//        for (var i = 0; i < response.length; i++) {
//            html = html + '<option value=' + response[i].id + '>' + response[i].name + '</option>';
//        }
//        $("#DivisionId").html(html);
//        $("#LocalityId").html('<option value="">Select</option>');
//    });
//};
//function onChangeDivision(id) {
//    HttpGet(`/InspectionRegisterPeriodReport/GetLocalityList/?DivisionId=${id}`, 'json', function (response) {
//        var html = '<option value="">Select</option>';
//        for (var i = 0; i < response.length; i++) {
//            html = html + '<option value=' + response[i].id + '>' + response[i].name + '</option>';
//        }
//        $("#LocalityId").html(html);
//    });
//};
var currentPageNumber = 1;
var currentPageSize = 10;
var sortby = 1;//default Ascending 

$(document).ready(function () {
    $("#btnGenerate").click(function () {
        var result = ValidateForm();
        var departmentid = $('#DepartmentId option:selected').val();
        var zoneid = $('#ZoneId option:selected').val();
        var divisionid = $('#DivisionId option:selected').val();
        var localityid = $('#LocalityId option:selected').val();
        var FromDate = $('#txtFromDate').val();
        var ToDate = $('#txtToDate').val();
        if (result) {
            GetDetails(currentPageNumber, currentPageSize, sortby);
        }
    });
    $(".linkdisabled").click(function () {
        return false;
    });
});
function GetDetails(pageNumber, pageSize, order) {
    var param = GetSearchParam(pageNumber, pageSize, order);
    debugger
    HttpPost(`/InspectionRegisterPeriodReport/GetDetails`, 'html', param, function (response) {
        $('#LoadReportView').html("");
        $('#LoadReportView').html(response);
    });
}

function GetSearchParam(pageNumber, pageSize, sortOrder) {
    debugger;
    var departmentid = $('#DepartmentId option:selected').val();
    var zoneid = $('#ZoneId option:selected').val();
    var divisionid = $('#DivisionId option:selected').val();
    var localityid = $('#LocalityId option:selected').val();
    var FromDate = $('#txtFromDate').val();
    var ToDate = $('#txtToDate').val();
    var model = {
        name: "report",
        pageSize: parseInt(pageSize),
        pageNumber: parseInt(pageNumber),
        departmentId: parseInt(departmentid),
        zoneId: parseInt(zoneid),
        divisionId: parseInt(divisionid),
        localityId: parseInt(localityid),
        fromDate: FromDate,
        toDate: ToDate,
        sortBy: $("#ddlSort").children("option:selected").val(),
        sortOrder: parseInt(sortOrder),

    }
    return model;
}


$("#btnAscending").click(function () {
    $("#btnDescending").removeClass("active");
    $("#btnAscending").addClass("active");
    sortby = 1;//for Ascending
    GetDetails(currentPageNumber, currentPageSize, sortby);
});


$("#btnDescending").click(function () {
    $("#btnAscending").removeClass("active");
    $("#btnDescending").addClass("active");
    sortby = 2;//for Descending
    GetDetails(currentPageNumber, currentPageSize, sortby);
});

$("#btnReset").click(function () {
    $('#DepartmentId').val('0').trigger('change');
    $('#ZoneId').val('0').trigger('change');
    $('#DivisionId').val('0').trigger('change');
    $('#LocalityId').val('0').trigger('change');
    $('#txtFromDate').val('');
    $('#txtToDate').val('');


    //GetDetails(currentPageNumber, currentPageSize, sortby);

});

function onPaging(pageNo) {
    GetDetails(parseInt(pageNo), parseInt(currentPageSize), sortby);
    currentPageNumber = pageNo;
}

function onChangePageSize(pageSize) {
    GetDetails(parseInt(currentPageNumber), parseInt(pageSize), sortby);
    currentPageSize = pageSize;
}
function onChangeDepartment(id) {
    HttpGet(`/InspectionRegisterPeriodReport/GetZoneList/?DepartmentId=${id}`, 'json', function (response) {
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
    HttpGet(`/InspectionRegisterPeriodReport/GetDivisionList/?ZoneId=${id}`, 'json', function (response) {
        var html = '<option value="0">All</option>';
        for (var i = 0; i < response.length; i++) {
            html = html + '<option value=' + response[i].id + '>' + response[i].name + '</option>';
        }
        $("#DivisionId").html(html);
        $("#LocalityId").html('<option value="0">All</option>');
    });
};
function onChangeDivision(id) {
    HttpGet(`/InspectionRegisterPeriodReport/GetLocalityList/?DivisionId=${id}`, 'json', function (response) {
        var html = '<option value="0">All</option>';
        for (var i = 0; i < response.length; i++) {
            html = html + '<option value=' + response[i].id + '>' + response[i].name + '</option>';
        }
        $("#LocalityId").html(html);
    });
};