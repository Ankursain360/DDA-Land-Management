
var currentPageNumber = 1;
var currentPageSize = 5;
var sortOrder = 1;//default Ascending 
$(document).ready(function () {
    $("#btnGenerate").click(function () {
        debugger;
        var result = ValidateForm();
          if (result) {
              GetDetails(currentPageNumber, currentPageSize, sortOrder);
        }
       
    }); 
});

function GetExcel(pageNumber, pageSize, order) {
    var param = GetSearchParam(pageNumber, pageSize, order);
    HttpPost(`/LegalReport/LegalReportList`, 'html', param, function (response) {
        var a = document.createElement("a");
        a.target = '_blank';
        a.href = '/LegalReport/download';
        a.click();
    });
}

$("#btndownload").click(function () {
    GetExcel(currentPageNumber, currentPageSize, sortOrder);
});

$("#btnAscending").click(function () {
    $("#btnDescending").removeClass("active");
    $("#btnAscending").addClass("active");
    sortOrder = 1;//for Ascending
    GetDetails(currentPageNumber, currentPageSize, sortOrder);
});

$("#btnDescending").click(function () {
    $("#btnAscending").removeClass("active");
    $("#btnDescending").addClass("active");
    sortOrder = 2;//for Descending
    GetDetails(currentPageNumber, currentPageSize, sortOrder);
});

$('#ddlSort').change(function () {
    GetDetails(currentPageNumber, currentPageSize, sortOrder);
});



function GetDetails(pageNumber, pageSize, sortOrder) {
    var param = GetSearchParam(pageNumber, pageSize, sortOrder);
    debugger
    HttpPost(`/LegalReport/GetDetails`, 'html', param, function (response) {
        $('#LoadReportView').html("");
        $('#LoadReportView').html(response);
    });
}

function GetSearchParam(pageNumber, pageSize, sortOrder) {
    debugger;
   
    var fileNo = $('#Id option:selected').val();
    var CaseNo = $('#CaseNO option:selected').val();
    var ContemptOfCourt = $('#ContemptOfCourt option:selected').val();
    var CourtType = $('#CourtTypeId option:selected').val();
    var CaseStatus = $('#CaseStatusId option:selected').val();
    var Zone = $('#ZoneId option:selected').val();
    var Locality = $('#LocalityId option:selected').val();
    var StayInterimGranted = $('#StayInterimGranted option:selected').val();
    var Judgement = $('#Judgement option:selected').val();
    var fromDate = $('#txtFromDate').val();
    var toDate = $('#txtToDate').val();
       
   
    var model = {
        name: "test",
        PageSize: parseInt(pageSize),
        PageNumber: parseInt(pageNumber),
        FileNo: parseInt(fileNo),
        CaseNo: parseInt(CaseNo),
        ContemptOfCourt: parseInt(ContemptOfCourt),
        CourtType: parseInt(CourtType),
        CaseStatus: parseInt(CaseStatus),
        Zone: parseInt(Zone),
        Locality: parseInt(Locality),
        StayInterimGranted: parseInt(StayInterimGranted),
        Judgement: parseInt(Judgement),
        FromDate: fromDate,
        ToDate: toDate,
        sortBy: $("#ddlSort").children("option:selected").val(),
        sortOrder: parseInt(sortOrder)
    }
    return model;
}
function onPaging(pageNo) {
    GetDetails(parseInt(pageNo), parseInt(currentPageSize), sortOrder);
    currentPageNumber = pageNo;
}

function onChangePageSize(pageSize) {
    GetDetails(parseInt(currentPageNumber), parseInt(pageSize), sortOrder);
    currentPageSize = pageSize;
}




//Bind dependent Dropdown 



function GetLocalityList(id) {
    debugger;

    HttpGet(`/LegalReport/GetLocalityList/?zoneId=${id}`, 'json', function (response) {
        var html = '<option value="0">All</option>';
        for (var i = 0; i < response.length; i++) {
            html = html + '<option value=' + response[i].id + '>' + response[i].name + '</option>';
        }
        $("#LocalityId").html(html);
    });
};

function GetCourtCaseNoList(id) {
    HttpGet(`/LegalReport/GetCourtCaseNoList/?filenoId=${id}`, 'json', function (response) {
        var html = '<option value="0">All</option>';
        for (var i = 0; i < response.length; i++) {
            html = html + '<option value=' + response[i].id + '>' + response[i].courtCaseNo + '</option>';
        }
        $("#CaseNO").html('');
        $("#CaseNO").html(html);
    });
};