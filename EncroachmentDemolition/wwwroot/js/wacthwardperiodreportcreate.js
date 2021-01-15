var currentPageNumber = 1;
var currentPageSize = 10;
$(document).ready(function () {
    $("#btnGenerate").click(function () {
        debugger;
        var result = ValidateForm();
        var localityid = $('#LocalityId option:selected').val();
        var fromDate = $('#txtFromDate').val();
        var toDate = $('#txtToDate').val();
        if (selected.val() == 0) {
            var Date_val = $('#LocalityId').val();
            if (Date_val == "") {
                checkresult = false;
                $("#LocalityID").show();
            } else {
                checkresult = true;
                $("#LocalityID").hide();
            }
        }

        //if (localityid != '' && localityid != undefined && fromDate != '' && toDate != '' && localityid != null && fromDate != null && toDate != null) {
        if (result) {
            GetDetails(currentPageNumber, currentPageSize);
        }
        //}
        //else {
        //    alert('Please Fill All Fields');
        //}
        
    });

    $(".linkdisabled").click(function () {
        return false;
    });
});

function GetDetails(pageNumber, pageSize) {
    var param = GetSearchParam(pageNumber, pageSize);
    debugger
    HttpPost(`/WacthWardPeriodReport/GetDetails`, 'html', param, function (response) {
        $('#LoadReportView').html("");
        $('#LoadReportView').html(response);
    });
}

function GetSearchParam(pageNumber, pageSize) {
    debugger;
    var localityid = $('#LocalityId option:selected').val();
    var FromDate = $('#txtFromDate').val();
    var ToDate = $('#txtToDate').val();
    var model = {
        name: "test",
        pageSize: parseInt(pageSize),
        pageNumber: parseInt(pageNumber),
        localityId: parseInt(localityid),
        fromDate: FromDate,
        toDate: ToDate
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