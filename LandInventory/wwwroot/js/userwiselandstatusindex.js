var currentPageNumber = 1;
var currentPageSize = 10;
var sortby = 1;//default Ascending 

$(document).ready(function () {
    $(".linkdisabled").click(function () {
        return false;
    });

    GetDetails(currentPageNumber, currentPageSize, sortby);
});
 

$("#btnGenerate").click(function () {
    GetDetails(currentPageNumber, currentPageSize, sortby);
});
function GetDetails(pageNumber, pageSize, order) {
    var param = GetSearchParam(pageNumber, pageSize, order);
    debugger
    HttpPost(`/UserWiseLandStatus/List`, 'html', param, function (response) {
        $('#LoadReportView').html("");
        $('#LoadReportView').html(response);
    });
}

function GetSearchParam(pageNumber, pageSize, sortOrder) {
    debugger;
    var departmentid = $('#DepartmentId option:selected').val();
    var zoneid = $('#ZoneId option:selected').val();
    var divisionid = $('#DivisionId option:selected').val();


    var model = {
        name: "report",
        pageSize: parseInt(pageSize),
        pageNumber: parseInt(pageNumber),
        departmentId: parseInt(departmentid),
        zoneId: parseInt(zoneid),
        divisionId: parseInt(divisionid),
     
  
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
    HttpGet(`/UserWiseLandStatus/GetZoneList/?DepartmentId=${id}`, 'json', function (response) {
        var html = '<option value="0">All</option>';
        for (var i = 0; i < response.length; i++) {
            html = html + '<option value=' + response[i].id + '>' + response[i].name + '</option>';
        }
        $("#ZoneId").html(html);
        $("#DivisionId").html('<option value="0">All</option>');
      
    });
};
function onChangeZone(id) {
    HttpGet(`/UserWiseLandStatus/GetDivisionList/?ZoneId=${id}`, 'json', function (response) {
        var html = '<option value="0">All</option>';
        for (var i = 0; i < response.length; i++) {
            html = html + '<option value=' + response[i].id + '>' + response[i].name + '</option>';
        }
        $("#DivisionId").html(html);
       
    });
};


