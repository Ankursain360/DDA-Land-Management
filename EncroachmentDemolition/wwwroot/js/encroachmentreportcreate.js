﻿var currentPageNumber = 1;
var currentPageSize = 10;
var sortby = 1;//default Ascending 

$(document).ready(function () {
    $("#btnGenerate").click(function () {
        var result = ValidateForm();
        if (result) {
            GetDetails(currentPageNumber, currentPageSize, sortby);
        }
    });
    $(".linkdisabled").click(function () {
        return false;
    });
});


function GetExcel(pageNumber, pageSize, order) {
    var param = GetSearchParam(pageNumber, pageSize, order);
    HttpPost(`/EncroachmentReport/Encroachmentreporlist`, 'html', param, function (resorce) {
        var a = document.createElement("a");
        a.target = '_blank';
        a.href = '/EncroachmentReport/download';
        a.click();
    });
}
$("#btnDownload").click(function () {
    GetExcel(currentPageNumber, currentPageSize, sortby);
})
function GetDetails(pageNumber, pageSize, order) {
    var param = GetSearchParam(pageNumber, pageSize, order);
    debugger
    HttpPost(`/EncroachmentReport/GetDetails`, 'html', param, function (response) {
        $('#LoadReportView').html("");
        $('#LoadReportView').html(response);
    });
}

$('#ddlSort').change(function () {
    GetDetails(currentPageNumber, currentPageSize, sortby);
});

function GetSearchParam(pageNumber, pageSize, sortOrder) {
    debugger;
    var departmentid = $('#DepartmentId option:selected').val();
    var zoneid = $('#ZoneId option:selected').val();
    var divisionid = $('#DivisionId option:selected').val();
    var localityid = $('#LocalityId option:selected').val();
    var model = {
        name: "report",
        pageSize: parseInt(pageSize),
        pageNumber: parseInt(pageNumber),
        departmentId: parseInt(departmentid),
        zoneId: parseInt(zoneid),
        divisionId: parseInt(divisionid),
        localityId: parseInt(localityid),
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
  
  
    GetDetails(currentPageNumber, currentPageSize, sortby);

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