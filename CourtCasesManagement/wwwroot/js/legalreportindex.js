
var currentPageNumber = 1;
var currentPageSize = 10;
$(document).ready(function () {
    $("#btnGenerate").click(function () {
        debugger;
        var result = ValidateForm();
          if (result) {
              GetDetails(currentPageNumber, currentPageSize);
        }
       
    });

   
});

function GetDetails(pageNumber, pageSize) {
    var param = GetSearchParam(pageNumber, pageSize);
    debugger
    HttpPost(`/LegalReport/GetDetails`, 'html', param, function (response) {
        $('#LoadReportView').html("");
        $('#LoadReportView').html(response);
    });
}

function GetSearchParam(pageNumber, pageSize) {
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
        pageSize: parseInt(pageSize),
        pageNumber: parseInt(pageNumber),
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
        ToDate: toDate
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