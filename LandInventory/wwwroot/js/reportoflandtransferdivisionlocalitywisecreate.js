﻿var currentPageNumber = 1;
var currentPageSize = 5;
var sortby = 1;//default Ascending 

$(document).ready(function () {
    $("#btnGenerate").click(function () {
        var result = ValidateForm();
        //var IsValid = $("#frmUserPersonalInfo").valid(); 
        debugger;
        var StartDate = $('#txtFromDate').val();
        if (StartDate == "") {
            checkresult = false;
            $("#txtFromDateMsg").show();
        } else {
            $("#txtFromDateMsg").hide();
            checkresult = true;
        }
        var EndDate = $('#txtToDate').val();
        if (EndDate == "") {
            checkresult = false;
            $("#txtToDateMsg").show();
        } else {
            $("#txtToDateMsg").hide();
            checkresult = true;
        }

        if (StartDate == "" || EndDate == "") {

            checkresult = false;
        }
        else {
            checkresult = true;
        }
        if (checkresult) {
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
    HttpPost(`/ReportofLandTransferDivisionLocalityWise/List`, 'html', param, function (response) {
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
        StartDate: FromDate,
        EndDate: ToDate,
        sortBy: $("#ddlSort").children("option:selected").val(),
        sortOrder: parseInt(sortOrder),

    }
    return model;
}


$("#btnAscending").click(function () {
    debugger;
    $("#btnDescending").removeClass("active");
    $("#btnAscending").addClass("active");
    sortby = 1;//for Ascending
    var fromDate = (($('#txtFromDate').val()));
    var todate = (($('#txtToDate').val())); 
    if (todate != '' && fromDate != '') {

        GetDetails(currentPageNumber, currentPageSize, sortby);
    }
    else {
        alert('Please enter FromDate and ToDate');
    }
   
});


$("#btnDescending").click(function () {
    debugger;
    $("#btnAscending").removeClass("active");
    $("#btnDescending").addClass("active");
    sortby = 2;//for Descending
    var fromDate = (($('#txtFromDate').val()));
    var todate = (($('#txtToDate').val())); 
    if (todate != '' && fromDate != '') {

        GetDetails(currentPageNumber, currentPageSize, sortby);
    }
    else {
        alert('Please enter FromDate and ToDate');
    }
});



$("#btnReset").click(function () {
    //document.getElementById("VillageId").selectedIndex = "";

    location.reload();



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
    HttpGet(`/ReportofLandTransferDivisionLocalityWise/GetZoneList/?DepartmentId=${id}`, 'json', function (response) {
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

